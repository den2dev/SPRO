Imports System.Data.SqlClient

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private ReadOnly repo As New DailyWorkRepository

    Function Index() As ActionResult

        'Dim usr = User.Identity.Name 'เรียกใช้ cookie

        'Session("salescode") = "99999"

        Return View()
    End Function

    Function Landing(FSMCODE As String) As ActionResult

        'Response.Redirect("/DailyWork/Landing?FSMCODE=" & SalesmanCode)
        '<a href = "/DailyWork/Landing?FSMCODE=0101" >
        '    เปิด Daily Work
        '</a>

        ' Dim fsmCode As String = Request.QueryString("FSMCODE")

        If Not String.IsNullOrEmpty(FSMCODE) Then
            Session("FSMCODE") = FSMCODE
            Return RedirectToAction("Index", "DailyWork") 'Redirect("/DailyWork/Index") '
        Else
            Return View() 'แจ้งเตือนไม่พบ FSMCODE
        End If

    End Function

    Function Logout() As ActionResult

        Session("FSMCODE") = Nothing

        ' หรือ
        Session.Remove("FSMCODE")

        Return RedirectToAction("Index", "Home")

    End Function

    Function Test() As ActionResult
        ViewData("Message") = "Test page."
        Return View()
    End Function


End Class
