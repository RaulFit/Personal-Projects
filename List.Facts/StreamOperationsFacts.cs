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
        private StreamOperations streamOperations;

        public StreamOperationsFacts()
        {
            this.streamOperations = new StreamOperations();
        }

        public void WriteToStream(string text, bool gzip = false, bool encrypt = false)
        {
            streamOperations.WriteToStream(text, gzip, encrypt);
        }

        public string ReadFromStream(bool gzip = false, bool encrypt = false)
        {
            return streamOperations.ReadFromStream(gzip, encrypt);
        }

        [Fact]
        void MethodsReadAndWriteWorkCorrectly()
        {
            string text = "Text to write to the stream";
            StreamOperationsFacts streamOperations = new StreamOperationsFacts();
            streamOperations.WriteToStream(text);
            Assert.Equal(text, streamOperations.ReadFromStream());
        }

        [Fact]
        void WriteGziParamIsTrue_ShouldCompressString()
        {
            string text = "Text to compress";
            StreamOperationsFacts streamOperations = new StreamOperationsFacts();
            streamOperations.WriteToStream(text, gzip: true);
            Assert.NotEqual(text, streamOperations.ReadFromStream());
        }

        [Fact]
        void ReadGzipParamIsTrue_ShouldCompressAndDecompressString()
        {
            string text = "Text to compress";
            StreamOperationsFacts streamOperations = new StreamOperationsFacts();
            streamOperations.WriteToStream(text, gzip: true);
            Assert.Equal(text, streamOperations.ReadFromStream(gzip: true));
        }

        [Fact]
        void WriteEncryptParamIsTrue_ShouldEncryptString()
        {
            string text = "Text to encrypt";
            StreamOperationsFacts streamOperations = new StreamOperationsFacts();
            streamOperations.WriteToStream(text, encrypt: true);
            Assert.NotEqual(text, streamOperations.ReadFromStream());
        }

        [Fact]
        void ReadEncryptParamIsTrue_ShouldEncryptAndDecryptString()
        {
            string text = "Text to encrypt";
            StreamOperationsFacts streamOperations = new StreamOperationsFacts();
            streamOperations.WriteToStream(text, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(encrypt: true));
        }

        [Fact]
        void StreamOperationsWorksIfBothParamsAreTrue()
        {
            string text = "Text to encrypt";
            StreamOperationsFacts streamOperations = new StreamOperationsFacts();
            streamOperations.WriteToStream(text, gzip: true, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(gzip: true, encrypt: true));
        }
    }
}
