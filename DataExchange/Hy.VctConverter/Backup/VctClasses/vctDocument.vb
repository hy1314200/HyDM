Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem

Public Enum LayerType
    UnKnown = 0
    Point = 1
    Line = 2
    Polygon = 3
    Annotation = 4
    Graphics = 5
    Raster = 6
    Topology = 7
    Tin = 8
    RasterCatalog = 9
End Enum

''' <summary>
''' 最终生成一个数据表
''' </summary>
''' <remarks></remarks>
Public Class vctDocument
    ''Implements IvctDocument

#Region "内部变量"

    Private p_IsOpen As Boolean
    ''VCT头部
    Private p_Header As New vctHeader

    Private p_Map As New vctMap

    Private m_FileName As String = ""

    Private m_Count As Integer = 0

    Private m_TableName As String = ""
    ''点层
    Private m_Point As vctPoint
    ''线层
    Private m_Line As vctLine
    Private m_PointCount As Integer = 2
    ''面层
    Private m_Polygon As vctPolygon

    Private m_Annotation As vctAnnotation

#End Region

#Region "属性"

    Public ReadOnly Property IsOpen() As Boolean
        Get
            Return Me.p_IsOpen
        End Get
    End Property

    Public ReadOnly Property Header() As vctHeader
        Get
            Return Me.p_Header
        End Get
    End Property

    Public ReadOnly Property Map() As vctMap
        Get
            Return Me.p_Map
        End Get
    End Property

#End Region

