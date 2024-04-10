using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEcoSpace.Logger.Enums;
using MyEcoSpace.Logger.Interfaces;

namespace MyEcoSpace.Logger
{
    /// <summary>
    /// Абстрактный логгер, инициализирует конфигурации
    /// </summary>
    /// <typeparam name="T">Логгируемый класс</typeparam>
    public abstract class BaseLogger<T> : ILogger<T>
    {
        private IEmergencyNotification? _notificationService;

        public GetMethodType GetMethodType {  get; private set; }
        public LoggerType LoggerType { get; private set; }
        public LogLevel LogLevel { get; private set; }

        public BaseLogger(LoggerConfiguration config)
        {
            GetMethodType = config.GetMethodType;
            LoggerType = config.LoggerType;
            LogLevel = config.LogLevel;
        }

        public BaseLogger(LoggerConfiguration config, IEmergencyNotification notificationService) : this(config)
        {
            _notificationService = notificationService;
        }

        public abstract Task Critical(string message);
        public abstract Task Debug(string message);
        public abstract Task Error(string message);
        public abstract Task Info(string message);
        public abstract Task Trace(string message);
        public abstract Task Warn(string message);
    }
}
