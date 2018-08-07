using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Party_2018
{
    public static class MGP_Constants
    {


        public static int SCREEN_HEIGHT = 720;
        public static int SCREEN_WIDTH = 1280;

        public static int SCREEN_MID_X = SCREEN_WIDTH / 2;
        public static int SCREEN_MID_Y = SCREEN_HEIGHT / 2;

        public static int BOARD_MAX_WIDTH = 2400;
        public static int BOARD_MAX_HEIGHT = 1800;
        public static int BOARD_MIN_WIDTH = 0;
        public static int BOARD_MIN_HEIGHT = 0;

        public static double EASY_MULTIPLIER = 0.80;
        public static double MEDIUM_MULTIPLIER = 0.65;
        public static double HARD_MULTIPLIER = 0.30;



        public enum soundEffects {
          pressStart = 0,
          space
        }

        public enum music {
          pirateBay = 0
        }





    }




}
