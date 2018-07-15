using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Party_2018 {
    public class GameOptions
    {
        public MenuItem.MainMenu mapName;
        public int numPlayers;
        public MenuItem.Difficulty difficulty;
        public List<MenuItem.Characters> characters;
        public int numRounds;
        public bool allowBonus;

        //Contructor
        public GameOptions() { ; }
    }
}
