Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Reflection
Imports System.Web.Util
Imports Dapper
Imports Microsoft.VisualBasic.ApplicationServices

Public Class DailyReportRepository
    Public Function GetTIOList(SalesmanCode As String, UserID As String, StartDate As String, EndDate As String) As List(Of DailyReportViewModel)

#Region "SQL"

        Dim model As New List(Of DailyReportViewModel)

        Dim sql As String = "
            SELECT  
                FIANO,
                FACTDATE,
                FESTRDATE,
                FESTRTIME,
                FACTFNDATE,
                FACTFNTIME,
                FVEHICLENO,
                FMETERSTR,
                FVEHICLETY,
                FACTLOCATION,
                FMETEREND,
                LD01SMAN.FSMNAME

            FROM OD50CIAC LEFT OUTER JOIN LD01SMAN ON LD01SMAN.FSMCODE = OD50CIAC.FSMCODE
            WHERE OD50CIAC.FSMCODE = @FSMCODE
            AND FIACODE = @FIACODE
            AND (FACTDATE >=@StartDate AND FACTDATE <=@EndDate )"


        Using cn As SqlConnection = DBConnection.GetConnection()

            cn.Open()

            Using cmd As New SqlCommand(sql, cn)

                cmd.Parameters.AddWithValue("@FSMCODE", SalesmanCode)
                cmd.Parameters.AddWithValue("@FIACODE", "TIO")
                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = StartDate
                cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = EndDate

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    If dr.Read() Then

                        Dim item As New DailyReportViewModel
                        With item

                            .SalesmanCode = dr("FSMNAME").ToString()
                            .UserID = UserID

                            .TIO_Date = If(IsDBNull(dr("FACTDATE")), "", Convert.ToDateTime(dr("FACTDATE")).ToString("dd/MM/yyyy"))
                            .TIO_DocNumber = dr("FIANO").ToString()

                            .TimeIn_DateTime = If(IsDBNull(dr("FESTRDATE")), "", Convert.ToDateTime(dr("FESTRDATE")).ToString("dd/MM/yyyy")) &
                                If(IsDBNull(dr("FESTRTIME")), "", "  " & dr("FESTRTIME").ToString() & " น.")

                            .TimeOut_DateTime = If(IsDBNull(dr("FACTFNDATE")), "", Convert.ToDateTime(dr("FACTFNDATE")).ToString("dd/MM/yyyy")) &
                                If(IsDBNull(dr("FACTFNTIME")), "", "  " & dr("FACTFNTIME").ToString() & " น.")

                            .VehicleNo = dr("FVEHICLENO").ToString()
                            .VehicleType = If(IsDBNull(dr("FVEHICLETY")), "", If(dr("FVEHICLETY") = 0, "รถบริษัท", "รถตนเอง"))
                            .OdometerStart = dr("FMETERSTR").ToString()
                            .OdometerEnd = dr("FMETEREND").ToString()
                            .GeoLocation = dr("FACTLOCATION").ToString()
                            .IsTimeOut = Not String.IsNullOrEmpty(.TimeOut_DateTime)

                        End With

                        item.ActivityItems = GetActivityItems(dr("FIANO").ToString())

                        model.Add(item)

                    End If

                End Using

            End Using


        End Using

#End Region


        Return model

    End Function

    Private Function GetActivityItems(fiacno As String) As List(Of ActivityItem)
        Dim items As New List(Of ActivityItem)

        Dim sqlActivity = "SELECT  
                                 FIACODE,
                                 FIANO,
                                 FCONTCODE,
                                 FCONTNAME,
                                 FESTRDATE,
                                 FESTRTIME,
                                 FACTFNDATE,
                                 FACTFNTIME	
                                FROM OD50CIAC 
                                WHERE FIACODE='VIS' 
                                and FACTDATE=@FACTDATE
                                order by FACTTIME"

        Using cmd_Activity As New SqlCommand(sqlActivity, cn)

            cmd_Activity.Parameters.AddWithValue("@FACTDATE", FACTDate)

            Using dr As SqlDataReader = cmd_Activity.ExecuteReader()

                While dr.Read()

                    ActivityItems.Add(
                                New ActivityItem With {
                                    .ActivityCode = dr("FIACODE").ToString(),
                                    .ActivityNumber = dr("FIANO").ToString(),
                                    .ContactCode = dr("FCONTCODE").ToString(),
                                    .ContactName = dr("FCONTNAME").ToString(),
                                    .CheckInDateTime = If(IsDBNull(dr("FESTRDATE")), "", Convert.ToDateTime(dr("FESTRDATE")).ToString("dd/MM/yyyy")) &
                                                         If(IsDBNull(dr("FESTRTIME")), "", dr("FESTRTIME").ToString() & " น."),
                                    .CheckOutDateTime = If(IsDBNull(dr("FACTFNDATE")), "", Convert.ToDateTime(dr("FACTFNDATE")).ToString("dd/MM/yyyy")) &
                                                        If(IsDBNull(dr("FACTFNTIME")), "", dr("FACTFNTIME").ToString() & " น.")
                            })

                End While

            End Using

        End Using

        model.ActivityItems = ActivityItems

        Dim sql As String = "SELECT  
                                 FIACODE,
                                 FIANO,
                                 FCONTCODE,
                                 FCONTNAME,
                                 FESTRDATE,
                                 FESTRTIME,
                                 FACTFNDATE,
                                 FACTFNTIME	
                                FROM OD50CIAC 
                                WHERE FIANO=@FIANO"

        Using cn As SqlConnection = DBConnection.GetConnection()
            cn.Open()
            Using cmd As New SqlCommand(sql, cn)

                cmd.Parameters.AddWithValue("@FIANO", fiacno)

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    If dr.Read() Then

                        With 

                            .ActivityCode = dr("FIACODE").ToString()
                            .ActivityNumber = dr("FIANO").ToString()
                            .ContactCode = dr("FCONTCODE").ToString()
                            .ContactName = dr("FCONTNAME").ToString()
                            .CheckInDateTime = If(IsDBNull(dr("FESTRDATE")), "", Convert.ToDateTime(dr("FESTRDATE")).ToString("dd/MM/yyyy")) &
                                                     If(IsDBNull(dr("FESTRTIME")), "", dr("FESTRTIME").ToString() & " น.")
                            .CheckOutDateTime = If(IsDBNull(dr("FACTFNDATE")), "", Convert.ToDateTime(dr("FACTFNDATE")).ToString("dd/MM/yyyy")) &
                                                    If(IsDBNull(dr("FACTFNTIME")), "", dr("FACTFNTIME").ToString() & " น.")

                        End With

                    End If

                End Using

            End Using
        End Using

        Return items
    End Function



End Class