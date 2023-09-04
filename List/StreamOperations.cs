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
        private Stream writeStream;
        private Stream readStream;

        public StreamOperations(Stream writeStream)
        {
            this.writeStream = writeStream;
            this.readStream = this.writeStream;
        }

        public void WriteToStream(string text, bool gzip = false, bool encrypt = false)
        {
            if (writeStream == null)
            {
                throw new ArgumentNullException("WriteStream cannot be null.");
            }

            if (gzip)
            {
                writeStream = new GZipStream(writeStream, CompressionMode.Compress, true);
            }

            if (encrypt)
            {
                writeStream = new CryptoStream(writeStream, aes.CreateEncryptor(), CryptoStreamMode.Write, true);
            }

            var writer = new StreamWriter(writeStream);
            writer.Write(text);
            writer.Flush();

            if (writeStream is CryptoStream cryptoStream)
            {
                cryptoStream.FlushFinalBlock();
            }
        }

        public string ReadFromStream(bool gzip = false, bool encrypt = false)
        {
            if (readStream == null)
            {
                throw new ArgumentNullException("ReadStream cannot be null.");
            }

            readStream.Position = 0;

            if (gzip)
            {
                readStream = new GZipStream(readStream, CompressionMode.Decompress, true);
            }

            if (encrypt)
            {
                readStream = new CryptoStream(readStream, aes.CreateDecryptor(), CryptoStreamMode.Read, true);
            }

            var reader = new StreamReader(readStream);
            return reader.ReadToEnd();
        }
    }
}