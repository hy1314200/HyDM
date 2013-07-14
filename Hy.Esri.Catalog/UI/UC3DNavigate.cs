using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ThreeDimenDataManage.UI
{
    public partial class UC3DNavigate : DevExpress.XtraEditors.XtraUserControl
    {
        private TerraExplorerX.TerraExplorerClass TerrainExplorer = null;
        private TerraExplorerX.SGWorld61 SGWorld =null;
        private Utility.TEHookHelper m_TEHelper;

        public UC3DNavigate()
        {
            InitializeComponent();

            this.TerrainExplorer = new TerraExplorerX.TerraExplorerClass();
            this.SGWorld = new TerraExplorerX.SGWorld61();
            SGWorld.SetOptionParam("AltitudeAndDistance", 0);
            SGWorld.SetOptionParam("AltitudeType", 0);

            m_TEHelper = new Utility.TEHookHelper(TerrainExplorer, SGWorld);

            Utility.CommandAdapter cmdAdapter = new Utility.CommandAdapter(m_TEHelper);
            //cmdAdapter.AddCommand(new ThreeDimenDataManage.Command.TE.CommandLoadProject());
            string[] strCommands =
            {
                "ThreeDimenDataManage.Command.TE.CommandLoadProject",
                "ThreeDimenDataManage.Command.TE.CommandViewGlobe",
                "ThreeDimenDataManage.Command.TE.CommandViewState",
                "ThreeDimenDataManage.Command.TE.CommandViewCity",
                "ThreeDimenDataManage.Command.TE.CommandViewStreet",
                "ThreeDimenDataManage.Command.TE.CommandViewHouse",
                "ThreeDimenDataManage.Command.TE.CommandAddShpLayer"
            };
            cmdAdapter.Adapter(this.barManager1, strCommands);
        }

    }
}
