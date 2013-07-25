
Public Class VctMap

#Region "内部变量"

    Private p_Layers As New List(Of VctLayer)

#End Region

#Region "属性"

    Property Layers() As List(Of VctLayer)
        Get
            Return Me.p_Layers
        End Get
        Set(ByVal value As List(Of VctLayer))
            Me.p_Layers = value
        End Set
    End Property

#End Region

#Region "公有函数"

    Public Function GetLayerByName(ByVal p_LayerName As String) As VctLayer
        For i As Integer = 0 To Me.Layers.Count - 1
            If Me.Layers(i).Name = p_LayerName Then
                Return Me.Layers(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetLayerByChineseName(ByVal p_LayerChineseName As String) As VctLayer
        For i As Integer = 0 To Me.Layers.Count - 1
            If Me.Layers(i).ChineseName = p_LayerChineseName Then
                Return Me.Layers(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetLayerByCode(ByVal p_LayerCode As String) As VctLayer
        For i As Integer = 0 To Me.Layers.Count - 1
            If Me.Layers(i).Code = p_LayerCode Then
                Return Me.Layers(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetLineByID(ByVal p_ID As Integer) As VctLine
        For i As Integer = 0 To Me.Layers.Count - 1
            Dim mLayer As VctLayer = Me.Layers(i)
            For j As Integer = 0 To mLayer.Lines.Count - 1
                Dim mLine As VctLine = mLayer.Lines(j)
                If mLine.ID = p_ID Then
                    Return mLine
                End If
            Next
        Next
        Return Nothing
    End Function

#End Region

End Class
