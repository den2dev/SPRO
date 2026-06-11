Imports System.Data.SqlClient
Imports System.Web.Mvc

Namespace Controllers
    Public Class TestController
        Inherits Controller

        ' GET: Test
        Function Index() As ActionResult
            Return View()
        End Function
        Function TestLoading() As ActionResult
            Return View()
        End Function
        Function TestMessagebox() As ActionResult
            Return View()
        End Function

        Function TestConnection() As ActionResult
            Return View()
        End Function
        <HttpPost>
        Function TestConnectDB() As ActionResult
            Try

                Using conn As SqlConnection = DBConnection.GetConnection()

                    conn.Open()

                    Return Content("Connect Success")

                End Using

            Catch ex As Exception

                Return Content("Connect Failed : " & ex.Message)

            End Try
        End Function

        Function TestGPS() As ActionResult
            Return View()
        End Function
        Function TestCamera() As ActionResult
            Return View()
        End Function
        Function TestDesignDataModel() As ActionResult
            'ViewBag.Title = "Daily Report"

            Dim SalesmanCode = User.Identity.Name


            Dim model As New DailyWorkViewModel

            'If today IsNot Nothing Then

            '    model.IsCheckedIn = True
            '    '  model.CheckInID = today.ID

            'Else

            '    model.IsCheckedIn = False

            'End If

            With model
                .SalesmanName = "นายพนักงาน xxxx"
            End With
            Return View(model)

        End Function
    End Class
End Namespace