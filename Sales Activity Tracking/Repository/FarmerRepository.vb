Imports System.Data.SqlClient
Imports System.Reflection

Public Class FarmerRepository
    Public Function GetFarmerList(FSMCODE As String) As List(Of Farmer)

#Region "SQL"

        Dim FarmerList As New List(Of Farmer)

        'Dim sql As String = "SELECT 
        '                        'VIS' as FCODE,
        '                     FIANO as FCONTCODE,
        '                     FCONTNAME as FCONTENM,
        '                     FCONTPERSTEL as FMOBILE,
        '                     FCONTADDR as FHADDNO, 
        '                        '' as FHADDMOO,
        '                        '' as FHADDVILL,
        '                        '' as FHPROVCD,
        '                        '' as FHCITYCD,
        '                        '' as FHDISTRICTCD,
        '                        '' as  ProvinceName,
        '                        '' as  CityName,
        '                        '' as  DistrictName 
        '                    FROM OD50CIAC 
        '                    where FIACODE='VIS'                             
        '                    AND (FCONTCODE is null or FCONTCODE='') 
        '                    AND FSMCODE=@FSMCODE
        '                    AND FACTDATE=CONVERT(DATE, GETDATE())

        '                    Union all 

        '                    SELECT
        '                        'OD50' as FCODE,
        '                        R.FCONTCODE,
        '                        R.FCONTENM ,
        '                        R.FMOBILE,
        '                        R.FHADDNO,
        '                        R.FHADDMOO,
        '                        R.FHADDVILL,
        '                        R.FHPROVCD,
        '                        R.FHCITYCD,
        '                        R.FHDISTRICTCD,
        '                        A.FPROVINCE  AS ProvinceName,
        '                        A.FCITY      AS CityName,
        '                        A.FDISTRICT  AS DistrictName
        '                    FROM OD50RCVD R
        '                    LEFT JOIN LD07AZIP A
        '                        ON A.FPROVCD = R.FHPROVCD
        '                       AND A.FCITYCD = R.FHCITYCD
        '                       AND A.FDISTRICTCD = R.FHDISTRICTCD
        '                    where (R.FNOTUSE is null or R.FNOTUSE='N') 
        '                    and  R.FSMCODE=@FSMCODE
        '                  "

        Dim sql As String = "  SELECT 
                                R.FCONTCODE,
                                R.FCONTENM ,
                                R.FMOBILE,
                                R.FHADDNO,
                                R.FHADDMOO,
                                R.FHADDVILL,
                                R.FHPROVCD,
                                R.FHCITYCD,
                                R.FHDISTRICTCD,
                                A.FPROVINCE  AS ProvinceName,
                                A.FCITY      AS CityName,
                                A.FDISTRICT  AS DistrictName
                            FROM OD50RCVD R
                            LEFT JOIN LD07AZIP A
                                ON A.FPROVCD = R.FHPROVCD
                               AND A.FCITYCD = R.FHCITYCD
                               AND A.FDISTRICTCD = R.FHDISTRICTCD
                            where (R.FNOTUSE is null or R.FNOTUSE='N') 
                            and  R.FSMCODE=@FSMCODE
                          "

        'Debug.WriteLine("[" & FSMCODE & "]")

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                cmd.Parameters.AddWithValue("@FSMCODE", FSMCODE)

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()

                        FarmerList.Add(
                            New Farmer With {
                                .IsNewFarmer = False, 'If(dr("FCODE").ToString() = "VIS", True, False),
                                .FarmerCode = dr("FCONTCODE").ToString(),
                                .FarmerName = dr("FCONTENM").ToString(),
                                .MobileNo = dr("FMOBILE").ToString(),
                                .AddressNo = dr("FHADDNO").ToString(),
                                .Moo = dr("FHADDMOO").ToString(),
                                .VillageName = dr("FHADDVILL").ToString(),
                                .SubDistrict = dr("DistrictName").ToString(),
                                .District = dr("CityName").ToString(),
                                .Province = dr("ProvinceName").ToString(),
                                .ContractNo = ""
                            })

                    End While

                End Using


            End Using

        End Using

        Return FarmerList
#End Region


