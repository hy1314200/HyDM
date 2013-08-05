using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Skyline.UrbanConstruction.Bussiness
{
    public partial class FrmBatchModeling : DevExpress.XtraEditors.XtraForm
    {
        public FrmBatchModeling()
        {
            InitializeComponent();

        }

        private string[][] m_Texture =
        {
          new string[]{"钢结构","gang.jpg"}, 
          new string[]{"铁结构","tie.jpg"}, 
          new string[]{"砖结构","zhuan.jpg"}, 
          new string[]{"仝结构","tong.jpg"}, 
          new string[]{"木结构","mu.jpg"}, 
          new string[]{"混结构","hun.jpg"}, 
          new string[]{"其它结构","qita.jpg"}, 
        };



        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Height = 426;
            Application.DoEvents();

            // Showevent
            string strEvents = ConfigurationManager.AppSettings["PreBuildEvents"];
            int count = int.Parse(ConfigurationManager.AppSettings["BuildingCount"]);

            DisplayEvents(strEvents,lblStatus, count, true);
            for (int i = 0; i < count; i++)
            {
                lblStatus.Text = string.Format("正在生成建筑模型{0}/{1}...", i + 1, count);
                Application.DoEvents();
                m_Index++;
                this.progressBarControl1.Position = 100 * m_Index / m_ProgressCount;
                Application.DoEvents();

                DisplayEvents(ConfigurationManager.AppSettings["BuildingEvnets"], lblSubStatus, 0, false);
            }

            this.progressBarControl1.Position = 100;
            lblStatus.Text = "构建完成。";
            lblSubStatus.Text = "";
            Application.DoEvents();

            MessageBox.Show("构建完成。");

            string lyrName = ConfigurationManager.AppSettings["LayerPath"];
            int id= Program.IInfoTree.FindItem(lyrName);
            Program.IInfoTree.SetVisibility(id, 1);             
            Program.pNavigate6.FlyTo(Program.sgworld.ProjectTree.GetLayer(id).Position);
        }

        private int m_Index = 0;
        private int m_ProgressCount = 0;
        private void DisplayEvents(string strEvents,Control lbl, int count,bool progress)
        {
            char[] outterSplit = { ';' }, innerSplit = { '|' };


            string[] allEvents = strEvents.Split(outterSplit, StringSplitOptions.RemoveEmptyEntries);
            
            if (progress)
            {
                m_ProgressCount = (count + allEvents.Length);
            }

            foreach (string strEvent in allEvents)
            {
                string[] e = strEvent.Split(innerSplit, StringSplitOptions.RemoveEmptyEntries);
                if (e != null && e.Length == 2)
                {
                    lbl.Text = e[0];
                    Application.DoEvents();

                    System.Threading.Thread.Sleep(int.Parse(e[1]));
                    if (progress)
                    {
                        m_Index++;
                        this.progressBarControl1.Position = 100 * m_Index / m_ProgressCount;
                        Application.DoEvents();
                    }
                }
            }
        }

        private void FrmBatchModeling_Load(object sender, EventArgs e)
        {
            foreach (string[] strMap in m_Texture)
            {
                this.dataGridView1.Rows.Add(strMap);
            }

            this.dataGridView1.Refresh();
        }

    }
}