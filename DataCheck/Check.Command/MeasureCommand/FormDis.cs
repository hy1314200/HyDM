using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
//using Measure.Tool;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace Check.Command.MeasureCommand
{
    /// <summary>
    /// 测量类型，Length长度，Area面积
    /// </summary>
    public enum MeasureType
    {
        Length,
        Area
    };

    public partial class FormDis : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 参数
        /// </summary>
        public MeasureType m_MeasureType;  //测量类型
        public ITool m_Tool;               //传过来的axMapControl的ITool实体

        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="type">测量类型</param>
        /// <param name="ipTool">传过来的axMapControl的ITool实体</param>
        public FormDis(MeasureType type,ITool ipTool)
        {
            InitializeComponent();
            this.m_MeasureType = type;
            this.m_Tool = ipTool;
            this.TopMost = true;
            this.WriteLabelText(null);

            if (this.m_Tool.GetType() == typeof(ToolMeasureLength))
            {
                (this.m_Tool as ToolMeasureLength).MyInit();
            }
            if (this.m_Tool.GetType() == typeof(ToolMeasureArea))
            {
                (this.m_Tool as ToolMeasureArea).MyInit();
            }
        }

        /// <summary>
        /// 算出量测计算结果，并在FormDis上进行显示
        /// </summary>
        /// <param name="ipGeo">人机交互生成的实体</param>
        public void WriteLabelText(IGeometry ipGeo)
        {
            switch(m_MeasureType)
            {
                case MeasureType.Length:          //量测长度
                    {
                        string strUnit = " " + (this.m_Tool as ToolMeasureLength).m_hookHelper.FocusMap.MapUnits.ToString().Substring(4);
                        //this.m_LabelMeasureType.Text = "Line Measurement";
                        this.m_LabelMeasureType.Text = "长度量测";
                        this.m_labelArea.Visible = true;
                        if(ipGeo!=null)
                        {
                            IGeometryCollection ipGeoCol = ipGeo as IGeometryCollection;
                            ISegmentCollection ipSegmentColl = ipGeoCol.get_Geometry(0) as ISegmentCollection;
                            ILine ipLine = ipSegmentColl.get_Segment(ipSegmentColl.SegmentCount-1) as ILine;
                            //this.m_labelSegment.Text = "段长度:" + ipLine.Length.ToString() + strUnit;
                            //this.m_labelLength.Text = "长度:" + (ipGeoCol.get_Geometry(0) as ICurve).Length.ToString() + "米";
                            this.m_labelArea.Text = "长度:" + (ipGeoCol.get_Geometry(0) as ICurve).Length.ToString("f3") + "米";
                        }
                        else
                        {
                            this.m_labelArea.Text = "长度:" + "0" + "米";
                            //this.m_labelLength.Text = "总长度:" + "0" + strUnit;
                        }
                    }
                    break;
                case MeasureType.Area:  //量测面积
                    {
                        string strUnit = " " + (this.m_Tool as ToolMeasureArea).m_hookHelper.FocusMap.MapUnits.ToString().Substring(4);
                        //this.m_LabelMeasureType.Text = "Area Measurement";
                        this.m_LabelMeasureType.Text = "面积量测";
                        this.m_labelArea.Visible = true;
                        if (ipGeo != null)
                        {
                            IGeometryCollection ipGeoCol = ipGeo as IGeometryCollection;
                            ISegmentCollection ipSegmentColl = ipGeoCol.get_Geometry(0) as ISegmentCollection;
                            ILine ipLine = ipSegmentColl.get_Segment(ipSegmentColl.SegmentCount-1) as ILine;
                            //this.m_labelSegment.Text = "Segment:" + ipLine.Length.ToString() + strUnit;
                            //this.m_labelLength.Text = "Perimeter:" + (ipGeoCol.get_Geometry(0) as IRing).Length.ToString() + strUnit;
                            //this.m_labelSegment.Text = "段长度:" + ipLine.Length.ToString() + strUnit;
                            //this.m_labelLength.Text = "周长:" + (ipGeoCol.get_Geometry(0) as IRing).Length.ToString() + strUnit;
                            
                            IClone ipClone = ipGeo as IClone;
                            IGeometry ipGeo1 = ipClone.Clone() as IGeometry;
                            ITopologicalOperator ipTopo = ipGeo1 as ITopologicalOperator;
                            ipTopo.Simplify();

                            this.m_labelArea.Text = "面积:" + ((ipGeo1 as IPolygon) as IArea).Area.ToString(".###") + "平方米";
                        }
                        else
                        {
                            this.m_labelArea.Text = "面积:" + "0" + "平方米";                            
                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// 关闭窗口，并清空相应参数：m_Element，m_GeoMeasure，m_FormDis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDis_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
            if (this.m_Tool.GetType() == typeof(ToolMeasureLength))
            {
                IElement m_Element = (this.m_Tool as ToolMeasureLength).m_Element;
                if (m_Element != null)
                {
                    (this.m_Tool as ToolMeasureLength).m_hookHelper.ActiveView.GraphicsContainer.DeleteElement(m_Element);
                    m_Element = null;
                }
                (this.m_Tool as ToolMeasureLength).m_hookHelper.ActiveView.Refresh();
            }
            if (this.m_Tool.GetType() == typeof(ToolMeasureArea))
            {
                IElement m_Element = (this.m_Tool as ToolMeasureArea).m_Element;
                if (m_Element != null)
                {
                    (this.m_Tool as ToolMeasureArea).m_hookHelper.ActiveView.GraphicsContainer.DeleteElement(m_Element);
                    m_Element = null;
                }
                (this.m_Tool as ToolMeasureArea).m_hookHelper.ActiveView.Refresh();
            }*/

            if (this.m_Tool.GetType() == typeof(ToolMeasureLength))
            {
                (this.m_Tool as ToolMeasureLength).m_GeoMeasure = null;
                (this.m_Tool as ToolMeasureLength).m_FormDis = null;
                (this.m_Tool as ToolMeasureLength).m_Element = null;
            }
            if (this.m_Tool.GetType() == typeof(ToolMeasureArea))
            {
                (this.m_Tool as ToolMeasureArea).m_GeoMeasure = null;
                (this.m_Tool as ToolMeasureArea).m_FormDis = null;
                (this.m_Tool as ToolMeasureArea).m_Element = null;
            }
            
        }

        /// <summary>
        /// 加入的定时器相关操作，用人机交互产生的图形在FormDis上显示结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_timer_Tick(object sender, EventArgs e)
        {
            IGeometry ipGeo = (this.m_Tool as ToolMeasureArea).m_Element.Geometry;
            if (this.m_Tool.GetType() == typeof(ToolMeasureArea))
            {
                if (ipGeo != null)
                {
                    this.WriteLabelText(ipGeo);
                }
            }
            
        }
    }
}