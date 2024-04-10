using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <typeparam name="T">Логируемый класс</typeparam>
    public abstract class BaseLogger<T> : ILogger<T>
    {
        public GetMethodType GetMethodType {  get; private set; }
        public LoggerType LoggerType { get; private set; }
        public LogLevel LogLevel { get; private set; }
        public LogLevel AlarmLogLevel { get; private set; }
        public int BufferLength { get; private set; }
        public string DateTimeFormat { get; private set; }

        protected List<string> logBuffer;
        private readonly IEmergencyNotification? _notificationService;
        private readonly StringBuilder SB;
        private readonly Func<string, LogLevel, Task> MethodNameDelegate;

        /// <summary>
        /// Конструктор абстрактного логгера
        /// </summary>
        /// <param name="config">Конфигурационный файл</param>
        /// <exception cref="Exception">Исключение при неправильном конфигурационном файле</exception>
        public BaseLogger(LoggerConfiguration config)
        {
            GetMethodType = config.GetMethodType;
            LoggerType = config.LoggerType;
            LogLevel = config.LogLevel;
            BufferLength = config.BufferLength;
            this.DateTimeFormat = config.DateTimeFormat ?? string.Empty;
            this.AlarmLogLevel = config.AlarmLogLever;

            logBuffer = [];
            SB = new StringBuilder();

            MethodNameDelegate = GetMethodType switch
            {
                Enums.GetMethodType.HardCode => DelegateHardCode,
                GetMethodType.StackTrace => DelegateStackTrace,
                GetMethodType.Reflection => DelegateReflection,
                _ => throw new Exception("Конфигурационный файл содержит неправильное определение для GetMethodType"),
            };
        }

        /// <summary>
        /// Конструктор абстрактного логгера
        /// </summary>
        /// <param name="config">Конфигурационный файл</param>
        /// <param name="notificationService">Сервис экстренных сообщений</param>
        public BaseLogger(LoggerConfiguration config, IEmergencyNotification notificationService) : this(config)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Метод формирования лога
        /// </summary>
        /// <param name="level">Уровень</param>
        /// <param name="message">Комментарий</param>
        /// <param name="methodName">Название метода (если есть)</param>
        /// <returns>Лог</returns>
        protected virtual async Task<string> LogGeneration(LogLevel level, string message, string? methodName = null)
        {
            return await Task.Factory.StartNew(() =>
            {
                string log = string.Empty;
                SB.Append(DateTime.Now.ToString(DateTimeFormat));
                SB.Append(" | ");
                SB.Append(level.ToString());
                SB.Append(" | ");
                SB.Append(typeof(T).Name);
                SB.Append(" | ");
                SB.Append(methodName);
                SB.Append(" | ");
                SB.Append(message);
                return SB.ToString();
            });
        }

        /// <summary>
        /// Метод добавления лога в буфер
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private async Task AddLogToBuffer(LogLevel level, string message, string? methodName = null)
        {
            await Task.Factory.StartNew(async () =>
            {
                var log = await LogGeneration(level, message, methodName);
                logBuffer.Add(log);
                SB.Clear();

                if(logBuffer.Count >= BufferLength)
                {
                    await WriteBufferToStore();
                    logBuffer.Clear();
                }
                if (level == AlarmLogLevel && _notificationService != null)
                {
                    await _notificationService.SendAlarm(log);
                }
            });
        }

        /// <summary>
        /// Метод присваивания названия метода лога юзером
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="level">Уровень лога</param>
        /// <returns></returns>
        private async Task DelegateHardCode(string message, LogLevel level)
        {
            await AddLogToBuffer(level, message);
        }
        /// <summary>
        /// Метод присваивания названия метода лога с помощью StackTrace
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="level">Уровень лога</param>
        /// <returns></returns>
        private async Task DelegateStackTrace(string message, LogLevel level)
        {
            var stack = new StackTrace();
            StackFrame frame = stack.GetFrame(3);
            string methodName = frame.GetMethod().Name;
            await AddLogToBuffer(level, message, methodName);
        }
        /// <summary>
        /// Метод присваивания названия метода лога с помощью Reflection
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="level">Уровень лога</param>
        /// <returns></returns>
        private Task DelegateReflection(string message, LogLevel level)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод сохранения буфера логов в хранилище.
        /// Реализовать для каждого типа логгера индивидуально
        /// </summary>
        /// <returns></returns>
        protected abstract Task WriteBufferToStore(); 

        /// <summary>
        /// Метод логирования
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="level">Уровень лога</param>
        /// <returns></returns>
        public async Task Log(string message, LogLevel level)
        {
            await MethodNameDelegate.Invoke(message, level);
        }

        #region Синтаксический сахар
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
        #endregion
    }
}
