Imports System.Data.SqlClient
Imports System.Drawing
Imports Dapper
Imports Microsoft.VisualBasic.ApplicationServices

Public Class DailyWorkRepository
    Public Function GetTodayTimIn(SalesmanCode As String) As DailyWorkViewModel

        Dim vechicle As New VehicleRepository
        Dim model As New DailyWorkViewModel

#Region "SQL"

        Dim sql As String = "
            SELECT TOP 1 
                FIANO,
                FESTRDATE,
                FESTRTIME,
                FACTFNDATE,
                FACTFNTIME,
                FVEHICLENO,
                FMETERSTR,
                LD01SMAN.FSMNAME
            FROM OD50CIAC LEFT OUTER JOIN LD01SMAN ON LD01SMAN.FSMCODE = OD50CIAC.FSMCODE
            WHERE OD50CIAC.FSMCODE = @FSMCODE
            AND FIACODE = @FIACODE
            AND FACTDATE = @FACTDATE"

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                cmd.Parameters.AddWithValue("@FSMCODE", SalesmanCode)
                cmd.Parameters.AddWithValue("@FIACODE", "TIO")
                cmd.Parameters.AddWithValue("@FACTDATE", Date.Today)

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    If dr.Read() Then 'login แล้ว

                        With model

                            .UserID = "ZZY" '--default gsUserId = "ZZY"

                            .SalesmanCode = SalesmanCode
                            .SalesmanName = dr("FSMNAME").ToString()

                            .DocNumber = dr("FIANO").ToString()

                            .TimeInDate = If(IsDBNull(dr("FESTRDATE")), "", Convert.ToDateTime(dr("FESTRDATE")).ToString("dd/MM/yyyy"))
                            .TimeInTime = If(IsDBNull(dr("FESTRTIME")), "", dr("FESTRTIME").ToString() & " น.")

                            .TimeOutDate = If(IsDBNull(dr("FACTFNDATE")), "", Convert.ToDateTime(dr("FACTFNDATE")).ToString("dd/MM/yyyy"))
                            .TimeOutTime = If(IsDBNull(dr("FACTFNTIME")), "", dr("FACTFNTIME").ToString() & " น.")

                            .IsTimeIn = Not String.IsNullOrEmpty(.TimeInDate)
                            .IsTimeOut = Not String.IsNullOrEmpty(.TimeOutDate)

                            .VehicleNo = dr("FVEHICLENO").ToString()
                            .OdometerStart = dr("FMETERSTR").ToString()
                        End With

                    Else 'ยังไม่ login

                        With model
                            .UserID = "ZZY" '--default gsUserId = "ZZY"
                            .SalesmanCode = SalesmanCode
                            .IsTimeIn = False
                            .VehicleList = vechicle.GetVehicleList()
                        End With

                    End If

                End Using

            End Using

        End Using
#End Region

