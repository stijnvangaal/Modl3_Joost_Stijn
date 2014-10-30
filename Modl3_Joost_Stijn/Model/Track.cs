using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Track
    {

        public Track Next { get; set; }
        public Track Previous { get; set; }
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
            if (Previous != null)
            {
                if (Previous.Cart != null)
                {
                    if (Cart == null)
                    {
                        Cart = Previous.Cart;
                        Previous.Cart = null;
                    }
                    else
                    {
                        Console.WriteLine("GameOver");
                    }
                }   
            }
            if (IsLast && Cart != null)
            {
                Cart = null;
            }

        }

    }
}
