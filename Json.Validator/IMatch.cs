using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public interface IMatch
    {
        bool Success();

        string RemainingText();

        string ModifiedText();
    }
}
