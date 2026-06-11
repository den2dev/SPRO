Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.Mvc

Public Class DailyWorkController
    Inherits Controller

    Private ReadOnly repo As New DailyWorkRepository

    ' GET: DailyWork
    Function Index() As ActionResult

        'check TimeIn & assign value to model. *ตรวจสอบจาก cookie(User.Identity.Name)
        Dim usr As String = If(
                                String.IsNullOrEmpty(User.Identity.Name),
                                Convert.ToString(Session("Temp_login")),
                                User.Identity.Name
                            )

        Dim model As DailyWorkViewModel = repo.GetTodayCheckIn(usr)

        Return View(model)
    End Function


    '****** พนักงาน
    <HttpPost>
    Function TimeInSave(model As TimeInViewModel, PhotoFile As HttpPostedFileBase) As JsonResult
        'model As TimeInViewModel ***MVC จะ Bind ให้เอง ถ้าชื่อใน FormData ตรงกับ Property
        Try

            'Dim lat = Request.Form("Latitude")
            'Dim lng = Request.Form("Longitude")



            Dim vehicleLicensePlate = model.VehicleLicensePlate
            Dim odometerStart = model.OdometerStart
            Dim lat = model.Latitude
            Dim lng = model.Longitude

            Dim file = Request.Files("PhotoFile")

            If file Is Nothing OrElse file.ContentLength = 0 Then
                Return Json(New With {
                    .Success = False,
                    .Message = "กรุณาถ่ายรูป"
                })

            Else

                ' 1. Insert Database
                ' Dim id =  repo.TimeIn(
                'employeeCode,
                'lat,
                'lng,
                'PathFile) 

                Dim id = 123 ' id = 12345

                ' 2. สร้างชื่อไฟล์
                'Dim fileName = Guid.NewGuid().ToString() & ".jpg" 
                Dim fileName = "TIMEIN_" & id.ToString("000000") & Path.GetExtension(file.FileName)

                ' 3. Save File
                Dim PathFile = Server.MapPath("~/Uploads/" & fileName)
                file.SaveAs(PathFile)




                FormsAuthentication.SetAuthCookie(Session("salescode"), True) 'เรียกใช้ผ่าน User.Identity.Name 

                'Temp_login markstatus การ login ชั่วคราว ก่อนที่จะตรวจสอบการ TimeIn จริงจาก database 
                Session("Temp_login") = Session("salescode")


                Return Json(New With {
                    .Success = True,
                    .Message = "Success"
                })

            End If
        Catch ex As Exception
            Return Json(New With {
                .Success = False,
                .Message = ex.Message
            })
        End Try

    End Function


    <HttpPost>
    Public Function TimeOutSave() As JsonResult
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


        'Temp_login markstatus การ login ชั่วคราว ก่อนที่จะตรวจสอบการ TimeIn จริงจาก database 
        Session("Temp_login") = ""

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
