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
            bool result = CreateItemToWorkspace(this.StandardItem);
            if (result)
                SendMessage("完成.");
            else
                SendMessage("创建失败!");
        }

        private  bool CreateItemToWorkspace(StandardItem sItem)
        {
            if (fws == null || sItem == null)
                return false;

            StandardHelper.InitItemDetial(sItem);
            switch (sItem.Type)
            {
                case enumItemType.Standard:
                    foreach (StandardItem subItem in sItem.SubItems)
                    {
                        bool result = CreateItemToWorkspace(subItem);
                        if (!result)
                            return false;
                    }

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

                                return false;
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                        SendMessage(string.Format("创建FeatureDataset[{0}]失败", sItem.Name));
                        Environment.Logger.AppendMessage(Define.enumLogType.Debug, string.Format("创建FeatureDataset[{0}]失败,信息：{1}", sItem.Name, exp));
                        
                        return false;
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

                        return false;
                    }
                    break;

            }

            return true;
        }

    }
}
