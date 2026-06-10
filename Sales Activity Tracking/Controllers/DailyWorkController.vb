Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.Mvc

Public Class DailyWorkController
    Inherits Controller

    Private ReadOnly repo As New DailyWorkRepository

    ' GET: DailyWork
    Function Index() As ActionResult
        'Dim salesCode = User.Identity.Name

        Dim salesCode = Session("salesCode")

        ' Dim today = repo.GetTodayCheckIn(salesCode)
        'If today IsNot Nothing Then 
        'End If

        Dim model As New DailyWorkViewModel
        With model
            .SalesmanName = "นายพนักงาน นามสกุล"
            .SalesmanCode = "xxx"
        End With

        If repo.GetTodayCheckIn(salesCode) = True Then '

            model.IsCheckedIn = True

        Else
            With model
                .IsCheckedIn = False
                .VehicleList = repo.GetVehicleList()
            End With
        End If


        Return View(model)
    End Function


    '****** พนักงาน
    Function TimeIn() As ActionResult
        Dim model As New TimeInViewModel

        model.SalesmanCode = User.Identity.Name
        model.SalesmanName = "Den"

        model.WorkDate = Today
        model.WorkTime = DateTime.Now.ToString("HH:mm")

        Return View(model)

    End Function
    Function TimeOut() As ActionResult
        Return View()
    End Function
    <HttpPost>
    Function TimeInSave(model As TimeInViewModel,
    PhotoFile As HttpPostedFileBase) As JsonResult

        Try

            ' Save Image

            ' Insert Database

            Return Json(New With {
                .Success = True
            })

            Session("salesCode") = model.SalesmanCode

        Catch ex As Exception

            Return Json(New With {
            .Success = False,
            .Message = ex.Message
        })

        End Try

    End Function

    <HttpPost>
    Public Function SaveTimeIn() As JsonResult

        '    Dim employeeCode = User.Identity.Name

        '    Dim lat = Request.Form("Latitude")
        '    Dim lng = Request.Form("Longitude")
        '    Dim file = Request.Files("PhotoFile")

        '    Dim fileName = Guid.NewGuid.ToString() & Path.GetExtension(file.FileName)
        '    Dim PathFile = Server.MapPath("~/Uploads/" & fileName)
        '    file.SaveAs(PathFile)

        '    Dim id = repo.TimeIn(
        'employeeCode,
        'lat,
        'lng,
        'PathFile)

        Return Json(New With {
                .success = True,
                .id = 555
            })
    End Function

    <HttpPost>
    Public Function SaveTimeOut() As JsonResult
        '    Dim employeeCode = User.Identity.Name

        '    Dim lat = Request.Form("Latitude")
        '    Dim lng = Request.Form("Longitude")
        '    Dim file = Request.Files("PhotoFile")

        '    Dim fileName = Guid.NewGuid.ToString() & Path.GetExtension(file.FileName)
        '    Dim PathFile = Server.MapPath("~/Uploads/" & fileName)
        '    file.SaveAs(PathFile)

        '    Dim id = repo.TimeIn(
        'employeeCode,
        'lat,
        'lng,
        'PathFile)

        Return Json(New With {
            .success = True,
            .id = 888'id
        })
    End Function


    '****** ติดต่อลูกค้าแต่ละคน WorkItems
    Function CheckIn() As ActionResult
        Return View()
    End Function

    Function CheckOut() As ActionResult
        Return View()
    End Function

    <HttpPost>
    Public Function SaveCheckIn() As JsonResult


        Dim employeeCode = User.Identity.Name

        Dim lat = Request.Form("Latitude")
        Dim lng = Request.Form("Longitude")
        Dim file = Request.Files("PhotoFile")

        Dim fileName = Guid.NewGuid.ToString() & Path.GetExtension(file.FileName)
        Dim PathFile = Server.MapPath("~/Uploads/" & fileName)
        file.SaveAs(PathFile)

        Dim id = repo.CheckIn(
    employeeCode,
    lat,
    lng,
    PathFile)

        Return Json(New With {
            .success = True,
            .id = id
        })

    End Function

    <HttpPost>
    Public Function SaveCheckOut() As JsonResult


        Dim id = CLng(Request.Form("ID"))
        Dim lat = Request.Form("Latitude")
        Dim lng = Request.Form("Longitude")

        Dim file = Request.Files("PhotoFile")
        Dim fileName = Guid.NewGuid.ToString() & Path.GetExtension(file.FileName)
        Dim PathFile = Server.MapPath("~/Uploads/" & fileName)

        file.SaveAs(PathFile)

        repo.CheckOut(id, lat, lng, PathFile)

        Return Json(New With {
        .success = True
    })

    End Function


End Class
