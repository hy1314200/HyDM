using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using System.Reflection;
using DevExpress.XtraWizard;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using Check.Task;
using Common.Utility.Esri;
using Check.Task.DataImport;
using Check.Define;
using Check.Utility;
using Common.UI;
using CheckTask = Check.Task.Task;

namespace Check.UI.Forms 
{
    public partial class FrmBatchTasksWizard: XtraForm
    {
        private const string Text_CheckMode_CreateOnly = "仅创建";
        private const string Text_CheckMode_CheckPartly = "抽检";
        private const string Text_CheckMode_CheckAll = "全检";


        /// <summary>
        /// 任务是否创建完成
        /// </summary>
        private bool m_CreateFinishd = false;

        /// <summary>
        /// 搜索数据源时的相对路径
        /// </summary>
        public string RelationalPath { set; private get; }

        /// <summary>
        /// 数据放在不同的文件夹下
        /// </summary>
        public bool DataInDeferenceFolder { set; private get; }

        //定义在点击grid 数据源列时弹出Directory选项框
        private RepositoryItemButtonEdit m_RepTxtDatasource = new RepositoryItemButtonEdit();
        //定义在点击grid中比例尺列时弹出combox对话框
        private RepositoryItemComboBox m_RepCmbMapScale = new RepositoryItemComboBox();
        //在点击grid中的检查方式列时弹出抽检/全检对话框
        private RepositoryItemComboBox m_RepCmbCheckType = new RepositoryItemComboBox();
        private RepositoryItemButtonEdit m_RepCmbTaskPath = new RepositoryItemButtonEdit();

        private List<int> m_StandardIDList = null;
        private Dictionary<string, string> m_DicSchema = null;
        private int[] m_MapScales =
        {
            500,
            1000,
            2000,
            5000,
            10000,
            25000,
            50000,
            100000,
            250000,
            500000,
            1000000
        };

        /// <summary>
        /// 公共构造函数
        /// </summary>
        public FrmBatchTasksWizard()
        {
            InitializeComponent();

            Init();

        }


        private void Init()
        {
            // 标准和方案
            Dictionary<int, string> dicStandard = SysDbHelper.GetStandardInfo();
            if (dicStandard == null)
            {
                XtraMessageBox.Show("当前系统库中未建立标准", "HyDC 错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            Dictionary<int, string>.Enumerator enStandard = dicStandard.GetEnumerator();
            m_StandardIDList = new List<int>();
            while (enStandard.MoveNext())
            {
                cmbStandard.Properties.Items.Add(enStandard.Current.Value);
                m_StandardIDList.Add(enStandard.Current.Key);
            }
            if (cmbStandard.Properties.Items.Count > 0)
            {
                cmbStandard.SelectedIndex = 0;

               
            }


            //设置点击可以修改的列弹出的修改文本框的属性和订阅事件
            m_RepTxtDatasource.TextEditStyle = TextEditStyles.DisableTextEditor;
            m_RepTxtDatasource.ButtonClick += new ButtonPressedEventHandler(m_RepTxtDatasource_ButtonClick);
            m_RepCmbTaskPath.TextEditStyle = TextEditStyles.DisableTextEditor;
            m_RepCmbTaskPath.ButtonClick+= new ButtonPressedEventHandler(pRepTxtTaskPath_ButtonClick);
            m_RepCmbMapScale.Items.AddRange(m_MapScales);// new string[] {"1:500", "1:1000", "1:2000", "1:5000", "1:10000","1:25000", "1:50000" , "1:100000", "1:25000", "1:500000", "1:1000000"});
            m_RepCmbMapScale.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            m_RepCmbMapScale.DisplayFormat.FormatString = "1:{0}";
            m_RepCmbCheckType.Items.AddRange(new string[] { Text_CheckMode_CreateOnly, Text_CheckMode_CheckPartly, Text_CheckMode_CheckAll });
            m_RepCmbMapScale.TextEditStyle = TextEditStyles.DisableTextEditor;
            //m_RepCmbMapScale.SelectedIndexChanged += new EventHandler(m_pRepComBox_Scale_SelectedIndexChanged);
            m_RepCmbCheckType.TextEditStyle = TextEditStyles.DisableTextEditor;
            m_RepCmbCheckType.CloseUp += new CloseUpEventHandler(m_pRepComBox_CheckType_CloseUp);


            //
            wpSourceSelect.AllowNext = false;
        }



        private void txtTopoTolerance_Validating(object sender, CancelEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtTopoTolerance.Text, "^[1-9]d*.d*|0.d*[1-9]d*$"))
            {
                dxErrorProvider.SetError(txtTopoTolerance, "拓扑容限值为非数字");
                e.Cancel = true;
            }
        }

