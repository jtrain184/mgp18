
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Party_2018
{
    public class PauseItem
    {

        public S_Pause.pauseOptions activeValue;
        public Vector2 screenPos;
        public Vector2 screenPosCentered;
        public string text;
        public SpriteFont font;

        public PauseItem(Vector2 initPos, string initText, SpriteFont sf, S_Pause.pauseOptions activeValue)
        {

            this.text = initText;

            // Recenter based on text:
            this.screenPos = initPos;
            this.screenPosCentered = CenterString.getCenterStringVector(initPos, this.text, sf);

            this.activeValue = activeValue;
            this.font = sf;
        }
    }
}
