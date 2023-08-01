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
        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            stream = DecorateStream(stream, gzip, encrypt);

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

            DecorateStream(stream, gzip, encrypt);

            stream.Position = 0;

            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        static Stream DecorateStream(Stream stream, bool gzip, bool encrypt)
        {
            if (gzip)
            {
                return new GZipStream(stream, CompressionMode.Compress);
            }

            if (encrypt)
            {
                byte[] key = new byte[32];
                RandomNumberGenerator rng = RandomNumberGenerator.Create();
                rng.GetBytes(key);
                Aes aes = Aes.Create();
                aes.Key = key;
                ICryptoTransform encryptor = aes.CreateEncryptor();
                return new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
            }

            return stream;
        }
    }
}
