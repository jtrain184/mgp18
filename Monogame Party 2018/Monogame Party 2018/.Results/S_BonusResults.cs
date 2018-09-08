using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{
    public class S_BonusResults : State
    {
        // Regular class variables
        public GameStateManager gsm;
        public Player minigameWinner;
        public int minigameWinnerIndex = 0;
        public Player mostCoinsWinner;
        public int mostCoinsWinnerIndex = 0;
        public int winnerIndex;
        public bool animation = false;

        // timers for drawing
        public int timer = 0;
        public const int maxTime = 120;

        // Text variables
        public List<string> text;
        public int textIndex = 0;


        // Meeple variables
        public List<Vector2> origMeeplePos = new List<Vector2>
        {
            new Vector2(150, 500),
            new Vector2(450, 500),
            new Vector2(750, 500),
            new Vector2(1050, 500)
        };
        public const int meepleHeight = 100;
        public const int meepleWidth = 100;
        public List<Vector2> currMeeplePos;
        public Vector2 awardPos = new Vector2(MGP_Constants.SCREEN_MID_X - (meepleHeight / 2), MGP_Constants.SCREEN_MID_Y - (meepleWidth / 2));
        public bool movedPlayer = false;

        // Star Variables
        public Rectangle origStarPos = new Rectangle(MGP_Constants.SCREEN_MID_X - 50, -100, 100, 100);  // star star just above center of screen
        public Rectangle curStarPos;    // current star position
        public Vector2 starAwardPos = new Vector2(MGP_Constants.SCREEN_MID_X - 75, MGP_Constants.SCREEN_MID_Y - 175); // move star to center or screen
        public List<Texture2D> stars;   // will contain the alternating sprites to make star apear to spin
        public int starIndex = 0;   // used to change what star image is drawn
        public bool movedStar = false;


        // Constructor
        public S_BonusResults(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos)
        {
            gsm = creator;

            // start meeples and star in their original positions
            currMeeplePos = new List<Vector2>(origMeeplePos);
            curStarPos = origStarPos;

            // Add star textures to list for drawing
            stars = new List<Texture2D>()
            {
                gsm.game.spr_star1,
                gsm.game.spr_star2,
                gsm.game.spr_star3,
                gsm.game.spr_star4,
                gsm.game.spr_star5

            };

            // Find winners
            minigameWinner = creator.gameOptions.players[0];
            mostCoinsWinner = creator.gameOptions.players[0];

            for (int i = 1; i < creator.gameOptions.players.Count; i++)
            {
                if (creator.gameOptions.players[i].totalMiniGameWins > minigameWinner.totalMiniGameWins)
                {
                    minigameWinner = creator.gameOptions.players[i];
                    minigameWinnerIndex = i;
                }
                if (creator.gameOptions.players[i].totalCoinsGained > mostCoinsWinner.totalCoinsGained)
                {
                    mostCoinsWinner = creator.gameOptions.players[i];
                    mostCoinsWinnerIndex = i;
                }
            }

            // Add strings to list for drawing
            text = new List<string>()
            {
                "Now to award\n the bonus stars!",
                "The winner for most\n minigames won is...",
                minigameWinner.type.ToString(),
                "The winner for most\n coins gained is ...",
                mostCoinsWinner.type.ToString()
           };

            // Give the winners each a star
            minigameWinner.stars++;
            mostCoinsWinner.stars++;

            timer = 0;
        }

        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            if (timer >= maxTime && !animation)
            {
                // Move onto final results
                if (textIndex == text.Count - 1)
                {
                    S_FinalResults finalResults = new S_FinalResults(parentManager, xPos, yPos);
                    parentManager.AddStateQueue(finalResults);
                    this.flagForDeletion = true;
                }
                // Show next text
                else
                {
                    textIndex++;
                    timer = 0;
                }

            }
            // Start animation
            else if (textIndex == 2 || textIndex == 4)
            {
                if (textIndex == 2)
                    winnerIndex = minigameWinnerIndex;
                else
                    winnerIndex = mostCoinsWinnerIndex;
                animation = true;

                // move the player towards center
                if (!movedPlayer)
                {
                    if (Vector2.Distance(currMeeplePos[winnerIndex], awardPos) < 5.0f)
                    {
                        movedPlayer = true;
                        timer = 0;
                    }
                    else
                    {
                        Vector2 temp = currMeeplePos[winnerIndex];
                        temp.X = MGP_Tools.Ease(currMeeplePos[winnerIndex].X, awardPos.X, 0.05f);
                        temp.Y = MGP_Tools.Ease(currMeeplePos[winnerIndex].Y, awardPos.Y, 0.05f);
                        currMeeplePos[winnerIndex] = new Vector2(temp.X, temp.Y);

                    }

                }
                // move the star
                else if (!movedStar)
                {
                    if (Math.Abs(curStarPos.Y - starAwardPos.Y) < 10 && timer > maxTime)
                        movedStar = true;
                    else
                    {
                        curStarPos.Y = (int)MGP_Tools.Ease(curStarPos.Y, starAwardPos.Y, 0.1f);
                    }
                }

                // move player back
                else
                {
                    // move star back up
                    curStarPos = origStarPos;

                    // Finished all animations
                    if (Vector2.Distance(currMeeplePos[winnerIndex], origMeeplePos[winnerIndex]) < 5.0f)
                    {
                        animation = false;
                        movedStar = false;
                        movedPlayer = false;
                    }
                    // ease player back to orig spot
                    else
                    {
                        Vector2 temp = currMeeplePos[winnerIndex];
                        temp.X = MGP_Tools.Ease(currMeeplePos[winnerIndex].X, origMeeplePos[winnerIndex].X, 0.1f);
                        temp.Y = MGP_Tools.Ease(currMeeplePos[winnerIndex].Y, origMeeplePos[winnerIndex].Y, 0.1f);
                        currMeeplePos[winnerIndex] = new Vector2(temp.X, temp.Y);
                    }
                } // End of moving player back
            }  // End of animation

            // Spin the star!
            if (timer % 30 < 6)
                starIndex = 0;
            else if (timer % 30 >= 6 && timer % 30 < 12)
                starIndex = 1;
            else if (timer % 30 >= 12 && timer % 30 < 18)
                starIndex = 2;
            else if (timer % 30 >= 18 && timer % 30 < 24)
                starIndex = 3;
            else
                starIndex = 4;


            timer++;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw Background:
            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();

            sb.Draw(this.parentManager.game.bg_titleScreen, new Vector2(xPos, yPos), Color.White);

            // Background Box
            Vector2 backgroundBox = new Vector2(50, 50);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, 500, 150), new Color(0, 0, 128, 200));

            // Text
            Vector2 textPos = CenterString.getCenterStringVector(new Vector2(300, 125), text[textIndex], this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, text[textIndex], textPos, Color.White);

            // Player Meeples
            for (int i = 0; i < 4; i++)
            {
                sb.Draw(gsm.gameOptions.players[i].meeple.sprite, new Rectangle((int)currMeeplePos[i].X, (int)currMeeplePos[i].Y, meepleWidth, meepleHeight), Color.White);
            }

            // Star
            sb.Draw(stars[starIndex], curStarPos, Color.White);

            sb.End();

        }
    }
}
