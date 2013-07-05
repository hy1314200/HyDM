using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using Microsoft.Office.Interop.Excel;

using Common.UI;
using Path = System.IO.Path;
using System.Collections.Specialized;

using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using IPoint = ESRI.ArcGIS.Geometry.IPoint;

namespace Hy.Check.Utility
{
    public class ErrorExporter
    {

        public static DataTable GetError(IDbConnection resultConnection)
        {
            string strSQL = @"
                            SELECT
                            b.CheckType,
                            a.TargetFeatClass1 as YSTC,
                            b.GZBM,
                            a.TargetFeatClass2 as MBTC,
                            a.BSM2 as BSM2,
                            a.BSM as SourceBSM,
                            a.ErrMsg as Description,
                            '' as TopoLayerName,
                            '' as ArcGisRule,
                            '' as JHLX,
                            'LR_ResAutoAttr' as SysTab,
                            a.IsException ,
                            a.Remark
                            from LR_ResAutoAttr as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                                union all

                            SELECT
                            b.CheckType,
                            a.YSTC as YSTC,
                            b.GZBM ,
                            a.MBTC as MBTC,
                            a.TargetBSM as BSM2,
                            a.SourceBSM as SourceBSM,
                            a.Reason as Description,
                            a.TPTC as TopoLayerName,
                            a.ArcGisRule as ArcGisRule,
                            a.JHLX as JHLX,
                            'LR_ResAutoTopo' as SysTab,
                            a.IsException ,
                            a.Remark
                            from LR_ResAutoTopo as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                                union all

                            SELECT
                            b.CheckType,
                            a.AttrTabName as YSTC,
                            b.GZBM,
                            '' as MBTC,
                            '' as BSM2,
                            '' as SourceBSM,
                            a.ErrorReason as Description,
                            '' as TopoLayerName,
                            '' as ArcGisRule,
                            '' as JHLX,
                            'LR_ResIntField' as SysTab,
                            a.IsException ,
                            a.Remark
                            from LR_ResIntField as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID

                                union all

                            SELECT
                            b.CheckType,
                            a.ErrorLayerName as YSTC,
                            b.GZBM,
                            '' as MBTC,
                            '' as BSM2,
                            '' as SourceBSM,
                            a.ErrorReason as Description,
                            '' as TopoLayerName,
                            '' as ArcGisRule,
                            '' as JHLX,
                            'LR_ResIntLayer' as SysTab,
                            a.IsException ,
                            a.Remark
                            from LR_ResIntLayer as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID";

            DataTable dtError = Common.Utility.Data.AdoDbHelper.GetDataTable(resultConnection, strSQL);

            // 修改字段Caption
            dtError.Columns["CheckType"].Caption = "检查类型";
            dtError.Columns["YSTC"].Caption = "图层名";
            dtError.Columns["SourceBSM"].Caption = "标识码";
            dtError.Columns["MBTC"].Caption = "图层2";
            dtError.Columns["BSM2"].Caption = "标识码2";
            dtError.Columns["TopoLayerName"].Caption = "拓扑图层名";
            dtError.Columns["Description"].Caption = "错误描述";
            dtError.Columns["IsException"].Caption = "是否例外";
            dtError.Columns["Remark"].Caption = "说明";
            dtError.Columns["GZBM"].Caption = "规则编码";


            return dtError;
        }


