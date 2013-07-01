using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Point=System.Drawing.Point;

namespace Check.UI.UC
{
    public delegate void ScaleUpdated(double scale);

    public class UCMap : AxMapControl
    {
        public AxMapControl pMapControlYY = null;
        private UCMapNavigate ucMapNavigate1;
        private AxLicenseControl axLicenseControl1;


        public UCMap()
            : base()
        {
            InitializeComponent();

            //InitScale();
            base.OnExtentUpdated += new IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(UCMap_OnExtentUpdated);
            base.OnMouseDown += UCMap_MouseDown;
        }

        public ILayer this[string name]
        {
            get
            {
                for (int i = 0; i < LayerCount; i++)
                {
                    if (get_Layer(i).Name == name)
                        return get_Layer(i);
                }
                return null;
            }
        }

        private ScaleUpdated scaleUpdated;

        public ScaleUpdated ScaleUpdated
        {
            set { scaleUpdated = value; }
        }

        private void UCMap_MouseDown(object sender,IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button==2)
                Pan();
        }

        private void UCMap_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            if (scaleUpdated != null)
                scaleUpdated(MapScale);

            ucMapNavigate1.MapScale = (int) base.MapScale;

            // 得到新范围
            IEnvelope pEnv = (IEnvelope) e.newEnvelope;
            IGraphicsContainer pGra = this.Map as IGraphicsContainer;
            IActiveView pAv = pGra as IActiveView;

            //在绘制前，清除axMapControl2中的任何图形元素
            pGra.DeleteAllElements();
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pEle = pRectangleEle as IElement;
            pEle.Geometry = pEnv;


            //设置鹰眼图中的红线框

            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;

            //产生一个线符号对象
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 1;
            pOutline.Color = pColor;

            //设置颜色属性
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;

            //设置填充符号的属性
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pEle as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGra.AddElement((IElement) pFillShapeEle, 0);
            pAv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof (UCMap));
            ucMapNavigate1 = new UCMapNavigate();
            axLicenseControl1 = new AxLicenseControl();
            ((ISupportInitialize) (axLicenseControl1)).BeginInit();
            ((ISupportInitialize) (this)).BeginInit();
            SuspendLayout();
            // 
            // ucMapNavigate1
            // 
            ucMapNavigate1.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            ucMapNavigate1.Appearance.BackColor = Color.Black;
            ucMapNavigate1.Appearance.Options.UseBackColor = true;
            ucMapNavigate1.Location = new Point(200, 10);
            ucMapNavigate1.Name = "ucMapNavigate1";
            ucMapNavigate1.Size = new Size(57, 244);
            ucMapNavigate1.TabIndex = 0;
            ucMapNavigate1.OnNavigate += new delegateNavigate(ucMapNavigate1_OnNavigate);
            // 
            // axLicenseControl1
            // 
            axLicenseControl1.Enabled = true;
            axLicenseControl1.Location = new Point(0, 0);
            axLicenseControl1.Name = "axLicenseControl1";
            axLicenseControl1.Size = new Size(75, 23);
            axLicenseControl1.TabIndex = 0;
            // 
            // UCMap
            // 
            Controls.Add(ucMapNavigate1);
            OcxState = ((State)(resources.GetObject("$this.OcxState")));
            Size = new Size(265, 265);
            ((ISupportInitialize) (axLicenseControl1)).EndInit();
            ((ISupportInitialize) (this)).EndInit();
            ResumeLayout(false);
        }

        private void InitScale()
        {
            Dictionary<int, structScale> TrackScale = new Dictionary<int, structScale>();
            TrackScale.Add(0, new structScale(500000, "市级"));
            TrackScale.Add(1, new structScale(180000, "区县级"));
            TrackScale.Add(2, new structScale(120000, "乡镇级"));
            TrackScale.Add(3, new structScale(40000, ""));
            TrackScale.Add(4, new structScale(20000, ""));
            TrackScale.Add(5, new structScale(9900, "街道级"));
            TrackScale.Add(6, new structScale(8000, ""));
            TrackScale.Add(7, new structScale(4000, ""));
            TrackScale.Add(8, new structScale(2000, ""));
            TrackScale.Add(9, new structScale(1000, "建筑级"));
            TrackScale.Add(10, new structScale(500, ""));
            TrackScale.Add(11, new structScale(200, ""));

            ucMapNavigate1.TrackScale = TrackScale;
            ucMapNavigate1.DefaultScaleIndex = 4;
        }

        private ICommand pControlsMapFullExtentCommand;

        private void ucMapNavigate1_OnNavigate(enumNavigate navigate, int scale)
        {
            switch (navigate)
            {
                case enumNavigate.Left:
                    PanMap(-0.3d, 0d);
                    break;
                case enumNavigate.Up:
                    PanMap(0d, 0.3d);
                    break;
                case enumNavigate.Right:
                    PanMap(0.3d, 0d);
                    break;
                case enumNavigate.Down:
                    PanMap(0d, -0.3d);
                    break;
                case enumNavigate.Full:
                    //ILayer pLayer = EngineAPI.GetLayerFromMapByName(this.Map, Common.UI.GT_COMMONNOUN.XZQChnName);
                    //if (pLayer == null)
                    //{
                    //    return;
                    //}
                    //IActiveView pActiveView = this.ActiveView;
                    //pActiveView.Extent = pLayer.AreaOfInterest;
                    //pActiveView.Refresh();
                    if (pControlsMapFullExtentCommand == null)
                    {
                        pControlsMapFullExtentCommand = new ControlsMapFullExtentCommandClass();
                        pControlsMapFullExtentCommand.OnCreate(base.Object);
                    }
                    pControlsMapFullExtentCommand.OnClick();
                    break;
                case enumNavigate.ZoomInOut:
                    base.MapScale = scale;
                    Refresh();
                    break;
                default:
                    break;
            }
            //this.Refresh();
        }

        private void PanMap(double ratioX, double ratioY)
        {
            //Pans map by amount specified given in a fraction of the extent e.g. rationX=0.5, pan right by half a screen 
            IEnvelope envelope = base.Extent;
            double h = envelope.Width;
            double w = envelope.Height;
            envelope.Offset(h*ratioX, w*ratioY);
            base.Extent = envelope;
        }
    }
}