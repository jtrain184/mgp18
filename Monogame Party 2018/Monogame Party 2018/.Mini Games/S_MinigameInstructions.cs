using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class S_MinigameInstructions : State
    {
        // Constructor
        public S_MinigameInstructions(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.one))
            {
                S_Minigame1 minigame = new S_Minigame1(parentManager, 0, 0, false);
                parentManager.AddStateQueue(minigame);
                this.flagForDeletion = true;
                Console.WriteLine("Showed the minigame instructions. Now to play the minigame");
            }
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            // DEBUG IN PROGRESS
            sb.Draw(this.parentManager.game.confirmPlayerFade, new Rectangle(0, 0, MGP_Constants.SCREEN_WIDTH, MGP_Constants.SCREEN_HEIGHT), Color.White);

            sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(xPos, yPos), Color.White);

            // TODO:  Change minigame instructions based on minigame type <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            string gameInstructions = "Bomb Game\nTake turns selecting from the set of\nboxes while avoiding the bomb.\nLast player standing wins.\nPress Enter to continue";

            //sb.Draw(this.parentManager.game.spr_miniGameInstructionBox, new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), Color.White);
            Vector2 boxPos = new Vector2(650, 350);
            sb.Draw(this.parentManager.game.spr_miniGameInstructionBox, new Rectangle((int)boxPos.X - 650 / 2, (int)boxPos.Y - 250 / 2, 650, 250), Color.White);
            Vector2 textPos = CenterString.getCenterStringVector(boxPos, gameInstructions, this.parentManager.game.ft_confirmPlayer_s27);
            sb.DrawString(this.parentManager.game.ft_confirmPlayer_s27, gameInstructions, textPos, Color.White);
            sb.End();
        }
    }
}
