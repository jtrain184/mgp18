using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{
    public class S_MovePlayer : State
    {
        public int moveNum;
        public Player currPlayer;
        bool soundPlayed;


        // Constructor
        public S_MovePlayer(GameStateManager creator, float xPos, float yPos, int moveNum) : base(creator, xPos, yPos)
        {
            this.moveNum = moveNum;

            this.currPlayer = parentManager.round.currPlayer;

            this.soundPlayed = false;
        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks)
        {
            base.Update(gameTime, ks);

            // Move the player
            if (moveNum > 0)
            {
                // Find next space
                E_Space spaceToMoveTo = currPlayer.currSpace.spacesAhead[0];

                // Move the meeple untill it's close enough to space
                if (Vector2.Distance(spaceToMoveTo.getPosCenter(), currPlayer.meeple.getPosCenter()) > 1.0F)
                {
                    float newX = MGP_Tools.Ease(currPlayer.meeple.getPosCenter().X, spaceToMoveTo.getPosCenter().X, 0.15F);
                    float newY = MGP_Tools.Ease(currPlayer.meeple.getPosCenter().Y, spaceToMoveTo.getPosCenter().Y, 0.15F);
                    currPlayer.meeple.setPos(new Vector2(newX, newY));
                    MGP_Tools.Follow_Player(parentManager, currPlayer);

                    // Play space sound effect:
                    if (!soundPlayed)
                    {
                        parentManager.audioEngine.playSound(MGP_Constants.soundEffects.space, 0.7f);
                        soundPlayed = true;
                    }
                }
                // Meeple has arrived at new space
                else
                {
                    moveNum--;
                    currPlayer.currSpace = spaceToMoveTo;

                    // allow sound to play next time:
                    soundPlayed = false;

                    // If player passes a star
                    if (currPlayer.currSpace.type == Entity.typeSpace.star)
                    {
                        S_BuyStar buyStar = new S_BuyStar(parentManager, 0, 0);
                        parentManager.AddStateQueue(buyStar);
                        this.active = false; //pause moving player
                    }
                }
            }
            // finished moving meeple
            else
            {
                S_LandAction landAction = new S_LandAction(parentManager, 0, 0);
                parentManager.AddStateQueue(landAction);
                this.flagForDeletion = true;
            }

            // Listen for pausing here:
            ListenPause();
        }



        // Draw:
        public override void Draw(GameTime gameTime)
        {

            // DEBUG: Show the number of spaces left to move.
            SpriteBatch sb = this.parentManager.game.spriteBatch;
            sb.Begin();
            SpriteFont spacesLeft = this.parentManager.game.ft_playerUIdata;

            // Draw the move count
            if ((moveNum - 1) > 0)
            {
                sb.DrawString(spacesLeft, (moveNum - 1).ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 22, MGP_Constants.SCREEN_MID_Y - 130), Color.Black);
                sb.DrawString(spacesLeft, (moveNum - 1).ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 18, MGP_Constants.SCREEN_MID_Y - 130), Color.Black);
                sb.DrawString(spacesLeft, (moveNum - 1).ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 20, MGP_Constants.SCREEN_MID_Y - 128), Color.Black);
                sb.DrawString(spacesLeft, (moveNum - 1).ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 20, MGP_Constants.SCREEN_MID_Y - 132), Color.Black);

                sb.DrawString(spacesLeft, (moveNum - 1).ToString(), new Vector2(MGP_Constants.SCREEN_MID_X + 20, MGP_Constants.SCREEN_MID_Y - 130), Color.White);
            }

            sb.End();

        }
    }
}
