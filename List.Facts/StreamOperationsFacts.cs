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
            string text = "Text to write to the stream";
            StreamOperations streamOperations = new StreamOperations(new MemoryStream());
            streamOperations.WriteToStream(text);
            Assert.Equal(text, streamOperations.ReadFromStream());
        }

        [Fact]
        void MethodsReadAndWriteThrowExceptionIfStreamIsNull()
        {
            string text = "Text to write to the stream";
            StreamOperations streamOperations = new StreamOperations(null);
            Assert.Throws<ArgumentNullException>(() => streamOperations.WriteToStream(text));
            Assert.Throws<ArgumentNullException>(() => streamOperations.ReadFromStream());
        }

        [Fact]
        void WriteGziParamIsTrue_ShouldCompressString()
        {
            string text = "Text to compress";
            StreamOperations streamOperations = new StreamOperations(new MemoryStream());
            streamOperations.WriteToStream(text, gzip: true);
            Assert.NotEqual(text, streamOperations.ReadFromStream());
        }

        [Fact]
        void ReadGzipParamIsTrue_ShouldCompressAndDecompressString()
        {
            string text = "Text to compress";
            StreamOperations streamOperations = new StreamOperations(new MemoryStream());
            streamOperations.WriteToStream(text, gzip: true);
            Assert.Equal(text, streamOperations.ReadFromStream(gzip: true));
        }

        [Fact]
        void WriteEncryptParamIsTrue_ShouldEncryptString()
        {
            string text = "Text to encrypt";
            StreamOperations streamOperations = new StreamOperations(new MemoryStream());
            streamOperations.WriteToStream(text, encrypt: true);
            Assert.NotEqual(text, streamOperations.ReadFromStream());
        }

        [Fact]
        void ReadEncryptParamIsTrue_ShouldEncryptAndDecryptString()
        {
            string text = "Text to encrypt";
            StreamOperations streamOperations = new StreamOperations(new MemoryStream());
            streamOperations.WriteToStream(text, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(encrypt: true));
        }

        [Fact]
        void StreamOperationsWorksIfBothParamsAreTrue()
        {
            string text = "Text to encrypt";
            StreamOperations streamOperations = new StreamOperations(new MemoryStream());
            streamOperations.WriteToStream(text, gzip: true, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(gzip: true, encrypt: true));
        }
    }
}
