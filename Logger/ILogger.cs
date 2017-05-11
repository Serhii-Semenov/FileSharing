using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
        void Warrning(string message);
        void Fatal(string message);
        void Info(string message);
    }
}
