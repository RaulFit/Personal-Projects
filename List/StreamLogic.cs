using StreamDecorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public class StreamLogic
    {
        public IStreamOperations streamOperations;

        public StreamLogic(IStreamOperations streamOperations)
        {
            this.streamOperations = streamOperations;
        }

        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false)
        {
            this.streamOperations.WriteToStream(stream, text, gzip, encrypt);
        }

        public string ReadFromStream(Stream stream, bool gzip = false, bool encrypt = false)
        {
            return this.streamOperations.ReadFromStream(stream, gzip, encrypt);
        }
    }
}
