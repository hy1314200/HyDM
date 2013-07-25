Imports ESRI.ArcGIS.Geometry

Public Class VctLine

#Region "内部变量"

    Private p_ID As Int64
    Private p_FID As String
    Private p_LayerName As String = "Unknow"
    Private p_FeatureType As Integer
    Private p_PointCount As Integer
    Private p_Points As New List(Of VctPoint)

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
    ''' 线的特征类型
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
    Property PointCount() As Integer
        Get
            Return Me.p_PointCount
        End Get
        Set(ByVal value As Integer)
            Me.p_PointCount = value
        End Set
    End Property

    Property Points() As List(Of VctPoint)
        Get
            Return Me.p_Points
        End Get
        Set(ByVal value As List(Of VctPoint))
            Me.p_Points = value
        End Set
    End Property

#End Region

#Region "公有变量"

    Public Function ConvertToEsriGeometry() As IPolyline

        Dim mPoints As IPointCollection = New Polyline
        For i As Integer = 0 To Me.p_Points.Count - 1
            Dim mPoint As IPoint = New ESRI.ArcGIS.Geometry.Point : mPoint.PutCoords(Me.p_Points(i).X, Me.p_Points(i).Y)

            mPoints.AddPoint(mPoint)
        Next
        Dim mPolyLine As IPolyline = mPoints
        Return mPolyLine
    End Function

#End Region

End Class
