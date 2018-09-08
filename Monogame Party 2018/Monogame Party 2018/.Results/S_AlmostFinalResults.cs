using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{
    public class S_AlmostFinalResult : State
    {
        public List<Player> players;
        public List<E_BonusResult> entities;
        public List<Vector2> resultPos;
        public int currentIndex;
        public int waitTime;
        public const int maxWaitTime = 60;



        // Constructor
        public S_AlmostFinalResult(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            creator.round.updatePlayerPlaces();
            this.players = creator.gameOptions.players;

            // Sort players based on place
            this.players.Sort(delegate (Player X, Player Y)
            {
                return X.place.CompareTo(Y.place);
            });

            entities = new List<E_BonusResult>();
            resultPos = new List<Vector2>();
            for (int i = 0; i < players.Count; i++)
            {
                entities.Add(new E_BonusResult(creator, players[i].place, players[i], new Vector2(325, -100)));
                resultPos.Add(new Vector2(325, 200 + (i * 100)));

            }

            currentIndex = 0;
            waitTime = 0;

        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            // If result has finished easing
            if (Vector2.Distance(entities[currentIndex].position, resultPos[currentIndex]) < 1.0f)
            {
                // Last result has been eased
                if (currentIndex == 3)
                {
                    // Wait  time after displaying last result
                    if (waitTime >= maxWaitTime)
                    {
                        S_BonusResults bonusResults = new S_BonusResults(parentManager, xPos, yPos);
                        parentManager.AddStateQueue(bonusResults);
                        this.flagForDeletion = true;
                    }
                    waitTime++;
                }
                else
                {
                    // ease next result
                    currentIndex++;
                }
            }
            // Continue to ease the current indexed result
            else
            {
                entities[currentIndex].position.Y = MGP_Tools.Ease(entities[currentIndex].position.Y, resultPos[currentIndex].Y, 0.05f);
            }
        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            sb.Draw(parentManager.game.bg_titleScreen, new Vector2(0, 0), Color.White);

            // Background Box
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X - 325, MGP_Constants.SCREEN_MID_Y - 300);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, 650, 600), new Color(0, 0, 128, 200));

            // Title
            String title = "Current Standings";
            Vector2 titlePos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + 325, backgroundBox.Y + 50), title, this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, title, titlePos, Color.White);

            sb.End();


            // Player Result Entities
            for (int i = 0; i <= 3; i++)
            {
                entities[i].Draw(gameTime);
            }

        }


    }
}


