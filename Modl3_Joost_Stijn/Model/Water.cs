using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modl3_Joost_Stijn.Model
{
    class Water
    {

        public Water Next { get; set; }
        public Water Previous { get; set; }
        public Boat MyBoat { get; set; }
        public Boolean isCoast { get; set; }
    }
}
