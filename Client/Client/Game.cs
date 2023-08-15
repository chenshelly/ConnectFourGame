using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using Client.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

using System.Threading;
using static Client.Form1;
using static Client.Models.Ball;

using System.Data.SqlClient;
using System.Diagnostics;

namespace Client
{
     public class Game
    {
        public int idGame { get; set; }
        public int playerId { get; set; }
        public List<Player> playerList;

        public static HttpClient client=new HttpClient();
		int BoardWidth, BoardHeight;
        public bool player1;
        public bool player2;
        public Color pieceColor;
        public List<Ball> balls = new List<Ball>();
		
	    public enum state { empty = 0, player1 = 1, player2 = 2 };
        public state[,] board = new state[7, 6];
        
        public List<int> full = new List<int> { 5, 5, 5, 5, 5, 5, 5 };
        int X;
        int stepServer { get; set; }
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		Stopwatch stopWatch = new Stopwatch();
		public readonly string apiUrl = "https://localhost:44382/";
     
        string _connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=gameDB;Integrated Security=True";
		public Game(int x, int y, Color pcolor)
		{

			state STATE = new state();
			if (pcolor == Color.Red)
			{
				player1 = true;
				player2 = false;
				pieceColor = Color.Red;
				STATE = state.player1;
			}
			else if (pcolor == Color.Yellow)
			{
				player2 = true;
				player1 = false;
				STATE = state.player2;

			}

			board[x / 100, y / 100] = STATE;
			X = x / 100;


		}

		public Game()
		{


			client.BaseAddress = new Uri("https://localhost:44382/");
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));


