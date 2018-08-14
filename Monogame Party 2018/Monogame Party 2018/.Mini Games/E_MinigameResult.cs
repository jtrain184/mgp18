using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Party_2018
{
    public class E_MinigameResult : Entity
    {
        // Member Variables
        public GameStateManager parentManager;
        public Texture2D placeSprite;
        public Texture2D playerMeeple;
        public string playerName;
        public Texture2D coinSprite;
        public string coinValueString;
        public Vector2 position;
        public int changeValue;

        public Texture2D background;

        public E_MinigameResult(GameStateManager parentManager, int place, Player player, Vector2 pos)
        {
            this.parentManager = parentManager;
            this.placeSprite = GetPlaceSprite(place);
            this.playerMeeple = player.meeple.sprite;
            this.playerName = player.type.ToString();
            this.coinSprite = parentManager.game.spr_coin;
            this.coinValueString = getCoinValueString(place);
            this.position = pos;



        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
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

            // Coin Sprite
            Vector2 coinPos = new Vector2(position.X + 400, playerNamePos.Y);
            sb.Draw(this.coinSprite, coinPos, Color.White);

            // Coin String
            Vector2 coinStringPos = new Vector2(coinPos.X + coinSprite.Width + 10, coinPos.Y);
            sb.DrawString(this.parentManager.game.ft_mainMenuFont, coinValueString, coinStringPos, Color.White);

            sb.End();
        }


        Texture2D GetPlaceSprite(int place)
        {
            switch (place)
            {
                case 1: return parentManager.game.spr_firstPlace;

                case 2: return parentManager.game.spr_secondPlace;

                case 3: return parentManager.game.spr_thirdPlace;

                case 4: return parentManager.game.spr_fourthPlace;

                default: // error
                    return parentManager.game.noSprite;

            } // end switch


        }


        string getCoinValueString(int place)
        {
            switch (place)
            {
                case 1:
                    this.changeValue = 10;
                    return " + 10";

                case 2:
                    this.changeValue = 5;
                    return " + 5";

                case 3:
                    this.changeValue = 0;
                    return " + 0";

                case 4:
                    this.changeValue = -3;
                    return " - 3"; ;

                default: // error
                    return "Error: Invalid Place Value";

            } // end switch
        }
    }
}
