Imports System.Windows.Forms
Imports System.IO
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Display

'Public Enum enumTableType
'    OleDB
'    Sde
'    Access
'End Enum

'Public Enum enumMapServerType
'    Sde = 0
'    Access = 1
'End Enum

'Public Enum enumSdeType
'    SQLServer = 0
'    Oracle = 1
'End Enum

'<Serializable()> _
'Public Class FeatureNodeImageIndex
'    Private p_GroupImageIndex As Integer = 0
'    Private p_PointImageIndex As Integer = 1
'    Private p_LineImageIndex As Integer = 2
'    Private p_PolygonImageIndex As Integer = 3
'    Private p_AnnoImageIndex As Integer = 4
'    Private p_RasterImageIndex As Integer = 5
'    Private p_TableImageIndex As Integer = 6
'    Private p_TopologyImageIndex As Integer = 7
'    Private p_RasterCatalogImageIndex As Integer = 8
'    Private p_UnKnownImageIndex As Integer = 9

'    Property GroupImageIndex() As Integer
'        Get
'            GroupImageIndex = p_GroupImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_GroupImageIndex = value
'        End Set
'    End Property

'    Property PointImageIndex() As Integer
'        Get
'            PointImageIndex = p_PointImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_PointImageIndex = value
'        End Set
'    End Property

'    Property LineImageIndex() As Integer
'        Get
'            LineImageIndex = p_LineImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_LineImageIndex = value
'        End Set
'    End Property

'    Property PolygonImageIndex() As Integer
'        Get
'            PolygonImageIndex = p_PolygonImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_PolygonImageIndex = value
'        End Set
'    End Property

'    Property AnnoImageIndex() As Integer
'        Get
'            AnnoImageIndex = p_AnnoImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_AnnoImageIndex = value
'        End Set
'    End Property

'    Property RasterImageIndex() As Integer
'        Get
'            RasterImageIndex = p_RasterImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_RasterImageIndex = value
'        End Set
'    End Property

'    Property TableImageIndex() As Integer
'        Get
'            TableImageIndex = p_TableImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_TableImageIndex = value
'        End Set
'    End Property

'    Property TopologyImageIndex() As Integer
'        Get
'            Return Me.p_TopologyImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            Me.p_TopologyImageIndex = value
'        End Set
'    End Property

'    Property RasterCatalogImageIndex() As Integer
'        Get
'            Return Me.p_RasterCatalogImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            Me.p_RasterCatalogImageIndex = value
'        End Set
'    End Property

'    Property UnKnownImageIndex() As Integer
'        Get
'            UnKnownImageIndex = p_UnKnownImageIndex
'        End Get
'        Set(ByVal value As Integer)
'            p_UnKnownImageIndex = value
'        End Set
'    End Property

'End Class

