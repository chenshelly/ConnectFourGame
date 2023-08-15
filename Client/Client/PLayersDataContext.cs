using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Http.Headers;
using System.Net.Http;

namespace Client
{
    public partial class PLayersDataContext : Form
    {
      
        static HttpClient client = new HttpClient();

        Player player = new Player();
        
        public PLayersDataContext()
        {
            InitializeComponent();
        }

        public void PLayersDataContext_Load(object sender, EventArgs e)
        {
            PlayersContext playersDB = new PlayersContext();
            /*
             Player p1 = new Player
            {
               Name = "gogo",
                Id = 123,
                PhoneNumber = 0503927499,
                Country = "Israel",
                numWins = 3

            };
            playersDB.playerList.Add(p1);
            */

            bindingSource.DataSource = playersDB.playerList;
            dataGridView1.DataSource = bindingSource.DataSource;
            client.BaseAddress = new Uri("https://localhost:44382/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        static async Task<IEnumerable<Player>> GetProductAsync(string path)
        {
            IEnumerable<Player> player = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<IEnumerable<Player>>();
            }
            return player;
        }


        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            
        }

        
    }
}
