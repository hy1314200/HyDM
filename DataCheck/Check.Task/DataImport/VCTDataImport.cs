using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using VCTEXCHANGELib;


using Common.Utility.Esri;
using Check.Define;

namespace Check.Task.DataImport
{
    public delegate void VCTConvertStepHandler(int totalCount);

    public class VCTDataImport:DefaultDataImport
    {

        /// <summary>
        /// 抛出VCT转换时的Step事件
        /// </summary>
        public event VCTConvertStepHandler ConvertStepping;

        private int m_ObjectCount = -1;
        /// <summary>
        /// 获取VCT转换时需要转换的“Layer”数
        /// @remark 此方法使用了VCTReaderClass进行获取，预料的消耗较大，请不要频繁调用--而是使用变量保存起来
        /// </summary>
        public int ObjectCount
        {
            get
            {
                if (m_ObjectCount == -1)
                {
                    //VctReaderClass vctReader = new VctReaderClass();
                    //vctReader.VctFile = this.m_Datasource;
                    //m_ObjectCount = vctReader.LayerCount;

                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(vctReader);

                }          
                return m_ObjectCount;
            }
        }

        protected override bool ImportToBase(ref ESRI.ArcGIS.Geodatabase.IWorkspace wsBase)
        {
           // Common.Utility.Esri.AEAccessFactory.CreateFGDB(this.m_TargetPath, COMMONCONST.DB_Name_Base,ref wsBase);
           // VctReaderClass vctReader = new VctReaderClass();
           // vctReader.VctFile = this.m_Datasource;   
           //// vctReader.SpatialReference = this.m_SpatialReference;

           // vctReader.ProgressStep += new _IVctReaderEvents_ProgressStepEventHandler(VCTReader_ProgressStep);

           // //执行转换
           // m_ObjectCount = vctReader.LayerCount;
           // bool isSucceed = vctReader.ConvertToWorkspace(wsBase);
           // System.Runtime.InteropServices.Marshal.ReleaseComObject(vctReader);
           // if (!isSucceed)
           // {
           //     if (m_Messager != null)
           //         m_Messager(enumMessageType.Exception, "转换VCT时出错");

           //     return false;
           // }
            return true;
        }

        void VCTReader_ProgressStep()
        {
            if(this.ConvertStepping!=null)
                ConvertStepping(this.ObjectCount);
        }


    }
}
