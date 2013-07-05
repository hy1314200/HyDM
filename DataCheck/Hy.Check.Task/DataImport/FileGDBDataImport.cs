using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Common.Utility.Esri;
using Hy.Check.Define;

namespace Hy.Check.Task.DataImport
{
    public class FileGDBDataImport:DefaultDataImport
    {
        public override string Datasource
        {
            set
            {

                if (!(new ESRI.ArcGIS.DataSourcesGDB.FileGDBWorkspaceFactoryClass()).IsWorkspace(value))
                {
                    SendMessage(enumMessageType.Exception, "对FileGDB数据导入器传入了非FileGDB Workspace路径");
                    return;
                }

                base.Datasource = value;
            }
        }

        public override enumDataType DataType
        {
            set
            {
                if (value != enumDataType.FileGDB)
                {
                    SendMessage(enumMessageType.Exception, "对FileGDB数据导入器传入了非enumDataType.FileGDB的数据类型");
                }
                else
                {
                    base.DataType = value;
                }
            }
        }
    }
}
