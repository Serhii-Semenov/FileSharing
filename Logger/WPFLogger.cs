﻿using System;
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
        private object objName;
        private WPFLogger() { }

        public void Initialize(object objName)
        {
            this.objName = objName;
        }

        //public void Debug(string message) { throw new NotImplementedException(); }

        public void Error(string message) { throw new NotImplementedException(); }

        public void Fatal(string message) { throw new NotImplementedException(); }

        public void Info(string message) { throw new NotImplementedException(); }

        public void Warrning(string message) { throw new NotImplementedException(); }

        public void Debug(string message)
        {
            try
            {
                //var v = (ICollection<string>)objName;
                //v.Add(message);
                var v = (ListBox)objName;
                v.Items.Add(message);
            }
            catch ( Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
