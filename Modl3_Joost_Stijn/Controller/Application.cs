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
        public View.View myView { get; set; }
        public Game myGame { get; set; }

        public Application()
        {
            myView = new View.View();
            myGame = new Game(this);

            myView.setField(myGame.FirstUpperWater, myGame.BarrackA, myGame.BarrackB, myGame.BarrackC, myGame.FirstDownWater);
            myView.drawFirst();
            myGame.KeyListener();
        }

        internal void endGame(int Points, int steps)
        {
            myView.endMessage(Points, steps);
        }

        internal void Start()
        {
            myView.drawField(0);
            myGame.started = true;
        }
    }
}
