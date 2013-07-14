using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Define;
using System.Collections;
using ESRI.ArcGIS.esriSystem;

namespace Hy.Esri.Catalog
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
                    IList<WorkspaceInfo> list = Environment.NhibernateHelper.GetAll<WorkspaceInfo>();
                    foreach (WorkspaceInfo item in list)
                    {
                        ICatalogItem subItem = new WorkspaceCatalogItem(item.Args, item.Type, this, item.Name);
                        subItem.Tag = item;
                        this.m_Children.Add(subItem);
                    }
                }

                return this.m_Children;
            }
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


        public void AddItem(ICatalogItem worksapceItem)
        {
            if (m_Children == null)
            {
                m_Children = this.Childrens;
            }
            this.m_Children.Add(worksapceItem);

            SendOpenEvent(true);
        }

        public bool DeleteItem(ICatalogItem itemTarget)
        {
            if (m_Children == null)
                return false;

            if (m_Children.Contains(itemTarget))
            {
                m_Children.Remove(itemTarget);
                SendOpenEvent(true);

                return true;
            }

            return false;
        }
    }
}
