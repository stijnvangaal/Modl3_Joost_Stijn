using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    //    ~~~~~~~~~~~~~~~~~~~~~~~~~~
    //    A--- -----  ----k---------------
    //       s-s   s -
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

//        private void CreatePlayerFirstFields(Player p, Field join)
//        {
//            Field previous = null;
//            Field newOne = null;
//            for (int i = 0; i < 4; i++) // 4 velden aanmaken
//           {
//                if (i == 3) // vierde (index 3) is Rozet
//                {
//                    newOne = new RozetField();
//                }
//                else
//                {
//                    newOne = new Field();
//                }
//
//                if (i == 0) // bij begin koppelen aan start
//                {
//                    p.StartField.NextField = newOne;
//                }
//                else // anders koppelen aan vorige
//                {
//                    previous.NextField = newOne;
//                }
//                previous = newOne;
//            }
//            newOne.NextField = join; // laatste koppelen aan join
//       }

    }
}
