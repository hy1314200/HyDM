using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;

using ESRI.ArcGIS.Controls;

using Esri.HuiDong.UI;


namespace Esri.HuiDong
{
    public class CommandFeatureCode : EsriBaseCommand
    {
        public CommandFeatureCode()
        {
            this.m_Caption = "要素代码";
        }
        public override System.Windows.Forms.Form CreateForm()
        {

            return new FrmFeatureCode();
        }
    }
}
