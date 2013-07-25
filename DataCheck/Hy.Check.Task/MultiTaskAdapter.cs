using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Hy.Common.Utility.Esri;

namespace Hy.Check.Task
{
    /// <summary>
    /// 批量任务适配器
    /// @remark
    /// 数据源存放路径@see::DatasourceFolder和任务存放路径@see::TaskPath是必须的
    /// 另外，默认数据源不分别存储@see::DataInDeferenceFolder，比例尺默认1：10000，拓扑容限默认0.0001
    /// 默认所有数据建立任务时不直接使用数据源，不立即执行
    /// </summary>
    public class MultiTaskAdapter
    {
        public MultiTaskAdapter()
        {
            this.DataInDeferenceFolder = false;
            this.MapScale = 10000;
            this.TopoTolerance = 0.0001;
        }

        /// <summary>
        /// 搜索数据源时的相对路径
        /// </summary>
        public string RelationalPath { set; private get; }

        /// <summary>
        /// 数据放在不同的文件夹下
        /// </summary>
        public bool DataInDeferenceFolder { set; private get; }

        /// <summary>
        /// 指数据源存放路径，即多任务数据源的父路径
        /// </summary>
        public string DatasourceFolder { set; private get; }

        /// <summary>
        /// 任务存放路径
        /// </summary>
        public string TaskPath { set; private get; }

        /// <summary>
        /// 方案
        /// </summary>
        public string SchemaID { set; private get; }

        /// <summary>
        /// 拓扑容限
        /// </summary>
        public double TopoTolerance { set; private get; }

        /// <summary>
        /// 空间参考
        /// 若设置了ReferenceLayer @see::ReferenceLayer属性，可以不用设置本属性
        /// </summary>
        public ESRI.ArcGIS.Geometry.ISpatialReference SpatialReference { set; private get; }

        /// <summary>
        /// 空间参考来源图层名
        /// 若设置了SpatialReference @see::SpatialReference属性，可以不用设置本属性
        /// </summary>
        public string ReferenceLayer { set; private get; }

        /// <summary>
        /// 地图比例尺
        /// </summary>
        public int MapScale { set; private get; }

