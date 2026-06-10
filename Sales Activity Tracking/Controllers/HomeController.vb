Imports System.Data.SqlClient

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private ReadOnly repo As New DailyWorkRepository

    Function Index() As ActionResult
        Return View()
    End Function



    Function TestDesignPage() As ActionResult
        'ViewBag.Title = "Daily Report"

        Dim SalesmanCode = User.Identity.Name

        Dim today = repo.GetTodayCheckIn(SalesmanCode)

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
    Function Test() As ActionResult
        ViewData("Message") = "Test page."
        Return View()
    End Function


    '-------Test connection-------
    Public Function TestDB() As ActionResult

        Try

            Using conn As SqlConnection = DBConnection.GetConnection()

                conn.Open()

                Return Content("Connect Success")

            End Using

        Catch ex As Exception

            Return Content("Connect Failed : " & ex.Message)

        End Try

    End Function

End Class
