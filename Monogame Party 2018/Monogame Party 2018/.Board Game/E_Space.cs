using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018
{

    // A "Space" on the "Board"
    public class E_Space : Entity
    {

        // To make meeples not land on each other:
        public const int OVERLAP_SHIFT = 32;
        List<Player> playersOnSpace;
        public enum direction {
          up = 0,
          right,
          down,
          left
        }


        // Member variables
        public Entity.typeSpace type;
        public Entity.typeSpace prevType;
        public List<E_Space> spacesAhead;
        public List<E_Space> spacesBehind;

        Vector2 locSecondPlayer;
        Vector2 locThirdPlayer;
        Vector2 locFourthPlayer;

        // Constructor:
        public E_Space(State parentState, Vector2 pos, Entity.typeSpace type) : base(parentState, pos)
        {
            this.type = type;

            spacesAhead = new List<E_Space>();
            spacesBehind = new List<E_Space>();

            assignSprite(this);


            this.spriteWidth = sprite.Width;
            this.spriteHeight = sprite.Height;

            // updated later:
            playersOnSpace = new List<Player>();
            locSecondPlayer = pos;
            locThirdPlayer = pos;
            locFourthPlayer = pos;
        }




        void addAhead(E_Space newSpace) { this.spacesAhead.Add(newSpace); }
        void addBehind(E_Space newSpace) { this.spacesBehind.Add(newSpace); }
        void removeAhead(E_Space newSpace) { this.spacesAhead.Remove(newSpace); }
        void removeBehind(E_Space newSpace) { this.spacesBehind.Remove(newSpace); }

        public void assignSpaces(E_Space prev)
        {
            prev.addAhead(this);
            this.addBehind(prev);
        }

        public void assignSprite(E_Space space)
        {

            // Get graphic based on type:
            switch (space.type)
            {
                case Entity.typeSpace.blue:
                    this.sprite = parentState.parentManager.game.piece_blue64;
                    break;

                case Entity.typeSpace.red:
                    this.sprite = parentState.parentManager.game.piece_red64;
                    break;

                case Entity.typeSpace.chance:
                    this.sprite = parentState.parentManager.game.piece_chance64;
                    break;

                case Entity.typeSpace.bonus:
                    this.sprite = parentState.parentManager.game.piece_purple64;
                    break;

                case Entity.typeSpace.star:
                    this.sprite = parentState.parentManager.game.piece_star64; // TODO: make a star space sprite
                    break;

                case Entity.typeSpace.invisible:
                    this.sprite = parentState.parentManager.game.noSprite;
                    this.visible = false;
                    break;

                default:
                    this.sprite = parentState.parentManager.game.noSprite;
                    break;
            }

        }

        public void changeSpace(E_Space.typeSpace type)
        {
            // assign current type to old type before changing
            this.prevType = this.type;
            this.type = type;
            assignSprite(this);

        }

        // Update:
        public override void Update(GameTime gameTime, KeyboardState ks) { }


        // Draw:
        public override void Draw(GameTime gameTime) { }


        // Update Location of overlapping meeples:
        public void setOverlapPositions(direction dir) {

            Vector2 pos = getPosCenter();

            switch (dir) {
              case direction.up:
                locSecondPlayer = new Vector2(pos.X + OVERLAP_SHIFT, pos.Y);
                locThirdPlayer = new Vector2(pos.X - OVERLAP_SHIFT, pos.Y);
                locFourthPlayer = new Vector2(pos.X + OVERLAP_SHIFT*2, pos.Y);
                break;

              case direction.right:
                locSecondPlayer = new Vector2(pos.X, pos.Y - OVERLAP_SHIFT);
                locThirdPlayer = new Vector2(pos.X, pos.Y + OVERLAP_SHIFT);
                locFourthPlayer = new Vector2(pos.X, pos.Y - OVERLAP_SHIFT*2);
                break;

              case direction.down:
                locSecondPlayer = new Vector2(pos.X - OVERLAP_SHIFT, pos.Y);
                locThirdPlayer = new Vector2(pos.X + OVERLAP_SHIFT, pos.Y);
                locFourthPlayer = new Vector2(pos.X - OVERLAP_SHIFT*2, pos.Y);
                break;

              case direction.left:
                locSecondPlayer = new Vector2(pos.X, pos.Y + OVERLAP_SHIFT);
                locThirdPlayer = new Vector2(pos.X, pos.Y - OVERLAP_SHIFT);
                locFourthPlayer = new Vector2(pos.X, pos.Y + OVERLAP_SHIFT*2);
                break;

              default:
                break;
            } // end switch
        } // end setOverlapPositions


        // Depending on who is currently 'occupying the space' get the correct position:
        public Vector2 getMeepleLocation() {
          if (playersOnSpace.Count == 0) { return getPosCenter(); }
          else if (playersOnSpace.Count == 1) { return locSecondPlayer; }
          else if (playersOnSpace.Count == 2) { return locThirdPlayer; }
          else { return locFourthPlayer; }
        } // end getMeepleLocation


        // Add and remove meeples from a space to make sure they don't overlap
        public void occupySpace(Player p) { playersOnSpace.Add(p); }

        // Leave space and update other players if there are any:
        public void leaveSpace(Player p) {
          int num = playersOnSpace.Count - 1;
          if (num >= 3) { playersOnSpace[3].meeple.setPos(locThirdPlayer); } // set forth to third pos
          if (num >= 2) { playersOnSpace[2].meeple.setPos(locSecondPlayer); } // set third to second pos
          if (num >= 1) { playersOnSpace[1].meeple.setPos(getPosCenter()); } // set second to first pos
          playersOnSpace.Remove(p);
        }
    }
}
