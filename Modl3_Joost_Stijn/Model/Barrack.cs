﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Barrack : Track
    {

        public void generateCart()
        {

        }

        public void newCart()
        {
            Cart = new Cart();
            Next.Cart = Cart;
            Cart = null;
        }
    }
}
