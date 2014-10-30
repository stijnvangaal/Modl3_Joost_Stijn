using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

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
        public Controller.Application MyApp { get; set; }
        public Water FirstUpperWater { get; set; }
        public Barrack BarrackA { get; set; }
        public Barrack BarrackB { get; set; }
        public Barrack BarrackC { get; set; }
        public Water FirstDownWater { get; set; }

        public Switch Switch1 { get; set; }
        public Switch Switch2 { get; set; }
        public Switch Switch3 { get; set; }
        public Switch Switch4 { get; set; }
        public Switch Switch5 { get; set; }

        public Game(Controller.Application app)
        {
            MyApp = app;
            buildField();
        }

        public void buildField()
        {
            FirstUpperWater = new Water();
            Water previous = FirstUpperWater;
            Water current           = null;
            Water upperCoastWater   = null;
            Water bottomCoastWater  = null;

            //topline of water
            for (int i = 0; i < 30; i++)
            {
                current = new Water();
                current.Previous = previous;
                previous.Next = current;

                if (i == 16) { upperCoastWater = current; current.IsCoast = true; current.MyBoat = new Boat(); }

                previous = current;
            }

            FirstDownWater = new Water();
            previous = FirstDownWater;
            current = null;

            //Bottomline of water
            for (int i = 0; i < 30; i++)
            {
                current = new Water();
                current.Previous = previous;
                previous.Next = current;

                if (i == 14) { bottomCoastWater = current; current.IsCoast = true; current.MyBoat = new Boat(); }

                previous = current;
            }

            //starting from Barrack A
            BarrackA = new Barrack();
            Track currentTrack = new Track();
            BarrackA.Next = currentTrack;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 1 UP
            Switch1 = new Switch(false);
            currentTrack.Next = Switch1;
            Switch1.PreviousUp = currentTrack;
            Switch1.Up = false;
            Switch1.Previous = Switch1.PreviousUp;
            currentTrack = Switch1;

            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 2 UP
            Switch2 = new Switch(true);
            currentTrack.Next = Switch2;
            Switch2.Previous = currentTrack;
            Switch2.Up = true;
            currentTrack = Switch2;
            currentTrack.Next = new Track();
            Switch2.NextUp = currentTrack.Next;
            currentTrack = currentTrack.Next;

            for (int i = 0; i < 4; i++)
            {
                currentTrack.Next = new Track();
                currentTrack = currentTrack.Next;

            }

            //switch 5 UP
            Switch5 = new Switch(false);
            currentTrack.Next = Switch5;
            Switch5.PreviousUp = currentTrack;
            Switch5.Up = true;
            Switch5.Previous = Switch5.PreviousUp;
            currentTrack = Switch5;

            for (int i = 0; i < 12; i++)
            {
                if (i == 7)
                {
                    Coast coast = new Coast();
                    currentTrack.Next = coast;
                    coast.MyWater = upperCoastWater;
                }
                else
                {
                    currentTrack.Next = new Track();
                }
                currentTrack = currentTrack.Next;
            }

            //starting from Barrack B
            BarrackB = new Barrack();
            currentTrack = new Track();
            BarrackB.Next = currentTrack;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 1 DOWN
            currentTrack.Next = Switch1;
            Switch1.PreviousDown = currentTrack;

            //switch 2 DOWN
            Switch2.NextDown = new Track();
            currentTrack = Switch2.NextDown;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 3 UP
            Switch3 = new Switch(false);
            currentTrack.Next = Switch3;
            Switch3.PreviousUp = currentTrack;
            Switch3.Up = true;
            Switch3.Previous = Switch3.PreviousUp;
            currentTrack = Switch3;

            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 4 UP
            Switch4 = new Switch(true);
            currentTrack.Next = Switch4;
            Switch4.Previous = currentTrack;
            Switch4.Up = false;
            currentTrack = Switch4;
            currentTrack.Next = new Track();
            Switch4.NextUp = currentTrack.Next;
            currentTrack = currentTrack.Next;

            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 5 DOWN
            currentTrack.Next = Switch5;
            Switch5.PreviousDown = currentTrack;

            //starting from Barrack C
            BarrackC = new Barrack();
            currentTrack = new Track();
            BarrackC.Next = currentTrack;

            for (int i = 0; i < 5; i++)
            {
                currentTrack.Next = new Track();
                currentTrack = currentTrack.Next;
            }

            //switch 3 DOWN
            currentTrack.Next = Switch3;
            Switch3.PreviousDown = currentTrack;

            //switch 4 DOWN
            Switch4.NextDown = new Track();
            currentTrack = Switch4.NextDown;

            for (int i = 0; i < 12; i++)
            {
                if (i == 6)
                {
                    Coast coast = new Coast();
                    currentTrack.Next = coast;
                    coast.MyWater = bottomCoastWater;
                }
                else
                {
                    currentTrack.Next = new Track();
                }
                currentTrack = currentTrack.Next;
            }
        }

        private Track addTrack(Track current)
        {
            current.Next = new Track();
            current.Next.Previous = current;
            return current.Next;
        }
        public void KeyListener()
        {
            ConsoleKeyInfo cki;
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;

            Console.WriteLine("Press any switch number. Or esc to exit");
            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.D1) { Switch1.change(); }
                if (cki.Key == ConsoleKey.D2) { Switch2.change(); }
                if (cki.Key == ConsoleKey.D3) { Switch3.change(); }
                if (cki.Key == ConsoleKey.D4) { Switch4.change(); }
                if (cki.Key == ConsoleKey.D5) { Switch5.change(); }
                MyApp.myView.drawField();
                Console.WriteLine("Press any switch number. Or esc to exit");
            } while (cki.Key != ConsoleKey.Escape);
        }
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
