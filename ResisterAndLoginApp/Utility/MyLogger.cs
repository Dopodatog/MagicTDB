using NLog;

namespace ResisterAndLoginApp.Utility
{
    public class MyLogger : ILogger
    {
        //singleton pattern 

        private static MyLogger instance;
        private static Logger lumberjack;

        public static MyLogger GetInstance()
        {
            if(instance == null)
            {
                instance = new MyLogger();
            }
            
            return instance;
            
        }
        public static Logger GetLogger()
        {
            if(lumberjack == null)
            {
                lumberjack = LogManager.GetLogger("LogginAppRule");
                //creates a logger. The argument is found in the config file
            }
            return lumberjack;
        }
        public void Debug(string message)
        {
            GetLogger().Debug(message);
        }

        public void Error(string message)
        {
            GetLogger().Error(message);
        }

        public void Info(string message)
        {
            GetLogger().Info(message);
        }

        public void Warning(string message)
        {
            GetLogger().Warn(message);
        }
    }
}
