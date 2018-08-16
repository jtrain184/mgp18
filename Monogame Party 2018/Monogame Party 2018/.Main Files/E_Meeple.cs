
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Party_2018
{
    public class E_Meeple : Entity
    {

        // Member variables:
        public Player.Type type;
        GameStateManager gsm;

        // Constructor
        public E_Meeple(GameStateManager gameStateManager, Vector2 pos, Player.Type type) : base()
        {
            gsm = gameStateManager;

            switch (type)
            {

                case Player.Type.FRANK:
                    this.sprite = gameStateManager.game.spr_Frank;
                    break;

                case Player.Type.LOUIE:
                    this.sprite = gameStateManager.game.spr_Louie;
                    break;

                case Player.Type.MANFORD:
                    this.sprite = gameStateManager.game.spr_Manford;
                    break;

                case Player.Type.SUE:
                    this.sprite = gameStateManager.game.spr_Sue;
                    break;

                case Player.Type.VELMA:
                    this.sprite = gameStateManager.game.spr_Velma;
                    break;

                case Player.Type.WILBER:
                    this.sprite = gameStateManager.game.spr_Wilber;
                    break;

            }


        } // end constructor

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch sb = gsm.game.spriteBatch;
            sb.Begin(transformMatrix: gsm.game.cameraObject.GetViewMatrix());

            // draw sprite by half its size:
            sb.Draw(this.sprite, new Vector2(this.getPosCenter().X + 8, this.getPosCenter().Y + 2), Color.White);
            sb.End();
        }
    }
}
