using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;
using System.Collections;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using stdole;
using ESRI.ArcGIS.DataSourcesFile;

namespace Common.Utility.Esri
{
    public class FeatClsOperAPI
    {
        /// <summary>
        /// 检查数据集是否有锁,一旦检测到数据集内有图层含有锁，就返回有锁信息
        /// </summary>
        /// <param name="ipFeatDataset">被检查的数据集</param>
        /// <returns>是否有锁</returns>
        public static bool CheckFeatureDatasetHasLock(IFeatureDataset ipFeatDataset)
        {
            IFeatureClassContainer ipFeatClassContainer = (IFeatureClassContainer)ipFeatDataset;
            int numFeatClasses = ipFeatClassContainer.ClassCount;

            IFeatureClass ipFeatClass = null;
            bool hasLock = false;

            for (int i = 0; i < numFeatClasses; i++)
            {
                ipFeatClass = ipFeatClassContainer.get_Class(i);
                if (CheckFeatureClassHasLock(ipFeatClass))
                {
                    hasLock = true;
                    break;
                }
            }
            return hasLock;
        }

        /// <summary>
        /// 检查数据集是否有被锁定
        /// </summary>
        /// <param name="ipFeatClass">被检查是否有表的图层</param>
        /// <returns>是否有锁</returns>
        public static bool CheckFeatureClassHasLock(IFeatureClass ipFeatClass)
        {
            ISchemaLock ipSchemaLock = (ISchemaLock)ipFeatClass;
            if (ipSchemaLock == null)
            {
                return false;
            }

            //通过设置锁成功与否判断该图层是否被其他人使用
            ipSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);

            return false;
        }

