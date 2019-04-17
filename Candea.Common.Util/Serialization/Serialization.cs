/*
This source file is under MIT License (MIT)
Copyright (c) 2017 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System.IO;
using System.Text;

namespace Candea.Common.Serialization
{
    public class StringWriterWithEncoding : StringWriter
    {
        public StringWriterWithEncoding(Encoding e)
        {
            Encoding = e ?? Encoding.UTF8;
        }

        public override Encoding Encoding { get; }
    }

}
