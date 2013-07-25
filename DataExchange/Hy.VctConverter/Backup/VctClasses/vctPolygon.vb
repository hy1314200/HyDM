Imports ESRI.ArcGIS.Geometry

Public Class vctPolygon

#Region "内部变量"

    Private p_ID As Int64
    Private p_FID As String
    Private p_LayerName As String = "Unknow"
    Private p_LabelX As Double
    Private p_LabelY As Double
    Private p_PointCounts As New List(Of Int64)
    Private p_PointCollections As New List(Of vctPointCollection)
    Private p_FeatureType As Integer

#End Region

#Region "属性"

    ''' <summary>
    ''' 标识码
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID() As Int64

        Get
            Return Me.p_ID
        End Get
        Set(ByVal value As Int64)
            Me.p_ID = value
        End Set
    End Property

    ''' <summary>
    ''' 要素代码
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FID() As String
        Get
            Return Me.p_FID
        End Get
        Set(ByVal value As String)
            Me.p_FID = value
        End Set
    End Property

    ''' <summary>
    ''' 层名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LayerName() As String
        Get
            Return Me.p_LayerName
        End Get
        Set(ByVal value As String)
            p_LayerName = value
        End Set
    End Property

    ''' <summary>
    ''' 面的特征类型
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FeatureType() As Integer
        Get
            Return p_FeatureType
        End Get
        Set(ByVal value As Integer)
            p_FeatureType = value

        End Set
    End Property

    ''' <summary>
    ''' 点数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PointCount() As List(Of Int64)
        Get
            Return Me.p_PointCounts
        End Get
        Set(ByVal value As List(Of Int64))
            Me.p_PointCounts = value
        End Set
    End Property

    Property PointCollections() As List(Of vctPointCollection)
        Get
            Return Me.p_PointCollections
        End Get
        Set(ByVal value As List(Of vctPointCollection))
            Me.p_PointCollections = value
        End Set
    End Property

    Property LabelX() As Double
        Get
            Return Me.p_LabelX
        End Get
        Set(ByVal value As Double)
            Me.p_LabelX = value
        End Set
    End Property

    Property LabelY() As Double
        Get
            Return Me.p_LabelY
        End Get
        Set(ByVal value As Double)
            Me.p_LabelY = value
        End Set
    End Property

#End Region

#Region "公有函数"

    Public Function ConvertToEsriGeometry() As IPolygon
        Dim mgeometryCollection As IGeometryCollection = New Polygon
        For i As Integer = 0 To Me.PointCollections.Count - 1
            Dim mPointCollection As vctPointCollection = Me.PointCollections(i)
            Dim mSubPolygon As IRing = mPointCollection.ConvertToESRIRing
            mgeometryCollection.AddGeometry(mSubPolygon)
        Next
        Return mgeometryCollection
    End Function

#End Region
   
End Class
