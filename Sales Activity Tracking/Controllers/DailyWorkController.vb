Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Web.Mvc

Public Class DailyWorkController
    Inherits Controller

    Private ReadOnly repo As New DailyWorkRepository

    ' GET: DailyWork 
    Function Index() As ActionResult

        'check TimeIn & assign value to model. *ตรวจสอบจาก cookie(User.Identity.Name)

        'Dim usr As String = If(
        '                        String.IsNullOrEmpty(User.Identity.Name),
        '                        Convert.ToString(Session("FSMCODE")),
        '                        User.Identity.Name
        '                    )

        Dim model As DailyWorkViewModel = repo.GetTodayTimIn(Convert.ToString(Session("FSMCODE")))
        Return View(model)
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
    Function CheckIn() As ActionResult
        Return View()
    End Function
    Function CheckOut() As ActionResult
        Return View()
    End Function

    <HttpPost>
    Public Function CheckInSave(model As CheckInViewModel) As JsonResult


        Dim employeeCode = User.Identity.Name

        Dim lat = Request.Form("Latitude")
        Dim lng = Request.Form("Longitude")
        Dim file = Request.Files("PhotoFile")

        Dim fileName = Guid.NewGuid.ToString() & Path.GetExtension(file.FileName)
        Dim PathFile = Server.MapPath("~/Uploads/" & fileName)
        file.SaveAs(PathFile)

        Dim id = repo.CheckIn(model)

        Return Json(New With {
            .success = True,
            .id = id
        })

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


    Public Function VisitFarmer(isnewfarmer As Boolean, farmerCode As String) As ActionResult
        Dim _repoFarmer As New FarmerRepository


        Dim model As New VisitFarmerViewModel()
        model.Farmer = _repoFarmer.GetFarmer(isnewfarmer, farmerCode)

        'Dim _repoQuesn As New QuestionnaireRepository
        'model.Questionnaire = _repoQuesn.GetQuestionnaireActiveForm()

        Return View(model)

    End Function

#End Region



#Region "Add New Farmer"
    Public Function NewFarmer() As ActionResult
        Dim model As New NewFarmerViewModel

        With model
            .ProvinceList = New AddressRepository().GetProvinceList()
            .DistrictList = New List(Of SelectListItem)
            .SubDistrictList = New List(Of SelectListItem)
        End With

        Return View(model)
    End Function

    <HttpPost>
    Public Function NewFarmer(model As NewFarmerViewModel) As ActionResult

        Try

            Dim repo As New FarmerRepository

            Dim _farmer = repo.CreateFarmer(model)

            Return RedirectToAction(
            "VisitFarmer",
            New With {
                .farmerCode = _farmer.FarmerCode,
                ._farmer = _farmer
            })

        Catch ex As Exception

            'ModelState.AddModelError("", ex.Message)
            ViewBag.ErrorMessage = ex.Message

            model.ProvinceList = New AddressRepository().GetProvinceList()

            Return View(model)

        End Try

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


End Class
