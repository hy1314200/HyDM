using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using System.Data;
using System.Diagnostics;
using ESRI.ArcGIS.Geometry;
using Hy.Esri.Catalog.Define;
namespace Hy.Esri.Catalog.Utility
{
    internal class File3DHelper
    {
        public const string Table_Name_Registerer = "File3DRegister";
        public const string Table_Name_Attribute = "File3DAttribute";
        public const string Field_Name_ObjectID = "ObjectID";
        public const string Field_Name_DataID = "DataID";
        public const string Field_Name_DataLink = "DataLink";
        public const string Field_Name_DataName = "DataName";

        //public static string Field_Name_3DRender_ModelName = "ModelName";
        //public static string Field_Name_3DRender_ModelPath = "ModelPath";


        public static string ErrorMessage { get; private set; }

        public static IWorkspace m_Workspace { get; set; }// = GISOpr.getInstance().WorkSpace;

//        public static void CreateRegisterTable()
//        {
//            if (!(m_Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, Table_Name_Registerer))
//            {
//                string strSQL =
//                    @"CREATE TABLE File3DRegister(  
//                        DATAID VARCHAR2(256) NOT NULL ENABLE, 
//                        OBJECTID INTEGER NOT NULL ENABLE, 
//                        DATALINK VARCHAR2(256) NOT NULL ENABLE,
//                        DATANAME VARCHAR2(256), 
//
//                        CONSTRAINT KEY_OBJECTID PRIMARY KEY (DATAID)
//                    )
//                ";
//                //COMMENT ON COLUMN File3DRegister.DATAID IS '3D文件的标识，文件名或其它特征值'; 
//                //COMMENT ON COLUMN File3DRegister.OBJECTID IS '记录标识'; 
//                //COMMENT ON COLUMN File3DRegister.DATANAME IS '数据名称'; 
//                //COMMENT ON COLUMN File3DRegister.DATALINK IS '数据链接';
//                m_Workspace.ExecuteSQL(strSQL);

//                string strSQLAtrribute =
//                    @"create table File3DAttribute(
//                      OBJECTID          INTEGER not null,
//                      YSDM              NVARCHAR2(10),
//                      YSLX              NVARCHAR2(30),
//                      MC                NVARCHAR2(50),
//                      LXR               NVARCHAR2(50),
//                      GXSJ              NVARCHAR2(20),
//                      LXDH              NVARCHAR2(20),
//                      XXDZ              NVARCHAR2(80),
//                      JLXH              NVARCHAR2(50),
//                      LDH               NVARCHAR2(50),
//                      LFH               NVARCHAR2(50),
//                      MPH               NVARCHAR2(50),
//                      GM                NVARCHAR2(50),
//                      JYFW              NVARCHAR2(50),
//                      QYXZ              NVARCHAR2(50),
//                      ZCZJ              NVARCHAR2(50),
//                      FR                NVARCHAR2(50),
//                      ZCSJ              NVARCHAR2(20),
//                      ZCDD              NVARCHAR2(50),
//                      FZRDH             NVARCHAR2(50),
//                      FZRDZ             NVARCHAR2(80),
//                      SJZGDW            NVARCHAR2(50),
//                      XXFL              NVARCHAR2(50),
//                      DM                NVARCHAR2(50),
//                      BSM               NVARCHAR2(50),
//                      SSXQBM            NVARCHAR2(20),
//                      XZJDBM            NVARCHAR2(50),
//                      PCSBM             NVARCHAR2(20),
//                      JCWHBM            NVARCHAR2(20),
//                      JLXBM             NVARCHAR2(10),
//                      SFZHM             NVARCHAR2(30),
//                      ZADWBM            NVARCHAR2(7),
//                      XFDWBM            NVARCHAR2(7),
//                      CJR               NVARCHAR2(50),
//                      DETAILHREF        NVARCHAR2(100),
//                      YJFL              NVARCHAR2(50),
//                      SJFL              NVARCHAR2(50),
//                      EJFL              NVARCHAR2(50),
//                      FID_JJZQ_DJXZQ_PY NUMBER(10),
//                      YSLX_1            NVARCHAR2(30),
//                      MC_1              NVARCHAR2(60),
//                      DM_1              NVARCHAR2(15),
//                      LNG_1             NUMBER(38,8),
//                      LAT_1             NUMBER(38,8),
//                      CX                NUMBER(38,8),
//                      CY                NUMBER(38,8),
//                      DISTANCETOCENTER  NUMBER(5),
//                      WEB1              NVARCHAR2(50),
//                      WEB               NVARCHAR2(50),
//                      D3SIGN            NVARCHAR2(2),
//                      D3SIGN1           NVARCHAR2(2),
//                      FULLSCENCESIGN    NVARCHAR2(2),
//                      FULLSCENCESIGN1   NVARCHAR2(2)
//                    )"
//                    ;
//                /*
//                @"
//                    create table File3DAttribute
//                    (
//                        objectid char(32) not null,
//                        dataname varchar2(256)
//                    );

//                    comment on column File3DAttribute.objectid is '记录ID';
//                    create unique index Index_ObjectID on File3DAttribute (objectid);

//                    alter table File3DAttribute  add constraint F_Key_Object foreign key (OBJECTID)  references File3DRegister (DATAID) on delete cascade;
//                ";
//                 * */

//                m_Workspace.ExecuteSQL(strSQLAtrribute);
//            }
//        }

//        //public static string GetObjectID(string dataID)
//        //{
//        //    if (string.IsNullOrWhiteSpace(dataID))
//        //        return null;

//        //    object[] objParamters =
//        //    {
//        //        Field_Name_ObjectID,
//        //        Table_Name_Registerer,
//        //        Field_Name_DataID,
//        //        dataID.Replace("'","''")
//        //    };
//        //    string strSQL = string.Format("select {0} from {1} where {2}='{2}'", objParamters);

//        //    return GlobalInit.DataHelper().ExecSQLScalar(strSQL) as string;
//        //}

//        public static ITable GetAttributeTable()
//        {
//            return (m_Workspace as IFeatureWorkspace).OpenTable(Table_Name_Attribute);
//        }

//        //public static void Synchronization(List<string> exactlyDatasets)
//        //{
//        //    IWorkspace wsMain = GISOpr.getInstance().WorkSpace;
//        //    if ((wsMain as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, Table_Name_Registerer))
//        //    {
//        //        CreateRegisterTable();
//        //    }

//        //    ITable tRecords = GetAttributeTable();
//        //    tRecords.DeleteSearchedRows(
//        //}


//        //public static List<IField> GetDisplayAbleFields()
//        //{  
//        //    // DataTable dtTest= SunzSoft.Platform.Global.GlobalInit.DataHelper().ExecSQLReader("select  * from File3DRegister where 1=2").GetSchemaTable();

//        //    IFeatureWorkspace fwsMain = GISOpr.getInstance().WorkSpace as IFeatureWorkspace;
//        //    ITable tSchema = fwsMain.OpenTable(Table_Name_Registerer);
//        //    IFields fieldsAll = tSchema.Fields;
//        //    int count = fieldsAll.FieldCount;
//        //    List<IField> fieldList = new List<IField>();
//        //    for (int i = 0; i < count; i++)
//        //    {
//        //        IField curField = fieldsAll.get_Field(i);
//        //        if (curField.Name == Field_Name_DataID
//        //            || curField.Name == Field_Name_ObjectID
//        //            || curField.Name == Field_Name_DataLink)
//        //            continue;

//        //        fieldList.Add(curField);
//        //    }

//        //    return fieldList;
//        //}

//        public static string GetDataID(string fClassName, int featureOID)
//        {
//            return string.Format("{0}-{1}", fClassName, featureOID);
//        }

//        public static int GetDataObjectID(string dataID)
//        {
//            if (string.IsNullOrEmpty(dataID))
//                return -1;

//            ITable tRegister = (m_Workspace as IFeatureWorkspace).OpenTable(Table_Name_Registerer);
//            IQueryFilter qFilter = new QueryFilterClass();
//            qFilter.SubFields = Field_Name_ObjectID;
//            qFilter.WhereClause = string.Format("DataID='{0}", dataID.Replace("'", "''"));
//            ICursor cursor = tRegister.Search(qFilter, false);
//            IRow row = cursor.NextRow();
//            if (row != null)
//            {
//                int fIndex = row.Fields.FindField(Field_Name_ObjectID);
//                if (fIndex > -1)
//                    return Convert.ToInt32(row.get_Value(fIndex));
//            }

//            return 1;
//            //return Convert.ToInt32(GlobalInit.DataHelper().ExecSQLScalar(string.Format("select ObjectID from File3DRegister where DataID='{0}'", dataID.Replace("'", "''"))));
//        }

//        public static string GetDataName(string dataID)
//        {
//            if (string.IsNullOrEmpty(dataID))
//                return null;
             
//            ITable tRegister = (m_Workspace as IFeatureWorkspace).OpenTable(Table_Name_Registerer);
//            IQueryFilter qFilter = new QueryFilterClass();
//            qFilter.SubFields = Field_Name_DataName;
//            qFilter.WhereClause = string.Format("DataID='{0}'", dataID.Replace("'", "''"));
//            ICursor cursor = tRegister.Search(qFilter, false);
//            IRow row = cursor.NextRow();
//            if (row != null)
//            {
//                int fIndex = row.Fields.FindField(Field_Name_DataName);
//                if (fIndex > -1)
//                    return row.get_Value(fIndex) as string;
//            }

//            return null;
//            //return GlobalInit.DataHelper().ExecSQLScalar(string.Format("select DataLink from File3DRegister where DataID='{0}'", dataID.Replace("'", "''"))) as string;
//        }
//        public static string GetDataPath(string dataID)
//        {
//            if (string.IsNullOrEmpty(dataID))
//                return null;


//            ITable tRegister = (m_Workspace as IFeatureWorkspace).OpenTable(Table_Name_Registerer);
//            IQueryFilter qFilter = new QueryFilterClass();
//            qFilter.SubFields = Field_Name_DataLink;
//            qFilter.WhereClause = string.Format("DataID='{0}'", dataID.Replace("'", "''"));
//            ICursor cursor = tRegister.Search(qFilter, false);
//            IRow row = cursor.NextRow();
//            if (row != null)
//            {
//                int fIndex = row.Fields.FindField(Field_Name_DataLink);
//                if (fIndex > -1)
//                    return row.get_Value(fIndex) as string;
//            }

//            return null;
//            //return GlobalInit.DataHelper().ExecSQLScalar(string.Format("select DataLink from File3DRegister where DataID='{0}'", dataID.Replace("'", "''"))) as string;
//        }

