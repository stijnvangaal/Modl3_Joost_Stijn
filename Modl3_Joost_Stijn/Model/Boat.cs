using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Boat
    {
        private Game game;
        public int Capacity { get; set; }
        public int Cargo { get; set; }


        public Boat(Game game)
        {
            this.game = game;
            Capacity = 8;
            Cargo = 0;
        }

        public void load()
        {
            if (Cargo < Capacity)
            {
                Cargo = Cargo + 1;
                game.points++;
                if (Cargo == Capacity) { game.points += 10; }
            }
        }
    }
}