        /// <summary>
        /// 测试数据源存放路径下是否具有数据源，并获取所有数据源路径，及相应的数据格式、默认任务名
        /// </summary>
        /// <param name="datasourceList"></param>
        /// <param name="datasourceTypeList"></param>
        /// <param name="taskNameList"></param>
        /// <returns></returns>
        public bool TestDatasourceFolder(ref List<string> datasourceList,ref List<enumDataType> datasourceTypeList,ref List<string> taskNameList)
        {
            if (!Directory.Exists(DatasourceFolder))
                return false;

            datasourceList = new List<string>();
            datasourceTypeList = new List<enumDataType>();
            taskNameList = new List<string>();

            // 顺序 MDB，FileGDB，VCT，Shp
            // 数据源分文件夹存放
            if (DataInDeferenceFolder)
            {
                string[] subDirs = Directory.GetDirectories(DatasourceFolder);
                for (int i = 0; i < subDirs.Length; i++)
                {
                    string subDir = subDirs[i];
                    string strTaskName = (new DirectoryInfo(subDir)).Name;    // 因不能保证每个文件夹能搜索成功，TaskName须在搜索成功时加入列表
                    if (!string.IsNullOrEmpty(this.RelationalPath))
                    {
                        subDir = subDir + "\\" + this.RelationalPath;
                    }

                    //mdb
                    string[] mdbFiles = Directory.GetFiles(subDir, "*.mdb", SearchOption.TopDirectoryOnly);
                    if (mdbFiles.Length > 0)
                    {
                        datasourceList.Add(mdbFiles[0]);
                        datasourceTypeList.Add(enumDataType.PGDB);
                        taskNameList.Add(strTaskName);
                        continue;
                    }

                    // FileGDB
                    string[] fileGDBFolders = Directory.GetDirectories(subDir, "*.gdb", SearchOption.TopDirectoryOnly);
                    if (fileGDBFolders.Length > 0)
                    {
                        datasourceList.Add(fileGDBFolders[0]);
                        datasourceTypeList.Add(enumDataType.FileGDB);
                        taskNameList.Add(strTaskName);
                        continue;
                    }

                    // VCT
                    string[] vctFiles = Directory.GetFiles(subDir, "*.VCT", SearchOption.TopDirectoryOnly);
                    if (vctFiles.Length > 0)
                    {
                        datasourceList.Add(vctFiles[0]);
                        datasourceTypeList.Add(enumDataType.VCT);
                        taskNameList.Add(strTaskName);
                        continue;
                    }

                    // shp
                    // Shp的搜索认为“相对路径”包含Shp Workspace
                    if (Directory.GetFiles(subDir, "*.shp", SearchOption.TopDirectoryOnly).Length > 0)
                    {
                        datasourceList.Add(subDir);
                        datasourceTypeList.Add(enumDataType.SHP);
                        taskNameList.Add(strTaskName);
                        continue;
                    }
                }
            }
            else
            {
                bool matched = false;

                //mdb
                string[] mdbFiles = Directory.GetFiles(DatasourceFolder, "*.mdb", SearchOption.TopDirectoryOnly);
                if (mdbFiles.Length > 0)
                {
                    for (int i = 0; i < mdbFiles.Length; i++)
                    {
                        datasourceList.Add(mdbFiles[i]);
                        datasourceTypeList.Add(enumDataType.PGDB);
                        taskNameList.Add(System.IO.Path.GetFileNameWithoutExtension(mdbFiles[i]));
                    }

                    matched = true;
                }

                if (!matched)
                {
                    // FileGDB
                    string[] fileGDBFolders = Directory.GetDirectories(DatasourceFolder, "*.gdb", SearchOption.TopDirectoryOnly);
                    if (fileGDBFolders.Length > 0)
                    {
                        for (int i = 0; i < fileGDBFolders.Length; i++)
                        {
                            datasourceList.Add(fileGDBFolders[i]);
                            datasourceTypeList.Add(enumDataType.FileGDB);
                            taskNameList.Add((new DirectoryInfo(fileGDBFolders[i])).Name);
                        }

                        matched = true;
                    }
                }

                if (!matched)
                {
                    // VCT
                    string[] vctFiles = Directory.GetFiles(DatasourceFolder, "*.VCT", SearchOption.TopDirectoryOnly);
                    if (vctFiles.Length > 0)
                    {
                        for (int i = 0; i < vctFiles.Length; i++)
                        {
                            datasourceList.Add(vctFiles[i]);
                            datasourceTypeList.Add(enumDataType.VCT);
                            taskNameList.Add(System.IO.Path.GetFileNameWithoutExtension(vctFiles[i]));
                        }

                        matched = true;
                    }
                }

                if (!matched)
                {
                    // shp
                    string[] strFolders = Directory.GetDirectories(DatasourceFolder);
                    //ESRI.ArcGIS.Geodatabase.IWorkspaceFactory wsFactory = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
                    for (int i = 0; i < strFolders.Length; i++)
                    {
                        //if (wsFactory.IsWorkspace(strFolders[0]))
                        if (Directory.GetFiles(strFolders[i], "*.shp", SearchOption.TopDirectoryOnly).Length > 0)
                        {
                            datasourceList.Add(strFolders[i]);
                            datasourceTypeList.Add(enumDataType.SHP);
                            taskNameList.Add((new DirectoryInfo(strFolders[i])).Name);
                        }
                    }
                }
            }

            return datasourceList.Count > 0;

        }

        public MultiTask CreateMultiTask()
        {
            List<string> m_DatasourceList = null;
            List<enumDataType> m_DatasourceTypes = null;
            List<string> m_TaskNameList = null;

            TestDatasourceFolder(ref m_DatasourceList, ref m_DatasourceTypes, ref m_TaskNameList);

            MultiTask multiTask = new MultiTask();

            int count = m_DatasourceList.Count;
            for (int i = 0; i < count; i++)
            {
                ExtendTask task = new ExtendTask();

                task.SourcePath = m_DatasourceList[i];
                string strTaskName = TaskHelper.GetValidateTaskName(this.DatasourceFolder, m_TaskNameList[i]);
                task.Path= this.DatasourceFolder;
                task.Name = strTaskName;
                task.TopoTolerance= this.TopoTolerance;
                task.MapScale= this.MapScale;
                task.DatasourceType= m_DatasourceTypes[i];
                task.UseSourceDirectly= false;
                task.SchemaID = this.SchemaID;
                task.CheckMode = enumCheckMode.CreateOnly;
               
                

                multiTask.AddTask(task);
            }

            return multiTask;

        }

    }
}
