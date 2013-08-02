using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skyline.Core.Helper
{
    internal class ImageHelper
    {
        public static Image KiResizeImage(Image bmp, int newW, int newH)
        {
            try
            {
                Image b = (Image)new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Unexpected Error:" + ex.Message);
                return null;
            }
        }
    }
}
