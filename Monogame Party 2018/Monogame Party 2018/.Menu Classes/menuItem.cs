using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Party_2018
{
    public class MenuItem
    {

        public int activeValue;
        public String text;
        public float xPos;
        public float yPos;
        public Vector2 pos;


        public MenuItem above;
        public MenuItem right;
        public MenuItem below;
        public MenuItem left;

        public Texture2D sprite;

        public enum MainMenu
        {
            PIRATE = 0,
            MOUNTAIN,
            ABOUT,
            EXIT
        }
        public enum Difficulty
        {
            EASY = 0,
            MEDIUM,
            HARD
        }

        public enum Characters
        {
            Manford = 0,
            Louie,
            Sue,
            Frank,
            Velma,
            Wilber
        }


        public MenuItem(float x, float y, String text, int activeValue)
        {
            this.xPos = x;
            this.yPos = y;
            this.text = text;
            this.activeValue = activeValue;


        }

        public MenuItem(float x, float y, Texture2D sprite)
        {
            this.xPos = x;
            this.yPos = y;
            this.pos = new Vector2(x, y);
            this.sprite = sprite;
        }


    }
}
