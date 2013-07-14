using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimenDataManage.Catalog;
using System.Data;
using ESRI.ArcGIS.Geodatabase;

namespace HzGeoSpaceSys.Main.GISForm.DataManage
{
    class File3DWorkspaceCatalogItem : CatalogItem, IWorkspaceCatalogItem
    {
        public File3DWorkspaceCatalogItem(ICatalogItem parent)
            : base(null, parent)
        {
        }

        public override string GetGpString()
        {
            return null;
        }

        public override List<ICatalogItem> Childrens
        {
            get { throw new NotImplementedException(); }
        }

        private Dictionary<string, int> m_DictMapping;

        public override bool HasChild
        {
            get
            {              
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "我的三维库";
            }
        }


        public object WorkspacePropertySet
        {
            get { return null; }
        }

        /// <summary>
        /// 是不是应该考虑使用叫“三正”的
        /// </summary>
        public ThreeDimenDataManage.Utility.enumWorkspaceType WorkspaceType
        {
            get { return ThreeDimenDataManage.Utility.enumWorkspaceType.Unknown; }
        }
    }
}
