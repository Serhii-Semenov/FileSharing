using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class EmptyLogger : ILogger
    {
        public static EmptyLogger Instance
        {
            get
            {
                if (instance == null) instance = new EmptyLogger();
                return instance;
            }
        }

        private static EmptyLogger instance;

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Warrning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
