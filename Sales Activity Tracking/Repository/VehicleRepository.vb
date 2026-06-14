Imports System.Data.SqlClient
Imports System.Reflection

Public Class VehicleRepository

    Public Function GetVehicleList() As List(Of SelectListItem)

        Dim VehicleList As New List(Of SelectListItem)

#Region "SQL"

        Dim sql As String = "SELECT FMACHCODE,FNOTUSE FROM KD01MACH WHERE (FNOTUSE is null or FNOTUSE='N') Order by FMACHCODE"

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()

                        VehicleList.Add(
                            New SelectListItem With {
                                .Text = dr("FMACHCODE").ToString(),
                                .Value = dr("FMACHCODE").ToString()
                            })

                    End While

                End Using


            End Using

        End Using
#End Region


#Region "Mock"
        'Dim dt As New DataTable()

        'dt.Columns.Add("VehiclePlateNo")
        'dt.Columns.Add("OdometerStart")

        'dt.Rows.Add("สส 1234 กทม", "39463")
        'dt.Rows.Add("สว 5678 กทม", "40158")
        'dt.Rows.Add("สก 9012 กทม", "10522")

        'VehicleList =
        '    dt.AsEnumerable().
        '    Select(Function(r) New SelectListItem With {
        '        .Text = r("VehiclePlateNo").ToString(),
        '        .Value = r("OdometerStart").ToString()
        '    })
#End Region


        Return VehicleList

    End Function

End Class
