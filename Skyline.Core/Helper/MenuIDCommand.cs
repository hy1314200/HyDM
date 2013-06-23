using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;

namespace Skyline.Core
{
    public enum CommandParam
    {
        /// <summary>
        /// 特效
        /// </summary>
        ISunshine = 1026,

        PSunshine = 0,

        ITimeSlider = 1065,

        PTimeSlider = 4,

        IArcSDEDatabase = 1013,

        PArcSDEDatabase = 9,

        ILineofSight = 1046,

        PLineofSight = 0,

        IAddText = 1012,

        PAddText = 0,

        IAddImage = 1012,

        PAddImage = 1,

        IGlobe = 1055,

        PGlobe = 5,

        ICountry = 1055,

        PCountry = 4,

        IState = 1055,

        PState = 3,

        ICity = 1055,

        PCity = 2,

        IStreet = 1055,

        PStreet = 1,

        IHouse = 1055,

        PHouse = 0,

        IArea = 1037,

        PArea = 0,

        IViewShed = 1047,

        PViewShed = 0,

        IBestpath = 1042,

        PBestpath =0,

        IWater = 1155,

        PWater =0,

        IClouds = 1154,

        PClouds =0,

        ILineOfSight = 1046,

        PLineOfSight = 0,

        ISaveCamera =1068,

        PSaveCamera =0,

        ISlopeDirections = 1093,

        PSlopeDirections = 0,

        ISlopeColorMap = 1092,

        PSlopeColorMap =0,

        IContourLines = 1039,

        PContourLines = 0,

        ICreate3DModel = 1012,

        PCreate3DModel = 13,

        ICreatePolyline = 1012,

        PCreatePolyline = 4,

        ICreatePolygon = 1012,

        PCreatePolygon = 5,

        IFlood = 1044,

        PFlood = 0,

        ITerrainProfile = 1043,

        PTerrainProfile = 0,

        IFeatureSelection = 1070,

        PFeatureSelection = 0,

        ISlopePalettess = 1098, //坡度颜色图例

        PSlopePalettess = 0,

        IContourPallets = 1041,//等高线图例

        PContourPallets = 0,

        IContourColorsandLines = 1040,

        PContourColorsandLines = 0,

        ISaveAs = 1004,

        PSaveAs = 0,

        ISelect = 1021,

        PSelect = 0,

        Iearthwork = 1045,

        Pearthwork = 0,

        ICollisionDetection = 1140,//碰撞开关

        PCollisionDetection = 0,

        IIndoorView = 1149, //内部观察

        PIndoorView = 0,

        ICreateHole = 1012,

        PCreateHole = 16,
        
        ISaveLayer = 1088,
        
        PSaveLayer = 0,

        IPresentation = 1015,

        PPresentation = 0,

        IModifyTerrain=1012,

        PModifyTerrain=15,
         
        IInfomation=1023,

        PInfomation=0
    }
    public class MenuIDCommand
	{
        public static void RunMenuCommand(ISGWorld61 sgWorld, CommandParam ICommandID, CommandParam pCommandID)
        {
            sgWorld.Command.Execute((int)ICommandID, (int)pCommandID);
        }
        public static object returnValue(ISGWorld61 sgWorld,CommandParam ICommandID)
        {
            return sgWorld.Command.GetValue((int)ICommandID);
        }

	}
}
