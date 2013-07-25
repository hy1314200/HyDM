Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

Public Enum vctFieldType
    vctString
    vctDouble
    vctInt
    vctBoolean
    vctDateTime
    vctVarbin
End Enum

Public Class vctField

#Region "内部变量"

    Private p_Name As String
    Private p_Type As vctFieldType
    Private p_Length As Integer
    Private p_DecimalLength As Integer = 0

#End Region

#Region "属性"

    Public Property Name() As String
        Get
            Return Me.p_Name
        End Get
        Set(ByVal value As String)
            Me.p_Name = value
        End Set
    End Property

    Public Property Type() As vctFieldType
        Get
            Return Me.p_Type
        End Get
        Set(ByVal value As vctFieldType)
            Me.p_Type = value
        End Set
    End Property

    Public Property Length() As Integer
        Get
            Return Me.p_Length
        End Get
        Set(ByVal value As Integer)
            Me.p_Length = value
        End Set
    End Property

    Public Property DecimalLength() As Integer
        Get
            Return Me.p_DecimalLength
        End Get
        Set(ByVal value As Integer)
            Me.p_DecimalLength = value
        End Set
    End Property

#End Region

#Region "公有函数"

    Public Function ConvertToESRIField() As IField
        Dim mField As IField = New FieldClass
        Dim mFieldEdit As IFieldEdit = mField
        mFieldEdit.Name_2 = Me.Name
        Select Case Me.Type
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

            Case vctFieldType.vctVarbin '二进制
                mFieldEdit.Type_2 = esriFieldType.esriFieldTypeBlob

        End Select
        '  mFieldEdit.Precision_2 = Me.DecimalLength
        'mFieldEdit.Length_2 = Me.Length
        Return mField
    End Function

#End Region

End Class
