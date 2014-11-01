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
        public Water LastUpperWater { get; set; }
        public Barrack BarrackA { get; set; }
        public Barrack BarrackB { get; set; }
        public Barrack BarrackC { get; set; }
        public Water FirstDownWater { get; set; }
        public Water LastDownWater { get; set; }
        public Track EndingTop { get; set; }
        public Track EndingBottom { get; set; }

        public Boolean started = false;
        private Boolean ended = false;
        public int points = 0;
        private int steps = 0;
        
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
            myThread = new Thread(new ThreadStart(this.GameLoop));
            myThread.Start();

        }

        public void buildField()
        {
            FirstUpperWater = new Water(this);
            FirstUpperWater.IsFirst = true;
            Water previous = FirstUpperWater;
            Water current           = null;
            Water upperCoastWater   = null;
            Water bottomCoastWater  = null;

            //topline of water
            for (int i = 0; i < 30; i++)
            {
                current = new Water(this);
                current.Previous = previous;
                previous.Next = current;

                if (i == 16) { upperCoastWater = current; current.IsCoast = true; }

                previous = current;
            }
            LastUpperWater = current;
            LastUpperWater.IsLast = true;

            FirstDownWater = new Water(this);
            FirstDownWater.IsFirst = true;
            previous = FirstDownWater;
            current = null;

            //Bottomline of water
            for (int i = 0; i < 30; i++)
            {
                current = new Water(this);
                current.Previous = previous;
                previous.Next = current;

                if (i == 14) { bottomCoastWater = current; current.IsCoast = true;  }

                previous = current;
            }
            LastDownWater = current;
            LastDownWater.IsLast = true;

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
                    coast.MyWater = bottomCoastWater;
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
        public void doStep()
        {
            MoveBoats();
            Boolean finished = moveCarts();
            Random rand = new Random();
            int randInt = rand.Next(6);
            Console.WriteLine(randInt);

            if(randInt == 0) { 
                BarrackA.newCart(); }
            else if(randInt == 1){ BarrackB.newCart(); }
            else if(randInt == 2){ BarrackC.newCart(); }

            if (!finished) { MyApp.myView.drawField(points); }
            else {
                ended = true;
                MyApp.endGame(points, steps); 
                myThread.Abort();
                
            }
            steps++;
        }

        //return true if game is over
        public Boolean moveCarts()
        {
            Boolean finished = false;
            Track currentTop = EndingTop;
            for (int i = 0; i < 12; i++)
            {
                currentTop.moveCart();
                if ("" + currentTop.GetType() == "Modl3_Joost_Stijn.Model.Coast")
                {
                    if (currentTop.Cart != null)
                    {
                         Coast temp = (Coast)currentTop;
                         if (temp.MyWater.MyBoat != null) 
                        { 
                             temp.MyWater.MyBoat.load();
                             currentTop.Cart.unLoad();
                         }
                    }
                }
                currentTop = currentTop.Previous;  
            }

            Track currentBottom = EndingBottom;
            for (int i = 0; i < 12; i++)
            {
                currentBottom.moveCart();
                if ("" + currentBottom.GetType() == "Modl3_Joost_Stijn.Model.Coast")
                {
                    if (currentBottom.Cart != null)
                    {
                        Coast temp = (Coast)currentBottom;
                        if (temp.MyWater.MyBoat != null)
                        {
                            temp.MyWater.MyBoat.load();
                            currentBottom.Cart.unLoad();
                        }
                    }
                }
                currentBottom = currentBottom.Previous;
            }

            Switch5.moveCart();
            if (Switch5.PreviousDown.moveCart()) { finished = true; }
            if (Switch4.Up) { Switch5.PreviousDown.Previous.moveCart(); }
            else { currentBottom.moveCart(); }

            Switch4.moveCart();
            Switch4.Previous.moveCart();

            Switch3.moveCart();
            currentBottom = Switch3.PreviousDown;
            for (int i = 0; i < 5; i++)
            {
                if(currentBottom.moveCart()) { finished = true; }
                currentBottom = currentBottom.Previous;
            }

            currentTop = Switch5.PreviousUp;
            for (int i = 0; i < 4; i++)
            {
                if(currentTop.moveCart()) { finished = true; }
                currentTop = currentTop.Previous;
            }

            if(Switch3.PreviousUp.moveCart()) { finished = true; }

            if (Switch2.Up) { currentTop.moveCart(); }
            else { Switch3.PreviousUp.Previous.moveCart(); }
            currentTop.Previous.moveCart();
            currentTop.Previous.Previous.moveCart();

            Switch1.moveCart();
            if(Switch1.PreviousUp.moveCart()) { finished = true; }
            Switch1.PreviousUp.Previous.moveCart();
            if(Switch1.PreviousDown.moveCart()) { finished = true; }
            Switch1.PreviousDown.Previous.moveCart();

            return finished;
        }

        public void MoveBoats()
        {
            Water currentWaterUp;
            Water currentWaterDown;
            currentWaterUp = LastUpperWater;
            currentWaterDown = LastDownWater;
            
            while (currentWaterUp != null)
            {
                currentWaterUp.moveBoat();
                currentWaterUp = currentWaterUp.Previous;
            }

            while (currentWaterDown != null)
            {
                currentWaterDown.moveBoat();
                currentWaterDown = currentWaterDown.Previous;
            }

            Random random = new Random();
            if (random.Next(10) == 1)
            {
                if (FirstUpperWater.MyBoat == null && FirstUpperWater.Next.MyBoat == null && FirstUpperWater.Next.Next.MyBoat == null)
                {
                    FirstUpperWater.newBoat();
                }
            }
            if (random.Next(10) == 0)
            {
                if (FirstDownWater.MyBoat == null && FirstDownWater.Next.MyBoat == null && FirstDownWater.Next.Next.MyBoat == null)
                {
                    FirstDownWater.newBoat();
                }
            }

        }

        public void KeyListener()
        {
            ConsoleKeyInfo cki;
            Console.TreatControlCAsInput = true;

            do
            {
                cki = Console.ReadKey();
                if (!ended && started)
                {
                    if (cki.Key == ConsoleKey.D1) { Switch1.change(); }
                    if (cki.Key == ConsoleKey.D2) { Switch2.change(); }
                    if (cki.Key == ConsoleKey.D3) { Switch3.change(); }
                    if (cki.Key == ConsoleKey.D4) { Switch4.change(); }
                    if (cki.Key == ConsoleKey.D5) { Switch5.change(); }
                    if (cki.Key == ConsoleKey.Escape) { myThread.Abort(); Environment.Exit(0); }
                    MyApp.myView.drawField(points);
                }
                else { 
                    if (cki.Key == ConsoleKey.Escape) { Environment.Exit(0); }
                    if (cki.Key == ConsoleKey.Spacebar) { MyApp.Start(); } 
                }
            } while (true);
            
        }

        public void GameLoop()
        {
            int sleepTime = 2000;
            while (running) 
            {
                if (started)
                {
                    Thread.Sleep(sleepTime);
                    doStep();
                    sleepTime -= sleepTime/50;
                    if (sleepTime < 500) { sleepTime = 500; }
                }
            }
        }
    }
}
