using Microsoft.Extensions.Configuration;
using MyEcoSpace.Logger.Enums;
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

        public static LoggerConfiguration GetConfig()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            var loggerConfigSection = Configuration.GetSection("LoggerConfiguration");
            JsonConfig JC = new JsonConfig();
            loggerConfigSection.Bind(JC);
            return JC.Loggers[0];
        }

        public MainLogger(LoggerConfiguration config) : base(config)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            var loggerConfigSection = Configuration.GetSection("LoggerConfiguration");
            JsonConfig JC = new JsonConfig();
            loggerConfigSection.Bind(JC);
            loggers = new List<ILogger<T>>();

            foreach (var c in JC.Loggers)
            {
                switch(c.LoggerType)
                {
                    case Enums.LoggerType.ConsoleLogger:
                        loggers.Add(new ConsoleLogger<T>(c));
                        break;
                    case Enums.LoggerType.FileLogger:
                        loggers.Add(new FileLogger<T>(c));
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
