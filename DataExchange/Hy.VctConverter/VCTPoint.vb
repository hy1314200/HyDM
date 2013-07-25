Imports ESRI.ArcGIS.Geometry

Public Class VctPoint

#Region "内部变量"

    Private p_ID As Int64
    Private p_FID As String
    Private p_LayerName As String = "Unknow"
    Private p_FeatureType As Integer
    Private p_X As Double
    Private p_Y As Double

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
    ''' 点的特征类型
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

    Public Property X() As Double
        Get
            Return Me.p_X
        End Get
        Set(ByVal value As Double)
            Me.p_X = value
        End Set
    End Property

    Public Property Y() As Double
        Get
            Return Me.p_Y
        End Get
        Set(ByVal value As Double)
            Me.p_Y = value
        End Set
    End Property

#End Region

#Region "公有函数"

    Public Function ConvertToEsriGeometry() As IPoint
        Dim mPoint As IPoint = New ESRI.ArcGIS.Geometry.Point
        mPoint.PutCoords(Me.X, Me.Y)
        Return mPoint
    End Function

#End Region

End Class


