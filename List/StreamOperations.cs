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
        private MemoryStream outputStream;
        private byte[] encrypted;
        private readonly byte[] key;

        public StreamOperations(string key = "")
        {
            this.outputStream = new MemoryStream();
            this.key = Encoding.UTF8.GetBytes(key);
        }

        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if (gzip)
            {
                stream = new GZipStream(this.outputStream, CompressionMode.Compress);
            }

            if (encrypt)
            {
                Aes aes = Aes.Create();
                byte[] iv = new byte[16];
                aes.Key = this.key;
                aes.IV = iv;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                stream = new CryptoStream(this.outputStream, encryptor, CryptoStreamMode.Write);
            }

            var writer = new StreamWriter(stream);

            writer.Write(text);

            writer.Flush();

            this.encrypted = outputStream.ToArray();

        }

        public string ReadFromStream(Stream stream, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if (gzip)
            {
                using (var gZipStream = new GZipStream(this.outputStream, CompressionMode.Decompress))
                using (var outputStream = new MemoryStream())
                {
                    this.outputStream.Position = 0;
                    gZipStream.CopyTo(outputStream);
                    var outputBytes = outputStream.ToArray();

                    string decompressed = Encoding.UTF8.GetString(outputBytes);
                    if (!encrypt)
                    {
                        return decompressed;
                    }
                }
            }

            if (encrypt)
            {
                using Aes aes = Aes.Create();
                byte[] iv = new byte[16];
                aes.Key = this.key;
                aes.IV = iv;
                using MemoryStream input = new(encrypted);
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using CryptoStream cryptoStream = new(input, decryptor, CryptoStreamMode.Read);
                using MemoryStream output = new();
                cryptoStream.CopyTo(output);
                return Encoding.Unicode.GetString(output.ToArray());
            }
            

            stream.Position = 0;

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}