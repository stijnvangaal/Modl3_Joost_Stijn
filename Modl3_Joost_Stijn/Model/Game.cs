using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    //    ~~~~~~~~~~~~~~~~~~~~~~~~~~
    //    A--- ----- ----k---------------
    //       s-s   s-
    //    B--- -- --  
    //          s-s-
    //    C------   -----k---------
    //    ~~~~~~~~~~~~~~~~~~~~~~~~~~
    class Game
    {

        public Water FirstUpperWater { get; set; }
        public Barrack BarrackA { get; set; }
        public Barrack BarrackB { get; set; }
        public Barrack BarrackC { get; set; }
        public Water FirstDownWater { get; set; }

        public Game()
        {
            buildField();
        }

        public void buildField()
        {
            FirstUpperWater = new Water();
            Water previous = FirstUpperWater;
            Water current = null;
            Water upperCoastWater;

            for (int i = 0; i < 30; i++)
            {
                current = new Water();
                current.Previous = previous;
                previous.Next = current;

                if (i == 17) { upperCoastWater = current; current.isCoast = true; }

                previous = current;
            }


        }
    }
}
