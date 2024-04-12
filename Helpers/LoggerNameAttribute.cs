using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LoggerNameAttribute : Attribute
    {
        public string LoggerName { get; }

        public LoggerNameAttribute(string loggerName)
        {
            LoggerName = loggerName;
        }
    }
}
