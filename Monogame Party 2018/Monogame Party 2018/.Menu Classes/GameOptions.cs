using System.Collections.Generic;

namespace Monogame_Party_2018
{
    public class GameOptions
    {
        public MenuItem.MainMenu mapName;
        public int numPlayers;
        public MenuItem.Difficulty difficulty;
        public List<Player> players;
        public int numRounds;
        public bool allowBonus;

        //Contructor
        public GameOptions()
        {
            this.players = new List<Player>();
        }
    }
}
