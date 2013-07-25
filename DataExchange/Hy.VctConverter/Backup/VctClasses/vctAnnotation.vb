Imports System.Drawing
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.systemUI
Imports stdole

Public Enum vctFontType
    Regular = 0
    left = 1
    right = 2
    lefttop = 3
    righttop = 4
End Enum

Public Class vctAnnotation

#Region "字段"

    Private p_ID As Int64
    Private p_FID As String
    Private p_LayerName As String = "Unknow"
    Private p_FontName As String
    Private p_Color As Color
    Private p_Weigh As Integer
    Private p_Italic As vctFontType
    Private p_UnderLine As String
    Private p_FontSize As Double
    Private p_Margin As Double
    Private p_Text As String
    Private p_Pointcount As Integer
    Private p_Points As New List(Of vctPoint)

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

    Property FontName() As String
        Get
            Return Me.p_FontName
        End Get
        Set(ByVal value As String)
            Me.p_FontName = value
        End Set
    End Property

    Property Color() As Color
        Get
            Return Me.p_Color
        End Get
        Set(ByVal value As Color)
            Me.p_Color = value
        End Set
    End Property

    Property Weigh() As Integer
        Get
            Return Me.p_Weigh
        End Get
        Set(ByVal value As Integer)
            Me.p_Weigh = value
        End Set
    End Property

    Property Italic() As vctFontType
        Get
            Return Me.p_Italic
        End Get
        Set(ByVal value As vctFontType)
            Me.p_Italic = value
        End Set
    End Property

    Property UnderLine() As String
        Get
            Return Me.p_UnderLine
        End Get
        Set(ByVal value As String)
            Me.p_UnderLine = value
        End Set
    End Property

    Property FontSize() As Integer
        Get
            Return Me.p_FontSize
        End Get
        Set(ByVal value As Integer)
            Me.p_FontSize = value
        End Set
    End Property

    ''' <summary>
    ''' 文字间隔
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Margin() As Double
        Get
            Return Me.p_Margin
        End Get
        Set(ByVal value As Double)
            Me.p_Margin = value
        End Set
    End Property

    ''' <summary>
    ''' 注记内容
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Text() As String
        Get
            Return Me.p_Text
        End Get
        Set(ByVal value As String)
            Me.p_Text = value
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
            Return Me.p_Pointcount
        End Get
        Set(ByVal value As Integer)
            Me.p_Pointcount = value
        End Set
    End Property

    Property Points() As List(Of vctPoint)
        Get
            Return Me.p_Points
        End Get
        Set(ByVal value As List(Of vctPoint))
            Me.p_Points = value
        End Set
    End Property

#End Region

#Region "私有函数"

    Private Function GetSymbol() As ISymbol
        Dim mTextSymbol As ITextSymbol = New TextSymbol
        Dim mColor As IRgbColor = New RgbColor
        mColor.RGB = Me.p_Color.ToArgb
        mTextSymbol.Color = mColor
        Dim mFont As IFont = New SystemFont
        mFont.Name = Me.p_FontName
        mFont.Size = Me.p_FontSize
        mFont.Weight = Me.p_Weigh

        mTextSymbol.Font = mFont
        Return mTextSymbol
    End Function

    Private Function GetGeometry() As IGeometry
        Dim mgeometry As IGeometry = Nothing
        If Me.p_Points.Count = 1 Then
            Dim mPoint As IPoint = New ESRI.ArcGIS.Geometry.Point
            mPoint.PutCoords(Me.p_Points(0).x, Me.p_Points(0).y)
            mgeometry = mPoint
        Else
            Dim mPoints As IPointCollection = New Polyline
            For i As Integer = 0 To Me.p_Points.Count - 1
                Dim mPoint As IPoint = New ESRI.ArcGIS.Geometry.Point
                mPoint.PutCoords(Me.p_Points(i).x, Me.p_Points(i).y)
                mPoints.AddPoint(mPoint)
            Next
            Dim mpolyline As IPolyline = mPoints
            mgeometry = mpolyline
        End If
        Return mgeometry
    End Function

#End Region

#Region "公有函数"

    Public Function GetElement() As IElement
        Dim mTextElement As ITextElement = New TextElement

        mTextElement.Text = Me.p_Text
        mTextElement.ScaleText = False
        mTextElement.Symbol = Me.GetSymbol
        Dim mElememnt As IElement = mTextElement
        mElememnt.Geometry = Me.GetGeometry
        Return mElememnt
    End Function

#End Region

End Class
