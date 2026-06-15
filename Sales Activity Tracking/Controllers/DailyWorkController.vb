Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Web.Mvc
Imports System.Web.Services.Description
Imports Sales_Activity_Tracking.Controllers

Public Class DailyWorkController
    Inherits BaseController

    Private ReadOnly repo As New DailyWorkRepository

    ' GET: DailyWork 
    Function Index() As ActionResult

        'check TimeIn & assign value to model. *ตรวจสอบจาก cookie(User.Identity.Name)

        'Dim usr As String = If(
        '                        String.IsNullOrEmpty(User.Identity.Name),
        '                        Convert.ToString(Session("FSMCODE")),
        '                        User.Identity.Name
        '                    )

        Dim model As DailyWorkViewModel = repo.GetTodayTimIn(
                            Convert.ToString(Session("FSMCODE")),
                            Convert.ToString(Session("FUSERID"))
                            )
        Return View(model)
    End Function

    Function AlertMessage() As ActionResult
        Return View()
    End Function

#Region "TimeIn & TimeOut พนักงาน"
    <HttpPost>
    Function TimeInSave(model As TimeInViewModel, PhotoFile As HttpPostedFileBase) As JsonResult
        'model As TimeInViewModel ***MVC จะ Bind ให้เอง ถ้าชื่อใน FormData ตรงกับ Property
        Try


            Dim SalesmanCode = model.SalesmanCode
            Dim UseID = model.UserID
            Dim GeoLocation = model.GeoLocation
            Dim VehicleType = model.VehicleType
            Dim VehicleNo = model.VehicleNo
            Dim odometerStart = model.OdometerStart

            Dim file = Request.Files("PhotoFile")

            If file Is Nothing OrElse file.ContentLength = 0 Then
                Return Json(New With {
                    .Success = False,
                    .Message = "กรุณาถ่ายรูป"
                })

            Else

                ' 1. Insert Database
                Dim rs = repo.TimeIn(model)

                If rs.IsSuccess = True Then

                    ' 2. สร้างชื่อไฟล์  Dim fileName = Guid.NewGuid().ToString() & ".jpg" id.ToString("000000") 
                    ' {IA-Numbering}_01_xxx
                    Dim fileName = rs.DocNumber & "_01_TIMEIN" & Path.GetExtension(file.FileName)

                    ' 3. Save File
                    Dim PathFile = Server.MapPath("~/Uploads/" & fileName)
                    file.SaveAs(PathFile)

                    FormsAuthentication.SetAuthCookie(Session("FSMCODE"), True) 'เรียกใช้ผ่าน User.Identity.Name 

                    Session("FUSERID") = model.UserID

                End If


                Return Json(New With {
                    .Success = rs.IsSuccess,
                    .Message = rs.Message
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
    Public Function TimeOutSave(model As TimeOutViewModel) As JsonResult
        'model As TimeInViewModel ***MVC จะ Bind ให้เอง ถ้าชื่อใน FormData ตรงกับ Property
        Try

            Dim DocNumber = model.DocNumber
            Dim UserID = model.UserID
            Dim odometerStr = model.OdometerStart
            Dim odometerEnd = model.OdometerEnd
            Dim GeoLocation = model.GeoLocation

            If model.OdometerEnd < model.OdometerStart Then

                Return Json(New With {
                    .Success = False,
                    .Message = "เลขไมล์หลังใช้ต้องมากกว่าหรือเท่ากับเลขไมล์เริ่มต้น"
                })

            Else


                Dim file = Request.Files("PhotoFile")

                If file Is Nothing OrElse file.ContentLength = 0 Then
                    Return Json(New With {
                        .Success = False,
                        .Message = "กรุณาถ่ายรูป"
                    })

                Else

                    ' 1. Update Database
                    Dim rs = repo.TimeOut(model)
                    If rs.IsSuccess = True Then

                        ' 2. สร้างชื่อไฟล์  Dim fileName = Guid.NewGuid().ToString() & ".jpg" id.ToString("000000") 
                        ' {IA-Numbering}_02_xxx
                        Dim fileName = rs.DocNumber & "_02_TIMEOUT" & Path.GetExtension(file.FileName)

                        ' 3. Save File
                        Dim PathFile = Server.MapPath("~/Uploads/" & fileName)
                        file.SaveAs(PathFile)


                        FormsAuthentication.SignOut() 'เคลียร์ค่า

                        'userlogin markstatus การ login ชั่วคราว ก่อนที่จะตรวจสอบการ TimeIn จริงจาก database  
                        Session("FSMCODE") = ""
                        Session("FUSERID") = ""

                    End If


                    Return Json(New With {
                        .Success = rs.IsSuccess,
                        .Message = rs.Message
                    })

                End If

            End If

        Catch ex As Exception
            Return Json(New With {
                .Success = False,
                .Message = ex.Message
            })
        End Try

    End Function
#End Region

#Region "CheckIn & CheckOut เยี่ยมลูกค้า"

    <HttpPost>
    Public Function NewFarmerCheckInSave(newfarmer As NewFarmerViewModel) As ActionResult

        Try


            Dim model As New CheckInViewModel
            With model
                .SalesmanCode = Session("FSMCODE")
                .UserID = Session("FUSERID")
                .IsNewFarmer = True
                .FarmerCode = ""
                .FarmerName = newfarmer.FarmerName
                .MobileNo = newfarmer.MobileNo
                .AddressNo = newfarmer.AddressNo
                .Moo = newfarmer.Moo
                .VillageName = newfarmer.VillageName
                .SubDistrict = newfarmer.SubDistrictCode
                .District = newfarmer.DistrictCode
                .Province = newfarmer.ProvinceCode
                .GeoLocation = newfarmer.GeoLocation
            End With

            Dim rs = repo.CheckIn(model)

            If rs.IsSuccess = True Then

                Return View("Index")

            Else
                TempData("ErrorMessage") = rs.Message
                Return RedirectToAction("AlertMessage")
            End If
        Catch ex As Exception

            'ModelState.AddModelError("", ex.Message)
            'ViewBag.ErrorMessage = ex.Message
            TempData("ErrorMessage") = ex.Message
            Return RedirectToAction("AlertMessage")

        End Try

    End Function

    <HttpPost>
    Public Function CheckInSave(farmercode As String, farmername As String, GeoLocation As String) As JsonResult

        Try
            Dim _repoFarmer As New FarmerRepository
            Dim _farmer = _repoFarmer.GetFarmer(False, farmercode)

            Dim model As New CheckInViewModel
            With model
                .SalesmanCode = Session("FSMCODE")
                .UserID = Session("FUSERID")
                .IsNewFarmer = False
                .FarmerCode = farmercode
                .FarmerName = farmername
                .MobileNo = _farmer.MobileNo
                .AddressNo = _farmer.AddressNo
                .Moo = _farmer.Moo
                .VillageName = _farmer.VillageName
                .SubDistrict = _farmer.SubDistrict
                .District = _farmer.District
                .Province = _farmer.Province
                .GeoLocation = GeoLocation
            End With


            Dim rs = repo.CheckIn(model)

            If rs.IsSuccess = True Then

                Return Json(New With {
                    .Success = True,
                    .Message = "เพิ่มรายการแล้ว!",
                    .RedirectUrl = "/DailyWork/VisitFarmer?farmerCode=" & farmercode
                })

            Else
                Return Json(New With {
                    .Success = False,
                    .Message = rs.Message
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
    Public Function CheckOutSave(model As CheckOutViewModel) As JsonResult


        Dim id = CLng(Request.Form("ID"))
        Dim lat = Request.Form("Latitude")
        Dim lng = Request.Form("Longitude")

        Dim file = Request.Files("PhotoFile")
        Dim fileName = Guid.NewGuid.ToString() & Path.GetExtension(file.FileName)
        Dim PathFile = Server.MapPath("~/Uploads/" & fileName)

        file.SaveAs(PathFile)

        repo.CheckOut(model)

        Return Json(New With {
        .success = True
    })

    End Function

#End Region



#Region "Farmer & visit"

    Public Function SelectFarmer(FSMCODE As String) As ActionResult

        Dim model As New SelectFarmerViewModel

        model.FarmerList = New FarmerRepository().GetFarmerList(FSMCODE)

        Return View(model)

    End Function


    Public Function VisitFarmer(farmerCode As String) As ActionResult

        Dim repo As New FarmerRepository
        Dim farmer = repo.GetFarmer(False, farmerCode)

        If farmer Is Nothing Then
            TempData("ErrorMessage") = "ไม่พบรหัส : " & farmerCode
            Return RedirectToAction("AlertMessage")
        Else
            Dim model As New VisitFarmerViewModel
            model.Farmer = farmer

            Return View(model)
        End If

    End Function

    Public Function VisitFarmerEditMode(fiano As String) As ActionResult

        Dim model As New VisitFarmerEditModeViewModel()

        Dim atv As ActivityItem = repo.GetActivityItem(fiano)
        model.ActivityItem = atv

        Dim _repoFarmer As New FarmerRepository
        model.Farmer = _repoFarmer.GetFarmer(atv.IsNewCont, If(atv.IsNewCont, atv.ActivityNumber, atv.ContactCode))


        'Dim _repoQuesn As New QuestionnaireRepository
        'model.Questionnaire = _repoQuesn.GetQuestionnaireActiveForm()

        Return View(model)

    End Function
    Public Function VisitItemDelete(fiano As String) As ActionResult

        Dim model As New VisitFarmerEditModeViewModel()

        Dim atv As ActivityItem = repo.GetActivityItem(fiano)
        model.ActivityItem = atv

        Dim _repoFarmer As New FarmerRepository
        model.Farmer = _repoFarmer.GetFarmer(atv.IsNewCont, If(atv.IsNewCont, atv.ActivityNumber, atv.ContactCode))

        Return View(model)

    End Function



    <HttpPost>
    Public Function DeleteVisitItem(activityNo As String) As ActionResult

        Try

            Dim rs = repo.DeleteActivity(activityNo)
            If rs.IsSuccess Then
                Return RedirectToAction("Index")
            Else
                TempData("ErrorMessage") = rs.Message
                Return RedirectToAction("AlertMessage")
            End If


        Catch ex As Exception
            TempData("ErrorMessage") = ex.Message
            Return RedirectToAction("AlertMessage")
        End Try

    End Function

    Public Function NewFarmer() As ActionResult

        Dim model As New NewFarmerViewModel

        With model
            .ProvinceList = New AddressRepository().GetProvinceList()
            .DistrictList = New List(Of SelectListItem)
            .SubDistrictList = New List(Of SelectListItem)
        End With

        Return View(model)
    End Function

    Public Function GetDistrictList(provinceCode As String) As JsonResult
        Dim list = New AddressRepository().GetDistrictList(provinceCode)
        Return Json(list, JsonRequestBehavior.AllowGet)
    End Function

    Public Function GetSubDistrictList(provinceCode As String, districtCode As String) As JsonResult
        Dim list = New AddressRepository().GetSubDistrictList(provinceCode, districtCode)
        Return Json(list, JsonRequestBehavior.AllowGet)
    End Function
#End Region




    <HttpPost>
    Public Function DeleteActivity(activityNo As String) As JsonResult
        Try

            Dim rs = repo.DeleteActivity(activityNo)

            Return Json(New With {
                    .Success = rs.IsSuccess,
                    .Message = rs.Message,
                    .RedirectUrl = "/DailyWork/Index"
                })
        Catch ex As Exception

            Return Json(New With {
                .Success = False,
                .Message = ex.Message
            })


        End Try

    End Function

End Class