#Region "公有函数"

    ''' <summary>
    ''' 打开VCT文件
    ''' 读取头部Head /Layer/TableStruct 部分
    ''' </summary>
    ''' <param name="p_FileName"></param>
    ''' <remarks></remarks>
    Public Sub Open(ByVal p_FileName As String)
        Dim mTextReader As TextReader = New StreamReader(p_FileName, System.Text.Encoding.Default)

        Me.m_FileName = p_FileName

        '读取头部
        Dim mLine As String = mTextReader.ReadLine
        Try
            Dim mStr As String = ""
            While mLine IsNot Nothing
                If mLine.ToUpper.Trim.StartsWith("HEADBEGIN") Then
                    mStr = "ReadHead"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("FEATURECODEBEGIN") Then
                    Me.Map.Layers.Clear()
                    mStr = "ReadFeatureCode"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("TABLESTRUCTUREBEGIN") Then
                    Me.m_TableName = ""
                    mStr = "ReadTableStructure"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("POINTBEGIN") Then
                    Exit While
                ElseIf mLine.ToUpper.Trim.StartsWith("LINEBEGIN") Then
                    Exit While
                ElseIf mLine.ToUpper.Trim.StartsWith("POLYGONBEGIN") Then
                    Exit While
                ElseIf mLine.ToUpper.Trim.StartsWith("ANNOTATIONBEGIN") Then
                    Exit While
                ElseIf mLine.ToUpper.Trim.StartsWith("ATTRIBUTEBEGIN") Then
                    Exit While
                ElseIf mLine.ToUpper.Trim.StartsWith("ATTRIBUTEEND") Then
                    Exit While
                End If

                Select Case mStr
                    Case "ReadHead" '读取文件头
                        If Not mLine.ToUpper.Trim.StartsWith("HEADEND") Then
                            Me.ReadHeader(mLine)
                        End If
                    Case "ReadFeatureCode"
                        If Not mLine.ToUpper.Trim.StartsWith("FEATURECODEEND") Then
                            Me.ReadLayer(mLine)
                        End If
                    Case "ReadTableStructure"
                        If Not mLine.ToUpper.Trim.StartsWith("TABLESTRUCTUREEND") Then
                            Me.ReadTablestructure(mLine)
                        End If
                    Case "ReadPoint"
                        'If Not mLine.ToUpper.Trim.StartsWith("POINTEND") Then
                        '    Me.ReadPoint(mLine)
                        'End If
                    Case "ReadLine"
                        'If Not mLine.ToUpper.Trim.StartsWith("LINEEND") Then
                        '    Me.ReadLine(mLine)
                        'End If
                    Case "ReadPolygon"
                        'If Not mLine.ToUpper.Trim.StartsWith("POLYGONEND") Then
                        '    Me.ReadPolygon(mLine)
                        'End If
                    Case "ReadAnnotation"
                        'If Not mLine.ToUpper.Trim.StartsWith("ANNOTATIONEND") Then
                        '    Me.ReadAnnotation(mLine)
                        'End If
                    Case "ReadAttribute"
                        'If Not mLine.ToUpper.Trim.StartsWith("ATTRIBUTEEND") Then
                        '    Me.ReadAttribute(mLine)
                        'End If
                End Select

                mLine = mTextReader.ReadLine
            End While

            Me.p_IsOpen = True
        Catch ex As ApplicationException
            Me.p_IsOpen = False
            Throw ex
        Finally
            mTextReader.Close()
        End Try
    End Sub

    ''' <summary>
    ''' 读取VCT中点/线/面/注记要素的数据
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadData()
        Dim mTextReader As TextReader = New StreamReader(Me.m_FileName, System.Text.Encoding.Default)
        '读取头部
        Dim mLine As String = mTextReader.ReadLine
        Try
            Dim mStr As String = ""
            While mLine IsNot Nothing
                If mLine.ToUpper.Trim.StartsWith("HEADBEGIN") Then
                    mStr = "ReadHead"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("FEATURECODEBEGIN") Then
                    mStr = "ReadFeatureCode"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("TABLESTRUCTUREBEGIN") Then
                    Me.m_TableName = ""
                    mStr = "ReadTableStructure"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("POINTBEGIN") Then
                    mStr = "ReadPoint"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("LINEBEGIN") Then
                    mStr = "ReadLine"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("POLYGONBEGIN") Then
                    mStr = "ReadPolygon"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("ANNOTATIONBEGIN") Then
                    mStr = "ReadAnnotation"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("ATTRIBUTEBEGIN") Then
                    Me.m_TableName = ""
                    mStr = "ReadAttribute"
                    m_Count = 0
                    mLine = mTextReader.ReadLine
                    Continue While
                ElseIf mLine.ToUpper.Trim.StartsWith("ATTRIBUTEEND") Then
                    Exit While
                End If

                Select Case mStr
                    Case "ReadHead" '读取文件头
                        'If Not mLine.ToUpper.Trim.StartsWith("HEADEND") Then
                        '    Me.ReadHeader(mLine)
                        'End If
                    Case "ReadFeatureCode"
                        'If Not mLine.ToUpper.Trim.StartsWith("FEATURECODEEND") Then
                        '    Me.ReadLayer(mLine)
                        'End If
                    Case "ReadTableStructure"
                        'If Not mLine.ToUpper.Trim.StartsWith("TABLESTRUCTUREEND") Then
                        '    Me.ReadTablestructure(mLine)
                        'End If
                    Case "ReadPoint"
                        If Not mLine.ToUpper.Trim.StartsWith("POINTEND") Then
                            Me.ReadPoint(mLine)
                        End If
                    Case "ReadLine"
                        If Not mLine.ToUpper.Trim.StartsWith("LINEEND") Then
                            Me.ReadLine(mLine)
                        End If
                    Case "ReadPolygon"
                        If Not mLine.ToUpper.Trim.StartsWith("POLYGONEND") Then
                            Me.ReadPolygon(mLine)
                        End If
                    Case "ReadAnnotation"
                        If Not mLine.ToUpper.Trim.StartsWith("ANNOTATIONEND") Then
                            Me.ReadAnnotation(mLine)
                        End If
                    Case "ReadAttribute"
                        If Not mLine.ToUpper.Trim.StartsWith("ATTRIBUTEEND") Then
                            Me.ReadAttribute(mLine)
                        End If
                End Select

                mLine = mTextReader.ReadLine
            End While
        Catch ex As ApplicationException
            Throw ex
        Finally
            mTextReader.Close()
        End Try
    End Sub

    Public Function ConvertVctFileToSDE(ByVal p_Layer As vctLayer, ByVal p_Workspace As IWorkspace, ByVal p_LayerName As String, ByVal p_DatasetName As String, ByVal p_SpatialReference As ISpatialReference, ByRef p_ErrMessage As String) As Boolean
        If p_Workspace Is Nothing Then
            p_ErrMessage = "没有输出的工作空间！"
            Return False
        End If

        Dim mFields As IFields = Nothing
        If p_Layer.Fields.Count > 0 Then
            mFields = New Fields
            Dim mFieldsEdit As IFieldsEdit = mFields
            For i As Integer = 0 To p_Layer.Fields.Count - 1
                Dim mField As IField = New FieldClass
                Dim mFieldEdit As IFieldEdit = mField
                mFieldEdit.Name_2 = p_Layer.Fields(i).Name
                mFieldEdit.AliasName_2 = p_Layer.Fields(i).Name
                Select Case p_Layer.Fields(i).Type
                    Case vctFieldType.vctBoolean
                        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
                    Case vctFieldType.vctDateTime
                        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate
                    Case vctFieldType.vctDouble
                        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
                    Case vctFieldType.vctInt
                        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
                    Case vctFieldType.vctString
                        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeString
                    Case vctFieldType.vctVarbin
                        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeBlob
                End Select
                mFieldsEdit.AddField(mField)
            Next
        End If

        Try
            Dim mMapDBClient As New MapDBClient

            Dim mWorkSpace As Object = p_Workspace
            If p_DatasetName IsNot Nothing Then
                If p_DatasetName.Trim <> "" Then
                    Dim mFeatureWorkspace As IFeatureWorkspace = p_Workspace
                    Try
                        mWorkSpace = mFeatureWorkspace.OpenFeatureDataset(p_DatasetName)
                    Catch ex As Exception
                        '创建一个要素集
                        mWorkSpace = mFeatureWorkspace.CreateFeatureDataset(p_DatasetName, p_SpatialReference)
                    End Try
                End If
            End If

            Dim mFeatureClass As IFeatureClass = Nothing

            Select Case p_Layer.LayerType
                Case vctLayerType.Annotation
                    Dim mUnit As esriUnits = esriUnits.esriMeters
                    Select Case Me.Header.Unit
                        Case "M"
                            mUnit = esriUnits.esriMeters
                    End Select
                    mFeatureClass = mMapDBClient.CreateAnnotationClass(mWorkSpace, p_LayerName, mFields, p_SpatialReference, Me.Header.Scale, mUnit)
                Case vctLayerType.Point
                    mFeatureClass = mMapDBClient.CreateFeatureClass(mWorkSpace, p_LayerName, p_SpatialReference, esriFeatureType.esriFTSimple, esriGeometryType.esriGeometryPoint, mFields, "")
                Case vctLayerType.Line
                    mFeatureClass = mMapDBClient.CreateFeatureClass(mWorkSpace, p_LayerName, p_SpatialReference, esriFeatureType.esriFTSimple, esriGeometryType.esriGeometryPolyline, mFields, "")
                Case vctLayerType.Polygon
                    mFeatureClass = mMapDBClient.CreateFeatureClass(mWorkSpace, p_LayerName, p_SpatialReference, esriFeatureType.esriFTSimple, esriGeometryType.esriGeometryPolygon, mFields, "")
                Case vctLayerType.Image
                    p_ErrMessage = "影像层没有创建"
                    Return True
            End Select

            If mFeatureClass Is Nothing Then
                Return False
            End If

            '导入数据
            '1.找到对应的图形数据并生成图形
            Select Case p_Layer.LayerType
                Case vctLayerType.Annotation
                    For j As Integer = 0 To p_Layer.Annotations.Count - 1
                        Try
                            Dim mFeature As IFeature = mFeatureClass.CreateFeature
                            Dim mAnnoFeature As IAnnotationFeature = mFeature
                            mAnnoFeature.Annotation = p_Layer.Annotations(j).GetElement

                            Dim mRow As DataRow = Me.GetAttrFromAttributeTable(p_Layer.Annotations(j).ID, p_Layer.DataTable.Columns(0).ColumnName, p_Layer.DataTable)

                            Me.SaveAttributeOfFeature(mRow, mFeature)
                            '保存注记的关联要素信息?
                            mFeature.Store()
                        Catch ex As Exception
                            p_ErrMessage += "创建注记要素" + p_LayerName + "时错误，错误原因：" + ex.ToString + vbCrLf
                        End Try
                    Next
                Case vctLayerType.Point
                    For j As Integer = 0 To p_Layer.Points.Count - 1
                        Dim mPoint As IPoint = p_Layer.Points(j).ConvertToEsriGeometry()
                        Dim mFeature As IFeature = mFeatureClass.CreateFeature
                        '赋属性
                        Try
                            mFeature.Shape = mPoint

                            Dim mRow As DataRow = Me.GetAttrFromAttributeTable(p_Layer.Points(j).ID, p_Layer.DataTable.Columns(0).ColumnName, p_Layer.DataTable)

                            Me.SaveAttributeOfFeature(mRow, mFeature)

                            mFeature.Store()
                        Catch ex As Exception
                            p_ErrMessage += "创建" + p_LayerName + "要素错误，错误原因：" + ex.ToString + vbCrLf
                        End Try
                    Next
                Case vctLayerType.Line
                    For j As Integer = 0 To p_Layer.Lines.Count - 1
                        Dim mLine As IPolyline = p_Layer.Lines(j).ConvertToEsriGeometry()
                        Dim mFeature As IFeature = mFeatureClass.CreateFeature
                        Try
                            mFeature.Shape = mLine

                            Dim mRow As DataRow = Me.GetAttrFromAttributeTable(p_Layer.Lines(j).ID, p_Layer.DataTable.Columns(0).ColumnName, p_Layer.DataTable)

                            Me.SaveAttributeOfFeature(mRow, mFeature)

                            mFeature.Store()
                        Catch ex As Exception
                            p_ErrMessage += "创建" + p_LayerName + "要素错误，错误原因：" + ex.ToString + vbCrLf
                        End Try
                    Next
                Case vctLayerType.Polygon
                    For j As Integer = 0 To p_Layer.Polygons.Count - 1
                        Dim mPolygon As IPolygon = p_Layer.Polygons(j).ConvertToEsriGeometry()
                        Dim mFeature As IFeature = mFeatureClass.CreateFeature
                        Try
                            mFeature.Shape = mPolygon

                            Dim mRow As DataRow = Me.GetAttrFromAttributeTable(p_Layer.Polygons(j).ID, p_Layer.DataTable.Columns(0).ColumnName, p_Layer.DataTable)

                            Me.SaveAttributeOfFeature(mRow, mFeature)

                            mFeature.Store()
                        Catch ex As Exception
                            p_ErrMessage += "创建" + p_LayerName + "要素错误，错误原因：" + ex.ToString + vbCrLf
                        End Try
                    Next
            End Select

            Return True
        Catch ex As Exception
            p_ErrMessage = ex.Message
            Return False
        End Try
    End Function

    Public Function ConvertFeatureClassToVCTFile(ByVal p_OutputFileName As String, ByVal p_FeatureClass As IFeatureClass, ByVal p_OutName As String, ByVal p_SpatialReference As ISpatialReference, ByRef p_ErrMessage As String) As Boolean
        Dim mStreamWriter As StreamWriter = New StreamWriter(p_OutputFileName, False, System.Text.Encoding.Default)

        Dim mGeoDataset As IGeoDataset = p_FeatureClass
        Dim mSpatialReference As ISpatialReference = mGeoDataset.SpatialReference
        If p_SpatialReference IsNot Nothing Then
            mSpatialReference = p_SpatialReference
        End If

        Dim mVctHead As New vctHeader
        mVctHead.SetSpatialReference(mSpatialReference, p_ErrMessage)
        mVctHead.WriteToVctFile(mStreamWriter)
        mStreamWriter.WriteLine("")

        Dim mVctFeatureClassList As New List(Of vctOutFeatureClass)
        Dim mVctFeatureClass As vctOutFeatureClass = Me.GetVctOutFeatureClass(p_FeatureClass, p_OutName, 1, 1)
        mVctFeatureClassList.Add(mVctFeatureClass)

        Me.WriteFeatureCode(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteTableStructure(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WritePoint(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteLine(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WritePolygon(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteAnnotation(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteAttributeTable(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")

        mStreamWriter.Close()

        Return True
    End Function

    Public Function ConvertDatasetToVCTFile(ByVal p_OutputFileName As String, ByVal p_Dataset As IFeatureDataset, ByVal p_ConvertClassNames As List(Of String), ByVal p_OutClassNames As List(Of String), ByVal p_SpatialReference As ISpatialReference, ByRef p_ErrMessage As String) As Boolean
        Dim mStreamWriter As StreamWriter = New StreamWriter(p_OutputFileName, False, System.Text.Encoding.Default)

        Dim mGeoDataset As IGeoDataset = p_Dataset
        Dim mSpatialReference As ISpatialReference = mGeoDataset.SpatialReference
        If p_SpatialReference IsNot Nothing Then
            mSpatialReference = p_SpatialReference
        End If

        Dim mVctHead As New vctHeader
        mVctHead.SetSpatialReference(mSpatialReference, p_ErrMessage)
        mVctHead.WriteToVctFile(mStreamWriter)
        mStreamWriter.WriteLine("")

        Dim mVctFeatureClassList As New List(Of vctOutFeatureClass)

        Dim mEnumdataset As IEnumDataset = p_Dataset.Subsets

        Dim mFID As Integer = 1
        Dim mID As Int64 = 1

        Dim mDataSet As IDataset = mEnumdataset.Next

        If p_ConvertClassNames Is Nothing Then
            While mDataSet IsNot Nothing
                If TypeOf mDataSet Is IFeatureClass Then
                    Dim mFeatureClass As IFeatureClass = mDataSet

                    Dim mVctFeatureClass As vctOutFeatureClass = Me.GetVctOutFeatureClass(mFeatureClass, mDataSet.Name, mFID, mID)
                    mVctFeatureClassList.Add(mVctFeatureClass)

                    mFeatureClass = mEnumdataset.Next
                End If
            End While
        Else
            While mDataSet IsNot Nothing
                If TypeOf mDataSet Is IFeatureClass Then
                    Dim mFeatureClass As IFeatureClass = mDataSet
                    Dim mName As String = mDataSet.Name
                    If mName.Contains(".") Then
                        mName = mName.Substring(mName.LastIndexOf(".") + 1)
                    End If

                    Dim mIndex As Integer = p_ConvertClassNames.IndexOf(mName)

                    If mIndex >= 0 Then
                        Dim mVctFeatureClass As vctOutFeatureClass = Me.GetVctOutFeatureClass(mFeatureClass, p_OutClassNames(mIndex), mFID, mID)
                        mVctFeatureClassList.Add(mVctFeatureClass)
                    End If
                End If
                mDataSet = mEnumdataset.Next
            End While
        End If

        Me.WriteFeatureCode(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteTableStructure(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WritePoint(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteLine(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WritePolygon(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteAnnotation(mStreamWriter, mVctFeatureClassList)
        mStreamWriter.WriteLine("")
        Me.WriteAttributeTable(mStreamWriter, mVctFeatureClassList)

        mStreamWriter.Close()

        Return True
    End Function

#End Region

#Region "私有函数"

    Private Function SplitString(ByVal p_Str As String) As String()
        Dim mStr() As String = Nothing
        If p_Str.Contains(",") Then
            mStr = p_Str.Split(",")
        ElseIf p_Str.Contains("，") Then
            mStr = p_Str.Split("，")
        ElseIf p_Str.Contains(Me.Header.Separator) Then
            mStr = p_Str.Split(Me.Header.Separator)
        End If
        Return mStr
    End Function

    Private Sub ReadHeader(ByVal p_Line As String)
        Dim mStr() As String = Nothing
        Dim mSplitStr As String = ""
        If p_Line.Contains(":") Then
            mStr = p_Line.Split(":")
            mSplitStr = ":"
        ElseIf p_Line.Contains("：") Then
            mStr = p_Line.Split("：")
            mSplitStr = "："
        End If

        If mStr IsNot Nothing Then
            Dim mKeyWord As String = mStr(0).Trim.ToUpper
            Dim mValue As String = mStr(1).Trim
            If mKeyWord.Contains("DATAMARK") Then
                Me.Header.DataMark = mValue
            ElseIf mKeyWord.Contains("VERSION") Then
                Me.Header.Version = mValue
            ElseIf mKeyWord.Contains("UNIT") Then
                Me.Header.Unit = mValue
            ElseIf mKeyWord.Contains("DIM") Then
                Me.Header.Dimension = mValue
            ElseIf mKeyWord.Contains("TOPO") Then
                Me.Header.Topo = System.Convert.ToInt32(mValue)
            ElseIf mKeyWord.Contains("COORDINATE") Then
                Me.Header.Coordinate = mValue
            ElseIf mKeyWord.Contains("PROJECTION") Then
                Me.Header.Projection = mValue
            ElseIf mKeyWord.Contains("PARAMETERS") Then
                Dim mValues() As String = Me.SplitString(mValue)
                If mValues IsNot Nothing Then
                    Me.Header.Parameters(0) = Convert.ToDouble(mValues(0))
                    Me.Header.Parameters(1) = Convert.ToDouble(mValues(1))
                End If
            ElseIf mKeyWord.Contains("MERIDINAN") Then
                Me.Header.Meridinan = mValue
            ElseIf mKeyWord.Contains("MINX") Then
                Me.Header.MinX = System.Convert.ToDouble(mValue)
            ElseIf mKeyWord.Contains("MINY") Then
                Me.Header.MinY = System.Convert.ToDouble(mValue)
            ElseIf mKeyWord.Contains("MAXX") Then
                Me.Header.MaxX = System.Convert.ToDouble(mValue)
            ElseIf mKeyWord.Contains("MAXY") Then
                Me.Header.MaxY = System.Convert.ToDouble(mValue)
            ElseIf mKeyWord.Contains("SCALE") Then
                Me.Header.Scale = System.Convert.ToInt32(mValue)
            ElseIf mKeyWord.Contains("DATE") Then
                If mValue.Contains("-") Or mValue.Contains("/") Or mValue.Contains("\") Then
                    Me.Header.SurvyDate = System.Convert.ToDateTime(mValue)
                ElseIf mValue.Length = 8 Then
                    Dim mYear As String = mValue.Substring(0, 4)
                    Dim mMonth As String = mValue.Substring(4, 2)
                    Dim mDay As String = mValue.Substring(6, 2)
                    Me.Header.SurvyDate = System.Convert.ToDateTime(mYear & "-" & mMonth & "-" & mDay)
                Else
                    Me.Header.SurvyDate = Now
                End If
            ElseIf mKeyWord.Contains("SPHEROID") Then
                Me.Header.Spheroid = mValue
            ElseIf mKeyWord.Contains("SEPARATOR") Then
                Me.Header.Separator = mValue
            End If
        End If
    End Sub

    Private Sub ReadLayer(ByVal p_Line As String)
        Dim mValues() As String = Me.SplitString(p_Line)

        If mValues IsNot Nothing Then
            Dim mVctLayer As New vctLayer
            mVctLayer.Code = mValues(0).Trim
            mVctLayer.Name = mValues(6).Trim
            mVctLayer.ChineseName = mValues(1).Trim
            Select Case mValues(2).Trim.ToUpper
                Case "POINT"
                    mVctLayer.LayerType = vctLayerType.Point
                Case "LINE"
                    mVctLayer.LayerType = vctLayerType.Line
                Case "POLYGON"
                    mVctLayer.LayerType = vctLayerType.Polygon
                Case "ANNOTATION"
                    mVctLayer.LayerType = vctLayerType.Annotation
                Case "IMAGE"
                    mVctLayer.LayerType = vctLayerType.Image
            End Select
            Dim mRed As Integer = Convert.ToInt32(mValues(3))
            Dim mGreen As Integer = Convert.ToInt32(mValues(4))
            Dim mBlue As Integer = Convert.ToInt32(mValues(5))
            mVctLayer.DefaultColor = Color.FromArgb(mRed, mGreen, mBlue)
            If mValues.Length > 7 Then
                mVctLayer.ExtTableName = mValues(7).Trim
            End If
            Me.p_Map.Layers.Add(mVctLayer)
        End If
    End Sub

    Private Sub ReadTablestructure(ByVal p_Line As String)
        Dim mValues() As String = Me.SplitString(p_Line)

        If mValues IsNot Nothing Then
            If mValues.Length = 2 And IsNumeric(mValues(1)) Then
                Me.m_TableName = mValues(0).Trim
                Dim mLayer As vctLayer = Me.Map.GetLayerByName(Me.m_TableName)
                If mLayer IsNot Nothing Then
                    mLayer.FieldCount = Convert.ToInt32(mValues(1))
                End If
            Else
                Dim mLayer As vctLayer = Me.Map.GetLayerByName(Me.m_TableName)
                If mLayer IsNot Nothing Then
                    Dim mField As New vctField
                    mField.Name = mValues(0)
                    Select Case mValues(1).ToUpper
                        Case "INTEGER", "INT"
                            Dim mColumn As DataColumn = New DataColumn
                            mColumn.ColumnName = mField.Name
                            mColumn.Caption = mField.Name
                            mColumn.DataType = Type.GetType("System.Int32")
                            mLayer.DataTable.Columns.Add(mColumn)

                            mField.Type = vctFieldType.vctInt
                        Case "CHAR", "VARCHAR"
                            Dim mColumn As DataColumn = New DataColumn
                            mColumn.ColumnName = mField.Name
                            mColumn.Caption = mField.Name
                            mColumn.DataType = Type.GetType("System.String")
                            mLayer.DataTable.Columns.Add(mColumn)

                            mField.Type = vctFieldType.vctString
                        Case "FLOAT"
                            Dim mColumn As DataColumn = New DataColumn
                            mColumn.ColumnName = mField.Name
                            mColumn.Caption = mField.Name
                            mColumn.DataType = Type.GetType("System.Double")
                            mLayer.DataTable.Columns.Add(mColumn)

                            mField.Type = vctFieldType.vctDouble
                        Case "DATE", "TIME"
                            Dim mColumn As DataColumn = New DataColumn
                            mColumn.ColumnName = mField.Name
                            mColumn.Caption = mField.Name
                            mColumn.DataType = Type.GetType("System.DateTime")
                            mLayer.DataTable.Columns.Add(mColumn)

                            mField.Type = vctFieldType.vctDateTime
                        Case "VARBIN"
                            Dim mColumn As DataColumn = New DataColumn
                            mColumn.ColumnName = mField.Name
                            mColumn.Caption = mField.Name
                            mColumn.DataType = Type.GetType("System.Byte[]")
                            mLayer.DataTable.Columns.Add(mColumn)

                            mField.Type = vctFieldType.vctVarbin
                        Case "BOOLEAN", "BOOL"
                            Dim mColumn As DataColumn = New DataColumn
                            mColumn.ColumnName = mField.Name
                            mColumn.Caption = mField.Name
                            mColumn.DataType = Type.GetType("System.Boolean")
                            mLayer.DataTable.Columns.Add(mColumn)

                            mField.Type = vctFieldType.vctBoolean
                    End Select
                    If mValues.Length > 2 Then
                        mField.Length = System.Convert.ToInt32(mValues(2))
                    End If
                    If mValues.Length > 3 Then
                        mField.DecimalLength = Convert.ToInt32(mValues(3))
                    End If

                    mLayer.Fields.Add(mField)
                End If
            End If
        End If
    End Sub

    Private Sub ReadPoint(ByVal p_Line As String)
        Select Case m_Count
            Case 0
                If p_Line = "" Then
                    Exit Sub
                End If
                Me.m_Point = New vctPoint
                Me.m_Point.ID = Convert.ToInt64(p_Line.Trim)
                Me.m_Count += 1
            Case 1
                If Me.m_Point IsNot Nothing Then
                    Me.m_Point.FID = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 2
                If Me.m_Point IsNot Nothing Then
                    Me.m_Point.LayerName = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 3
                If Me.m_Point IsNot Nothing Then
                    Me.m_Point.FeatureType = Convert.ToInt32(p_Line.Trim)
                End If
                Me.m_Count += 1
            Case 4
                Dim mValues() As String = Me.SplitString(p_Line)

                If mValues IsNot Nothing And Me.m_Point IsNot Nothing Then
                    Me.m_Point.X = Convert.ToDouble(mValues(0))
                    Me.m_Point.Y = Convert.ToDouble(mValues(1))

                    Dim mVctLayer As vctLayer = Me.Map.GetLayerByCode(Me.m_Point.FID)
                    If mVctLayer IsNot Nothing Then
                        mVctLayer.Points.Add(Me.m_Point)
                    End If

                    Me.m_Point = Nothing
                End If

                Me.m_Count = 0
        End Select
    End Sub

    Private Sub ReadLine(ByVal p_Line As String)
        Select Case m_Count
            Case 0
                If p_Line = "" Then
                    Exit Sub
                End If
                Me.m_Line = New vctLine
                Me.m_Line.ID = Convert.ToInt64(p_Line.Trim)
                Me.m_Count += 1
            Case 1
                If Me.m_Line IsNot Nothing Then
                    Me.m_Line.FID = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 2
                If Me.m_Line IsNot Nothing Then
                    Me.m_Line.LayerName = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 3
                If Me.m_Line IsNot Nothing Then
                    Me.m_Line.FeatureType = Convert.ToInt32(p_Line.Trim)
                End If
                Me.m_Count += 1
            Case 4
                If Me.m_Line IsNot Nothing Then
                    Me.m_Line.PointCount = Convert.ToInt32(p_Line.Trim)
                    Me.m_PointCount = Me.m_Line.PointCount
                End If
                Me.m_Count += 1
            Case 5 To 4 + Me.m_PointCount
                Dim mValues() As String = Me.SplitString(p_Line)

                If mValues IsNot Nothing And Me.m_Line IsNot Nothing Then
                    Dim mPoint As New vctPoint
                    mPoint.X = Convert.ToDouble(mValues(0))
                    mPoint.Y = Convert.ToDouble(mValues(1))

                    Me.m_Line.Points.Add(mPoint)

                    Dim mLayer As vctLayer = Me.Map.GetLayerByCode(Me.m_Line.FID)
                    If mLayer IsNot Nothing Then
                        If Not mLayer.Lines.Contains(Me.m_Line) Then
                            mLayer.Lines.Add(Me.m_Line)
                        End If
                    End If
                End If

                If m_Count = m_PointCount + 4 Then
                    m_Count = 0
                Else
                    m_Count = m_Count + 1
                End If
        End Select
    End Sub

    Private Sub ReadPolygon(ByVal p_Line As String)
        Select Case m_Count
            Case 0
                If p_Line = "" Then
                    Exit Sub
                End If
                Me.m_Polygon = New vctPolygon
                Me.m_Polygon.ID = Convert.ToInt64(p_Line.Trim)
                Me.m_Count += 1
            Case 1
                If Me.m_Polygon IsNot Nothing Then
                    Me.m_Polygon.FID = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 2
                If Me.m_Polygon IsNot Nothing Then
                    Me.m_Polygon.LayerName = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 3
                Dim mValues() As String = Me.SplitString(p_Line)
                If mValues IsNot Nothing And Me.m_Polygon IsNot Nothing Then
                    Me.m_Polygon.LabelX = Convert.ToDouble(mValues(0))
                    Me.m_Polygon.LabelY = Convert.ToDouble(mValues(1))
                End If
                Me.m_Count += 1
            Case 4
                If Me.m_Polygon IsNot Nothing Then
                    Me.m_PointCount = Convert.ToInt64(p_Line.Trim)
                    Me.m_Polygon.PointCount.Add(m_PointCount)
                End If
                Me.m_Count += 1
            Case 5 To m_PointCount + 5
                Dim mValues() As String = Me.SplitString(p_Line)
                If mValues Is Nothing Then
                    ReDim mValues(0)
                    mValues(0) = p_Line
                End If

                If Me.m_Polygon IsNot Nothing Then
                    For i As Integer = 0 To mValues.Length - 1
                        Dim mLineID As Integer = Convert.ToInt32(mValues(i))
                        If mLineID = 0 Then
                            Dim mPoints As New vctPointCollection
                            Me.m_Polygon.PointCount.Add(m_PointCount)
                            Me.m_Polygon.PointCollections.Add(mPoints)
                        Else
                            Dim mVctLine As vctLine = Me.Map.GetLineByID(Math.Abs(mLineID))
                            If mVctLine IsNot Nothing Then
                                If mLineID > 0 Then
                                    For j As Integer = 0 To mVctLine.Points.Count - 1
                                        Dim mPoint As New vctPoint
                                        mPoint.X = mVctLine.Points(j).X
                                        mPoint.Y = mVctLine.Points(j).Y

                                        If Me.m_Polygon.PointCollections.Count = 0 Then
                                            Dim mPoints As New vctPointCollection
                                            mPoints.Points.Add(mPoint)
                                            Me.m_Polygon.PointCollections.Add(mPoints)
                                        Else
                                            Me.m_Polygon.PointCollections(Me.m_Polygon.PointCollections.Count - 1).Points.Add(mPoint)
                                        End If
                                    Next
                                Else
                                    For j As Integer = mVctLine.Points.Count - 1 To 0 Step -1
                                        Dim mPoint As New vctPoint
                                        mPoint.X = mVctLine.Points(j).X
                                        mPoint.Y = mVctLine.Points(j).Y

                                        If Me.m_Polygon.PointCollections.Count = 0 Then
                                            Dim mPoints As New vctPointCollection
                                            mPoints.Points.Add(mPoint)
                                            Me.m_Polygon.PointCollections.Add(mPoints)
                                        Else
                                            Me.m_Polygon.PointCollections(Me.m_Polygon.PointCollections.Count - 1).Points.Add(mPoint)
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        Me.m_Count += 1
                    Next

                    Dim mLayer As vctLayer = Me.Map.GetLayerByCode(Me.m_Polygon.FID)
                    If mLayer IsNot Nothing Then
                        If Not mLayer.Polygons.Contains(Me.m_Polygon) Then
                            mLayer.Polygons.Add(Me.m_Polygon)
                        End If
                    End If
                End If

                If Me.m_Count = m_PointCount + 5 Then
                    Me.m_Count = 0
                End If
        End Select

    End Sub

    Private Sub ReadAnnotation(ByVal p_Line As String)
        Select Case m_Count
            Case 0
                If p_Line = "" Then
                    Exit Sub
                End If
                Me.m_Annotation = New vctAnnotation
                Me.m_Annotation.ID = Convert.ToInt64(p_Line.Trim)
                Me.m_Count += 1
            Case 1
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.FID = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 2
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.LayerName = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 3
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.FontName = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 4
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.Color = Color.FromArgb(Convert.ToInt32(p_Line.Trim))
                End If
                Me.m_Count += 1
            Case 5
                Dim mValues() As String = Me.SplitString(p_Line)

                If mValues IsNot Nothing And Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.Weigh = Convert.ToInt32(mValues(0).Trim)
                    Me.m_Annotation.Italic = Convert.ToInt32(mValues(1).Trim)
                    Me.m_Annotation.UnderLine = mValues(2).Trim
                End If
                Me.m_Count += 1
            Case 6
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.FontSize = Convert.ToInt32(p_Line.Trim)
                End If
                Me.m_Count += 1
            Case 7
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.Margin = Convert.ToInt32(p_Line.Trim)
                End If
                Me.m_Count += 1
            Case 8
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.Text = p_Line.Trim
                End If
                Me.m_Count += 1
            Case 9
                If Me.m_Annotation IsNot Nothing Then
                    Me.m_Annotation.PointCount = Convert.ToInt32(p_Line.Trim)
                    Me.m_PointCount = Me.m_Annotation.PointCount
                End If
                Me.m_Count += 1
            Case 10 To 9 + Me.m_PointCount
                Dim mValues() As String = Me.SplitString(p_Line)

                If mValues IsNot Nothing And Me.m_Annotation IsNot Nothing Then
                    Dim mPoint As New vctPoint
                    mPoint.X = Convert.ToDouble(mValues(0))
                    mPoint.Y = Convert.ToDouble(mValues(1))

                    Me.m_Annotation.Points.Add(mPoint)

                    Dim mLayer As vctLayer = Me.Map.GetLayerByCode(Me.m_Annotation.FID)
                    If mLayer IsNot Nothing Then
                        If Not mLayer.Annotations.Contains(Me.m_Annotation) Then
                            mLayer.Annotations.Add(Me.m_Annotation)
                        End If
                    End If
                End If

                If Me.m_Count = 9 + Me.m_PointCount Then
                    Me.m_Count = 0
                Else
                    Me.m_Count += 1
                End If
        End Select
    End Sub

    Private Sub ReadAttribute(ByVal p_Line As String)
        If p_Line.Trim = "" Then
            Exit Sub
        End If
        If p_Line.Trim.ToUpper = "TABLEEND" Then
            Me.m_TableName = ""
            Exit Sub
        End If
        Dim mValues() As String = Me.SplitString(p_Line)
        If mValues Is Nothing And Me.m_TableName = "" Then
            Me.m_TableName = p_Line.Trim
        Else
            Dim mLayer As vctLayer = Me.Map.GetLayerByName(Me.m_TableName)
            If mLayer IsNot Nothing Then
                mLayer.Attributes.Add(p_Line)

                Dim mRow As DataRow = mLayer.DataTable.NewRow
                For k As Integer = 0 To mValues.Length - 1
                    Dim mValue As String = mValues(k)
                    Try
                        mValue = Convert.ChangeType(mValue.Trim, mLayer.DataTable.Columns(k).DataType)
                        mRow.Item(k) = mValue
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                mLayer.DataTable.Rows.Add(mRow)
            End If
        End If
    End Sub

    Private Sub SaveAttributeOfFeature(ByVal p_Row As DataRow, ByVal p_Feature As IFeature)
        For i As Integer = 0 To p_Row.ItemArray.Length - 1
            Dim mIndex As Integer = p_Feature.Fields.FindField(p_Row.Table.Columns(i).ColumnName.Trim)
            If mIndex >= 0 Then
                If p_Feature.Fields.Field(mIndex).Editable = True Then
                    p_Feature.Value(mIndex) = p_Row.Item(i)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' 根据图形的标识码得到对应的属性信息
    ''' </summary>
    ''' <param name="p_ID"></param>
    ''' <param name="p_IDFieldName"></param>
    ''' <param name="p_AttrTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAttrFromAttributeTable(ByVal p_ID As Integer, ByVal p_IDFieldName As String, ByVal p_AttrTable As DataTable) As DataRow
        Dim mRow As DataRow() = p_AttrTable.Select(p_IDFieldName + "='" + p_ID.ToString + "'")
        If mRow Is Nothing Then
            Return Nothing
        End If
        Return mRow(0)
    End Function

    Private Function GetVctOutFeatureClass(ByVal p_FeatureClass As IFeatureClass, ByVal p_OutName As String, ByRef p_FID As Integer, ByRef p_ID As Int64) As vctOutFeatureClass
        Dim mVctFeatureClass As New vctOutFeatureClass
        mVctFeatureClass.FeatureClassID = p_FID.ToString.PadLeft(10, "0")

        p_FID += 1

        Dim mOutName As String = p_OutName
        If mOutName.Contains(".") Then
            mOutName = mOutName.Substring(mOutName.LastIndexOf(".") + 1)
        End If
        mVctFeatureClass.OutName = mOutName
        Dim mAliasName As String = p_FeatureClass.AliasName
        If mAliasName.Contains(".") Then
            mAliasName = mAliasName.Substring(mAliasName.LastIndexOf(".") + 1)
        End If
        mVctFeatureClass.OutAliasName = mAliasName

        '输出字段设置
        Dim mVctField As New vctField
        mVctField.Name = "ID"
        mVctField.Type = vctFieldType.vctInt
        mVctField.Length = 10
        mVctField.DecimalLength = 0
        mVctFeatureClass.OutFields.Add(mVctField)

        Dim mFieldIndexs As New List(Of Integer)

        Dim mFields As IFields = p_FeatureClass.Fields
        For i As Integer = 0 To mFields.FieldCount - 1
            Dim mField As IField = mFields.Field(i)

            Dim mVctField1 As New vctField
            mVctField1.Name = mField.Name
            Select Case mVctField1.Name.ToUpper
                Case "Shape.Area".ToUpper, "Shape.len".ToUpper

                Case Else
                    Dim mFind As Boolean = True
                    While mFind
                        mFind = False
                        For j As Integer = 0 To mVctFeatureClass.OutFields.Count - 1
                            If mVctFeatureClass.OutFields(j).Name.ToLower = mVctField1.Name.ToLower Then
                                mVctField1.Name += "_"
                                mFind = True
                            End If
                        Next
                    End While
                    Select Case mField.Type
                        Case esriFieldType.esriFieldTypeBlob
                            mVctField1.Type = vctFieldType.vctVarbin
                            mVctField1.Length = mField.Length
                            mVctField1.DecimalLength = mField.Precision

                            mVctFeatureClass.OutFields.Add(mVctField1)
                            mFieldIndexs.Add(i)
                        Case esriFieldType.esriFieldTypeDate
                            mVctField1.Type = vctFieldType.vctDateTime
                            mVctField1.Length = mField.Length
                            mVctField1.DecimalLength = mField.Precision

                            mVctFeatureClass.OutFields.Add(mVctField1)
                            mFieldIndexs.Add(i)
                        Case esriFieldType.esriFieldTypeDouble, esriFieldType.esriFieldTypeSingle
                            mVctField1.Type = vctFieldType.vctDouble
                            mVctField1.Length = mField.Length
                            mVctField1.DecimalLength = mField.Precision

                            mVctFeatureClass.OutFields.Add(mVctField1)
                            mFieldIndexs.Add(i)
                        Case esriFieldType.esriFieldTypeInteger, esriFieldType.esriFieldTypeSmallInteger
                            mVctField1.Type = vctFieldType.vctInt
                            mVctField1.Length = mField.Length
                            mVctField1.DecimalLength = mField.Precision

                            mVctFeatureClass.OutFields.Add(mVctField1)
                            mFieldIndexs.Add(i)
                        Case esriFieldType.esriFieldTypeString, esriFieldType.esriFieldTypeXML
                            mVctField1.Type = vctFieldType.vctString
                            mVctField1.Length = mField.Length
                            mVctField1.DecimalLength = mField.Precision

                            mVctFeatureClass.OutFields.Add(mVctField1)
                            mFieldIndexs.Add(i)
                    End Select
            End Select
        Next

        Dim mLayerType As LayerType = Me.GetLayerType(p_FeatureClass)

        Select Case mLayerType
            Case LayerType.Point
                mVctFeatureClass.GeometryType = vctLayerType.Point
            Case LayerType.Line
                mVctFeatureClass.GeometryType = vctLayerType.Line
            Case LayerType.Polygon
                mVctFeatureClass.GeometryType = vctLayerType.Polygon
            Case LayerType.Raster
                mVctFeatureClass.GeometryType = vctLayerType.Image
            Case LayerType.Annotation
                mVctFeatureClass.GeometryType = vctLayerType.Annotation
        End Select

        Dim mFeaturecursor As IFeatureCursor = p_FeatureClass.Search(Nothing, False)
        Dim mFeature As IFeature = mFeaturecursor.NextFeature
        While mFeature IsNot Nothing
            Dim mStr As String = p_ID.ToString
            'Dim mSitFeature As New SitFeature(mFeature)
            For i As Integer = 0 To mFieldIndexs.Count - 1
                mStr &= "," & mFeature.Value(mFieldIndexs(i)).ToString ' mSitFeature.GetFieldValue(mFieldIndexs(i)).ToString
            Next
            mVctFeatureClass.OutAttributes.Add(mStr)

            Select Case mLayerType
                Case LayerType.Point
                    Dim mVctPoint As New vctPoint
                    mVctPoint.ID = p_ID
                    mVctPoint.FID = mVctFeatureClass.FeatureClassID
                    mVctPoint.LayerName = mVctFeatureClass.OutAliasName
                    Dim mPoint As IPoint = mFeature.ShapeCopy
                    mVctPoint.X = mPoint.X
                    mVctPoint.Y = mPoint.Y
                    mVctFeatureClass.OutPoints.Add(mVctPoint)
                Case LayerType.Line
                    Dim mVctLine As New vctLine
                    mVctLine.ID = p_ID
                    mVctLine.FID = mVctFeatureClass.FeatureClassID
                    mVctLine.LayerName = mVctFeatureClass.OutAliasName
                    Dim mPointCollection As IPointCollection = mFeature.ShapeCopy
                    mVctLine.PointCount = mPointCollection.PointCount
                    For i As Integer = 0 To mPointCollection.PointCount - 1
                        Dim mPoint As IPoint = mPointCollection.Point(i)
                        Dim mVctPoint As New vctPoint
                        mVctPoint.X = mPoint.X
                        mVctPoint.Y = mPoint.Y
                        mVctLine.Points.Add(mVctPoint)
                    Next
                    mVctFeatureClass.OutLines.Add(mVctLine)
                Case LayerType.Polygon
                    Dim mArea As IArea = mFeature.ShapeCopy

                    Dim mVctPolygon As New vctPolygon
                    mVctPolygon.ID = p_ID
                    mVctPolygon.FID = mVctFeatureClass.FeatureClassID
                    mVctPolygon.LayerName = mVctFeatureClass.OutAliasName
                    mVctPolygon.LabelX = mArea.LabelPoint.X
                    mVctPolygon.LabelY = mArea.LabelPoint.Y

                    Dim mPolygons As List(Of IPolygon) = Me.GetSimplePolygons(mFeature.ShapeCopy)
                    For i As Integer = 0 To mPolygons.Count - 1
                        Dim mVctLine As New vctLine
                        mVctLine.ID = p_ID
                        mVctLine.FID = mVctFeatureClass.FeatureClassID
                        mVctLine.LayerName = mVctFeatureClass.OutAliasName
                        Dim mPointCollection As IPointCollection = mPolygons(i)
                        mVctLine.PointCount = mPointCollection.PointCount
                        For j As Integer = 0 To mPointCollection.PointCount - 1
                            Dim mPoint As IPoint = mPointCollection.Point(j)
                            Dim mVctPoint As New vctPoint
                            mVctPoint.X = mPoint.X
                            mVctPoint.Y = mPoint.Y
                            mVctLine.Points.Add(mVctPoint)
                        Next

                        mVctFeatureClass.OutLines.Add(mVctLine)

                        mVctPolygon.PointCount.Add(p_ID)

                        p_ID += 1
                    Next
                    mVctFeatureClass.OutPolygons.Add(mVctPolygon)
                Case LayerType.Annotation
                Case LayerType.Raster
            End Select

            p_ID += 1

            mFeature = mFeaturecursor.NextFeature
        End While

        Return mVctFeatureClass
    End Function

    Private Sub WriteFeatureCode(ByVal p_StreamWriter As StreamWriter, ByVal p_FeatureClassList As List(Of vctOutFeatureClass))
        p_StreamWriter.WriteLine("FeatureCodeBegin")

        For i As Integer = 0 To p_FeatureClassList.Count - 1
            Dim mFeatureClass As vctOutFeatureClass = p_FeatureClassList(i)

            Dim mGeometryType As String = ""
            Select Case mFeatureClass.GeometryType
                Case vctLayerType.Point
                    mGeometryType = "Point"
                Case vctLayerType.Line
                    mGeometryType = "Line"
                Case vctLayerType.Polygon
                    mGeometryType = "Polygon"
                Case vctLayerType.Annotation
                    mGeometryType = "Annotation"
                Case vctLayerType.Image
                    mGeometryType = "Image"
                Case Else
                    mGeometryType = "Any"
            End Select

            Dim mStr As String = mFeatureClass.FeatureClassID.ToString() & "," & mFeatureClass.OutAliasName & "," & mGeometryType & "," & "0,0,0" & "," & mFeatureClass.OutName

            p_StreamWriter.WriteLine(mStr)
        Next
        p_StreamWriter.WriteLine("FeatureCodeEnd")
    End Sub

    Private Sub WriteTableStructure(ByVal p_StreamWriter As StreamWriter, ByVal p_FeatureClassList As List(Of vctOutFeatureClass))
        p_StreamWriter.WriteLine("TableStructureBegin")

        For i As Integer = 0 To p_FeatureClassList.Count - 1
            Dim mFeatureClass As vctOutFeatureClass = p_FeatureClassList(i)

            p_StreamWriter.WriteLine(mFeatureClass.OutName + "," + mFeatureClass.OutFields.Count.ToString)

            For j As Integer = 0 To mFeatureClass.OutFields.Count - 1
                Dim mField As vctField = mFeatureClass.OutFields(j)
                Dim mFieldType As String = ""
                Select Case mField.Type
                    Case vctFieldType.vctVarbin
                        mFieldType = "Varbin"
                    Case vctFieldType.vctDateTime
                        mFieldType = "Date"
                    Case vctFieldType.vctDouble
                        mFieldType = "Float"
                    Case vctFieldType.vctInt
                        mFieldType = "Integer"
                    Case vctFieldType.vctString
                        mFieldType = "Char"
                End Select
                Dim mLine As String = mField.Name + "," + mFieldType + "," + mField.Length.ToString
                If mField.Type = vctFieldType.vctDouble Then
                    mLine += "," + mField.DecimalLength.ToString
                End If
                If mFieldType = "" Then
                    Continue For
                End If
                p_StreamWriter.WriteLine(mLine)
            Next
        Next

        p_StreamWriter.WriteLine("TableStructureEnd")
    End Sub

    Private Sub WritePoint(ByVal p_StreamWriter As StreamWriter, ByVal p_FeatureClasslist As List(Of vctOutFeatureClass))
        p_StreamWriter.WriteLine("PointBegin")

        For i As Integer = 0 To p_FeatureClasslist.Count - 1
            Dim mFeatureClass As vctOutFeatureClass = p_FeatureClasslist(i)

            For j As Integer = 0 To mFeatureClass.OutPoints.Count - 1
                Dim mPoint As vctPoint = mFeatureClass.OutPoints(j)
                p_StreamWriter.WriteLine(mPoint.ID.ToString)
                p_StreamWriter.WriteLine(mPoint.FID)
                p_StreamWriter.WriteLine(mPoint.LayerName)
                p_StreamWriter.WriteLine("1")
                p_StreamWriter.WriteLine(mPoint.X.ToString + "," + mPoint.Y.ToString)
                p_StreamWriter.WriteLine("")
            Next
        Next

        p_StreamWriter.WriteLine("PointEnd")
    End Sub

    Private Sub WriteLine(ByVal p_StreamWriter As StreamWriter, ByVal p_FeatureClassList As List(Of vctOutFeatureClass))
        p_StreamWriter.WriteLine("LineBegin")

        For i As Integer = 0 To p_FeatureClassList.Count - 1
            Dim mFeatureClass As vctOutFeatureClass = p_FeatureClassList(i)

            For j As Integer = 0 To mFeatureClass.OutLines.Count - 1
                Dim mLine As vctLine = mFeatureClass.OutLines(j)
                p_StreamWriter.WriteLine(mLine.ID)
                p_StreamWriter.WriteLine(mLine.FID)
                p_StreamWriter.WriteLine(mLine.LayerName)
                p_StreamWriter.WriteLine("1")
                p_StreamWriter.WriteLine(mLine.Points.Count.ToString)
                For k As Integer = 0 To mLine.Points.Count - 1
                    Dim mPoint As vctPoint = mLine.Points(k)
                    p_StreamWriter.WriteLine(mPoint.X.ToString & "," & mPoint.Y.ToString)
                Next
            Next
        Next

        p_StreamWriter.WriteLine("LineEnd")
    End Sub

    Private Sub WritePolygon(ByVal p_StreamWriter As StreamWriter, ByVal p_FeatureClassList As List(Of vctOutFeatureClass))
        p_StreamWriter.WriteLine("PolygonBegin")

        For i As Integer = 0 To p_FeatureClassList.Count - 1
            Dim mFeatureClass As vctOutFeatureClass = p_FeatureClassList(i)

            For j As Integer = 0 To mFeatureClass.OutPolygons.Count - 1
                Dim mPolygon As vctPolygon = mFeatureClass.OutPolygons(j)
                p_StreamWriter.WriteLine(mPolygon.ID.ToString)
                p_StreamWriter.WriteLine(mPolygon.FID)
                p_StreamWriter.WriteLine(mPolygon.LayerName)
                p_StreamWriter.WriteLine(mPolygon.LabelX.ToString & "," & mPolygon.LabelY.ToString)

                Dim mOutStr As New List(Of String)
                Dim mCount As Integer = 0
                Dim mIndex As Integer = 0
                Dim mStr As String = ""
                For k As Integer = 0 To mPolygon.PointCount.Count - 1
                    If mIndex = 0 Then
                        mStr = mPolygon.PointCount(k).ToString
                        mIndex += 1
                        mCount += 1
                    ElseIf mIndex = 8 Then
                        mOutStr.Add(mStr)
                        mStr = mPolygon.PointCount(k).ToString
                        mIndex = 1
                        mCount += 1
                    Else
                        mStr &= ",0"
                        mIndex += 1
                        If mIndex = 8 Then
                            mOutStr.Add(mStr)
                            mStr = mPolygon.PointCount(k).ToString
                            mIndex = 1
                        Else
                            mStr &= "," & mPolygon.PointCount(k).ToString
                            mIndex += 1
                        End If
                        mCount += 2
                    End If
                    If k = mPolygon.PointCount.Count - 1 Then
                        If Not mOutStr.Contains(mStr) Then
                            mOutStr.Add(mStr)
                        End If
                    End If
                Next

                p_StreamWriter.WriteLine(mCount.ToString)
                For k As Integer = 0 To mOutStr.Count - 1
                    p_StreamWriter.WriteLine(mOutStr(k))
                Next
            Next
        Next

        p_StreamWriter.WriteLine("PolygonEnd")
    End Sub

    Private Sub WriteAnnotation(ByVal p_StreamWriter As StreamWriter, ByVal p_FeatureClassList As List(Of vctOutFeatureClass))
        p_StreamWriter.WriteLine("AnnotationBegin")
        'For i As Integer = 0 To p_FeatureClasslist.Count - 1
        '    Dim mfeatureclass As IFeatureClass = p_FeatureClasslist(i)
        '    Dim mIndex As Integer = mfeatureclass.Fields.FindField("MBBSM")

        '    If mfeatureclass.FeatureType = esriFeatureType.esriFTAnnotation Then
        '        Dim mFeaturecursor As IFeatureCursor = mfeatureclass.Search(Nothing, False)
        '        Dim mfeature As IFeature = mFeaturecursor.NextFeature
        '        Dim mfontnameIndex As Integer = mfeatureclass.FindField("FontName")
        '        Dim indexFontSize As Integer = mfeatureclass.FindField("FontSize")
        '        Dim indexBold As Integer = mfeatureclass.FindField("Bold")
        '        Dim indexItalic As Integer = mfeatureclass.FindField("Italic")
        '        Dim indexUnderline As Integer = mfeatureclass.FindField("Underline")
        '        Dim indexVerticalAlignment As Integer = mfeatureclass.FindField("VerticalAlignment")
        '        Dim indexHorizontalAlignment As Integer = mfeatureclass.FindField("HorizontalAlignment")
        '        Dim indexCharacterSpacing As Integer = mfeatureclass.FindField("CharacterSpacing")
        '        Dim indexTextString As String = mfeatureclass.FindField("TextString")

        '        While mfeature IsNot Nothing
        '            Dim mAnnoFeature As IAnnotationFeature = mfeature
        '            p_StreamWriter.WriteLine(mfeature.OID)

        '            p_StreamWriter.WriteLine(mfeatureclass.FeatureClassID)
        '            p_StreamWriter.WriteLine(mfeatureclass.FeatureClassID)


        '            Dim mElement As IElement = mAnnoFeature.Annotation
        '            Dim mTextElement As ITextElement = mElement
        '            p_StreamWriter.WriteLine(mfeature.Value(mfontnameIndex))
        '            p_StreamWriter.WriteLine(mTextElement.Symbol.Color.RGB)
        '            Dim mUnderline As String = ""
        '            If mfeature.Value(indexUnderline) = 1 Then
        '                mUnderline = "T"
        '            ElseIf mfeature.Value(indexUnderline) = 0 Then
        '                mUnderline = "F"

        '            End If
        '            Dim mShape As String = ""

        '            p_StreamWriter.WriteLine("400,0," + mUnderline) '磅重,形状，下划线
        '            p_StreamWriter.WriteLine(mfeature.Value(indexFontSize))
        '            p_StreamWriter.WriteLine(mfeature.Value(indexCharacterSpacing))
        '            p_StreamWriter.WriteLine(mfeature.Value(indexTextString))
        '            Dim mGeometry As IGeometry = mfeature.Shape
        '            Dim mCount As String = "1"

        '            Select Case mGeometry.GeometryType
        '                Case esriGeometryType.esriGeometryLine, esriGeometryType.esriGeometryPolygon
        '                    Dim mpoints As IPointCollection = mGeometry
        '                    mCount = mpoints.PointCount.ToString
        '                    For j As Integer = 0 To mpoints.PointCount - 1
        '                        Dim mpoint As IPoint = mpoints.Point(j)
        '                        p_StreamWriter.WriteLine(mpoint.X.ToString + "," + mpoint.Y.ToString)
        '                    Next
        '                Case Else
        '                    mCount = "1"
        '                    Dim mpoint As IPoint = mGeometry
        '                    p_StreamWriter.WriteLine(mpoint.X.ToString + "," + mpoint.Y.ToString)
        '            End Select
        '            mfeature = mFeaturecursor.NextFeature
        '        End While

        '    End If
        'Next
        p_StreamWriter.WriteLine("AnnotationEnd")

    End Sub

    Private Sub WriteAttributeTable(ByVal p_StreamWriter As StreamWriter, ByVal p_FeatureClassList As List(Of vctOutFeatureClass))
        p_StreamWriter.WriteLine("AttributeBegin")

        For i As Integer = 0 To p_FeatureClassList.Count - 1
            Dim mFeatureClass As vctOutFeatureClass = p_FeatureClassList(i)

            p_StreamWriter.WriteLine(mFeatureClass.OutName)

            For j As Integer = 0 To mFeatureClass.OutAttributes.Count - 1
                p_StreamWriter.WriteLine(mFeatureClass.OutAttributes(j))
            Next

            p_StreamWriter.WriteLine("TableEnd")
            p_StreamWriter.WriteLine("")
        Next

        p_StreamWriter.WriteLine("AttributeEnd")
    End Sub

    Private Function GetLayerType(ByVal p_FeatureClass As IFeatureClass) As LayerType
        '栅格
        If p_FeatureClass.FeatureType = esriFeatureType.esriFTRasterCatalogItem Then
            Dim mDataset As IDataset = p_FeatureClass
            Dim mRasterWorkspaceEx As IRasterWorkspaceEx = mDataset.Workspace
            Dim mRasterCatalog As IRasterCatalog = mRasterWorkspaceEx.OpenRasterCatalog(mDataset.Name)
            If mRasterCatalog.IsRasterDataset Then
                Return LayerType.Raster
            Else
                Return LayerType.RasterCatalog
            End If
        End If
        '注记
        If p_FeatureClass.FeatureType = esriFeatureType.esriFTAnnotation Or _
                    p_FeatureClass.FeatureType = esriFeatureType.esriFTCoverageAnnotation Then
            Return LayerType.Annotation
        End If
        '点
        If p_FeatureClass.ShapeType = esriGeometryType.esriGeometryPoint Or _
            p_FeatureClass.ShapeType = esriGeometryType.esriGeometryMultipoint Then
            Return LayerType.Point
        End If
        '线
        If p_FeatureClass.ShapeType = esriGeometryType.esriGeometryLine Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryCircularArc Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryEllipticArc Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryLine Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryMultiPatch Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryPath Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryPolyline Then
            Return LayerType.Line
        End If
        '面
        If p_FeatureClass.ShapeType = esriGeometryType.esriGeometryPolygon Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryRing Or _
                    p_FeatureClass.ShapeType = esriGeometryType.esriGeometryEnvelope Then
            Return LayerType.Polygon
        End If
        Return LayerType.UnKnown
    End Function

    Private Function GetSimplePolygons(ByVal p_Polygon As ESRI.ArcGIS.Geometry.IPolygon) As List(Of IPolygon)
        If p_Polygon Is Nothing Then
            Return Nothing
        End If

        If p_Polygon.IsEmpty Then
            Return Nothing
        End If

        Dim mResultPolygons As New List(Of IPolygon)

        Dim pPolygon4 As IPolygon4 = p_Polygon
        Dim pExteriorRings As IGeometryCollection = pPolygon4.ExteriorRingBag
        Dim i, j As Integer
        For i = 0 To pExteriorRings.GeometryCount - 1
            Dim pRing As IRing = pExteriorRings.Geometry(i)
            Dim pInteriorRings As IGeometryCollection = pPolygon4.InteriorRingBag(pRing)
            If pInteriorRings.GeometryCount = 0 Then
                Dim pTempPolygon As IGeometryCollection = New PolygonClass
                Dim pTempGeometry As IGeometry = pExteriorRings.Geometry(i)
                pTempPolygon.AddGeometries(1, pTempGeometry)
                mResultPolygons.Add(pTempPolygon)
            Else
                Dim pTempPolygon As IGeometryCollection = New PolygonClass
                Dim pTempGeometry As IGeometry = pExteriorRings.Geometry(i)
                pTempPolygon.AddGeometries(1, pTempGeometry)
                mResultPolygons.Add(pTempPolygon)

                For j = 0 To pInteriorRings.GeometryCount - 1
                    pTempPolygon = New PolygonClass
                    pTempGeometry = pInteriorRings.Geometry(j)
                    pTempPolygon.AddGeometries(1, pTempGeometry)
                    mResultPolygons.Add(pTempPolygon)
                Next j
            End If
        Next i

        Return mResultPolygons
    End Function

#End Region

End Class

Public Interface IvctDocument
    ''方法
    Sub Open(ByVal p_FileName As String)

    Sub ReadData()

    Function ConvertVctFileToSDE(ByVal p_Layer As vctLayer, ByVal p_Workspace As IWorkspace, ByVal p_LayerName As String, ByVal p_DatasetName As String, ByVal p_SpatialReference As ISpatialReference, ByRef p_ErrMessage As String) As Boolean

    Function ConvertFeatureClassToVCTFile(ByVal p_OutputFileName As String, ByVal p_FeatureClass As IFeatureClass, ByVal p_OutName As String, ByVal p_SpatialReference As ISpatialReference, ByRef p_ErrMessage As String) As Boolean

    Function ConvertDatasetToVCTFile(ByVal p_OutputFileName As String, ByVal p_Dataset As IFeatureDataset, ByVal p_ConvertClassNames As List(Of String), ByVal p_OutClassNames As List(Of String), ByVal p_SpatialReference As ISpatialReference, ByRef p_ErrMessage As String) As Boolean

    ''属性
    ''read only
    Property IsOpen() As Boolean

    ''read only
    Property Header() As vctHeader

    ''read only
    Property Map() As vctMap

End Interface