        /// <summary>
        /// 导出错误到Excel文件
        /// </summary>
        public static bool ExportToExcel(XProgress xProgress, DataTable dtError, string strFilePath)
        {
            try
            {


                if (File.Exists(strFilePath))
                {
                    try
                    {
                        File.Delete(strFilePath);
                    }
                    catch(Exception exp)
                    {
                        Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                        return false;
                    }

                }

                Application xlApp = new Application();

                Workbook workbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet worksheet = null;

                xProgress.ShowGifProgress(null);
                xProgress.ShowHint("正在导出错误结果至Excel文件...");

                try
                {

                    //写入数值 
                    DataTable PropertyTable = dtError;
                    //DataTable TopoTable = CCheckApplication.ucTopoErrMap.m_DataTable;

                    #region 导出属性记录表和拓扑记录表错误
                    if (PropertyTable != null && PropertyTable.Rows.Count != 0)
                    {
                        //对于每种质检类型，创建一个worksheet
                        Hashtable hashtable = new Hashtable();
                        int index = 1;
                        foreach (DataRow dr in PropertyTable.Rows)
                        {
                            string strChkType = dr["CheckType"].ToString();
                            if (strChkType == "")
                            {
                                continue;
                            }
                            if (!hashtable.Contains(strChkType))
                            {
                                hashtable.Add(strChkType, "");
                                if (index == 1)
                                {
                                    worksheet = (Worksheet)workbook.Worksheets[1]; //取得sheet1 
                                    worksheet.Name = strChkType;
                                }
                                else
                                {
                                    worksheet =
                                        (Worksheet)
                                        workbook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                                    worksheet.Name = strChkType;
                                }
                                index++;
                            }
                        }

                        ///向worksheet中写入字段名
                        //写入字段
                        List<string> listPropertyFields = GetPropertyListFields();
                        List<string> listTableStructFields = GetStructFields();
                        List<string> listTopoFields = GetTopoListFields();

                        Worksheet tempSheet = null;
                        for (int k = 1; k <= workbook.Sheets.Count; k++)
                        {
                            tempSheet = (Worksheet)workbook.Sheets[k];
                            if (tempSheet.Name.Contains("拓扑关系") && tempSheet.Name != "点层内拓扑关系")
                            {
                                for (int i = 0; i < listTopoFields.Count; i++)
                                {
                                    tempSheet.Cells[1, i + 1] = listTopoFields[i];
                                }


                            }
                            else if (tempSheet.Name == "结构符合性")
                            {
                                for (int i = 0; i < listTableStructFields.Count; i++)
                                {
                                    tempSheet.Cells[1, i + 1] = listTableStructFields[i];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listPropertyFields.Count; i++)
                                {
                                    tempSheet.Cells[1, i + 1] = listPropertyFields[i];
                                }
                            }

                            Marshal.ReleaseComObject(tempSheet);
                        }


                        int PropertyRowCount = PropertyTable.Rows.Count;
                        int PropertyColumnCount = PropertyTable.Columns.Count;

                        xProgress.ShowProgress(0, PropertyRowCount, 1, null);
                        DataRow[] listDr = null;
                        DataColumn dc;

                        tempSheet = new Worksheet();
                        for (int l = 1; l <= workbook.Sheets.Count; l++)
                        {
                            tempSheet = (Worksheet)workbook.Sheets[l];
                            string strChkType = tempSheet.Name;
                            listDr = PropertyTable.Select("CheckType = '" + strChkType + "'");
                            for (int r = 0; r < listDr.Length; r++)
                            {
                                xProgress.Step();
                                for (int i = 0; i < PropertyColumnCount; i++)
                                {
                                    dc = PropertyTable.Columns[i];

                                    if (strChkType.Contains("拓扑关系") && strChkType != "点层内拓扑关系")
                                    {
                                        string Exception = "顺序号，源图层ID，目标图层ID，目标OID，源OID，几何类型，规则ID，系统表名";
                                        if (Exception.Contains(dc.Caption))
                                        {
                                            continue;
                                        }
                                        for (int k = 0; k < listTopoFields.Count; k++)
                                        {
                                            if (dc.Caption == listTopoFields[k])
                                            {
                                                if (dc.Caption == "是否例外")
                                                {
                                                    if (Convert.ToBoolean(listDr[r][i]) == false)
                                                    {
                                                        tempSheet.Cells[r + 2, k + 1] = "否";
                                                    }
                                                    else
                                                    {
                                                        tempSheet.Cells[r + 2, k + 1] = "是";
                                                    }
                                                }
                                                else
                                                {
                                                    tempSheet.Cells[r + 2, k + 1] = listDr[r][i];
                                                }
                                                break;
                                            }
                                        }

                                    }
                                    else if (tempSheet.Name == "结构符合性")
                                    {
                                        if (dc.Caption == "图层2" || dc.Caption == "标识码2" || dc.Caption == "拓扑图层名" ||
                                            dc.Caption == "顺序号" || dc.Caption == "源图层ID" || dc.Caption == "目标图层ID" ||
                                            dc.Caption == "目标OID" || dc.Caption == "源OID" || dc.Caption == "几何类型" ||
                                            dc.Caption == "规则ID" || dc.Caption == "系统表名")
                                        {
                                            continue;
                                        }
                                        for (int k = 0; k < listTableStructFields.Count; k++)
                                        {
                                            if (dc.Caption == listTableStructFields[k])
                                            {
                                                if (dc.Caption == "是否例外")
                                                {
                                                    object bIsException = listDr[r][i];

                                                    if (bIsException == null || bIsException.ToString() == "")
                                                    {
                                                        tempSheet.Cells[r + 2, k + 1] = "否";
                                                    }
                                                    else
                                                    {

                                                        if (Convert.ToBoolean(bIsException) == false)
                                                        {
                                                            tempSheet.Cells[r + 2, k + 1] = "否";
                                                        }
                                                        else
                                                        {
                                                            tempSheet.Cells[r + 2, k + 1] = "是";
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    tempSheet.Cells[r + 2, k + 1] = listDr[r][i];
                                                }

                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (dc.Caption == "图层2" || dc.Caption == "标识码2" || dc.Caption == "拓扑图层名" ||
                                            dc.Caption == "顺序号" || dc.Caption == "源图层ID" || dc.Caption == "目标图层ID" ||
                                            dc.Caption == "目标OID" || dc.Caption == "源OID" || dc.Caption == "几何类型" ||
                                            dc.Caption == "规则ID" || dc.Caption == "系统表名")
                                        {
                                            continue;
                                        }
                                        for (int k = 0; k < listPropertyFields.Count; k++)
                                        {
                                            if (dc.Caption == listPropertyFields[k])
                                            {
                                                if (dc.Caption == "是否例外")
                                                {
                                                    object bIsException = listDr[r][i];

                                                    if (bIsException == null || bIsException.ToString() == "")
                                                    {
                                                        tempSheet.Cells[r + 2, k + 1] = "否";
                                                    }
                                                    else
                                                    {

                                                        if (Convert.ToBoolean(bIsException) == false)
                                                        {
                                                            tempSheet.Cells[r + 2, k + 1] = "否";
                                                        }
                                                        else
                                                        {
                                                            tempSheet.Cells[r + 2, k + 1] = "是";
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    tempSheet.Cells[r + 2, k + 1] = listDr[r][i];
                                                }

                                                break;
                                            }
                                        }
                                    }


                                }
                            }
                            tempSheet.Columns.AutoFit();
                        }

                        if (tempSheet != null)
                        {
                            Marshal.ReleaseComObject(tempSheet);
                            tempSheet = null;
                        }


                    }
                    #endregion

                    xProgress.Hide();
                    //XtraMessageBox.Show("导出错误记录到Excel文件成功!", "提示");

                    ///保存excel文件
                    workbook.Close(true, strFilePath, null);
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    Marshal.ReleaseComObject(workbook);
                    if (worksheet != null)
                        Marshal.ReleaseComObject(worksheet);
                    GC.Collect();
                    if (xlApp != null)
                    {
                        Process[] pProcess;
                        pProcess = Process.GetProcessesByName("EXCEL"); //关闭excel进程
                        pProcess[0].Kill();
                    }
                    PropertyTable = null;
                    xlApp = null;
                    workbook = null;
                    worksheet = null;

                }
                catch (Exception exp)
                {
                    Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                    xProgress.Hide();

                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    Marshal.ReleaseComObject(workbook);
                    if (worksheet != null)
                        Marshal.ReleaseComObject(worksheet);
                    GC.Collect();
                    if (xlApp != null)
                    {
                        Process[] pProcess;
                        pProcess = Process.GetProcessesByName("EXCEL"); //关闭excel进程
                        pProcess[0].Kill();
                    }
                    //XtraMessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    xProgress.Hide();

                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                xProgress.Hide();
                //XtraMessageBox.Show(ex.Message);

                return false;

            }
            return true;
        }

        #region 错误列表的列名

        /// <summary>
        /// excel表中拓扑错误字段名
        /// </summary>
        /// <returns></returns>
        private static List<string> GetTopoListFields()
        {
            List<string> listFields = new List<string>();
            //listFields.Add("顺序号");
            listFields.Add("检查类型");
            listFields.Add("图层名");
            listFields.Add("标识码");
            listFields.Add("图层2");
            listFields.Add("标识码2");
            listFields.Add("拓扑图层名");
            listFields.Add("错误描述");
            listFields.Add("是否例外");
            listFields.Add("说明");

            // 2012-04-24 张航宇
            // 添加规则编码
            listFields.Add("规则编码");

            return listFields;

        }

        /// <summary>
        /// excel表中属性错误字段名
        /// </summary>
        /// <returns></returns>
        private static List<string> GetPropertyListFields()
        {
            List<string> listFields = new List<string>();
            //listFields.Add("顺序号");
            listFields.Add("检查类型");
            listFields.Add("图层名");
            listFields.Add("标识码");
            listFields.Add("错误描述");
            listFields.Add("是否例外");
            listFields.Add("说明");

            // 2012-04-24 张航宇
            // 添加规则编码
            listFields.Add("规则编码");

            return listFields;
        }

        /// <summary>
        /// 结构符合性需要增加字段名称字段
        /// </summary>
        /// <returns></returns>
        private static List<string> GetStructFields()
        {
            List<string> listFields = new List<string>();
            //listFields.Add("顺序号");
            listFields.Add("检查类型");
            listFields.Add("图层名");
            listFields.Add("字段名称");
            listFields.Add("错误描述");
            listFields.Add("是否例外");
            listFields.Add("说明");

            // 2012-04-24 张航宇
            // 添加规则编码
            listFields.Add("规则编码");

            return listFields;
        }

        #endregion


        public string SchemaID { private get; set; }

        public ITopology Topology { private get; set; }

        public IWorkspace BaseWorkspace { private get; set; }

        public IDbConnection ResultConnection { private get; set; }


        public bool ExportToShp(string strPath, string strName)
        {
            IFeatureClass fClassDest = CreateShpFile(strPath, strName);
            bool isSucceed = (fClassDest != null);
            isSucceed = isSucceed && InsertNomarlError(fClassDest);
            isSucceed = isSucceed && InsertTopoError(fClassDest);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(fClassDest);
            fClassDest = null;

            return isSucceed;
        }

        private IFeatureClass CreateShpFile(string strPath, string strName)
        {
            ISpatialReference spatialRef = (this.Topology as IGeoDataset).SpatialReference;
            IFeatureClass fClassDest = CreateShpFile(strPath, strName, "错误记录", spatialRef);
            AddFields(fClassDest);

            return fClassDest;
        }

        private List<string> m_FieldCaptions = new List<string>{
            "检查类型",
            "图层名",
            "标识码",
            "图层2",
            "标识码2",
            "拓扑图层名",
            "错误描述", 
            "是否例外",
            "说明",
            "规则编码"
    };
        private bool InsertTopoError(IFeatureClass destFClass)
        {
            try
            {
                string strSQL = @"SELECT
                            b.CheckType,
                            IIF(b.TargetFeatClass1 is Null,'',b.TargetFeatClass1) as YSTC,
                            IIF(a.SourceBSM is Null,'',a.SourceBSM) as SourceBSM,
                            IIF(a.MBTC is Null,'',a.MBTC) as MBTC,
                            IIF(a.TargetBSM is Null,'',a.TargetBSM) as BSM2,
                            a.TPTC as TopoLayerName,
                            a.Reason as Description,
                            a.IsException as IsException,
                            IIf(a.Remark is Null,'',a.Remark) as Remark,
                            b.GZBM ,
                            a.ArcGisRule as ArcGisRule,
                            a.JHLX as JHLX,
                            a.SourceLayerID,
                            a.TargetLayerID,
                            a.SourceOID as OID,
                            a.TargetOID as OID2
                            from LR_ResAutoTopo as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID
                            ";


                DataTable dtError = Common.Utility.Data.AdoDbHelper.GetDataTable(this.ResultConnection, strSQL);
                
                IFeatureCursor fCusorInsert = destFClass.Insert(false);
                Dictionary<int, int> dictFieldIndex = new Dictionary<int, int>();
                for (int i = 0; i < m_FieldCaptions.Count; i++)
                {
                    dictFieldIndex.Add(i,destFClass.FindField(m_FieldCaptions[i]));
                }
                int xFieldIndex = destFClass.FindField("X坐标");
                int yFieldIndex = destFClass.FindField("Y坐标");

                IErrorFeatureContainer errFeatureContainer = this.Topology as IErrorFeatureContainer;
                ISpatialReference spatialRef = (this.Topology as IGeoDataset).SpatialReference;
                for (int i = 0; i < dtError.Rows.Count; i++)
                {
                    DataRow rowError = dtError.Rows[i];
                    int fClassID = Convert.ToInt32(rowError["SourceLayerID"]);
                    int fClassID2 = Convert.ToInt32(rowError["TargetLayerID"]);
                    int oid = Convert.ToInt32(rowError["OID"]);
                    int oid2 = Convert.ToInt32(rowError["OID2"]);
                    esriGeometryType geoType = (esriGeometryType)Convert.ToInt32(rowError["JHLX"]);
                    esriTopologyRuleType ruleType = (esriTopologyRuleType)Convert.ToInt32(rowError["ArcGISRule"]);

                    IFeature srcFeature = errFeatureContainer.get_ErrorFeature(spatialRef, ruleType, geoType, fClassID, fClassID2, oid, oid2) as IFeature;

                    IFeatureBuffer fNew = destFClass.CreateFeatureBuffer();
                    for (int j = 0; j < m_FieldCaptions.Count; j++)
                    {
                        int fIndex = dictFieldIndex[j];
                        if (fIndex < 0)
                            continue;

                        fNew.set_Value(fIndex, rowError[j]);
                    }
                    fNew.Shape = GetErrorGeometry(srcFeature);
                    IPoint point = fNew.Shape as IPoint;
                    fNew.set_Value(xFieldIndex, point.X);
                    fNew.set_Value(yFieldIndex, point.Y);

                    fCusorInsert.InsertFeature(fNew);

                    if (i % 2000 == 0)
                        fCusorInsert.Flush();

                }

                fCusorInsert.Flush();

                return true;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                return false;
            }
        }


        private bool InsertNomarlError(IFeatureClass destFClass)
        {
            try
            {
                string strSQL = @"
                            SELECT
                            b.CheckType,
                            a.TargetFeatClass1 as YSTC,
                            a.BSM as SourceBSM,
                            a.TargetFeatClass2 as MBTC,
                            a.BSM2 as BSM2,
                            '' as TopoLayerName,
                            a.ErrMsg as Description,
                            IIF(a.IsException,1,0) as IsException,
                            IIf(a.Remark is Null,'',a.Remark) as Remark,
                            b.GZBM,
                            a.OID as OID
                            
                            from LR_ResAutoAttr as a, LR_ResultEntryRule as b where a.RuleInstID=b.RuleInstID";


                DataTable dtError = Common.Utility.Data.AdoDbHelper.GetDataTable(this.ResultConnection, strSQL);
                IFeatureCursor fCusorInsert = destFClass.Insert(false);
                Dictionary<string, IFeatureClass> dictFeatureClass = new Dictionary<string, IFeatureClass>();
                Dictionary<int, int> dictFieldIndex = new Dictionary<int, int>();
                for (int i = 0; i < m_FieldCaptions.Count; i++)
                {
                    dictFieldIndex.Add(i,destFClass.FindField(m_FieldCaptions[i]));
                }
                int xFieldIndex = destFClass.FindField("X坐标");
                int yFieldIndex = destFClass.FindField("Y坐标");

                for (int i = 0; i < dtError.Rows.Count; i++)
                {
                    DataRow rowError = dtError.Rows[i];
                    IFeatureClass curFClass;
                    string strFClassAlias = rowError["YSTC"] as string;
                    if (!dictFeatureClass.ContainsKey(strFClassAlias))
                    {
                        int standardID = SysDbHelper.GetStandardIDBySchemaID(this.SchemaID);
                        string strFClass = LayerReader.GetNameByAliasName(strFClassAlias, standardID);
                        IFeatureClass fClass = (this.BaseWorkspace as IFeatureWorkspace).OpenFeatureClass(strFClass);
                        dictFeatureClass.Add(strFClassAlias, fClass);
                        curFClass = fClass;
                    }
                    else
                    {
                        curFClass = dictFeatureClass[strFClassAlias];
                    }

                    if (curFClass == null)
                        continue;

                    object objOID = rowError["OID"];
                    if (objOID == null)
                        continue;

                    int oid = Convert.ToInt32(objOID);
                    IFeature srcFeature = curFClass.GetFeature(oid);
                    if (srcFeature == null)
                        continue;

                    IFeatureBuffer fNew = destFClass.CreateFeatureBuffer();
                    for (int j = 0; j < m_FieldCaptions.Count; j++)
                    {
                        int fIndex = dictFieldIndex[j];
                        if (fIndex < 0)
                            continue;

                        fNew.set_Value(fIndex, rowError[j]);
                    }
                    fNew.Shape = GetErrorGeometry(srcFeature);
                    IPoint point = fNew.Shape as IPoint;
                    fNew.set_Value(xFieldIndex, point.X);
                    fNew.set_Value(yFieldIndex, point.Y);

                    fCusorInsert.InsertFeature(fNew);

                    if (i % 2000 == 0)
                        fCusorInsert.Flush();
                }

                fCusorInsert.Flush();

                return true;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
        }

        /// <summary>
        /// 创建shape图层
        /// </summary>
        /// <param name="strShpPath"></param>
        /// <param name="strFtName"></param>
        /// <returns></returns>
        private IFeatureClass CreateShpFile(string strShpPath, string strFtName, string strAliasFtName, ISpatialReference pSpatial)
        {
            string connectionstring = "DATABASE=" + strShpPath;
            IWorkspaceFactory2 pFactory = (IWorkspaceFactory2)new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
            IWorkspace workspace = pFactory.OpenFromString(connectionstring, 0);
            IFeatureWorkspace ipFtWs = (IFeatureWorkspace)workspace;

            //创建字段IFields
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            ///创建几何类型字段
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;

            ////设置FID字段
            //IFieldEdit ipFldEdit = new FieldClass(); //(__uuidof(Field));
            //ipFldEdit.Name_2 = "FID";
            //ipFldEdit.AliasName_2 = "唯一标志码";
            //ipFldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            //pFieldsEdit.AddField(ipFldEdit);


            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.AliasName_2 = "几何类型";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;


            IGeometryDef pGeomDef = new GeometryDefClass();
            IGeometryDefEdit pGeomDefEdit = (IGeometryDefEdit)pGeomDef;
            pGeomDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            pGeomDefEdit.SpatialReference_2 = pSpatial;
            pFieldEdit.GeometryDef_2 = pGeomDef;
            pFieldsEdit.AddField(pField);


            IFeatureClass _featureClass =
                ipFtWs.CreateFeatureClass(strFtName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            //更新图层别名
            //IClassSchemaEdit ipEdit = (IClassSchemaEdit)_featureClass;
            //ipEdit.AlterAliasName(strAliasFtName);

            pFactory = null;
            workspace = null;
            ipFtWs = null;

            return _featureClass;
        }

        /// <summary>
        /// 给图层添加字段
        /// </summary>
        /// <param name="pFtCls"></param>
        private void AddFields(IFeatureClass pFtCls)
        {
            try
            {
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Name_2 = "检查类型";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Name_2 = "图层名";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Name_2 = "字段名称";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Name_2 = "标识码";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Name_2 = "图层2";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Name_2 = "标识码2";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 50;
                pFieldEdit.Name_2 = "拓扑图层名";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 255;
                pFieldEdit.Name_2 = "错误描述";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);


                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 50;
                pFieldEdit.Name_2 = "是否例外";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 255;
                pFieldEdit.Name_2 = "说明";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 255;
                pFieldEdit.Name_2 = "规则编码";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFtCls.AddField(pField);


                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 50;
                pFieldEdit.Name_2 = "X坐标";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFtCls.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = 50;
                pFieldEdit.Name_2 = "Y坐标";
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = false;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFtCls.AddField(pField);

            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
            }
        }

        /// <summary>
        /// 将点线面要素都转换为点要素，并返回点要素的IGeometry
        /// </summary>
        /// <param name="pFeature">要素</param>
        /// <returns></returns>
        private IGeometry GetErrorGeometry(IFeature pFeature)
        {
            IGeometry pGeo = null;
            esriGeometryType type = pFeature.Shape.GeometryType;
            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    pGeo = pFeature.ShapeCopy;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    {
                        IPolygon pPolygon = (IPolygon)pFeature.Shape;
                        IArea pArea = (IArea)pPolygon;
                        IPoint pPoint = pArea.Centroid;
                        pGeo = (IGeometry)pPoint;
                        break;
                    }
                case esriGeometryType.esriGeometryPolyline:
                    {
                        IPolyline pPolyLine = (IPolyline)pFeature.Shape;
                        IPoint pPoint = GetMidPoint(pPolyLine);
                        pGeo = (IGeometry)pPoint;
                        break;
                    }
            }
            return pGeo;
        }

        /// <summary>
        /// 获取指定线的中心点
        /// </summary>
        /// <param name="pPolyline">线图形</param>
        /// <returns>返回线图形的中新点坐标</returns>
        private IPoint GetMidPoint(IPolyline pPolyline)
        {
            IPoint pPoint = null;

            if (pPolyline == null) return null;

            IPointCollection pPoints = pPolyline as IPointCollection;

            long lPointCount;
            //记录线物点个数
            lPointCount = pPoints.PointCount;

            long nMid = (lPointCount - 1) / 2 + 1;
            pPoint = pPoints.get_Point((int)nMid);
            return pPoint;
        }
    }

    public delegate void ErrorExported();
}
