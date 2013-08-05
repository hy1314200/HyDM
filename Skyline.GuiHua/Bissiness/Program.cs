using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;

namespace Skyline.GuiHua.Bussiness
{
    static class Program
    {
        static Program()
        {
            TE = new TerraExplorerClass();
            sgworld = new SGWorld61Class();

            Program.ITE = Program.TE as ITerraExplorer5;
            Program.IInfoTree = (IInformationTree5)Program.TE;
            Program.pRender = (IRender5)Program.TE;
            Program.pPlane5 = (IPlane5)Program.TE;
            Program.Terrain5 = (ITerrain5)Program.TE;
            Program.pNavigate6 = (INavigate61)Program.sgworld.Navigate;
            Program.pIobject = (IObjectManager51)Program.TE;
            Program.pCreator6 = (ICreator61)Program.sgworld.Creator;
            Program.pDateTime = (IDateTime61)Program.sgworld.DateTime;
            Program.pCoordServices6 = (ICoordServices61)Program.sgworld.CoordServices;
            Program.CoordSys = (ICoordSys3)Program.TE;
        }

        public static TerraExplorerClass TE;
        public static SGWorld61 sgworld;
        public static ITerraExplorer5 ITE;
        public static ILayer5 iLyr;
        public static IInformationTree5 IInfoTree;
        public static IRender5 pRender;
        public static ITerrain5 Terrain5;
        public static IPlane5 pPlane5;
        public static IObjectManager51 pIobject;
        public static INavigate61 pNavigate6;
        public static IDateTime61 pDateTime;

        public static ITerraExplorerObject61 ObjectManager;
        public static ICreator61 pCreator6;

        public static ICoordSys3 CoordSys;

        public static ICoordServices61 pCoordServices6;

        public static ITENavigationMap5 pTENavigationMap5;

    }
}
