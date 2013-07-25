using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hy.Common.Utility.Esri;
using Hy.Check.Define;

namespace Hy.Check.Task.DataImport
{
    public class MDBDataImport:DefaultDataImport
    {

        public override string Datasource
        {
            set
            {

                if (!(new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass()).IsWorkspace(value))
                {
                    SendMessage(enumMessageType.Exception, "对MDB数据导入器传入了非MDB路径");
                    return;
                }

                base.Datasource = value;
            }
        }

        public override enumDataType DataType
        {
            set
            {
                if (value != enumDataType.PGDB)
                {
                    SendMessage(enumMessageType.Exception, "对MDB数据导入器传入了非enumDataType.PGDB的数据类型");
                }
                else
                {
                    base.DataType = value;
                }
            }
        }
    }
}
