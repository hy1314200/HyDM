using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIST.DGP.DataExchange.VCT.FileData;
using ESRI.ArcGIS.Geodatabase;
using DIST.DGP.DataExchange.VCT.Metadata;

namespace DIST.DGP.DataExchange.VCT.ESRIData {
	/// <summary>
	/// 数据表类
	/// </summary>
    public class TableLayer
    {
        /// <summary>
        /// 标识码字段名称
        /// </summary>
        protected string m_strEntityFieldName="BSM";
        //public string EntityFieldName
        //{
        //    set
        //    {
        //        m_strEntityFieldName = value;
        //    }
        //}

        /// <summary>
        /// 要素代码名称
        /// </summary>
        protected string m_strSYDMFieldName="SYDM";

        /// <summary>
        /// esri注记中的非属性字段名称集合
        /// </summary>
        private readonly string m_strAnnotationFields = "FeatureID;ZORDER;AnnotationClassID;Element;SymbolID;Status;TextString;FontName;FontSize;"
                             + "Bold;Italic;Underline;VerticalAlignment;HorizontalAlignment;XOffset;YOffset;Angle;FontLeading"
                             + "WordSpacing;CharacterWidth;CharacterSpacing;FlipAngle;Override;ZOrder";

        private string m_strFeatureCode;

        /// <summary>
        /// 要素代码
        /// </summary>
        public string FeatureCode
        {
            get
            {
                return m_strFeatureCode;
            }
            set
            {
                m_strFeatureCode = value;
            }
        }
        private string m_GeometryType;
        /// <summary>
        /// 要素类型（点，线，面，属性表）
        /// </summary>
        public string GeometryType
        {
            get
            {
               return m_GeometryType;
            }
            set
            {
                m_GeometryType = value;
            }
        }
        /// <summary>
        /// ESRI数据表
        /// </summary>
        public ITable Table
        {
            get
            {
                return m_pITable;
            }
            set
            {
                m_pITable = value;
            }
        }
        private ITable m_pITable;

        /// <summary>
        /// 表结构节点
        /// </summary>
        public TableStructureNode StructureNode
        {
            get
            {
                if (m_TableStructureNode == null)
                    m_TableStructureNode = GetTableStructureNode();
                return m_TableStructureNode;
            }
            set
            {
                m_TableStructureNode = value;
            }
        }
        private TableStructureNode m_TableStructureNode = null;

