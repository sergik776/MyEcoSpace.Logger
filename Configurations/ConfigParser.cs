using MyEcoSpace.Logger.Enums;
using MyEcoSpace.Logger.Exceptions;
using MyEcoSpace.Logger.Helpers;
using MyEcoSpace.Logger.Models.Config;
using MyEcoSpace.Logger.Realizations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Configurations
{
    internal static class ConfigParser
    {
        public static Dictionary<string, Type> GetAllLoggerOfBaseLogger()
        {
            var baseLoggerType = typeof(BaseLogger<>);
            Dictionary<string, Type> allLogger = new Dictionary<string, Type>();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly[] assemblies = currentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                types.AddRange(assembly.GetTypes().Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == baseLoggerType));
            }
            foreach (Type type in types)
            {
                allLogger.Add(type.Name.TrimEnd('1').Trim('`'), type);
            }

            return allLogger;
        }

        public static Dictionary<string, Type> GetAllLoggerConfigTypes()
        {
            Dictionary<string, Type> allLoggerConfiguration = new Dictionary<string, Type>();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly[] assemblies = currentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (typeof(LoggerConfiguration).IsAssignableFrom(type))
                    {
                        if (Attribute.IsDefined(type, typeof(LoggerNameAttribute)))
                        {
                            LoggerNameAttribute attribute = (LoggerNameAttribute)Attribute.GetCustomAttribute(type, typeof(LoggerNameAttribute));
                            allLoggerConfiguration.Add(attribute.LoggerName, type);
                        }
                    }
                }
            }

            return allLoggerConfiguration;
        }

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
                    var all = GetAllLoggerConfigTypes();
                    var genLogger = all.FirstOrDefault(x => x.Key == item["LoggerType"].ToString()).Value;
                    if (genLogger != null)
                    {
                        var JC = typeof(ConfigParser);
                        var met = JC.GetMethod("GetConfig");
                        var methodInfo = met.MakeGenericMethod(genLogger);
                        var q = methodInfo.Invoke(null, new object[] { item });
                        config.Loggers.Add((LoggerConfiguration)q);
                    }
                    else
                    {
                        if(ConfigParser.GetAllLoggerOfBaseLogger().Where(x=>x.Key == item["LoggerType"].ToString()).Count() > 0)
                        {
                            config.Loggers.Add(ConfigParser.GetConfig<LoggerConfiguration>(item));
                        }
                    }
                }
            }
            return config;
        }

        public static T GetConfig<T>(JToken obj)
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
