using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace StreamDecorator.Facts
{
    public class StreamOperationsFacts
    {
        [Fact]
        void MethodsReadAndWriteWorkCorrectly()
        {
            string text = "Text to write to the stream";
            StreamOperations streamOperations = new StreamOperations();
            var ms = new MemoryStream();
            streamOperations.WriteToStream(ms, text);
            ms.Position = 0;
            Assert.Equal(text, streamOperations.ReadFromStream(ms));
        }

        [Fact]
        void WriteGziParamIsTrue_ShouldCompressString()
        {
            string text = "Text to compress";
            StreamOperations streamOperations = new StreamOperations();
            var ms = new MemoryStream();
            var gs = new GZipStream(ms, CompressionMode.Compress, true);
            streamOperations.WriteToStream(gs, text);
            ms.Position = 0;
            Assert.NotEqual(text, streamOperations.ReadFromStream(ms));
        }

        [Fact]
        void ReadGzipParamIsTrue_ShouldCompressAndDecompressString()
        {
            string text = "Text to compress";
            StreamOperations streamOperations = new StreamOperations();
            var ms = new MemoryStream();
            var gs = new GZipStream(ms, CompressionMode.Compress, true);
            streamOperations.WriteToStream(gs, text);
            gs = new GZipStream(ms, CompressionMode.Decompress, true);
            ms.Position = 0;
            Assert.Equal(text, streamOperations.ReadFromStream(gs));
        }

        [Fact]
        void WriteEncryptParamIsTrue_ShouldEncryptString()
        {
            string text = "Text to encrypt";
            StreamOperations streamOperations = new StreamOperations();
            var ms = new MemoryStream();
            Aes aes = Aes.Create();
            var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write, true);
            streamOperations.WriteToStream(cs, text);
            ms.Position = 0;
            Assert.NotEqual(text, streamOperations.ReadFromStream(ms));
        }

        [Fact]
        void ReadEncryptParamIsTrue_ShouldEncryptAndDecryptString()
        {
            string text = "Text to encrypt";
            StreamOperations streamOperations = new StreamOperations();
            var ms = new MemoryStream();
            Aes aes = Aes.Create();
            var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write, true);
            streamOperations.WriteToStream(cs, text);
            cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read, true);
            ms.Position = 0;
            Assert.Equal(text, streamOperations.ReadFromStream(cs));
        }

        [Fact]
        void StreamOperationsWorksIfBothParamsAreTrue()
        {
            string text = "Text to compress and encrypt";
            StreamOperations streamOperations = new StreamOperations();
            var ms = new MemoryStream();
            var gs = new GZipStream(ms, CompressionMode.Compress, true);
            Aes aes = Aes.Create();
            var cs = new CryptoStream(gs, aes.CreateEncryptor(), CryptoStreamMode.Write, true);
            streamOperations.WriteToStream(cs, text);
            gs = new GZipStream(ms, CompressionMode.Decompress, true);
            cs = new CryptoStream(gs, aes.CreateDecryptor(), CryptoStreamMode.Read, true);
            ms.Position = 0;
            Assert.Equal(text, streamOperations.ReadFromStream(cs));
        }
    }
}
