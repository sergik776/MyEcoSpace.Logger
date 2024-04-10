# MyEcoSpace.Logger
Библиотека для логгирования приложений.

## Описание
Библиотека позволяет логгировать события. Для логгирования можно использовать встроенные классы, такие как: ConsoleLogger, FileLogger, SQLiteDBLogger. Или создать свой, на основе ILogger.
При использовании библиотеки можно настроить такие параметры как:
<br>LogLevel - уровни логгирования (Trace, Debug, Information, Warning, Error, Critical)
<br>LoggerType - указать тип логгера (ConsoleLogger, FileLogger, SQLiteDBLogger)
<br>GetMethodType - способ получения названия логгируемого метода (HardCode, StackTrace, Reflection)
<br>ChainResponsibilityMethod - метод ведения логов в цепочке обязанностей (TakeTurns, Parallel)
<br>Эти параметры можно указать в applicationconfig.json

## Установка

## Использование

## Примеры

## Лицензия
Этот проект распространяется под лицензией [MIT](https://opensource.org/licenses/MIT), которая разрешает свободное использование, изменение и распространение кода в соответствии с условиями лицензии MIT.
