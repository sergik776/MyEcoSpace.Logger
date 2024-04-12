using Microsoft.Extensions.Configuration;
using MyEcoSpace.Logger.Enums;
using MyEcoSpace.Logger.Exceptions;
using MyEcoSpace.Logger.Interfaces;
using MyEcoSpace.Logger.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger
{
    public class MainLogger<T> : BaseLogger<T>
    {
        List<ILogger<T>> loggers;

        public MainLogger(JsonConfig config) : base(config.Loggers[0])
        {
            loggers = new List<ILogger<T>>();
            foreach (var conf in config.Loggers)
            {
                switch(conf.LoggerType)
                {
                    case LoggerType.ConsoleLogger:
                        loggers.Add(new ConsoleLogger<T>(conf));
                        break;
                    case LoggerType.FileLogger:
                        loggers.Add(new FileLogger<T>((FileLoggerConfiguration)conf));
                        break;
                    default:
                        loggers.Add(new ConsoleLogger<T>(conf));
                        break;
                }
            }
        }

        protected override async Task WriteBufferToStore()
        {
            
        }

        public override async Task Log(string message, LogLevel level)
        {
            foreach(var a in loggers)
            {
                await a.Log(message, level);
            }
        }
    }
}
