using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Water
    {
        private Game game;
        public Water Next { get; set; }
        public Water Previous { get; set; }
        public Boat MyBoat { get; set; }
        public Boolean IsCoast { get; set; }
        public Boolean IsLast { get; set; }
        public Boolean IsFirst { get; set; }

        public Water(Game game)
        {
            this.game = game;
            IsCoast = false;
            IsLast = false;
            IsFirst = false;
        }

        public void newBoat()
        {
            if (IsFirst)
            {
                MyBoat = new Boat(game);
            }
        }
        public void moveBoat()
        {
            
            if (Next == null)
            {
                if (IsLast)
                {
                    MyBoat = null;
                }
            }
            else if (IsCoast)
            {
                if (MyBoat != null)
                {
                    if (MyBoat.Cargo == MyBoat.Capacity)
                    {
                        if (Next.MyBoat == null)
                        {
                            Next.MyBoat = MyBoat;
                            MyBoat = null;
                        }
                    }
                }
            } 
            else
            {
                if (Next.MyBoat == null && Next.Next == null) { Next.MyBoat = MyBoat; MyBoat = null; }
                else if (Next.MyBoat == null && Next.Next.MyBoat == null && Next.Next.Next == null) { Next.MyBoat = MyBoat; MyBoat = null; }
                else if (Next.MyBoat == null && Next.Next.MyBoat == null && Next.Next.Next.MyBoat == null) { Next.MyBoat = MyBoat; MyBoat = null; }
              
            }
        }

    }
}
