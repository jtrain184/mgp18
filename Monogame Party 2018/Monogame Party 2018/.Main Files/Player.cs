using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {
    public class Player
    {

        public enum Type
        {
            FRANK = 0,
            LOUIE,
            MANFORD,
            SUE,
            VELMA,
            WILBER
        }


        public bool isHuman;
        public E_Meeple meeple;
        public Player.Type type;
        public E_Space currSpace;

        public int coins;
        public int stars;

        public int place; // such as first place, second place, etc..

        // Other interesting information tracked throughout gameplay:
        public int totalMiniGameWins;
        public int totalMiniGameLosses;

        public int totalRedSpaceLands;
        public int totalBlueSpaceLands;
        public int totalChanceSpaceLands;
        public int totalSpecialSpaceLands;

        public int totalCoinsGained;
        public int totalCoinsLost;


        public Player(GameStateManager gameStateManager, Player.Type type, bool isHuman)
        {
            this.isHuman = isHuman;
            this.coins = 0;
            this.stars = 0;

            this.totalMiniGameWins = 0;
            this.totalMiniGameLosses = 0;

            this.totalRedSpaceLands = 0;
            this.totalBlueSpaceLands = 0;
            this.totalChanceSpaceLands = 0;
            this.totalSpecialSpaceLands = 0;

            this.totalCoinsGained = 0;
            this.totalCoinsLost = 0;

            switch (type)
            {

                case Player.Type.FRANK:
                    this.meeple = new E_Meeple(gameStateManager, new Vector2(0, 0), type);
                    this.type = type;
                    break;

                case Player.Type.LOUIE:
                    this.meeple = new E_Meeple(gameStateManager, new Vector2(0, 0), type);
                    this.type = type;
                    break;

                case Player.Type.MANFORD:
                    this.meeple = new E_Meeple(gameStateManager, new Vector2(0, 0), type);
                    this.type = type;
                    break;

                case Player.Type.SUE:
                    this.meeple = new E_Meeple(gameStateManager, new Vector2(0, 0), type);
                    this.type = type;
                    break;

                case Player.Type.VELMA:
                    this.meeple = new E_Meeple(gameStateManager, new Vector2(0, 0), type);
                    this.type = type;
                    break;

                case Player.Type.WILBER:
                    this.meeple = new E_Meeple(gameStateManager, new Vector2(0, 0), type);
                    this.type = type;
                    break;
            } // end switch


        } // end constructor



    } // end player class
}
