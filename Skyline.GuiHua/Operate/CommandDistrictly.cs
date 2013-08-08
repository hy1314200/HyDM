using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;
using Define;
using DevExpress.XtraBars;
using TerraExplorerX;
using System.Configuration;

namespace Skyline.GuiHua.Operate
{
    public class CommandDistrictly: GuiHuaBaseCommand, ICommandEx
    {
        public CommandDistrictly()
        {
            this.m_Caption = "分区显示";

            this.m_Message = "分区显示";
            this.m_Tooltip = "分区显示";

            DisplayOnArea.Caption = "选择分区:";
            DisplayOnArea.EditValueChanged += new System.EventHandler(this.SelectedDistrictChanged);

        }

        BarEditItem DisplayOnArea = new BarEditItem();

        public object ExControl
        {
            get { return DisplayOnArea; }
        }

        public override bool Enabled
        {
            get
            {
                return (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
            }
        }
        public override void OnClick()
        {
            
        }

        private void SelectedDistrictChanged(object sender, EventArgs e)
        {
            try
            {
                if (DisplayOnArea.EditValue.ToString() != "")
                    DisplayOnDistrict(DisplayOnArea.EditValue.ToString());
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }

        }
        private void DisplayOnDistrict(string DistrictName)
        {
            
            if (m_SkylineHook.SGWorld.Project.Name != "")
            {
                try
                {
                    int LayerID = m_SkylineHook.SGWorld.ProjectTree.FindItem("分区");
                    if (LayerID >= 0)
                    {
                        int ModelID = m_SkylineHook.SGWorld.ProjectTree.FindItem(ConfigurationManager.AppSettings["LayerPath"]);
                        if (ModelID >= 0)
                        {
                            //根据DistricName从分区shp表中读取相应Code，再将SGModel中相应Code的模型显示出来
                            ILayer61 DistrictLayer = m_SkylineHook.SGWorld.ProjectTree.GetLayer(LayerID);
                            IFeatureGroup61 Polygons = DistrictLayer.FeatureGroups.Polygon;
                            string Code = "all";
                            foreach (IFeature61 a in Polygons)
                            {
                                if (a.FeatureAttributes.GetFeatureAttribute("Name").Value == DistrictName)
                                    Code = a.FeatureAttributes.GetFeatureAttribute("Code").Value;
                            }
                            ILayer61 ModelLayer = m_SkylineHook.SGWorld.ProjectTree.GetLayer(ModelID);
                            IFeatureGroup61 Models = ModelLayer.FeatureGroups.Point;
                            //MessageBox.Show(Models.GetProperty("File Name").ToString());
                            foreach (IFeature61 m in Models)
                            {
                                if (Code == "all")
                                    m.FeatureAttributes.GetFeatureAttribute("Display").Value = m.FeatureAttributes.GetFeatureAttribute("ModelPath").Value;
                                else
                                {
                                    if (m.FeatureAttributes.GetFeatureAttribute("Code").Value == Code)
                                        m.FeatureAttributes.GetFeatureAttribute("Display").Value = m.FeatureAttributes.GetFeatureAttribute("ModelPath").Value;
                                    else
                                        m.FeatureAttributes.GetFeatureAttribute("Display").Value = "";

                                }
                            }
                            ModelLayer.Save();
                            ModelLayer.Refresh();
                            //由于Display字段变化，二次加载时可能无法全部显示
                        }

                    }
                }
                catch
                {
                    MessageBox.Show("对不起，分区显示过程出现了错误!这通常是由于配置不当引起的。");
                }
            }
        }
    }
}
