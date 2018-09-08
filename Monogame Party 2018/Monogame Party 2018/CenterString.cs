
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Party_2018
{
    public static class CenterString {

    public static Vector2 getCenterStringVector(Vector2 origin, string text, SpriteFont sf) {

      // Measure the font:
      Vector2 measurements = sf.MeasureString(text);
      float stringWidth = measurements.X;
      float stringHeight = measurements.Y;

      float drawStringX = (float)origin.X - stringWidth / 2;
      float drawStringY = (float)origin.Y - stringHeight / 2;

      return new Vector2(drawStringX, drawStringY);
    }



  }
}
