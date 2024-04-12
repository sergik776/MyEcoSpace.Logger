using MyEcoSpace.Logger.Enums;
using MyEcoSpace.Logger.Exceptions;
using MyEcoSpace.Logger.Models.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Configurations
{
    public static class ConfigParser
    {
        public static JsonConfig GetGonfig()
        {
            JsonConfig config = new JsonConfig();
            using (StreamReader r = new StreamReader("appconfig.json"))
            {
                using StreamReader reader = new("appconfig.json");
                var json = reader.ReadToEnd();
                var jobj = JObject.Parse(json);

                JArray array = (JArray)jobj["LoggerConfiguration"]["Loggers"];

                config = JsonConvert.DeserializeObject<JsonConfig>(json);
                config.GetMethodType = Enum.Parse<GetMethodType>(jobj["LoggerConfiguration"]["GetMethodType"].ToString());
                config.LogginingType = Enum.Parse<ChainResponsibilityMethod>(jobj["LoggerConfiguration"]["LogginingType"].ToString());
                foreach (var item in array)
                {
                    var loggerType = item["LoggerType"].ToString();
                    switch (loggerType)
                    {
                        case "ConsoleLogger":
                            config.Loggers.Add(ConfigParser.GetConfig<LoggerConfiguration>(item));
                            break;
                        case "FileLogger":
                            config.Loggers.Add(ConfigParser.GetConfig<FileLoggerConfiguration>(item));
                            break;
                        default:
                            config.Loggers.Add(ConfigParser.GetConfig<LoggerConfiguration>(item));
                            break;
                    };

                }
            }
            return config;
        }

        private static T GetConfig<T>(JToken obj)
        {
            try
            {
                var config = Activator.CreateInstance(typeof(T));
                var type = typeof(T);
                var fields = type.GetProperties();
                foreach (var field in fields)
                {
                    string fieldName = field.Name;
                    JToken fieldValue = obj[fieldName];
                    if (fieldValue != null)
                    {
                        var convertedValue = fieldValue.ToObject(field.PropertyType);
                        field.SetValue(config, convertedValue);
                    }
                }
                return (T)config;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not convert"))
                {
                    Regex regex = new Regex(@"'([^']+)' to (.+)");
                    Match match = regex.Match(ex.Message);

                    if (match.Success)
                    {
                        string loggerName = match.Groups[1].Value;
                        string remainder = match.Groups[2].Value;
                        throw new ConfigLoadException(loggerName, remainder);
                    }
                }
                throw new Exception(ex.Message);
            }
        }

    }
}