#Region "Mock data"
        'Return New List(Of Farmer) From {
        '       New Farmer With {.FarmerCode = "F0001", .FarmerName = "สมชาย ใจดี", .MobileNo = "0812345678"},
        '       New Farmer With {.FarmerCode = "F0002", .FarmerName = "สมศรี รุ่งเรือง", .MobileNo = "0823456789"},
        '       New Farmer With {.FarmerCode = "F0003", .FarmerName = "วิชัย ทองสุข", .MobileNo = "0834567890"},
        '       New Farmer With {.FarmerCode = "F0004", .FarmerName = "สุภาพร บุญมี", .MobileNo = "0845678901"},
        '       New Farmer With {.FarmerCode = "F0005", .FarmerName = "ประสิทธิ์ แก้วดี", .MobileNo = "0856789012"},
        '       New Farmer With {.FarmerCode = "F0006", .FarmerName = "อรทัย ศรีสุข", .MobileNo = "0867890123"},
        '       New Farmer With {.FarmerCode = "F0007", .FarmerName = "ธนพล แสนดี", .MobileNo = "0878901234"},
        '       New Farmer With {.FarmerCode = "F0008", .FarmerName = "มาลี พูนทรัพย์", .MobileNo = "0889012345"},
        '       New Farmer With {.FarmerCode = "F0009", .FarmerName = "กิตติชัย เจริญผล", .MobileNo = "0890123456"},
        '       New Farmer With {.FarmerCode = "F0010", .FarmerName = "สุดารัตน์ ทองคำ", .MobileNo = "0801234567"}
        '   }
#End Region

    End Function

    'Public Function SearchFarmer(keyword As String) As IEnumerable(Of FarmerModel)

    'End Function

    '--ถ้าเป็น new farmer ให้ส่ง FIANO มา.
    Public Function GetFarmer(isnewfarmer As Boolean, farmercode As String) As Farmer

        Dim sql As String

        If isnewfarmer = True Then
            sql = " SELECT  
	                FIANO as FCONTCODE,
	                FCONTNAME as FCONTENM,
	                FCONTPERSTEL as FMOBILE,
	                FCONTADDR as FHADDNO, 
                    '' as FHADDMOO,
                    '' as FHADDVILL,
                    '' as FHPROVCD,
                    '' as FHCITYCD,
                    '' as FHDISTRICTCD,
                    '' as  ProvinceName,
                    '' as  CityName,
                    '' as  DistrictName 
                FROM OD50CIAC 
                where FIACODE='VIS'  
                AND FIANO=@FCONTCODE
                "
        Else
            sql = " SELECT
                R.FCONTCODE,
                R.FCONTENM ,
                R.FMOBILE,
                R.FHADDNO,
                R.FHADDMOO,
                R.FHADDVILL,
                R.FHPROVCD,
                R.FHCITYCD,
                R.FHDISTRICTCD,
                A.FPROVINCE  AS ProvinceName,
                A.FCITY      AS CityName,
                A.FDISTRICT  AS DistrictName
            FROM OD50RCVD R
            LEFT JOIN LD07AZIP A
                ON A.FPROVCD = R.FHPROVCD
                AND A.FCITYCD = R.FHCITYCD
                AND A.FDISTRICTCD = R.FHDISTRICTCD
            where FCONTCODE=@FCONTCODE 
            "
        End If



        Dim _Farmer As New Farmer

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                cmd.Parameters.AddWithValue("@FCONTCODE", farmercode)

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then 'login แล้ว

                        With _Farmer
                            .IsNewFarmer = isnewfarmer
                            .FarmerCode = dr("FCONTCODE").ToString()
                            .FarmerName = dr("FCONTENM").ToString()
                            .MobileNo = dr("FMOBILE").ToString()
                            .AddressNo = dr("FHADDNO").ToString()
                            .Moo = dr("FHADDMOO").ToString()
                            .VillageName = dr("FHADDVILL").ToString()
                            .SubDistrict = dr("DistrictName").ToString()
                            .District = dr("CityName").ToString()
                            .Province = dr("ProvinceName").ToString()
                            .ContractNo = ""
                        End With


                    End If


                End Using


            End Using

        End Using

        Return _Farmer

#Region "Mock"
        'Return New Farmer With {
        '    .FarmerCode = farmercode,
        '    .FarmerName = "สมชาย ใจดี",
        '    .MobileNo = "0812345678",
        '    .AddressNo = "99/1",
        '    .Moo = "5",
        '    .SubDistrict = "บ้านใหม่",
        '    .District = "เมือง",
        '    .Province = "ขอนแก่น",
        '    .ContractNo = "C20250001"
        '}
#End Region


    End Function



End Class
