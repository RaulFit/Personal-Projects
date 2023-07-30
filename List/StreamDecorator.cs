using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace List
{
    public class StreamDecorator : Stream
    {
        readonly Stream inner;

        public StreamDecorator(Stream inner)
        {
            this.inner = inner;
        }

        public override bool CanRead => inner.CanRead;

        public override bool CanSeek => inner.CanSeek;

        public override bool CanWrite => inner.CanWrite;

        public override long Length => inner.Length;

        public override long Position { get => inner.Position; set => inner.Position = value; }

        public override void Flush()
        {
            inner.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return inner.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return inner.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            inner.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            inner.Write(buffer, offset, count);
        }

        public void ReadFromStream(string path, bool gzip = false, bool encrypt = false)
        {
            StreamReader reader = new StreamReader(path);
            string? line = reader.ReadLine();

            while (line != null)
            {
                Console.WriteLine(line);
                line = reader.ReadLine();
            }

            reader.Close();
        }

        public void WriteToStream(string path, bool gzip = false, bool encrypt = false)
        {
            StreamWriter writer = new StreamWriter(path);

            string? text = Console.ReadLine();

            writer.WriteLine(text);

            writer.Close();
        }
    }
}
