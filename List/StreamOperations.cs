using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace StreamDecorator
{
    public class StreamOperations
    {
        public void WriteToStream(MemoryStream stream, string text, bool gzip = false, bool encrypt = false, string passPhrase = "")
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if(gzip)
            {
                var msi = new MemoryStream(bytes);
                var gs = new GZipStream(stream, CompressionMode.Compress);
                save(msi, gs);
            }

            if (encrypt)
            {
                byte[] iv = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};
                using Aes aes = Aes.Create();
                aes.Key = DeriveKeyFromPassword(passPhrase);
                aes.IV = iv;
                CryptoStream cryptoStream = new(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(bytes);
                cryptoStream.FlushFinalBlock();
                return;
            }

            if (!gzip && !encrypt)
            {
                var writer = new StreamWriter(stream);

                writer.Write(text);

                writer.Flush();
            }
        }

        public string ReadFromStream(MemoryStream stream, bool gzip = false, bool encrypt = false, byte[]? bytes = null, string passPhrase = "")
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null.");
            }

            if(gzip)
            {
                var msi = new MemoryStream(bytes);
                var gs = new GZipStream(msi, CompressionMode.Decompress);
                save(gs, stream);
            }

            if (encrypt)
            {
                byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
                using Aes aes = Aes.Create();
                aes.Key = DeriveKeyFromPassword(passPhrase);
                aes.IV = iv;
                stream = new(bytes);
                using CryptoStream cryptoStream = new(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using MemoryStream output = new MemoryStream();
                save(cryptoStream, output);
                return Encoding.Unicode.GetString(output.ToArray());
            }

            stream.Position = 0;

            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private byte[] DeriveKeyFromPassword(string password)
        {
            var emptySalt = Array.Empty<byte>();
            var iterations = 1000;
            var desiredKeyLength = 16;
            var hashMethod = HashAlgorithmName.SHA384;
            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                             emptySalt,
                                             iterations,
                                             hashMethod,
                                             desiredKeyLength);
        }

        private void save(Stream source, Stream destination)
        {
            byte[] bytes = new byte[4096];

            int count;

            while ((count = source.Read(bytes, 0, bytes.Length)) != 0)
            {
                destination.Write(bytes, 0, count);
            }
        }
    }
}