        public static bool GetDataInfo(string dataID, ref int ObjectID, ref string DataPath, ref string DataName)
        {
            if (string.IsNullOrEmpty(dataID))
                return false;

            ITable tRegister = (m_Workspace as IFeatureWorkspace).OpenTable(Table_Name_Registerer);
            IQueryFilter qFilter = new QueryFilterClass();
            qFilter.WhereClause = string.Format("DataID='{0}'", dataID.Replace("'", "''"));
            ICursor cursor = tRegister.Search(qFilter, false);
            IRow row = cursor.NextRow();
            if (row == null)
                return false;


            int fIndex = row.Fields.FindField(Field_Name_ObjectID);
            if (fIndex > -1)
                ObjectID = Convert.ToInt32(row.get_Value(fIndex));

            fIndex = row.Fields.FindField(Field_Name_DataLink);
            if (fIndex > -1)
                DataPath = row.get_Value(fIndex) as string;

            fIndex = row.Fields.FindField(Field_Name_DataName);
            if (fIndex > -1)
                DataName = row.get_Value(fIndex) as string;


            return true;
        }

        //public static bool Publish(double x, double y, double z, string modelFile, string shpFile)
        //{
        //    try
        //    {
        //        string strModelName = System.IO.Path.GetFileNameWithoutExtension(modelFile);
        //        string strModelPath = System.IO.Path.GetDirectoryName(shpFile);

