using System;
using ESRI.ArcGIS;
using System.Windows.Forms;

namespace Skyline.Frame
{
  internal partial class EsriLicenseInitializer
  {
      public EsriLicenseInitializer()
    {
      ResolveBindingEvent += new EventHandler(BindingArcGISRuntime);
    }

    void BindingArcGISRuntime(object sender, EventArgs e)
    {
      ProductCode[] supportedRuntimes = new ProductCode[] { 
        ProductCode.Engine, ProductCode.Desktop };
      foreach (ProductCode c in supportedRuntimes)
      {
        if (RuntimeManager.Bind(c))
          return;
      }
      MessageBox.Show("ArcGIS运行时绑定失败，应用程序将关闭。");
      System.Environment.Exit(0);

    }
  }
}