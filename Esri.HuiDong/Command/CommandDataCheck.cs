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
    public class CommandDataCheck : EsriBaseCommand
    {
        public CommandDataCheck()
        {
            this.m_Caption = "数据质检";
        }
        public override System.Windows.Forms.Form CreateForm()
        {

            return new FrmDataCheck();
        }
    }
}
