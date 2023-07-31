using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StreamDecorator
{
    public class StreamDecorator
    {
        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            var writer = new StreamWriter(stream);

            writer.Write(text);

            writer.Flush();
        }

        public string ReadFromStream(Stream stream, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            stream.Position = 0;

            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
