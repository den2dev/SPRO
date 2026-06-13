Imports System.Data.SqlClient
Imports System.Drawing
Imports Dapper

Public Class DailyWorkRepository
    Public Function GetTodayTimIn(SalesmanCode As String) As DailyWorkViewModel

        Dim vechicle As New VehicleRepository
        Dim model As New DailyWorkViewModel

#Region "SQL"
        'Using cn As SqlConnection = DBConnection.GetConnection()

        '    Dim sql As String =
        '        "
        '        SELECT TOP 1 *
        '        FROM CheckInLog
        '        WHERE EmployeeCode = @EmployeeCode
        '        AND CAST(CheckInDateTime AS DATE)=CAST(GETDATE() AS DATE)
        '        "

        '    Return cn.QueryFirstOrDefault(Of CheckInLog)(
        '        sql,
        '        New With {
        '            .EmployeeCode = employeeCode
        '        })

        'End Using
#End Region

#Region "Mock"
        If SalesmanCode = "" Or IsDBNull(SalesmanCode) Then
            With model
                .IsTimeIn = False
                .VehicleList = vechicle.GetVehicleList()
            End With
        Else

            With model
                .IsTimeIn = True

                .SalesmanName = "นายพนักงาน ขายโซนA"
                .SalesmanCode = SalesmanCode


                .TimeInDate = "11/06/2026"
                .TimeInTime = "09:05"

                .DocNumber = "26DW1904250001"
                .VehicleNo = "9กส 8543 กทม"
                .OdometerStart = "85043"

                If SalesmanCode = "TimeOut" Then
                    .TimeOutDate = "11/06/2026"
                    .TimeOutTime = "18.12"
                    .OdometerEnd = "85154"
                Else
                    .TimeOutDate = ""
                    .TimeOutTime = ""
                    .OdometerEnd = ""
                End If

            End With


            'get list
            model.WorkItems = New List(Of WorkItem) From {
                    New WorkItem With {
                        .ActivityCode = "IA1",
                        .ActivityName = "เอกสารคำขอ",
                        .ActivityNumber = "26IA1904250001",
                        .ContactCode = "19900006",
                        .ContactName = "อโณทัย ชาวไร่",
                        .TypeContactCode = "F01",
                        .TypeContactName = "ชาวไร่เดิม",
                        .CheckInDateTime = "03/05/2026 08:20",
                        .CheckOutDateTime = "03/05/2026 08:40"
                    },
                    New WorkItem With {
                        .ActivityCode = "IA2",
                        .ActivityName = "เยี่ยมลูกค้า",
                        .ActivityNumber = "26IA1904250002",
                        .ContactCode = "19900015",
                        .ContactName = "สมชาย ใจดี",
                        .TypeContactCode = "F02",
                        .TypeContactName = "ชาวไร่ใหม่",
                        .CheckInDateTime = "03/05/2026 09:10",
                        .CheckOutDateTime = "03/05/2026 09:35"
                    },
                    New WorkItem With {
                        .ActivityCode = "IA3",
                        .ActivityName = "ติดตามผลผลิต",
                        .ActivityNumber = "26IA1904250003",
                        .ContactCode = "19900027",
                        .ContactName = "วิชัย เกษตรกร",
                        .TypeContactCode = "F01",
                        .TypeContactName = "ชาวไร่เดิม",
                        .CheckInDateTime = "03/05/2026 10:00",
                        .CheckOutDateTime = "03/05/2026 10:45"
                    },
                    New WorkItem With {
                        .ActivityCode = "IA4",
                        .ActivityName = "สำรวจพื้นที่ปลูก",
                        .ActivityNumber = "26IA1904250004",
                        .ContactCode = "19900038",
                        .ContactName = "กิตติศักดิ์ ชาวไร่",
                        .TypeContactCode = "F03",
                        .TypeContactName = "ผู้สนใจปลูกอ้อย",
                        .CheckInDateTime = "03/05/2026 13:15",
                        .CheckOutDateTime = "CheckOut"
                    }
                }
        End If
