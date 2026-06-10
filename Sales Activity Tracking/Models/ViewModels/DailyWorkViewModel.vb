
Public Class DailyWorkViewModel
    Public Property SalesmanCode As String
    Public Property SalesmanName As String
    Public Property VehicleList As IEnumerable(Of SelectListItem) 'get value เมื่อยังมี่มีการ login


    Public Property WorkDate As Date
    Public Property VehicleLicensePlate As String 'เลขทะเบียน
    Public Property OdometerStart As String 'เลขไมล์เริ่มต้น


    Public Property IsCheckedIn As Boolean 'มีการเช็ค TimeIn แล้ว?
    Public Property IsCheckedOut As Boolean 'มีการเช็ค TimeOut แล้ว?

    Public Property TimeInDate As String 'วันที่ TimeIn 
    Public Property TimeInTime As String 'เวลา TimeIn 
    Public Property TimeOutDate As String 'วันที่ TimeOut
    Public Property TimeOutTime As String 'เวลา TimeIn 

    Public Property DailyWorks As List(Of WorkItem)
End Class