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
    public class S_BonusResults : State
    {
        public Player minigameWinner;
        public Player mostCoinsWinner;

        // timers for drawing
        public int timer;
        public const int introTime = 60;
        public const int bonusOneIntro = 120;
        public const int bonusOneWinner = 180;
        public const int bonusTwoIntro = 240;
        public const int bonusTwoWinner = 300;
        public int maxTime;
        public List<string> strings;
        public GameStateManager gsm;


        // Constructor
        public S_BonusResults(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            gsm = creator;
            strings = new List<string>();
            // Add strings to list for drawing
            strings.Add("Now to award\n the bonus stars!");
            strings.Add("The winner for most\n minigames won is...");
            strings.Add("The winner for most\n coins gained is ...");
            // Find winners
            minigameWinner = creator.gameOptions.players[0];
            mostCoinsWinner = creator.gameOptions.players[0];
            Console.WriteLine("Current Most Coins is " + mostCoinsWinner.totalCoinsGained);
            for (int i = 1; i < creator.gameOptions.players.Count; i++ )
            {
                Console.WriteLine("Current Most Coins is" + mostCoinsWinner.totalCoinsGained);
                if(creator.gameOptions.players[i].totalMiniGameWins > minigameWinner.totalMiniGameWins)
                {
                    minigameWinner = creator.gameOptions.players[i];
                }

                if (creator.gameOptions.players[i].totalCoinsGained > mostCoinsWinner.totalCoinsGained)
                {
                    mostCoinsWinner = creator.gameOptions.players[i];
                }

            }

            // Give the winners each a star
            minigameWinner.stars++;
            mostCoinsWinner.stars++;

            timer = 0;
        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            if (timer > bonusTwoWinner + 60)
            {
                if (gsm.km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all))
                { 
                    S_FinalResults finalResults = new S_FinalResults(parentManager, xPos, yPos);
                    parentManager.AddStateQueue(finalResults);
                    this.flagForDeletion = true;
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

            string text = "";

            if (timer < introTime)
            {
                text = strings[0];
            }
            else if(timer < bonusOneIntro)
            {
                text = strings[1];
            }
            else if (timer < bonusOneWinner)
            {
                text = minigameWinner.type.ToString();
            }
            else if (timer < bonusTwoIntro)
            {
                text = strings[2];
            }
            else if (timer < bonusTwoWinner)
            {
                text = mostCoinsWinner.type.ToString();
            }

        //    Vector2 boldTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer_Bold);
          //  sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, text, boldTextPos, Color.White);

            Vector2 textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, text, textPos, Color.Black);

        //    Vector2 sm_textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer_sm);
        //    sb.DrawString(this.parentManager.game.ft_confirmPlayer_sm, text, sm_textPos, Color.Gold);
        



        sb.End();

        }


    }
}
