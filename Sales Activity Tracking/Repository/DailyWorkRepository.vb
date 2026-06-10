Imports System.Data.SqlClient
Imports System.Drawing
Imports Dapper

Public Class DailyWorkRepository
    Public Function GetTodayCheckIn(SalesmanCode As String) As Boolean ' CheckInLog

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

        If SalesmanCode = "" Then
            Return False
        Else
            Return True
        End If


    End Function

    Public Function GetVehicleList() As IEnumerable(Of SelectListItem)

        '    Dim sql As String = "
        '    SELECT VehicleCode,
        '           VehicleLicenseNo
        '    FROM Vehicle
        '    WHERE IsActive = 1
        '    ORDER BY VehicleLicenseNo
        '"

        '    Using cn As SqlConnection = DBConnection.GetConnection()

        '        Using da As New SqlDataAdapter(sql, cn)

        '            Dim dt As New DataTable()

        '            da.Fill(dt)

        '            Return dt

        '        End Using

        '    End Using


        Dim dt As New DataTable()

        dt.Columns.Add("VehiclePlateNo")
        dt.Columns.Add("OdometerStart")

        dt.Rows.Add("สส 1234 กทม", "39463")
        dt.Rows.Add("สว 5678 กทม", "40158")
        dt.Rows.Add("สก 9012 กทม", "10522")



        Dim VehicleList As IEnumerable(Of SelectListItem)
        VehicleList =
            dt.AsEnumerable().
            Select(Function(r) New SelectListItem With {
                .Text = r("VehiclePlateNo").ToString(),
                .Value = r("OdometerStart").ToString()
            })

        Return VehicleList

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

            cmd.Parameters.AddWithValue("@UserID", model.SalesmanCode)
            cmd.Parameters.AddWithValue("@UserName", model.SalesmanName)
            cmd.Parameters.AddWithValue("@WorkDate", model.WorkDate)
            cmd.Parameters.AddWithValue("@VehicleCode", model.VehicleLicensePlate)
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