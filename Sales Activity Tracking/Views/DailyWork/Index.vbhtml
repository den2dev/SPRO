@ModelType DailyWorkViewModel
@Code
    ViewData("Title") = "Daily Report"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
    Dim HomeURL = ConfigurationManager.AppSettings("HomeURL")
End Code

<style>
    #mainItems {
        height: calc(100vh - 217px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }
</style>

<div>
    @*class="container"*@

    <div class="panel panel-primary" style="padding-bottom:5px">

        <div class="panel-body">

            <div class="row" hidden>

                <div class="col-6 text-start">
                    <input type="checkbox" id="chkallcheckout" checked="@Model.IsAllCheckOut" />
                </div>
                <div class="col-6 text-start">
                    <input type="text" id="txtUserID" value="@Model.UserID" />
                </div>

                <div class="col-6 text-start">
                    <input type="text" id="txtSalesmanCode" value="@Model.SalesmanCode" />
                </div>


                <div class="col-6 text-start">
                    <input type="text" id="txtDocumentNumber" value="@Model.DocNumber" />
                </div>

                <input type="text" id="txtgeolocation" />

            </div>

            <div class="row">
                <div class="col-4 text-end">
                    พนักงานส่งเสริม :
                </div>
                <div class="col-8 text-start">
                    @Model.SalesmanName
                </div>
            </div>
            <div class="row">
                <div class="col-4 text-end">
                    วันที่เข้างาน :
                </div>
                <div class="col-8">
                    @Model.TimeInDate @Model.TimeInTime
                </div>
            </div>
            <div class="row">
                <div class="col-4 text-end">
                    ทะเบียนรถ :
                </div>
                <div class="col-8">
                    @Model.VehicleNo
                </div>
            </div>
            <div class="row">
                <div class="col-4 text-end">
                    เลขไมล์เริ่มต้น :
                </div>
                <div class="col-8">
                    @Model.OdometerStart
                </div>
            </div>
        </div>

    </div>



    <div class="row g-0">
        <div id="mainItems">

            @If Model.ActivityItems IsNot Nothing AndAlso Model.ActivityItems.Any() Then


                @For Each item In Model.ActivityItems

                    @<div class="card"
                          style="margin-bottom: 10px; padding: 10px; border: 1px solid #ddd; border-radius: 2px;">

                        <!-- Header -->
                        <div style="display:flex;justify-content:space-between;align-items:center;">
                            <strong>@item.ActivityName</strong>

                            <span id="btnDeleteAtivity"
                                  onclick="DeleteAtivityItem('@item.ActivityNumber')"
                                  style="color:red;cursor:pointer;font-size:16px;">
                                🗑️
                            </span>
                        </div>

                        <div style="cursor: pointer;"
                             onclick="location.href='@Url.Action("VisitFarmer", New With {.fiano = item.ActivityNumber})'">

                            <div style="margin-top:5px;">
                                Activity No. : @item.ActivityNumber
                            </div>

                            <!-- Contact + Type -->
                            <div style="display:flex;
                                        justify-content:space-between;
                                        align-items:center;
                                        margin-top:5px;">

                                <span>@item.ContactCode  @item.ContactName</span>

                                <span style="color:#666;">
                                    @item.TypeContactName
                                </span>
                            </div>

                            @If item.IsCheckOut Then
                                @<div style="margin-top:5px;">
                                    ⏰ @item.CheckInDateTime - @item.CheckOutDateTime
                                </div>
                            Else
                                @<div style="margin-top:5px;">
                                    ⏰ @item.CheckInDateTime - <span style="color:red">Check Out!</span>
                                </div>
                            End If
                        </div>

                    </div>

                Next

            Else

                @<div style="height:100%;display:flex;align-items:center;justify-content:center;">
                    @If Not Model.IsTimeIn Then
                        @<span class="small">กรุณาบันทึกลงเวลา Time In!</span>
                    Else
                        @<span class="small">0 items.</span>
                    End If
                </div>

            End If

        </div>
    </div>


