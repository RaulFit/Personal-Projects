using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace StreamDecorator
{
    public class StreamOperations
    {
        private Aes aes = Aes.Create();
        private GZipStream gs;
        private CryptoStream cs;

        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Compress, true);
            }

            if (encrypt)
            {
                stream = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write, true);
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

            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }

            if (encrypt)
            {
                stream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            }

            stream.Position = 0;
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}