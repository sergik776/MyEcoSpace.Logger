﻿using Microsoft.Extensions.Configuration;
using MyEcoSpace.Logger.Configurations;
using MyEcoSpace.Logger.Enums;
using MyEcoSpace.Logger.Exceptions;
using MyEcoSpace.Logger.Interfaces;
using MyEcoSpace.Logger.Models.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Realizations
{
    internal class MainLogger<T> : ILogger<T>
    {
        List<ILogger<T>> loggers;
        JsonConfig config;

        public MainLogger()
        {
            config = ConfigParser.GetGonfig();
            loggers = new List<ILogger<T>>();
            foreach (var conf in config.Loggers)
            {
                var loggerType = ConfigParser.GetAllLoggerOfBaseLogger().FirstOrDefault(x => x.Key == conf.LoggerType).Value;
                if(loggerType != null)
                {
                    Type genericType = typeof(T);
                    var genericedLogger = loggerType.MakeGenericType(genericType);
                    var contsruct = genericedLogger.GetConstructors().FirstOrDefault();
                    object[] parameters = new object[] { conf };
                    object instance = contsruct.Invoke(parameters);
                    loggers.Add((ILogger<T>)instance);
                }



                //switch (conf.LoggerType)
                //{
                //    case "ConsoleLogger":
                //        loggers.Add(new ConsoleLogger<T>(conf));
                //        break;
                //    case "FileLogger":
                //        loggers.Add(new FileLogger<T>((FileLoggerConfiguration)conf));
                //        break;
                //    default:
                //        loggers.Add(new ConsoleLogger<T>(conf));
                //        break;
                //}
            }

            if (config.LogginingType == ChainResponsibilityMethod.Parallel)
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

        public async Task Trace(string message)
        {
           await Log(message, LogLevel.TRAC);
        }

        public async Task Debug(string message)
        {
            await Log(message, LogLevel.DEBG);
        }

        public async Task Info(string message)
        {
            await Log(message, LogLevel.INFO);
        }

        public async Task Warn(string message)
        {
            await Log(message, LogLevel.WARN);
        }

        public async Task Error(string message)
        {
            await Log(message, LogLevel.EROR);
        }

        public async Task Critical(string message)
        {
            await Log(message, LogLevel.CRIT);
        }

        public async Task Log(string message, LogLevel level)
        {
            await logDelegate.Invoke(message, level);
        }
    }
}
