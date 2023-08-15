using System.ComponentModel.DataAnnotations.Schema;

namespace Q.Models
{

    [Table("TblPlayers")]
    public class TblPlayers
    { 
        
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int PhoneNumber { get; set; } = default!;
        public string Country { get; set; } = default!;
        public TimeSpan? Duration { get; set; }
        public int? numWins { get; set; }
		public int? gamesRound { get; set; }
		public int? IdGame { get; set; }

    }
}
