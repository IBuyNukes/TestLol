using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace game_match3
{
    public class GameBoard
    {
        public Candy[,] Candies;
        public int Rows { get; }
        public int Columns { get; }

        private Random rand = new Random();
        private const int CandyTypeCount = 6;

        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Candies = new Candy[rows, columns];
            Initialize();
        }

        private void Initialize()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Candies[r, c] = new Candy(r, c, rand.Next(6));
                }
            }
        }

        public void Swap(int r1, int c1, int r2, int c2)
        {
            var temp = Candies[r1, c1].Type;
            Candies[r1, c1].Type = Candies[r2, c2].Type;
            Candies[r2, c2].Type = temp;
        }


        public void CheckMatches()
        {
            foreach (var candy in Candies)
                candy.IsMatched = false;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns - 2; c++)
                {
                    var type = Candies[r, c].Type;
                    if (type != -1 && type == Candies[r, c + 1].Type && type == Candies[r, c + 2].Type)
                    {
                        Candies[r, c].IsMatched = true;
                        Candies[r, c + 1].IsMatched = true;
                        Candies[r, c + 2].IsMatched = true;
                    }
                }
            }

            for (int c = 0; c < Columns; c++)
            {
                for (int r = 0; r < Rows - 2; r++)
                {
                    var type = Candies[r, c].Type;
                    if (type != -1 && type == Candies[r + 1, c].Type && type == Candies[r + 2, c].Type)
                    {
                        Candies[r, c].IsMatched = true;
                        Candies[r + 1, c].IsMatched = true;
                        Candies[r + 2, c].IsMatched = true;
                    }
                }
            }
        }


        public int RemoveMatches()
        {
            int removedCount = 0;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Candies[r, c].IsMatched)
                    {
                        Candies[r, c].Type = -1;
                        Candies[r, c].IsMatched = false;
                        removedCount++;
                    }
                }
            }

            return removedCount;
        }

        public bool DropCandies()
        {
            bool dropped = false;

            for (int c = 0; c < Columns; c++)
            {
                for (int r = Rows - 1; r > 0; r--)
                {
                    if (Candies[r, c].Type == -1)
                    {
                        for (int k = r - 1; k >= 0; k--)
                        {
                            if (Candies[k, c].Type != -1)
                            {
                                Candies[r, c].Type = Candies[k, c].Type;
                                Candies[k, c].Type = -1;
                                dropped = true;
                                break;
                            }
                        }
                    }
                }

                for (int r = 0; r < Rows; r++)
                {
                    if (Candies[r, c].Type == -1)
                    {
                        Candies[r, c].Type = rand.Next(CandyTypeCount);
                        dropped = true;
                    }
                }
            }

            return dropped;
        }
        public bool HasMatches()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns - 2; c++)
                {
                    var type = Candies[r, c].Type;
                    if (type != -1 &&
                        type == Candies[r, c + 1].Type &&
                        type == Candies[r, c + 2].Type)
                        return true;
                }
            }

            for (int c = 0; c < Columns; c++)
            {
                for (int r = 0; r < Rows - 2; r++)
                {
                    var type = Candies[r, c].Type;
                    if (type != -1 &&
                        type == Candies[r + 1, c].Type &&
                        type == Candies[r + 2, c].Type)
                        return true;
                }
            }

            return false;
        }

        public int MatchesScore()
        {
            int removedCount = 0;
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Candies[r, c].IsMatched)
                    {
                        Candies[r, c].Type = -1;
                        removedCount++;
                    }
                }
            }
            return removedCount;
        }

        public bool HasPossibleMoves()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {

                    if (c < Columns - 1)
                    {
                        Swap(r, c, r, c + 1);
                        CheckMatches();
                        if (Candies[r, c].IsMatched || Candies[r, c + 1].IsMatched)
                        {
                            Swap(r, c, r, c + 1);
                            ClearMatchesFlags();
                            return true;
                        }
                        Swap(r, c, r, c + 1);
                        ClearMatchesFlags();
                    }

                    if (r < Rows - 1)
                    {
                        Swap(r, c, r + 1, c);
                        CheckMatches();
                        if (Candies[r, c].IsMatched || Candies[r + 1, c].IsMatched)
                        {
                            Swap(r, c, r + 1, c);
                            ClearMatchesFlags();
                            return true;
                        }
                        Swap(r, c, r + 1, c);
                        ClearMatchesFlags();
                    }
                }
            }
            return false;
        }

        private void ClearMatchesFlags()
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    Candies[r, c].IsMatched = false;
        }




    }

}
