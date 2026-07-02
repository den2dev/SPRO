Public Class DailyReportViewModel
    Public Property SalesmanCode As String
    Public Property UserID As String
    Public Property VehicleType As String
    Public Property VehicleNo As String
    Public Property TIO_Date As String
    Public Property TIO_DocNumber As String
    Public Property TimeIn_DateTime As String
    Public Property TimeOut_DateTime As String
    Public Property IsTimeOut As Boolean
    Public Property OdometerStart As Integer
    Public Property OdometerEnd As Integer
    Public Property GeoLocation As String
    Public Property ActivityItems As New List(Of ActivityItem)

End Class
