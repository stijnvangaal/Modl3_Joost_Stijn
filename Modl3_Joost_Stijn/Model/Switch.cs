﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Switch : Track
    {
        public Boolean Up { get; set; }
        public Boolean IsSplit { get; set; }
        public Track NextUp { get; set; }
        public Track NextDown { get; set; }
        public Track PreviousUp { get; set; }
        public Track PreviousDown { get; set; }

        public Switch(Boolean isSplit)
        {
            Up = true;
            if (isSplit)
            {
                IsSplit = true;
            }
            else
            {
                IsSplit = false;
            }
        }


        public void change()
        {
            if (this.Cart == null) { Up = !Up; }
        }

        public Boolean moveCart()
        {
            if (IsSplit)
            {
                if (IsLast && Cart != null)
                {
                    Cart = null;
                }
                else if (Previous != null)
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
                return false;
            }
            else
            {
                if (Up)
                {
                    if (PreviousUp.Cart != null)
                    {
                        if (Cart == null)
                        {
                            Cart = PreviousUp.Cart;
                            PreviousUp.Cart = null;
                        }
                        else
                        {
                            Console.WriteLine("GameOver");
                            return true;
                        }
                    }
                }
                else
                {
                    if (PreviousDown.Cart != null)
                    {
                        if (Cart == null)
                        {
                            Cart = PreviousDown.Cart;
                            PreviousDown.Cart = null;
                        }
                        else
                        {
                            Console.WriteLine("GameOver");
                            return true;
                        }
                    }
                }
                return false;
            }
        }

    }
}
