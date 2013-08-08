using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Skyline.GuiHua.Bussiness
{
    public class ProjectHelper
    {
        string strSQL = @"create table GH_Project(
                                                    ProjectID varchar2(32) default sys_GUID() not null,
                                                    ProjectName varchar(50),
                                                    ProjectType varchar(50),
                                                    Enterprise varchar(255),
                                                    ProjectAdrress varchar(50),
                                                    ProjectFolder varchar(255),
                                                    ProjectFile varchar(255)
                                                    )
";
        string strSQL2 = @"create table GH_Schema(
                                                    SchemaID varchar2(32) default sys_GUID() not null,
                                                    SchemaName varchar(50),
                                                    SchemaType varchar(50),
                                                    BuildingArea float,
                                                    VegetationArea float,
                                                    RoadArea float,
                                                    ProjectID varchar2(32) not null,
                                                    SchemaFolder varchar(255),
                                                    SchemaFile varchar(255)
                                                    )
";

        public bool TestSysTable(ref List<ProjectInfo> projectInfos)
        {
            if (!Environment.AdodbHelper.TableExists("GH_Project"))
            {
                Environment.AdodbHelper.ExecuteSQL(strSQL);
                Environment.AdodbHelper.ExecuteSQL(strSQL2);

                return false;
            }

            try
            {
                DataTable  tProject = Environment.AdodbHelper.OpenTable("GH_Project");
                DataTable tSchema =  Environment.AdodbHelper.OpenTable("GH_Schema");

               

                int pID_Index = tProject.Columns.IndexOf("ProjectID");
                int pName_Index = tProject.Columns.IndexOf("ProjectName");
                int pType_Index = tProject.Columns.IndexOf("ProjectType");
                int pEnterprise_Index = tProject.Columns.IndexOf("Enterprise");
                int pAddress_Index = tProject.Columns.IndexOf("ProjectAdrress");
                int pFolder_Index = tProject.Columns.IndexOf("ProjectFolder");
                int pFile_Index = tProject.Columns.IndexOf("ProjectFile");


                int sID_Index = tSchema.Columns.IndexOf("SchemaID");
                int sName_Index = tSchema.Columns.IndexOf("SchemaName");
                int sType_Index = tSchema.Columns.IndexOf("SchemaType");
                int sBuilding_Index = tSchema.Columns.IndexOf("BuildingArea");
                int sVegetation_Index = tSchema.Columns.IndexOf("VegetationArea");
                int sRoad_Index = tSchema.Columns.IndexOf("RoadArea");
                int sFolder_Index = tSchema.Columns.IndexOf("SchemaFolder");
                int sFile_Index = tSchema.Columns.IndexOf("SchemaFile");

                projectInfos = new List<ProjectInfo>();

                foreach(DataRow rowProject in tProject.Rows)
                {
                    ProjectInfo pInfo = new ProjectInfo();
                    pInfo.ID = rowProject[pID_Index] as string;
                    pInfo.Name = rowProject[pName_Index] as string;
                    pInfo.Type = rowProject[pType_Index]as string;
                    pInfo.Enterprise = rowProject[pEnterprise_Index] as string;
                    pInfo.Address = rowProject[pAddress_Index] as string;
                    pInfo.Folder = rowProject[pFolder_Index] as string;
                    pInfo.File = rowProject[pFile_Index] as string;

                    List<SchemaInfo> sInfoList = new List<SchemaInfo>();
                    DataRow[] rowsSchema = tSchema.Select(string.Format("ProjectID='{0}'", pInfo.ID));
                   foreach(DataRow rowSchema in rowsSchema)
                    {
                        SchemaInfo sInfo = new SchemaInfo();
                        sInfo.ID = rowSchema[sID_Index] as string;
                        sInfo.Name = rowSchema[sName_Index] as string;
                        sInfo.Type = rowSchema[sType_Index] as string;
                        sInfo.VegetationArea = Convert.ToDouble(rowSchema[sVegetation_Index]);
                        sInfo.BuildingArea = Convert.ToDouble(rowSchema[sBuilding_Index]);
                        sInfo.RoadArea = Convert.ToDouble(rowSchema[sRoad_Index]);
                        sInfo.Folder = rowSchema[sFolder_Index] as string;
                        sInfo.File = rowSchema[sFile_Index] as string;

                        sInfo.Project = pInfo;
                        sInfoList.Add(sInfo);
                       
                    }
                    pInfo.Schemas = sInfoList;

                    projectInfos.Add(pInfo);
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        private const string ProjectExcel = "Project.xls";
        private const string FlyFile = "Project.fly";
        private const string ImageFile = "Image.Tif";
        private const string DemFile = "Dem.Tif";

        private const string SchemaExcel = "Schema.xls";
        private const string LocationExcel = "Building.shp";//Location.xls";
        private const string ModelFolder = "Model";

        public static bool TestProjectFolder(string strFolder, ref ProjectFileStructure pFileStructure, ref string errMsg)
        {
            if (!System.IO.Directory.Exists(strFolder))
                return false;

            ProjectFileStructure pTemp = new ProjectFileStructure();
            pTemp.RootFolder = strFolder;

            pTemp.ProjectExcel = Path.Combine(strFolder, ProjectExcel);
            if (!File.Exists(pTemp.ProjectExcel))
            {
                errMsg = "项目信息文件（Project.xls）不存在";
                return false;
            }

            pTemp.FlyFile = Path.Combine(strFolder, FlyFile);
            if (!File.Exists(pTemp.FlyFile))
            {
                errMsg = "项目Fly文件不存在";
                return false;
            }

            pTemp.ImageFile = Path.Combine(strFolder, ImageFile);
            //if (!File.Exists(pTemp.ImageFile))
            //{
            //    errMsg = "项目影像文件不存在";
            //    return false;
            //}

            pTemp.DemFile = Path.Combine(strFolder, ImageFile);
            //if (!File.Exists(pTemp.DemFile))
            //{
            //    errMsg = "项目高程文件不存在";
            //    return false;
            //}

            string[] schemaFolders = Directory.GetDirectories(strFolder);
            if (strFolder == null || strFolder.Length == 0)
            {
                errMsg = "没有方案";
                return false;
            }

            List<SchemaFileStructure> sListTemp = new List<SchemaFileStructure>();
            foreach (string schemaFolder in schemaFolders)
            {
                SchemaFileStructure sTemp = new SchemaFileStructure();
                sTemp.SchemaFolder = schemaFolder;
                sTemp.SchemaExcel = Path.Combine(schemaFolder, SchemaExcel);
                if (!File.Exists(sTemp.SchemaExcel))
                    continue;

                sTemp.LocationExcel = Path.Combine(schemaFolder, LocationExcel);
                if (!File.Exists(sTemp.LocationExcel))
                    continue;

                sTemp.ModelFolder = Path.Combine(schemaFolder, ModelFolder);
                if (!Directory.Exists(sTemp.ModelFolder))
                    continue;

                sListTemp.Add(sTemp);
            }

            if (sListTemp.Count == 0)
            {
                errMsg = "当前文件夹下没有方案";
                return false;
            }

            pTemp.SchemaStruactures = sListTemp;

            pFileStructure = pTemp;
            return true;
        }


        private static string m_WorkFolder = System.Windows.Forms.Application.StartupPath + "\\GuiHua\\WorkFolder";
        private static string m_LocationTemplateShp = System.Windows.Forms.Application.StartupPath + "\\GuiHua\\Template\\Building.shp";
        private const string BuildingShp = "Building.shp";
        public static bool ImportProject(ProjectFileStructure pFileStructure,ref ProjectInfo projectInfo)
        {
            if (pFileStructure == null)
                return false;

            if (projectInfo == null)
                projectInfo = new ProjectInfo();

            try
            {
                projectInfo.Folder = Path.Combine(m_WorkFolder, Guid.NewGuid().ToString());// pFileStructure.RootFolder;
                Directory.CreateDirectory(projectInfo.Folder);
                projectInfo.File = Path.Combine(projectInfo.Folder, FlyFile);
                File.Copy(pFileStructure.FlyFile, projectInfo.File);

                DataTable dtProejct = ExcelHelper.ReadData(pFileStructure.ProjectExcel);
                if (dtProejct != null && dtProejct.Rows.Count > 0)
                {
                    projectInfo.Name = dtProejct.Rows[0][0] as string;
                    projectInfo.Type = dtProejct.Rows[0][1] as string;
                    projectInfo.Enterprise = dtProejct.Rows[0][2] as string;
                    projectInfo.Address = dtProejct.Rows[0][3] as string;
                }


                List<SchemaInfo> schemaList = new List<SchemaInfo>();
                foreach (SchemaFileStructure schemaStructure in pFileStructure.SchemaStruactures)
                {
                    SchemaInfo schemaInfo = new SchemaInfo();
                    schemaInfo.Folder = System.IO.Path.Combine(projectInfo.Folder, System.IO.Path.GetFileName(schemaStructure.SchemaFolder));
                    schemaInfo.File = Path.Combine(schemaInfo.Folder, BuildingShp);

                    DataTable dtSchema = ExcelHelper.ReadData(schemaStructure.SchemaExcel);
                    if (dtSchema != null && dtSchema.Rows.Count > 0)
                    {
                        schemaInfo.Name = dtSchema.Rows[0][0] as string;
                        schemaInfo.Type = dtSchema.Rows[0][1] as string;
                        schemaInfo.BuildingArea = Convert.ToDouble(dtSchema.Rows[0][2]);
                        schemaInfo.VegetationArea = Convert.ToDouble(dtSchema.Rows[0][3]);
                        schemaInfo.RoadArea = Convert.ToDouble(dtSchema.Rows[0][4]);
                    }

                    schemaInfo.Project = projectInfo;

                    Directory.CreateDirectory(schemaInfo.Folder);                   // 方案文件夹
                    string modelFolder = Path.Combine(schemaInfo.Folder, ModelFolder); // 模型文件夹
                    Directory.CreateDirectory(modelFolder);
                    string[] modelFiles = Directory.GetFiles(schemaStructure.ModelFolder);// 模型
                    foreach (string strFile in modelFiles)
                    {
                        File.Copy(strFile, Path.Combine(modelFolder, Path.GetFileName(strFile)));
                    }
                    //schemaStructure.LocationExcel
                    //File.Copy(m_LocationTemplateShp+".shp",schemaInfo.File);

                    // shp
                    //CopyShp(Path.Combine(schemaStructure.SchemaFolder, BuildingShp), Path.Combine(schemaInfo.Folder, BuildingShp));
                    //CopyShp(Path.Combine(schemaStructure.SchemaFolder, "Building"), schemaInfo.Folder,"Building");
                    CopyShp(Path.Combine(schemaStructure.SchemaFolder, "Building"), Path.Combine(schemaInfo.Folder, "Building"));


                    // ESRI.ArcGIS.Geodatabase 

                    schemaList.Add(schemaInfo);
                }

                projectInfo.Schemas = schemaList;


                // 记录 
                AddProjectInfo(projectInfo);

            }
            catch
            {
                return false;
            }

            return true;

        }

        public static bool AddProjectInfo(ProjectInfo projectInfo)
        {
            if (projectInfo == null)
                return false;

            string projectID = Guid.NewGuid().ToString("N");
            string strSQL = @"Insert into GH_Project( 
                                                        ProjectID,
                                                        ProjectName ,
                                                        ProjectType ,
                                                        Enterprise ,
                                                        ProjectAdrress ,
                                                        ProjectFolder,
                                                        ProjectFile
                                                    )
                                               values(
                                                        '{0}',
                                                        '{1}',
                                                        '{2}',
                                                        '{3}',
                                                        '{4}',
                                                        '{5}',
                                                        '{6}'
                                                      )";

            object[] objArgs ={
                                 projectID,
                                 projectInfo.Name,
                                 projectInfo.Type,
                                 projectInfo.Enterprise,
                                 projectInfo.Address,
                                 projectInfo.Folder,
                                 projectInfo.File
                             };

            try
            {
                Environment.AdodbHelper.ExecuteSQL(string.Format(strSQL, objArgs));
            }
            catch
            {
                return false;
            }

            foreach (SchemaInfo schemaInfo in projectInfo.Schemas)
            {
                strSQL = @"Insert into GH_Schema(
                                                    SchemaID,
                                                    SchemaName,
                                                    SchemaType ,
                                                    BuildingArea ,
                                                    VegetationArea,
                                                    RoadArea ,
                                                    ProjectID ,
                                                    SchemaFolder ,
                                                    SchemaFile 

                                                )
                                          values(
                                                    '{0}',
                                                    '{1}',
                                                    '{2}',
                                                    {3},
                                                    {4},
                                                    {5},
                                                    '{6}',
                                                    '{7}',
                                                    '{8}'
                                                )";

                object[] objArgs2 ={
                                        Guid.NewGuid().ToString("N"),
                                        schemaInfo.Name,
                                        schemaInfo.Type,
                                        schemaInfo.BuildingArea,
                                        schemaInfo.VegetationArea,
                                        schemaInfo.RoadArea,
                                        projectID,
                                        schemaInfo.Folder,
                                        schemaInfo.File
                                };

                try
                {
                    Environment.AdodbHelper.ExecuteSQL(string.Format(strSQL, objArgs2));
                }
                catch
                {
                    return false;
                }

            }

            return true;
        }


        public static bool DeleteProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                Environment.AdodbHelper.ExecuteSQL(string.Format("delete from GH_Project where ProjectID='{0}'", projectInfo.ID));
                Environment.AdodbHelper.ExecuteSQL(string.Format("delete from GH_Schema where ProjectID='{0}'", projectInfo.ID));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CopyShp(string strSource, string strTarget)
        {
            try
            {
                File.Copy(strSource + ".shp", strTarget + ".shp");
                File.Copy(strSource + ".shx", strTarget + ".shx");
                File.Copy(strSource + ".dbf", strTarget + ".dbf");
                File.Copy(strSource + ".shp.xml", strTarget + ".shp.xml");
                File.Copy(strSource + ".prj", strTarget + ".prj");
                File.Copy(strSource + ".sbx", strTarget + ".sbx");
                File.Copy(strSource + ".sbn", strTarget + ".sbn");

                return true;
            }
            catch
            {
                return false;
            }
        }

        //public static bool CopyShp(string strSource, string targetPath,string targetName)
        //{
        //    FeatureClassToFeatureClass gpCopyFeatureClass = new FeatureClassToFeatureClass();
        //    gpCopyFeatureClass.in_features = strSource;
        //    gpCopyFeatureClass.out_path = targetPath;
        //    gpCopyFeatureClass.out_name = targetName;

        //    Geoprocessor geoProcessor = new Geoprocessor();
        //    geoProcessor.OverwriteOutput = true;
        //    try
        //    {
        //        IGeoProcessorResult gpResult = geoProcessor.Execute(gpCopyFeatureClass, null) as IGeoProcessorResult;
        //        //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);

        //        return gpResult.Status == esriJobStatus.esriJobSucceeded;

        //    }
        //    catch
        //    {
        //        if (geoProcessor.MessageCount > 2)
        //        {
        //            //ErrorMessage = geoProcessor.GetMessage(2);
        //        }
        //        else
        //        {
        //            object objArg = geoProcessor.MaxSeverity;
        //            //ErrorMessage = geoProcessor.GetMessages(ref objArg);
        //        }


        //        return false;
        //    }
        //    //CopyFeatures gpCopy = new CopyFeatures();
        //    //gpCopy.in_features = strSource;
        //    //gpCopy.out_feature_class = strTarget;

        //    //Geoprocessor geoProcessor = new Geoprocessor();
        //    //geoProcessor.OverwriteOutput = true;
        //    //try
        //    //{
        //    //    IGeoProcessorResult gpResult = geoProcessor.Execute(gpCopy, null) as IGeoProcessorResult;
        //    //    //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(geoProcessor);

        //    //    return gpResult.Status == esriJobStatus.esriJobSucceeded;

        //    //}
        //    //catch
        //    //{
        //    //    if (geoProcessor.MessageCount > 2)
        //    //    {
        //    //       // ErrorMessage = geoProcessor.GetMessage(2);
        //    //    }
        //    //    else
        //    //    {
        //    //       // object objArg = geoProcessor.MaxSeverity;
        //    //       // ErrorMessage = geoProcessor.GetMessages(ref objArg);
        //    //    }

        //    //    return false;
        //    //}
        //}

        //private static bool ImportModelToShp(string shpFile, string excelFile)
        //{
        //    //ESRI.ArcGIS.Geodatabase.IWorkspaceFactory wsf = new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass();
        //    //IWorkspace wsShp = wsf.OpenFromFile(Path.GetDirectoryName(shpFile), 0);
        //    //IFeatureClass fClass = (wsShp as IFeatureWorkspace).OpenFeatureClass(Path.GetFileNameWithoutExtension(shpFile));

        //}


        public static List<VersionInfo> GetVersionList(string projectID)
        {

            if (!Environment.AdodbHelper.TableExists("GH_Version"))
            {
                Environment.AdodbHelper.ExecuteSQL(@"Create table GH_Version (
                                        VersionDate varchar(255),
                                        VersionFile varchar(255),
                                        Description varchar(4000), 
                                        ProjectID varchar(32) not null)"
                    );

            }

            List<VersionInfo> dicVersion = new List<VersionInfo>();

            try
            {
                DataTable tVersion = Environment.AdodbHelper.OpenTable("GH_Version");
                int dIndex = tVersion.Columns.IndexOf("VersionDate");
                int fIndex = tVersion.Columns.IndexOf("VersionFile");
                int desIndex = tVersion.Columns.IndexOf("Description");

               
                DataRow[] c = tVersion.Select( string.Format("Projectid='{0}'", projectID));
                foreach (DataRow rowVersion in c)
                {
                    VersionInfo vInfo = new VersionInfo();
                    vInfo.Date = rowVersion[dIndex] as string;
                    vInfo.VersionFile = rowVersion[fIndex] as string;
                    vInfo.Description = rowVersion[desIndex] as string;

                    dicVersion.Add(vInfo);
                }
            }
            catch
            {
                return null;
            }

            return dicVersion;
        }


        public static bool AddVersion(string projectID, string vFile,string description)
        {
            try
            {
                object[] objArgs=
                {
                    DateTime.Now.ToString(),
                    vFile,
                    description,
                    projectID
                };
                Environment.AdodbHelper.ExecuteSQL(string.Format("Insert into GH_Version(VersionDate,VersionFile,Description,ProjectID) values('{0}','{1}','{2}','{3}')", objArgs));
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteVersion(string projectID, string vDate)
        {
            try
            {

                Environment.AdodbHelper.ExecuteSQL(string.Format("Delete from GH_Version where Projectid='{0}' and VersionDate='{1}'", projectID, vDate));

                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }

    public class VersionInfo
    {
        public string VersionFile { get; set; }

        public string Date { get; set; }

        public string Description{get;set;}
    }
}
