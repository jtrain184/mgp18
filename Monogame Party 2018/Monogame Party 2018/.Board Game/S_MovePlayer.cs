using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_Party_2018
{
    public class S_MovePlayer : State
    {
        public int moveNum;
        public MenuItem.Characters currPlayer;
        // Constructor
        public S_MovePlayer(GameStateManager creator, float xPos, float yPos, int moveNum) : base(creator, xPos, yPos)
        {
            this.moveNum = moveNum;
            if (parentManager.round == null)
                this.currPlayer = MenuItem.Characters.King;
            else
                this.currPlayer = parentManager.round.currPlayer;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            
        
            S_LandAction landAction = new S_LandAction(parentManager, 0, 0);
            parentManager.AddStateQueue(landAction);
            this.flagForDeletion = true;
            Console.WriteLine("Moved " + currPlayer + " " + moveNum.ToString() + " spaces");
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
