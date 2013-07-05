using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System.Reflection;

namespace Hy.Check.Command.MeasureCommand
{
    /// <summary>
    /// Summary description for ToolMeasureLength.
    /// </summary>
    [Guid("d082f72c-fd81-444c-8625-8e2e2c884344")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Hy.Check.Command.MeasureCommand.ToolMeasureLength")]
    public sealed class ToolMeasureLength : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        public IHookHelper m_hookHelper;

        public IGeometry m_GeoMeasure; //记录量测中鼠标按下的点
        public IElement m_Element; //存放显示图形的IElement
        private ILineSymbol m_LineSymbol;//显示的符号
        public FormDis m_FormDis;//计算并显示测量结果的对话框


        public ToolMeasureLength()
        {
            //
            // TODO: Define values for the public properties
            //
            //base.m_category = ""; //localizable text 
            //base.m_caption = "";  //localizable text 
            //base.m_message = "";  //localizable text
            //base.m_toolTip = "";  //localizable text
            //base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            base.m_category = "疑问地物"; //localizable text
            base.m_caption = "长度测量"; //localizable text
            base.m_message = "长度测量"; //localizable text 
            base.m_toolTip = "在地图窗口单击鼠标左键，开始量算；双击鼠标左键或单击鼠标右键，结束量算"; //localizable text 
            base.m_name = "长度测量"; //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            try
            {


                Assembly thisExe = Assembly.GetExecutingAssembly();

                base.m_cursor =
                    new Cursor(thisExe.GetManifestResourceStream("Hy.Check.Command.Resources.ToolMeasureLength.cur"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add ToolMeasureLength.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolMeasureLength.OnClick implementation
        }

        //声明INewLineFeedback 显示轨迹
        private INewLineFeedback m_pNewLineFeed;
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            //增加单击右键结束量测 田晶添加20081028
            if (Button == 2)
            {
                OnDblClick();
                return;
            }

            IActiveView ipAV = this.m_hookHelper.ActiveView;

            //生成新点
            IPoint ipPt = new PointClass();
            ipPt = ipAV.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            SnapPoint(ipPt);
            //结果显示窗口
            //if (this.m_FormDis == null)
            //{
            //    this.m_FormDis = new FormDis(MeasureType.Length, this);
            //    this.m_FormDis.TopMost = true;
            //    this.m_FormDis.Show();
            //    this.m_FormDis.WriteLabelText(null);
            //} 

            if (this.m_GeoMeasure == null)
            {
                m_pNewLineFeed = new NewLineFeedbackClass();
                IScreenDisplay pScreen = ipAV.ScreenDisplay;
                m_pNewLineFeed.Display = pScreen;
                m_pNewLineFeed.Start(ipPt);

                this.m_GeoMeasure = new PolylineClass();

            }
            else
            {
                m_pNewLineFeed.AddPoint(ipPt);
            }

            ////生成实体
            //if (this.m_GeoMeasure == null)
            //{
            //    this.m_GeoMeasure = new PolylineClass();
            //    ISegmentCollection ipSegCol = this.m_GeoMeasure as ISegmentCollection;
            //    ILine ipLine = new LineClass();
            //    ipLine.PutCoords(ipPt, ipPt);
            //    object obj = Type.Missing;
            //    ipSegCol.AddSegment(ipLine as ISegment, ref obj, ref obj);
            //}
            //else
            //{
            //    ISegmentCollection ipSegCol = this.m_GeoMeasure as ISegmentCollection;
            //    ILine ipLine = new LineClass();
            //    ipLine.PutCoords(ipSegCol.get_Segment(ipSegCol.SegmentCount - 1).ToPoint, ipPt);
            //    object obj = Type.Missing;
            //    ipSegCol.AddSegment(ipLine as ISegment, ref obj, ref obj);
            //}

            ////////////////////显示/////////////////////
            //this.m_Element.Geometry = this.m_GeoMeasure;
            ////局部刷新
            //ipAV.PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.m_GeoMeasure, null);
            ////ipAV.Refresh();

            ////ESRI.ArcGIS.esriSystem.IClone ipClone = this.m_GeoMeasure as ESRI.ArcGIS.esriSystem.IClone;
            ////IGeometry ipGeoCopy = ipClone.Clone() as IGeometry;
            ////this.m_FormDis.WriteLabelText(ipGeoCopy);
        }


        /// <summary>
        /// 捕捉到捕捉点
        /// </summary>
        /// <param name="pSnapPnt"></param>
        /// <returns></returns>
        public bool SnapPoint(IPoint pSnapPnt)
        {
            IEngineSnapEnvironment pSnapEnv = new EngineEditorClass();
            return pSnapEnv.SnapPoint(pSnapPnt);
        }


        /// <summary>
        /// 鼠标移动，在此动态在m_GeoMeasure中加入鼠标现在的点进行动态量测
        /// </summary>
        /// <param name="Button"></param>
        /// <param name="Shift"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolMeasureLength.OnMouseMove implementation
            IActiveView ipAV = this.m_hookHelper.ActiveView;
            ///////////生成新点///////////////
            IPoint ipPt = new PointClass();
            ipPt = ipAV.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            SnapPoint(ipPt);

            if (m_pNewLineFeed != null)
            {
                m_pNewLineFeed.MoveTo(ipPt);
            }

            /*
            /////////////////生成图形////////////////
            if ((this.m_GeoMeasure as IGeometryCollection).GeometryCount == 0)
            {
                return;
            }
            IClone ipClone = this.m_GeoMeasure as IClone;
            IGeometry ipGeo = ipClone.Clone() as IGeometry;
            ISegmentCollection ipSegCol = ipGeo as ISegmentCollection;
            ILine ipLine = new LineClass();
            ipLine.PutCoords(ipSegCol.get_Segment(ipSegCol.SegmentCount - 1).ToPoint, ipPt);
            object obj = Type.Missing;
            ipSegCol.AddSegment(ipLine as ISegment, ref obj, ref obj);

            ////////显示/////////////
            this.m_Element.Geometry = ipGeo;
            //局部刷新
            ipAV.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ipGeo, null);
            //ipAV.Refresh();

            this.m_FormDis.WriteLabelText(ipGeo);
             */
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode.Equals(27))
            {
                m_GeoMeasure = null;
                m_pNewLineFeed = null;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, m_hookHelper.ActiveView.Extent);
            }
        }

