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
            string text = "Text to write to the stream";
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(stream, text);

            Assert.Equal(text, streamOperations.ReadFromStream(stream));
        }

        [Fact]
        void MethodsReadAndWriteThrowExceptionIfStreamIsNull()
        {
            MemoryStream nullStream = null;
            string text = "Text to write to the stream";
            StreamOperations streamOperations = new StreamOperations();

            Assert.Throws<ArgumentNullException>(() => streamOperations.WriteToStream(nullStream, text));
            Assert.Throws<ArgumentNullException>(() => streamOperations.ReadFromStream(nullStream));
        }

        [Fact]
        void WriteGziParamIsTrue_ShouldCompressString()
        {
            string text = "Text to compress";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, gzip: true);
            Assert.NotEqual(text, streamOperations.ReadFromStream(memoryStream));
        }

        [Fact]
        void ReadGzipParamIsTrue_ShouldCompressAndDecompressString()
        {
            string text = "Text to compress";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, gzip: true);
            Assert.Equal(text, streamOperations.ReadFromStream(memoryStream, gzip: true));
        }

        [Fact]
        void WriteEncryptParamIsTrue_ShouldEncryptString()
        {
            string text = "Text to encrypt";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, encrypt: true);
            Assert.NotEqual(text, streamOperations.ReadFromStream(memoryStream));
        }

        [Fact]
        void ReadEncryptParamIsTrue_ShouldEncryptAndDecryptString()
        {
            string text = "Text to encrypt";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(memoryStream, encrypt: true));
        }

        [Fact]
        void StreamOperationsWorksIfBothParamsAreTrue()
        {
            string text = "Text to encrypt";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, gzip: true, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(memoryStream, gzip: true, encrypt: true));
        }
    }
}
