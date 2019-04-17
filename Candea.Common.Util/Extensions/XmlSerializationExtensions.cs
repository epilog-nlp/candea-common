/*
This source file is under MIT License (MIT)
Copyright (c) 2017 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Candea.Common.Extensions
{
    using Serialization;

    public static partial class SerializationExtensions
    {
        private static readonly ConcurrentDictionary<Type, XmlSerializer> XmlSerializers =
            new ConcurrentDictionary<Type, XmlSerializer>();

        public static string Serialize<T>(this T obj, Encoding encoding = null)
        {
            var t = typeof(T);
            var ser = GetSerializer<T>(t);

            using (var stream = new StringWriterWithEncoding(encoding))
            {
                ser.Serialize(stream, obj);
                stream.Flush();
                return stream.ToString();
            }
        }

        private static XmlSerializer GetSerializer<T>(Type t)
        {
            XmlSerializer ser;
            if (XmlSerializers.ContainsKey(t))
                ser = XmlSerializers[t];
            else
            {
                ser = new XmlSerializer(t);
                XmlSerializers.TryAdd(t, ser);
            }
            return ser;
        }

        public static T Deserialize<T>(this string xmlData) where T : new()
        {
            var t = typeof(T);
            var ser = GetSerializer<T>(t);
            if (ser == null) return default;

            using (var reader = XmlReader.Create(new StringReader(xmlData)))
            {
                return (T)ser.Deserialize(reader);
            }
        }

    }
}