        private void cmbStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 方案联动
            cmbSchema.Properties.Items.Clear();

            if (cmbStandard.SelectedIndex < 0)
                return;

            m_DicSchema = SysDbHelper.GetSchemasInfo(cmbStandard.SelectedItem as string);
            if (m_DicSchema == null)
            {
                dxErrorProvider.SetError(cmbStandard, "当前标准下没有配置检查方案");
                return;
            }
            Dictionary<string, string>.Enumerator enSchema = m_DicSchema.GetEnumerator();
            while (enSchema.MoveNext())
            {
                cmbSchema.Properties.Items.Add(enSchema.Current);
            }
            if (cmbSchema.Properties.Items.Count > 0)
            {
                cmbSchema.SelectedIndex = 0;
            }

            // 空间参考图层联动
            List<StandardLayer> lyrList = LayerReader.GetLayersByStandard(m_StandardIDList[cmbStandard.SelectedIndex]);
            int count = lyrList.Count;
            for (int i = 0; i < count; i++)
            {
                if (lyrList[i].Type == enumLayerType.Table
                    || lyrList[i].Type == enumLayerType.UnKnown)
                    continue;
                cmbSpatialRefLayer.Properties.Items.Add(new KeyValuePair<string, string>(lyrList[i].Name, lyrList[i].AliasName));
            }
            if (cmbSpatialRefLayer.Properties.Items.Count > 0)
            {
                cmbSpatialRefLayer.SelectedIndex = 0;
            }
        }



        private FrmPreCheck m_FrmRules = new FrmPreCheck();
        void m_pRepComBox_CheckType_CloseUp(object sender, CloseUpEventArgs e)
        {
            if (e.CloseMode == PopupCloseMode.ButtonClick || e.CloseMode == PopupCloseMode.Normal)
            {
                try
                {
                    string strPreValue = e.Value as string;
                    if (strPreValue == Text_CheckMode_CreateOnly)  // 仅创建
                    {
                        gridViewCheckSetting.SetFocusedRowCellValue(gridViewCheckSetting.Columns["Check.Rules"], null);
                        return;
                    }
                    if (strPreValue == Text_CheckMode_CheckAll)  // 全检
                    {                        
                        //gridViewCheckSetting.SetFocusedRowCellValue(gridViewCheckSetting.Columns["Check.Rules"], (new Check.Engine.TemplateRules(((KeyValuePair<string, string>)cmbSchema.SelectedItem).Key)).CurrentSchemaRules);
                        gridViewCheckSetting.SetFocusedRowCellValue(gridViewCheckSetting.Columns["Check.Rules"], null);
                        return;
                    }
                    if (strPreValue == Text_CheckMode_CheckPartly)  // 抽检
                    {
                        if (m_FrmRules == null)
                            m_FrmRules = new FrmPreCheck();

                        m_FrmRules.CurrentTemplateRules = new TemplateRules(((KeyValuePair<string, string>)cmbSchema.SelectedItem).Key);

                        List<SchemaRuleEx> curValue = gridViewCheckSetting.GetFocusedRowCellValue("Check.Rules") as List<SchemaRuleEx>;
                        if (curValue != null)
                            m_FrmRules.SchemaRulesSelection = curValue;

                        if (m_FrmRules.ShowDialog() == DialogResult.Yes)
                        {
                            curValue = m_FrmRules.SchemaRulesSelection;
                            gridViewCheckSetting.SetFocusedRowCellValue(gridViewCheckSetting.Columns["Check.Rules"], curValue);
                        }
                        else
                        {
                            e.AcceptValue = false;
                        }
                    }

                }
                catch { }
            }
        }     

