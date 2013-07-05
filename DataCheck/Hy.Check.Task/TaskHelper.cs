using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

using Common.Utility.Data;
using Common.Utility.Esri;
using Hy.Check.Utility;

namespace Hy.Check.Task
{
    /// <summary>
    /// Task的相关处理
    /// </summary>
    public class TaskHelper
    {

        /// <summary>
        /// 指写入数据库记录
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static bool AddTask(Task task)
        {
            try
            {
                IDbConnection sysConnection = SysDbHelper.GetSysDbConnection();
                DataTable tTask = AdoDbHelper.GetDataTable(sysConnection, "select * from LR_ModelTask where 1=2");
                DataRow rowNewTask = tTask.NewRow();
                rowNewTask["TaskID"] = task.ID;
                rowNewTask["TaskName"] = task.Name;
                rowNewTask["TaskPath"] = task.Path;
                rowNewTask["LibraryName"] = task.SourcePath;
                rowNewTask["TaskType"] = (int)task.DatasourceType;
                rowNewTask["SchemaID"] = task.SchemaID;
                rowNewTask["ExeState"] =(int) task.State;
                rowNewTask["Institution"] = task.Institution;
                rowNewTask["Person"] = task.Creator;
                rowNewTask["CreateTime"] = task.CreateTime;
                rowNewTask["Remark"] = task.Remark;
                rowNewTask["MapScale"] = task.MapScale;
                rowNewTask["TopoTolerance"] = task.TopoTolerance;
                rowNewTask["UseDatasource"] = task.UseSourceDirectly;
                
                rowNewTask["StandardID"]=SysDbHelper.GetStandardID( task.StandardName);

                rowNewTask["LibraryID"] = 0;
                rowNewTask["BIsTemplate"] = 0;

                tTask.Rows.Add(rowNewTask);
                return AdoDbHelper.UpdateTable("LR_ModelTask", tTask, sysConnection);

            }
            catch
            {
                return false;
            }
        }
      
        //public static bool DeleteTask(TaskInfo task)
        //{
        //    return false;
        //}

        /// <summary>
        /// 指删除数据库记录
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static bool DeleteTask(string taskID)
        {
            IDbConnection sysConnection = SysDbHelper.GetSysDbConnection();
            return AdoDbHelper.ExecuteSql(sysConnection, string.Format("delete from LR_ModelTask where TaskID='{0}'", taskID));
        }

        /// <summary>
        /// 通过ID获取Task对象
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static Task GetTask(string taskID)
        {
            IDbConnection sysConnection = SysDbHelper.GetSysDbConnection();
            DataTable tTask = AdoDbHelper.GetDataTable(sysConnection,string.Format( "select * from LR_ModelTask where TaskID='{0}'",taskID));

            if (tTask == null || tTask.Rows.Count == 0)
                return null;

            return GetTaskFromDataRow(tTask.Rows[0]);
        }

        /// <summary>
        /// 获取系统库中所有记录，形成Task对象
        /// </summary>
        /// <returns></returns>
        public static List<Task> GetAllTasks()
        {
            DataTable tTask = GetAllTaskRecord();
            if (tTask == null)
                return null;

            int count = tTask.Rows.Count;
            List<Task> taskList = new List<Task>(count);
            for (int i = 0; i < count; i++)
            {
                Task task = GetTaskFromDataRow(tTask.Rows[i]);
                if (task != null)
                    taskList.Add(task);
            }

            return taskList;
        }

        /// <summary>
        /// 获取系统库中所有记录
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTaskRecord()
        {
            IDbConnection sysConnection = SysDbHelper.GetSysDbConnection();
            return  AdoDbHelper.GetDataTable(sysConnection, "select * from LR_ModelTask where BIsTemplate=0");
        }

