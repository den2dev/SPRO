Imports System.Data.SqlClient
Imports System.Reflection

Public Class AddressRepository

    Public Function GetProvinceList() As List(Of SelectListItem)


#Region "SQL"

        Dim ProvinceList As New List(Of SelectListItem)

        Dim sql As String = "select distinct FPROVCD,FPROVINCE from LD07AZIP order by FPROVCD "

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()

                        ProvinceList.Add(
                            New SelectListItem With {
                                .Text = dr("FPROVINCE").ToString(),
                                .Value = dr("FPROVCD").ToString()
                            })

                    End While

                End Using


            End Using

        End Using
#End Region


#Region "Mock"
        'ProvinceList = New List(Of SelectListItem) From {
        '                    New SelectListItem With {.Value = "10", .Text = "กรุงเทพมหานคร"},
        '                    New SelectListItem With {.Value = "20", .Text = "ชลบุรี"},
        '                    New SelectListItem With {.Value = "30", .Text = "นครราชสีมา"},
        '                    New SelectListItem With {.Value = "33", .Text = "ศรีสะเกษ"},
        '                    New SelectListItem With {.Value = "34", .Text = "อุบลราชธานี"},
        '                    New SelectListItem With {.Value = "35", .Text = "ยโสธร"},
        '                    New SelectListItem With {.Value = "36", .Text = "ชัยภูมิ"},
        '                    New SelectListItem With {.Value = "37", .Text = "อำนาจเจริญ"},
        '                    New SelectListItem With {.Value = "39", .Text = "หนองบัวลำภู"},
        '                    New SelectListItem With {.Value = "40", .Text = "ขอนแก่น"},
        '                    New SelectListItem With {.Value = "41", .Text = "อุดรธานี"},
        '                    New SelectListItem With {.Value = "42", .Text = "เลย"},
        '                    New SelectListItem With {.Value = "43", .Text = "หนองคาย"},
        '                    New SelectListItem With {.Value = "44", .Text = "มหาสารคาม"},
        '                    New SelectListItem With {.Value = "45", .Text = "ร้อยเอ็ด"},
        '                    New SelectListItem With {.Value = "46", .Text = "กาฬสินธุ์"},
        '                    New SelectListItem With {.Value = "47", .Text = "สกลนคร"},
        '                    New SelectListItem With {.Value = "48", .Text = "นครพนม"},
        '                    New SelectListItem With {.Value = "49", .Text = "มุกดาหาร"},
        '                    New SelectListItem With {.Value = "30", .Text = "นครราชสีมา"},
        '                    New SelectListItem With {.Value = "38", .Text = "บึงกาฬ"}
        '                }
#End Region


        Return ProvinceList

    End Function

    Public Function GetDistrictList(provinceCode As String) As List(Of SelectListItem)

#Region "SQL"

        Dim DistrictList As New List(Of SelectListItem)

        Dim sql As String = "select distinct FCITYCD,FCITY from LD07AZIP  where FPROVCD=@FPROVCD order by FCITYCD"

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                cmd.Parameters.AddWithValue("@FPROVCD", provinceCode)

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()

                        DistrictList.Add(
                            New SelectListItem With {
                                .Text = dr("FCITY").ToString(),
                                .Value = dr("FCITYCD").ToString()
                            })

                    End While

                End Using


            End Using

        End Using

        Return DistrictList
#End Region


#Region "Mock"
        'Select Case provinceCode

        '    Case "10" ' กรุงเทพมหานคร

        '        Return New List(Of SelectListItem) From {
        '        New SelectListItem With {.Value = "1001", .Text = "เขตบางเขน"},
        '        New SelectListItem With {.Value = "1002", .Text = "เขตลาดพร้าว"},
        '        New SelectListItem With {.Value = "1003", .Text = "เขตจตุจักร"}
        '    }

        '    Case "20" ' ชลบุรี

        '        Return New List(Of SelectListItem) From {
        '        New SelectListItem With {.Value = "2001", .Text = "เมืองชลบุรี"},
        '        New SelectListItem With {.Value = "2002", .Text = "บ้านบึง"},
        '        New SelectListItem With {.Value = "2003", .Text = "พนัสนิคม"}
        '    }

        '    Case "30" ' นครราชสีมา

        '        Return New List(Of SelectListItem) From {
        '        New SelectListItem With {.Value = "3001", .Text = "เมืองนครราชสีมา"},
        '        New SelectListItem With {.Value = "3002", .Text = "ปากช่อง"},
        '        New SelectListItem With {.Value = "3003", .Text = "สีคิ้ว"}
        '    }

        '    Case Else

        '        Return New List(Of SelectListItem)

        'End Select
#End Region



    End Function

    Public Function GetSubDistrictList(provinceCode As String, districtCode As String) As List(Of SelectListItem)

#Region "SQL"

        Dim DistrictList As New List(Of SelectListItem)

        Dim sql As String = "select distinct FDISTRICTCD,FDISTRICT from LD07AZIP where FPROVCD=@FPROVCD and FCITYCD=@FCITYCD  order by FDISTRICTCD"

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                cmd.Parameters.AddWithValue("@FPROVCD", provinceCode)
                cmd.Parameters.AddWithValue("@FCITYCD", districtCode)

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()

                        DistrictList.Add(
                            New SelectListItem With {
                                .Text = dr("FDISTRICT").ToString(),
                                .Value = dr("FDISTRICTCD").ToString()
                            })

                    End While

                End Using


            End Using

        End Using

        Return DistrictList
#End Region

#Region "Mock"
        'Select Case districtCode

        '    Case "1001" ' เขตบางเขน

        '        Return New List(Of SelectListItem) From {
        '        New SelectListItem With {.Value = "100101", .Text = "อนุสาวรีย์"},
        '        New SelectListItem With {.Value = "100102", .Text = "ท่าแร้ง"}
        '    }

        '    Case "2001" ' เมืองชลบุรี

        '        Return New List(Of SelectListItem) From {
        '        New SelectListItem With {.Value = "200101", .Text = "บางปลาสร้อย"},
        '        New SelectListItem With {.Value = "200102", .Text = "มะขามหย่ง"}
        '    }

        '    Case "3001" ' เมืองนครราชสีมา

        '        Return New List(Of SelectListItem) From {
        '        New SelectListItem With {.Value = "300101", .Text = "ในเมือง"},
        '        New SelectListItem With {.Value = "300102", .Text = "โพธิ์กลาง"}
        '    }

        '    Case Else

        '        Return New List(Of SelectListItem)

        'End Select
#End Region


    End Function

End Class
