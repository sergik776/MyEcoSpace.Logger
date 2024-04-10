namespace MyEcoSpace.Logger.Enums
{
    /// <summary>
    /// Метод ведения логов в цепочке обязанностей
    /// </summary>
    internal enum ChainResponsibilityMethod : byte
    {
        /// <summary>
        /// Переходить к следующему логгеру, если текущий ушел в ошибку
        /// </summary>
        TakeTurns,
        /// <summary>
        /// Паралельная запись во все логгеры одновременно
        /// </summary>
        Parallel
    }
}
