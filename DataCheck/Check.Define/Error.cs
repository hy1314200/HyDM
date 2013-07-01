using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 质检错误
    /// @remark 考虑错误信息不同，要求自身提供写入数据库的方法
    /// 若对Topo错误不重载而使用此类，需要注意的地方为LayerName和ReferLayerName分别为FeatureClassID
    /// </summary>
    public class Error
    {
        public Error()
        {
            this.LayerName = "";
            this.ReferLayerName = "";
            this.Description = "";
            this.BSM = "";
            this.ReferBSM = "";
            this.Remark = "";
            this.ReferOID = "";
        }

        /// <summary>
        /// 错误（本身）的标识
        /// 
        /// </summary>
        public int ID { get; internal set; }

        private string m_RuleID = "";
        /// <summary>
        /// 所对应的规则标识
        /// </summary>
        public string RuleID {
            get
        {
            if (m_RuleID == null)
                m_RuleID = "";

            return m_RuleID;
        }
            set
            {
                m_RuleID = value;
            }
        }

        /// <summary>
        /// 错误的规则编码
        /// </summary>
        /// <value>The rule GZBM.</value>
        public string RuleGZBM { get; set; }

        /// <summary>
        /// 错误的类型
        /// </summary>
        public enumErrorType ErrorType { get; set; }

        /// <summary>
        /// 缺陷级别
        /// </summary>
        public enumDefectLevel DefectLevel { get; set; }

        private string m_Description = "";
        /// <summary>
        /// 错误描述
        /// </summary>
        public string Description
        {
            get
            {
                if (m_Description == null)
                    m_Description = "";
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        private string m_LayerName = "";
        /// <summary>
        /// （错误）图层名（FeatureClass名）
        /// </summary>
        public string LayerName
        {
            get
            {
                if (m_LayerName == null)
                    m_LayerName = "";
                return m_LayerName;
            }

            set
            {
                m_LayerName = value;
            }
        }

        private string m_ReferLayerName = "";
        /// <summary>
        /// 对于多图层检查出来的错误，此属性记录参考图层名
        /// </summary>
        public string ReferLayerName
        {
            get            
            {
                if (m_ReferLayerName == null)
                    m_ReferLayerName = "";

                return m_ReferLayerName;
            }
            set
            {
                m_ReferLayerName = value;
            }

        }

        private string m_BSM = "";
        /// <summary>
        /// 错误所在记录的标识码
        /// </summary>
        public string BSM
        {
            get
            {
                if (m_BSM == null)
                    m_BSM = "";

                return m_BSM;
            }
            set
            {
                m_BSM = value;
            }
        }

        private string m_ReferBSM = "";
        /// <summary>
        /// 对于多图层/记录检查出来的错误，此属性记录参考记录的标识码
        /// </summary>
        public string ReferBSM
        {
            get
            {
                if (m_ReferBSM == null)
                    m_ReferBSM = "";

                return m_ReferBSM;
            }
            set
            {
                m_ReferBSM = value;
            }
        }

        /// <summary>
        /// 错误所在记录的OID
        /// </summary>
        public int OID { get; set; }

        private string m_ReferOID = "";

        /// <summary>
        /// 对于多图层/记录检查出来的错误，此属性记录参考记录的OID
        /// </summary>
        public string ReferOID
        {
            get
            {
                if (m_ReferOID == null)
                    m_ReferOID = "";

                return m_ReferOID;
            }

            set
            {
                m_ReferOID = value;
            }
        }

        /// <summary>
        /// 是否例外
        /// </summary>
        public bool IsException { get; set; }

        public string m_Remark = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                if (m_Remark == null)
                    m_Remark = "";

                return m_Remark;
            }
            set
            {
                m_Remark = value;
            }
        }

        public virtual string ToSQLString()
        {
            //string fID = "ErrNum";
            string fRuleID = "RuleInstID";
            string fDefectLevel = "DefectLevel";
            string fIsException = "IsException";
            string fDescription = "ErrMsg";

            string fLayerName = "TargetFeatClass1";
            string fReferLayerName = "TargetFeatClass2";
            string fBSM = "BSM";
            string fReferBSM = "BSM2";
            string fOID = "OID";
            string fReferOID = "OID2";

            StringBuilder strBuilder = new StringBuilder("Insert into ");
            if (this.ErrorType == enumErrorType.Normal)
            {
                // Fields
                strBuilder.Append(" LR_ResAutoAttr (");
                //strBuilder.Append(fID); strBuilder.Append(",");
                strBuilder.Append(fRuleID); strBuilder.Append(",");
                strBuilder.Append(fDefectLevel); strBuilder.Append(",");
                strBuilder.Append(fIsException); strBuilder.Append(",");
                strBuilder.Append(fDescription); strBuilder.Append(",");

                strBuilder.Append(fLayerName); strBuilder.Append(",");
                strBuilder.Append(fReferLayerName); strBuilder.Append(",");
                strBuilder.Append(fBSM); strBuilder.Append(",");
                strBuilder.Append(fReferBSM); strBuilder.Append(",");
                strBuilder.Append(fOID); strBuilder.Append(",");
                strBuilder.Append(fReferOID);
                strBuilder.Append(") Values(");

                // Values

               // strBuilder.Append(this.ID); strBuilder.Append(",'");
                strBuilder.Append("'");
                strBuilder.Append(this.RuleID); strBuilder.Append("',");
                strBuilder.Append((int)this.DefectLevel); strBuilder.Append(",");
                strBuilder.Append(this.IsException); strBuilder.Append(",'");
                strBuilder.Append(this.Description.Replace("'","''")); strBuilder.Append("','");

                strBuilder.Append(this.LayerName.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.ReferLayerName.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.BSM.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.ReferBSM.Replace("'", "''")); strBuilder.Append("',");
                strBuilder.Append(this.OID); strBuilder.Append(",'");
                strBuilder.Append(this.ReferOID.Replace("'", "''")); strBuilder.Append("'");
                strBuilder.Append(")");   
            }
            else if (this.ErrorType == enumErrorType.Topology)
            {
                fLayerName = "SourceLayerID";
                fReferLayerName = "TargetLayerID";
                fBSM = null;
                fReferBSM = null;
                fOID = "SourceOID";
                fReferOID = "TargetOID";
                fDescription = "Reason";


                // Fields
                strBuilder.Append(" LR_ResAutoTopo (");
                strBuilder.Append("CheckType,MBTC,");
                //strBuilder.Append(fID); strBuilder.Append(",");
                strBuilder.Append(fRuleID); strBuilder.Append(",");
                strBuilder.Append(fDefectLevel); strBuilder.Append(",");
                strBuilder.Append(fIsException); strBuilder.Append(",");
                strBuilder.Append(fDescription); strBuilder.Append(",");

                strBuilder.Append(fLayerName); strBuilder.Append(",");
                strBuilder.Append(fReferLayerName); strBuilder.Append(",");
                strBuilder.Append(fOID); strBuilder.Append(",");
                strBuilder.Append(fReferOID);
                strBuilder.Append(") Values(");

                // Values

                //strBuilder.Append(this.ID); strBuilder.Append(",'");
                strBuilder.Append("'','',");
                strBuilder.Append("'");
                strBuilder.Append(this.RuleID); strBuilder.Append("',");
                strBuilder.Append((int)this.DefectLevel); strBuilder.Append(",");
                strBuilder.Append(this.IsException); strBuilder.Append(",'");
                strBuilder.Append(this.Description.Replace("'", "''")); strBuilder.Append("',");

                strBuilder.Append(this.LayerName.Replace("'", "''")); strBuilder.Append(",");
                strBuilder.Append(this.ReferLayerName.Replace("'", "''")); strBuilder.Append(",");
                strBuilder.Append(this.OID); strBuilder.Append(",'");
                strBuilder.Append(this.ReferOID.Replace("'", "''")); strBuilder.Append("'");
                strBuilder.Append(")");
            }

            return strBuilder.ToString();
        }
    }
}
