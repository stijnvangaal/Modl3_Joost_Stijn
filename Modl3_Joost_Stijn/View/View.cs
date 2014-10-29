using Modl3_Joost_Stijn.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.View
{

//    ~~~~~~~~~~~~~~~~~~~~~~~~~~
//    A--- -----  ----k---------------
//       s-s   s -
//    B--- -- --  
//          s-s-
//    C------   -----k---------
//    ~~~~~~~~~~~~~~~~~~~~~~~~~~
//      ~water  -track  sSwitch  kKade `switchup ,switchdown
    class View
    {
        public Water FirstUpperWater { get; set; }
        public Barrack BarrackA { get; set; }
        public Barrack BarrackB { get; set; }
        public Barrack BarrackC { get; set; }
        public Water FirstDownWater { get; set; }

        public void drawField()
        {
            Console.Clear();


        }



        public void setField(Water FUW, Barrack BA, Barrack BB, Barrack BC, Water FDW)
        {
            FirstUpperWater = FUW;
            BarrackA        = BA;
            BarrackB        = BB;
            BarrackC        = BC;
            FirstDownWater  = FDW;
        }
    }
}