</div>

@***หาพิกัด*****@
<div id="gpsLoadingModal"
     class="modal fade"
     data-backdrop="static"
     data-keyboard="false">

    <div class="modal-dialog modal-sm">

        <div class="modal-content">

            <div class="modal-body text-center">

                <h4>กำลังค้นหาตำแหน่ง</h4>

                <br />

                <div class="loading-spinner"></div>

                <br /><br />

                กรุณารอสักครู่...

            </div>

        </div>

    </div>

</div>

<div id="gpsErrorModal"
     class="modal fade"
     data-bs-backdrop="static"
     data-bs-keyboard="false">

    <div class="modal-dialog">

        <div class="modal-content">

            <div class="modal-header">

                <h4>ไม่สามารถระบุตำแหน่งได้</h4>

            </div>

            <div class="modal-body">

                กรุณาเปิด Location ของอุปกรณ์
                แล้วลองใหม่อีกครั้ง

            </div>

            <div class="modal-footer">

                @*<button class="btn btn-danger"
                    data-dismiss="modal" onclick="$('#gpsErrorModal').modal('hide');">

                        ปิด

                    </button>*@

                <a href="#" data-dismiss="modal"
                   class="ui-btn btn-style col no-padding"
                   onclick="$('#gpsErrorModal').modal('hide');">

                    <img src="@(StaticRootImgs)/back-black.png" alt="ปิด" class="button-menu-icon" />
                    <span class="button-menu-label">ปิด</span>

                </a>

            </div>

        </div>

    </div>

</div>


<style>
    /* Iframe Image */
    .image-frame {
        width: 100%;
        height: 250px;
        border: 2px dashed #999;
        border-radius: 5px;
        position: relative;
        overflow: hidden;
        background-color: #f8f8f8;
    }

    .image-placeholder {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        color: #888;
        font-size: 16px;
        text-align: center;
    }

    #previewImage {
        width: 100%;
        height: 100%;
        object-fit: cover; /* ใช้สัดส่วนเท่าเดิมแต่ตัดบางส่วนออกให้แสดงเท่ากันกรอบ    */
    }

    /*#previewImage {
        width: 100%;
        height: 250px;
        object-fit: contain;*/ /* ใช้สัดส่วนเท่าเดิม*/
    /*}*/

    .custom-modal {
        /* transform: translateY(-200px);*/
        transform: translateY(0vh);
    }
</style>

