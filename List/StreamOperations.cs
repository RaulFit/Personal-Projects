using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;

namespace StreamDecorator
{
    public class StreamOperations
    {
        private byte[] bytes;
        private readonly string key;
        private string cipherText;

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
                this.bytes = Encoding.UTF8.GetBytes(text);
                var msi = new MemoryStream(bytes);
                var gs = new GZipStream(stream, CompressionMode.Compress);
            }

            if (encrypt)
            {
                byte[] iv = new byte[16];
                Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(this.key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                CryptoStream cs = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new StreamWriter(cs))
                {
                    streamWriter.Write(text);
                }

                this.cipherText = Convert.ToBase64String(stream.ToArray());
                return;
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
                var msi = new MemoryStream(bytes);

                var gs = new GZipStream(msi, CompressionMode.Decompress);

                if (!encrypt)
                {
                    return Encoding.UTF8.GetString(msi.ToArray());
                }

            }

            if (encrypt)
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(this.cipherText);
                Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(this.key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                MemoryStream memoryStream = new MemoryStream(buffer);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                {
                    return streamReader.ReadToEnd();
                }
            }

            stream.Position = 0;

            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
