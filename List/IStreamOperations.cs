using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public interface IStreamOperations
    {
        public void WriteToStream(Stream stream, string text, bool gzip = false, bool encrypt = false);

        string ReadFromStream(Stream stream, bool gzip = false, bool encrypt = false);
    }
}
