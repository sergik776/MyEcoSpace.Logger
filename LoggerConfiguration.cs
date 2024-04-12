using MyEcoSpace.Logger.Enums;
using Newtonsoft.Json.Linq;

namespace MyEcoSpace.Logger
{
    /// <summary>
    /// Класс конфигурации логгера
    /// </summary>
    public class LoggerConfiguration
    {
        /// <summary>
        /// Тип логгера
        /// </summary>
        public LoggerType LoggerType { get; set; }
        /// <summary>
        /// Уровень логов
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// Формат даты и времени
        /// </summary>
        public string? DateTimeFormat { get; set; }
        /// <summary>
        /// Способ определения названия метода
        /// </summary>
        public GetMethodType GetMethodType { get; set; }
        /// <summary>
        /// Длинна буфера перед записью в хранилище логов
        /// </summary>
        public int BufferLength { get; set; }
        /// <summary>
        /// Уровень логирования при котором будет отправляться уведомление в сервис экстренных уведомлений
        /// </summary>
        public LogLevel AlarmLogLever { get; set; }
    }

    /// <summary>
    /// Класс конфигурации файлового логгера
    /// </summary>
    public class FileLoggerConfiguration : LoggerConfiguration
    {
        /// <summary>
        /// Путь к папке с логами
        /// </summary>
        public string? SaveFilePath { get; set; }
    }

    /// <summary>
    /// Класс конфигурации логгера базы данных
    /// </summary>
    public class DBLoggerConfiguration : LoggerConfiguration
    {
        /// <summary>
        /// Название БД
        /// </summary>
        public string? DBName { get; set; }
        /// <summary>
        /// Логин БД
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// Пароль БД
        /// </summary>
        public string? Password { get; set; }
    }
}
