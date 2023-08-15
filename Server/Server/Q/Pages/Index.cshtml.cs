using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Q.Models;
using System;
using System.Numerics;

namespace Q.Pages
{
    public class IndexModel : PageModel
    {
        private string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=gameDB;Integrated Security=True";
        private readonly ILogger<IndexModel> _logger;
        public readonly int SIZE = 1;
       
        public String Country { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public List<Player> Players { get; set; } = default!;
	


		public void OnGet()
        {
			
		}


		
		
		public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                foreach (Player player in Players)
                {
					int id = player.Id;
					string name = player.Name;
					int phoneNumberStr = player.PhoneNumber;
					string country = player.Country;

                 
                    InsertToTable(id, name, phoneNumberStr, country);


                    string res = string.Join("<br>", new List<string> { player.Id.ToString(), player.Name,
                                                            player.PhoneNumber.ToString(),player.Country
                                                });
                    Response.WriteAsync("<p>" + res + "</p>");
                }
            }
            return Page();
        }
       
        public void InsertToTable(int id, string name, int phoneNumber, string country)
        {
            // Set the connection string
            string conn = connectionString;
            Random _random = new Random();
            int x = 0;
            int y = 1000;
           
            int idGame = _random.Next(x, y);
            // SQL query to insert data into the TblPlayers table
            string queryString = "INSERT INTO TblPlayers (Id,Name,PhoneNumber, Country,IdGame,GamesRound) " +
								 "VALUES (@Id, @Name, @PhoneNumber, @Country,@IdGame,@GamesRound)";
           
            using (SqlConnection connPlayerTbl = new SqlConnection(conn))
            {
                connPlayerTbl.Open();
                SqlCommand command = new SqlCommand(queryString, connPlayerTbl);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@Country", country);
                command.Parameters.AddWithValue("@Duration", DateTime.Now); 
                command.Parameters.AddWithValue("@NumWins", 0);
                command.Parameters.AddWithValue("@IdGame ", idGame);
				command.Parameters.AddWithValue("@GamesRound ", 0);

				// Execute the insert command
				int rowsAffected = command.ExecuteNonQuery();
          
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Insert successful.");
                }
                else
                {
                    Console.WriteLine("Insert failed.");
                }

                connPlayerTbl.Close();
            }
            string queryStringGame = "INSERT INTO Game (IdGame,Player_Name,player_Id) " +
                               "VALUES (@IdGame, @Player_Name, @player_Id)";
            using (SqlConnection connGameTbl = new SqlConnection(conn))
            {
                connGameTbl.Open();
                

                SqlCommand commandGame = new SqlCommand(queryStringGame, connGameTbl);
                commandGame.Parameters.AddWithValue("@IdGame", idGame);
                commandGame.Parameters.AddWithValue("@Player_Name", name);
                commandGame.Parameters.AddWithValue("@player_Id", id);
                // Execute the insert command
            
                int rowsAffectedGameTbl = commandGame.ExecuteNonQuery();
                if ( rowsAffectedGameTbl > 0)
                {
                    Console.WriteLine("Insert successful.");
                }
                else
                {
                    Console.WriteLine("Insert failed.");
                }

                connGameTbl.Close();
            }


        }
    }
}