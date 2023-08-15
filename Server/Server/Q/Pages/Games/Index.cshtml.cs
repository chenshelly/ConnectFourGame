using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Q.Models;
using System.Linq;
using System.Numerics;

namespace Q.Pages.Games
{
    public class IndexModel : PageModel
    {
		private readonly Q.Data.QContext _context;
		private string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=gameDB;Integrated Security=True";
      //  private readonly ILogger<IndexModel> _logger;
        public readonly int SIZE = 1;
		public IndexModel(Q.Data.QContext context)
		{
			_context = context;
			Names = _context.TblPlayers.Select(p => p.Name).Distinct().OrderBy(s => s).ToList();
		}
		/*public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}
        */
		public List<string> Names { get; set; } = default!;
		// [BindProperty]
		public string Name { get; set; } = default!;
		// public IList<TblPlayers> TblPlayers { get;set; } = default!;
		public IEnumerable<TblPlayers> TblPlayers { get; set; }
		public IEnumerable<TblGame> TblGame { get; set; }
		
		

        [BindProperty]
        public List<Player> playersGames { get; set; } = default!;
        public TblGame theGame { get; set; } = default!;
     
		public void OnGet()
		{

			TblPlayers = _context.TblPlayers;


		}


		public IActionResult OnPost()
        {
            var orderedPlayers = playersGames.OrderByDescending(player => player.numWins);

            // Print the ordered players
            foreach (var player in orderedPlayers)
            {
                Console.WriteLine($"{player.Name}: {player.numWins}");
            }

            if (ModelState.IsValid)
            {
                foreach (Player player in playersGames)
                {
                    theGame.all_players.Add(player);
                    theGame.IdGame = SIZE + 1;

                    InsertToGameTable(theGame.IdGame, player.Name);


                    string res = string.Join("<br>", new List<string> { theGame.IdGame.ToString(),
                                            player.Name,
                                             player.PhoneNumber.ToString()
                                                });
                    Response.WriteAsync("<p>" + res + "</p>");
                }
            }
            return Page();
        }


        public void InsertToGameTable(int idGame, string Player_Name)
        {
            // Set the connection string
            string conn = connectionString;

            string queryString = "INSERT INTO TblPlayers (IdGame,TimedPlayed,Moves,Player_Name) " +
                                 "VALUES (@idGame,@Player_Name)";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Id", idGame);
                command.Parameters.AddWithValue("@Name", Player_Name);
                command.Parameters.AddWithValue("@Duration", DateTime.Now); 
               

                connection.Open();
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
            }

        }
    }
}
