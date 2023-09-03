using List;
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
            StreamLogic streamLogic = new StreamLogic(new StreamOperations());
            streamLogic.WriteToStream(stream, text);

            Assert.Equal(text, streamLogic.ReadFromStream(stream));
        }

        [Fact]
        void MethodsReadAndWriteThrowExceptionIfStreamIsNull()
        {
            MemoryStream nullStream = null;
            string text = "Text to write to the stream";
            StreamLogic streamLogic = new StreamLogic(new StreamOperations());

            Assert.Throws<ArgumentNullException>(() => streamLogic.WriteToStream(nullStream, text));
            Assert.Throws<ArgumentNullException>(() => streamLogic.ReadFromStream(nullStream));
        }

        [Fact]
        void WriteGziParamIsTrue_ShouldCompressString()
        {
            string text = "Text to compress";
            MemoryStream memoryStream = new MemoryStream();
            StreamLogic streamLogic = new StreamLogic(new StreamOperations());
            streamLogic.WriteToStream(memoryStream, text, gzip: true);
            Assert.NotEqual(text, streamLogic.ReadFromStream(memoryStream));
        }

        [Fact]
        void ReadGzipParamIsTrue_ShouldCompressAndDecompressString()
        {
            string text = "Text to compress";
            MemoryStream memoryStream = new MemoryStream();
            StreamLogic streamLogic = new StreamLogic(new StreamOperations());
            streamLogic.WriteToStream(memoryStream, text, gzip: true);
            Assert.Equal(text, streamLogic.ReadFromStream(memoryStream, gzip: true));
        }

        [Fact]
        void WriteEncryptParamIsTrue_ShouldEncryptString()
        {
            string text = "Text to encrypt";
            MemoryStream memoryStream = new MemoryStream();
            StreamLogic streamLogic = new StreamLogic(new StreamOperations());
            streamLogic.WriteToStream(memoryStream, text, encrypt: true);
            Assert.NotEqual(text, streamLogic.ReadFromStream(memoryStream));
        }

        [Fact]
        void ReadEncryptParamIsTrue_ShouldEncryptAndDecryptString()
        {
            string text = "Text to encrypt";
            MemoryStream memoryStream = new MemoryStream();
            StreamLogic streamLogic = new StreamLogic(new StreamOperations());
            streamLogic.WriteToStream(memoryStream, text, encrypt: true);
            Assert.Equal(text, streamLogic.ReadFromStream(memoryStream, encrypt: true));
        }

        [Fact]
        void StreamOperationsWorksIfBothParamsAreTrue()
        {
            string text = "Text to encrypt";
            MemoryStream memoryStream = new MemoryStream();
            StreamLogic streamLogic = new StreamLogic(new StreamOperations());
            streamLogic.WriteToStream(memoryStream, text, gzip: true, encrypt: true);
            Assert.Equal(text, streamLogic.ReadFromStream(memoryStream, gzip: true, encrypt: true));
        }
    }
}
