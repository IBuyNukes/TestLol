using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_match3
{

    public class Candy
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Type { get; set; }
        public bool IsMatched { get; set; }

        public Candy(int row, int column, int type)
        {
            Row = row;
            Column = column;
            Type = type;
            IsMatched = false;
        }
    }
}
