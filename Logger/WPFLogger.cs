using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Logger
{
    public class WPFLogger : ILogger
    {
        public static WPFLogger Instance { get { return instance; } }
        private static readonly WPFLogger instance = new WPFLogger();
        private ListBox objName;
        private WPFLogger() { }

        public void Initialize(ListBox objName)
        {
            this.objName = objName;
        }

        public void Debug(string message) { objName.Items.Add("DEBUG : " + message); }

        public void Error(string message) { objName.Items.Add("ERROR : " + message); }

        public void Fatal(string message) { objName.Items.Add("FATAL : " + message); }

        public void Info(string message) { objName.Items.Add("INFO : " + message); }

        public void Warrning(string message) { objName.Items.Add("WARRNING : " + message); }

    }
}