<div id="timeInModal"
     class="modal fade"
     data-bs-backdrop="static"
     data-bs-keyboard="false">

    <div class="modal-dialog">

        <div class="modal-content">

            <div class="modal-body text-center">
                <h1>
                    @If Not Model.IsTimeIn Then
                        @<span>Time In</span>
                    ElseIf Not Model.IsTimeOut Then
                        @<span>Time Out</span>
                    End if
                </h1>
            </div>


            <div class="text-center">

                @If Not Model.IsTimeIn Then
                    @<div>วันที่เข้างาน : @DateTime.Now.ToString("dd/MM/yyyy HH:mm")</div>
                ElseIf Not Model.IsTimeOut Then
                    @<div>วันที่เข้างาน : @Model.TimeInDate  @Model.TimeInTime</div>
                    @<div>วันที่ออกงาน : @DateTime.Now.ToString("dd/MM/yyyy HH:mm") น.</div>
                End if


            </div>


            <div class="modal-body container-sm">

                @If Not Model.IsTimeIn Then

                    @<div class="container" style="padding-bottom:5px;padding-top:5px;">
                        <div class="row">ประเภทรถ</div>
                        <div class="row">
                            <select id="ddlVehicletype" data-role="none"
                                    style="border: 1px solid #ddd; background-color: white; width: 100%!important; height: 40px">

                                <option value=""> --กรุณาเลือกประเภทรถ - -</option>
                                <option style="width:100% !important;" value="0">
                                    รถบริษัท
                                </option>
                                <option style="width:100% !important;" value="1">
                                    รถตนเอง
                                </option>

                            </select>
                        </div>
                    </div>

                    @<div id="vehicleSection" class="container" style="padding-bottom: 5px; padding-top: 5px; display: none;">
                        <div class="row">ทะเบียนรถ</div>
                        <div class="row">

                            <input type="text" data-role="none"
                                   id="txtVehicleno" style="display: none; border: 1px solid #ddd; background-color: white; width: 100%; height: 40px "
                                   placeholder="กรอกทะเบียนรถ" />

                            <select id="ddlVehicle" data-role="none"
                                    style="display: none; border: 1px solid #ddd; background-color: white; width: 100% !important; height: 40px">

                                <option value=""> --กรุณาเลือกทะเบียนรถ - -</option>

                                @If Model.VehicleList IsNot Nothing Then
                                    For Each item In Model.VehicleList
                                        @<option style="width:100% !important;" value="@item.Value">
                                            @item.Text
                                        </option>
                                    Next
                                End If

                            </select>
                        </div>
                    </div>

                    @<div class="container" style="padding-bottom:5px;padding-top:5px;">
                        <div class="row">เลขไมล์เริ่มต้น</div>
                        <div class="row">
                            <input type="number" data-role="none"
                                   id="txtOdometerStart" style="border: 1px solid #ddd; background-color: white; width: 100%; height: 40px "
                                   placeholder="กรอกเลขไมล์เริ่มต้น" />
                        </div>
                    </div>

                ElseIf Not Model.IsTimeOut Then

                    @<div class="container" style="padding-bottom:5px;padding-top:5px;">
                        <div class="row">ทะเบียนรถ</div>
                        <div class="row">
                            <input type="text" data-role="none"
                                   style="border: 1px solid #ddd; background-color: white; width: 100%; height: 40px "
                                   value="@Model.VehicleNo" disabled />
                        </div>
                    </div>

                    @<div class="container" style="padding-bottom:5px;padding-top:5px;">
                        <div class="row">เลขไมล์เริ่มต้น</div>
                        <div class="row">
                            <input type="number" id="txtTimeOutOdometerStart" data-role="none"
                                   style="border: 1px solid #ddd; background-color: white; width: 100%; height: 40px "
                                   value="@Model.OdometerStart" disabled />
                        </div>
                    </div>

                    @<div class="container" style="padding-bottom:5px;padding-top:5px;">
                        <div class="row">เลขไมล์หลังใช้</div>
                        <div class="row">
                            <input type="number" data-role="none"
                                   id="txtOdometerEnd" style="border: 1px solid #ddd; background-color: white; width: 100%; height: 40px "
                                   placeholder="กรอกเลขไมล์หลังใช้" />
                        </div>
                    </div>
                End if


                <br />

                <div style="display:flex; justify-content:space-between; align-items:center;">

                    <label style="margin:0;">
                        รูปภาพ
                    </label>

                    @*<label style="margin:0; cursor:pointer;" id="btnRetake">
                            🗑️ ล้างรูป
                        </label>*@

                    <a id="btnRetake" style="margin:0;"
                       href="#"
                       Class="ui-btn btn-style no-padding">

                        @*<img src="@(StaticRootImgs)/check-black.png" alt="ล้างรูป" Class="button-menu-icon" />*@
                        <span Class="button-menu-label">🗑️ ล้างรูป</span>

                    </a>


                </div>

                <div id="imageContainer" class="image-frame">

                    <div id="imagePlaceholder" class="image-placeholder">
                        <a href="#" style="text-decoration:none;"
                           id="btnOpenCamera"
                           Class="ui-btn btn-style no-padding"
                           onclick="ShowCameraModal()">

                            <img src="@(StaticRootImgs)/camera-black.png" alt="ล้างรูป" Class="button-menu-icon" />
                            @If Not Model.IsTimeIn Then
                                @<span class="button-menu-label">รูปไมล์รถก่อนใช้งาน</span>
                            ElseIf Not Model.IsTimeOut Then
                                @<span class="button-menu-label">รูปไมล์รถหลังใช้งาน</span>
                            End if

                        </a>
                    </div>

                    <img id="previewImage"
                         style="width: 100%; display: none; margin-top: 0px; " />
                </div>



                <div class="row">

                    @If Not Model.IsTimeIn Then

                        @*@<div Class="col-6 no-padding">
                                <Button id="btnSaveTimeIn" Class="ui-btn btn-confirm" style="height: 60px;" disabled>
                                    ยืนยัน
                                </Button>
                            </div>*@

                        @<a id="btnSaveTimeIn"
                            href="#"
                            Class="ui-btn btn-style col no-padding disabled-link">

                            <img src="@(StaticRootImgs)/check-black.png" alt="ยืนยัน" Class="button-menu-icon" />
                            <span Class="button-menu-label">ยืนยัน</span>

                        </a>

                    ElseIf Not Model.IsTimeOut Then

                        @*@<div Class="col-6 no-padding">
                                <Button id="btnSaveTimeOut" Class="ui-btn btn-confirm" style="height: 60px;" disabled>
                                    ยืนยัน
                                </Button>
                            </div>*@

                        @<a id="btnSaveTimeOut"
                            href="#"
                            Class="ui-btn btn-style col no-padding disabled-link">

                            <img src="@(StaticRootImgs)/check-black.png" alt="ยืนยัน" Class="button-menu-icon" />
                            <span Class="button-menu-label">ยืนยัน</span>

                        </a>

                    End if

                    @*<div Class="col-6 no-padding">
                            <Button onclick="CloseTimeInModal()" data-dismiss="modal" Class="ui-btn btn-cancel" style="height: 60px;">
                                ยกเลิก
                            </Button>
                        </div>*@

                    <a href="#" data-dismiss="modal"
                       class="ui-btn btn-style col no-padding"
                       onclick="CloseTimeInModal()">

                        <img src="@(StaticRootImgs)/back-black.png" alt="ปิด" class="button-menu-icon" />
                        <span class="button-menu-label">ปิด</span>

                    </a>

                </div>


            </div>



        </div>

    </div>

