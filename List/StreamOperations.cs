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
        private readonly string key;
        private byte[] bytes;
        private byte[] iv;

        public StreamOperations(string key = "")
        {
            this.key = key;
        }

        public void WriteToStream(MemoryStream stream, string text, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if (gzip)
            {
                using (var gZipStream = new GZipStream(stream, CompressionMode.Compress, true))
                using (var writer = new StreamWriter(gZipStream, Encoding.UTF8))
                {
                    writer.Write(text);
                }

                stream.Position = 0;
            }

            if (encrypt)
            {
                using (Aes aes = Aes.Create())
                {
                    this.bytes = Encoding.UTF8.GetBytes(this.key);
                    aes.Key = this.bytes;
                    aes.GenerateIV();
                    this.iv = aes.IV;

                    var encryptor = aes.CreateEncryptor();
                    var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
                    var writer = new StreamWriter(cryptoStream, Encoding.UTF8);
                    
                    writer.Write(text);
                }
            }

            if (!gzip && !encrypt)
            {
                var writer = new StreamWriter(stream);

                writer.Write(text);

                writer.Flush();
            }
        }

        public string ReadFromStream(MemoryStream stream, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if (gzip)
            {
                using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
                using (var reader = new StreamReader(gZipStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }

            if (encrypt)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = this.bytes;
                    aes.IV = this.iv;
                    using (var decryptor = aes.CreateDecryptor())
                    using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cryptoStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            stream.Position = 0;

            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
