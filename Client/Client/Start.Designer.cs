namespace Client
{
    partial class Start
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
            this.textBoxIdUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartGameUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxIdUser
            // 
            this.textBoxIdUser.Location = new System.Drawing.Point(270, 178);
            this.textBoxIdUser.Name = "textBoxIdUser";
            this.textBoxIdUser.Size = new System.Drawing.Size(160, 20);
            this.textBoxIdUser.TabIndex = 0;
            this.textBoxIdUser.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxIdUser_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(280, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Id user:";
            // 
            // StartGameUser
            // 
            this.StartGameUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.StartGameUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StartGameUser.Location = new System.Drawing.Point(304, 236);
            this.StartGameUser.Name = "StartGameUser";
            this.StartGameUser.Size = new System.Drawing.Size(75, 23);
            this.StartGameUser.TabIndex = 2;
            this.StartGameUser.Text = "Start Game";
            this.StartGameUser.UseVisualStyleBackColor = false;
            this.StartGameUser.Click += new System.EventHandler(this.Start_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(227)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StartGameUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxIdUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Start";
            this.Text = "Start";
            this.Load += new System.EventHandler(this.Start_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIdUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartGameUser;
    }
}