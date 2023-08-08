using System;
using System.Collections.Generic;
using System.IO;
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
            MemoryStream stream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();

            string text = "Text to write to the stream";
            streamOperations.WriteToStream(stream, text);

            Assert.Equal(text, streamOperations.ReadFromStream(stream));
        }

        [Fact]
        void MethodsReadAndWriteThrowExceptionIfStreamIsNull()
        {
            MemoryStream nullStream = null;
            StreamOperations streamOperations = new StreamOperations();
            string text = "Text to write to the stream";

            Assert.Throws<ArgumentNullException>(() => streamOperations.WriteToStream(nullStream, text));
            Assert.Throws<ArgumentNullException>(() => streamOperations.ReadFromStream(nullStream));
        }

        [Fact]
        void WriteGzipParamIsTrue_ShouldCompressStream()
        {
            var stream = new MemoryStream();
            var text = "Text to be compressed";
            var streamOperations = new StreamOperations();
            streamOperations.WriteToStream(stream, text, gzip: true);
            Assert.NotEqual(text, streamOperations.ReadFromStream(stream));
        }

        [Fact]
        void ReadGzipParamIsTrue_ShouldDecompressStream()
        {
            var stream = new MemoryStream();
            var text = "Text to be compressed";
            var streamOperations = new StreamOperations();
            streamOperations.WriteToStream(stream, text, gzip: true);
            byte[] compressed = stream.ToArray();
            Assert.Equal(text, streamOperations.ReadFromStream(stream, gzip: true, bytes: compressed));
        }



        [Fact]
        void WriteEncryptParamIsTrue_ShouldEncryptStream()
        {
            string text = "Text to encrypt";
            const string passPhrase = "Sup3rS3curePass!";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, encrypt: true, passPhrase: passPhrase);
            Assert.NotEqual(text, streamOperations.ReadFromStream(memoryStream));
        }

        [Fact]
        void ReadEncryptParamIsTrue_ShouldDecryptStream()
        {
            string text = "Text to encrypt";
            const string passPhrase = "Sup3rS3curePass!";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, encrypt: true, passPhrase: passPhrase);
            byte[] encrypted = memoryStream.ToArray();
            Assert.Equal(text, streamOperations.ReadFromStream(memoryStream, encrypt: true, passPhrase: passPhrase, bytes: encrypted));
        }
    }
}