</div>

<div id="CameraModal"
     class="modal fade"
     data-bs-backdrop="static"
     data-bs-keyboard="false">

    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-body">
                <video id="video"
                       autoplay
                       playsinline
                       style="width:100%;height:100%;border:1px solid #ccc;">
                </video>
                <canvas id="canvas"
                        style="display:none;">
                </canvas>

                @*<button id="btnTakePhoto" Class="ui-btn btn-confirm" style="height: 60px;">
                        ถ่ายรูป
                    </button>
                    <button Class="ui-btn btn-cancel" style="height: 60px;"
                            data-dismiss="modal" onclick="CloseCameraModal()">
                        ยกเลิก
                    </button>*@

                <div class="row">
                    <a id="btnTakePhoto"
                       href="#" data-dismiss="modal"
                       class="ui-btn btn-style col no-padding">

                        <img src="@(StaticRootImgs)/camera-black.png" alt="ปิด" class="button-menu-icon" />
                        <span class="button-menu-label">ถ่ายรูป</span>

                    </a>

                    <a href="#"
                       class="ui-btn btn-style col no-padding"
                       data-dismiss="modal"
                       onclick="CloseCameraModal()">

                        <img src="@(StaticRootImgs)/back-black.png" alt="ปิด" class="button-menu-icon" />
                        <span class="button-menu-label">ยกเลิก</span>

                    </a>
                </div>
            </div>
        </div>
    </div>
</div>

