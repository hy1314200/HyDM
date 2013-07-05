using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Hy.Check.Command.CustomCommand
{
    /// <summary>
    /// Summary description for MapIdentifyCommand.
    /// </summary>
    [Guid("6ed31625-6b8c-4c0e-9758-02d3d7faffad")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Hy.Check.Command.CustomCommand.MapIdentifyCommand")]
    public sealed class MapIdentifyCommand : BaseCommand, ICommandSubType
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

        private IHookHelper m_hookHelper;
        private ICommand mapTool;
        private long m_nsubtype;

        public MapIdentifyCommand()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add MapIdentifyCommand.OnClick implementation

            CheckApplication.m_UCDataMap.CurActivityMapControl.CurrentTool = null;
            mapTool = null;
            switch (m_nsubtype)
            {
                case 1:  //identify
                    {
                        Identify identifyTool = new Identify();
                        identifyTool.OnCreate(CheckApplication.m_UCDataMap.CurActivityMapControl.Object);
                        CheckApplication.m_UCDataMap.CurActivityMapControl.CurrentTool = (ITool)identifyTool;
                        break;
                    }
                case 2:  //放大
                    {
                        if (mapTool == null)
                        {
                            mapTool = new ControlsMapZoomInToolClass();
                            CreateTool(false);
                        }
                        break;
                    }
                case 3:  //缩小
                    {
                        if (mapTool == null)
                            mapTool = new ControlsMapZoomOutToolClass();
                        CreateTool(false);
                        break;
                    }
                case 4:  //全图
                    {
                        //if (mapTool == null)
                        //    mapTool = new ControlsMapFullExtentCommand();
                        //CreateTool(true);

                        FullView();

                        break;
                    }
                case 5:  //漫游
                    {
                        if (mapTool == null)
                            mapTool = new ControlsMapPanToolClass();
                        CreateTool(false);
                        break;
                    }
                case 6:  //前一视图
                    {
                        if (mapTool == null)
                            mapTool = new ControlsMapZoomToLastExtentForwardCommandClass();
                        CreateTool(true);
                        break;
                    }
                case 7:  //后一视图
                    {
                        if (mapTool == null)
                            mapTool = new ControlsMapZoomToLastExtentBackCommandClass();
                        CreateTool(true);
                        break;
                    }
            }
        }

        private void CreateTool(bool OnClick)
        {
            if (mapTool != null)
            {
                mapTool.OnCreate(CheckApplication.m_UCDataMap.CurActivityMapControl.Object);
                if (OnClick)
                {
                    mapTool.OnClick();
                }
                else
                {
                    CheckApplication.m_UCDataMap.CurActivityMapControl.CurrentTool = (ITool)mapTool;
                }
            }
        }

        public override bool Enabled
        {
            get
            {
                return CheckApplication.CurrentTask != null;
            }
        }
        #endregion

        #region ICommandSubType 成员

        public int GetCount()
        {
            return 7;
        }

        public void SetSubType(int SubType)
        {
            m_nsubtype = SubType;
        }

        #endregion


        private void FullView()
        {
            IEnumDataset enDataset =CheckApplication.CurrentTask.BaseWorkspace.get_Datasets(esriDatasetType.esriDTAny);
            IDataset dataset = enDataset.Next();
            double xMin = double.MaxValue, xMax = double.MinValue, yMin = double.MaxValue, yMax = double.MinValue;
            IEnvelope envExtent = null;
            while (dataset != null)
            {
                IGeoDataset geoDataset = dataset as IGeoDataset;
                if (geoDataset != null)
                {
                    envExtent = geoDataset.Extent;

                    if (xMin > envExtent.XMin) xMin = envExtent.XMin;
                    if (yMin > envExtent.YMin) yMin = envExtent.YMin;
                    if (xMax < envExtent.XMax) xMax = envExtent.XMax;
                    if (yMax < envExtent.YMax) yMax = envExtent.YMax;
                }
                dataset = enDataset.Next();
            }
            envExtent.PutCoords(xMin,yMin,xMax,yMax);
            m_hookHelper.ActiveView.Extent = envExtent;
            m_hookHelper.ActiveView.Refresh();
        }
    }
}
