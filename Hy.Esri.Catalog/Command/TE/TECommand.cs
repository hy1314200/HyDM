using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using TerraExplorerX;

namespace ThreeDimenDataManage.Command.TE
{
    public abstract class TECommand : BaseCommand
    {
        protected Utility.TEHookHelper m_TEHelper;
        public override void OnCreate(object hook)
        {
            m_TEHelper = hook as Utility.TEHookHelper;
        }
    }
}
