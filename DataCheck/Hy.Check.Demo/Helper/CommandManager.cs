using System;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace Hy.Check.Demo.Helper
{
    public class CommandManager
    {
        private AxToolbarControl axToolbarControl1;
        private IMapControl4 m_mapControl;
        private IPageLayoutControl3 m_pageControl;
        private bool m_bMapView = true;

        public AxToolbarControl ToolbarControl
        {
            set
            {
                axToolbarControl1 = value;
                ////Create an operation stack for the undo and redo commands to use
                IOperationStack operationStack = new ControlsOperationStackClass();
                axToolbarControl1.OperationStack = operationStack;
                if (axToolbarControl1.Buddy is IMapControl4)
                {
                    m_mapControl = (IMapControl4) axToolbarControl1.Buddy;
                    m_bMapView = true;
                }
                else if (axToolbarControl1.Buddy is IPageLayoutControl3)
                {
                    m_pageControl = (IPageLayoutControl3) axToolbarControl1.Buddy;
                    m_bMapView = false;
                }
            }
            get { return axToolbarControl1; }
        }

        /// <summary>
        /// 添加工具
        /// </summary>
        public void LoadDefaultMapCommand()
        {
            #region mapbrowse

            axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            
            axToolbarControl1.AddItem("GISDB.MapBrowse.CmdAddLayerToMap", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsMapZoomInFixedCommand", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutFixedCommand", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsFullScreenCommand", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapRefreshViewCommand", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapGoToCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsZoomToSelectedCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            //axToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool", 0, -1, false, 0,
            //                          esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsClearSelectionCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapFindCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapMeasureTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapCreateBookmarkCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            #endregion

            #region Export

            axToolbarControl1.AddItem("GISDB.DbExtract.CmdExportActiveView", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleTextOnly);

            #endregion
        }


        /// <summary>
        /// 添加打印工具
        /// </summary>
        public void LoadDefaultPageCommand()
        {
            #region 装入标准工具

            //Page browse

            axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPagePanTool", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPageZoomInFixedCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutFixedCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPageZoom100PercentCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsRedoCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsUndoCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPageZoomPageToLastExtentBackCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsPageZoomPageToLastExtentForwardCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            //cmd
            axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            //Align
            axToolbarControl1.AddItem("esriControls.ControlsSelectTool", 0, -1, true, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAlignLeftCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAlignRightCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAlignCenterCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAlignMiddleCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAlignBottomCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAlignTopCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);


            axToolbarControl1.AddItem("esriControls.ControlsSelectByGraphicsCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsGroupCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsUngroupCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSendBackwardCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSendToBackCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsBringForwardCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsBringToFrontCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapRotateTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapClearMapRotationCommand", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            #endregion

            #region 插入布局要素

            axToolbarControl1.AddItem("GISDB.MapPrint.CmdInsertNewElement", 1, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GISDB.MapPrint.CmdInsertNewElement", 2, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GISDB.MapPrint.CmdInsertNewElement", 3, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GISDB.MapPrint.CmdInsertNewElement", 4, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GISDB.MapPrint.CmdInsertNewElement", 5, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GISDB.MapPrint.CmdInsertNewElement", 6, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GISDB.MapPrint.CmdInsertNewElement", 7, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            axToolbarControl1.AddItem("esriControls.ControlsNewCircleTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsNewLineTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsNewMarkerTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsNewPolygonTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsNewRectangleTool", 0, -1, false, 0,
                                      esriCommandStyles.esriCommandStyleIconOnly);

            #endregion
        }

        public bool AddCommand(string cmdprogid)
        {
            try
            {
                axToolbarControl1.AddItem(cmdprogid, 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmdprogid"></param>
        /// <returns></returns>
        public bool ExecuteCommand(string cmdprogid)
        {
            ICommandPool pCmdPool = axToolbarControl1.CommandPool;
            UID pUid = GetUIDFromStr(cmdprogid);
            ICommand pCmd = pCmdPool.FindByUID(pUid);
            if (pCmd != null)
            {
                
                if (pCmd is ITool)
                {
                    if (m_bMapView && m_mapControl != null)
                        m_mapControl.CurrentTool = pCmd as ITool;
                    else if (m_pageControl != null)
                        m_pageControl.CurrentTool = pCmd as ITool;
                }
                else
                    pCmd.OnClick();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据字符串获取UID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public UID GetUIDFromStr(string str)
        {
            char[] sp1 = {':'};
            string[] strvalue = str.Split(sp1);
            try
            {
                UID pUid = new UIDClass();
                pUid.Value = strvalue[0];
                if (strvalue.Length == 2)
                {
                    int subtype = Convert.ToInt16(strvalue[1]);
                    if (subtype != 0)
                        pUid.SubType = subtype;
                }
                return pUid;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool LoadFromFile(string cmdfile)
        {
            //if (System.IO.File.Exists(cmdfile) == true)
            //{
            //    //Create a MemoryBlobStream
            //    IBlobStream blobStream = new MemoryBlobStreamClass();
            //    //Get the IStream interface
            //    IStream stream = (IStream)blobStream;
            //    //Get the IToolbarControl2 interface;
            //    IToolbarControl2 toolbarControl = (IToolbarControl2)axToolbarControl1.Object;

            //    //Load the stream from the file
            //    blobStream.LoadFromFile(cmdfile);
            //    //Load the stream into the ToolbarControl
            //    toolbarControl.LoadItems(stream);
            //}
            return true;
        }

        public bool SaveToFile(string cmdfile)
        {
            //Create a MemoryBlobStream 
            //IBlobStream blobStream = new MemoryBlobStreamClass();
            IXMLStream xmlstream = new XMLStream();
            //Get the IStream interface
            IStream stream = (IStream) xmlstream;
            //Get the IToolbarControl2 interface
            IToolbarControl2 toolbarControl = (IToolbarControl2) axToolbarControl1.Object;

            //Save the ToolbarControl into the stream
            toolbarControl.SaveItems(stream);
            //Save the stream to a file
            xmlstream.SaveToFile(cmdfile);
            return true;
        }
    }
}