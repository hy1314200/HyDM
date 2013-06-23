using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;

namespace Skyline.Core.UI
{
    static class Program
    {
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

        public static ESRI.ArcGIS.Geodatabase.IWorkspace pWorkspace;
    }
}
