using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata.UI
{
    public interface IStandardManager
    {
        MetaStandard CurrentMetaStandard { get; }

        void Refresh();
    }
}
