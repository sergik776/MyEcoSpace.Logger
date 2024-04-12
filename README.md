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
Создание экземпляра: ILogger<T> logger = new MainLogger<T>(config); // где config можно получить из метода ConfigParser.GetGonfig().
ASP.NET: Services.AddLogger<T>();

## Настройка
Для настройки файла конфигурации необходимо, что бы в проекте был файл конфигурации с названием "appconfig.json". В нем должен быть блок "LoggerConfiguration", который соответствует следующей сигнатуре:
```json
{
  "LoggerConfiguration": {
    // LogginingType - Способ логирования
	// TakeTurns - вызывает логирование у первого в очереди логгера, если он не доступен, делегирует задачу следующему логгеру в очереди
	// Parallel - логгирует комментраий во все доступные логгеры паралельно
    "LogginingType": "Parallel",  
	// GetMethodType - способ получения названия метода в котором вызвано логирование
	// HardCodeм - название метода указывает разработчик передавая в лог
	// StackTrace - название метода вытягивается из стек трейса
	// Reflection - название метода вытягивается из рефлексии (не реализовано)
    "GetMethodType": "StackTrace",
	//Список логгеров
    "Loggers": [
      {
		//LoggerType - тип логгера, можно создать свой унаследовав класс от BaseLogger или ILogger
		//Уже существующие варианты:
		//ConsoleLogger, FileLogger, SQLiteDBLogger (не реализован)
        "LoggerType": "ConsoleLogger",
		// LogLevel - Уровни логирования
		// TRAC, DEBG, INFO, WARN, EROR, CRIT
		// LogLevel - Указать с какого уровня начинаеться запись в лог. По умолчанию INFO.
        "LogLevel": "INFO",
		// DateTimeFormat - формат записи времени лога. Используете сигнатуру DateTime.ToString()
        "DateTimeFormat": "dd.MM.yyyy HH:mm:ss",
		// BufferLength - длинна буфера. Восле достижения количества сообщений, логи из буфера щаписываются в хранилище, буфер очищаеться.
        "BufferLength": 1,
		// AlarmLogLevel - Уровень лога при котором отправляеться уведосление в сервис экстренных уведомлений
        "AlarmLogLevel": "EROR"
      },
      {
        "LoggerType": "FileLogger",
        "LogLevel": "INFO",
        "DateTimeFormat": "dd.MM.yyyy HH:mm:ss",
        "BufferLength": 1,
        "AlarmLogLevel": "EROR",
        "SaveFilePath": "D://Logs"
      }
    ]
  }
}

```

## Примеры

## Лицензия
Этот проект распространяется под лицензией [MIT](https://opensource.org/licenses/MIT), которая разрешает свободное использование, изменение и распространение кода в соответствии с условиями лицензии MIT.
