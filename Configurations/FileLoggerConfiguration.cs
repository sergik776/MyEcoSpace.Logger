using MyEcoSpace.Logger.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Configurations
{
    /// <summary>
    /// Класс конфигурации файлового логгера
    /// </summary>
    [LoggerName("FileLogger")]
    internal class FileLoggerConfiguration : LoggerConfiguration
    {
        /// <summary>
        /// Путь к папке с логами
        /// </summary>
        public string? SaveFilePath { get; set; }
    }
}
