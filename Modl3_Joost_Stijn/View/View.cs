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

        public View()
        { }

        public void drawField()
        {
            Console.Clear();
            drawWater(FirstUpperWater);
            drawTracks();
            drawWater(FirstDownWater);

        }

        private void drawWater(Water firstWater)
        {
            Water current = firstWater;
            while (current != null)
            {
                if (current.Previous != null && current.Next != null)
                {
                    if (current.Next.MyBoat != null) { Console.Write("<"); }
                    else if (current.MyBoat != null) { Console.Write(current.MyBoat.Cargo); }
                    else if (current.Previous.MyBoat != null) { Console.Write(">"); }
                    else { Console.Write("~"); }
                }
                else if(current.Previous == null)
                {
                    if (current.Next.MyBoat != null) { Console.Write("<"); }
                    else if (current.MyBoat != null) { Console.Write(current.MyBoat.Cargo); }
                    else { Console.Write("~"); }
                }
                else if (current.Next == null) { Console.Write("~");}
                current = current.Next;
            }
            Console.ReadLine();
        }

        private void drawTracks()
        {

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
