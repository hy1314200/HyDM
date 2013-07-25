Imports System.Drawing
Imports ESRI.ArcGIS.Geometry

Public Enum vctLayerType
    Point
    Line
    Polygon
    Annotation
    Image
End Enum

Public Class vctLayer

#Region "字段"

    Private p_Code As String
    Private p_Name As String
    Private p_ChineseName As String
    Private p_LayerType As vctLayerType
    Private p_DefaultColor As Color
    Private p_ExtTableName As String

    Private p_FieldCount As Integer
    Private p_Fields As New List(Of vctField)
    Private p_Points As New List(Of vctPoint)
    Private p_Lines As New List(Of vctLine)
    Private p_Polygons As New List(Of vctPolygon)
    Private p_Annotations As New List(Of vctAnnotation)
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
    Public Property LayerType() As vctLayerType
        Get
            Return Me.p_LayerType
        End Get
        Set(ByVal value As vctLayerType)
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

    ReadOnly Property Fields() As List(Of vctField)
        Get
            Return Me.p_Fields
        End Get
    End Property

    ReadOnly Property Points() As List(Of vctPoint)
        Get
            Return Me.p_Points
        End Get
    End Property

    ReadOnly Property Lines() As List(Of vctLine)
        Get
            Return Me.p_Lines
        End Get
    End Property

    ReadOnly Property Polygons() As List(Of vctPolygon)
        Get
            Return Me.p_Polygons
        End Get
    End Property

    ReadOnly Property Annotations() As List(Of vctAnnotation)
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

    Shared Function ConvertVctGeoTypeToESRIGeoType(ByVal p_VctGeoType As vctLayerType) As esriGeometryType
        Select Case p_VctGeoType
            Case vctLayerType.Annotation
                Return esriGeometryType.esriGeometryAny
            Case vctLayerType.Line
                Return esriGeometryType.esriGeometryPolyline
            Case vctLayerType.Point
                Return esriGeometryType.esriGeometryPoint
            Case vctLayerType.Polygon
                Return esriGeometryType.esriGeometryPolygon
        End Select
    End Function

#End Region

End Class
