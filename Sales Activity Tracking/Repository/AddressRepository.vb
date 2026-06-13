Imports System.Reflection

Public Class AddressRepository

    Public Function GetProvinceList() As IEnumerable(Of SelectListItem)

        Dim ProvinceList As IEnumerable(Of SelectListItem)

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
        ProvinceList = New List(Of SelectListItem) From {
                            New SelectListItem With {.Value = "10", .Text = "กรุงเทพมหานคร"},
                            New SelectListItem With {.Value = "20", .Text = "ชลบุรี"},
                            New SelectListItem With {.Value = "30", .Text = "นครราชสีมา"},
                            New SelectListItem With {.Value = "33", .Text = "ศรีสะเกษ"},
                            New SelectListItem With {.Value = "34", .Text = "อุบลราชธานี"},
                            New SelectListItem With {.Value = "35", .Text = "ยโสธร"},
                            New SelectListItem With {.Value = "36", .Text = "ชัยภูมิ"},
                            New SelectListItem With {.Value = "37", .Text = "อำนาจเจริญ"},
                            New SelectListItem With {.Value = "39", .Text = "หนองบัวลำภู"},
                            New SelectListItem With {.Value = "40", .Text = "ขอนแก่น"},
                            New SelectListItem With {.Value = "41", .Text = "อุดรธานี"},
                            New SelectListItem With {.Value = "42", .Text = "เลย"},
                            New SelectListItem With {.Value = "43", .Text = "หนองคาย"},
                            New SelectListItem With {.Value = "44", .Text = "มหาสารคาม"},
                            New SelectListItem With {.Value = "45", .Text = "ร้อยเอ็ด"},
                            New SelectListItem With {.Value = "46", .Text = "กาฬสินธุ์"},
                            New SelectListItem With {.Value = "47", .Text = "สกลนคร"},
                            New SelectListItem With {.Value = "48", .Text = "นครพนม"},
                            New SelectListItem With {.Value = "49", .Text = "มุกดาหาร"},
                            New SelectListItem With {.Value = "30", .Text = "นครราชสีมา"},
                            New SelectListItem With {.Value = "38", .Text = "บึงกาฬ"}
                        }
#End Region


        Return ProvinceList

    End Function

    Public Function GetDistrictList(provinceCode As String) As IEnumerable(Of SelectListItem)

#Region "Mock"
        Select Case provinceCode

            Case "10" ' กรุงเทพมหานคร

                Return New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "1001", .Text = "เขตบางเขน"},
                New SelectListItem With {.Value = "1002", .Text = "เขตลาดพร้าว"},
                New SelectListItem With {.Value = "1003", .Text = "เขตจตุจักร"}
            }

            Case "20" ' ชลบุรี

                Return New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "2001", .Text = "เมืองชลบุรี"},
                New SelectListItem With {.Value = "2002", .Text = "บ้านบึง"},
                New SelectListItem With {.Value = "2003", .Text = "พนัสนิคม"}
            }

            Case "30" ' นครราชสีมา

                Return New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "3001", .Text = "เมืองนครราชสีมา"},
                New SelectListItem With {.Value = "3002", .Text = "ปากช่อง"},
                New SelectListItem With {.Value = "3003", .Text = "สีคิ้ว"}
            }

            Case Else

                Return New List(Of SelectListItem)

        End Select
#End Region


    End Function

    Public Function GetSubDistrictList(districtCode As String) As IEnumerable(Of SelectListItem)

#Region "Mock"
        Select Case districtCode

            Case "1001" ' เขตบางเขน

                Return New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "100101", .Text = "อนุสาวรีย์"},
                New SelectListItem With {.Value = "100102", .Text = "ท่าแร้ง"}
            }

            Case "2001" ' เมืองชลบุรี

                Return New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "200101", .Text = "บางปลาสร้อย"},
                New SelectListItem With {.Value = "200102", .Text = "มะขามหย่ง"}
            }

            Case "3001" ' เมืองนครราชสีมา

                Return New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "300101", .Text = "ในเมือง"},
                New SelectListItem With {.Value = "300102", .Text = "โพธิ์กลาง"}
            }

            Case Else

                Return New List(Of SelectListItem)

        End Select
#End Region


    End Function

End Class