        //        string xplPath = System.IO.Path.Combine(strModelPath, strModelName + ".Xpl2");
        //        if (!System.IO.File.Exists(xplPath)) // 有可能已经发布过了，目标xpl已经存在
        //        {
        //            ProcessStartInfo processInfo = new ProcessStartInfo();
        //            //processInfo.FileName="%SkylineHome%\\MakeXpl.exe";
        //            processInfo.FileName = System.IO.Path.Combine(Environment.GetEnvironmentVariable("SkylineHome",EnvironmentVariableTarget.Machine), "MakeXpl.exe");
        //            processInfo.Arguments = string.Format("-inputFile {0} -outputDir {1} -Skipbadtextures", modelFile, strModelPath);
        //            Process process = new Process();
        //            process.StartInfo = processInfo;
        //            process.Start();
        //            process.WaitForExit();
        //        }


        //        IWorkspace wsShp = Hy.Esri.Catalog.Utility.WorkspaceHelper.OpenWorkspace(Hy.Esri.Catalog.enumWorkspaceType.File, System.IO.Path.GetDirectoryName(shpFile));
        //        IFeatureClass fClass = (wsShp as IFeatureWorkspace).OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(shpFile));
        //        IFeature fModel = fClass.CreateFeature();

        //        IPoint pModel = fModel.Shape as IPoint;
        //        pModel.X = x;
        //        pModel.Y = y;
        //        pModel.Z = z;
        //        pModel.M = 0;
                

