using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using TerraExplorerX;

namespace ThreeDimenDataManage.Command.TE
{
    public class CommandAddShpLayer:TECommand
    {
        OpenFileDialog m_DialogOpenShp = new OpenFileDialog();
        public CommandAddShpLayer()
        {
            m_DialogOpenShp.Filter = "Shp文件 | *.Shp";
            this.m_name = "加载Shp文件";
        }
        public override void OnClick()
        {
            if (m_DialogOpenShp.ShowDialog() == DialogResult.OK)
            {
                string strFile=m_DialogOpenShp.FileName;
                string strName=System.IO.Path.GetFileNameWithoutExtension(strFile);
                ILayer61 lyrNew= m_TEHelper.SGWorld.Creator.CreateFeatureLayer(strName, string.Format("FileName={0};TEPlugName=OGR;", strFile));
                m_TEHelper.SGWorld.Navigate.FlyTo(lyrNew);
            }
        }

        public override bool Enabled
        {
            get
            {
                return !string.IsNullOrEmpty(m_TEHelper.SGWorld.Project.Name);
            }
        }
    }
}
