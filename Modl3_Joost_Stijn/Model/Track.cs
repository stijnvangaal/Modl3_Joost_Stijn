using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Track
    {

        public Track Next { get; set; }
        public Cart Cart { get; set; }
        public Boolean IsLast { get; set; }

        public Track()
        {

        }

        public void next(Track newNext){
            Next = newNext;
        }

        public void moveCart()
        {
            if (Next == null)
        	{
                if (IsLast)
                {
                    Cart = null;
                }
            }
            else
            {
                if (Next.Cart == null)
                {
                    Next.Cart = Cart;
                    Cart = null;
                }
                else
                {
                    Console.WriteLine(" Game Over");
                }
            }

        }

    }
}
