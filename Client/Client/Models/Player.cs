
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client.Models
{
    public class Player
    {
     
        public string Name { get; set; }
        public int Id { get; set; }

        public int PhoneNumber { get; set; }
        public string Country { get; set; }
        public int numWins { get; set; }
		public int step { get; set; }
        public int idGame { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration => StartTime - EndTime;

		private readonly HttpClient _httpClient;
        public Player(int id, string name, int phoneNumber,string country, int num_Wins, TimeSpan start)
        {
            Id = id;
            Name = name;
			PhoneNumber = phoneNumber;
			Country = country;
            numWins = num_Wins;
            StartTime = start;
		}
		public Player()
		{
			

		}

	
        

		public Player(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public void OnGet()
        {
  
        }

        
    }
}