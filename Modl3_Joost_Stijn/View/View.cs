using Modl3_Joost_Stijn.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.View
{

//    ~~~~~~~~~~~~~~~~~~~~~~~~~~
//    A--- ----- ----k---------------
//       s-s   s-
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
        { 
        }

        public void drawFirst()
        {
            Console.WriteLine("Gold Fever");
            Console.WriteLine();
            Console.WriteLine("   1 2   5");
            drawWater(FirstUpperWater);
            drawTracks();
            drawWater(FirstDownWater);
            Console.WriteLine("      3 4");

            Console.WriteLine();
            Console.WriteLine("press spaceBar to begin. press esc to exit.");
            Console.WriteLine();
            Console.WriteLine("Legenda");
            Console.WriteLine();
            Console.WriteLine(" - track                 " + (char)127 + " filled cart           " + (char)34 + "  empty cart ");
            Console.WriteLine(" ` switch up             , switch down           K coast ");
            Console.WriteLine(" % cart on switch up     " + (char)247 + " cart on track down    " + (char)94 + " cart on coast ");
            Console.WriteLine(" ~ water                 <0> boat (with index of cargo) ");
        }

        public void endMessage(int Points, int steps)
        {
            Console.Clear();
            Console.WriteLine("Gold Fever");
            Console.WriteLine();
            Console.WriteLine("   1 2   5");
            drawWater(FirstUpperWater);
            drawTracks();
            drawWater(FirstDownWater);
            Console.WriteLine("      3 4");

            Console.WriteLine();
            Console.WriteLine("GAME OVER");
            Console.WriteLine();
            Console.WriteLine("you scored {0} points", Points);
            Console.WriteLine("and survived for {0} steps", steps);
            Console.WriteLine("press esc to leave");
        }

        public void drawField(int points)
        {
            Console.Clear();
            Console.WriteLine("Gold Fever");
            Console.WriteLine();
            Console.WriteLine("Points: {0}", points);
            Console.WriteLine();
            Console.WriteLine("   1 2   5");
            drawWater(FirstUpperWater);
            drawTracks();
            drawWater(FirstDownWater);
            Console.WriteLine("      3 4");

            Console.WriteLine("Press any switch number. Or esc to exit");
            Console.WriteLine("Switches are numbered from left to right: 1-5");
            Console.WriteLine();
            Console.WriteLine("Legenda");
            Console.WriteLine();
            Console.WriteLine(" - track                 " + (char)127 + " filled cart           " + (char)34 + "  empty cart ");
            Console.WriteLine(" ` switch up             , switch down           K coast ");
            Console.WriteLine(" % cart on switch up     " + (char)247 + " cart on track down    " + (char)94 + " cart on coast ");
            Console.WriteLine(" ~ water                 <0> boat (with index of cargo) ");
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
                currentWater = currentWater.Next;
            }
            Console.WriteLine();
        }

        private void drawTracks()
        {
            Switch switch1 = null;
            Switch switch2 = null;
            Switch switch3 = null;
            Switch switch4 = null;
            Switch switch5 = null;

            //toprow A
            Console.Write("A");
            Track currentTrack = BarrackA.Next;
            for (int i = 0; i < 24; i++ )
            {
                if ((i >= 0 && i <= 2) || ( i>= 6 && i <= 10) || i > 12)
                {
                    drawInstance(currentTrack);
                }
                else if(i==5 || i == 11){ Console.Write(" "); }
                if (i == 3) { switch1 = (Switch)currentTrack; }
                else if (i == 5) { switch2 = (Switch)currentTrack; }
                else if (i == 11) { switch5 = (Switch)currentTrack; }
                
                currentTrack = currentTrack.Next;
            }
            Console.WriteLine();

            //top inbetween row
            currentTrack = BarrackA;
            for (int i = 0; i < 14; i++)
            {
                if ((i >= 1 && i <= 3) || (i >= 7 && i <= 9))
                {
                    Console.Write(" ");
                }
                else if(i!= 0 && i!= 10 && i!=11){ drawInstance(currentTrack); }

                currentTrack = currentTrack.Next;
            }
            Console.WriteLine();

            //middel row B
            Console.Write("B");
            currentTrack = BarrackB.Next;
            for (int i = 0; i < 13; i++)
            {
                if ((i >= 0 && i <= 2) || (i >= 6 && i <= 7) || (i >= 11 && i <= 12))
                {
                    drawInstance(currentTrack);
                }
                else if( i!=4 && i!=5 && i!=9 && i!= 10){ Console.Write(" "); }
                if (i == 8) { switch3 = (Switch)currentTrack; }
                else if (i == 10) { switch4 = (Switch)currentTrack; }

                if (i == 5)
                {
                    Switch temp = (Switch)currentTrack;
                    currentTrack = temp.NextDown;
                }
                else { currentTrack = currentTrack.Next; }
            }
            Console.WriteLine();

            //bottom inbetween row
            currentTrack = BarrackB;
            for (int i = 0; i < 12; i++)
            {
                if ((i >= 9 && i <= 11))
                {
                    drawInstance(currentTrack);
                }
                else if(i>2){ Console.Write(" "); }

                if (i == 6)
                {
                    Switch temp = (Switch)currentTrack;
                    currentTrack = temp.NextDown;
                }
                else { currentTrack = currentTrack.Next; }
            }
            Console.WriteLine();

            //starting from barrack C
            Console.Write("C");
            currentTrack = BarrackC.Next;
            for (int i = 0; i < 22; i++)
            {
                if ((i >= 0 && i <= 5) || i >= 9)
                {
                    drawInstance(currentTrack);
                }
                else if(i!=6 && i!=7){ Console.Write(" "); }
               

                if (i == 8)
                {
                    Switch temp = (Switch)currentTrack;
                    currentTrack = temp.NextDown;
                }
                else { currentTrack = currentTrack.Next; }
            }
            Console.WriteLine();

            Console.WriteLine();
        }

        private void drawInstance(Track track)
        {
            String domain = "Modl3_Joost_Stijn.Model.";

            if (track.Cart == null)
            {
                if      ("" + track.GetType() == domain + "Track") { Console.Write("-"); }
                else if ("" + track.GetType() == domain + "Switch")
                {
                    Switch tempSwitch = (Switch)track;
                    if (tempSwitch.Up) { Console.Write("`"); }
                    else { Console.Write(","); }
                }
                else if ("" + track.GetType() == domain + "Coast") { Console.Write("K"); }
            }
            else
            {
                if ("" + track.GetType() == domain + "Track") {
                    if (track.Cart.Loaded) { Console.Write((char)127); }
                    else { Console.Write((char)34); }

                }
                else if ("" + track.GetType() == domain + "Switch")
                {
                    Switch tempSwitch = (Switch)track;
                    if (tempSwitch.Up) { Console.Write("%"); }
                    else { Console.Write((char)247); }
                }
                else if ("" + track.GetType() == domain + "Coast") { Console.Write((char)94); }
            }
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
