Public Class VehicleRepository

    Public Function GetVehicleList() As IEnumerable(Of SelectListItem)

        Dim VehicleList As IEnumerable(Of SelectListItem)

#Region "SQL"
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
#End Region


#Region "Mock"
        Dim dt As New DataTable()

        dt.Columns.Add("VehiclePlateNo")
        dt.Columns.Add("OdometerStart")

        dt.Rows.Add("สส 1234 กทม", "39463")
        dt.Rows.Add("สว 5678 กทม", "40158")
        dt.Rows.Add("สก 9012 กทม", "10522")

        VehicleList =
            dt.AsEnumerable().
            Select(Function(r) New SelectListItem With {
                .Text = r("VehiclePlateNo").ToString(),
                .Value = r("OdometerStart").ToString()
            })
#End Region


        Return VehicleList

    End Function

    'Public Function GetVehicleById(vehicleId As Integer) As VehicleModel

    'End Function

End Class