        /// <summary>
        /// 获取要素代码节点
        /// </summary>
        /// <returns></returns>
        public virtual FeatureCodeNode GetFeatureCodeNode()
        {
            try
            {
                FeatureCodeNode pFeatureCodeNode = new FeatureCodeNode();
                ///如果是空间数据
                if (m_pITable is IFeatureClass)
                {
                    ///要素名称
                    pFeatureCodeNode.FeatureName = (m_pITable as IObjectClass).AliasName;

                    //要素代码
                    string sAttriTableName = (m_pITable as IDataset).Name;
                    pFeatureCodeNode.FeatureCode = MetaDataFile.GetFeatureCodeByName(sAttriTableName);

                    ///获取要素类型
                    if ((m_pITable as IFeatureClass).FeatureType != esriFeatureType.esriFTAnnotation)
                    {
                        switch ((m_pITable as IFeatureClass).ShapeType)
                        {
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryLine:
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                                pFeatureCodeNode.GeometryType =Metadata.MetaDataFile.GraphConfig.GetGraphTypeMark("Line");
                                break;
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultipoint:
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                                pFeatureCodeNode.GeometryType = Metadata.MetaDataFile.GraphConfig.GetGraphTypeMark("Point");
                                break;
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                                pFeatureCodeNode.GeometryType = Metadata.MetaDataFile.GraphConfig.GetGraphTypeMark("Polygon");
                                break;
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryNull:
                                pFeatureCodeNode.GeometryType = Metadata.MetaDataFile.GraphConfig.GetGraphTypeMark("NoneGeometry");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        pFeatureCodeNode.GeometryType = Metadata.MetaDataFile.GraphConfig.GetGraphTypeMark("Annotation");
                    }
                    ///数据表名称
                    pFeatureCodeNode.TableName = sAttriTableName;

                    ///获取标识码字段
                    string sKeyFieldName = "";
                    sKeyFieldName = Metadata.MetaDataFile.GetEntityIDFieldName(sAttriTableName);
                    if (sKeyFieldName != "")
                        m_strEntityFieldName = sKeyFieldName;

                    ////获取要素代码字段
                    sKeyFieldName = Metadata.MetaDataFile.GetYSDMFieldName(sAttriTableName);
                    if (sKeyFieldName != "")
                        m_strSYDMFieldName = sKeyFieldName;
                }
                    ///如果是属性数据
                else if(m_pITable!=null)
                {
                    pFeatureCodeNode.FeatureName = (m_pITable as IObjectClass).AliasName;
                    string sAttriTableName = (m_pITable as IDataset).Name;
                    pFeatureCodeNode.FeatureCode = MetaDataFile.GetFeatureCodeByName(sAttriTableName);
                    ///数据表名称
                    pFeatureCodeNode.TableName = sAttriTableName;
                    pFeatureCodeNode.GeometryType = Metadata.MetaDataFile.GraphConfig.GetGraphTypeMark("NoneGeometry");

                }
              
                return pFeatureCodeNode;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return null;
            }
        }


        /// <summary>
        /// 获取VCT表结构节点
        /// </summary>
        private TableStructureNode GetTableStructureNode()
        {
            try
            {
                
                m_TableStructureNode = new TableStructureNode();
                ///判断当前是否为空间数据表
                if (m_pITable is IFeatureClass)
                {
                    m_TableStructureNode.IsGeometryTable = true;
                    IFeatureClass pFeatureCls = this.m_pITable as IFeatureClass;
                    ///获取表名称
                    m_TableStructureNode.TableName = (m_pITable as IDataset).Name;
                }
                else
                {
                    m_TableStructureNode.IsGeometryTable = false;
                    //m_TableStructureNode.TableName = (m_pITable as IObjectClass).AliasName;
                    m_TableStructureNode.TableName = (m_pITable as IDataset).Name;
                    
                }
                //获取字段信息
                m_TableStructureNode.FieldNodes = GetFieldNodes(m_pITable.Fields);

                ///通过配置获取标识码字段名称
                string sKeyFieldName = "";
                sKeyFieldName = Metadata.MetaDataFile.GetEntityIDFieldName(m_TableStructureNode.TableName);
                if (sKeyFieldName != "")
                    m_strEntityFieldName = sKeyFieldName;

                ///通过配置获取要素代码字段名称
                sKeyFieldName = Metadata.MetaDataFile.GetYSDMFieldName(m_TableStructureNode.TableName);
                if (sKeyFieldName != "")
                    m_strEntityFieldName = sKeyFieldName;
                return m_TableStructureNode;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return null;
            }
        }
        private List<FieldNode> GetFieldNodes(IFields pFields)
        {
            try
            {
                IFeatureClass pFeatureCls = this.m_pITable as IFeatureClass;
                List<FieldNode> pListFieldNode = new List<FieldNode>();

                for (int i = 0; i < pFields.FieldCount; i++)
                {
                    IField itemField = pFields.get_Field(i);
                        ///只保持属性字段
                    if (itemField.Type != esriFieldType.esriFieldTypeGeometry
                        && itemField.Type != esriFieldType.esriFieldTypeOID
                        && itemField.Name.ToUpper() != "SHAPE_LENGTH"
                        && itemField.Name.ToUpper() != "SHAPE_AREA"
                        )
                    {
                        if (pFeatureCls != null && pFeatureCls.FeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            AddAnnotationFieldNode(itemField, pListFieldNode);
                        }
                        else
                        {
                            string sLine = "";
                            if (itemField.Precision != null && itemField.Precision != 0)
                                sLine = itemField.Name + HeadNode.Separator
                                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
                                  + itemField.Length + HeadNode.Separator
                                   + itemField.Precision;
                            else
                                sLine = itemField.Name + HeadNode.Separator
                                + GetVCTFieldType(itemField.Type) + HeadNode.Separator
                                 + itemField.Length;
                            FieldNode pFieldNode = new FieldNode(sLine);
                            pListFieldNode.Add(pFieldNode);
                        }
                    }   
                }
                return pListFieldNode;


            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return null;
            }
        }

        private void AddAnnotationFieldNode(IField itemField, List<FieldNode> pListFieldNode)
        {
            if (!m_strAnnotationFields.Contains(itemField.Name))
            {
               string sLine = itemField.Name + HeadNode.Separator
                           + GetVCTFieldType(itemField.Type) + HeadNode.Separator
                            + itemField.Length + HeadNode.Separator
                             + itemField.Precision;
               FieldNode pFieldNode = new FieldNode(sLine);
                pListFieldNode.Add(pFieldNode);

            }
            //string sName = itemField.Name;
            //string sLine;
            //FieldNode pFieldNode;
            //if (sName == "TextString")
            //{
            //    sLine = "BSM" + HeadNode.Separator
            //               + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                + itemField.Length + HeadNode.Separator
            //                 + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);

            //    sLine = "YSDM" + HeadNode.Separator
            //                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                  + itemField.Length + HeadNode.Separator
            //                   + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);

            //    sLine = "ZJNR" + HeadNode.Separator
            //                   + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                    + itemField.Length + HeadNode.Separator
            //                     + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //}
            //else if (sName == "FontName")
            //{
            //    sLine = "ZT" + HeadNode.Separator
            //                    + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                     + itemField.Length + HeadNode.Separator
            //                      + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //    sLine = "YS" + HeadNode.Separator
            //                    + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                     + itemField.Length + HeadNode.Separator
            //                      + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //}
            ////else if(sName=="YS")颜色
            ////   sName="";
            //else if (sName == "FontSize")
            //{
            //    sLine = "BS" + HeadNode.Separator
            //                  + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                   + itemField.Length + HeadNode.Separator
            //                    + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);

            //    sLine = "XZ" + HeadNode.Separator
            //                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                  + itemField.Length + HeadNode.Separator
            //                   + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //}
            ////else if(sName=="XZ")形状
            ////   sName="";
            //else if (sName == "Underline")
            //{
            //    sLine = "XHX" + HeadNode.Separator
            //                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                  + itemField.Length + HeadNode.Separator
            //                   + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //}
            //else if (sName == "CharacterWidth")
            //{

            //    sLine = "KD" + HeadNode.Separator
            //                     + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                      + itemField.Length + HeadNode.Separator
            //                       + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);

            //    sLine = "GD" + HeadNode.Separator
            //                    + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                     + itemField.Length + HeadNode.Separator
            //                      + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //}
            //else if (sName == "WordSpacing")
            //{
            //    sLine = "JG" + HeadNode.Separator
            //                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                  + itemField.Length + HeadNode.Separator
            //                   + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //}
            //else if (sName == "Angle")
            //{
            //    sLine = "ZJFX" + HeadNode.Separator
            //                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                  + itemField.Length + HeadNode.Separator
            //                   + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);

            //    sLine = "ZJDZXJXZB" + HeadNode.Separator
            //                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                  + itemField.Length + HeadNode.Separator
            //                   + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);

            //    sLine = "ZJDZXJYZB" + HeadNode.Separator
            //                 + GetVCTFieldType(itemField.Type) + HeadNode.Separator
            //                  + itemField.Length + HeadNode.Separator
            //                   + itemField.Precision;
            //    pFieldNode = new FieldNode(sLine);
            //    pListFieldNode.Add(pFieldNode);
            //}

        }
        /// <summary>
        /// 将esri的字段类型转换为VCT格式的字段类型
        /// </summary>
        /// <param name="pEsriFieldType"></param>
        /// <returns></returns>
        private string GetVCTFieldType(esriFieldType pEsriFieldType)
        {
            string strType = "Varchar";
            switch (pEsriFieldType)
            {
                case esriFieldType.esriFieldTypeBlob:
                    break;
                case esriFieldType.esriFieldTypeDate:
                    strType = "Datetime";
                    break;
                case esriFieldType.esriFieldTypeDouble:
                    strType = "Float";
                    break;
                case esriFieldType.esriFieldTypeGUID:
                    break;
                case esriFieldType.esriFieldTypeGeometry:
                    break;
                case esriFieldType.esriFieldTypeGlobalID:
                    break;
                case esriFieldType.esriFieldTypeInteger:
                    strType = "Int";
                    break;
                case esriFieldType.esriFieldTypeOID:
                    strType = "Int";
                    break;
                case esriFieldType.esriFieldTypeRaster:
                    break;
                case esriFieldType.esriFieldTypeSingle:
                    strType = "Float";
                    break;
                case esriFieldType.esriFieldTypeSmallInteger:
                    strType = "Int";
                    break;
                case esriFieldType.esriFieldTypeString:
                    strType = "Char";
                    break;
                case esriFieldType.esriFieldTypeXML:
                    break;
                default:
                    break;
            }
            return strType;
        }

        /// <summary>
        /// 创建
        /// </summary>
        public virtual bool Create(TableStructureNode tableStructureNode)
        {
            m_TableStructureNode = tableStructureNode;

            return true;
        }

        /// <summary>
        /// 创建字段索引
        /// </summary>
        public bool UpdateFieldIndex()
        {
            if (this.m_pITable != null && this.StructureNode != null)
            {
                for (int i = 0; i < m_TableStructureNode.FieldNodes.Count; i++)
                {
                    m_TableStructureNode.FieldNodes[i].FieldIndex = m_pITable.FindField(m_TableStructureNode.FieldNodes[i].FieldName);
                    ///add by zengping 2011-4-25 含有标识码的字段集合，将标识码字段放在集合的第一位
                    if (m_TableStructureNode.FieldNodes[i].FieldName == "BSM")
                    {
                        m_TableStructureNode.FieldNodes.Insert(0, m_TableStructureNode.FieldNodes[i]);
                        m_TableStructureNode.FieldNodes.RemoveAt(i + 1);
                    }
                }
                
                return true;
            }
            return false;
        }


        /// <summary>
        /// 获取VCT表节点
        /// </summary>
        public TableNode GetTableNode()
        {
            try
            {
                TableNode pTableNode = new TableNode();
                pTableNode.TableName = (this.m_pITable as IDataset).Name;
                return pTableNode;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 将vct属性表数据插入到对应的属性表中
        /// </summary>
        /// <param name="recordNode"></param>
        public void InsertAttibuteTableRecord(RecordNode recordNode)
        {
            try
            {
                IRow pRow = m_pITable.CreateRow();
                if (pRow != null)
                {
                    /////遍历要素的字段集合，除去空间数据相关字段
                    //List<IField> pListField = new List<IField>();
                    //for (int i = 0; i < m_pIFeature.Fields.FieldCount; i++)
                    //{
                    //    IField pField = m_pIFeature.Fields.get_Field(i);
                    //    if (pField.Type != esriFieldType.esriFieldTypeGeometry
                    //        && pField.Type != esriFieldType.esriFieldTypeOID
                    //        && pField.Name.ToUpper() != "SHAPE_LENGTH"
                    //        && pField.Name.ToUpper() != "SHAPE_AREA")
                    //    {
                    //        pListField.Add(pField);
                    //    }
                    //}
                    /////遍历属性字段集合，进行赋值
                    //for (int j = 0; j < pListField.Count; j++)
                    //{
                    //    ///查找到对应的字段索引
                    //    int index = m_pIFeature.Fields.FindField(pListField[j].Name);
                    //    string sValue = recordNode.FieldValues[j];
                    //    if (index != -1)
                    //        m_pIFeature.set_Value(index, sValue);

                    //}
                    ///按照表结构插入数据
                    for (int i = 0; i < StructureNode.FieldNodes.Count; i++)
                    {
                        FieldNode pFieldNode = StructureNode.FieldNodes[i];
                        if(pFieldNode.FieldIndex!=-1)
                            pRow.set_Value(pFieldNode.FieldIndex, recordNode.FieldValues[i]);
                    }
                    pRow.Store();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
        }

        private List<RecordNode> m_RecordNodes;
        /// <summary>
        /// 属性集合
        /// </summary>
        public List<RecordNode> RecordNodes
        {
            get
            {
                if (m_RecordNodes == null || m_RecordNodes.Count == 0)
                   m_RecordNodes = GetRecordNodes();
                return m_RecordNodes;
            }
        }

        /// <summary>
        /// 获取属性表数据集合
        /// </summary>
        /// <returns></returns>
        public List<RecordNode> GetRecordNodes()
        {
            try
            {
                List<RecordNode> pListNodes = new List<RecordNode>();
                ///遍历属性表数据
                ICursor pCursor = m_pITable.Search(null,false);
                IRow pRow = pCursor.NextRow();
                while (pRow!=null)
                {
                    RecordNode pRecordNode = new RecordNode();
                    pRecordNode.FieldValues = new List<string>();

                    ///按照表结构中获取到的字段索引添加数据
                    for (int i = 0; i < StructureNode.FieldNodes.Count; i++)
                    {
                        FieldNode pFieldNode = StructureNode.FieldNodes[i];
                        if(pFieldNode.FieldIndex!=-1)
                            pRecordNode.FieldValues.Add( pRow.get_Value(pFieldNode.FieldIndex).ToString());
                    }
                    pListNodes.Add(pRecordNode);
                    pRow = pCursor.NextRow();
                }
                m_RecordNodes = pListNodes;
                return pListNodes;
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return null;
            }
        }

        public virtual void Disposing()
        {
            if(m_pITable!=null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pITable);
            m_pITable = null;
            GC.Collect();
        }
    }
}
