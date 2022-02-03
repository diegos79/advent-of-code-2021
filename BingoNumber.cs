using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aocd4
{
    internal class BingoNumber
    {
        public BingoNumber(int number)
        {
            Value = number;
        }

        public bool Extracted { get; set; }

        public int Value { get; private set; }
    }
}
