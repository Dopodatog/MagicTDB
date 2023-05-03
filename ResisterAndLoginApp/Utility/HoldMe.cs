using NLog;

namespace MagicTDB.Utility
{
    public class HoldMe
    {
        private static int cardID;
        public static void SetInt(int id)
        {
            cardID = id;           
        }
        public static int GetInt()
        {
            return cardID;
        }
    }
}

