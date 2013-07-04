using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public interface IConfigItemReader
    {
        string FileName { set; }

        object Args { set; }

        void Reset();

        bool Next();

        string CurrentKey { get; }

        string CurrentValue { set; }

        
    }
}
