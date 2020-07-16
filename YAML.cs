using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace PoorCommonTools
{
    /// <summary>
    /// Serializing：传入对象返回字符串
    /// DeSerialzing：传入字符串与类型返回对象
    /// </summary>
    public static class Yaml
    {
        public static string Serializing(object obj)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            var serializer = new Serializer();
            serializer.Serialize(tw, obj);
            return sb.ToString();
        }
        public static T DeSerialzing<T>(string doc)
        {
            Type t = typeof(T);
            Deserializer deserializer = new Deserializer();
            return (T)deserializer.Deserialize(doc??"", t);
        }
    }
}
