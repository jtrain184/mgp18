using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{
    public class S_FinalResults : State
    {
        public Player winner;
        public int timer;
        public int maxTime;
        public GameStateManager gsm;

        // Constructor
        public S_FinalResults(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            gsm = creator;
            // update current players places
            gsm.round.updatePlayerPlaces();

            // get winner
            winner = gsm.gameOptions.players.Find(x => x.place == 1);

            timer = 0;
            maxTime = 45;   // number of frames before winner is shown
        }


        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            if(timer > 60)
            {
                if(gsm.km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all)){

                    // Remove all states
                    foreach(State s in gsm.states)
                    {
                        s.flagForDeletion = true;
                    }

                    // Go back to main menu
                    S_MainMenu newMenu = new S_MainMenu(gsm, 0, 0);
                    gsm.AddStateQueue(newMenu);
                }
            }
            timer++;
        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();

            sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(xPos, yPos), Color.White);

            if(timer < maxTime)
            {
                string text = "AND THE WINNER IS...";
                Vector2 textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), text, gsm.game.ft_mainMenuFont);
                sb.DrawString(this.parentManager.game.ft_mainMenuFont, text, textPos, Color.Black);
            }
            else
            {
                // Draw Star
                Vector2 starPos = new Vector2(MGP_Constants.SCREEN_MID_X - 150, MGP_Constants.SCREEN_MID_Y - 150);
                Texture2D star;
                if(timer % 30 < 6)
                    star = gsm.game.spr_star1;
                else if(timer % 30 >= 6 && timer % 30 < 12)
                    star = gsm.game.spr_star2;
                else if (timer % 30 >= 12 && timer % 30 < 18)
                    star = gsm.game.spr_star3;
                else if (timer % 30 >= 18 && timer % 30 < 24)
                    star = gsm.game.spr_star4;
                else
                    star = gsm.game.spr_star5;



                sb.Draw(star, starPos, Color.White);

                // Draw Player Meeple

                // Draw Player's name
            }
            

            sb.End();
        }
    }
}