        //        int modelNameIndex = fModel.Fields.FindField(Field_Name_3DRender_ModelName);
        //        int modelPathIndex = fModel.Fields.FindField(Field_Name_3DRender_ModelPath);
        //        if (modelNameIndex > -1)
        //            fModel.set_Value(modelNameIndex, strModelName + ".Xpl2");
        //        if (modelPathIndex > -1)
        //            fModel.set_Value(modelPathIndex, xplPath);

        //        fModel.Store();

        //        // 生成相应的属性表
        //        string strSQL = string.Format("Insert into File3DAttribute(objectID) values({0})", fModel.OID);
        //        m_Workspace.ExecuteSQL(strSQL);

        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(fModel);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(fClass);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(wsShp);

        //        return true;
        //        //return GlobalInit.DataHelper().ExecSQL(strSQL) > 0;
        //        //return true;
        //    }
        //    catch (Exception exp)
        //    {
        //        ErrorMessage = exp.Message;
        //        return false;
        //    }
        //}

        //public static int GetNextObjectID()
        //{

        //    ITable tRegister = (m_Workspace as IFeatureWorkspace).OpenTable(Table_Name_Registerer);
        //    IQueryFilter qFilter = new QueryFilterClass();
        //    qFilter.SubFields = Field_Name_ObjectID;
        //    qFilter.WhereClause = "ObjectID=(Select Max(ObjectID) from File3DRegister)";
        //    ICursor cursor = tRegister.Search(qFilter, false);
        //    IRow row = cursor.NextRow();
        //    if (row != null)
        //        return Convert.ToInt32(row.get_Value(0)) + 1;

        //    return 1;
        //    //return Convert.ToInt32(GlobalInit.DataHelper().ExecSQLScalar("select nvl(max(ObjectID),0)+1  from File3DRegister"));
        //}

        //public static bool SaveAndRegister3DFile(string str3DFile, string featureClass, int featureID)
        //{
        //    CreateRegisterTable();

        //    string str3DName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(str3DFile);
        //    string str3DPath=Environment.GetEnvironmentVariable("ModelShareFolder",System.EnvironmentVariableTarget.Machine);
        //    //string str3DPath = Properties.Settings.Default.ModelShareFolder;
        //    try
        //    {
        //        string strTarget = System.IO.Path.Combine(str3DPath, str3DName);
        //        System.IO.File.Copy(str3DFile, strTarget);

        //        // Register
        //        int newOID = GetNextObjectID();
        //        string strSQL = string.Format("Insert into File3DRegister(DataID,ObjectID,DataLink)  values('{0}',{1},'{2}')", GetDataID(featureClass, featureID).Replace("'", "''"), newOID, strTarget);
        //        //if (GlobalInit.DataHelper().ExecSQL(strSQL) < 1)
        //        //    return false;
        //        m_Workspace.ExecuteSQL(strSQL);

