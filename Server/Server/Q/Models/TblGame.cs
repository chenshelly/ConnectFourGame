using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Q.Models
{

    public class TblGame
    {
        public int IdGame { get; set; }
        public int TimedPlayed { get; set; }
        public int Moves { get; set; }
        public string Player_Name { get; set; } = default!;
        public List<Player> all_players = new List<Player>();


        

    }
}
