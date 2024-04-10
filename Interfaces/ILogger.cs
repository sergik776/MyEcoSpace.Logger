using MyEcoSpace.Logger.Enums;

namespace MyEcoSpace.Logger.Interfaces
{
    /// <summary>
    /// Интерфейс описывающий логирование
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILogger<T>
    {
        /// <summary>
        /// Трассировка (логирование каждого действия)
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <returns></returns>
        Task Trace(string message);
        /// <summary>
        /// Логи процесса отладки
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Debug(string message);

        /// <summary>
        /// Стандартный метод логирования
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Info(string message);
        /// <summary>
        /// Логирование предупреждений
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Warn(string message);
        /// <summary>
        /// Логирование ошибок
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Error(string message);

        /// <summary>
        /// Логирование критических ошибок, приводящих к отключению приложения
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Critical(string message);
        /// <summary>
        /// Логирование с указание уровня лога в параметрах
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="level">Уровень</param>
        /// <returns></returns>
        Task Log(string message, LogLevel level);
    }
}