        /// <summary>
        /// 从数据记录形成Task对象
        /// </summary>
        /// <param name="rowTask"></param>
        /// <returns></returns>
        public static Task GetTaskFromDataRow(System.Data.DataRow rowTask)
        {
            if (rowTask == null)
                return null;

            try
            {
                Task task = new Task();
                task.ID = rowTask["TaskID"] as string;
                task.Name = rowTask["TaskName"] as string;
                task.Path = rowTask["TaskPath"] as string;
                task.SourcePath = rowTask["LibraryName"] as string;
                task.DatasourceType =(enumDataType)Convert.ToInt32( rowTask["TaskType"]);
                task.SchemaID = rowTask["SchemaID"] as string;
                task.State =(enumTaskState) Convert.ToInt32(rowTask["ExeState"]);
                task.StandardName = SysDbHelper.GetStandardName(Convert.ToInt32(rowTask["StandardID"]));
                task.Institution = rowTask["Institution"] as string;
                task.Creator = rowTask["Person"] as string;
                task.CreateTime = rowTask["CreateTime"] as string;
                task.Remark = rowTask["Remark"] as string;
                task.MapScale = Convert.ToInt32(rowTask["MapScale"]);
                task.TopoTolerance = Convert.ToDouble(rowTask["TopoTolerance"]);
                task.UseSourceDirectly = (bool)rowTask["UseDatasource"];
                return task;

            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 检测指定路径下指定名称的任务是否存在（包括文件和系统库）
        /// </summary>
        /// <param name="strTaskName"></param>
        /// <param name="strTaskPath"></param>
        /// <returns></returns>
        public static bool TaskExists(string strName, string strPath, ref string existString)
        {
            if (System.IO.Directory.Exists(strPath + "\\" + strName))
            {
                existString = string.Format("指定文件夹下存在名为{0}的任务或文件夹", strName);
                return true;
            }
            if (TaskExistsInDB(strName, strPath))
            {
                existString = string.Format("数据库中存在与此路径和名称都相同的任务记录");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 检测指定路径下指定名称的任务在数据库中是否存在
        /// </summary>
        /// <param name="strTaskName"></param>
        /// <param name="strTaskPath"></param>
        /// <returns></returns>
        public static bool TaskExistsInDB(string strName, string strPath)
        {
            IDbConnection sysConnection = SysDbHelper.GetSysDbConnection();
            DataTable tTask = AdoDbHelper.GetDataTable(sysConnection, string.Format("select * from LR_ModelTask where TaskPath='{0}' and TaskName='{1}'", strPath.Replace("'", "''"), strName.Replace("'", "''")));

            return tTask.Rows.Count > 0;
        }

        /// <summary>
        /// 测试任务名是否合法（指作为Windows下的文件夹名）
        /// </summary>
        /// <param name="strTaskName"></param>
        /// <returns></returns>
        public static bool TestTaskName(string strName)
        {
            string strTestPath = System.Windows.Forms.Application.StartupPath + "\\" + strName;
            try
            { 
                System.IO.Directory.CreateDirectory(strTestPath);
                System.IO.Directory.Delete(strTestPath);
                return true;
            }
            catch
            {
                if (System.IO.Directory.Exists(strTestPath))
                {
                    try
                    {
                        System.IO.Directory.Delete(strTestPath);
                    }
                    catch
                    {
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 验证指定路径下任务名是否合适，并返回可用的任务名
        /// </summary>
        /// <param name="strTaskPath"></param>
        /// <param name="strTaskName"></param>
        /// <param name="trueTaskName"></param>
        /// <returns></returns>
        public static string GetValidateTaskName(string strTaskPath, string strTaskName)
        {
            string errMsg = null;
            if (TaskExists(strTaskName, strTaskPath, ref errMsg) || !TestTaskName(strTaskName))
            {
                return string.Format("{0}_{1}", strTaskName, DateTime.Now.ToString("yyyy-MM-dd-h-m-s"));
            }

            return strTaskName;
        }

        /// <summary>
        /// 使用XmlSerializer进行反序列化
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        private static Task FromXml(string strXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Task));
            TextReader txtReader = new StringReader(strXml);

            return xmlSerializer.Deserialize(txtReader) as Task;
        }

        /// <summary>
        /// 从SystemConfig.xml文件还原任务
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static Task FromTaskConfig(string strFile)
        {
            string strXml = Common.Utility.Encryption.FileEncryption.DecryptKey(strFile);
            return FromXml(strXml);
        }

        /// <summary>
        /// 生成任务文件夹
        /// </summary>
        /// <param name="strTaskPath"></param>
        /// <param name="strTaskName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool CreateTaskFolder(string strPath, string strName, ref string errMsg)
        {
            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                Directory.CreateDirectory(strPath + "\\" + strName);

                return true;
            }
            catch (Exception exp)
            {
                errMsg = "创建任务文件夹结构出错"+exp.ToString();
                return false;
            }

        }

        /// <summary>
        /// 生成检查数据库（Base库和Query库）
        /// </summary>
        /// <param name="strTaskPath">任务所在路径</param>
        /// <param name="strTaskName"></param>
        /// <param name="baseUseSource"></param>
        /// <param name="queryUseBase"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool CreateCheckDB(string strTaskPath, string strTaskName, bool baseUseSource, bool queryUseBase, ref string errMsg)
        {
            string strPath = strTaskPath + "\\" + strTaskName;
            // Base 库
            if (!baseUseSource)
            {
                if (!AEAccessFactory.CreateFGDB(strPath, COMMONCONST.DB_Name_Base))
                {
                    errMsg = "创建Base库时出错";
                    return false;
                }
            }

            // Query
            if (!queryUseBase)
            {
                if (!AEAccessFactory.CreatePGDB(strPath, COMMONCONST.DB_Name_Query))
                {
                    errMsg = "创建Query库出错";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 生成结果库
        /// </summary>
        /// <param name="strTaskPath">任务所在路径</param>
        /// <param name="strTaskName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool CreateResultDB(string strTaskPath, string strTaskName, ref string errMsg)
        {
            try
            {
                // Result
                File.Copy(System.Windows.Forms.Application.StartupPath + "\\template\\report\\result_AutoTmpl.mdb", strTaskPath + "\\" + strTaskName + "\\" + COMMONCONST.DB_Name_Result, true);
                return true;
            }
            catch(Exception exp)
            {
                errMsg = "创建结果库出错"+exp.ToString();
                return false;
            }
        }

        /// <summary>
        /// 创建抽检文件夹
        /// </summary>
        /// <param name="strTaskPath">任务所在路径</param>
        /// <param name="strTaskName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool CreatePartlyCheckFolder(string strTaskPath, string strTaskName, ref string errMsg)
        {
            // 抽检文件夹
            string path = strTaskPath + "\\" + strTaskName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = path + "\\" + COMMONCONST.Folder_Name_PartlyCheck;
            if (Directory.Exists(path))
            {
                errMsg = "抽检文件夹创建失败：文件夹已经存在";
                return false;
            }

            try
            {
                File.Copy(System.Windows.Forms.Application.StartupPath + "\\template\\report\\result_AutoTmpl.mdb", path + "\\" + COMMONCONST.DB_Name_Result, true);
            }
            catch (Exception exp)
            {
                errMsg = "生成结果库出错，信息：" + exp.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新Task的状态到数据库
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskState"></param>
        /// <returns></returns>
        public static bool UpdateTaskState(string taskID, enumTaskState taskState)
        {
            if (taskState == enumTaskState.PartlyExcuted)
                return true;

            string strSQL = string.Format("Update LR_ModelTask set ExeState={0} where TaskID='{1}'", (int)taskState, taskID);
            return AdoDbHelper.ExecuteSql(SysDbHelper.GetSysDbConnection(), strSQL);
        }
    }
}
