using NLog;

namespace MagicTDB.Utility
{
    public class HoldMe
    {
        private static int myInt;



 

        public static void SetInt(int id)
        {
            myInt = id;
            
        }
        public static int GetInt()
        {
            return myInt;
        }
    }
}

