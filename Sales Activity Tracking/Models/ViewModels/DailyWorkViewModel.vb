
Public Class DailyWorkViewModel
    Public Property IsMustTimeOut As Boolean

    Public Property IsTimeIn As Boolean 'มีการเช็ค TimeIn แล้ว?
    Public Property IsTimeOut As Boolean 'มีการเช็ค TimeOut แล้ว?

    Public Property UserID As String
    Public Property SalesmanCode As String
    Public Property SalesmanName As String
    Public Property VehicleList As IEnumerable(Of SelectListItem) 'get value เมื่อยังมี่มีการ login


    Public Property DocNumber As String 'เลขที่รันนิ่ง TimeIn ของวันนั้นๆ 
    Public Property VehicleNo As String 'เลขทะเบียน 

    Public Property OdometerStart As String 'เลขไมล์เริ่มต้น 
    Public Property TimeInDate As String 'วันที่ TimeIn 
    Public Property TimeInTime As String 'เวลา TimeIn 


    Public Property TimeOutDate As String 'วันที่ TimeOut
    Public Property TimeOutTime As String 'เวลา TimeIn 
    Public Property OdometerEnd As String 'เลขไมล์หลังใช้


    Public Property ActivityItems As List(Of ActivityItem)
End Class