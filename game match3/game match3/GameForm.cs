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
    public partial class GameForm : Form
    {
        private GameBoard board;
        private const int tileSize = 50;
        private Point selectedCell = Point.Empty;

        public GameForm()
        {
            InitializeComponent();
            board = new GameBoard(8, 8);
            this.DoubleBuffered = true;
            this.ClientSize = new Size(board.Columns * tileSize, board.Rows * tileSize + 40);
            this.MouseClick += Game_Click;
            this.Paint += GameForm_Paint;
            board.CheckMatches();
            int removed = board.MatchesScore();
            if (removed > 0)
            {
                score += removed * 10;
                UpdateScoreLabel();
                if (score >= scoreTarget)
                {
                    MessageBox.Show("Вітаємо! Ви набрали 1000 балів і перемогли!", "Перемога");
                    Close(); 
                }
            }

        }
        private void UpdateScoreLabel()
        {
            scoreLabel.Text = $"Бали: {score}/{scoreTarget}";
        }
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {

            int offsetY = 40; 

            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Columns; c++)
                {
                    int x = c * tileSize;
                    int y = offsetY + r * tileSize;

                    if (candyImages != null && board.Candies[r, c].Type >= 0)
                    {
                        e.Graphics.DrawImage(candyImages[board.Candies[r, c].Type], x, y, tileSize, tileSize);
                    }

                    e.Graphics.DrawRectangle(Pens.Black, x, y, tileSize, tileSize);
                }
            }

        }

        private Image[] candyImages;

        private void LoadCandyImages()
        {
            candyImages = new Image[]
            {
        Properties.Resources.Apple,
        Properties.Resources.Banana,
        Properties.Resources.Grape,
        Properties.Resources.Lemon,
        Properties.Resources.Orange,
        Properties.Resources.Watermelon
            };
        }



        private void Game_Click(object sender, MouseEventArgs e)
        {
            int row = (e.Y - 40) / tileSize;
            int col = e.X / tileSize;

            if (row < 0 || row >= board.Rows || col < 0 || col >= board.Columns)
                return;

            if (selectedCell.IsEmpty)
            {
                selectedCell = new Point(col, row);
            }
            else
            {
                int dx = Math.Abs(selectedCell.X - col);
                int dy = Math.Abs(selectedCell.Y - row);

                if ((dx == 1 && dy == 0) || (dx == 0 && dy == 1))
                {
                    board.Swap(row, col, selectedCell.Y, selectedCell.X);

                    if (board.HasMatches())
                    {
                        board.CheckMatches();
                        int removed = board.RemoveMatches();
                        score += removed * 10;
                        UpdateScoreLabel();

                        board.DropCandies(); 
                        Invalidate();

                        if (score >= scoreTarget)
                        {
                            MessageBox.Show("Вітаємо! Ви набрали 1000 балів і перемогли!", "Перемога");
                            Close();
                            return;
                        }
                    }
                    else
                    {
                        board.Swap(row, col, selectedCell.Y, selectedCell.X);
                    }
                }

                selectedCell = Point.Empty;

                if (!board.HasPossibleMoves())
                {
                    MessageBox.Show($"На жаль, немає доступних ходів.\nВаш рахунок: {score} балів", "Гру завершено");
                    Close();
                }
            }
        }



        private Timer animationTimer;
        private bool isFalling = false;


        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (isFalling) return;

            isFalling = true;

            board.CheckMatches();
            board.RemoveMatches();

            if (board.DropCandies())
            {
                Invalidate(); 
            }

            isFalling = false;
        }

        private int score = 0;
        private const int scoreTarget = 1000;
        private Label scoreLabel;


    }

}
