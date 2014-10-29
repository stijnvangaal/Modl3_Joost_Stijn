using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Cart
    {
        public Boolean Loaded { get; set; }

        public Cart()
        {
            Loaded = true;
        }

        public void unLoad()
        {
            Loaded = false;
        }
    }
}
