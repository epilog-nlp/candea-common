/*
This source file is under MIT License (MIT)
Copyright (c) 2017 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System.IO;

namespace Candea.Common.Extensions
{
    public static partial class SerializationExtensions
    {
        public static void WriteToFile(this string data, string path)
        {
            File.WriteAllText(path, data);
        }
    }
}
