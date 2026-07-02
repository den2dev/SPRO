Imports System.Web.Mvc

Namespace Controllers
    Public Class DailyReportController
        Inherits Controller

        Private ReadOnly repo As New DailyReportRepository
        ' GET: DailyReport
        Function Index() As ActionResult

            Dim model As List(Of DailyReportViewModel) = repo.GetTIOList(
                            Convert.ToString(Session("FSMCODE")),
                            Convert.ToString(Session("FUSERID")), Today().ToString("dd/MM/yyyy"), Today().ToString("dd/MM/yyyy"))

            ViewBag.tio_couns = model.Count

            Return View(model)

        End Function
        Function TIOList(startdate As String, enddate As String) As PartialViewResult

            Dim model As List(Of DailyReportViewModel) = repo.GetTIOList(
                            Convert.ToString(Session("FSMCODE")),
                            Convert.ToString(Session("FUSERID")), startdate, enddate)

            ViewBag.tio_couns = model.Count

            Return PartialView("_TIOList", model)

        End Function
    End Class
End Namespace