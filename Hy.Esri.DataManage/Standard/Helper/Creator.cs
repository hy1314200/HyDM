using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.DataManage.Standard.Helper
{
    internal class Creator:global::Define.Messager
    {
        public IWorkspace fws { private get; set; }

        public StandardItem StandardItem { private get; set; }

        public void CreateToWorkspace()
        {
            SendMessage("准备创建...");
            CreateItemToWorkspace(this.StandardItem);
        }

        private  void CreateItemToWorkspace(StandardItem sItem)
        {
            if (fws == null || sItem == null)
                return;

            StandardHelper.InitItemDetial(sItem);
            switch (sItem.Type)
            {
                case enumItemType.Standard:
                    foreach (StandardItem subItem in sItem.SubItems)
                        CreateItemToWorkspace(subItem);

                    break;

                case enumItemType.FeatureDataset:
                    SendMessage(string.Format("正在创建FeatureDataset[{0}]...", sItem.Name));
                    try
                    {
                        IFeatureDataset fds = (fws as IFeatureWorkspace).CreateFeatureDataset(sItem.Name, sItem.SpatialReference);

                        foreach (StandardItem subItem in sItem.SubItems)
                        {
                            SendMessage(string.Format("正在创建矢量图层[{0}]...", sItem.Name));
                            try
                            {
                                StandardHelper.InitItemDetial(subItem);
                                StandardHelper.CreateFeatureClass(fds, subItem.Details as FeatureClassInfo);
                            }
                            catch (Exception exp)
                            {
                                SendMessage(string.Format("创建矢量图层[{0}]失败", sItem.Name));

                                Environment.Logger.AppendMessage(Define.enumLogType.Debug, string.Format("创建矢量图层[{0}]失败,信息：{1}", sItem.Name, exp));
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                        SendMessage(string.Format("创建FeatureDataset[{0}]失败", sItem.Name));

                        Environment.Logger.AppendMessage(Define.enumLogType.Debug, string.Format("创建FeatureDataset[{0}]失败,信息：{1}", sItem.Name, exp));
                    }
                    break;

                case enumItemType.FeatureClass:
                    SendMessage(string.Format("正在创建矢量图层[{0}]...", sItem.Name));
                    try
                    {
                        if (sItem.Parent != null && sItem.Parent.Type == enumItemType.FeatureDataset)
                        {
                            IFeatureDataset fdsParent = (fws as IFeatureWorkspace).OpenFeatureDataset(sItem.Parent.Name);
                            StandardHelper.CreateFeatureClass(fdsParent, sItem.Details as FeatureClassInfo);
                        }
                        else
                        {
                            StandardHelper.CreateFeatureClass(fws, sItem.Details as FeatureClassInfo);
                        }
                    }
                    catch (Exception exp)
                    {
                        SendMessage(string.Format("创建矢量图层[{0}]失败", sItem.Name));

                        Environment.Logger.AppendMessage(Define.enumLogType.Debug, string.Format("创建矢量图层[{0}]失败,信息：{1}", sItem.Name, exp));
                    }
                    break;

            }
        }

    }
}