<div id="AddClickModal"
     class="modal fade"
     data-bs-backdrop="static"
     data-bs-keyboard="false">

    <div class="modal-dialog custom-modal">
        <div class="modal-content">

            <div class="modal-body text-center">
                <h1>
                    บันทึกการทำงาน
                </h1>
            </div>

            <div class="modal-body">

                <div class="row g-0">
                    <div Class="col-4 no-padding">
                        <Button Class="ui-btn btn-deny ui-icon-user ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="location.href='@Url.Action("SelectFarmer")'">
                            ชาวไร่เดิม
                        </Button>
                    </div>
                    <div Class="col-4 no-padding">
                        <Button Class="ui-btn btn-confirm ui-icon-plus ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="CloseAddClickModal()">
                            ชาวไร่ใหม่
                        </Button>
                    </div>
                    <div Class="col-4 no-padding">
                        <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" data-dismiss="modal" style="height: 60px; padding-top: 25px !important;" onclick="CloseAddClickModal()">
                            ยกเลิก
                        </Button>
                    </div>


                </div>

            </div>
        </div>
    </div>
</div>


@*Show Popup Messgaebox*@
<div id="msgOverlay" class="msg-overlay">

    <div class="msg-box">

        <div id="msgTitle" class="msg-title">
            แจ้งเตือน
        </div>

        <div id="msgText"
             class="msg-text"
             style="white-space: pre-line;">
            ข้อความ
        </div>

        @*<button id="btnMsgOK"
                    class="msg-btn"
                    onclick="document.getElementById('msgOverlay').style.display='none';">
                ตกลง
            </button>*@

        <a id="btnMsgOK"
           href="#"
           class="ui-btn btn-style col no-padding"
           onclick="document.getElementById('msgOverlay').style.display='none';">

            <img src="@(StaticRootImgs)/back-black.png" alt="ปิด" class="button-menu-icon" />
            <span class="button-menu-label">ตกลง</span>

        </a>

    </div>

</div>
@*END Messagbox*@

@*Confirm Messagbox*@
<div id="confirmOverlay" class="msg-overlay">

    <div class="msg-box">

        <div id="confirmTitle" class="msg-title">
            ยืนยันรายการ
        </div>

        <div id="confirmText" class="msg-text">
        </div>

        <div class="confirm-buttons">

            @*<button id="btnConfirmYes" class="msg-btn">
                    ตกลง
                </button>

                <button id="btnConfirmNo" class="msg-btn btn-cancel">
                    ยกเลิก
                </button>*@

     

                <a id="btnConfirmYes"
                   href="#"
                   class="ui-btn btn-style col no-padding">

                    <img src="@(StaticRootImgs)/check-black.png" alt="ตกลง" class="button-menu-icon" />
                    <span class="button-menu-label">ตกลง</span>

                </a>
                <a id="btnConfirmNo"
                   href="#"
                   class="ui-btn btn-style col no-padding">

                    <img src="@(StaticRootImgs)/back-black.png" alt="ยกเลิก" class="button-menu-icon" />
                    <span class="button-menu-label">ยกเลิก</span>

                </a>
 


        </div>

    </div>

</div>
@*End Confirm Messagbox*@

