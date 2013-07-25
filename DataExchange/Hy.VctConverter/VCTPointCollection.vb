Imports ESRI.ArcGIS.Geometry

Public Class VctPointCollection

    Private p_Points As New List(Of VctPoint)

    Property Points() As List(Of VctPoint)
        Get
            Return Me.p_Points
        End Get
        Set(ByVal value As List(Of VctPoint))
            Me.p_Points = value
        End Set
    End Property

    Public Function ConvertToESRIRing() As IRing

        Dim mPoints As IPointCollection = New Ring
        For i As Integer = 0 To Me.Points.Count - 1
            Dim mPoint As IPoint = New ESRI.ArcGIS.Geometry.Point
            mPoint.PutCoords(Me.Points(i).X, Me.Points(i).Y)
            mPoints.AddPoint(mPoint)
        Next
        Dim mRing As IRing = mPoints
        Return mRing
    End Function

End Class
