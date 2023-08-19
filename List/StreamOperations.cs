﻿using System;
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

        public StreamOperations(string key = "")
        {
            this.key = key;
        }

        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if (gzip && !encrypt)
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                using (var msi = new MemoryStream(bytes))
                {
                    using (var gs = new GZipStream(stream, CompressionMode.Compress, true))
                    {
                        msi.CopyTo(gs);
                    }
                }
                return;
            }

            if (encrypt)
            {
                using Aes aes = Aes.Create();
                aes.Key = Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(this.key), Array.Empty<byte>(), 1000, HashAlgorithmName.SHA384, 16);
                aes.IV = new byte[16];
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

            if (gzip && !encrypt)
            {
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        stream.Position = 0;
                        gs.CopyTo(mso);
                    }
                    return Encoding.UTF8.GetString(mso.ToArray());
                }
            }

            if (encrypt)
            {
                using Aes aes = Aes.Create();
                aes.Key = Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(this.key), Array.Empty<byte>(), 1000, HashAlgorithmName.SHA384, 16);
                aes.IV = new byte[16];
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