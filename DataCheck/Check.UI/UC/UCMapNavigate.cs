using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Check.UI.Properties;

namespace Check.UI.UC
{
    public enum enumNavigate
    {
        Left = 1,
        Up = 2,
        Right = 3,
        Down = 4,
        Full = 5,
        ZoomInOut = 6
    }

    public struct structScale
    {
        public int MapScale;
        public string ScaleDesc;

        public structScale(int mapscale, string scaledesc)
        {
            MapScale = mapscale;
            ScaleDesc = scaledesc;
        }
    }

    public delegate void delegateNavigate(enumNavigate navigate, int scale);

    public partial class UCMapNavigate : XtraUserControl
    {
        //public int LargeChange { set { this.trackBarControl1.Properties.LargeChange = value; } }
        //public int SmallChange { set { this.trackBarControl1.Properties.SmallChange = value; } }
        //public int Maximum { set { this.trackBarControl1.Properties.Maximum = value; } }
        //public int Minimum { set { this.trackBarControl1.Properties.Minimum = value; } }
        private bool sysbool = false;
        private bool userbool = false;

        public int MapScale
        {
            set
            {
                try
                {
                    sysbool = true;
                    for (int i = 0; i < trackscale.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (value >= trackscale[i].MapScale)
                            {
                                if (!userbool)
                                    trackBarControl1.Value = i;
                                break;
                            }
                        }
                        if (i == trackscale.Count - 1)
                        {
                            if (value <= trackscale[i].MapScale)
                            {
                                if (!userbool)
                                    trackBarControl1.Value = i;
                                break;
                            }
                        }
                        if (value < trackscale[i].MapScale && value > trackscale[i + 1].MapScale)
                        {
                            if (!userbool)
                                trackBarControl1.Value = i + 1;
                            break;
                        }
                    }
                    sysbool = false;
                }
                catch (Exception ex)
                {
                }
            }
        }

        public event delegateNavigate OnNavigate;
        private Dictionary<int, structScale> trackscale;

        public Dictionary<int, structScale> TrackScale
        {
            set
            {
                if (value == null) return;
                trackscale = value;
                trackBarControl1.Properties.Maximum = trackscale.Count - 1;
            }
        }

        public int DefaultScaleIndex
        {
            set { trackBarControl1.Value = value; }
        }

        public UCMapNavigate()
        {
            InitializeComponent();
            Region = BmpRgn(Resources.mapnav2);
        }

        private void UCMapNav_Resize(object sender, EventArgs e)
        {
            Size = new Size(57, 244);
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            if (OnNavigate == null) return;
            try
            {
                if (!sysbool)
                {
                    userbool = true;
                    OnNavigate(enumNavigate.ZoomInOut, trackscale[trackBarControl1.Value].MapScale);
                    userbool = false;
                }
                trackBarControl1.ToolTip = trackscale[trackBarControl1.Value].ScaleDesc;
            }
            catch
            {
            }
        }


        private void NavigateMouseDown(object sender, MouseEventArgs e)
        {
            sysbool = false;
            SimpleButton button = (SimpleButton) sender;
            if (OnNavigate == null) return;
            switch (button.Name)
            {
                case "butLeft":
                    OnNavigate(enumNavigate.Left, 0);
                    break;
                case "butUp":
                    OnNavigate(enumNavigate.Up, 0);
                    break;
                case "butRight":
                    OnNavigate(enumNavigate.Right, 0);
                    break;
                case "butDown":
                    OnNavigate(enumNavigate.Down, 0);
                    break;
                case "butFull":
                    OnNavigate(enumNavigate.Full, 0);
                    trackBarControl1.Value = 0;
                    break;
                case "butOut":
                    try
                    {
                        trackBarControl1.Value += 1;
                    }
                    catch
                    {
                        trackBarControl1.Value = trackscale.Count - 1;
                    }
                    break;
                case "butIn":
                    try
                    {
                        trackBarControl1.Value -= 1;
                    }
                    catch
                    {
                        trackBarControl1.Value = 0;
                    }

                    break;
                default:
                    break;
            }
        }

        #region Region

        private static Region BmpRgn(Bitmap Picture) //,   Color   TransparentColor)   
        {
            #region

            int nWidth = Picture.Width;
            int nHeight = Picture.Height;
            Region rgn = new Region();
            rgn.MakeEmpty();
            bool isTransRgn; //前一个点是否在透明区   
            Color curColor; //当前点的颜色   
            Rectangle curRect = new Rectangle();
            curRect.Height = 1;
            int x = 0, y = 0;
            Color TransparentColor = Picture.GetPixel(0, 0);
            //逐像素扫描这个图片，找出非透明色部分区域并合并起来   
            for (y = 0; y < nHeight; ++y)
            {
                isTransRgn = true;
                for (x = 0; x < nWidth; ++x)
                {
                    curColor = Picture.GetPixel(x, y);
                    if (curColor == TransparentColor || x == nWidth - 1) //如果遇到透明色或行尾   
                    {
                        if (isTransRgn == false) //退出有效区   
                        {
                            curRect.Width = x - curRect.X;
                            rgn.Union(curRect);
                        }
                    }
                    else //非透明色   
                    {
                        if (isTransRgn == true) //进入有效区   
                        {
                            curRect.X = x;
                            curRect.Y = y;
                        }
                    } //if   curColor   
                    isTransRgn = curColor == TransparentColor;
                } //for   x   
            } //for   y   
            return rgn;

            #endregion
        }

        #endregion
    }
}