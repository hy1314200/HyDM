using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Esri.Catalog.UI
{
    public partial class UCCatalog : DevExpress.XtraEditors.XtraUserControl
    {
        public UCCatalog()
        {
            InitializeComponent();
        }

        CatalogAdapter m_Adapter = new CatalogAdapter();
        public void Init(object esriHook,global::Define.MessageHandler messageHandler)
        {
            if (esriHook is ESRI.ArcGIS.Controls.IHookHelper)
                m_Adapter.Hook.Hook = (esriHook as ESRI.ArcGIS.Controls.IHookHelper).Hook;

            m_Adapter.MessageHandler = messageHandler;
            m_Adapter.AdapterTreeList(this.tlCatalog);
        }

        public object Hook
        {
            get
            {
                return m_Adapter.Hook;
            }
        }
    }
}
