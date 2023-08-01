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
        void MethodWriteToStreamWritesSpecifiedTextToStream()
        {
            Stream stream = new MemoryStream();
            StreamOperations decorator = new StreamOperations();

            string text = "Text to write to the stream";
            decorator.WriteToStream(stream, text);

            stream.Position = 0;
            var reader = new StreamReader(stream);
            string resultText = reader.ReadToEnd();
            Assert.Equal(text, resultText);
        }

        [Fact]
        void MethodWriteToStreamThrowsExceptionIfStreamIsNull()
        {
            Stream nullStream = null;
            StreamOperations decorator = new StreamOperations();
            string text = "Text to write to the stream";

            Assert.Throws<ArgumentNullException>(() => decorator.WriteToStream(nullStream, text));
        }

        [Fact]
        void MethodReadFromStreamThrowsExceptionIfStreamIsNull()
        {
            Stream nullStream = null;
            StreamOperations decorator = new StreamOperations();

            Assert.Throws<ArgumentNullException>(() => decorator.ReadFromStream(nullStream));
        }

        [Fact]
        void MethodReadFromStreamReadsTextFromSpecifiedStream()
        {
            Stream stream = new MemoryStream();
            StreamOperations decorator = new StreamOperations();

            string streamText = "Text from stream";

            decorator.WriteToStream(stream, streamText);

            Assert.Equal("Text from stream", decorator.ReadFromStream(stream));
        }

        [Fact]
        void GzipParamIsTrue_ShouldCompressStream()
        {
            var stream = new MemoryStream();
            var text = "Text to be compressed";
            var streamOperations = new StreamOperations();
            streamOperations.WriteToStream(stream, text, true, false);
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
            byte[] encryptedData = memoryStream.ToArray();
            var decryptedText = streamOperations.ReadFromStream(new MemoryStream(encryptedData), encrypt: true);
            Assert.NotEqual(text, decryptedText);
        }
    }
}