#Region "Mock"
        'If SalesmanCode = "" Or IsDBNull(SalesmanCode) Then
        '    With model
        '        .IsTimeIn = False
        '        .VehicleList = vechicle.GetVehicleList()
        '    End With
        'Else

        '    With model
        '        .IsTimeIn = True

        '        .SalesmanName = "นายพนักงาน ขายโซนA"
        '        .SalesmanCode = SalesmanCode


        '        .TimeInDate = "11/06/2026"
        '        .TimeInTime = "09:05"

        '        .DocNumber = "26DW1904250001"
        '        .VehicleNo = "9กส 8543 กทม"
        '        .OdometerStart = "85043"

        '        If SalesmanCode = "TimeOut" Then
        '            .TimeOutDate = "11/06/2026"
        '            .TimeOutTime = "18.12"
        '            .OdometerEnd = "85154"
        '        Else
        '            .TimeOutDate = ""
        '            .TimeOutTime = ""
        '            .OdometerEnd = ""
        '        End If

        '    End With


        '    'get list
        '    model.WorkItems = New List(Of WorkItem) From {
        '            New WorkItem With {
        '                .ActivityCode = "IA1",
        '                .ActivityName = "เอกสารคำขอ",
        '                .ActivityNumber = "26IA1904250001",
        '                .ContactCode = "19900006",
        '                .ContactName = "อโณทัย ชาวไร่",
        '                .TypeContactCode = "F01",
        '                .TypeContactName = "ชาวไร่เดิม",
        '                .CheckInDateTime = "03/05/2026 08:20",
        '                .CheckOutDateTime = "03/05/2026 08:40"
        '            },
        '            New WorkItem With {
        '                .ActivityCode = "IA2",
        '                .ActivityName = "เยี่ยมลูกค้า",
        '                .ActivityNumber = "26IA1904250002",
        '                .ContactCode = "19900015",
        '                .ContactName = "สมชาย ใจดี",
        '                .TypeContactCode = "F02",
        '                .TypeContactName = "ชาวไร่ใหม่",
        '                .CheckInDateTime = "03/05/2026 09:10",
        '                .CheckOutDateTime = "03/05/2026 09:35"
        '            },
        '            New WorkItem With {
        '                .ActivityCode = "IA3",
        '                .ActivityName = "ติดตามผลผลิต",
        '                .ActivityNumber = "26IA1904250003",
        '                .ContactCode = "19900027",
        '                .ContactName = "วิชัย เกษตรกร",
        '                .TypeContactCode = "F01",
        '                .TypeContactName = "ชาวไร่เดิม",
        '                .CheckInDateTime = "03/05/2026 10:00",
        '                .CheckOutDateTime = "03/05/2026 10:45"
        '            },
        '            New WorkItem With {
        '                .ActivityCode = "IA4",
        '                .ActivityName = "สำรวจพื้นที่ปลูก",
        '                .ActivityNumber = "26IA1904250004",
        '                .ContactCode = "19900038",
        '                .ContactName = "กิตติศักดิ์ ชาวไร่",
        '                .TypeContactCode = "F03",
        '                .TypeContactName = "ผู้สนใจปลูกอ้อย",
        '                .CheckInDateTime = "03/05/2026 13:15",
        '                .CheckOutDateTime = "CheckOut"
        '            }
        '        }
        'End If
