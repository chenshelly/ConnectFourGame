namespace Client
{
    partial class PlayerDataPopUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerDataPopUp));
            this.playerName = new System.Windows.Forms.Label();
            this.PhoneNumber = new System.Windows.Forms.Label();
            this.Country = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playerName
            // 
            resources.ApplyResources(this.playerName, "playerName");
            this.playerName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.playerName.Name = "playerName";
            this.playerName.Tag = "LabelName";
            // 
            // PhoneNumber
            // 
            resources.ApplyResources(this.PhoneNumber, "PhoneNumber");
            this.PhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.Tag = "PhoneNumber";
            // 
            // Country
            // 
            resources.ApplyResources(this.Country, "Country");
            this.Country.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Country.Name = "Country";
            this.Country.Tag = "Country";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Name = "label1";
            this.label1.Tag = "IdPlayer";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.Tag = "resName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(227)))), ((int)(((byte)(241)))));
            this.label2.Name = "label2";
            this.label2.Tag = "LabelName";
            // 
            // PlayerDataPopUp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(227)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Country);
            this.Controls.Add(this.PhoneNumber);
            this.Controls.Add(this.playerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "PlayerDataPopUp";
            this.Load += new System.EventHandler(this.PlayerDetailsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playerName;
        private System.Windows.Forms.Label PhoneNumber;
        private System.Windows.Forms.Label Country;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}