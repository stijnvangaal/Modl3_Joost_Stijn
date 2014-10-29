using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modl3_Joost_Stijn.View;
using Modl3_Joost_Stijn.Model;

namespace Modl3_Joost_Stijn.Controller
{
    class Application
    {
        private View.View myView { get; set; }
        private Game myGame { get; set; }

        public Application()
        {
            myView = new View.View();
            myGame = new Game();

            myView.setField(myGame.FirstUpperWater, myGame.BarrackA, myGame.BarrackB, myGame.BarrackC, myGame.FirstDownWater);
        }
    }
}
