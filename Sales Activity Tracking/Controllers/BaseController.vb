Imports System.Web.Mvc

Namespace Controllers
    Public Class BaseController
        Inherits Controller

        Protected Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)

            'Session("FSMCODE") = ""
            'Session("FUSERID") = ""

            If Session("FSMCODE") Is Nothing Or Session("FUSERID") Is Nothing Then

                FormsAuthentication.SignOut()

                filterContext.Result = RedirectToAction("SessionTimeOut", "Home")

                Return

            End If

            MyBase.OnActionExecuting(filterContext)

        End Sub


    End Class
End Namespace