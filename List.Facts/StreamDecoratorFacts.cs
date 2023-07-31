using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamDecorator.Facts
{
    public class StreamDecoratorFacts
    {
        [Fact]
        void MethodWriteToStreamWritesSpecifiedTextToStream()
        {
            Stream stream = new MemoryStream();
            StreamDecorator decorator = new StreamDecorator();

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
            StreamDecorator decorator = new StreamDecorator();
            string text = "Text to write to the stream";

            Assert.Throws<ArgumentNullException>(() => decorator.WriteToStream(nullStream, text));
        }

        [Fact]
        void MethodReadFromStreamThrowsExceptionIfStreamIsNull()
        {
            Stream nullStream = null;
            StreamDecorator decorator = new StreamDecorator();

            Assert.Throws<ArgumentNullException>(() => decorator.ReadFromStream(nullStream));
        }

        [Fact]
        void MethodReadFromStreamReadsTextFromSpecifiedStream()
        {
            Stream stream = new MemoryStream();
            StreamDecorator decorator = new StreamDecorator();

            string streamText = "Text from stream";

            decorator.WriteToStream(stream, streamText);

            Assert.Equal("Text from stream", decorator.ReadFromStream(stream));
        }
    }
}