#End Region


        Return model

    End Function


    Public Function TimeIn(
        model As TimeInViewModel,
        photoPath As String) As Long

        Dim sql As String = "

INSERT INTO SalesCheckIn
(
    UserID,
    UserName,
    WorkDate,
    CheckInDateTime,
    VehicleCode,
    OdometerStart,
    OdometerPhoto
)
VALUES
(
    @UserID,
    @UserName,
    @WorkDate,
    GETDATE(),
    @VehicleCode,
    @OdometerStart,
    @OdometerPhoto
)

SELECT CAST(SCOPE_IDENTITY() AS BIGINT)

"

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Dim cmd As New SqlCommand(sql, cn)

            'cmd.Parameters.AddWithValue("@UserID", model.SalesmanCode)
            'cmd.Parameters.AddWithValue("@UserName", model.SalesmanName)
            'cmd.Parameters.AddWithValue("@WorkDate", model.WorkDate)
            cmd.Parameters.AddWithValue("@VehicleCode", model.VehicleNo)
            cmd.Parameters.AddWithValue("@OdometerStart", model.OdometerStart)
            cmd.Parameters.AddWithValue("@OdometerPhoto", photoPath)

            Return CLng(cmd.ExecuteScalar())

        End Using

    End Function
    Public Function TimeOut(
        model As TimeInViewModel,
        photoPath As String) As Long

        Dim sql As String = "

INSERT INTO SalesCheckIn
(
    UserID,
    UserName,
    WorkDate,
    CheckInDateTime,
    VehicleCode,
    OdometerStart,
    OdometerPhoto
)
VALUES
(
    @UserID,
    @UserName,
    @WorkDate,
    GETDATE(),
    @VehicleCode,
    @OdometerStart,
    @OdometerPhoto
)

SELECT CAST(SCOPE_IDENTITY() AS BIGINT)

"

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Dim cmd As New SqlCommand(sql, cn)

            'cmd.Parameters.AddWithValue("@UserID", model.SalesmanCode)
            'cmd.Parameters.AddWithValue("@UserName", model.SalesmanName)
            'cmd.Parameters.AddWithValue("@WorkDate", model.WorkDate)
            cmd.Parameters.AddWithValue("@VehicleCode", model.VehicleNo)
            cmd.Parameters.AddWithValue("@OdometerStart", model.OdometerStart)
            cmd.Parameters.AddWithValue("@OdometerPhoto", photoPath)

            Return CLng(cmd.ExecuteScalar())

        End Using

    End Function
    Public Function CheckIn(
        employeeCode As String,
        lat As String,
        lng As String,
        photoPath As String) As Long

        Using cn As SqlConnection = DBConnection.GetConnection()

            Dim sql = "
            INSERT INTO SalesCheckIn
            (
                EmployeeCode,
                CheckInDateTime,
                CheckInLat,
                CheckInLng,
                CheckInPhoto
            )
            VALUES
            (
                @EmployeeCode,
                GETDATE(),
                @Lat,
                @Lng,
                @Photo
            )

            SELECT CAST(SCOPE_IDENTITY() AS BIGINT)
            "

            Return cn.ExecuteScalar(Of Long)(
                sql,
                New With {
                    .EmployeeCode = employeeCode,
                    .Lat = lat,
                    .Lng = lng,
                    .Photo = photoPath
                })

        End Using

    End Function

    Public Sub CheckOut(
        id As Long,
        lat As String,
        lng As String,
        photoPath As String)

        Using cn As SqlConnection = DBConnection.GetConnection()

            Dim sql = "
            UPDATE SalesCheckIn
            SET
                CheckOutDateTime = GETDATE(),
                CheckOutLat = @Lat,
                CheckOutLng = @Lng,
                CheckOutPhoto = @Photo
            WHERE ID = @ID
            "

            cn.Execute(
                sql,
                New With {
                    .ID = id,
                    .Lat = lat,
                    .Lng = lng,
                    .Photo = photoPath
                })

        End Using

    End Sub

End Class