using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;
using System.Configuration;

namespace Skyline.GuiHua.Bussiness
{
    public class PipeAnalysis
    {
        private PipeAnalysis()
        {
        }


        public const string Pipe_Group_Name = "PipeAnalysis";

        public const string Pipe_Hole_Name = "Hole";


        
        private static PipeAnalysis m_Instance = new PipeAnalysis();

        public static PipeAnalysis Instance
        {
            get
            {
                return m_Instance;
            }
        }


        public event System.Threading.ThreadStart Analysised;

        private bool m_AnalysisResult=false;

        public  bool AnalysisResult
        {
            get
            {
                return m_AnalysisResult;
            }
        }

        //public  IGeometry HoleGeometry { get;private set; }

        private IBBox3D61 m_HoleBox { get; set; }

        public TerraExplorerClass TE { get; set; }

        public ISGWorld61 Hook { get; set; }

        public bool HasError { get; private set; }

        //private ITerrainModel61 m_Model = null;
        private string m_ModelName = null;
        IFeature61 m_FeatureSelected = null;

        public void StartPipeAnalysis()
        {
            HasError = false;

            TE.OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            TE.OnLButtonDown += new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
      
        }

        public double GetArea()
        {
            if (m_HoleBox == null)
                return 0;

            return (m_HoleBox.MaxX - m_HoleBox.MinX) * (m_HoleBox.MaxY - m_HoleBox.MinY);
        }

        private string m_ModelField =ConfigurationManager.AppSettings["ModelField"];
        /// <summary>
        /// 左键单击事件
        /// 获取建筑物信息
        /// </summary>
        /// <param name="Flags"></param>
        /// <param name="X"></param>mo
        /// <param name="Y"></param>
        /// <param name="pbHandled"></param>
        void TE_OnLButtonDown(int Flags, int X, int Y, ref object pbHandled)
        {
            try
            {

                int modelLayerID = Hook.ProjectTree.FindItem(ConfigurationManager.AppSettings["LayerPath"]);                


                // 模型复原
                // 显示
                int groupID = Hook.ProjectTree.FindItem(Pipe_Group_Name);
                if (groupID > 0)
                {
                    Hook.ProjectTree.DeleteItem(groupID);
                }
                if (m_FeatureSelected != null)
                {

                    //m_FeatureSelected.FeatureAttributes.GetFeatureAttribute("MODELNAME").Value = m_ModelName;

                    if (modelLayerID > 0)
                    {
                        ILayer61 lyrModel = Hook.ProjectTree.GetLayer(modelLayerID);
                        lyrModel.Filter = "";
                        //lyrModel.Refresh();
                        //lyrModel.Save();
                        //lyrModel.Refresh();
                    }
                }


                // 开始新的
                // 取到模型
                object longitude, latitude, objectID, height, objType;
                objType = 17;
                Program.pRender.ScreenToWorld(X, Y, ref objType, out longitude, out height, out latitude, out objectID);
                if (objectID.ToString() == "")
                {
                    objType = 16;
                    Program.pRender.ScreenToWorld(X, Y, ref objType, out longitude, out height, out latitude, out objectID);
                }

                if (objectID.ToString() == "")
                    return;

                groupID = Hook.ProjectTree.CreateGroup(Pipe_Group_Name);

                ITerraExplorerObject61 teOjbect = Program.pCreator6.GetObject(objectID as string);
                m_FeatureSelected = teOjbect as IFeature61;
                if (m_FeatureSelected == null)
                {
                    return;
                }

                m_ModelName = m_FeatureSelected.FeatureAttributes.GetFeatureAttribute(m_ModelField).Value;

                string strFile = System.IO.Path.Combine(ConfigurationManager.AppSettings["ModelPath"], m_ModelName);

                IPoint pSel = m_FeatureSelected.Geometry as IPoint;

                ITerrainModel61 m_Model =Hook.Creator.CreateModel( Hook.Creator.CreatePosition(pSel.X, pSel.Y),strFile,1,ModelTypeCode.MT_NORMAL,groupID,"Temp");
                
                m_Model.Visibility.Show = false;
                if (m_Model == null)
                    return;

                m_AnalysisResult=false;
                string strPipedFileList = ConfigurationManager.AppSettings["PipedFileList"];
                strPipedFileList = strPipedFileList.ToLower();

                m_HoleBox = m_Model.Terrain.BBox;
                Hook.Creator.DeleteObject(m_Model.ID);

                double[] points = 
                {
                    m_HoleBox.MinX,m_HoleBox.MinY,0,
                    m_HoleBox.MaxX,m_HoleBox.MinY,0,
                    m_HoleBox.MaxX,m_HoleBox.MaxY,0,
                    m_HoleBox.MinX,m_HoleBox.MaxY,0
                };

                IPolygon polygonHole = Hook.Creator.GeometryCreator.CreatePolygonGeometry(points);


                Hook.Creator.CreateHoleOnTerrain(polygonHole as IGeometry, groupID, Pipe_Hole_Name);

                if (strPipedFileList.Contains(m_ModelName.ToLower()))
                {
                    m_AnalysisResult = true;
                }

                // 隐藏
               // m_FeatureSelected.FeatureAttributes.GetFeatureAttribute("MODELNAME").Value = "";
                if (modelLayerID > 0)
                {
                    ILayer61 lyrModel = Hook.ProjectTree.GetLayer(modelLayerID);
                    //lyrModel.Save();
                    lyrModel.Filter = string.Format("{0} <> '{1}'",m_ModelField, m_ModelName);
                    lyrModel.Refresh();
                }

            }
            catch (Exception ex)
            {
                HasError = true;
            }
            finally
            {
                if (Analysised != null)
                    Analysised.Invoke();
                TE.OnLButtonDown -= new TerraExplorerX._ITerraExplorerEvents5_OnLButtonDownEventHandler(TE_OnLButtonDown);
            }
        }
               
    }
}
