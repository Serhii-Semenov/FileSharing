using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class FileLogger : ILogger
    {
        public static FileLogger Instance { get { return instance; } }
        private static readonly FileLogger instance = new FileLogger();
        private string filename;
        private FileLogger() { }

        public void Initialize(string filename)
        {
            this.filename = filename;
        }

        public void Debug(string message)
        {
            File.AppendAllText(filename, "DEBUG : " + message);
        }

        public void Error(string message)
        {
            File.AppendAllText(filename, "ERROR : " + message);
        }

        public void Warrning(string message)
        {
            File.AppendAllText(filename, "WARRNING : " + message);
        }

        public void Fatal(string message)
        {
            File.AppendAllText(filename, "FATAL : " + message);
        }

        public void Info(string message)
        {
            File.AppendAllText(filename, "INFO : " + message);
        }
    }
}