        //打开含有数据源的目录
        private void txtSourceFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.Description = "请选择数据源所在目录...";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string strSelectedPath=folderBrowserDialog.SelectedPath;
                if (strSelectedPath.Length == 3)
                {
                    strSelectedPath = strSelectedPath.Substring(0, 2);
                }
                if (this.TestSourcePath(strSelectedPath))
                {
                    txtSourceFolder.Text = strSelectedPath;

                    this.wpSourceSelect.AllowNext = !string.IsNullOrEmpty(txtTaskPath.Text);
                    this.labDatasource.Text = string.IsNullOrEmpty(txtSourceFolder.Text) ? "不能为空" : "";
                }
                else
                {
                    XtraMessageBox.Show("所选目录下没有数据源！");
                    txtSourceFolder.Text = "";
                    this.wpSourceSelect.AllowNext = false;
                }
            }
        }

        //选择存储任务的目录
        private void txtTaskPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "请选择保存质检任务的目录...";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtTaskPath.Text = folderBrowserDialog.SelectedPath;
                if (txtTaskPath.Text.Length == 3)
                {
                    txtTaskPath.Text = txtTaskPath.Text.Substring(0,2);
                }

                this.wpSourceSelect.AllowNext = !string.IsNullOrEmpty(txtSourceFolder.Text);
                this.labTaskPath.Text = string.IsNullOrEmpty(txtTaskPath.Text) ? "不能为空" : "";
            }
        }

  
        private void gridViewCheckSetting_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn == null)
                return;

            switch (e.FocusedColumn.FieldName)
            {
                case "TaskName":
                case "Datasource":
                case "MapScale":
                case "CheckMode":
                case "TopoTolerance":
                case "TaskPath":
                    gridViewCheckSetting.OptionsBehavior.Editable = true;
                    break;

                    // VCT是不允许直接使用数据源的
                case "UseSourceDirectly":
                    enumDataType dataType = (enumDataType)gridViewCheckSetting.GetFocusedRowCellValue(gridViewCheckSetting.Columns["DataType"]);
                    if (dataType == enumDataType.VCT)
                    {
                        gridViewCheckSetting.OptionsBehavior.Editable = false;
                    }
                    else
                    {
                        gridViewCheckSetting.OptionsBehavior.Editable = true;
                    }
                    break;

                default:
                    gridViewCheckSetting.OptionsBehavior.Editable = false;
                    break;
            }

            //if (e.FocusedColumn.FieldName == "Datasource"
            //  || e.FocusedColumn.FieldName == "MapScale"
            //  || e.FocusedColumn.FieldName == "CheckMode"
            //  || e.FocusedColumn.FieldName == "TopoTolerance"
            //  || e.FocusedColumn.FieldName == "Check.Rules")
            //{
            //    gridViewCheckSetting.OptionsBehavior.Editable = true;
            //}
            //else
            //{
            //    gridViewCheckSetting.OptionsBehavior.Editable = false;
            //}
        }

        /// <summary>
        /// 数据源路径修改
        /// </summary>
        void m_RepTxtDatasource_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            enumDataType dataType = (enumDataType)gridViewCheckSetting.GetRowCellValue(gridViewCheckSetting.FocusedRowHandle, "DataType");
            switch (dataType)
            {
                case enumDataType.SHP:
                case enumDataType.FileGDB:
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (folderBrowserDialog.SelectedPath.Length == 3)
                        {
                            gridViewCheckSetting.SetRowCellValue(gridViewCheckSetting.FocusedRowHandle, "Datasource", folderBrowserDialog.SelectedPath.Substring(0, 2));
                        }
                        else
                        {
                            gridViewCheckSetting.SetRowCellValue(gridViewCheckSetting.FocusedRowHandle, "Datasource", folderBrowserDialog.SelectedPath);
                        }
                    }

                    break;

                case enumDataType.PGDB:
                    dlgFileOpen.Filter = "*.MDB |*.MDB";
                    if (dlgFileOpen.ShowDialog() == DialogResult.OK)
                    {
                        gridViewCheckSetting.SetRowCellValue(gridViewCheckSetting.FocusedRowHandle, gridViewCheckSetting.FocusedColumn, dlgFileOpen.FileName);
                    }
                    break;

                case enumDataType.VCT:
                    dlgFileOpen.Filter = "*.VCT |*.VCT";
                    if (dlgFileOpen.ShowDialog() == DialogResult.OK)
                    {
                        gridViewCheckSetting.SetRowCellValue(gridViewCheckSetting.FocusedRowHandle, "Datasource", dlgFileOpen.FileName);
                    }
                    break;
            }
        }
        // 设置或修改质检任务保存路径
        private void pRepTxtTaskPath_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                string sCheckPath = gridViewCheckSetting.FocusedValue.ToString();
                folderBrowserDialog.ShowNewFolderButton = true;
                folderBrowserDialog.Description = "修改质检任务保存路径...";
                folderBrowserDialog.SelectedPath = sCheckPath;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    if (folderBrowserDialog.SelectedPath.Length == 3)
                    {
                        gridViewCheckSetting.SetRowCellValue(gridViewCheckSetting.FocusedRowHandle, gridViewCheckSetting.FocusedColumn, folderBrowserDialog.SelectedPath.Substring(0, 2));
                    }
                    else
                    {
                        gridViewCheckSetting.SetRowCellValue(gridViewCheckSetting.FocusedRowHandle, gridViewCheckSetting.FocusedColumn, folderBrowserDialog.SelectedPath);
                    }
                }
            }
            catch
            {

            }
        }


        // 选择数据源存放路径后获取所有数据源路径，及相应的数据格式、默认任务名
        List<string> m_DatasourceList =null;
        List<enumDataType> m_DatasourceTypes = null;
        List<string> m_TaskNameList = null;
        private bool TestSourcePath(string dataPath)
        {
            if (!Directory.Exists(dataPath))
                return false;

            m_DatasourceList = new List<string>();
            m_DatasourceTypes = new List<enumDataType>();
            m_TaskNameList = new List<string>();

            // 顺序 MDB，FileGDB，VCT，Shp
            // 数据源分文件夹存放
            if (DataInDeferenceFolder)
            {
                string[] subDirs = Directory.GetDirectories(dataPath);
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
                        m_DatasourceList.Add(mdbFiles[0]);
                        m_DatasourceTypes.Add(enumDataType.PGDB);
                        m_TaskNameList.Add(strTaskName);
                        continue;
                    }

                    // FileGDB
                    string[] fileGDBFolders = Directory.GetDirectories(subDir, "*.gdb", SearchOption.TopDirectoryOnly);
                    if (fileGDBFolders.Length > 0)
                    {
                        m_DatasourceList.Add(fileGDBFolders[0]);
                        m_DatasourceTypes.Add(enumDataType.FileGDB);
                        m_TaskNameList.Add(strTaskName);
                        continue;
                    }

                    // VCT
                    string[] vctFiles = Directory.GetFiles(subDir, "*.VCT", SearchOption.TopDirectoryOnly);
                    if (vctFiles.Length > 0)
                    {
                        m_DatasourceList.Add(vctFiles[0]);
                        m_DatasourceTypes.Add(enumDataType.VCT);
                        m_TaskNameList.Add(strTaskName);
                        continue;
                    }

                    // shp
                    // Shp的搜索认为“相对路径”包含Shp Workspace
                    if (Directory.GetFiles(subDir, "*.shp", SearchOption.TopDirectoryOnly).Length > 0)
                    {
                        m_DatasourceList.Add(subDir);
                        m_DatasourceTypes.Add(enumDataType.SHP);
                        m_TaskNameList.Add(strTaskName);
                        continue;
                    }
                }
            }
            else
            {
                // 2012-07-26 
                // 开放匹配模式为所选目录下的任何格式数据
                bool matched = false;

                //mdb
                string[] mdbFiles = Directory.GetFiles(dataPath, "*.mdb", SearchOption.TopDirectoryOnly);
                if (mdbFiles.Length > 0)
                {
                    for (int i = 0; i < mdbFiles.Length; i++)
                    {
                        m_DatasourceList.Add(mdbFiles[i]);
                        m_DatasourceTypes.Add(enumDataType.PGDB);
                        m_TaskNameList.Add(System.IO.Path.GetFileNameWithoutExtension(mdbFiles[i]));
                    }

                    //matched = true;
                }

                if (!matched)
                {
                    // FileGDB
                    string[] fileGDBFolders = Directory.GetDirectories(dataPath, "*.gdb", SearchOption.TopDirectoryOnly);
                    if (fileGDBFolders.Length > 0)
                    {
                        for (int i = 0; i < fileGDBFolders.Length; i++)
                        {
                            m_DatasourceList.Add(fileGDBFolders[i]);
                            m_DatasourceTypes.Add(enumDataType.FileGDB);
                            m_TaskNameList.Add((new DirectoryInfo(fileGDBFolders[i])).Name);
                        }

                        //matched = true;
                    }
                }

                if (!matched)
                {
                    // VCT
                    string[] vctFiles = Directory.GetFiles(dataPath, "*.VCT", SearchOption.TopDirectoryOnly);
                    if (vctFiles.Length > 0)
                    {
                        for (int i = 0; i < vctFiles.Length; i++)
                        {
                            m_DatasourceList.Add(vctFiles[i]);
                            m_DatasourceTypes.Add(enumDataType.VCT);
                            m_TaskNameList.Add(System.IO.Path.GetFileNameWithoutExtension(vctFiles[i]));
                        }

                        //matched = true;
                    }
                }

                if (!matched)
                {
                    // shp
                    string[] strFolders = Directory.GetDirectories(dataPath);
                    //ESRI.ArcGIS.Geodatabase.IWorkspaceFactory wsFactory = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
                    for (int i = 0; i < strFolders.Length; i++)
                    {
                        //if (wsFactory.IsWorkspace(strFolders[0]))
                        if (Directory.GetFiles(strFolders[i], "*.shp", SearchOption.TopDirectoryOnly).Length > 0)
                        {
                            m_DatasourceList.Add(strFolders[i]);
                            m_DatasourceTypes.Add(enumDataType.SHP);
                            m_TaskNameList.Add((new DirectoryInfo(strFolders[i])).Name);
                        }
                    }
                }
            }

            return m_DatasourceList.Count > 0;
        }

        /// <summary>
        /// 根据所选择的汇交成果路径和任务保存路径，为Gridt生成Datasource，以进行绑定
        /// </summary>
        /// <param name="taskPath"></param>
        /// <returns></returns>
        private DataTable GenerateGridSource(string taskPath,double topoTolerance,int mapScale)
        {
            int count = m_DatasourceList.Count;
            DataTable tSource = CreateTableShema();
            for (int i = 0; i < count; i++)
            {
                DataRow pDr = tSource.NewRow();
                //pDr["Index"] = i+1;

                pDr["Datasource"] = m_DatasourceList[i];
                string strTaskName =TaskHelper.GetValidateTaskName(taskPath, m_TaskNameList[i]);
                pDr["TaskPath"] = taskPath;
                pDr["TaskName"] = strTaskName;
                pDr["TopoTolerance"] = topoTolerance;
                pDr["MapScale"] = mapScale;
                pDr["DataType"] = m_DatasourceTypes[i];
                pDr["DisplayDataType"] = m_DatasourceTypes[i].ToString();
                pDr["UseSourceDirectly"] = false;
                pDr["CheckMode"] = Text_CheckMode_CreateOnly;                // 默认为创建并全检
                //pDr["Check.Rules"] = m_TemplateTask.pSchema.arrayChkTypes;   // 

                tSource.Rows.Add(pDr);
            }
            return tSource;

        }
        /// <summary>
        /// 生成DataTable框架
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableShema()
        {
            DataTable pTaskDt = new DataTable("Check.CheckTask");

            //构建一个包含质检任务设置的空datatable结构
            DataColumn pDc = new DataColumn();
            //pDc.Caption = "序号";
            //pDc.ColumnName = "Index";
            //pDc.DataType = Type.GetType("System.Int32");
            //pDc.ReadOnly = true;
            //pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.Caption = "任务名称";
            pDc.ColumnName = "TaskName";
            pDc.DataType = Type.GetType("System.String");
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.Caption = "数据源路径";
            pDc.ColumnName = "Datasource";
            pDc.DataType = Type.GetType("System.String");
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.Caption = "任务存放路径";
            pDc.ColumnName = "TaskPath";
            pDc.DataType = Type.GetType("System.String");
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn("UseSourceDirectly");
            pDc.Caption = "直接使用数据源";
            pDc.DataType = typeof(bool);
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn("DataType");
            pDc.DataType = typeof(enumDataType);
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn("DisplayDataType");
            pDc.Caption = "数据类型";
            pDc.DataType = typeof(string);
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.Caption = "拓扑容限";
            pDc.ColumnName = "TopoTolerance";
            pDc.DataType = Type.GetType("System.String");
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.Caption = "比例尺";
            pDc.ColumnName = "MapScale";
            pDc.DataType = Type.GetType("System.Int32");
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.Caption = "检查方式";
            pDc.ColumnName = "CheckMode";
            pDc.DataType = typeof(string);
            pTaskDt.Columns.Add(pDc);

            pDc = new DataColumn();
            pDc.ColumnName = "Check.Rules";
            pDc.DataType = typeof(object);
            pTaskDt.Columns.Add(pDc);

            return pTaskDt;
        }
       


        private void ValidatePath(WizardCommandButtonClickEventArgs e)
        {
           
        }

        /// <summary>
        /// 必要参数验证
        /// </summary>
        /// <returns></returns>
        private void ValidatePrimaryArgs(WizardCommandButtonClickEventArgs e)
        {
            // 验证
            bool flag = true;
            if (cmbStandard.SelectedIndex < 0)
            {
                dxErrorProvider.SetError(cmbStandard, "必须选择任务标准");
                flag = false;
            }
            else if (cmbSchema.Properties.Items.Count == 0)
            {
                dxErrorProvider.SetError(cmbStandard, "当前任务标准下没有配置检查方案");
                flag = false;
            }

            if (cmbSchema.SelectedIndex < 0)
            {
                dxErrorProvider.SetError(cmbSchema, "必须选择检查方案");
                flag = false;
            }

            e.Handled = !flag;

            // 生成下页数据
            gridViewCheckSetting.Columns.Clear();
            GridCheckSetting.DataSource = null;
            GridCheckSetting.Refresh();
            DataTable pTasksDt = this.GenerateGridSource(txtTaskPath.Text,double.Parse(txtTopoTolerance.Text),m_MapScales[ cmbScale.SelectedIndex]);// CBatchTasksApi.GetTasksDtatTable(BatchTaskType, BtnEditDCCG.Text, BtnEditZJCGSCML.Text);
            if (pTasksDt != null || pTasksDt.Rows.Count > 0)
            {

                GridCheckSetting.DataSource = pTasksDt;
                GridCheckSetting.RefreshDataSource();
                //gridViewCheckSetting.BestFitColumns();
                gridViewCheckSetting.RefreshData();
                GridCheckSetting.Refresh();
                //设置grid的属性
                //gridViewCheckSetting.OptionsBehavior.Editable = false;
                //gridViewCheckSetting.Columns["TaskName"].Visible = false;
                GridCheckSetting.RepositoryItems.Add(m_RepTxtDatasource);
                GridCheckSetting.RepositoryItems.Add(m_RepCmbTaskPath);
                GridCheckSetting.RepositoryItems.Add(m_RepCmbMapScale);
                GridCheckSetting.RepositoryItems.Add(m_RepCmbCheckType);
                //在点击button时弹出目录选择对话框
                //gridViewCheckSetting.Columns["Datasource"].ColumnEdit = m_RepTxtDatasource;
                gridViewCheckSetting.Columns["Datasource"].Visible = false;
                gridViewCheckSetting.Columns["TaskPath"].ColumnEdit = m_RepCmbTaskPath;
                //在点击修改比例尺大小时。弹出combox对话框进行选择
                gridViewCheckSetting.Columns["MapScale"].ColumnEdit = m_RepCmbMapScale;
                gridViewCheckSetting.Columns["MapScale"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                gridViewCheckSetting.Columns["MapScale"].DisplayFormat.FormatString = "1:{0}";

                //在点击检查方式时，弹出选择检查规则的对话框
                gridViewCheckSetting.Columns["CheckMode"].ColumnEdit = m_RepCmbCheckType;
                gridViewCheckSetting.Columns["Check.Rules"].Visible = false;
                gridViewCheckSetting.Columns["DataType"].Visible = false;
            }
            else
            {
                this.wpTaskSetting.AllowNext = false;
            }

        }

        private void ValidateTaskSetting(WizardCommandButtonClickEventArgs e)
        {
            wpComplete.FinishText = "完成";
            memoRemark.Text = "";
            //循环datagrid来创建和执行不同的任务
            for (int i = 0; i < gridViewCheckSetting.RowCount; i++)
            {
                if (i > 0)
                {
                    memoRemark.Text += "---------------------------------------------\r\n";
                }
                memoRemark.Text += string.Format("任务名称：{0}\r\n", gridViewCheckSetting.GetRowCellValue(i, gridViewCheckSetting.Columns["TaskName"]).ToString());
                memoRemark.Text += string.Format("数据目录：{0}\r\n", gridViewCheckSetting.GetRowCellValue(i, gridViewCheckSetting.Columns["Datasource"]).ToString());
                memoRemark.Text += string.Format("数据类型：{0}\r\n", ((enumDataType)gridViewCheckSetting.GetRowCellValue(i, gridViewCheckSetting.Columns["DataType"])).ToString());
                memoRemark.Text +=
                    string.Format("任务目录：{0}\r\n", gridViewCheckSetting.GetRowCellValue(i, gridViewCheckSetting.Columns["TaskPath"]).ToString());

                memoRemark.Text += string.Format("比例尺：\t1:{0}\r\n", gridViewCheckSetting.GetRowCellValue(i, gridViewCheckSetting.Columns["MapScale"]).ToString());
                memoRemark.Text += string.Format("拓扑容限：{0})\r\n", gridViewCheckSetting.GetRowCellValue(i, gridViewCheckSetting.Columns["TopoTolerance"]).ToString());

                memoRemark.Text += string.Format("检查单位：{0}\r\n", "");
                memoRemark.Text += string.Format("检查人：\t{0}\r\n", "");
                memoRemark.Text += string.Format("检查时间：{0}\r\n", DateTime.Now.ToString());
                //memoRemark.Text += string.Format("备注：\t\t{0}\r\n", "");
                memoRemark.Text += string.Format("检查方式:{0}\r\n\r\n", gridViewCheckSetting.GetRowCellValue(i, gridViewCheckSetting.Columns["CheckMode"]));
            }
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            DataTable tTasks = GridCheckSetting.DataSource as DataTable;
            if (tTasks.Rows.Count > 0)
            {

                this.Hide();

                MultiTask multiTask = new MultiTask();
                for (int i = 0; i < tTasks.Rows.Count; i++)
                {
                    multiTask.AddTask(this.GetTaskFromDataRow(tTasks.Rows[i]));
                }

                //FrmMultiTaskCheck frmCheck = new FrmMultiTaskCheck(multiTask);
                //frmCheck.ExcuteMultiTask(ref m_AvailableTasks);

                multiTask.CreatingTaskChanged += new TaskCreateEventsHandler(multiTask_CreatingTaskChanged);
                multiTask.TaskCreated += new TaskCreateEventsHandler(multiTask_TaskCreated);
                multiTask.CheckingTaskChanged += new TaskCheckEventsHandler(multiTask_CheckingTaskChanged);
                multiTask.TaskChecked += new TaskCheckEventsHandler(multiTask_TaskChecked);
                //m_FrmCheck.AdaptCheckerEvents(multiTask.TaskChecker);

                //System.Threading.ThreadStart threadStart = delegate
                //{
                bool isSucceed = multiTask.Excute(ref m_AvailableTasks);
                if (isSucceed)
                {
                    XtraMessageBox.Show(multiTask.PromptMessage);
                }
                else
                {
                    XtraMessageBox.Show("批量任务过程中出现错误，创建失败");
                }

                //};
                //System.Threading.Thread thread = new System.Threading.Thread(threadStart);
                //thread.Start();

            }
        }



        void multiTask_CreatingTaskChanged(CheckTask curTask)
        {
            m_GifProgress = new Common.UI.XGifProgress();
            m_GifProgress.ShowHint(string.Format("正在创建任务“{0}”…", curTask.Name));
        }
        void multiTask_TaskCreated(CheckTask curTask)
        {
            if (m_GifProgress != null)
                m_GifProgress.Hide();
        }

        void multiTask_CheckingTaskChanged(Check.Engine.Checker curChecker, CheckTask curTask)
        {
            m_FrmCheck = new FrmTaskCheck(curTask, null);
            m_FrmCheck.CurrentTask = curTask;
            m_FrmCheck.AdaptCheckerEvents(curChecker);

            m_FrmCheck.Show();
            m_FrmCheck.ReadyForCheck();
            Application.DoEvents();

            //if (m_FrmCheck.InvokeRequired)
            //{
            //System.Threading.ThreadStart threadStart = delegate
            //{
            //    if (m_FrmCheck.InvokeRequired)
            //    {
            //        System.Threading.ThreadStart threadStartSub = delegate
            //        {
            //            m_FrmCheck.Show();
            //            m_FrmCheck.ReadyForCheck();
            //        };
            //        m_FrmCheck.Invoke(threadStartSub);
            //    }
            //    else
            //    {
            //        m_FrmCheck.ReadyForCheck();
            //        m_FrmCheck.ShowDialog();
            //    }
            //};
            //System.Threading.Thread thread = new System.Threading.Thread(threadStart);
            //thread.Start();
            //}
            //else
            //{
            //    m_FrmCheck.Show();
            //    if (m_FrmCheck.InvokeRequired)
            //    {
            //        m_FrmCheck.Invoke(new System.Threading.ThreadStart(m_FrmCheck.ReadyForCheck));
            //    }
            //    else
            //    {
            //        m_FrmCheck.ReadyForCheck();
            //    }
            //    Application.DoEvents();
            //}

        }
        void multiTask_TaskChecked(Check.Engine.Checker curChecker, CheckTask curTask)
        {
            if (m_FrmCheck.InvokeRequired)
            {
                System.Threading.ThreadStart threadStart = delegate
                {
                    m_FrmCheck.Close();
                    m_FrmCheck.Dispose();
                };
                //System.Threading.Thread thread = new System.Threading.Thread(threadStart);
                //thread.Start();
                m_FrmCheck.Invoke(threadStart);
            }
            else
            {
                m_FrmCheck.Close();
                m_FrmCheck.Dispose();
            }
        }

        private FrmTaskCheck m_FrmCheck = null;//new FrmTaskCheck(null, null);


        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (e.Page == wpSourceSelect)
            {              
               ValidatePath(e);
            }
            else if (e.Page == wpParamterSet)
            {
                ValidatePrimaryArgs(e);
            }
            else if(e.Page==wpTaskSetting)
            {
                ValidateTaskSetting(e);
            }
        }

        private void FrmMoreTasksDevWiazard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.wpComplete==this.Taskwizard.SelectedPage)
            {
                return;
            }
            if (XtraMessageBox.Show("是否关闭创建任务向导?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
               DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private ExtendTask GetTaskFromDataRow(DataRow rowTask)
        {
            if (rowTask == null)
                return null;

            // Task本身
            ExtendTask task = new ExtendTask();
            task.DatasourceType = (enumDataType)rowTask["DataType"];
            task.MapScale = (int)rowTask["MapScale"];
            task.Name = rowTask["TaskName"] as string;
            task.Path = rowTask["TaskPath"] as string;
            task.SourcePath = rowTask["Datasource"] as string;
            task.StandardName = cmbStandard.SelectedItem as string;
            task.SchemaID = ((KeyValuePair<string, string>)cmbSchema.SelectedItem).Key;
            task.TopoTolerance = Convert.ToDouble( rowTask["TopoTolerance"]);
            task.UseSourceDirectly = (bool)rowTask["UseSourceDirectly"];
            task.Messager = this.MessageHandler;

            // DataImport
            IDataImport dataImporter = null;
            if (task.DatasourceType == enumDataType.VCT)
            {
                dataImporter = new VCTDataImport();
            }
            else
            {
                NoReferenceDataImport noRefDataImport = new NoReferenceDataImport();
                noRefDataImport.ReferenceLayer = ((KeyValuePair<string, string>)cmbSpatialRefLayer.SelectedItem).Key;
                dataImporter = noRefDataImport;
            }
            dataImporter.ImportingObjectChanged+=new ImportingObjectChangedHandler(dataImporter_ImportingObjectChanged);
            task.DataImporter = dataImporter;

            string strCheckMode = rowTask["CheckMode"] as string;
            enumCheckMode checkMode = enumCheckMode.CreateOnly;
            
            if (strCheckMode == Text_CheckMode_CheckPartly)
            {
                checkMode = enumCheckMode.CheckPartly;
            }
            if (strCheckMode == Text_CheckMode_CheckAll)
            {
                checkMode = enumCheckMode.CheckAll;
                task.ReadyForCheck(true);
            }
            task.CheckMode = checkMode;
            task.RuleInfos = rowTask["Check.Rules"] as List<SchemaRuleEx>;

            return task;
        }


        private List<CheckTask> m_AvailableTasks;
        public List<CheckTask> AvailableTasks { get { return m_AvailableTasks; } }


        XGifProgress m_GifProgress =null;// new Common.UI.XGifProgress();
        /// <summary>
        /// 显示进度条
        /// </summary>
        /// <param name="strOjbectName"></param>
        void dataImporter_ImportingObjectChanged(string strOjbectName)
        {
            m_GifProgress.ShowHint(strOjbectName);
        }
        /// <summary>
        /// 异常和操作消息处理
        /// </summary>
        void MessageHandler(enumMessageType msgType, string strMsg)
        {
            Common.Utility.Log.OperationalLogManager.AppendMessage(strMsg);
        }

      
        private void gridViewCheckSetting_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            DevExpress.XtraGrid.Columns .GridColumn colTaskName=gridViewCheckSetting.Columns["TaskName"];
            DevExpress.XtraGrid.Columns.GridColumn colTaskPath=gridViewCheckSetting.Columns["Datasource"];
            DevExpress.XtraGrid.Columns.GridColumn colValidating = gridViewCheckSetting.FocusedColumn;
            int rowHandle = gridViewCheckSetting.FocusedRowHandle;
            if (colValidating == colTaskName || colValidating == colTaskPath)
            {
                string strPath = gridViewCheckSetting.GetRowCellValue(rowHandle, colTaskPath) as string;
                string strName = gridViewCheckSetting.GetRowCellValue(rowHandle, colTaskName) as string;
                if (colValidating == colTaskName)
                {
                    strName = e.Value as string;
                }
                else
                {
                    strPath = e.Value as string;
                }

                if (string.IsNullOrEmpty(strName) || !TaskHelper.TestTaskName(strName))
                {
                    XtraMessageBox.Show("名称为空或者包含非法字符！");
                    e.Valid = false;
                    return;
                }
                string strMsg="";
                if (TaskHelper.TaskExists(strName, strPath, ref strMsg))
                {
                    XtraMessageBox.Show(strMsg);
                    e.Valid = false;
                    return;
                }
            }            
        }

     
    }
}