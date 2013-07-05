using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;
using Esri.Define;
using ESRI.ArcGIS.Controls;

using Esri.HuiDong.UI;


namespace Esri.HuiDong
{
    public class CommandDataExport : EsriBaseCommand
    {
        public CommandDataExport()
        {
            this.m_Caption = "数据导出";
        }
        public override System.Windows.Forms.Form CreateForm()
        {

            return new FrmDataExport();
        }
    }
}
