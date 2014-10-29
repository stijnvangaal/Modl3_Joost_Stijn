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
            Water currentWater = firstWater;
            while (currentWater != null)
            {
                if (currentWater.Previous != null && currentWater.Next != null)
                {
                    if (currentWater.Next.MyBoat != null) { Console.Write("<"); }
                    else if (currentWater.MyBoat != null) { Console.Write(currentWater.MyBoat.Cargo); }
                    else if (currentWater.Previous.MyBoat != null) { Console.Write(">"); }
                    else { Console.Write("~"); }
                }
                else if(currentWater.Previous == null)
                {
                    if (currentWater.Next.MyBoat != null) { Console.Write("<"); }
                    else if (currentWater.MyBoat != null) { Console.Write(currentWater.MyBoat.Cargo); }
                    else { Console.Write("~"); }
                }
                else if (currentWater.Next == null) { Console.Write("~");}
                currentWater = currentWater.Next;
            }
            Console.WriteLine();

            String domain = "Modl3_Joost_Stijn.Model.";

            Console.Write("A");
            Track currentTrack = BarrackA.Next;
            while (currentTrack != null)
            {
                if (currentTrack.Cart == null)
                {
                    if ("" + currentTrack.GetType() == domain + "Track") { Console.Write("-"); }
                    else if ("" + currentTrack.GetType() == domain + "Switch") { Console.Write("s"); }
                    else if ("" + currentTrack.GetType() == domain + "Coast") { Console.Write("K"); }
                }

                currentTrack = currentTrack.Next;
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