			BoardWidth = 700;
			BoardHeight = 600;
			player1 = true;
			player2 = false;
			pieceColor = Color.Red;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					board[i, j] = state.empty;
				}
			}


		}
		public void updateNumWins()
        {

            for(int i=0; i < playerList.Count; i++)
            {
                if (playerList[i].Id== playerId)
                {
                    playerList[i].numWins++;
                }
            }

            // Update the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE TblPlayers SET numWins = numWins + 1 WHERE Id = @playerId";
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@playerId", playerId);
               
                updateCommand.ExecuteNonQuery();

                connection.Close();
            }

            UpdateDurationTimeGame();

		}
		public void UpdateDurationTimeGame()
		{
			TimeSpan myDateResult = DateTime.Now.TimeOfDay;

			for (int i = 0; i < playerList.Count; i++)
			{
				if (playerList[i].Id == playerId)
				{
					playerList[i].EndTime = myDateResult;
                    TimeSpan duration = myDateResult- playerList[i].StartTime;
					
					// Update the database
					using (SqlConnection connection = new SqlConnection(_connectionString))
					{
						connection.Open();

						string updateQuery = "UPDATE TblPlayers SET duration = @duration WHERE Id = @playerId";
						SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
						updateCommand.Parameters.AddWithValue("@duration", duration); 
						updateCommand.Parameters.AddWithValue("@playerId", playerId);

						updateCommand.ExecuteNonQuery();

						connection.Close();
					}
				}
			}

			
		}
		public state getStatePlayer()
        {
            if (player1)
            {
                return state.player1;
            }
            else if(player2)
            {
                return state.player2;
            }
            return state.empty;
        }
       

        public async Task<int> getStepAsync(string path)
        {

            int step = 0;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
				step = await response.Content.ReadAsAsync<int>();


            }
            return step;
        }
        private async void getStep_()
        {
           
			string path = "api/TblPlayers/step";
            try
            {
                int res = await getStepAsync(path);
                stepServer =res;
            

            }

            catch (HttpRequestException e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
    
            

		}
       
		public void timerBalls(Ball b)
		{
		
			b.Move(b.Position);
			
			
		}
		private void start_timer()
		{
			timer.Enabled = true;
			timer.Start();
		}
		
		
	
		
		//Method for resting the board
		public void Reset()
        {
            full = new List<int>(7) { 5, 5, 5, 5, 5, 5, 5 };
            player1 = true;
            player2 = false;
            pieceColor = Color.Red;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    board[i, j] = state.empty;
                }
            }
        }
       
        //Method to draw blank game board
        public void drawBoard(PaintEventArgs e)
        {
            Pen line = new Pen(Color.Black);
            int lineXi = 0, lineXf = 700;
            int lineYi = 0, lineYf = 600;

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            for (int startY = 0; startY <= BoardWidth; startY += 100)
            {
                e.Graphics.DrawLine(line, startY, lineYi, startY, lineYf);
            }

            for (int startX = 0; startX <= BoardHeight; startX += 100)
            {
                e.Graphics.DrawLine(line, lineXi, startX, lineXf, startX);
            }

            for (int y = 0; y <= BoardHeight; y += 103)
            {
                for (int x = 0; x <= BoardWidth; x += 103)
                {
                    e.Graphics.FillEllipse(myBrush, new Rectangle(x, y, 75,75));
                }
            }
         
        }
           
        

        //Method that changes the players turn and game piece color
        public void playerTurn()
        {
            
            player1 = !player1;
            player2 = !player2;
            if (player1)
            {
                pieceColor = Color.Red;

            }
            else
            {
                pieceColor = Color.Yellow;
                Thread.Sleep(1000);
            }
        }
		private void create_ball(Graphics f,SolidBrush myBrush,Point point, int Radius)
		{
			start_timer();
			Ball ball = new Ball();
            ball.Position = point;
			ball.Draw(f, myBrush, point, Radius);
			balls.Add(ball);
           
			timerBalls(ball);
		}

		public void drawGamePiece(MouseEventArgs e, Graphics f)
        {
           
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(pieceColor);
            int xlocal = (e.X / 100);
           
            if (player1)
            {
                if (full[xlocal] >= 0)
                {

                    if (player1 && board[xlocal, full[xlocal]] == state.empty)
                    {

                        board[xlocal, full[xlocal]] = state.player1;
                        Point point = new Point();
						point.X = xlocal * 102;
						point.Y = full[xlocal] * 101;

						int Radius = 43;
                        timer.Interval = 50;
					
						create_ball(f, myBrush, point, Radius);
						stopWatch.Stop();
						full[xlocal]--;

                        playerTurn();



                    }

                }
            }
        
             getStep_();


			System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(pieceColor);
            int local = (this.stepServer / 100);

            if (player2)
            {
                if (full[local] >= 0)
                {

                    if (player2 && board[local, full[xlocal]] == state.empty)
                    {
                       board[local,full[local]] = state.player2;
						Point point = new Point();
						point.X = local * 101;
						point.Y = full[local] * 101;
						int Radius1 = 43;
						create_ball(f, myBrush1, point, Radius1);

						full[local]--;

                        playerTurn();

                    }

                }
            }
            

        }
        //Method to redraw the pieces after maximization
        public void redrawGamePiece(Graphics f)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(pieceColor);
            int xlocal = (X / 100);

            if (full[xlocal] >= 0)
            {

                if (player1)
                {
                    board[xlocal, full[xlocal]] = state.player1;
                    f.FillEllipse(myBrush, xlocal * 103, full[xlocal] * 103, 75, 75);
                }
                else if (player2)
                {
                    board[xlocal, full[xlocal]] = state.player2;
                    f.FillEllipse(myBrush, xlocal * 103, full[xlocal] * 103, 75, 75);
                }
            }
        }


        //Method to check if there is a winner
        public Color WinningPlayer()
        {
            bool RedPlayer = false;
            bool yellowPlayer = false;

            int colIndex = 6;
            int rowIndex = 5;

            //For loop to check vertical win
            for (int col = 0; col <= colIndex && !RedPlayer && !yellowPlayer; col++)
            {
                int RedWinner = 1;
                int yellowWinner = 1;
                for (int row = 0; row < rowIndex && !RedPlayer && !yellowPlayer; row++)
                {
                    if (board[col, row] != state.empty)
                    {

                        if ((board[col, row] == state.player1) && (board[col, row + 1] == state.player1))
                        {
                            RedWinner++;
                            if (RedWinner >= 4)
                                RedPlayer = true;
                        }
                        else if ((board[col, row] == state.player2) && (board[col, row + 1] == state.player2))
                        {
                            yellowWinner++;
                            if (yellowWinner >= 4)
                                yellowPlayer = true;
                        }
                        else
                        {
                            RedWinner = 1;
                            yellowWinner = 1;
                        }
                    }

                }
            }

            //For loop to check horizontal win
            for (int row = 0; row < colIndex && !RedPlayer && !yellowPlayer; row++)
            {
                int RedWinner = 1;
                int yellowWinner = 1;

                for (int col = 0; col < colIndex && !RedPlayer && !yellowPlayer; col++)
                {
                    if (board[col, row] != state.empty)
                    {


                        if ((board[col, row] == state.player1) && (board[col + 1, row] == state.player1))
                        {
                            RedWinner++;
                            if (RedWinner >= 4)
                                RedPlayer = true;
                        }
                        else if ((board[col, row] == state.player2) && (board[col + 1, row] == state.player2))
                        {
                            yellowWinner++;
                            if (yellowWinner >= 4)
                                yellowPlayer = true;
                        }
                        else
                        {
                            RedWinner = 1;
                            yellowWinner = 1;
                        }
                    }


                }
            }

            //For loop to check diagonal going up and right from starting piece
            for (int row = 0; row < rowIndex && !RedPlayer && !yellowPlayer; row++)
            {
                for (int col = 0; col < colIndex && !RedPlayer && !yellowPlayer; col++)
                    if (board[col, row] != state.empty)
                    {
                        int RedWinner = 1;
                        int yellowWinner = 1;

                        for (int i = 0; i + row < rowIndex && col - i >= 0; i++)
                        {
                            if ((board[col, row] == state.player1) && (board[col - i, row + i] == state.player1))
                            {
                                RedWinner++;
                                if (RedWinner >= 4)
                                    RedPlayer = true;
                            }
                            else if ((board[col, row] == state.player2) && (board[col - i, row + i] == state.player2))
                            {
                                yellowWinner++;
                                if (yellowWinner >= 4)
                                    yellowPlayer = true;
                            }
                            else
                            {
                                RedWinner = 1;
                                yellowWinner = 1;
                            }

                        }
                    }
            }

            //For loop to check diagonal win going up and to the left from starting piece
            for (int row = 0; row < rowIndex && !RedPlayer && !yellowPlayer; row++)
            {
                for (int col = 0; col < colIndex && !RedPlayer && !yellowPlayer; col++)
                {
                    if (board[col, row] != state.empty)
                    {
                        int RedWinner = 1;
                        int yellowWinner = 1;

                        for (int i = 0; i + row < rowIndex && col + i < rowIndex; i++)
                        {
                            if ((board[col, row] == state.player1) && (board[col + i, row + i] == state.player1))
                            {
                                RedWinner++;
                                if (RedWinner >= 4)
                                    RedPlayer = true;
                            }
                            else if ((board[col, row] == state.player2) && (board[col + i, row + i] == state.player2))
                            {
                                yellowWinner++;
                                if (yellowWinner >= 4)
                                    yellowPlayer = true;
                            }
                            else
                            {
                                RedWinner = 1;
                                yellowWinner = 1;
                            }
                        }
                    }
                }
            }
            if (RedPlayer)
            {
                updateNumWins();
                return Color.Red;
            }
                
            else if (yellowPlayer)
                return Color.Yellow;
            else
                return Color.Empty;
        }

		public int getX()
        {
            return this.X;
        }




    }



}