#End Region


        Return model

    End Function


    Public Function TimeIn(model As TimeInViewModel) As ResultMessageModel

        'Running Number  : TIONOK260613032344 
        '{DocCode}{UserID}{YY}{MM}{DD}{HH}{MM}{SS}

        Dim FIACODE = "TIO"
        Dim FIANO = FIACODE & model.UserID & DateTime.Now.ToString("yyMMddHHmmss")

        '-------------------------------
        Dim sql As String = "
        INSERT INTO OD50CIAC
        (
            FCONTCODE,
            FCONTPERSON,
            FSMCODE,
            FIACODE,
            FIANO,
 
            FACTDATE,
            FACTTIME,
            FACTBY,
            FACTRESULT,
            FACTREMARK, 

            FENTDATE,
            FENTTIME,
            FENTBY,
            FEPREDATE,
            FOPPNO,

            FFLUPYN,
            FFLUPACT,
            FFLUPREM,

            FFROMIANO,
            FACTOUTSIDEYN, 

            FACTLOCATION,

            FLASTUPD,
            FLASTUPDTM,
            FLASTUPDBY,
 
            FESTRDATE,
            FESTRTIME,
 
            FACTNEED,
            FACTAMOUNT, 

            FVEHICLENO,
            FVEHICLETY,
            FMETERSTR 
        )
        VALUES
        (
            @FCONTCODE,
            @FCONTPERSON,
            @FSMCODE,
            @FIACODE,
            @FIANO,
 
            @FACTDATE,
            @FACTTIME,
            @FACTBY,
            @FACTRESULT,
            @FACTREMARK, 

            @FENTDATE,
            @FENTTIME,
            @FENTBY,
            @FEPREDATE,
            @FOPPNO,

            @FFLUPYN,
            @FFLUPACT,
            @FFLUPREM,

            @FFROMIANO,
            @FACTOUTSIDEYN, 

            @FACTLOCATION,

            @FLASTUPD,
            @FLASTUPDTM,
            @FLASTUPDBY,
 
            @FESTRDATE,
            @FESTRTIME,
 
            @FACTNEED,
            @FACTAMOUNT, 

            @FVEHICLENO,
            @FVEHICLETY,
            @FMETERSTR 
 
        )"

        Dim result As New ResultMessageModel

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Dim cmd As New SqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@FCONTCODE", "")
            cmd.Parameters.AddWithValue("@FCONTPERSON", "")
            cmd.Parameters.AddWithValue("@FSMCODE", model.SalesmanCode)
            cmd.Parameters.AddWithValue("@FIACODE", FIACODE)
            cmd.Parameters.AddWithValue("@FIANO", FIANO)

            cmd.Parameters.AddWithValue("@FACTDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FACTTIME", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FACTBY", model.UserID)
            cmd.Parameters.AddWithValue("@FACTRESULT", "Y") 'default Y
            cmd.Parameters.AddWithValue("@FACTREMARK", "")

            cmd.Parameters.AddWithValue("@FENTDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FENTTIME", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FENTBY", model.UserID)
            cmd.Parameters.AddWithValue("@FEPREDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FOPPNO", "")

            '----Follow Up
            cmd.Parameters.AddWithValue("@FFLUPYN", "N") '--Default N ,Null ชาวไร่ใหม่
            cmd.Parameters.AddWithValue("@FFLUPACT", "")
            cmd.Parameters.AddWithValue("@FFLUPREM", "")

            cmd.Parameters.AddWithValue("@FFROMIANO", "") '--Default '',Null ชาวไร่ใหม่
            cmd.Parameters.AddWithValue("@FACTOUTSIDEYN", "Y") 'default Y นอกพื้นที่

            cmd.Parameters.AddWithValue("@FACTLOCATION", model.GeoLocation)

            cmd.Parameters.AddWithValue("@FLASTUPD", Date.Today)   '---Date.Today.ToString("yyyy-MM-dd") กรณีใน table เป็น varchar(10)
            cmd.Parameters.AddWithValue("@FLASTUPDTM", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FLASTUPDBY", model.UserID)

            cmd.Parameters.AddWithValue("@FESTRDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FESTRTIME", Date.Now.ToString("HH:mm"))

            cmd.Parameters.AddWithValue("@FACTNEED", 0)
            cmd.Parameters.AddWithValue("@FACTAMOUNT", 0)

            cmd.Parameters.AddWithValue("@FVEHICLENO", model.VehicleNo)
            cmd.Parameters.AddWithValue("@FVEHICLETY", model.VehicleType)
            cmd.Parameters.AddWithValue("@FMETERSTR", model.OdometerStart)


            For Each p As SqlParameter In cmd.Parameters
                Debug.WriteLine(p.ParameterName & "=" & p.Value)
            Next

            Try

                Dim rows As Integer = cmd.ExecuteNonQuery()

                If rows > 0 Then

                    result.IsSuccess = True
                    result.DocCode = FIACODE
                    result.DocNumber = FIANO
                    result.Message = "บันทึกข้อมูลสำเร็จ"

                Else

                    result.IsSuccess = False
                    result.DocNumber = ""
                    result.Message = "ไม่พบข้อมูลที่ถูกบันทึก"

                End If

            Catch ex As Exception

                result.IsSuccess = False
                result.DocNumber = ""
                result.Message = ex.Message

            End Try

            Return result

        End Using


    End Function
    Public Function TimeOut(model As TimeOutViewModel) As ResultMessageModel

        '-------------------------------
        Dim sql As String = "
        UPDATE OD50CIAC
                SET  
                    FLASTUPD       = @FLASTUPD,
                    FLASTUPDTM     = @FLASTUPDTM,
                    FLASTUPDBY     = @FLASTUPDBY,   

                    FACTFNDATE      = @FACTFNDATE,
                    FACTFNTIME      = @FACTFNTIME, 
                    FACTLOCATION   =  FACTLOCATION + '|' + @FACTLOCATION,

                    FMETEREND      = @FMETEREND

                WHERE FIANO = @FIANO"

        Dim result As New ResultMessageModel

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Dim cmd As New SqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@FIANO", model.DocNumber)

            cmd.Parameters.AddWithValue("@FLASTUPD", Date.Today)
            cmd.Parameters.AddWithValue("@FLASTUPDTM", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FLASTUPDBY", model.UserID)

            cmd.Parameters.AddWithValue("@FACTFNDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FACTFNTIME", Date.Now.ToString("HH:mm"))

            cmd.Parameters.AddWithValue("@FACTLOCATION", model.GeoLocation)

            cmd.Parameters.AddWithValue("@FMETEREND", model.OdometerEnd)

            Try

                Dim rows As Integer = cmd.ExecuteNonQuery()

                If rows > 0 Then

                    result.IsSuccess = True
                    result.DocNumber = model.DocNumber
                    result.Message = "บันทึกข้อมูลสำเร็จ"

                Else

                    result.IsSuccess = False
                    result.DocNumber = ""
                    result.Message = "ไม่พบข้อมูลที่ถูกบันทึก"

                End If

            Catch ex As Exception

                result.IsSuccess = False
                result.DocNumber = ""
                result.Message = ex.Message

            End Try

            Return result

        End Using

    End Function
    Public Function CheckIn(model As CheckInViewModel) As String

        'Running Number  : VISNOK260613025030
        '{DocCode}{UserID}{YY}{MM}{DD}{HH}{MM}{SS} 

        Dim FIACODE = "VIS"
        Dim FIANO = FIACODE & model.UserID & DateTime.Now.ToString("yyMMddHHmmss")


        '-------------------------------
        Dim sql As String = "
        INSERT INTO OD50CIAC
        (
            FCONTCODE,
            FCONTPERSON,
            FSMCODE,
            FIACODE,
            FIANO,
            FACTREMARK,

            FACTDATE,
            FACTTIME,
            FACTBY,

            FENTDATE,
            FENTTIME,
            FENTBY,
            FEPREDATE,

            FPREPYN,
            FAPPMYN,
            FAPPMBY,
            FAPPMDATE,

            FFLUPYN,
            FFLUPACT,

            FFROMIANO,
            FACTOUTSIDEYN,

            FCONTADDR,
            FCONTCHANNEL,
            FCONTNAME,
            FCONTPERSTEL,

            FLASTUPD,
            FLASTUPDTM,
            FLASTUPDBY,

            FACTRESULT,
            FESTRDATE,
            FESTRTIME,

            FACTNEED,
            FACTAMOUNT,

            FACTLOCATION,

            FVEHICLENO,
            FVEHICLETY,
            FMETERSTR 
        )
        VALUES
        (
            @FCONTCODE,
            @FCONTPERSON,
            @FSMCODE,
            @FIACODE,
            @FIANO,
            @FACTREMARK,

            @FACTDATE,
            @FACTTIME,
            @FACTBY,

            @FENTDATE,
            @FENTTIME,
            @FENTBY,
            @FEPREDATE,

            @FPREPYN,
            @FAPPMYN,
            @FAPPMBY,
            @FAPPMDATE,

            @FFLUPYN,
            @FFLUPACT,

            @FFROMIANO,
            @FACTOUTSIDEYN,

            @FCONTADDR,
            @FCONTCHANNEL,
            @FCONTNAME,
            @FCONTPERSTEL,

            @FLASTUPD,
            @FLASTUPDTM,
            @FLASTUPDBY,

            @FACTRESULT,
            @FESTRDATE,
            @FESTRTIME,

            @FACTNEED,
            @FACTAMOUNT,

            @FACTLOCATION,

            @FVEHICLENO,
            @FVEHICLETY,
            @FMETERSTR 
        )"

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Dim cmd As New SqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@FCONTCODE", "")
            cmd.Parameters.AddWithValue("@FCONTPERSON", "")
            cmd.Parameters.AddWithValue("@FSMCODE", model.SalesmanCode)
            cmd.Parameters.AddWithValue("@FIACODE", FIACODE)
            cmd.Parameters.AddWithValue("@FIANO", FIANO)
            cmd.Parameters.AddWithValue("@FACTREMARK", "")

            cmd.Parameters.AddWithValue("@FACTDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FACTTIME", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FACTBY", model.UserID)

            cmd.Parameters.AddWithValue("@FENTDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FENTTIME", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FENTBY", model.UserID)
            cmd.Parameters.AddWithValue("@FEPREDATE", Date.Today)

            cmd.Parameters.AddWithValue("@FPREPYN", 0) ' --ชาวไร่ใหม่=0,null

            cmd.Parameters.AddWithValue("@FAPPMYN", "Y") '--ชาวไร่ใหม่=Y,null
            cmd.Parameters.AddWithValue("@FAPPMBY", model.UserID)
            cmd.Parameters.AddWithValue("@FAPPMDATE", Date.Today)

            '----Follow Up
            cmd.Parameters.AddWithValue("@FFLUPYN", DBNull.Value) '--Default N ,Null ชาวไร่ใหม่
            cmd.Parameters.AddWithValue("@FFLUPACT", DBNull.Value) '--Default '',Null ชาวไร่ใหม่

            cmd.Parameters.AddWithValue("@FFROMIANO", DBNull.Value) '--Default '',Null ชาวไร่ใหม่
            cmd.Parameters.AddWithValue("@FACTOUTSIDEYN", "Y") 'default Y นอกพื้นที่

            cmd.Parameters.AddWithValue("@FCONTADDR", "") 'concat(address)
            cmd.Parameters.AddWithValue("@FCONTCHANNEL", 4) '-default 4-Visit
            cmd.Parameters.AddWithValue("@FCONTNAME", "ชื่อชาวไร่")
            cmd.Parameters.AddWithValue("@FCONTPERSTEL", "0812345678")

            cmd.Parameters.AddWithValue("@FLASTUPD", Date.Today)   '---Date.Today.ToString("yyyy-MM-dd") กรณีใน table เป็น varchar(10)
            cmd.Parameters.AddWithValue("@FLASTUPDTM", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FLASTUPDBY", model.UserID)

            cmd.Parameters.AddWithValue("@FACTRESULT", "Y") 'default Y

            cmd.Parameters.AddWithValue("@FESTRDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FESTRTIME", Date.Now.ToString("HH:mm"))

            cmd.Parameters.AddWithValue("@FACTNEED", 0)
            cmd.Parameters.AddWithValue("@FACTAMOUNT", 0)

            cmd.Parameters.AddWithValue("@FACTLOCATION", model.GeoLocation)

            cmd.Parameters.AddWithValue("@FVEHICLENO", "1กข1234")
            cmd.Parameters.AddWithValue("@FVEHICLETY", 1)
            cmd.Parameters.AddWithValue("@FMETERSTR", 50000)

            Dim rows As Integer = cmd.ExecuteNonQuery()
            Return If(rows > 0, FIANO, "")

        End Using
    End Function

    Public Function CheckOut(model As CheckOutViewModel) As Integer

        '-------------------------------
        Dim sql As String = "
        UPDATE OD50CIAC
                SET  
                    FLASTUPD       = @FLASTUPD,
                    FLASTUPDTM     = @FLASTUPDTM,
                    FLASTUPDBY     = @FLASTUPDBY,   

                    FACTFNDATE      = @FACTFNDATE,
                    FACTFNTIME      = @FACTFNTIME, 
                    FACTLOCATION   =  FACTLOCATION + '|' + @FACTLOCATION,

                    FMETEREND      = @FMETEREND

                WHERE FIANO = @FIANO"

        Dim rows As Integer

        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Dim cmd As New SqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@FIANO", model.DocNumber)

            cmd.Parameters.AddWithValue("@FLASTUPD", Date.Today)
            cmd.Parameters.AddWithValue("@FLASTUPDTM", Date.Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@FLASTUPDBY", model.UserID)

            cmd.Parameters.AddWithValue("@FACTFNDATE", Date.Today)
            cmd.Parameters.AddWithValue("@FACTFNTIME", Date.Now.ToString("HH:mm"))

            cmd.Parameters.AddWithValue("@FACTLOCATION", model.GeoLocation)

            cmd.Parameters.AddWithValue("@FMETEREND", model.OdometerEnd)

            rows = cmd.ExecuteNonQuery()

        End Using

        Return rows > 0

    End Function

End Class