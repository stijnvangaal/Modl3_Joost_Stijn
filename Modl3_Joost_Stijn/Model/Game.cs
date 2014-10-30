using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Water FirstUpperWater { get; set; }
        public Barrack BarrackA { get; set; }
        public Barrack BarrackB { get; set; }
        public Barrack BarrackC { get; set; }
        public Water FirstDownWater { get; set; }
        public Boolean running = true;
        

        public Game()
        {
            buildField();

            Thread myThread = new Thread(new ThreadStart(gameLoop)); 


        }

        public void buildField()
        {
            FirstUpperWater = new Water();
            FirstUpperWater.IsFirst = true;
            Water previous = FirstUpperWater;
            Water current           = null;
            Water upperCoastWater   = null;
            Water bottomCoastWater = null;

            Switch switch1          = null;
            Switch switch2          = null;
            Switch switch3          = null;
            Switch switch4          = null;
            Switch switch5          = null;

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
            BarrackA.Next = currentTrack;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 1 UP
            switch1 = new Switch(false);
            currentTrack.Next = switch1;
            switch1.PreviousUp = currentTrack;
            switch1.Up = false;
            switch1.Previous = switch1.PreviousUp;
            currentTrack = switch1;

            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 2 UP
            switch2 = new Switch(true);
            currentTrack.Next = switch2;
            switch2.Previous = currentTrack;
            switch2.Up = true;
            currentTrack = switch2;
            currentTrack.Next = new Track();
            switch2.NextUp = currentTrack.Next;
            currentTrack = currentTrack.Next;

            for (int i = 0; i < 4; i++)
            {
                currentTrack.Next = new Track();
                currentTrack = currentTrack.Next;

            }

            //switch 5 UP
            switch5 = new Switch(false);
            currentTrack.Next = switch5;
            switch5.PreviousUp = currentTrack;
            switch5.Up = true;
            switch5.Previous = switch5.PreviousUp;
            currentTrack = switch5;

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
            currentTrack.Next = switch1;
            switch1.PreviousDown = currentTrack;

            //switch 2 DOWN
            switch2.NextDown = new Track();
            currentTrack = switch2.NextDown;
            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 3 UP
            switch3 = new Switch(false);
            currentTrack.Next = switch3;
            switch3.PreviousUp = currentTrack;
            switch3.Up = true;
            switch3.Previous = switch3.PreviousUp;
            currentTrack = switch3;

            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 4 UP
            switch4 = new Switch(true);
            currentTrack.Next = switch4;
            switch4.Previous = currentTrack;
            switch4.Up = false;
            currentTrack = switch4;
            currentTrack.Next = new Track();
            switch4.NextUp = currentTrack.Next;
            currentTrack = currentTrack.Next;

            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //switch 5 DOWN
            currentTrack.Next = switch5;
            switch5.PreviousDown = currentTrack;

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
            currentTrack.Next = switch3;
            switch3.PreviousDown = currentTrack;

            //switch 4 DOWN
            switch4.NextDown = new Track();
            currentTrack = switch4.NextDown;

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

        public void gameLoop()
        {
            Water currentWaterUp;
            Water currentWaterDown;
            while (running)
            {
                
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
