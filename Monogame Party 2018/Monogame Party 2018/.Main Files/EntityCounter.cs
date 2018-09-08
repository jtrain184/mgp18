namespace Monogame_Party_2018
{

    public static class EntityCounter
    {

        public static int currentCount = 0;

        // Read current number:
        public static int readCount()
        {
            return currentCount;
        }

        // Take number:
        public static int takeNumber()
        {
            currentCount++;
            return currentCount - 1; // return previous after increment
        }

    } // end class definition
}