        /// <summary>
        /// 获取IFeatureDataset所有的要素类，返回IFeatureClass列表
        /// </summary>
        /// <param name="ipDataset"></param>
        /// <param name="arrayFeatLayer"></param>
        public static void GetFeatLayerInDs(IFeatureDataset ipDataset, ref List<IFeatureLayer> arrayFeatLayer)
        {
            try
            {
                if (ipDataset == null)
                {
                    return;
                }

                IFeatureClassContainer ipFcContain = (IFeatureClassContainer)ipDataset;

                IEnumFeatureClass ipFcEnum = ipFcContain.Classes;

                IFeatureClass ipFtClass = ipFcEnum.Next();


                arrayFeatLayer = new List<IFeatureLayer>();

                while (ipFtClass != null)
                {
                    IFeatureLayer pFLayer = new FeatureLayer();
                    pFLayer.FeatureClass = ipFtClass;
                    pFLayer.Name = ipFtClass.AliasName;

                    arrayFeatLayer.Add(pFLayer);

                    Marshal.ReleaseComObject(ipFtClass);

                    ipFtClass = ipFcEnum.Next();
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }

        /// <summary>
        ///  获取IFeatureDataset所有的要素类，返回IDataset列表
        /// </summary>
        /// <param name="ipDataset"></param>
        /// <param name="arrayDs"></param>
        public static void GetDatasetInDs(IFeatureDataset ipDataset, ref List<IDataset> arrayDs)
        {
            try
            {
                if (ipDataset == null)
                {
                    return;
                }

                IFeatureClassContainer ipFcContain = (IFeatureClassContainer)ipDataset;

                IEnumFeatureClass ipFcEnum = ipFcContain.Classes;

                IFeatureClass ipFtClass = ipFcEnum.Next();

                while (ipFtClass != null)
                {
                    arrayDs.Add((IDataset)ipFtClass);

                    ipFtClass = ipFcEnum.Next();
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }

        /// <summary>
        ///  获取IFeatureDataset所有的要素类，返回IDataset列表
        /// </summary>
        /// <param name="ipDataset"></param>
        /// <param name="arrayDs"></param>
        public static void GetFcNameInDs(IFeatureDataset ipDataset, ref Hashtable arrayDs)
        {
            try
            {
                if (ipDataset == null)
                {
                    return;
                }

                IFeatureClassContainer ipFcContain = (IFeatureClassContainer)ipDataset;

                IEnumFeatureClass ipFcEnum = ipFcContain.Classes;

                IFeatureClass ipFtClass = ipFcEnum.Next();

                while (ipFtClass != null)
                {
                    arrayDs.Add(((IDataset)ipFtClass).Name, (IDataset)ipFtClass);

                    ipFtClass = ipFcEnum.Next();
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }

        /// <summary>
        ///  获取FeatureWorkspace中所有的图层和属性表
        /// </summary>
        /// <param name="ipWs"></param>
        /// <param name="arrayFtInFWS"></param>
        /// <param name="arrayTab"></param>
        public static void GetFeatClsInFtWS(IFeatureWorkspace ipWs, ref List<IFeatureLayer> arrayFtInFWS,
                                           ref List<ITable> arrayTab)
        {
            try
            {
                if (ipWs == null)
                {
                    return;
                }
                IWorkspace pWs = (IWorkspace)ipWs;
                IEnumDataset pEnumDs = pWs.get_Datasets(esriDatasetType.esriDTAny);
                IDataset pDs = pEnumDs.Next();
                while (pDs != null)
                {
                    esriDatasetType esriDSType = pDs.Type;
                    if (esriDSType == esriDatasetType.esriDTTable)
                    {
                        ITable pTable = (ITable)pDs;
                        //string strFSName = pDs.Name;
                        //string str = strFSName + "[表]";
                        arrayTab.Add(pTable);
                        pDs = pEnumDs.Next();
                    }
                    else if (esriDSType == esriDatasetType.esriDTFeatureClass) //找到要素类
                    {
                        IFeatureClass ipFtClass = (IFeatureClass)pDs;

                        IFeatureLayer pFLayer = new FeatureLayer();
                        pFLayer.FeatureClass = ipFtClass;
                        pFLayer.Name = ipFtClass.AliasName;

                        arrayFtInFWS.Add(pFLayer);

                        Marshal.ReleaseComObject(ipFtClass);

                        pDs = pEnumDs.Next();
                    }
                    else if (esriDSType == esriDatasetType.esriDTFeatureDataset) //找到要素集)
                    {
                        IFeatureDataset pFtDs = (IFeatureDataset)pDs;
                        GetFeatLayerInDs(pFtDs, ref arrayFtInFWS);
                        pDs = pEnumDs.Next();
                    }
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }


        /// <summary>
        ///  获取FeatureWorkspace中所有的图层和表
        /// </summary>
        /// <param name="ipWs"></param>
        /// <param name="arrayDsInFWS"></param>
        public static void GetFcNameInFtWS(IFeatureWorkspace ipWs, ref Hashtable arrayDsInFWS)
        {
            try
            {
                if (ipWs == null)
                {
                    return;
                }
                IWorkspace pWs = (IWorkspace)ipWs;
                IEnumDataset pEnumDs = pWs.get_Datasets(esriDatasetType.esriDTAny);
                IDataset pDs = pEnumDs.Next();
                while (pDs != null)
                {
                    esriDatasetType esriDSType = pDs.Type;
                    if (esriDSType == esriDatasetType.esriDTTable) //找到属性表
                    {

                        arrayDsInFWS.Add(pDs.Name, pDs);
                        pDs = pEnumDs.Next();
                    }
                    else if (esriDSType == esriDatasetType.esriDTFeatureClass) //找到要素类
                    {
                        arrayDsInFWS.Add(pDs.Name, pDs);

                        pDs = pEnumDs.Next();
                    }
                    else if (esriDSType == esriDatasetType.esriDTFeatureDataset) //找到要素集)
                    {
                        IFeatureDataset pFtDs = (IFeatureDataset)pDs;
                        GetFcNameInDs(pFtDs, ref arrayDsInFWS);
                        pDs = pEnumDs.Next();
                    }
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return;
            }
        }

        /// <summary>
        /// 创建注记层
        /// </summary>
        /// <param name="strFeaClsName">注记层名称</param>
        /// <param name="destDataset">待创建的注记层所在的featuredataset</param>
        /// <param name="ipInputFields">注记层字段</param>
        public static IFeatureClass CreateAnnotation(IFeatureClass pInputCls, IFeatureDataset destDataset, IFields ipInputFields)
        {
            try
            {
                //要素类标识信息
                IObjectClassDescription ipObjectClassDesc = new AnnotationFeatureClassDescription();
                IFeatureClassDescription ipFeatClassDesc = (IFeatureClassDescription)ipObjectClassDesc;


                IAnnoClass pAnnoClass = pInputCls.Extension as IAnnoClass;
                double scale = pAnnoClass.ReferenceScale;


                IUID ipCLSID = ipObjectClassDesc.InstanceCLSID;
                ipCLSID.Value = "esriCore.AnnotationFeature";


                IUID ipExtCLSID = ipObjectClassDesc.ClassExtensionCLSID;
                ipExtCLSID.Value = "esriCore.AnnotationFeatureClassExtension";

                //IField ipField;
                IFields ipFields = ipObjectClassDesc.RequiredFields;
                IFieldsEdit ipFieldsEdit = (IFieldsEdit)ipFields;
                int numFields = ipInputFields.FieldCount;

                esriFieldType type;
                for (int i = 0; i < numFields; i++)
                {
                    IField ipField = ipInputFields.get_Field(i);
                    type = ipField.Type;
                    if (type != esriFieldType.esriFieldTypeOID && type != esriFieldType.esriFieldTypeGeometry)
                    {
                        string fldName = ipField.Name;
                        int fldIndex = ipFields.FindField(fldName);

                        if (fldIndex == -1)
                            ipFieldsEdit.AddField(ipField);
                    }
                }

                //工作空间
                IWorkspace ipWorkspace = destDataset.Workspace;
                IFeatureWorkspaceAnno ipFeatureWorkspaceAnno = (IFeatureWorkspaceAnno)ipWorkspace;


                //显示比例
                IGraphicsLayerScale ipGraphicsLayerScale = new GraphicsLayerScaleClass();
                ipGraphicsLayerScale.ReferenceScale = scale;
                ipGraphicsLayerScale.Units = pAnnoClass.ReferenceScaleUnits;

                //符号信息
                //' set up symbol collection
                ISymbolCollection pSymbolColl = new SymbolCollectionClass();

                ITextSymbol myTxtSym = new TextSymbolClass();
                //Set the font for myTxtSym
                stdole.IFontDisp myFont = new stdole.StdFontClass() as stdole.IFontDisp;
                IFont pFt = (IFont)myFont;
                pFt.Name = "Courier New";

                myTxtSym.Font = myFont;

                // Set the Color for myTxtSym to be Dark Red
                IRgbColor myColor = new RgbColorClass();
                myColor.Red = 150;
                myColor.Green = 0;
                myColor.Blue = 0;
                myTxtSym.Color = myColor;

                // Set other properties for myTxtSym
                myTxtSym.Angle = 0;
                myTxtSym.RightToLeft = false;
                myTxtSym.VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline;
                myTxtSym.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;
                myTxtSym.Size = 9;

                ISymbol pSymbol = (ISymbol)myTxtSym;
                pSymbolColl.set_Symbol(0, pSymbol);


                //set up the annotation labeling properties including the expression
                IAnnotateLayerProperties pAnnoProps = new LabelEngineLayerPropertiesClass();
                pAnnoProps.FeatureLinked = true;
                pAnnoProps.AddUnplacedToGraphicsContainer = false;
                pAnnoProps.CreateUnplacedElements = true;
                pAnnoProps.DisplayAnnotation = true;
                pAnnoProps.UseOutput = true;

                ILabelEngineLayerProperties pLELayerProps = (ILabelEngineLayerProperties)pAnnoProps;
                IAnnotationExpressionEngine aAnnoVBScriptEngine = new AnnotationVBScriptEngineClass();
                pLELayerProps.ExpressionParser = aAnnoVBScriptEngine;
                pLELayerProps.Expression = "[DESCRIPTION]";
                pLELayerProps.IsExpressionSimple = true;
                pLELayerProps.Offset = 0;
                pLELayerProps.SymbolID = 0;
                pLELayerProps.Symbol = myTxtSym;

                IAnnotateLayerTransformationProperties pATP = (IAnnotateLayerTransformationProperties)pAnnoProps;
                double dRefScale = ipGraphicsLayerScale.ReferenceScale;
                pATP.ReferenceScale = dRefScale;
                pATP.Units = esriUnits.esriMeters;
                pATP.ScaleRatio = 1;

                IAnnotateLayerPropertiesCollection pAnnoPropsColl = new AnnotateLayerPropertiesCollectionClass();
                pAnnoPropsColl.Add(pAnnoProps);

                //' use the AnnotationFeatureClassDescription co - class to get the list of required fields and the default name of the shape field
                IObjectClassDescription pOCDesc = new AnnotationFeatureClassDescriptionClass();
                IFeatureClassDescription pFDesc = (IFeatureClassDescription)pOCDesc;

                IFields pReqFields = pOCDesc.RequiredFields;
                IUID pInstCLSID = pOCDesc.InstanceCLSID;
                IUID pExtCLSID = pOCDesc.ClassExtensionCLSID;
                string bsShapeFieldName = pFDesc.ShapeFieldName;

                IDataset pDs = (IDataset)pInputCls;

                //创建
                string bstName = ipFeatClassDesc.ShapeFieldName;
                IFeatureClass pOutFcls =
                    ipFeatureWorkspaceAnno.CreateAnnotationClass(pDs.Name, ipFields, pOCDesc.InstanceCLSID,
                                                                 pOCDesc.ClassExtensionCLSID, pInputCls.ShapeFieldName, "", destDataset, null, pAnnoPropsColl,
                                                                 ipGraphicsLayerScale, pSymbolColl, true);
                return pOutFcls;
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
            return null;
        }

        /// <summary>
        /// 创建并导入注记层
        /// </summary>
        /// <param name="pInputCls"></param>
        /// <param name="Ws"></param>
        public static void ConvertAnnotation(IFeatureClass pInputCls, IWorkspace Ws)
        {
            try
            {


                IFields pFields = pInputCls.Fields;

                IEnumDataset pEnumDataset = Ws.get_Datasets(esriDatasetType.esriDTFeatureDataset);
                IFeatureDataset ipDataset = (IFeatureDataset)pEnumDataset.Next();
                IFeatureClass pOutputCls = CreateAnnotation(pInputCls, ipDataset, pFields);

                if (pOutputCls == null)
                {
                    return;
                }

                IFeatureCursor pInputCursor = pInputCls.Search(null, false);
                IFeature Inputfeature = pInputCursor.NextFeature();
                int index = -1;

                //获取输入图层的注记要素
                IElement ipElement = null;
                IAnnotationFeature ipAnno = null;
                IFeatureCursor _featureCursor;
                IFeatureBuffer _featureBuffer;
                IFields pOutputFields;
                IField ipField;
                object value;
                esriFieldType type;
                IAnnotationFeature ipAnnoOutput;
                IGeometry ipGeom;
                while (Inputfeature != null)
                {

                    if (Inputfeature.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        ipAnno = (IAnnotationFeature)Inputfeature;
                        ipElement = ipAnno.Annotation;
                    }
                    _featureCursor = pOutputCls.Insert(true);
                    _featureBuffer = pOutputCls.CreateFeatureBuffer();

                    pOutputFields = pOutputCls.Fields;
                    for (int k = 0; k < pFields.FieldCount; k++)
                    {

                        ipField = pFields.get_Field(k);
                        value = Inputfeature.get_Value(k);
                        if (ipField.Name.ToUpper().Contains("SHAPE") || value == null || value.ToString() == "")
                        {
                            continue;

                        }
                        type = ipField.Type;
                        if (type != esriFieldType.esriFieldTypeOID && type != esriFieldType.esriFieldTypeGeometry)
                        {
                            int indexField = pOutputFields.FindField(ipField.Name);
                            try
                            {
                                _featureBuffer.set_Value(indexField, value);
                            }
                            catch(Exception exp)
                            {
                                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());


                            }

                        }
                        Marshal.ReleaseComObject(ipField);  //释放接口，hehy-20090303

                    }

                    //在输出图层中插入注记要素
                    ipAnnoOutput = (IAnnotationFeature)_featureBuffer;
                    ipAnnoOutput.Annotation = ipElement;
                    ipGeom = Inputfeature.ShapeCopy;
                    _featureBuffer.Shape = ipGeom;

                    _featureCursor.InsertFeature(_featureBuffer); //将输出图层中的要素保存

                    Marshal.ReleaseComObject(_featureCursor);  //释放接口，hehy-20090303
                    Marshal.ReleaseComObject(_featureBuffer);
                    Marshal.ReleaseComObject(Inputfeature);
                    Marshal.ReleaseComObject(ipAnnoOutput);

                    Inputfeature = pInputCursor.NextFeature();
                }

                Marshal.ReleaseComObject(pInputCursor);
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
        }

        /// <summary>
        /// 创建shape图层
        /// </summary>
        /// <param name="strShpPath"></param>
        /// <param name="strFtName"></param>
        /// <returns></returns>
        public static IFeatureClass CreateShpFile(string strShpPath, string strFtName, string strAliasFtName, ISpatialReference pSpatial)
        {
            string connectionstring = "DATABASE=" + strShpPath;
            IWorkspaceFactory2 pFactory = (IWorkspaceFactory2)new ShapefileWorkspaceFactoryClass();
            IWorkspace workspace = pFactory.OpenFromString(connectionstring, 0);
            IFeatureWorkspace ipFtWs = (IFeatureWorkspace)workspace;

            //创建字段IFields
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            ///创建几何类型字段
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;

            ////设置FID字段
            //IFieldEdit ipFldEdit = new FieldClass(); //(__uuidof(Field));
            //ipFldEdit.Name_2 = "FID";
            //ipFldEdit.AliasName_2 = "唯一标志码";
            //ipFldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            //pFieldsEdit.AddField(ipFldEdit);


            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.AliasName_2 = "几何类型";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;


            IGeometryDef pGeomDef = new GeometryDefClass();
            IGeometryDefEdit pGeomDefEdit = (IGeometryDefEdit)pGeomDef;
            pGeomDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            pGeomDefEdit.SpatialReference_2 = pSpatial;
            pFieldEdit.GeometryDef_2 = pGeomDef;
            pFieldsEdit.AddField(pField);


            IFeatureClass _featureClass =
                ipFtWs.CreateFeatureClass(strFtName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            //更新图层别名
            //IClassSchemaEdit ipEdit = (IClassSchemaEdit)_featureClass;
            //ipEdit.AlterAliasName(strAliasFtName);

            pFactory = null;
            workspace = null;
            ipFtWs = null;

            return _featureClass;
        }

        /// <summary>
        /// 给图层添加字段
        /// </summary>
        /// <param name="strFieldCode"></param>
        /// <param name="strFieldName"></param>
        /// <param name="pFtCls"></param>
        public static void AddField(string strFieldCode, string strFieldName, Type pType, int nLength,
                                    IFeatureClass pFtCls)
        {
            try
            {
                ///创建一新字段
                esriFieldType fieldType = GetFieldType(pType);
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Length_2 = nLength;
                pFieldEdit.Name_2 = strFieldName;
                pFieldEdit.AliasName_2 = strFieldName;
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = true;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;

                pFtCls.AddField(pField);
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
        }

        /// <summary>
        /// 将type类型转换为esriFieldType
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public static esriFieldType GetFieldType(Type pType)
        {
            esriFieldType fieldtype = esriFieldType.esriFieldTypeString;
            string strType = pType.Name;

            switch (strType.ToLower())
            {
                case "int32":
                    fieldtype = esriFieldType.esriFieldTypeInteger;
                    break;
                case "string":
                    fieldtype = esriFieldType.esriFieldTypeString;
                    break;
                case "double":
                    fieldtype = esriFieldType.esriFieldTypeDouble;
                    break;
                case "float":
                    fieldtype = esriFieldType.esriFieldTypeSingle;
                    break;
                default:
                    fieldtype = esriFieldType.esriFieldTypeString;
                    break;
            }
            return fieldtype;
        }

        /// <summary>
        /// 根据某一图层的范围，创建mdb文件中的featuredataset及其空间范围
        /// </summary>
        /// <param name="motherWs">要创建featuredataset的工作空间</param>
        /// <param name="pGeoDataset">featuredataset所要依据的空间参考和空间范围</param>
        /// 这个函数要改！！
        public static void CreateDatasetInWs(IWorkspace motherWs, IGeoDataset pGeoDataset,string datasetName)
        {
            try
            {
                ISpatialReference pSpatialRef = null;
                IFeatureDataset pFtDs = null;


                if (pGeoDataset != null)
                {
                    pSpatialRef = pGeoDataset.SpatialReference;

                    IControlPrecision2 controlPrecision2 = pSpatialRef as IControlPrecision2;
                    if (!controlPrecision2.IsHighPrecision)
                        controlPrecision2.IsHighPrecision = true;

                    IEnvelope pEnv = pGeoDataset.Extent;
                    pEnv.Expand(1.5, 1.5, true);
                    pSpatialRef.SetDomain(pEnv.XMin, pEnv.XMax, pEnv.YMin, pEnv.YMax);

                    ISpatialReferenceTolerance pSpatialTolerance = (ISpatialReferenceTolerance)pSpatialRef;
                    double dXYTolerance = pSpatialTolerance.XYTolerance;
                    double dZTolerance = pSpatialTolerance.ZTolerance;
                    ISpatialReferenceResolution pSpatialRefResolution = (ISpatialReferenceResolution)pSpatialRef;
                    pSpatialRefResolution.set_XYResolution(true, dXYTolerance * 0.1);
                    pSpatialRefResolution.set_ZResolution(true, dZTolerance * 0.1);
                    pSpatialRefResolution.MResolution = pSpatialTolerance.MTolerance * 0.1;

                    pFtDs = ((IFeatureWorkspace)motherWs).CreateFeatureDataset(datasetName, pSpatialRef);
                }
                else
                {
                    pSpatialRef = new UnknownCoordinateSystemClass();

                    ISpatialReferenceTolerance pSpatialTolerance = (ISpatialReferenceTolerance)pSpatialRef;
                    double dXYTolerance = pSpatialTolerance.XYTolerance;
                    double dZTolerance = pSpatialTolerance.ZTolerance;
                    ISpatialReferenceResolution pSpatialRefResolution = (ISpatialReferenceResolution)pSpatialRef;
                    pSpatialRefResolution.set_XYResolution(true, dXYTolerance * 0.1);
                    pSpatialRefResolution.set_ZResolution(true, dZTolerance * 0.1);
                    pSpatialRefResolution.MResolution = pSpatialTolerance.MTolerance * 0.1;

                    pFtDs = ((IFeatureWorkspace)motherWs).CreateFeatureDataset(datasetName, pSpatialRef);
                }
            }
            catch(Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());


            }
        }
    }
}
