using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Boat
    {
        public int Capacity { get; set; }
        public int Cargo { get; set; }


        public Boat()
        {
            Capacity = 3;
            Cargo = 0;
        }

        public void load()
        {
            if (Cargo < Capacity)
            {
                Cargo = Cargo + 1;
            }
        }
    }
}
