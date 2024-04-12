using Microsoft.Extensions.Configuration;
using MyEcoSpace.Logger.Configurations;
using MyEcoSpace.Logger.Enums;
using MyEcoSpace.Logger.Exceptions;
using MyEcoSpace.Logger.Interfaces;
using MyEcoSpace.Logger.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Realizations
{
    public class MainLogger<T> : BaseLogger<T>
    {
        List<ILogger<T>> loggers;
        JsonConfig _config;

        public MainLogger(JsonConfig config) : base(config.Loggers[0])
        {
            _config = config;
            loggers = new List<ILogger<T>>();
            foreach (var conf in config.Loggers)
            {
                switch (conf.LoggerType)
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

            if (_config.LogginingType == ChainResponsibilityMethod.Parallel)
            {
                logDelegate = ParalelLog;
            }
            else
            {
                logDelegate = ForeachLog;
            }
        }

        private Func<string, LogLevel, Task> logDelegate;

        private async Task ParalelLog(string message, LogLevel level)
        {
            Parallel.ForEach(loggers, x => x.Log(message, level)); //Лоигруем паралельно во все логеры
        }
        private async Task ForeachLog(string message, LogLevel level)
        {
            foreach (var a in loggers)
            {
                try
                {
                    await a.Log(message, level);
                    break;
                }
                catch
                {

                }
            }
        }

        public override async Task Log(string message, LogLevel level)
        {
            logDelegate.Invoke(message, level);
        }

        protected override async Task WriteBufferToStore()
        {

        }
    }
}
