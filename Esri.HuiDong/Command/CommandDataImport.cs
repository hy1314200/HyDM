using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;

using ESRI.ArcGIS.Controls;

using Esri.HuiDong.UI;


namespace Esri.HuiDong
{
    public class CommandDataImport : EsriBaseCommand
    {
        public CommandDataImport()
        {
            this.m_Caption = "数据导入";
        }
        public override System.Windows.Forms.Form CreateForm()
        {

            return new FrmDataImport();
        }
    }
}
