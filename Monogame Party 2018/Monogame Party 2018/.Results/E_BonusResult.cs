using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Monogame_Party_2018
{
    public class E_BonusResult : E_MinigameResult
    {
        public Texture2D starSprite;
        public int starCount;

        public E_BonusResult(GameStateManager parentManager, int place, Player player, Vector2 pos) : base(parentManager, place, player, pos)
        {
            this.starSprite = parentManager.game.spr_star;
            this.starCount = player.stars;
            this.coinValueString = player.coins.ToString();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = this.parentManager.game.spriteBatch;

            sb.Begin();

            // Place Sprite
            sb.Draw(this.placeSprite, this.position, Color.White);

            // Player Meeple
            Vector2 meeplePos = new Vector2(position.X + 80, position.Y + 20);
            sb.Draw(this.playerMeeple, meeplePos, Color.White);

            // Player Name 
            Vector2 playerNamePos = new Vector2(meeplePos.X + playerMeeple.Width + 10, meeplePos.Y);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, playerName, playerNamePos, Color.White);

            // Star Sprite
            Vector2 starPos = new Vector2(position.X + 400, playerNamePos.Y);
            sb.Draw(this.starSprite, starPos, Color.White);

            // Star String
            Vector2 starStringPos = new Vector2(starPos.X + starSprite.Width + 10, starPos.Y);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, starCount.ToString(), starStringPos, Color.White);

            // Coin Sprite
            Vector2 coinPos = new Vector2(position.X + 500, playerNamePos.Y);
            sb.Draw(this.coinSprite, coinPos, Color.White);

            // Coin String
            Vector2 coinStringPos = new Vector2(coinPos.X + coinSprite.Width + 10, coinPos.Y);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, coinValueString, coinStringPos, Color.White);


            sb.End();
        }
    }
}
