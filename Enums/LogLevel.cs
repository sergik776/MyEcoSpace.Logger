namespace MyEcoSpace.Logger.Enums
{
    /// <summary>
    /// Уровни логирования
    /// </summary>
    public enum LogLevel : byte
    {
        /// <summary>
        /// Трассировка выполнения
        /// </summary>
        TRAC = 0x05,
        /// <summary>
        /// Логи для дебага
        /// </summary>
        DEBG = 0x04,
        /// <summary>
        /// Стандартный лог
        /// </summary>
        INFO = 0x03,
        /// <summary>
        /// Предупреждение
        /// </summary>
        WARN = 0x02,
        /// <summary>
        /// Ошибка требующая исправления
        /// </summary>
        EROR = 0x01,
        /// <summary>
        /// Приложение вылетает
        /// </summary>
        CRIT = 0x00
    }
}
