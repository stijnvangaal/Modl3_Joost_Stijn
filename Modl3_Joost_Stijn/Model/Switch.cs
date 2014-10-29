using System;
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
        public Track Previous { get; set; }
        public Track PreviousUp { get; set; }
        public Track PreviousDown { get; set; }

        public Switch(Boolean isSplit)
        {

        }

        public void next(Track newNextUp, Track newNextDown)
        {
            NextUp = newNextUp;
            NextDown = newNextDown;
        }

        public void Switch()
        {
            Up = !Up;
            if(IsSplit)
            {
                if (Up)
	            {
                    Next = NextUp;
	            }
                else
                {
                    Next = NextDown;
                }    
            }
            else{
                if (Up)
                {
                    Previous = PreviousUp;
                }
                else
                {
                    Previous = PreviousDown;
                }    
            }
        }

        

    }
}
