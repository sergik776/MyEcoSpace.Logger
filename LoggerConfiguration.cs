using MyEcoSpace.Logger.Enums;

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
        public string DateTimeFormat { get; set; }
        /// <summary>
        /// Способ определения названия метода
        /// </summary>
        public GetMethodType GetMethodType { get; set; }
    }

    /// <summary>
    /// Класс конфигурации файлового логгера
    /// </summary>
    public class FileLoggerConfiguration : LoggerConfiguration
    {
        /// <summary>
        /// Путь к папке с логами
        /// </summary>
        public string SaveFilePath { get; set; }
    }

    /// <summary>
    /// Класс конфигурации логгера базы данных
    /// </summary>
    public class DBLoggerConfiguration : LoggerConfiguration
    {
        /// <summary>
        /// Название БД
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// Логин БД
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Пароль БД
        /// </summary>
        public string Password { get; set; }
    }
}
