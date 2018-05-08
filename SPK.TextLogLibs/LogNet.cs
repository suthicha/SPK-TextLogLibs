using System;
using System.Globalization;
using System.IO;

namespace SPK.TextLogLibs
{
    public class LogNet
    {
        public static void EventLog(params string[] message)
        {
            writeLog(LogType.EVENT, message);
        }

        public static void ErrorLog(params string[] message)
        {
            writeLog(LogType.ERROR, message);
        }

        private static void writeLog(LogType logType, params string[] message)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            string output = Path.Combine(AppPath, string.Format("{0}_{1}.log", logType.ToString(), DateTime.Now.ToString("yyyyMMdd", cultureInfo)));

            using (StreamWriter sw = new StreamWriter(new FileStream(output, FileMode.Append)))
            {
                try
                {
                    sw.WriteLine("{0} {1}", DateTime.Now.ToString(), message);
                }
                catch { }
            }
        }

        public static string AppPath
        {
            get
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }
    }

    public enum LogType
    {
        EVENT,
        ERROR
    }
}