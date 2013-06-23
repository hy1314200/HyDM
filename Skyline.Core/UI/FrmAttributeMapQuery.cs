using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Collections;
using TerraExplorerX;
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using ESRI.ArcGIS.DataSourcesFile;
using Skyline.Core.Helper;

namespace Skyline.Core.UI
{
    public partial class FrmAttributeMapQuery : FrmBase
    {
        private IFeatureGroup61 pf;
        private string ConnString = ADODBHelper.ConfigConnectionString;
        private ADODBHelper m_OracleHelper;
        private SqlHelper m_SqlHelper;
        private DataSet ds;
        private IFeatureClass tFeatureClass;
        private string ModelID = ConfigurationManager.AppSettings["ModelID"];
        public FrmAttributeMapQuery(Form FrmMain)
        {
            base.BeginForm(FrmMain);
            InitializeComponent();

        }

        /// <summary>
        /// 窗口数据的初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAttributeMapQuery_Load(object sender, EventArgs e)
        {
            try
            {
                gridView1.OptionsView.ColumnAutoWidth = false;


                IFeatureWorkspace pFeatureWorkspace = Program.pWorkspace as IFeatureWorkspace;
                this.tFeatureClass = pFeatureWorkspace.OpenFeatureClass(ConfigurationManager.AppSettings["ModleTable"]);


                ILayer61 _pLayer61;
                int layerid = Program.sgworld.ProjectTree.FindItem(ConfigurationManager.AppSettings["LayerPath"]);
                _pLayer61 = Program.sgworld.ProjectTree.GetLayer(layerid);
                IFeatureGroups61 pfGroups = _pLayer61.FeatureGroups;
                this.pf = pfGroups.Point;

                this.m_OracleHelper = new ADODBHelper(ConnString, true);
                DataSet ds = new DataSet();
                ds = this.m_OracleHelper.OpenDS("select t.* from FILE3DATTRIBUTE t");
                //m_SqlHelper = new SqlHelper();
                //ds = new DataSet();
                //ds = m_SqlHelper.selectTableAll("builderObject");
                this.gridControl1.DataSource = ds.Tables[0];
                foreach (DevExpress.XtraGrid.Columns.GridColumn item in this.gridView1.Columns)
                {
                    string Asql = "select t.fieldalias from ATTR3DFIELDMAPPING t where t.fieldname ='" + item.FieldName + "'";
                    try
                    {
                        item.Caption = this.m_OracleHelper.ExecSQLScalar(Asql).ToString();
                    }
                    catch (Exception ex)
                    {

                        continue;
                    }

                }
                this.gridView1.BestFitColumns();
                // this.gridView1.FocusedRowHandle = 3;
                Program.TE.OnLButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            }
            catch (Exception ex)
            {
                MessageBox.Show("没有可查询图层！","SUNZ",MessageBoxButtons.OK,MessageBoxIcon.Information);
               /// throw;
            }
           
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 左键单击事件
        /// 获取建筑物信息
        /// </summary>
        /// <param name="Flags"></param>
        /// <param name="X"></param>mo
        /// <param name="Y"></param>
        /// <param name="pbHandled"></param>
        void TE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            try
            {
                object longitude, latitude, objectID, height, objType;
                objType = 1;
                Program.pRender.ScreenToWorld(X, Y, ref objType, out longitude, out height, out latitude, out objectID);
                if (objectID.ToString() == "")
                {
                    objType = 16;
                    Program.pRender.ScreenToWorld(X, Y, ref objType, out longitude, out height, out latitude, out objectID);
                }
                IFeature61 pf61 = pf.GetFeature(objectID.ToString());

                string fid = pf61.FeatureAttributes.GetFeatureAttribute(this.ModelID).Value.ToString();
                // Program.TE.ScreenToWorld(X, Y, ref objType, out longitude, out height, out latitude, out objectID);
                this.ShowBuilderInfo(fid);
                this.xtraTabControl1.SelectedTabPageIndex = 1;

            }
            catch (Exception ex)
            {
                
                
            }
           
           
        }
        /// <summary>
        /// 绘制方法  绘制信息到pan_builderinfo
        /// </summary>
        /// <param name="list"></param>
        private void DrawBuilderInfo(List<BuilderObject> list)
        {
            try
            {
                pan_builderInfo.Refresh();
                Graphics grap = Graphics.FromHwnd(pan_builderInfo.Handle);
                Font font = new Font("微软雅黑", 9, FontStyle.Bold);
                Brush brush = Brushes.Navy;

                if (list.Count == 0)
                {
                    grap.DrawString("点选目标不是物体", new Font("微软雅黑", 15, FontStyle.Bold), Brushes.Red, new PointF(113.0F, 98.0F));
                    grap.DrawString("或没有相关物体资料", new Font("微软雅黑", 15, FontStyle.Bold), Brushes.Red, new PointF(103.0F, 130.0F));
                }
                else
                {
                    //标题
                    grap.DrawString("物体名称：", font, brush, new PointF(21.0F, 29.0F));
                    grap.DrawString("物体高度：", font, brush, new PointF(21.0F, 65.0F));
                    grap.DrawString("物体面积：", font, brush, new PointF(21.0F, 98.0F));
                    grap.DrawString("物体说明：", font, brush, new PointF(21.0F, 135.0F));
                    grap.DrawString("物体经度：", font, brush, new PointF(21.0F, 258.0F));
                    grap.DrawString("物体纬度：", font, brush, new PointF(21.0F, 289.0F));

                    //内容
                    grap.DrawString(list[0].BuilName, font, brush, new PointF(103.0F, 29.0F));
                    grap.DrawString(list[0].BuilHeight + "   米（M）", font, brush, new PointF(103.0F, 65.0F));
                    grap.DrawString(list[0].BuilArea + "   平方米（M2）", font, brush, new PointF(103.0F, 98.0F));
                    grap.DrawString(list[0].BuilLongitude, font, brush, new PointF(103.0F, 258.0F));
                    grap.DrawString(list[0].BuilLatitude, font, brush, new PointF(103.0F, 289.0F));


                    if (list[0].BuilInfo != "")
                    {
                        string[] s = this.SplitBuilInfo(list[0].BuilInfo);
                        for (int i = 0; i < s.Length; i++)
                        {
                            grap.DrawString(s[i], font, brush, new PointF(103.0F, 135.0F + (float)(i * 19)));
                        }
                    }
                }
            }
            catch (Exception)
            {
                
               
            }
            
        }
        /// <summary>
        /// 将查询出的信息绘制到Panel控件上
        /// </summary>
        /// <param name="list"></param>
        private void DrawModelInfo(List<File3dattribute> list)
        {
            pan_builderInfo.Refresh();
            Graphics grap = Graphics.FromHwnd(pan_builderInfo.Handle);
            Font font = new Font("微软雅黑", 9, FontStyle.Bold);
            Brush brush = Brushes.Navy;

            if (list.Count == 0)
            {
                grap.DrawString("点选目标不是模型", new Font("微软雅黑", 15, FontStyle.Bold), Brushes.Red, new PointF(113.0F, 98.0F));
                grap.DrawString("或没有相关物体资料", new Font("微软雅黑", 15, FontStyle.Bold), Brushes.Red, new PointF(103.0F, 130.0F));
            }
            else
            {
                //标题
                grap.DrawString("建筑名称：", font, brush, new PointF(21.0F, 29.0F));
                grap.DrawString("联系人  ：", font, brush, new PointF(21.0F, 65.0F));
                grap.DrawString("联系电话：", font, brush, new PointF(21.0F, 98.0F));
                grap.DrawString("详细地址：", font, brush, new PointF(21.0F, 135.0F));
                grap.DrawString("街路巷号：", font, brush, new PointF(21.0F, 258.0F));
                grap.DrawString("更新时间：", font, brush, new PointF(21.0F, 289.0F));

                //内容
                grap.DrawString(list[0].Mc, font, brush, new PointF(103.0F, 29.0F));
                grap.DrawString(list[0].Lxr, font, brush, new PointF(103.0F, 65.0F));
                grap.DrawString(list[0].Lxdh, font, brush, new PointF(103.0F, 98.0F));
                grap.DrawString(list[0].Xxdz, font, brush, new PointF(103.0F, 135.0F));
                grap.DrawString(list[0].Jlxh, font, brush, new PointF(103.0F, 258.0F));
                grap.DrawString(list[0].Gxsj, font, brush, new PointF(103.0F, 289.0F));


                //if (list[0].BuilInfo != "")
                //{
                //    string[] s = this.SplitBuilInfo(list[0].BuilInfo);
                //    for (int i = 0; i < s.Length; i++)
                //    {
                //        grap.DrawString(s[i], font, brush, new PointF(103.0F, 135.0F + (float)(i * 19)));
                //    }
                //}


            }
        }
        /// <summary>
        /// 对builInfo属性分组 用于绘制显示
        /// </summary>
        /// <returns></returns>
        private string[] SplitBuilInfo(string builInfo)
        {
            //字符串长
            int count = builInfo.Length;
            if (count != 0)
            {
                //数组要分组数
                int arrCon = count / 21;
                //最后一组余数
                int arrCon_y = count % 21;

                if (arrCon != 0 && arrCon_y != 0)
                {
                    string[] s = new string[arrCon + 1];
                    for (int i = 0; i < arrCon; i++)
                    {
                        s[i] = builInfo.Substring(i * 21, 21);
                    }

                    s[arrCon] = builInfo.Substring(arrCon * 21, arrCon_y);

                    return s;
                }
                else
                {
                    if (arrCon != 0)
                    {
                        string[] s = new string[arrCon];
                        for (int i = 0; i < arrCon; i++)
                        {
                            s[i] = builInfo.Substring(i * 21, 21);
                        }

                        return s;
                    }
                    else
                    {
                        if (arrCon == 0 && arrCon_y != 0)
                        {
                            string[] s = new string[1];
                            s[0] = builInfo;
                            return s;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 显示物体信息
        /// </summary>
        /// <param name="objectID"></param>
        private void ShowBuilderInfo(string objectID)
        {
            try
            {
                BuilderObjectBiz bobTemp = new BuilderObjectBiz();
                List<File3dattribute> list = bobTemp.GetModelInfo(objectID);
                // List<BuilderObject> list = bobTemp.GetBuilderInfo(objectID);
                this.DrawModelInfo(list);
            }
            catch (Exception)
            {
            
            }
            
        }
        private void FrmAttributeMapQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.TE.OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string FID = this.gridView1.GetFocusedRowCellValue("OBJECTID").ToString();
                IQueryFilter pQuery = new QueryFilterClass();

                pQuery.WhereClause = String.Format("{0} = {1}", this.ModelID, FID);
                IFeatureCursor featureCursor = this.tFeatureClass.Search(pQuery, true);
                IFeature esriFeature = featureCursor.NextFeature();
                ESRI.ArcGIS.Geometry.IPoint pPoint = esriFeature.Shape as ESRI.ArcGIS.Geometry.IPoint;
                IPosition61 _Position6 = Program.pCreator6.CreatePosition(pPoint.X, pPoint.Y, 100, AltitudeTypeCode.ATC_TERRAIN_RELATIVE, 0, -89, 0, 100);
                Program.pNavigate6.FlyTo(_Position6, ActionCode.AC_FLYTO);
                //double x = 0;
                //double y = 0;
                //try
                //{
                //     x = Convert.ToDouble(this.gridView1.GetFocusedRowCellDisplayText(gridColumn8));
                //     y = Convert.ToDouble(this.gridView1.GetFocusedRowCellDisplayText(gridColumn9));
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("缺失坐标信息！","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //}


                //int FeatureCount = pf.Count;
                //int FCount = this.pf.Count;
                //IFeature61 pfeature = null;
                //for (int i = 0; i < FCount; i++)
                //{
                //    pfeature = this.pf[i] as IFeature61;
                //    string fid = pfeature.FeatureAttributes.GetFeatureAttribute("FID").Value.ToString();
                //    if (fid==FID)
                //    {
                //        break;
                //    }
                //    pfeature = null;
                //}
                //if (pfeature!=null)
                //{
                //    Program.sgworld.Navigate.FlyTo(pfeature, ActionCode.AC_FLYTO);
                //}

                // this.ShowBuilderInfo(ObjectID.ToString());
                //  this.xtraTabControl1.SelectedTabPageIndex = 1;
            }
            catch (Exception ex)
            {
                
               // throw;
            }
            
            
        }

        private void Close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                string ObjectID = this.gridView1.GetFocusedRowCellValue("OBJECTID").ToString();
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    this.ShowBuilderInfo(ObjectID.ToString());
                }
            }
            catch (Exception ex)
            {

            }
           
        }

    }
}