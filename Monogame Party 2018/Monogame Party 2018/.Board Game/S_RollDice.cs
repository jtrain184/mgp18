using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_RollDice : State
    {

        //public Player currPlayer;
        public MenuItem.Characters currPlayer;  //Swap with Player code once we get that set up


        // Constructor
        public S_RollDice(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            currPlayer = parentManager.round.currPlayer;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            //DEBUG:
            Random random;
            random = new Random();
            
            S_MovePlayer movePlayer = new S_MovePlayer(parentManager, 0, 0, random.Next(1, 13));
            parentManager.AddStateQueue(movePlayer);
            this.flagForDeletion = true;
            Console.WriteLine(currPlayer + " has rolled");
            
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
