using System;
using System.Globalization;
using System.IO;
using System.Linq;

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

        public static void Trash(int days = 30)
        {
            string path = Path.Combine(AppPath, "logs");
            var di = new DirectoryInfo(path);
            var fi = di.GetFiles("*.log");

            for (int i = 0; i < fi.Length; i++)
            {
                var diff = DateTime.Compare(DateTime.Now, fi[i].CreationTimeUtc);
                if (diff >= days) File.Delete(fi[i].FullName);
            }
        }

        private static void writeLog(LogType logType, params string[] message)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");

            string path = Path.Combine(AppPath, "logs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string output = Path.Combine(path, string.Format("{0}_{1}.log", logType.ToString(), DateTime.Now.ToString("yyyyMMdd", cultureInfo)));

            using (StreamWriter sw = new StreamWriter(new FileStream(output, FileMode.Append)))
            {
                try
                {
                    sw.WriteLine("{0} {1}", DateTime.Now.ToString(), string.Join(" | ", message.ToArray()));
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