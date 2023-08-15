using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;
using Client.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Runtime.Remoting.Contexts;
using Microsoft.SqlServer.Server;
using System.Diagnostics.Metrics;
using System.Numerics;
using static Client.Models.Ball;
using System.Threading;
using static Client.Game;

namespace Client
{
    public partial class Form1 : Form
    {
       public PaintEventArgs paint;
	    static HttpClient client = new HttpClient();
        public Game game1 = new Game();
		Player player = new Player();
        List<Game> pieces;
        private Point arrowPosition;
        int Xcoord;
        int Ycoord;
        private int currentColumn = 0; 
        private int currentRow = 0;
        private int timeLeftInSeconds = 60;
        int secondPause = 10;
      
       
        public Form1()
        {
            InitializeComponent();
            pieces = new List<Game>();
            arrowPosition = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2); 
            pictureBox1.Visible = false;
   
        }
		
		private void panel1_Paint(object sender, PaintEventArgs e)
        {
            game1.drawBoard(e);
            foreach (Game piece in pieces)
            {
             
                piece.redrawGamePiece(e.Graphics);
            }
        }

		static async Task<IEnumerable<Player>> GetPlayerAsync(string path)
		{
			IEnumerable<Player> product = null;
			HttpResponseMessage response = await client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				product = await response.Content.ReadAsAsync<IEnumerable<Player>>();
			}
			return product;
		}
		
        private  void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int column = e.X / (panel1.Width / 7);
            int row = e.Y / (panel1.Height / 6);
         
            currentColumn = column;
            currentRow = row;

            
            Color pcolor = new Color();
            Game piece = new Game(e.X, e.Y, pcolor);
            StartTimer();
            using (Graphics f = this.panel1.CreateGraphics())
            {
                game1.drawGamePiece(e, f);
                if (game1.player1)
                {
                    timeLeftInSeconds = 60;
                    timer1.Start();
                    lblTurn.Text = "Player 1's Turn";
                    lblTurn.ForeColor = Color.Red;
                    pcolor = Color.Yellow;
                    pieces.Add(piece);
               
                   
                }
                if(game1.player2)
                {
                    timeLeftInSeconds = 60;
                    timer1.Start();
                    lblTurn.Text = "Player 2's Turn";
                     lblTurn.ForeColor = Color.Yellow;  
                    showPlayerTurn(piece.pieceColor);
                    pcolor = Color.Red;
                    pieces.Add(piece);
                        



                }

            }

            if (game1.WinningPlayer() == Color.Red)
            {
                MessageBox.Show("Red Player Wins", "Red Beat Yellow", MessageBoxButtons.OK);
                game1.Reset();
                panel1.Invalidate();
            }
            else if (game1.WinningPlayer() == Color.Yellow)
            {
                MessageBox.Show("Yellow Player Wins", "Yellow Beat Red", MessageBoxButtons.OK);
                game1.Reset();
                panel1.Invalidate();
            }
        
        }

     
        public void showPlayerTurn(Color pColor)
        {
            if (pColor == Color.Red)
            {
                lblTurn.Text = "Player 1's Turn";
                lblTurn.ForeColor = Color.Red;
            }
            if(pColor == Color.Yellow)
            {
                lblTurn.Text = "Player 2's Turn";
                lblTurn.ForeColor = Color.Yellow;
            }
        }




        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            arrowPosition = e.Location;
            pictureBox1.Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int x_startAccess = panel1.Location.X;
            int x_endAccess = panel1.Width+ x_startAccess;
            int y_startAccess = 0;
            int y_endAccess = panel1.Location.Y - pictureBox1.Height / 2;

            if ((e.X > x_startAccess && e.X < x_endAccess) && (e.Y > y_startAccess && e.Y < y_endAccess))
            {

                pictureBox1.Location= new Point(e.X - pictureBox1.Width / 2, e.Y - pictureBox1.Height / 2);
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = true;
            }
           
        }

      

        private void timer2_Tick(object sender, EventArgs e)
        {
            waitingTime.Start();
            if (secondPause <= 0)
            {
                waitingTime.Stop();
                secondPause = 10;
           
            }
            else
            {

                secondPause--;
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            TblDindingNavigator.BindingSource = TblBindingSource;
            TblDataGridView.DataSource = TblBindingSource;

            client.BaseAddress = new Uri("https://localhost:44382/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

			StartTimer();
            if (game1.player2)
            {

               // EndTurn();
                waitingTime.Start();
                if(secondPause <= 0)
                {
                  
                    secondPause = 10;
                }
                else
                {
                    secondPause--;
                }
                StartTimer();

            }
        }

       
        private void Form1_BackgroundImageChanged(object sender, EventArgs e)
        {
            //this.BackgroundImage = Properties.Resources.background1;
        }

     
        public async Task<(int, int)> GetRandomCoordinateAsync()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/randomCoordinate");

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    string[] coordinates = responseBody.Split(',');
                    if (coordinates.Length == 2 && int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y))
                    {
                        return (x, y);
                    }
                    else
                    {
                        // The response format is invalid
                        throw new FormatException("Invalid response format.");
                    }
                    // var result = JsonConvert.DeserializeAnonymousType(responseBody, new { X = 0, Y = 0 });

                    //return (result.X, result.Y);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
               // return (0, 0); // or any default value
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
                // return (0, 0); // or any default value
            }
        }

       
        private async void button1_MouseClick(object sender, MouseEventArgs e)
        {
           
            Graphics f = this.panel1.CreateGraphics();
           
            (Xcoord, Ycoord) = await GetRandomCoordinateAsync();
           
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(game1.pieceColor);
            int xlocal = (Xcoord / 100);

            if (game1.player2)
            {
                
                
                if (game1.full[xlocal] >= 0)
                {

                    if (game1.player2 && game1.board[xlocal, game1.full[xlocal]] == state.empty)
                    {

                       
                        game1.board[xlocal, game1.full[xlocal]] = state.player2;
                        f.FillEllipse(myBrush, xlocal * 103, game1.full[xlocal] * 103, 75, 75);

                        game1.full[xlocal]--;

                        game1.playerTurn();


                
                    }

                }
            }
            

        }
       
        private void Timer1_Tick(object sender, EventArgs e)
        {
            labelTimer.Text = $"{timeLeftInSeconds / 60:00}:{timeLeftInSeconds % 60:00}";
            if (timeLeftInSeconds <= 0)
            {

                timeLeftInSeconds = 60;
                EndTurn();
  
            }
            else
            {
        
                timeLeftInSeconds--;
            }
        }
        public void StartTimer()
        {
            timer1.Start();
        }
        public void EndTurn()
        {
            timer1.Stop();
            timeLeftInSeconds = 60; // Reset the time to 1 minute for the next turn
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
           // if (game1.player2)
           // {
              //  timer2_Tick( sender,  e);
                
          //  }
        }

		private void TblBindingSource_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void timerBalls_Tick(object sender, EventArgs e)
		{
			

		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void panel1_MouseEnter(object sender, EventArgs e)
		{

		}

		private void pictureBoxRed_Move(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click_1(object sender, EventArgs e)
		{

		}

	}
}
