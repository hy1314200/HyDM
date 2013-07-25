using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hy.Common.Utility.Esri;
using Hy.Check.Define;

namespace Hy.Check.Task.DataImport
{
   public class SHPDataImport:DefaultDataImport
    {

       public override string Datasource
       {
           set
           {               

               if (!(new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass()).IsWorkspace(value))
               {
                   SendMessage(enumMessageType.Exception, "对Shp数据导入器传入了非SHP Workspace路径");
                   return;
               }
               base.Datasource = value;
           }
       }

       public override enumDataType DataType
       {
           set
           {
               if (value != enumDataType.SHP)
               {
                   SendMessage(enumMessageType.Exception, "对Shp数据导入器传入了非enumDataType.SHP的数据类型");
               }
               else
               {
                   base.DataType = value;
               }
           }
       }
    }
}
