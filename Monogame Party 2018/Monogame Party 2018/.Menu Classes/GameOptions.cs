using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Party_2018.Menu_Classes
{
    public class GameOptions
    {
        public MenuItem.MainMenu mapName;
        public int numPlayers;
        public MenuItem.Difficulty difficulty;
        public List<string> characters;
        public int numRounds;
        public bool allowBonus;

        //Contructor
        public GameOptions() { ; }
    }
}
