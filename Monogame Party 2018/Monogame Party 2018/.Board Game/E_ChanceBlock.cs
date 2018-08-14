using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{
    public class E_ChanceBlock : Entity
    {

        // Member Variables
        S_ChanceTime.BlockType bType;
        List<Player> players;
        int faceIndex;
        int faceIndexMax;
        int faceChangeTimer; // grows
        int faceChangeSpeed;
        const int FACE_CHANGE_SPEED_START = 24;
        bool hit;

        // Color the block after hitting it:
        Color blockColor;
        Color highlightColor;

        // Constructor
        public E_ChanceBlock(State parentState, Texture2D sprite, int x, int y, S_ChanceTime.BlockType bType, List<Player> pList) : base(parentState, sprite, x, y)
        {
            this.bType = bType;
            this.players = new List<Player>(); // this is empty for non-player type blocks

            // choosing the face for the block:
            if (bType == S_ChanceTime.BlockType.character) { faceIndexMax = 4; }
            else { faceIndexMax = ((int)S_ChanceTime.condition.MAX_CONDITIONS - 1); } // don't include MAX_CONDITIONS
            faceIndex = parentState.parentManager.random.Next(0, faceIndexMax);

            faceChangeTimer = 0;
            faceChangeSpeed = FACE_CHANGE_SPEED_START;
            foreach (Player p in pList) { players.Add(p); }

            hit = false;

            blockColor = Color.White;
            highlightColor = Color.White;
        } // end constructor




        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);
            faceChangeTimer++;

            if ((faceChangeTimer % faceChangeSpeed == 0) && (!hit))
            {
                int prevIndex = faceIndex;
                // Keep going till a new index:
                while (faceIndex == prevIndex) { faceIndex = parentState.parentManager.random.Next(0, faceIndexMax); }
            }
        } // end update function






        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch sb = this.parentState.parentManager.game.spriteBatch;
            sb.Begin();

            // Block:
            if (!hit)
                sb.Draw(this.parentState.parentManager.game.spr_chanceBlockLight, this.getPos(), Color.SaddleBrown);
            else
                sb.Draw(this.parentState.parentManager.game.spr_chanceBlockLight, this.getPos(), blockColor);

            // CHARACTER BLOCK:
            if (bType == S_ChanceTime.BlockType.character)
            {
                if (faceIndex > players.Count() - 1)
                    faceIndex = 0;
                sb.Draw(players[faceIndex].chancePicture, this.getPos(), highlightColor);
            }



            // CONDITION BLOCK:
            else if (bType == S_ChanceTime.BlockType.condition)
            {

                S_ChanceTime.condition c = (S_ChanceTime.condition)faceIndex;
                switch (c)
                {

                    // ---------- COINS ------------------------------------------
                    case S_ChanceTime.condition.leftCoin10:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowL, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance10, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.rightCoin10:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowR, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance10, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.leftCoin20:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowL, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance20, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.rightCoin20:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowR, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance20, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.leftCoin30:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowL, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance30, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.rightCoin30:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowR, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance30, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.swapCoins:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowSwap, this.getPos(), highlightColor);
                        // star slightly drawn right
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, new Vector2(this.getPos().X + 45, this.getPos().Y), Color.White);
                        break;

                    case S_ChanceTime.condition.swapStars:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowSwap, this.getPos(), highlightColor);
                        // star slightly drawn right
                        sb.Draw(this.parentState.parentManager.game.spr_chanceStar, new Vector2(this.getPos().X + 45, this.getPos().Y), Color.White);
                        break;

                    // ---------- STARS ------------------------------------------
                    case S_ChanceTime.condition.rightStar1:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowR, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceStar, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance1, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.rightStar2:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowR, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceStar, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance2, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.leftStar1:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowL, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceStar, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance1, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.leftStar2:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowL, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceStar, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance2, this.getPos(), highlightColor);
                        break;


                    // ---------- BOTH LOSE --------------------------------------
                    case S_ChanceTime.condition.bothLoseCoin10:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowDown, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance10, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.bothLoseCoin20:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowDown, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance20, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.bothLoseCoin30:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowDown, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance30, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.bothLoseStar:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowDown, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceStar, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance1, this.getPos(), highlightColor);
                        break;


                    // ---------- BOTH WIN ---------------------------------------
                    case S_ChanceTime.condition.bothGainCoin20:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowUp, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceCoin, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance20, this.getPos(), highlightColor);
                        break;

                    case S_ChanceTime.condition.bothGainStar:
                        sb.Draw(this.parentState.parentManager.game.spr_chanceArrowUp, this.getPos(), highlightColor);
                        sb.Draw(this.parentState.parentManager.game.spr_chanceStar, this.getPos(), Color.White);
                        sb.Draw(this.parentState.parentManager.game.spr_chance1, this.getPos(), highlightColor);
                        break;

                    default:
                        Console.WriteLine("Error, default switch value for ChanceBlock draw condition");
                        break;
                } // end switch
            }



            sb.End();

        } // end draw function




        public void removePlayerFace(Player p)
        {
            players.Remove(p);
            if (faceIndex == faceIndexMax)
                faceIndex--;
            faceIndexMax--;
        }

        public void hitBlock()
        {
            hit = true;
        }

        public void increaseFaceChangeSpeed(float factor)
        {
            faceChangeSpeed = (int)(faceChangeSpeed * factor);
        }


        public S_ChanceTime.condition getCurrentCondition()
        {
            return (S_ChanceTime.condition)faceIndex;
        }

        public Player getCurrentPlayer()
        {
            return players[faceIndex];
        }

        public void newBlockColor(Color c)
        {
            this.blockColor = c;
        }

        public void newHighlightColor(Color c)
        {
            this.highlightColor = c;
        }



    }
}
