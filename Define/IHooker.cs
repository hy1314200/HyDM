using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    public interface IHooker
    { 
        object Hook { get; }

        Guid ID { get; }
    }
}