@section Scripts
    <script src="~/Scripts/DailyWork/Index.js"></script> 
    <script>
        function goBack() {
            location.href = '@HomeURL'
        }

        /*submit TimeIn*/
        $("#btnSaveTimeIn").click(function () {

            //if ($("#btnSaveTimeIn").prop("disabled")) {
            //    return;
            //}

            if (!$("#ddlVehicletype").val()) {
                ShowMessage("กรุณาเลือกประเภทรถ!");
                return;
            }

            var vehicleType = $("#ddlVehicletype").val();

            if (vehicleType === "0") {

                // รถบริษัท
                if (!$("#ddlVehicle").val()) {
                    ShowMessage("กรุณาเลือกทะเบียนรถบริษัท!");
                    return;
                } else {
                    var vehicleno = $("#ddlVehicle").val();
                }

            }
            else if (vehicleType === "1") {

                // รถตนเอง
                if (!$("#txtVehicleno").val().trim()) {
                    ShowMessage("กรุณากรอกทะเบียนรถ!");
                    return;
                } else {
                    var vehicleno = $("#txtVehicleno").val();
                }

            }


            if (!$("#txtOdometerStart").val()) {
                ShowMessage("กรุณาระบุเลขไมล์!");
                return;
            }

            if (!$("#previewImage").attr("src")) {
                ShowMessage("กรุณาถ่ายรูปไมล์รถ!");
                return;
            }


            var formData = new FormData();

            formData.append("UserID", $("#txtUserID").val());

            formData.append("SalesmanCode", $("#txtSalesmanCode").val());

            formData.append("VehicleType", vehicleType);

            formData.append("VehicleNo", vehicleno);

            formData.append("OdometerStart", $("#txtOdometerStart").val());

            formData.append("GeoLocation", $("#txtgeolocation").val());

            formData.append("PhotoFile", photoBlob, "TimeIn.jpg");

            $.ajax({

                url: '@Url.Action("TimeInSave", "DailyWork")',

                type: 'POST',

                data: formData,

                processData: false,

                contentType: false,

                success: function (res) {

                    if (res.Success) {

                        /*  alert("Time In Completed");*/

                        $('#timeInModal')
                            .modal('hide');

                        location.reload(); /*โหลดหน้าเดิม คือเรียก action index()*/

                    }
                    else {
                        ShowMessage(res.Message, "มีข้อผิดพลาดเกิดขึ้น!");
                        /*alert(res.Message);*/

                    }

                },

                error: function (er) {

                    ShowMessage(er.Message, "Save Error.มีข้อผิดพลาดเกิดขึ้น!");
                    /*alert("Save Error");*/

                }

            });

        });

        /*submit TimeOut*/
        $("#btnSaveTimeOut").click(function () {


            //if ($("#btnSaveTimeIn").prop("disabled")) {
            //    return;
            //}


            var odometerStart = Number($("#txtTimeOutOdometerStart").val());
            var odometerEnd = Number($("#txtOdometerEnd").val());


            if (!odometerEnd) {
                /* alert('กรุณาระบุเลขไมล์หลังใช้');*/
                ShowMessage("กรุณาระบุเลขไมล์หลังใช้");
                return;
            }

            console.log(odometerStart);
            console.log(odometerEnd);


            if (odometerEnd < odometerStart) {
                /*  alert('เลขไมล์หลังใช้ต้องมากกว่าหรือเท่ากับเลขไมล์เริ่มต้น');*/
                ShowMessage("เลขไมล์หลังใช้ต้องมากกว่าหรือเท่ากับเลขไมล์เริ่มต้น");
                return;
            }

            if (!$("#previewImage").attr("src")) {
                /*  alert('กรุณาถ่ายรูปไมล์รถ');*/
                ShowMessage("กรุณาถ่ายรูปไมล์รถ");
                return;
            }

            /*  alert('formData');*/

            var formData = new FormData();

            formData.append(
                "SalesmanCode",
                $("#txtSalesmanCode").val());

            formData.append(
                "DocNumber",
                $("#txtDocumentNumber").val());

            formData.append(
                "UserID",
                $("#txtUserID").val());

            formData.append(
                "OdometerStart", odometerStart);

            formData.append(
                "OdometerEnd", odometerEnd);

            formData.append("GeoLocation", $("#txtgeolocation").val());


            formData.append(
                "PhotoFile",
                photoBlob,
                "TimeOut.jpg");

            $.ajax({

                url: '@Url.Action("TimeOutSave", "DailyWork")',

                type: 'POST',

                data: formData,

                processData: false,

                contentType: false,

                success: function (res) {

                    /* alert("before reload = " + res.Success);*/

                    if (res.Success) {

                        /*  alert("Time In Completed");*/

                        $('#timeInModal')
                            .modal('hide');

                        location.reload(); /*โหลดหน้าเดิม คือเรียก action index()*/

                    }
                    else {
                        ShowMessage(res.Message, "มีข้อผิดพลาดเกิดขึ้น!");
                        alert(res.Message);

                    }

                },

                error: function (er) {

                    ShowMessage(er.Message, "Save Error.มีข้อผิดพลาดเกิดขึ้น!");
                    /*alert("Save Error");*/

                }

            });

        });

    </script>
