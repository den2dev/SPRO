Public Class ActivityItem

    Public Property ActivityCode As String 'IA1
    Public ReadOnly Property ActivityName As String 'เอกสารคำขอ
        Get
            Select Case ActivityCode
                Case "VIS" : Return "ตรวจเยี่ยมชาวไร่"
                Case "TIO" : Return "TimeIn/TimeOut"
                Case Else
                    Return ""
            End Select
        End Get
    End Property
    Public Property ActivityNumber As String '26IA1904250001

    Public Property ContactCode As String '19900006
    Public Property ContactName As String 'อโณทัย ชาวไร่

    Public ReadOnly Property IsNewCont As Boolean
        Get
            If Not String.IsNullOrEmpty(ContactCode) Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public ReadOnly Property TypeContactName As String 'ชาวไร่เดิม,ชาวไร่ใหม่
        Get
            If IsNewCont = True Then
                Return "ชาวไร่ใหม่"
            Else
                Return "ชาวไร่เดิม"
            End If
        End Get
    End Property

    Public Property CheckInDateTime As String '03/05/2026 08:20
    Public Property CheckOutDateTime As String '03/05/2026 08:40

    Public ReadOnly Property IsCheckOut As Boolean
        Get
            Return Not String.IsNullOrEmpty(CheckOutDateTime)
        End Get
    End Property
End Class
