using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class PlayerDataPopUp : Form
    {
        public PlayerDataPopUp()
        {
            
            InitializeComponent();
        }

        public string LabelName { get; set; }

        public string labelId { get; set; }
        public string LabelPhoneNumber { get;  set; }
        public string LabelCountry { get;  set; }
       
        private void PlayerDetailsForm_Load(object sender, EventArgs e)
        {
            playerName.Text += LabelName;
            label1.Text += labelId;
            PhoneNumber.Text += LabelPhoneNumber;
            Country.Text += LabelCountry;
        }

        private void NameLabelRes_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
