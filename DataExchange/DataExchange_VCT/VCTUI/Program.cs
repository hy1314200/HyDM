using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DIST.DGP.DataExchange.VCTUI;

namespace VCTUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new VCT2MDBForm());
            VCT2MDBForm frm = new VCT2MDBForm();
            frm.ShowDialog();
        }
    }
}
