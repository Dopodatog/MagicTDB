namespace ResisterAndLoginApp.Utility
{
    public interface ILogger
    {
        //this file needs to be named ILogger to be identified as an interface by visual studio
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
    }
}
