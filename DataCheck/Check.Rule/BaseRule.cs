using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Check.Define;
using Check.Rule.Helper;
using Check.Utility;

namespace Check.Rule
{
    /// <summary>
    /// 提供ICheckRule的基本处理方法和机制
    /// @remark 
    /// ErrorType属性@see::ErrorType默认为普通自动检查
    /// Description属性@see::Description默认为空
    /// SendMessage方法@see::SendMessage作了null判断
    /// </summary>
    public abstract class BaseRule : ICheckRule
    {
        protected MessageHandler m_Messager;
        protected ESRI.ArcGIS.Geodatabase.IWorkspace m_BaseWorkspace;
        protected ESRI.ArcGIS.Geodatabase.IWorkspace m_QueryWorkspace;
        protected System.Data.IDbConnection m_QueryConnection;
        protected ESRI.ArcGIS.Geodatabase.IWorkspace m_TopoWorkspace;
        protected System.Data.IDbConnection m_ResultConnection;
        protected string m_InstanceID;
        protected string m_InstanceName;
        protected string m_SchemaID;
        protected enumDefectLevel m_DefectLevel;

        public virtual string InstanceID
        {
            get
            {
                return m_InstanceID;
            }
            set
            {
                m_InstanceID = value;
            }
        }
        public virtual string InstanceName
        {
            get
            {
                return m_InstanceName;
            }
            set
            {
                m_InstanceName = value;
            }
        }
        public virtual string Description
        {
            get { return this.Name; }
        }
        public virtual enumErrorType ErrorType
        {
            get
            {
                return enumErrorType.Normal;
            }
        }
        public virtual MessageHandler MessageHandler
        {
            set { m_Messager = value; }
        }
        public virtual ESRI.ArcGIS.Geodatabase.IWorkspace BaseWorkspace
        {
            set
            {
                m_BaseWorkspace = value;
            }
        }
        public virtual ESRI.ArcGIS.Geodatabase.IWorkspace QueryWorkspace
        {
            set
            {
                m_QueryWorkspace = value;
            }
        }
        public virtual System.Data.IDbConnection QueryConnection
        {
            set
            {
                m_QueryConnection = value;
            }
        }
        public virtual ESRI.ArcGIS.Geodatabase.IWorkspace TopoWorkspace
        {
            set
            {
                m_TopoWorkspace = value;
            }
        }
        public virtual System.Data.IDbConnection ResultConnection
        {
            set
            {
                m_ResultConnection = value;
            }
        }
        public virtual string SchemaID
        {
            set
            {
                this.m_SchemaID = value;

            }
        }
        public virtual enumDefectLevel DefectLevel
        {
            get
            {
                return m_DefectLevel;
            }
            set
            {
                m_DefectLevel = value;
            }
        }

        /// <summary>
        /// 默认为不进行预处理
        /// </summary>
        /// <returns></returns>
        public virtual bool Pretreat()
        {
            return true;
        }

        public abstract string Name { get; }
        public abstract IParameterSetter GetParameterSetter();
        public abstract void SetParamters(byte[] objParamters);
        public abstract bool Verify();
        public abstract bool Check(ref List<Error> checkResult);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="strMessage"></param>
        protected void SendMessage(enumMessageType msgType, string strMessage)
        {
            if (m_Messager != null)
                m_Messager.Invoke(msgType,"规则“" + this.m_InstanceName+"”:"+strMessage);
        }

        /// <summary>
        ///通过图层别名获取图层名称
        /// </summary>
        /// <param name="strAliasName">Name of the STR alias.</param>
        /// <returns></returns>
        protected string GetLayerName(string strAliasName)
        {
            if (string.IsNullOrEmpty(strAliasName)) return null;
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);

            return LayerReader.GetNameByAliasName(strAliasName, standardID);
            
        }

    }
}
