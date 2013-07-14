using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimenDataManage.Catalog;
using SunzSoft.Platform.DAL.Services;
using SunzSoft.Platform.Model.Entities;
using System.Collections;
using ESRI.ArcGIS.esriSystem;

namespace HzGeoSpaceSys.Main.GISForm.DataManage
{
    class RootCatalogItem : CatalogItem
    {
        public RootCatalogItem()
            : base(null, null)
        {
        }

        public override List<ICatalogItem> Childrens
        {
            get
            {
                if (this.m_Children == null)
                {
                    this.m_Children = new List<ICatalogItem>();
                    DBCore db = new DBCore(true);
                    IList list = db.GetAll(typeof(TBCATALOGCONNECTION));
                    foreach (TBCATALOGCONNECTION item in list)
                    {
                        ICatalogItem subItem = new WorkspaceCatalogItem(PropertySet(item), ThreeDimenDataManage.Utility.enumWorkspaceType.SDE, this, item.Conncname);
                        subItem.Tag = item;
                        this.m_Children.Add(subItem);
                    }
                }

                return this.m_Children;
            }
        }

        private IPropertySet PropertySet(TBCATALOGCONNECTION connParameter)
        {
            IPropertySet propertyset = new PropertySetClass();
            propertyset.SetProperty("Server", connParameter.Sdeserver);
            propertyset.SetProperty("instance", connParameter.Sdeinstance);
            propertyset.SetProperty("database",connParameter.Database);
            propertyset.SetProperty("user", connParameter.Sdeuser );
            propertyset.SetProperty("password",connParameter.Sdepassword);
            propertyset.SetProperty("version", connParameter.Sdeversion );

            return propertyset;
        }

        public override string Name
        {
            get
            {
                return "空间数据库";
            }
        }

        public override enumCatalogType Type
        {
            get
            {
                return enumCatalogType.Undefine;
            }
        }

        public override string GetGpString()
        {
            return null;
        }

        public override void Refresh()
        {
            if (m_Children != null)
            {
                int count = m_Children.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkspaceCatalogItem subItem = m_Children[i] as WorkspaceCatalogItem;
                    if (subItem.Openned)
                        subItem.Refresh();
                };
            }
            SendRefreshEvent();
        }
    }
}
