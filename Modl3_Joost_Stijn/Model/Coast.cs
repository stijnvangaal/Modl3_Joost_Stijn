using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Coast : Track
    {
        public Water MyWater { get; set; }

        public void loadBoat(){
            if (Cart != null && MyWater.MyBoat != null)
            {
                Cart.unLoad();
                MyWater.MyBoat.load();
            }
        }


    }
}
