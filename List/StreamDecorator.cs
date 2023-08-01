using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

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

            DecorateStream(stream, gzip, encrypt);

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

        static void DecorateStream(Stream stream, bool gzip, bool encrypt)
        {
            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Compress);
            }

            if (encrypt)
            {
                byte[] key = new byte[32];
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(key);
                Aes aes = Aes.Create();
                aes.Key = key;
                aes.GenerateIV();
                ICryptoTransform encryptor = aes.CreateEncryptor();
                stream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
            }
        }
    }
}
