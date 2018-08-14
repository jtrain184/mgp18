
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Party_2018
{
    public class E_MinigameOnePlunger : Entity
    {
        public Color color;
        public bool pressed;
        public bool isBomb;



        // Constructor
        public E_MinigameOnePlunger(State parentState, Texture2D sprite, int x, int y, Color color)
        {
            this.pressed = false;
            this.isBomb = false;
            this.parentState = parentState;
            this.pos.X = x;
            this.pos.Y = y;
            this.sprite = sprite;
            this.color = color;
        }

        public void Update_Sprite()
        {

            // If pressed change sprite to plunger down
            if (this.pressed)
            {
                this.sprite = parentState.parentManager.game.minigame_one_plungerDown;
            }
            // otherwise sprite should be plunger up
            if (!this.pressed)
            {
                this.sprite = parentState.parentManager.game.minigame_one_plungerUp;
            }
        }
    }
}