        //        //strSQL = string.Format("Insert into File3DAttribute(objectID) values({0})", newOID);
        //        ////return GlobalInit.DataHelper().ExecSQL(strSQL) > 0;
        //        //m_Workspace.ExecuteSQL(strSQL);

        //        return true;
        //    }
        //    catch (Exception exp)
        //    {
        //        ErrorMessage = exp.Message;
        //        return false;
        //    }
        //}

        //public static bool Import3DFileEx(string str3DFile, IWorkspace wsTarget, string featrueDatasetName, string featureClassName, string strSpatialRef, ref IFeature feature3D)
        //{
        //    if (!Hy.Esri.Catalog.Utility.GpTool.Import3DFile(str3DFile, wsTarget, featrueDatasetName, featureClassName, strSpatialRef, ref feature3D))
        //    {
        //        ErrorMessage = Hy.Esri.Catalog.Utility.GpTool.ErrorMessage;
        //        return false;
        //    }

        //    //string strClassName = System.IO.Path.GetFileNameWithoutExtension(outPut);
        //    //int featureID = 1;  // 除shp外，新建的FeatureClass第一条记录OID为1

        //    string strClassName = (feature3D.Class as IDataset).Name;   // 原因是：当存入SDE后，Dataset的名字将在前面加上“数据库名.用户名.”
        //    return SaveAndRegister3DFile(str3DFile, strClassName, feature3D.OID);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="str3DFile"></param>
        ///// <param name="wsOut">指导入到的Workspace</param>
        ///// <param name="outPut">指导入到的目标全路径（GPstring）</param>
        ///// <param name="strSpatialRef"></param>
        ///// <returns></returns>
        //public static bool Append3DFileEx(string str3DFile, IFeatureClass fClassTarget, string strSpatialRef, ref IFeature feature3D)
        //{
        //    if (!Hy.Esri.Catalog.Utility.GpTool.Append3DFile(str3DFile, fClassTarget, strSpatialRef, ref feature3D))
        //    {
        //        ErrorMessage = Hy.Esri.Catalog.Utility.GpTool.ErrorMessage;
        //        return false;
        //    }

        //    return SaveAndRegister3DFile(str3DFile, (fClassTarget as IDataset).Name, feature3D.OID);
        //}

        public static int GetCatalogImageIndex(ICatalogItem item)
        {

            //SDE
            //Local
            //FDs
            //FC3D
            //FCPoint
            //FCLine
            //FCArea
            //FCAnno
            //FCEmpty
            //RasterCatalog
            //RtasterSet
            //NestRaster
            //table
            //Terrain
            //Tin
            //Topo

            if (item == null)
                return 8;

            switch (item.Type)
            {
                case enumCatalogType.Workpace:
                    return 0;

                case enumCatalogType.Undefine:
                    return -1;

                case enumCatalogType.Topology:
                    return 16;

                case enumCatalogType.Tin:
                    return 15;

                case enumCatalogType.Terrain:
                    return 14;

                case enumCatalogType.Table:
                    return 13;

                case enumCatalogType.RasterMosaic:
                    return 12;

                case enumCatalogType.RasterBand:
                    return 11;
                case enumCatalogType.RasterSet:
                    return 10;
                case enumCatalogType.RasterCatalog:
                    return 9;

                case enumCatalogType.FeatureDataset:
                    return 2;
                case enumCatalogType.FeatureClass3D:
                    return 3;

                case enumCatalogType.FeatureClassPoint:
                    return 4;
                case enumCatalogType.FeatureClassLine:
                    return 5;
                case enumCatalogType.FeatureClassArea:
                    return 6;
                case enumCatalogType.FeatureClassAnnotation:
                    return 7;
                case enumCatalogType.FeatureClassEmpty:
                    return 8;
                case enumCatalogType.ThreeDimenModel:
                    return 19;
            }
            return -1;
        }
    }


}
