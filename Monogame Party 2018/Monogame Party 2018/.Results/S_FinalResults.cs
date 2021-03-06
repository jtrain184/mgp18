﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

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
            if (timer > 60)
            {
                if (gsm.km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all)) {
                    // Go back to main menu
                    // update music:
                    gsm.audioEngine.setNextSong(MGP_Constants.music.mainMenu);
                    gsm.audioEngine.playNextSong(20, true);

                    // Clear all states except audio engine and this state:
                    List<State> statesToRemove = new List<State>();
                    foreach (State s in gsm.states) {
                      if (s != gsm.audioEngine && s != this) { statesToRemove.Add(s); }
                    }

                    // Remove them:
                    foreach (State s in statesToRemove) { gsm.RemoveState(s); }

                    // Delay the update of the gameStateManager:
                    sendDelay = 2;

                    gsm.gameOptions = new GameOptions();
                    State newMenu = new S_MainMenu(gsm, 0, 0);
                    gsm.AddStateQueue(newMenu);
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

            if (timer < maxTime)
            {
                string text = "AND THE WINNER IS...";
                Vector2 boldTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer_Bold);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer_Bold, text, boldTextPos, Color.White);

                Vector2 textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer, text, textPos, Color.Black);

                Vector2 sm_textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y - 75), text, this.parentManager.game.ft_confirmPlayer_sm);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer_sm, text, sm_textPos, Color.Gold);
            }
            else
            {
                // Draw Star
                Vector2 starPos = new Vector2(MGP_Constants.SCREEN_MID_X - 150, MGP_Constants.SCREEN_MID_Y - 300);
                Texture2D star;
                if (timer % 30 < 6)
                    star = gsm.game.spr_star1;
                else if (timer % 30 >= 6 && timer % 30 < 12)
                    star = gsm.game.spr_star2;
                else if (timer % 30 >= 12 && timer % 30 < 18)
                    star = gsm.game.spr_star3;
                else if (timer % 30 >= 18 && timer % 30 < 24)
                    star = gsm.game.spr_star4;
                else
                    star = gsm.game.spr_star5;

                sb.Draw(star, starPos, Color.White);


                // Draw Player's name
                Vector2 namePos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y + 75), winner.type.ToString(), gsm.game.ft_confirmPlayer_Bold);
                sb.DrawString(gsm.game.ft_confirmPlayer_Bold, winner.type.ToString(), namePos, Color.White);

                Vector2 textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y + 75), winner.type.ToString(), this.parentManager.game.ft_confirmPlayer);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer, winner.type.ToString(), textPos, Color.Black);

                Vector2 sm_textPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y + 75), winner.type.ToString(), this.parentManager.game.ft_confirmPlayer_sm);
                sb.DrawString(this.parentManager.game.ft_confirmPlayer_sm, winner.type.ToString(), sm_textPos, Color.Gold);

                // Draw Player Meeple
                Vector2 meeplePos = new Vector2(namePos.X - 75, namePos.Y);
                sb.Draw(winner.meeple.sprite, new Rectangle((int)meeplePos.X, (int)meeplePos.Y, 72, 72), Color.White);

                string text = "Select...Return to main menu";

                Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, 675), text, parentManager.game.ft_rollDice_lg);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X - 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X + 2, smTextPos.Y), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y - 2), Color.Black);
                sb.DrawString(parentManager.game.ft_rollDice_lg, text, new Vector2(smTextPos.X, smTextPos.Y + 2), Color.Black);

                sb.DrawString(parentManager.game.ft_rollDice_lg, text, smTextPos, Color.White);


            }


            sb.End();
        }
    }
}
