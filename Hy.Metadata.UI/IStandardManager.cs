using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata.UI
{
    public interface IStandardManager
    {
        IList<MetaStandard> AllMetaStandard { get; }

        MetaStandard SelectedMetaStandard { get; }

        void SetEditStandard(MetaStandard standard);

        MetaStandard NewStandard();

        void Refresh();
    }
}