        /// <summary>
        /// 鼠标双击，结束本次量测，进行下一次量测
        /// </summary>
        public override void OnDblClick()
        {
            //base.OnDblClick();
            /*
            this.m_GeoMeasure = null;
            this.m_Element.Geometry = new PolylineClass();
            this.m_hookHelper.ActiveView.Refresh();
            this.m_FormDis.WriteLabelText(this.m_GeoMeasure);
            */
            //this.m_FormDis.WriteLabelText(null);

            if (this.m_GeoMeasure == null)
                return;

            //结果显示窗口
            if (this.m_FormDis == null)
            {
                this.m_FormDis = new FormDis(MeasureType.Length, this);
                this.m_FormDis.TopMost = true;
                //窗口显示位置
                this.m_FormDis.Location = new System.Drawing.Point(130, 180);
                this.m_FormDis.Show();
            }
            this.m_GeoMeasure = m_pNewLineFeed.Stop();
            this.m_FormDis.WriteLabelText(this.m_GeoMeasure);

            this.m_GeoMeasure = null;

        }
        /// <summary>
        /// 初始化信息，创建IElement实体存放人机交互产生的图形，并设定其显示符号
        /// </summary>
        public void MyInit()
        {
            //线显示类型
            this.m_LineSymbol = new SimpleLineSymbolClass();
            IRgbColor ipColor = new RgbColor();
            ipColor.Red = 0;
            ipColor.Green = 255;
            ipColor.Blue = 0;
            this.m_LineSymbol.Color = ipColor;
            this.m_LineSymbol.Width = 2;
            //加入实体
            IActiveView ipAV = this.m_hookHelper.ActiveView;
            IGraphicsContainer ipGraphicContainer = ipAV.FocusMap as IGraphicsContainer;
            ILineElement ipLineElement = new LineElementClass();
            ipLineElement.Symbol = this.m_LineSymbol;
            IElement ipElement = ipLineElement as IElement;
            ipElement.Geometry = new PolylineClass();
            this.m_Element = ipElement;
            ipGraphicContainer.AddElement(ipElement, 0);
        }

        public override bool Enabled
        {
            get
            {
                return (m_hookHelper.FocusMap.LayerCount > 0);
            }
        }
        #endregion
    }
}