<Serializable()> _
Public Class FeatureNodeImageKey
    Private p_GroupImageKey As String = "GroupImageKey"
    Private p_PointImageKey As String = "PointImageKey"
    Private p_LineImageKey As String = "LineImageKey"
    Private p_PolygonImageKey As String = "PolygonImageKey"
    Private p_AnnoImageKey As String = "AnnoImageKey"
    Private p_RasterImageKey As String = "GraphicImageKey"
    Private p_TableImageKey As String = "TableImageKey"
    Private p_ToplogyImageKey As String = "TopologyImageKey"
    Private p_RasterCatalogImageKey As String = "RasterCatalogImageKey"
    Private p_UnKnownImageKey As String = "UnKnownImageKey"

    Property GroupImageKey() As String
        Get
            GroupImageKey = p_GroupImageKey
        End Get
        Set(ByVal value As String)
            p_GroupImageKey = value
        End Set
    End Property

    Property PointImageKey() As String
        Get
            PointImageKey = p_PointImageKey
        End Get
        Set(ByVal value As String)
            p_PointImageKey = value
        End Set
    End Property

    Property LineImageKey() As String
        Get
            LineImageKey = p_LineImageKey
        End Get
        Set(ByVal value As String)
            p_LineImageKey = value
        End Set
    End Property

    Property PolygonImageKey() As String
        Get
            PolygonImageKey = p_PolygonImageKey
        End Get
        Set(ByVal value As String)
            p_PolygonImageKey = value
        End Set
    End Property

    Property AnnoImageKey() As String
        Get
            AnnoImageKey = p_AnnoImageKey
        End Get
        Set(ByVal value As String)
            p_AnnoImageKey = value
        End Set
    End Property

    Property RasterImageKey() As String
        Get
            RasterImageKey = p_RasterImageKey
        End Get
        Set(ByVal value As String)
            p_RasterImageKey = value
        End Set
    End Property

    Property TableImageKey() As String
        Get
            TableImageKey = p_TableImageKey
        End Get
        Set(ByVal value As String)
            p_TableImageKey = value
        End Set
    End Property

    Property ToplogyImageKey() As String
        Get
            Return Me.p_ToplogyImageKey
        End Get
        Set(ByVal value As String)
            Me.p_ToplogyImageKey = value
        End Set
    End Property

    Property RasterCatalogImageKey() As String
        Get
            Return Me.p_RasterCatalogImageKey
        End Get
        Set(ByVal value As String)
            Me.p_RasterCatalogImageKey = value
        End Set
    End Property

    Property UnKnownImageKey() As String
        Get
            UnKnownImageKey = p_UnKnownImageKey
        End Get
        Set(ByVal value As String)
            p_UnKnownImageKey = value
        End Set
    End Property

End Class

'<Serializable()> _
'Public Class MapDBParameter

'#Region "内部变量"

'    Private p_MapServerName As String
'    'Private p_MapServerType As enumMapServerType = MapServerType.Sde
'    Private p_MapDataSourceName As String
'    Private p_MapInstance As String
'    Private p_MapDataUserName As String
'    Private p_MapDataPassword As String
'    Private p_DefaultVersion As String = "SDE.DEFAULT"
'    'Private p_SdeType As enumSdeType

'#End Region

'#Region "属性"

'    Public Property MapServerName() As String
'        Get
'            Return Me.p_MapServerName
'        End Get
'        Set(ByVal Value As String)
'            Me.p_MapServerName = Value
'        End Set
'    End Property

'    'Public Property MapServerType() As enumMapServerType
'    '    Get
'    '        Return Me.p_MapServerType
'    '    End Get
'    '    Set(ByVal value As enumMapServerType)
'    '        Me.p_MapServerType = value
'    '    End Set
'    'End Property

'    Public Property MapDataSourceName() As String
'        Get
'            Return Me.p_MapDataSourceName
'        End Get
'        Set(ByVal Value As String)
'            Me.p_MapDataSourceName = Value
'        End Set
'    End Property

'    Public Property MapInstance() As String
'        Get
'            Return Me.p_MapInstance
'        End Get
'        Set(ByVal Value As String)
'            Me.p_MapInstance = Value
'        End Set
'    End Property

'    Public Property MapDataUserName() As String
'        Get
'            Return Me.p_MapDataUserName
'        End Get
'        Set(ByVal Value As String)
'            Me.p_MapDataUserName = Value
'        End Set
'    End Property

'    Public Property MapDataPassword() As String
'        Get
'            Return Me.p_MapDataPassword
'        End Get
'        Set(ByVal Value As String)
'            Me.p_MapDataPassword = Value
'        End Set
'    End Property

'    Public ReadOnly Property DefaultVersion() As String
'        Get
'            DefaultVersion = p_DefaultVersion
'        End Get
'    End Property

'    'Property SdeType() As enumSdeType
'    '    Get
'    '        Return Me.p_SdeType
'    '    End Get
'    '    Set(ByVal value As enumSdeType)
'    '        Me.p_SdeType = value
'    '    End Set
'    'End Property

