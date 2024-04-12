using MyEcoSpace.Logger.Enums;
using Newtonsoft.Json.Linq;

namespace MyEcoSpace.Logger.Configurations
{
    /// <summary>
    /// Класс конфигурации логгера
    /// </summary>
    public class LoggerConfiguration
    {
        /// <summary>
        /// Тип логгера
        /// </summary>
        public string LoggerType { get; set; }
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
}