End Section



<!-- Bottom Buttons -->

<div class="button-menu-container">
    <div class="container">
        <div class="row g-0">
            @If Not Model.IsTimeIn Then

                @<a id="btnHome"
                    href="#"
                    class="ui-btn btn-style col no-padding"
                    onclick="goBack()">

                    <img src="@(StaticRootImgs)/home-black.png" alt="Home" class="button-menu-icon" />
                    <span class="button-menu-label">Home</span>

                </a>

                @<a id="btnTimeIn"
                    href="#"
                    class="ui-btn btn-style col no-padding"
                    onclick="goTimeIn()">

                    <img src="@(StaticRootImgs)/clock-black.png" alt="Time In" class="button-menu-icon" />
                    <span class="button-menu-label">Time In</span>

                </a>

            ElseIf Not Model.IsTimeOut Then

                @<a id="btnHome"
                    href="#"
                    class="ui-btn btn-style col no-padding"
                    onclick="goBack()">

                    <img src="@(StaticRootImgs)/home-black.png" alt="Home" class="button-menu-icon" />
                    <span class="button-menu-label">Home</span>

                </a>

                @<a id="btnFarmerList" style="display:@(If(Model.IsMustTimeOut, "none", "block"))"
                    href="#"
                    class="ui-btn btn-style col no-padding"
                    onclick="location.href='@Url.Action("SelectFarmer", New With {.FSMCODE = Model.SalesmanCode})'">

                    <img src="@(StaticRootImgs)/user-black.png" alt="ข้อมูลชาวไร่" class="button-menu-icon" />
                    <span class="button-menu-label">ข้อมูลชาวไร่</span>

                </a>

                @<a id="btnTimeOut"
                    href="#"
                    class="ui-btn btn-style col no-padding"
                    onclick="goTimeOut()">

                    <img src="@(StaticRootImgs)/clock-black.png" alt="Time Out" class="button-menu-icon" />
                    <span class="button-menu-label">Time Out</span>

                </a>
            Else

                'TimeIn&TimeOut ในวันแล้ว
                @<a id="btnHome"
                    href="#"
                    class="ui-btn btn-style col no-padding"
                    onclick="goBack()">

                    <img src="@(StaticRootImgs)/home-black.png" alt="Home" class="button-menu-icon" />
                    <span class="button-menu-label">Home</span>

                </a>
            End If

        </div>
    </div>
</div>

<div style="
    display:none;
    position:fixed;
    bottom:0;
    left:0;
    width:100%;
    padding-left:5px;
    padding-right:5px;
    background:#fff;
    border-top:1px solid #ddd;
    z-index:999;">
    <div class="container">
        <div class="row g-0">

            @If Not Model.IsTimeIn Then
                @<div Class="col no-padding">
                    <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack()">
                        Home
                    </Button>
                </div>

                @<div Class="col no-padding">
                    <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goTimeIn()">
                        Time In
                    </Button>
                </div>

            ElseIf Not Model.IsTimeOut Then

                @<div Class="col no-padding">
                    <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack()">
                        Home
                    </Button>
                </div>

                @<div Class="col no-padding"
                      style="display:@(If(Model.IsMustTimeOut, "none", "block"))">
                    <Button Class="ui-btn btn-deny ui-icon-user ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;"
                            onclick="location.href='@Url.Action("SelectFarmer", New With {.FSMCODE = Model.SalesmanCode})'">
                        ข้อมูลชาวไร่
                    </Button>
                </div>

                @<div Class="col no-padding">
                    <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goTimeOut()">
                        Time Out
                    </Button>
                </div>

            Else

                'TimeIn&TimeOut ในวันแล้ว

                @<div Class="col no-padding">
                    <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack()">
                        Home
                    </Button>
                </div>

            End If

        </div>
    </div>
</div>


<!-- End Bottom Buttons -->