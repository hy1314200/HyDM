using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.DataManage.Standard
{
    public interface IStandardManager
    {
        void Refresh();

        StandardItem SelectedItem { get; }// set; }
    }
}
