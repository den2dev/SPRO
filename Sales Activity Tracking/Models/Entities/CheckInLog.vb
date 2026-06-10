Public Class CheckInLog

    Public Property ID As Long

    Public Property EmployeeCode As String

    Public Property CheckInDateTime As DateTime
    Public Property CheckInLatitude As String
    Public Property CheckInLongitude As String
    Public Property CheckInPhotoPath As String

    Public Property CheckOutDateTime As Nullable(Of DateTime)
    Public Property CheckOutLatitude As String
    Public Property CheckOutLongitude As String
    Public Property CheckOutPhotoPath As String

End Class


'CREATE TABLE CheckInLog
'(
'    ID BIGINT IDENTITY(1,1) PRIMARY KEY,

'    EmployeeCode NVARCHAR(50),

'    CheckInDateTime DATETIME,
'    CheckInLatitude NVARCHAR(50),
'    CheckInLongitude NVARCHAR(50),
'    CheckInPhotoPath NVARCHAR(500),

'    CheckOutDateTime DATETIME NULL,
'    CheckOutLatitude NVARCHAR(50) NULL,
'    CheckOutLongitude NVARCHAR(50) NULL,
'    CheckOutPhotoPath NVARCHAR(500) NULL
')