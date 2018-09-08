using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Monogame_Party_2018
{
    public class S_MinigameResults : State
    {
        public List<Player> results;
        public List<E_MinigameResult> entities;
        public List<Vector2> resultPos;
        public int currentIndex;
        public int waitTime;
        public const int maxWaitTime = 115; // # of frames
        public bool changedLastPlayer;

        public Texture2D background;



        // Constructor
        public S_MinigameResults(GameStateManager creator, float xPos, float yPos, List<Player> results, int minigame) : base(creator, xPos, yPos)
        {
            Console.WriteLine(parentManager.gameOptions.players[0].meeple.pos);
            // assign background based on which minigame was just played
            switch (minigame)
            {
                case 1:
                    background = parentManager.game.minigame_one_background;
                    break;
                default:
                    background = parentManager.game.mg2Alt;
                    break;
            }

            // Results are in reverse order ie. index 0 = last, index 3 = first
            this.results = new List<Player>();
            this.results = results.ToList();
            entities = new List<E_MinigameResult>();
            resultPos = new List<Vector2>();

            for (int i = 0; i <= 3; i++)
            {
                entities.Add(new E_MinigameResult(creator, Math.Abs(i - 4), results[i], new Vector2(350, -100)));
                resultPos.Add(new Vector2(350, 500 - (i * 100)));
            }

            // Add minigame win to 1st place player
            results[3].totalMiniGameWins++;

            currentIndex = 0;
            waitTime = 0;
            changedLastPlayer = false;
        }

        // Update:
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
                        // Begin next round:
                        parentManager.round.currRound++;
                        parentManager.round.playerIsPlaying = false;
                        parentManager.round.roundStart = true;
                        this.flagForDeletion = true;
                        parentManager.round.active = true;
                    }
                    else if (!changedLastPlayer)
                    {
                        // update last players coins
                        results[currentIndex].coins += entities[currentIndex].changeValue;

                        // Prevent negative coin values
                        if (results[currentIndex].coins < 0)
                        {
                            results[currentIndex].coins = 0;
                        }

                        changedLastPlayer = true;
                    }
                    waitTime++;
                }
                else
                {
                    // update players coins
                    results[currentIndex].coins += entities[currentIndex].changeValue;

                    // Prevent negative coin values
                    if (results[currentIndex].coins < 0)
                        results[currentIndex].coins = 0;

                    // ease next result
                    currentIndex++;
                }
            }
            // Continue to ease the current indexed result
            else
                entities[currentIndex].position.Y = MGP_Tools.Ease(entities[currentIndex].position.Y, resultPos[currentIndex].Y, 0.1f);
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            sb.Draw(background, new Vector2(0, 0), Color.White);

            // Background Box
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X - 300, MGP_Constants.SCREEN_MID_Y - 300);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, 600, 600), new Color(0, 0, 128, 200));

            // Title
            String title = "Minigame Results";
            Vector2 titlePos = CenterString.getCenterStringVector(new Vector2(backgroundBox.X + 300, backgroundBox.Y + 50), title, this.parentManager.game.ft_mainMenuFont);
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
