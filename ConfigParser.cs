using MyEcoSpace.Logger.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger
{
    public static class ConfigParser
    {
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
