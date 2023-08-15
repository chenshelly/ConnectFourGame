using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Client.Models;
using System.Numerics;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Client
{
    public partial class Start : Form
    {
        Game game;
		 Player player = new Player();
        static HttpClient client = new HttpClient();
        public int idUser { get; set; }
		public int theIdGame { get; set; }
		public PLayersDataContext pLayerData = new PLayersDataContext();
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=gameDB;Integrated Security=True");

        public Start()
        {
            InitializeComponent();
			 game = new Game();
            game.playerList = new List<Player>();

		}
        private void updateGamesRoundPlayerDB(int playerId, int id_Game)
        {
			connection.Open();
			string updateQuery1 = "UPDATE TblPlayers SET GamesRound = GamesRound +1 WHERE Id = @playerId AND IdGame = @idGame";
            SqlCommand updateCommand = new SqlCommand(updateQuery1, connection);
            updateCommand.Parameters.AddWithValue("@playerId", playerId);
            updateCommand.Parameters.AddWithValue("@idGame", id_Game);
            updateCommand.ExecuteNonQuery();
			connection.Close();


		}
        private void textBoxIdUser_Validating(object sender, CancelEventArgs e)
        {
            string str = textBoxIdUser.Text;
            this.idUser = int.Parse(str);
        }

		
		private void Start_Click(object sender, EventArgs e)
        {
            string strId = textBoxIdUser.Text;
            idUser = int.Parse(strId);
       
            connection.Open();
                string query1 = "select count(*) from TblPlayers where Id =' " + this.idUser + "'";
                SqlCommand command1 = new SqlCommand(query1, connection);
                int check = (int)command1.ExecuteScalar();
                connection.Close();

            if (check != 1)
            {
                MessageBox.Show("You need to register on the site to start playing");
            }
            else
            {
                PlayerDataPopUp playerDetailsForm = new PlayerDataPopUp();
                connection.Open();
                string query2 = "select Name from TblPlayers where Id =' " + this.idUser + "'";
                SqlCommand command2 = new SqlCommand(query2, connection);
                string  name = (string)command2.ExecuteScalar();

                string query3 = "select PhoneNumber from TblPlayers where Id =' " + this.idUser + "'";
                SqlCommand command3 = new SqlCommand(query3, connection);
                int phone = (int)command3.ExecuteScalar();

                string query4 = "select Country from TblPlayers where Id =' " + this.idUser + "'";
                SqlCommand command4 = new SqlCommand(query4, connection);
                string country = (string)command4.ExecuteScalar();


				string query5 = "select IdGame from TblPlayers where Id =' " + this.idUser + "'";
				SqlCommand command5 = new SqlCommand(query5, connection);
				int idgame = (int)command5.ExecuteScalar();
                theIdGame = idgame;

				string query6 = "select duration from TblPlayers where Id =' " + this.idUser + "'";
				SqlCommand command6 = new SqlCommand(query6, connection);
				TimeSpan duration = (TimeSpan)command6.ExecuteScalar();
				
				TimeSpan start = duration;
				game.playerList.Add(new Player(idUser,name,phone,country,0, start));

               

				connection.Close();

                playerDetailsForm.LabelName = name;
                playerDetailsForm.labelId = idUser.ToString();
                playerDetailsForm.LabelPhoneNumber = phone.ToString(); 
                playerDetailsForm.LabelCountry = country;


                playerDetailsForm.ShowDialog();
                Form1 newGame = new Form1();
				newGame.game1 = game;
                newGame.game1.playerId = idUser;
				updateGamesRoundPlayerDB(idUser, idgame);
				newGame.ShowDialog();

            }
        }


		
		private void Start_Load(object sender, EventArgs e)
		{
            client.BaseAddress = new Uri("https://localhost:44382/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
	}
}