'#End Region

'#Region "构造函数"

'    Public Sub New()

'    End Sub

'#End Region

'End Class

''' <summary>
''' 连接空间数据的客户端。
''' </summary>
''' <remarks></remarks>
Public Class EsriHelper

#Region "创建要素类"

    ''' <summary>
    ''' 创建要素类。
    ''' </summary>
    ''' <param name="p_WorkspaceOrDataSet">输入：要创建要素类的工作空间或要素集。</param>
    ''' <param name="p_Name">输入：要素类的名称</param>
    ''' <param name="p_SpatialReference">输入：要素类的空间参考信息。</param>
    ''' <param name="p_FeatureType">输入：要素的类型。</param>
    ''' <param name="p_GeometryType">输入：几何图形的类型。</param>
    ''' <param name="p_Fields">输入：字段。</param>
    ''' <param name="p_ConfigWord">输入：配置关键字。</param>
    ''' <returns>创建成功返回创建的要素类，否则返回Nothing。</returns>
    ''' <remarks></remarks>
    Public Function CreateFeatureClass(ByVal p_WorkspaceOrDataSet As Object, ByVal p_Name As String, ByVal p_SpatialReference As ISpatialReference, ByVal p_FeatureType As esriFeatureType, ByVal p_GeometryType As esriGeometryType, ByVal p_Fields As IFields, ByVal p_ConfigWord As String) As IFeatureClass
        '检查参数的有效性
        If p_WorkspaceOrDataSet Is Nothing Then
            Return Nothing
        End If
        If Not ((TypeOf p_WorkspaceOrDataSet Is IWorkspace) Or (TypeOf p_WorkspaceOrDataSet Is IFeatureDataset)) Then
            Return Nothing
        End If

        If p_Name Is Nothing Then
            Return Nothing
        Else
            If p_Name = "" Then
                Return Nothing
            End If
        End If

        If ((TypeOf p_WorkspaceOrDataSet Is IWorkspace) And (p_SpatialReference Is Nothing)) Then
            Return Nothing
        End If

        '设置 ClassID EXTClassID
        Dim mCLSID As UID = Nothing
        Dim mEXTCLSID As UID = Nothing
        Select Case p_FeatureType
            Case esriFeatureType.esriFTAnnotation '注记
                'p_UidCLSID.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}"
                Dim mObjectClassDesc As IObjectClassDescription = New AnnotationFeatureClassDescription

                mCLSID = mObjectClassDesc.InstanceCLSID
                mEXTCLSID = mObjectClassDesc.ClassExtensionCLSID

                p_GeometryType = esriGeometryType.esriGeometryPolygon
            Case esriFeatureType.esriFTComplexEdge
                mCLSID = New UIDClass
                mCLSID.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}"

                p_GeometryType = esriGeometryType.esriGeometryPolyline
            Case esriFeatureType.esriFTComplexJunction
                mCLSID = New UIDClass
                mCLSID.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}"
            Case esriFeatureType.esriFTCoverageAnnotation '注记
                mCLSID = New UIDClass
                mCLSID.Value = "{4AEDC069-B599-424B-A374-49602ABAD308}"

                p_GeometryType = esriGeometryType.esriGeometryPolygon
            Case esriFeatureType.esriFTDimension
                'p_UidCLSID.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}"
                Dim mObjectClassDesc As IObjectClassDescription = New DimensionClassDescription

                mCLSID = mObjectClassDesc.InstanceCLSID
                mEXTCLSID = mObjectClassDesc.ClassExtensionCLSID

                p_GeometryType = esriGeometryType.esriGeometryPolygon
            Case esriFeatureType.esriFTRasterCatalogItem
                Dim mObjectClassDesc As IObjectClassDescription = New RasterCatalogClassDescription

                mCLSID = mObjectClassDesc.InstanceCLSID
                mEXTCLSID = mObjectClassDesc.ClassExtensionCLSID

                p_GeometryType = esriGeometryType.esriGeometryAny
            Case esriFeatureType.esriFTSimple
                'mCLSID.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}"
                Dim mObjectClassDesc As IObjectClassDescription = New FeatureClassDescription

                mCLSID = mObjectClassDesc.InstanceCLSID
                mEXTCLSID = mObjectClassDesc.ClassExtensionCLSID
            Case esriFeatureType.esriFTSimpleEdge
                mCLSID = New UIDClass
                mCLSID.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}"

                p_GeometryType = esriGeometryType.esriGeometryPolyline
            Case esriFeatureType.esriFTSimpleJunction
                mCLSID = New UIDClass
                mCLSID.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}"

                p_GeometryType = esriGeometryType.esriGeometryPoint
        End Select

        '创建字段集合
        Dim i As Integer
        Dim mFindOIDField As Boolean = False
        Dim mFindGeoField As Boolean = False

        Dim mOIDField As IField = Nothing
        Dim mShapeField As IField = Nothing

        If p_Fields IsNot Nothing Then
            '读取输入字段
            For i = p_Fields.FieldCount - 1 To 0 Step -1
                Dim tmpField As IField = p_Fields.Field(i)
                Select Case tmpField.Name.ToUpper
                    Case "OBJECTID".ToUpper
                        Dim tmpFieldsEdit As IFieldsEdit = p_Fields
                        tmpFieldsEdit.DeleteField(tmpField)
                End Select
                If p_Fields.Field(i).Type = esriFieldType.esriFieldTypeOID Then
                    mOIDField = tmpField
                    mFindOIDField = True

                    Dim tmpFieldsEdit As IFieldsEdit = p_Fields
                    tmpFieldsEdit.DeleteField(tmpField)
                End If
                If p_Fields.Field(i).Type = esriFieldType.esriFieldTypeGeometry Then
                    mShapeField = tmpField
                    mFindGeoField = True

                    Dim tmpFieldsEdit As IFieldsEdit = p_Fields
                    tmpFieldsEdit.DeleteField(tmpField)
                End If
            Next i
        End If

        Dim mFields As IFields = New FieldsClass()
        Dim mFieldsEdit As IFieldsEdit = mFields

        If Not mFindOIDField Then
            '创建OID字段
            Dim mField As IField = New FieldClass
            Dim mFieldEdit As IFieldEdit = mField
            mFieldEdit.Name_2 = "OBJECTID"
            mFieldEdit.AliasName_2 = "OBJECTID"
            mFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID
            mFieldsEdit.AddField(mField)
        Else
            mFieldsEdit.AddField(mOIDField)
        End If

        If Not mFindGeoField Then
            '创建字段
            Dim mGeometryDef As IGeometryDef = New GeometryDefClass()
            Dim mGeometryDefEdit As IGeometryDefEdit = mGeometryDef

            '设置几何图形定义
            mGeometryDefEdit.GeometryType_2 = p_GeometryType
            mGeometryDefEdit.GridCount_2 = 1
            mGeometryDefEdit.GridSize_2(0) = 1000 '0.5 'set_GridSize(0, 0.5)
            mGeometryDefEdit.AvgNumPoints_2 = 2
            mGeometryDefEdit.HasM_2 = False
            mGeometryDefEdit.HasZ_2 = False
            If TypeOf p_WorkspaceOrDataSet Is IWorkspace Then
                '如果是一个 STANDALONE FeatureClass 则添加空间参考信息
                mGeometryDefEdit.SpatialReference_2 = p_SpatialReference
            End If

            '创建几何图形字段
            Dim mField As IField = New FieldClass
            Dim mFieldEdit As IFieldEdit = mField
            mFieldEdit.Name_2 = "SHAPE"
            mFieldEdit.AliasName_2 = "SHAPE"
            mFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry
            mFieldEdit.GeometryDef_2 = mGeometryDef
            mFieldsEdit.AddField(mField)

            mShapeField = mField
        Else
            Dim mGeometryDef As IGeometryDef = mShapeField.GeometryDef
            Dim mGeometryDefEdit As IGeometryDefEdit = mGeometryDef
            If mGeometryDef Is Nothing Then
                mGeometryDef = New GeometryDefClass()
                mGeometryDefEdit = mGeometryDef

                mGeometryDefEdit.GeometryType_2 = p_GeometryType
                mGeometryDefEdit.GridCount_2 = 1
                mGeometryDefEdit.GridSize_2(0) = 1000 '0.5 'set_GridSize(0, 0.5)
                mGeometryDefEdit.AvgNumPoints_2 = 2
                mGeometryDefEdit.HasM_2 = False
                mGeometryDefEdit.HasZ_2 = False

                Dim mFieldEdit As IFieldEdit = mShapeField
                mFieldEdit.GeometryDef_2 = mGeometryDef
            End If
            If TypeOf p_WorkspaceOrDataSet Is IWorkspace Then
                '如果是一个 STANDALONE FeatureClass 则添加空间参考信息
                mGeometryDefEdit.SpatialReference_2 = p_SpatialReference
            End If

            mFieldsEdit.AddField(mShapeField)
        End If

        '添加其他字段
        If p_Fields IsNot Nothing Then
            '读取输入字段
            For i = 0 To p_Fields.FieldCount - 1
                Dim mField As IField = p_Fields.Field(i)
                If mField.Type <> esriFieldType.esriFieldTypeOID And mField.Type <> esriFieldType.esriFieldTypeGeometry Then
                    mFieldsEdit.AddField(mField)
                End If
            Next i
        End If

        '定位图形字段名称
        Dim mShapeFieldName As String = mShapeField.Name

        Dim mFeatureClass As IFeatureClass = Nothing

        If TypeOf p_WorkspaceOrDataSet Is IWorkspace Then
            ' Create a STANDALONE FeatureClass
            Dim mWorkspace As IWorkspace = p_WorkspaceOrDataSet
            Dim mFeatureWorkspace As IFeatureWorkspace = mWorkspace
            Try
                mFeatureClass = mFeatureWorkspace.CreateFeatureClass(p_Name, mFields, mCLSID, mEXTCLSID, p_FeatureType, mShapeFieldName, p_ConfigWord)
            Catch ex As Exception
                Return Nothing
            End Try
        ElseIf TypeOf p_WorkspaceOrDataSet Is IFeatureDataset Then

            Dim mFeatureDataset As IFeatureDataset = p_WorkspaceOrDataSet

            Try
                mFeatureClass = mFeatureDataset.CreateFeatureClass(p_Name, mFields, mCLSID, mEXTCLSID, p_FeatureType, mShapeFieldName, p_ConfigWord)
            Catch ex As Exception
                Return Nothing
            End Try
        End If

        Return mFeatureClass
    End Function

    ''' <summary>
    ''' 创建注记要素类。
    ''' </summary>
    ''' <param name="p_WorkspaceOrDataSet">输入：要创建要素类的工作空间或要素集。</param>
    ''' <param name="p_Name">输入：注记类的名称</param>
    ''' <param name="p_SpatialReference">输入：注记类的空间参考信息。</param>
    ''' <param name="p_ReferenceScale">输入：注记的参考比例。</param>
    ''' <param name="p_Units">输入：注记的参考单位。</param>
    ''' <returns>创建成功返回创建的注记类，否则返回Nothing。</returns>
    ''' <remarks></remarks>
    Public Function CreateAnnotationClass(ByVal p_WorkspaceOrDataSet As Object, ByVal p_Name As String, ByVal p_Fields As IFields, ByVal p_SpatialReference As ISpatialReference, ByVal p_ReferenceScale As Double, ByVal p_Units As esriUnits) As IFeatureClass
        '检查参数的有效性
        If p_WorkspaceOrDataSet Is Nothing Then
            Return Nothing
        End If
        If Not ((TypeOf p_WorkspaceOrDataSet Is IWorkspace) Or (TypeOf p_WorkspaceOrDataSet Is IFeatureDataset)) Then
            Return Nothing
        End If

        If p_Name Is Nothing Then
            Return Nothing
        Else
            If p_Name = "" Then
                Return Nothing
            End If
        End If

        If ((TypeOf p_WorkspaceOrDataSet Is IWorkspace) And (p_SpatialReference Is Nothing)) Then
            Return Nothing
        End If

        '设置 ClassID EXTClassID
        Dim mObjectClassDesc As IObjectClassDescription = New AnnotationFeatureClassDescription
        Dim mFeatureClassDesc As IFeatureClassDescription = mObjectClassDesc

        Dim mCLSID As UID = mObjectClassDesc.InstanceCLSID
        Dim mEXTCLSID As UID = mObjectClassDesc.ClassExtensionCLSID


        Dim mFindOIDField As Boolean = False
        Dim mFindGeoField As Boolean = False

        Dim mOIDField As IField = Nothing
        Dim mShapeField As IField = Nothing

        If p_Fields IsNot Nothing Then
            For i As Integer = p_Fields.FieldCount - 1 To 0 Step -1
                Dim tmpField As IField = p_Fields.Field(i)
                Select Case tmpField.Name.ToUpper
                    Case "OBJECTID".ToUpper, "FeatureID".ToUpper, "ZOrder".ToUpper, "AnnotationClassID".ToUpper, "Element".ToUpper, "SymbolID".ToUpper, "Status".ToUpper, "TextString".ToUpper, "FontName".ToUpper, "FontSize".ToUpper, "Bold".ToUpper, "Italic".ToUpper, "Underline".ToUpper, "VerticalAlignment".ToUpper, "HorizontalAlignment".ToUpper, "XOffset".ToUpper, "YOffset".ToUpper, "Angle".ToUpper, "FontLeading".ToUpper, "WordSpacing".ToUpper, "CharacterWidth".ToUpper, "CharacterSpacing".ToUpper, "FlipAngle".ToUpper, "Override".ToUpper
                        Dim tmpFieldsEdit As IFieldsEdit = p_Fields
                        tmpFieldsEdit.DeleteField(tmpField)
                End Select
                If tmpField.Type = esriFieldType.esriFieldTypeOID Then
                    mOIDField = tmpField
                    mFindOIDField = True

                    Dim tmpFieldsEdit As IFieldsEdit = p_Fields
                    tmpFieldsEdit.DeleteField(tmpField)
                End If
                If tmpField.Type = esriFieldType.esriFieldTypeGeometry Then
                    mShapeField = tmpField
                    mFindGeoField = True

                    Dim tmpFieldsEdit As IFieldsEdit = p_Fields
                    tmpFieldsEdit.DeleteField(tmpField)
                End If
            Next
        End If

        '创建字段集合
        Dim mFields As IFields = New FieldsClass()
        Dim mFieldsEdit As IFieldsEdit = mFields

        Dim mField As IField = Nothing
        Dim mFieldEdit As IFieldEdit = Nothing

        If Not mFindOIDField Then
            mField = New FieldClass
            mFieldEdit = mField
            mFieldEdit.Name_2 = "OBJECTID"
            mFieldEdit.AliasName_2 = "OBJECTID"
            mFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID
            mFieldsEdit.AddField(mField)
        Else
            mFieldsEdit.AddField(mOIDField)
        End If

        If Not mFindGeoField Then
            '创建字段
            Dim mGeometryDef As IGeometryDef = New GeometryDefClass()
            Dim mGeometryDefEdit As IGeometryDefEdit = mGeometryDef

            '设置几何图形定义
            mGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon
            mGeometryDefEdit.GridCount_2 = 1
            mGeometryDefEdit.GridSize_2(0) = 1000 '0.5 'set_GridSize(0, 0.5)
            mGeometryDefEdit.AvgNumPoints_2 = 2
            mGeometryDefEdit.HasM_2 = False
            mGeometryDefEdit.HasZ_2 = False
            If TypeOf p_WorkspaceOrDataSet Is IWorkspace Then
                '如果是一个 STANDALONE FeatureClass 则添加空间参考信息
                mGeometryDefEdit.SpatialReference_2 = p_SpatialReference
            End If

            '创建几何图形字段
            mField = New FieldClass
            mFieldEdit = mField
            mFieldEdit.Name_2 = mFeatureClassDesc.ShapeFieldName '"SHAPE"
            mFieldEdit.AliasName_2 = mFeatureClassDesc.ShapeFieldName
            mFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry
            mFieldEdit.GeometryDef_2 = mGeometryDef
            mFieldsEdit.AddField(mField)
        Else
            Dim mGeometryDef As IGeometryDef = mShapeField.GeometryDef
            Dim mGeometryDefEdit As IGeometryDefEdit = mGeometryDef
            If mGeometryDef Is Nothing Then
                mGeometryDef = New GeometryDefClass()
                mGeometryDefEdit = mGeometryDef

                '设置几何图形定义
                mGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon
                mGeometryDefEdit.GridCount_2 = 1
                mGeometryDefEdit.GridSize_2(0) = 1000 '0.5 'set_GridSize(0, 0.5)
                mGeometryDefEdit.AvgNumPoints_2 = 2
                mGeometryDefEdit.HasM_2 = False
                mGeometryDefEdit.HasZ_2 = False

                mFieldEdit = mShapeField
                mFieldEdit.GeometryDef_2 = mGeometryDef
            End If
            If TypeOf p_WorkspaceOrDataSet Is IWorkspace Then
                '如果是一个 STANDALONE FeatureClass 则添加空间参考信息
                mGeometryDefEdit.SpatialReference_2 = p_SpatialReference
            End If

            mFieldsEdit.AddField(mShapeField)
        End If

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "FeatureID"
        mFieldEdit.AliasName_2 = "要素ID"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "ZOrder"
        mFieldEdit.AliasName_2 = "Z顺序"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "AnnotationClassID"
        mFieldEdit.AliasName_2 = "注记类ID"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "Element"
        mFieldEdit.AliasName_2 = "元素"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeBlob
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "SymbolID"
        mFieldEdit.AliasName_2 = "符号ID"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "Status"
        mFieldEdit.AliasName_2 = "状态"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "TextString"
        mFieldEdit.AliasName_2 = "注记文本"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeString
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "FontName"
        mFieldEdit.AliasName_2 = "字体名称"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeString
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "FontSize"
        mFieldEdit.AliasName_2 = "字体大小"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "Bold"
        mFieldEdit.AliasName_2 = "加粗"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "Italic"
        mFieldEdit.AliasName_2 = "斜体"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "Underline"
        mFieldEdit.AliasName_2 = "下划线"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "VerticalAlignment"
        mFieldEdit.AliasName_2 = "垂直对齐方式"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "HorizontalAlignment"
        mFieldEdit.AliasName_2 = "水平对齐方式"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "XOffset"
        mFieldEdit.AliasName_2 = "X偏移量"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "YOffset"
        mFieldEdit.AliasName_2 = "Y偏移量"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "Angle"
        mFieldEdit.AliasName_2 = "倾斜角度"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "FontLeading"
        mFieldEdit.AliasName_2 = "字体引导"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "WordSpacing"
        mFieldEdit.AliasName_2 = "字间距"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "CharacterWidth"
        mFieldEdit.AliasName_2 = "字符宽度"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "CharacterSpacing"
        mFieldEdit.AliasName_2 = "字符间距"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "FlipAngle"
        mFieldEdit.AliasName_2 = "旋转角度"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        mFieldsEdit.AddField(mField)

        mField = New Field
        mFieldEdit = mField
        mFieldEdit.Name_2 = "Override"
        mFieldEdit.AliasName_2 = "删除线"
        mFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        mFieldsEdit.AddField(mField)

        If p_Fields IsNot Nothing Then
            For i As Integer = 0 To p_Fields.FieldCount - 1
                Dim tmpField As IField = p_Fields.Field(i)
                mFieldsEdit.AddField(tmpField)
            Next
        End If

        Dim mFeatureClass As IFeatureClass = Nothing

        '设置参考比例
        Dim mGraphicsLayerScale As IGraphicsLayerScale = New GraphicsLayerScale
        mGraphicsLayerScale.ReferenceScale = p_ReferenceScale
        mGraphicsLayerScale.Units = p_Units

        ''设置符号集合
        'Dim mClone As IClone = mAnnoAdmin.SymbolCollection
        'Dim mSymbolColl As ISymbolCollection = mClone.Clone

        Dim mFeatureWorkspaceAnno As IFeatureWorkspaceAnno = Nothing
        If TypeOf p_WorkspaceOrDataSet Is IWorkspace Then
            mFeatureWorkspaceAnno = p_WorkspaceOrDataSet
            Try
                mFeatureClass = mFeatureWorkspaceAnno.CreateAnnotationClass(p_Name, mFields, mCLSID, mEXTCLSID, mFeatureClassDesc.ShapeFieldName, Nothing, Nothing, Nothing, Nothing, mGraphicsLayerScale, Nothing, True) 'pSymbolColl 
            Catch ex As Exception
                Return Nothing
            End Try
        ElseIf TypeOf p_WorkspaceOrDataSet Is IFeatureDataset Then
            Dim mFeatureDataset As IFeatureDataset = p_WorkspaceOrDataSet
            mFeatureWorkspaceAnno = mFeatureDataset.Workspace
            Try
                mFeatureClass = mFeatureWorkspaceAnno.CreateAnnotationClass(p_Name, mFields, mCLSID, mEXTCLSID, mFeatureClassDesc.ShapeFieldName, Nothing, mFeatureDataset, Nothing, Nothing, mGraphicsLayerScale, Nothing, True) 'pSymbolColl
            Catch ex As Exception
                Return Nothing
            End Try
        End If

        Return mFeatureClass
    End Function

    ''' <summary>
    ''' 创建要素集。
    ''' </summary>
    ''' <param name="p_WorkSpace">输入：要创建要素集的工作空间。</param>
    ''' <param name="p_Name">输入：要素集的名称。</param>
    ''' <param name="p_SpatialReference">输入：要素集的空间参考信息。</param>
    ''' <returns>创建成功返回创建的要素集，否则返回Nothing。</returns>
    ''' <remarks></remarks>
    Public Function CreateFeatureDataSet(ByVal p_WorkSpace As IWorkspace, ByVal p_Name As String, ByVal p_SpatialReference As ISpatialReference) As IDataset
        If p_WorkSpace Is Nothing Then
            Return Nothing
        End If

        If p_Name Is Nothing Then
            Return Nothing
        Else
            If p_Name = "" Then
                Return Nothing
            End If
        End If

        If p_SpatialReference Is Nothing Then
            Return Nothing
        End If

        Dim mFeatureWorkspace As IFeatureWorkspace = p_WorkSpace
        Dim mDataSet As IDataset = Nothing

        Try
            mDataSet = mFeatureWorkspace.CreateFeatureDataset(p_Name, p_SpatialReference)
        Catch ex As Exception
            Return Nothing
        End Try

        Return mDataSet
    End Function

#End Region

End Class
