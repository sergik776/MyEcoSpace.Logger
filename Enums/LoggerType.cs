namespace MyEcoSpace.Logger.Enums
{
    /// <summary>
    /// Тип логгера
    /// </summary>
    public enum LoggerType : byte
    {
        /// <summary>
        /// Запись в консоль
        /// </summary>
        ConsoleLogger,
        /// <summary>
        /// Запись в файл
        /// </summary>
        FileLogger,
        /// <summary>
        /// Логгер БД SQLite
        /// </summary>
        SQLiteDBLogger
    }
}
