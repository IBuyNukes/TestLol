using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_match3
{
    public partial class MenuForm : Form
    {
        

        private void startButton_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            this.Hide(); 
            gameForm.FormClosed += (s, args) => this.Show(); 
            gameForm.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public MenuForm()
        {
            InitializeComponent();
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(100, 50);
            this.startButton.Size = new System.Drawing.Size(120, 40);
            this.startButton.Text = "Почати гру";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(100, 110);
            this.exitButton.Size = new System.Drawing.Size(120, 40);
            this.exitButton.Text = "Вихід";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MenuForm
            // 
            this.ClientSize = new System.Drawing.Size(320, 200);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.exitButton);
            this.gameLabel.SendToBack();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Fruit Punch";
        }

    }
}
