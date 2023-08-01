using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StreamDecorator.Facts
{
    public class StreamOperationsFacts
    {
        [Fact]
        void MethodsReadAndWriteWorkCorrectly()
        {
            Stream stream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();

            string text = "Text to write to the stream";
            streamOperations.WriteToStream(stream, text);

            Assert.Equal(text, streamOperations.ReadFromStream(stream));
        }

        [Fact]
        void MethodsReadAndWriteThrowExceptionIfStreamIsNull()
        {
            Stream nullStream = null;
            StreamOperations streamOperations = new StreamOperations();
            string text = "Text to write to the stream";

            Assert.Throws<ArgumentNullException>(() => streamOperations.WriteToStream(nullStream, text));
            Assert.Throws<ArgumentNullException>(() => streamOperations.ReadFromStream(nullStream));
        }

        [Fact]
        void GzipParamIsTrue_ShouldCompressStream()
        {
            var stream = new MemoryStream();
            var text = "Text to be compressed";
            var streamOperations = new StreamOperations();
            streamOperations.WriteToStream(stream, text, gzip: true);
            stream.Position = 0;
            var gzipStream = new GZipStream(stream, CompressionMode.Decompress);
            var reader = new StreamReader(gzipStream);
            var decompressedText = reader.ReadToEnd();
            Assert.Equal(text, decompressedText);
        }

        [Fact]
        void EncryptParamIsTrue_ShouldEncryptStream()
        {
            var streamOperations = new StreamOperations();
            var text = "Text to be encrypted";
            var memoryStream = new MemoryStream();

            streamOperations.WriteToStream(memoryStream, text, encrypt: true);

            memoryStream.Position = 0;

            var decryptedText = streamOperations.ReadFromStream(memoryStream, encrypt: true);

            Assert.NotEqual(text, decryptedText);
        }

        [Fact]
        void WriteAndReadWithGzipAndEncrypt_ShouldSucceed()
        {
            var streamOperations = new StreamOperations();
            var text = "Text to be compressed and encrypted";
            var memoryStream = new MemoryStream();

            streamOperations.WriteToStream(memoryStream, text, gzip: true, encrypt: true);

            memoryStream.Position = 0;

            var decryptedText = streamOperations.ReadFromStream(memoryStream, gzip: true, encrypt: true);

            Assert.NotEqual(text, decryptedText);
        }
    }
}
