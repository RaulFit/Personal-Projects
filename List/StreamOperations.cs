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
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if (gzip)
            {
                using GZipStream gs = new GZipStream(stream, CompressionMode.Compress, true);
                gs.Write(Encoding.Unicode.GetBytes(text));
                return;
            }

            if (encrypt)
            {
                using CryptoStream cryptoStream = new(stream, aes.CreateEncryptor(), CryptoStreamMode.Write, true);
                cryptoStream.Write(Encoding.Unicode.GetBytes(text));
                cryptoStream.FlushFinalBlock();
                return;
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
                var output = new MemoryStream();
                using GZipStream gs = new GZipStream(stream, CompressionMode.Decompress);
                stream.Position = 0;
                gs.CopyTo(output);
                return Encoding.Unicode.GetString(output.ToArray());
            }

            if (encrypt)
            {
                using CryptoStream cryptoStream = new(stream, aes.CreateDecryptor(), CryptoStreamMode.Read, true);
                using MemoryStream output = new();
                stream.Position = 0;
                cryptoStream.CopyTo(output);
                return Encoding.Unicode.GetString(output.ToArray());
            }

            stream.Position = 0;
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}