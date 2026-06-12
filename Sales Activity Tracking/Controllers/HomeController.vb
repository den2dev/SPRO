Imports System.Data.SqlClient

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private ReadOnly repo As New DailyWorkRepository

    Function Index() As ActionResult

        'Dim usr = User.Identity.Name 'เรียกใช้ cookie

        Session("salescode") = "99999"

        Return View()
    End Function
    Function Logout() As ActionResult

        Session("userlogin") = Nothing

        ' หรือ
        Session.Remove("userlogin")

        Return RedirectToAction("Index", "Home")

    End Function

    Function Test() As ActionResult
        ViewData("Message") = "Test page."
        Return View()
    End Function


End Class
