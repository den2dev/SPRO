Imports System.Data.SqlClient

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private ReadOnly repo As New DailyWorkRepository

    Function Index() As ActionResult
        Session("salescode") = "99999"
        Return View()
    End Function


    Function Test() As ActionResult
        ViewData("Message") = "Test page."
        Return View()
    End Function


End Class
