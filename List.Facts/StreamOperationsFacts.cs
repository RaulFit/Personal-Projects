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
        void GzipParamIsTrue_ShouldCompressAndDecompressStream()
        {
            string text = "Text to compress";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations();
            streamOperations.WriteToStream(memoryStream, text, gzip: true);
            Assert.Equal(text, streamOperations.ReadFromStream(memoryStream, gzip: true));
        }

        [Fact]
        void EncryptParamIsTrue_ShouldEncryptAndDecryptStream()
        {
            string text = "Text to encrypt";
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations(key);
            streamOperations.WriteToStream(memoryStream, text, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(memoryStream, true));
        }

        [Fact]
        void GzipAndEncryptParamsSetOnTrue_ShouldCompressAndEncryptStream()
        {
            string text = "Text to encrypt";
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            MemoryStream memoryStream = new MemoryStream();
            StreamOperations streamOperations = new StreamOperations(key);
            streamOperations.WriteToStream(memoryStream, text, gzip:true, encrypt: true);
            Assert.Equal(text, streamOperations.ReadFromStream(memoryStream, gzip:true, encrypt: true));
        }
    }
}
