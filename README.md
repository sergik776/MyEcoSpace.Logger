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

## Настройка
Для настройки файла конфигурации необходимо, что бы в проекте был файл конфигурации с названием "appconfig.json". В нем должен быть блок "LoggerConfiguration", который соответствует следующей сигнатуре:
```json
{
  "LoggerConfiguration": {
    "LogginingType": "Parallel",  
    "GetMethodType": "StackTrace",
    "Loggers": [
      {
        "LoggerType": "ConsoleLogger",
        "LogLevel": "INFO",
        "DateTimeFormat": "dd.MM.yyyy HH:mm:ss",
        "BufferLength": 1,
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
Пояснения:<br>

<br>LogginingType - Способ логирования
<br>TakeTurns - вызывает логирование у первого в очереди логгера, если он не доступен, делегирует задачу следующему логгеру в очереди
<br>Parallel - логгирует комментраий во все доступные логгеры паралельно<br>
<br>GetMethodType - способ получения названия метода в котором вызвано логирование
<br>HardCodeм - название метода указывает разработчик передавая в лог
<br>StackTrace - название метода вытягивается из стек трейса
<br>Reflection - название метода вытягивается из рефлексии (не реализовано)<br>
<br>Loggers - Список логгеров<br>
<br>LoggerType - тип логгера, можно создать свой унаследовав класс от BaseLogger или ILogger
<br>Уже существующие варианты:
<br>ConsoleLogger, FileLogger, SQLiteDBLogger (не реализован)<br>
<br>LogLevel - Уровни логирования (TRAC, DEBG, INFO, WARN, EROR, CRIT). Указать с какого уровня начинаеться запись в лог. По умолчанию INFO.<br>
<br>DateTimeFormat - формат записи времени лога. Используете сигнатуру DateTime.ToString()<br>
<br>BufferLength - длинна буфера. Восле достижения количества сообщений, логи из буфера щаписываются в хранилище, буфер очищаеться<br>
<br>AlarmLogLevel - Уровень лога при котором отправляеться уведосление в сервис экстренных уведомлений

## Примеры
Создание экземпляра: 
```csharp
using MyEcoSpace.Logger.Interfaces;
using MyEcoSpace.Logger;

class Program
{
    static void Main()
    {
        LoggerFactory LF = new LoggerFactory();
		//Где Т - логируеммый класс
        ILogger<T> logger = LF.Create<T>(); 
        logger.Info("Hello logger");
    }
}
```
ASP.NET: 
```csharp
using MyEcoSpace.Logger;

public static partial class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddLogger<T>();
		//Добавление ваших сервисов
		var app = builder.Build();
		//Инициализация приложения
		app.Run();
    }
}
```

## Собственная реализация
Все что вам нужно, это унаследоваться от класса BaseLogger<T>, и реализовать метод Task WriteBufferToStore().<br>
Если у вам необходимо добавить собственные конфигурации, вы так же можете унаследоваться от базовых конфигураций LoggerConfiguration, добавить свои поля и свойства. И инициализировать их, добавив в  конфигурационный фалй свою конфигурацию логгера, которая будет сответствовать сигнатуре вашей реализации LoggerConfiguration.
Фабрика логгера сама найдет новую конфигурацию соответствующую сигнатуре параметрам конструктора новой реализации логгера.


## Лицензия
Этот проект распространяется под лицензией [MIT](https://opensource.org/licenses/MIT), которая разрешает свободное использование, изменение и распространение кода в соответствии с условиями лицензии MIT.
