using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDayWishMail.Models
{
    class Logger
    {
        public static int LOG_LEVEL = 0;
        private static string lastFileName;

        private static Logger objLogger = new Logger();
        public Logger()
        {
            int.TryParse(Util.getIniConfigString("Logger", "Level"), out LOG_LEVEL);
            initLogging();
        }
        private static string getFileName()
        {
            System.DateTime currentTime = default(System.DateTime);
            currentTime = DateTime.Now;
            return currentTime.Day.ToString() + currentTime.Month.ToString() + currentTime.Year.ToString() + ".txt";
        }
        private static void openLogFile(string filename)
        {
            StreamWriter outStream = default(StreamWriter);
            try
            {
                outStream = new StreamWriter(filename, true, Encoding.Default);
                //close the current log file when the new one can be opened
                closeLogFile();
                Console.SetError(outStream);
                Console.SetOut(outStream);
                lastFileName = filename;
                //ignore error...not thread safe
            }
            catch (Exception ex)
            {
            }
        }
        private static void closeLogFile()
        {
            try
            {
                LogMsg("Logging Stopped");
                Console.Error.Close();
                Console.Out.Close();
                //ignore error
            }
            catch (Exception ex)
            {
            }
        }
        private static void initLogging()
        {
            lastFileName = getFileName();
            openLogFile(lastFileName);

            LogMsg("Logging Started");
        }

        private static void LogMessage(string msg)
        {
            string newFileName = getFileName();

            if (lastFileName != newFileName)
            {
                openLogFile(newFileName);
            }
            Console.WriteLine(msg);
            Console.Out.Flush();
        }

        public static void LogError(Exception ex)
        {
            LogMessage(DateTime.Now + " - Error - " + ex.Message);
        }
        public static void LogError(string msg, Exception ex)
        {
            LogMessage(DateTime.Now + " - Error - " + msg);
            LogMessage(DateTime.Now + " - Error - " + ex.Message);
        }
        public static void LogError(string msg)
        {
            LogMessage(DateTime.Now + " - Error - " + msg);
        }
        public static void LogDebug(string msg)
        {
            if (LOG_LEVEL == 1)
            {
                LogMessage(DateTime.Now + " - Debug - " + msg);
            }
        }
        public static void LogInfo(string msg)
        {
            if (LOG_LEVEL == 2)
            {
                LogMessage(DateTime.Now + " - Info - " + msg);
            }
        }
        public static void LogMsg(string msg)
        {
            LogMessage(DateTime.Now + " - Message - " + msg);
        }
    }
}

