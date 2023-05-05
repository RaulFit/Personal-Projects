using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public interface IPattern
    {
        bool Match(string text);
    }
}
