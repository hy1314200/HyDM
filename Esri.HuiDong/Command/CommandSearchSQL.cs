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
    public class CommandSearchSQL : EsriBaseCommand
    {
        public CommandSearchSQL()
        {
            this.m_Caption = "SQL查询";
        }
        public override System.Windows.Forms.Form CreateForm()
        {

            return new FrmSearchSQL();
        }
    }
}
