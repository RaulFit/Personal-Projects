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

        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

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

            if(stream is CryptoStream cryptoStream)
            {
                cryptoStream.FlushFinalBlock();
            }
        }

        public string ReadFromStream(Stream stream, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            stream.Position = 0;

            if (gzip)
            {
                stream = new GZipStream(stream, CompressionMode.Decompress, true);
            }

            if (encrypt)
            {
                stream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read, true);
            }

            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}