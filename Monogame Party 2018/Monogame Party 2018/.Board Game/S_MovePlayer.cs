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
        public Player currPlayer;
        // Constructor
        public S_MovePlayer(GameStateManager creator, float xPos, float yPos, int moveNum) : base(creator, xPos, yPos)
        {
            this.moveNum = moveNum;
            if (parentManager.round == null)
                this.currPlayer = new Player(creator, Player.Type.FRANK, true);
            else
                this.currPlayer = parentManager.round.currPlayer;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            Console.WriteLine("Moved " + currPlayer.type + " " + moveNum.ToString() + " spaces");
           
            
            S_LandAction landAction = new S_LandAction(parentManager, 0, 0);
            parentManager.AddStateQueue(landAction);
            this.flagForDeletion = true;
            
            
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = parentManager.game.spriteBatch;
            sb.Begin();
            SpriteFont pipFont = parentManager.game.ft_menuDescriptionFont;
           
            // TODO:  FIX POSITION ---------------------------------------------------------------------------------------
            sb.DrawString(pipFont, moveNum.ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 250, MGP_Constants.SCREEN_MID_Y - 125), Color.Black);
            sb.End();
        }

        public void MovePlayer ()
        {
            // Add code that moves the player to next spaces

        }
    }
}
