using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Esri.DataManage.UI;
using Hy.Esri.DataManage.Standard;
using Hy.Metadata;

namespace Hy.Esri.DataManage.Command
{
    public  class CommandStandardAddFeatureClass : DMStandardBaseCommand
    {
        public CommandStandardAddFeatureClass()
        {
            this.m_Caption = "新建矢量图层";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && m_Manager.SelectedItem != null && (m_Manager.SelectedItem.Type == enumItemType.Standard || m_Manager.SelectedItem.Type==enumItemType.FeatureDataset);
            }
        }

        FrmFeatureClassInfo m_FrmAdd;
        public override void OnClick()
        {
            if (m_FrmAdd == null || m_FrmAdd.IsDisposed)
            {
                m_FrmAdd = new FrmFeatureClassInfo();
                m_FrmAdd.EditAble = true;
                m_FrmAdd.Text = "新建矢量图层";
            }
            FeatureClassInfo fcInfo = new FeatureClassInfo();
            fcInfo.Name = "NewFeatureLayer";
            fcInfo.ShapeFieldName = "Shape";
            m_FrmAdd.FeatureClassInfo = fcInfo;
            if (m_FrmAdd.ShowDialog() == DialogResult.OK)
            {
                fcInfo = m_FrmAdd.FeatureClassInfo;

                StandardItem sItem = new StandardItem();
                sItem.Name = fcInfo.Name;
                sItem.AliasName = fcInfo.AliasName;
                sItem.SpatialReferenceString = fcInfo.SpatialReferenceString;
                sItem.Type = enumItemType.FeatureClass;
                sItem.Parent = m_Manager.SelectedItem;
                sItem.Details = fcInfo;
                Environment.NhibernateHelper.SaveObject(sItem);

                fcInfo.Parent = sItem.ID;
                Environment.NhibernateHelper.SaveObject(fcInfo);

                foreach (FieldInfo fInfo in fcInfo.FieldsInfo)
                {
                    fInfo.Layer = fcInfo.ID;
                    Environment.NhibernateHelper.SaveObject(fInfo);
                }



                Environment.NhibernateHelper.Flush();

                Environment.NhibernateHelper.RefreshObject(m_Manager.SelectedItem, enumLockMode.None);
                this.m_Manager.Refresh();
            }
        }
    }
}
