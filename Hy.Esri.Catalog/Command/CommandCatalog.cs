using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Esri.Catalog.UI;
using Hy.Esri.Catalog.Define;
using ESRI.ArcGIS.Carto;

namespace Hy.Esri.Catalog.Command
{
    public class CommandStandard : BaseCommand
    {
        public CommandStandard()
        {
            this.m_Category = "Catalog";
            this.m_Caption = "Catalog";
            this.m_Message = "Catalog";
        }
        private UCCatalog m_UcCatalog;
     
        public override bool Checked
        {
            get
            {
                return (m_UcCatalog != null && m_UcCatalog.Visible);
            }
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && base.m_Hook.Hook is ESRI.ArcGIS.Controls.IHookHelper;
            }
        }
              

        private class CatalogHooker : IHooker
        {
            public CatalogHooker(UCCatalog uc)
            {
                this.m_UcCatalog = uc;
            }
            public string Caption
            {
                get { return "数据源管理"; }
            }

            private Guid m_Guid = Guid.NewGuid();
            public Guid ID
            {
                get { return m_Guid; }
            }

            private UCCatalog m_UcCatalog;

            public Control Control
            {
                get
                {
                    return m_UcCatalog;
                }
            }

            public object Hook
            {
                get { return m_UcCatalog.Hook; }
            }
        }

        private ICatalogItem m_SelectedCatalogItem;
        private ILayer m_CurrentLayer;

        private Guid m_Guid = Guid.Empty;
        public override void OnClick()
        {
            if (m_UcCatalog != null && m_UcCatalog.Visible)
            { 
                this.m_Hook.UIHook.CloseHookControl(m_Guid);
            }
            else
            {
                if (m_UcCatalog == null)
                {
                    m_UcCatalog = new UCCatalog();
                    m_UcCatalog.Init(m_Hook.Hook);
                    ESRI.ArcGIS.Controls.IHookHelper esriHookHelper = m_Hook.Hook as ESRI.ArcGIS.Controls.IHookHelper;
                    IHooker hooker= new CatalogHooker(m_UcCatalog);
                    (hooker.Hook as CatalogHookHelper).SelectedCatalogItemChanged += delegate(ICatalogItem cItem)
                    {
                        m_SelectedCatalogItem = cItem;
                        m_CurrentLayer = null;

                        if (m_SelectedCatalogItem == null)
                            return;

                        m_CurrentLayer = CatalogItemFactory.CreateLayer(m_SelectedCatalogItem);

                        esriHookHelper.FocusMap.ClearLayers();
                        esriHookHelper.FocusMap.SpatialReference = null;
                        if (m_CurrentLayer != null)
                        {
                            esriHookHelper.FocusMap.AddLayer(m_CurrentLayer);
                            esriHookHelper.ActiveView.Extent = m_CurrentLayer.AreaOfInterest;
                            esriHookHelper.ActiveView.Refresh();
                        }
                    };
                    m_Guid=hooker.ID;
                    base.m_Hook.UIHook.AddHooker(hooker, enumDockPosition.Left);
                }

                this.m_Hook.UIHook.ActiveHookControl(m_Guid);
            }
        }
    }
}
