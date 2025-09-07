using System;
using System.Drawing;
using System.Windows.Forms;

namespace game_match3
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.AnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            LoadCandyImages();
            // 
            // AnimationTimer
            // 
            this.AnimationTimer.Tick += new System.EventHandler(this.AnimationTimer_Tick);
            animationTimer = new Timer();
            animationTimer.Interval = 100; 
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "GameForm";
            this.Text = "The Game";
            this.MouseClick += Game_Click;
            this.MouseClick += new MouseEventHandler(this.Game_Click);
            this.ResumeLayout(false);

            scoreLabel = new Label();
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.Size = new Size(200, 30);
            scoreLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            scoreLabel.ForeColor = Color.Green;
            scoreLabel.Text = "Бали: 0/1000";
            Controls.Add(scoreLabel);



        }
        #endregion
        private Timer AnimationTimer;
    }
}

