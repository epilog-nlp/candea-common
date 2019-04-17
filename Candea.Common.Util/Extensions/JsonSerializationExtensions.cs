/*
This source file is under MIT License (MIT)
Copyright (c) 2017 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Candea.Common.Extensions
{
    using static Util.auxfunc;

    public static partial class SerializationExtensions
    {
        private static JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = appSetting("Json.IncludeTypeMetadata", true)
                        ? TypeNameHandling.All : TypeNameHandling.None,
        };

        public static string ToJson<T>(this T r, JsonSerializerSettings settings = null) =>
            JsonConvert.SerializeObject(r, settings ?? SerializerSettings);

        public static T FromJson<T>(this string s, JsonSerializerSettings settings = null) =>
            JsonConvert.DeserializeObject<T>(s, settings ?? SerializerSettings);

        /// <summary>
        /// Deserializes a JSON string to an object, given a custom converter
        /// </summary>
        /// <typeparam name="T">The type of the object to be hydrated (can be an interface as well)</typeparam>
        /// <param name="s">The serialized data (json string)</param>
        /// <param name="c">The custom converter.</param>
        /// <returns>An instance of the object reconstructed from the JSON string.</returns>
        public static T FromJson<T>(this string s, JsonConverter c) =>
            JsonConvert.DeserializeObject<T>(s, c);
    }
}
