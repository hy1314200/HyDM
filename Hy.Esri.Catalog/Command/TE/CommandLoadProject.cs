using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThreeDimenDataManage.Command.TE
{
    
    public class CommandLoadProject:TECommand
    {
        private OpenFileDialog m_DialogLoadProject = new OpenFileDialog();
        public override void OnClick()
        {
            this.m_DialogLoadProject.Title = "加载";
            this.m_DialogLoadProject.Filter = "三维工程文件(*.fly)|*.fly";

            if (this.m_DialogLoadProject.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.m_TEHelper.TerrainExplorer.Load(this.m_DialogLoadProject.FileName);
                }
                catch (Exception)
                {
                    // throw;
                }

            }
        }
    }
}
