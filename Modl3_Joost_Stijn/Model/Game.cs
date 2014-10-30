using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Threading;

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
        public Track EndingTop { get; set; }
        public Track EndingBottom { get; set; }
        
        public Switch Switch1 { get; set; }
        public Switch Switch2 { get; set; }
        public Switch Switch3 { get; set; }
        public Switch Switch4 { get; set; }
        public Switch Switch5 { get; set; }

        public Boolean running = true;
        public Thread myThread;

        public Game(Controller.Application app)
        {
            MyApp = app;
            buildField();
            //EndingTop.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Previous.Cart = new Cart();
            BarrackB.newCart();
            BarrackA.newCart();
            BarrackC.newCart();
            myThread = new Thread(new ThreadStart(this.GameLoop));
            myThread.Start();

        }

        public void buildField()
        {
            FirstUpperWater = new Water();
            FirstUpperWater.IsFirst = true;
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
            FirstDownWater.IsFirst = true;
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
            currentTrack.Previous = BarrackA;
            BarrackA.Next = currentTrack;
            currentTrack = addTrack(currentTrack);
            currentTrack = addTrack(currentTrack);

            //switch 1 UP
            Switch1 = new Switch(false);
            currentTrack.Next = Switch1;
            Switch1.PreviousUp = currentTrack;
            Switch1.Up = false;
            Switch1.Previous = Switch1.PreviousUp;
            currentTrack = Switch1;

            currentTrack = addTrack(currentTrack);

            //switch 2 UP
            Switch2 = new Switch(true);
            currentTrack.Next = Switch2;
            Switch2.Previous = currentTrack;
            Switch2.Up = true;
            currentTrack = Switch2;
            currentTrack.Next = new Track();
            Switch2.NextUp = currentTrack.Next;
            currentTrack = currentTrack.Next;
            currentTrack.Previous = Switch2;

            for (int i = 0; i < 4; i++) { currentTrack = addTrack(currentTrack); }

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
                    coast.Previous = currentTrack;
                    currentTrack = coast;
                }
                else
                {
                    currentTrack = addTrack(currentTrack);
                }
            }
            EndingTop = currentTrack;
            EndingTop.IsLast = true;

            //starting from Barrack B
            BarrackB = new Barrack();
            currentTrack = new Track();
            BarrackB.Next = currentTrack;
            currentTrack.Previous = BarrackB;
            currentTrack = addTrack(currentTrack);
            currentTrack = addTrack(currentTrack);

            //switch 1 DOWN
            currentTrack.Next = Switch1;
            Switch1.PreviousDown = currentTrack;

            //switch 2 DOWN
            Switch2.NextDown = new Track();
            currentTrack = Switch2.NextDown;
            currentTrack.Previous = Switch2;
            currentTrack = addTrack(currentTrack);

            //switch 3 UP
            Switch3 = new Switch(false);
            currentTrack.Next = Switch3;
            Switch3.PreviousUp = currentTrack;
            Switch3.Up = true;
            Switch3.Previous = Switch3.PreviousUp;
            currentTrack = Switch3;

            currentTrack = addTrack(currentTrack);

            //switch 4 UP
            Switch4 = new Switch(true);
            currentTrack.Next = Switch4;
            Switch4.Previous = currentTrack;
            Switch4.Up = false;
            currentTrack = Switch4;
            currentTrack.Next = new Track();
            Switch4.NextUp = currentTrack.Next;
            currentTrack = currentTrack.Next;
            currentTrack.Previous = Switch4;

            currentTrack = addTrack(currentTrack);

            //switch 5 DOWN
            currentTrack.Next = Switch5;
            Switch5.PreviousDown = currentTrack;

            //starting from Barrack C
            BarrackC = new Barrack();
            currentTrack = new Track();
            BarrackC.Next = currentTrack;
            currentTrack.Previous = BarrackC;

            for (int i = 0; i < 5; i++)
            {
                currentTrack = addTrack(currentTrack);
            }

            //switch 3 DOWN
            currentTrack.Next = Switch3;
            Switch3.PreviousDown = currentTrack;

            //switch 4 DOWN
            Switch4.NextDown = new Track();
            currentTrack = Switch4.NextDown;
            currentTrack.Previous = Switch4;

            for (int i = 0; i < 12; i++)
            {
                if (i == 6)
                {
                    Coast coast = new Coast();
                    currentTrack.Next = coast;
                    coast.MyWater = upperCoastWater;
                    coast.Previous = currentTrack;
                    currentTrack = coast;
                }
                else
                {
                    currentTrack = addTrack(currentTrack);
                }
            }
            EndingBottom = currentTrack;
            EndingBottom.IsLast = true;
        }

        private Track addTrack(Track current)
        {
            current.Next = new Track();
            current.Next.Previous = current;
            return current.Next;
        }

        //returns true if game is over
        public Boolean doStep()
        {
            //make water thingies do their shizzle
            moveCarts();
            return false;
        }

        //return true if game is over
        public Boolean moveCarts()
        {
            Track currentTop = EndingTop;
            for (int i = 0; i < 12; i++)
            {
                currentTop.moveCart();
                currentTop = currentTop.Previous;
                //check erbij wanneer coast bereikt is-------------------------
            }

            Track currentBottom = EndingBottom;
            for (int i = 0; i < 12; i++)
            {
                currentBottom.moveCart();
                currentBottom = currentBottom.Previous;
                //check erbij wanneer coast bereikt is-------------------------
                MyApp.myView.drawField();
            }

            Switch5.moveCart();
            Switch5.PreviousDown.moveCart();
            if (Switch4.Up) { Switch5.PreviousDown.Previous.moveCart(); }
            else { currentBottom.moveCart(); }

            Switch4.moveCart();
            Switch4.Previous.moveCart();

            Switch3.moveCart();
            currentBottom = Switch3.PreviousDown;
            for (int i = 0; i < 5; i++)
            {
                currentBottom.moveCart();
                currentBottom = currentBottom.Previous;
            }

            currentTop = Switch5.PreviousUp;
            for (int i = 0; i < 4; i++)
            {
                currentTop.moveCart();
                currentTop = currentTop.Previous;
            }

            Switch3.PreviousUp.moveCart();

            if (Switch2.Up) { currentTop.moveCart(); }
            else { Switch3.PreviousUp.Previous.moveCart(); }
            currentTop.Previous.moveCart();
            currentTop.Previous.Previous.moveCart();

            Switch1.moveCart();
            Switch1.PreviousUp.moveCart();
            Switch1.PreviousUp.Previous.moveCart();
            Switch1.PreviousDown.moveCart();
            Switch1.PreviousDown.Previous.moveCart();

            return false;
        }

        public void MoveBoats()
        {
            Water currentWaterUp;
            Water currentWaterDown;
            FirstUpperWater.moveBoat();
            currentWaterUp = FirstUpperWater.Next;
            FirstDownWater.moveBoat();
            currentWaterDown = FirstDownWater.Next;
            for (int i = 0; i < 30; i++)
            {
                currentWaterUp.moveBoat();
                currentWaterDown.moveBoat();
            }

            Random random = new Random();
            if ((random.Next(100) % 10) == 0)
            {
                FirstUpperWater.newBoat();
            }
            if ((random.Next(100) % 11) == 0)
            {
                FirstDownWater.newBoat();
            }

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
                if (cki.Key == ConsoleKey.Escape) { myThread.Abort(); ; Environment.Exit(0); }
                MyApp.myView.drawField();
                Console.WriteLine("Press any switch number. Or esc to exit");
            } while (true);
            
        }

        public void GameLoop()
        {
            while (running) 
            {
                Thread.Sleep(1000);
                doStep();
            }


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
