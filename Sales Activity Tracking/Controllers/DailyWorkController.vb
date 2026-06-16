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
    Public Function NewFarmerCheckInSave(newfarmer As NewFarmerViewModel) As JsonResult

        Try


            Dim model As New CheckInViewModel
            With model
                .SalesmanCode = Session("FSMCODE")
                .UserID = Session("FUSERID")
                .IsNewFarmer = True
                .NewFarmerType = newfarmer.NewType
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

                Return Json(New With {
                    .Success = True,
                    .Message = "เพิ่มรายการแล้ว!",
                    .RedirectUrl = "/DailyWork/VisitFarmer?fiano=" & rs.DocNumber
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
                    .RedirectUrl = "/DailyWork/VisitFarmer?fiano=" & rs.DocNumber
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
    Public Function CheckOutSave(fiano As String, GeoLocation As String) As JsonResult

        Try

            Dim model As New CheckOutViewModel
            With model
                .SalesmanCode = Session("FSMCODE")
                .UserID = Session("FUSERID")
                .DocNumber = fiano
                .GeoLocation = GeoLocation
            End With


            Dim rs = repo.CheckOut(model)

            If rs.IsSuccess = True Then

                Return Json(New With {
                    .Success = True,
                    .Message = "Check Out แล้ว!",
                    .RedirectUrl = "/DailyWork/Index"
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

#End Region



#Region "Farmer & visit"

    Public Function SelectFarmer(FSMCODE As String) As ActionResult

        Dim model As New SelectFarmerViewModel

        model.FarmerList = New FarmerRepository().GetFarmerList(FSMCODE)

        Return View(model)

    End Function

    Public Function Questionnaire(fiano As String, fcontcode As String, fqesntype As Integer) As ActionResult

        Dim baseUrl As String = "https://spclnt3.softprohub.net/AFSKSP/App_CRM/frmOC20QANS.aspx?"

        Dim qs As New List(Of String)

        qs.Add("ShowExit=Y")
        qs.Add("ISCODE=010004")
        qs.Add("REFNO=26IA1906150006")
        qs.Add("CONTCODE=19000001")
        qs.Add("ENTTYPE=2")
        qs.Add("ParamCode=B5NdcfPHKgjiyX5AOECjcA==")

        Dim url As String = baseUrl & String.Join("&", qs)

        ViewBag.QuestionnaireUrl = url

        Return View()

    End Function
    'Public Function VisitFarmer(farmerCode As String) As ActionResult

    '    Dim repo As New FarmerRepository
    '    Dim farmer = repo.GetFarmer(False, farmerCode)

    '    If farmer Is Nothing Then
    '        TempData("ErrorMessage") = "ไม่พบรหัส : " & farmerCode
    '        Return RedirectToAction("AlertMessage")
    '    Else
    '        Dim model As New VisitFarmerViewModel
    '        model.Farmer = farmer

    '        Return View(model)
    '    End If

    'End Function

    Public Function VisitFarmer(fiano As String) As ActionResult

        Dim model As New VisitFarmerEditModeViewModel()

        Dim atv As ActivityItem = repo.GetActivityItem(fiano)
        model.ActivityItem = atv

        Dim _repoFarmer As New FarmerRepository
        model.Farmer = _repoFarmer.GetFarmer(atv.IsNewCont, If(atv.IsNewCont, atv.ActivityNumber, atv.ContactCode))

        Return View(model)

    End Function

    Function VisitFarmerPhoto(activityNo As String, ischeckout As Boolean) As ActionResult

        Dim model As New ActivityPhotoViewModel

        model.ActivityNo = activityNo
        model.IsCheckOut = ischeckout

        model.Photos = GetPhotoList(activityNo)

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


#Region "Photo"
    Function PhotoList(activityNo As String) As PartialViewResult

        Dim photos = GetPhotoList(activityNo)

        Return PartialView("_PhotoList", photos)

    End Function


    <HttpPost>
    Public Function UploadPhoto(activityNo As String) As JsonResult

        Try

            Dim uploadFolder =
                Server.MapPath("~/Uploads")

            If Not Directory.Exists(uploadFolder) Then
                Directory.CreateDirectory(uploadFolder)
            End If

            Dim nextNo = GetNextPhotoNo(activityNo)

            For i = 0 To Request.Files.Count - 1

                Dim file = Request.Files(i)

                If file.ContentLength > 0 Then

                    Dim ext =
                        Path.GetExtension(file.FileName)

                    Dim fileName =
                        activityNo &
                        "_" &
                        nextNo &
                        ext

                    file.SaveAs(
                        Path.Combine(
                            uploadFolder,
                            fileName))

                    nextNo += 1

                End If

            Next

            Return Json(New With {
                .success = True
            })

        Catch ex As Exception

            Return Json(New With {
                .success = False,
                .message = ex.Message
            })

        End Try

    End Function

    <HttpPost>
    Public Function DeletePhoto(fileName As String) As JsonResult

        Try

            Dim physicalFile =
                Server.MapPath("~/Uploads/" & fileName)

            If System.IO.File.Exists(physicalFile) Then

                System.IO.File.Delete(physicalFile)

            End If

            Return Json(New With {
                .success = True
            })

        Catch ex As Exception

            Return Json(New With {
                .success = False,
                .message = ex.Message
            })

        End Try

    End Function

    Private Function GetPhotoList(activityNo As String) As List(Of String)

        Dim result As New List(Of String)

        Dim uploadFolder =
            Server.MapPath("~/Uploads")

        If Not Directory.Exists(uploadFolder) Then

            Return result

        End If

        Dim files =
            Directory.GetFiles(
                uploadFolder,
                activityNo & "_*.*")

        result =
            files.
            Select(Function(f)
                       Return Path.GetFileName(f)
                   End Function).
            OrderBy(Function(x) x).
            ToList()

        Return result

    End Function

    Private Function GetNextPhotoNo(
        activityNo As String
    ) As Integer

        Dim uploadFolder =
            Server.MapPath("~/Uploads")

        Dim files =
            Directory.GetFiles(
                uploadFolder,
                activityNo & "_*.*")

        If files.Count = 0 Then

            Return 1

        End If

        Dim maxNo =
            files.Select(Function(f)

                             Dim name =
                                 Path.GetFileNameWithoutExtension(f)

                             Dim arr =
                                 name.Split("_"c)

                             Return Integer.Parse(arr(1))

                         End Function).Max()

        Return maxNo + 1

    End Function
#End Region


End Class
