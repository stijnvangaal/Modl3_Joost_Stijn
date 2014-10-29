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
            Water current           = null;
            Water upperCoastWater   = null;

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

                if (i == 17) { upperCoastWater = current; current.isCoast = true; }

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

            //first switch
            switch1 = new Switch(false);
            currentTrack.Next = switch1;
            switch1.PreviousUp = currentTrack;
            switch1.Up = true;
            switch1.Previous = switch1.PreviousUp;
            currentTrack = switch1;

            currentTrack.Next = new Track();
            currentTrack = currentTrack.Next;

            //second switch
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

            //fifth switch
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
                    coast.myWater = upperCoastWater;

                }
                else
                {
                    currentTrack.Next = new Track();
                }
                
                currentTrack = currentTrack.Next;
            }

            for (int i = 0; i < 3; i++)
            {

            }
        }

        private Track addTrack(Track current)
        {
            current.Next = new Track();
            return current.Next;
        }
    }
}
