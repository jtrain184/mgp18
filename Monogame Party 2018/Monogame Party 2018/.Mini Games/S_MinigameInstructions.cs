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
        public string gameInstructions;
        public string gameControls;

        public Texture2D moveControl;
        public Vector2 moveControlPos;
        public Texture2D confirmSelection;
        public Rectangle confirmSelectionPos;
        public Texture2D background;

        public bool shownInstructions = false;
        public string screenInstructions;

        public int boxWidth;
        public int boxHeight;
        public int minigameType;

        // Constructor
        public S_MinigameInstructions(GameStateManager creator, float xPos, float yPos, int minigame) : base(creator, xPos, yPos)
        {
            screenInstructions = "Select ... Continue";
            minigameType = minigame;
            switch (minigameType)
            {
                // Minigame 1
                case 0:
                    gameInstructions = "Game Rules:\nTake turns pressing the plungers.\nIf the BOMB blows up on you,\nyou're out!\nLast one standing wins!";
                    gameControls = "  ...  Move\n\n  ...  Press Plunger";
                    moveControl = parentManager.game.keys_move;
                    confirmSelection = parentManager.game.keys_enter;
                    background = parentManager.game.minigame_one_background;
                    boxHeight = 300;
                    boxWidth = 750;
                    moveControlPos = new Vector2(MGP_Constants.SCREEN_MID_X - (boxWidth / 2 ), MGP_Constants.SCREEN_MID_Y - 125);
                    confirmSelectionPos = new Rectangle(MGP_Constants.SCREEN_MID_X - (boxWidth / 2 - 50), MGP_Constants.SCREEN_MID_Y + 25, 125, 125);
                    break;

                // Minigame 2
                case 1:
                    gameInstructions = "Game Rules:\nQuickly press the buttons\non your track and race\nto the finish line!";
                    background = parentManager.game.minigame_two_background;
                    boxHeight = 300;
                    boxWidth = 750;
                    if (parentManager.gameOptions.numPlayers == 2)
                    {
                        gameControls = "  ...  Player 1: Race\n\n  ...  Player 2: Race";
                        moveControl = parentManager.game.keys_move;
                        confirmSelection = parentManager.game.keys_move2;
                        moveControlPos = new Vector2(MGP_Constants.SCREEN_MID_X - (boxWidth / 2), MGP_Constants.SCREEN_MID_Y - 130);
                        confirmSelectionPos = new Rectangle(MGP_Constants.SCREEN_MID_X - (boxWidth / 2), MGP_Constants.SCREEN_MID_Y - 15, parentManager.game.keys_move2.Width, parentManager.game.keys_move2.Height);
                    }
                    else
                    {
                        gameControls = "  ...     Race";
                        moveControl = parentManager.game.keys_move;
                        confirmSelection = parentManager.game.noSprite;
                        moveControlPos = new Vector2(MGP_Constants.SCREEN_MID_X - (boxWidth / 2 - 40), MGP_Constants.SCREEN_MID_Y - 75);
                        confirmSelectionPos = new Rectangle(-100,-100, 1, 1);
                    }
                   
                    
                    break;

            }
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            if (km.ActionPressed(KeyboardManager.action.select, KeyboardManager.playerIndex.all))
            {
                if (shownInstructions)
                {
                    State minigame;
                    if (minigameType == 0) { minigame = new S_Minigame1(parentManager, 0, 0, true); }
                    else { minigame = new S_Minigame2(parentManager, 0, 0, true); }
                    parentManager.AddStateQueue(minigame);
                    this.flagForDeletion = true;
                }
                else
                {
                    screenInstructions = "Select ... Start Game\nCancel ... Back to instructions";
                    shownInstructions = true;
                }
            }
            if (km.ActionPressed(KeyboardManager.action.cancel, KeyboardManager.playerIndex.all) && shownInstructions)
            {
                shownInstructions = false;
                screenInstructions = "Select ... Continue";

            }
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            // Background
            sb.Draw(background, new Vector2(0, 0), Color.White);

            // Background Box
            Vector2 backgroundBox = new Vector2(MGP_Constants.SCREEN_MID_X - boxWidth / 2, MGP_Constants.SCREEN_MID_Y - boxHeight / 2);
            sb.Draw(this.parentManager.game.spr_messageBox, new Rectangle((int)backgroundBox.X, (int)backgroundBox.Y, boxWidth, boxHeight), new Color(0, 0, 128, 200));

            // Text
            string text;
            if (shownInstructions)
            {
                text = gameControls;
                sb.Draw(moveControl, moveControlPos, Color.White);
                sb.Draw(confirmSelection, confirmSelectionPos, Color.White);

            }
            else
                text = gameInstructions;

            Vector2 textDesPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, MGP_Constants.SCREEN_MID_Y), text, this.parentManager.game.ft_mainMenuFont);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, text, new Vector2(textDesPos.X - 2, textDesPos.Y), Color.Black);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, text, new Vector2(textDesPos.X + 2, textDesPos.Y), Color.Black);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, text, new Vector2(textDesPos.X, textDesPos.Y - 2), Color.Black);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, text, new Vector2(textDesPos.X, textDesPos.Y + 2), Color.Black);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, text, textDesPos, Color.White);


            Vector2 smTextPos = CenterString.getCenterStringVector(new Vector2(MGP_Constants.SCREEN_MID_X, 675), screenInstructions, parentManager.game.ft_rollDice_lg);
            sb.DrawString(parentManager.game.ft_rollDice_lg, screenInstructions, new Vector2(smTextPos.X - 2, smTextPos.Y), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, screenInstructions, new Vector2(smTextPos.X + 2, smTextPos.Y), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, screenInstructions, new Vector2(smTextPos.X, smTextPos.Y - 2), Color.Black);
            sb.DrawString(parentManager.game.ft_rollDice_lg, screenInstructions, new Vector2(smTextPos.X, smTextPos.Y + 2), Color.Black);

            sb.DrawString(parentManager.game.ft_rollDice_lg, screenInstructions, smTextPos, Color.White);


            sb.End();
        }
    }
}
