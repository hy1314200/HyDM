using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Skyline.UrbanConstruction.Bussiness
{
    internal class DisplayProxy
    {
        public static void Display(DataRow datasource, Control uc)
        {
            // 清空
            foreach (Control txt in uc.Controls)
            {
                if (txt is DevExpress.XtraEditors.TextEdit)
                {
                    (txt as DevExpress.XtraEditors.TextEdit).Text = null;
                }
            }

            // 赋值
            if (datasource != null)
            {
                foreach (Control txt in uc.Controls)
                {
                    if (txt is DevExpress.XtraEditors.TextEdit)
                    {
                        (txt as DevExpress.XtraEditors.TextEdit).Text = datasource[txt.Tag as string].ToString();
                    }
                }
            }
        }

        private static double[] m_Building=
        {
            62608.03,46277.71,1,
            62583.22,46322.92,1,
            62618.75,46345.22,1,
            62633.85,46343.66,1,
            62648.53,46314.92,1,
            62631.56,46302.79,1,
            62634.40,46293.78,1

        };
        private static double[] m_Solution =
        {
            62785.88, 45975.14,1,
            62298.33,46402.36,1,
            62212.69,46526.04,1,
            62252.07,46545.68,1,
            62301.40,46480.90,1,
            62620.57,46789.66,1,
            62974.68,46540.28,1,
            62843.16,46386.10,1,
            63036.88,46185.49,1,
            62816.67,45950.13,1
        };
        private static double[] m_Project=
        {
            62816.67,45950.13,1,
            62325.76,46382.56,1,
            62575.65,46625.64,1,
            63053.74,46197.75,1
        };

        private static void FlyTo(double[] coords,string strTitle)
        {
            int id = Program.IInfoTree.FindItem(strTitle);
            if(id>0)
            {
                Program.IInfoTree.DeleteItem(id);                
            }
            TerraExplorerX.IPolygon p= Program.sgworld.Creator.GeometryCreator.CreatePolygonGeometry(coords);
            TerraExplorerX.ITerrainPolygon61 pp = Program.sgworld.Creator.CreatePolygon(p, 0x0000ff, 0x00ff00, TerraExplorerX.AltitudeTypeCode.ATC_TERRAIN_RELATIVE, 0, strTitle);

            Program.pNavigate6.FlyTo(pp);
        }
        public static void GotoBuilding()
        {
            FlyTo(m_Building,"建筑位置");
        }

        public static void GotoSolution()
        {
            FlyTo(m_Solution, "城建项目位置");
        }

        public static void GotoProject()
        {
            FlyTo(m_Project, "工程位置");
        }
    }
}
