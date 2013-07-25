Imports System.Drawing
Imports ESRI.ArcGIS.Geometry



Public Class VctLayer

#Region "字段"

    Private p_Code As String
    Private p_Name As String
    Private p_ChineseName As String
    Private p_LayerType As enumLayerType
    Private p_DefaultColor As Color
    Private p_ExtTableName As String

    Private p_FieldCount As Integer
    Private p_Fields As New List(Of VctField)
    Private p_Points As New List(Of VctPoint)
    Private p_Lines As New List(Of VctLine)
    Private p_Polygons As New List(Of VctPolygon)
    Private p_Annotations As New List(Of VctAnnotation)
    Private p_Attributes As New List(Of String)
    Private p_DataTable As New DataTable

#End Region

#Region "属性"

    ''' <summary>
    ''' 图层编码(要素代码)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code() As String
        Get
            Return Me.p_Code
        End Get
        Set(ByVal value As String)
            Me.p_Code = value
        End Set
    End Property

    ''' <summary>
    ''' 属性表名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name() As String
        Get
            Return Me.p_Name
        End Get
        Set(ByVal value As String)
            Me.p_Name = value
        End Set
    End Property

    ''' <summary>
    ''' 图层名(要素名称)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChineseName() As String
        Get
            Return Me.p_ChineseName
        End Get
        Set(ByVal value As String)
            Me.p_ChineseName = value
        End Set
    End Property

    ''' <summary>
    ''' 几何类型
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LayerType() As enumLayerType
        Get
            Return Me.p_LayerType
        End Get
        Set(ByVal value As enumLayerType)
            Me.p_LayerType = value
        End Set
    End Property

    Public Property DefaultColor() As Color
        Get
            Return Me.p_DefaultColor
        End Get
        Set(ByVal value As Color)
            Me.p_DefaultColor = value
        End Set
    End Property

    ''' <summary>
    ''' 扩展属性表名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExtTableName() As String
        Get
            Return (Me.p_ExtTableName)
        End Get
        Set(ByVal value As String)
            Me.p_ExtTableName = value
        End Set
    End Property

    Property FieldCount() As Integer
        Get
            Return Me.p_FieldCount
        End Get
        Set(ByVal value As Integer)
            Me.p_FieldCount = value
        End Set
    End Property

    ReadOnly Property Fields() As List(Of VctField)
        Get
            Return Me.p_Fields
        End Get
    End Property

    ReadOnly Property Points() As List(Of VctPoint)
        Get
            Return Me.p_Points
        End Get
    End Property

    ReadOnly Property Lines() As List(Of VctLine)
        Get
            Return Me.p_Lines
        End Get
    End Property

    ReadOnly Property Polygons() As List(Of VctPolygon)
        Get
            Return Me.p_Polygons
        End Get
    End Property

    ReadOnly Property Annotations() As List(Of VctAnnotation)
        Get
            Return Me.p_Annotations
        End Get
    End Property

    ReadOnly Property Attributes() As List(Of String)
        Get
            Return Me.p_Attributes
        End Get
    End Property

    ReadOnly Property DataTable() As DataTable
        Get
            Return Me.p_DataTable
        End Get
    End Property

#End Region

#Region ""

    Shared Function ConvertVctGeoTypeToESRIGeoType(ByVal p_VctGeoType As enumLayerType) As esriGeometryType
        Select Case p_VctGeoType
            Case enumLayerType.Annotation
                Return esriGeometryType.esriGeometryAny
            Case enumLayerType.Line
                Return esriGeometryType.esriGeometryPolyline
            Case enumLayerType.Point
                Return esriGeometryType.esriGeometryPoint
            Case enumLayerType.Polygon
                Return esriGeometryType.esriGeometryPolygon
        End Select
    End Function

#End Region

End Class
