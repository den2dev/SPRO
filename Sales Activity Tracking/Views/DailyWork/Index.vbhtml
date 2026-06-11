@ModelType DailyWorkViewModel
@Code
    ViewData("Title") = "Daily Report"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="container">

    <div class="panel panel-primary">

        <div class="panel-body">

            <div class="row" hidden>
                <div class="col-6 text-start">
                    <input type="text" id="txtSalesmanCode" value="@Session("userlogin")" />
                </div>
                <div class="col-6 text-start">
                    <input type="text" id="txtTimeInDocumentNumber" value=@Model.TimeInDocumentNumber />
                </div>
            </div>

            <div class="row">
                <div class="col-4 text-end">
                    พนักงานส่งเสริม :
                </div>
                <div class="col-8 text-start">
                    @Model.SalesmanCode @Model.SalesmanName
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
                    @Model.VehicleLicensePlate
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
        <div id="mainWorkItems"
             style="height:400px;overflow-y:auto;border:1px solid #ddd;padding:10px;">

            @If Model.WorkItems IsNot Nothing AndAlso Model.WorkItems.Any() Then


                @For Each item In Model.WorkItems

                    @<div class="card" style="margin-bottom:10px;
                            padding:10px;
                            border:1px solid #ddd;
                            border-radius:10px;">

                        <!-- Header -->
                        <div style="display:flex;justify-content:space-between;align-items:center;">
                            <strong>@item.ActivityName</strong>

                            <span style="
                                        color:red;
                                        cursor:pointer;
                                        font-size:16px;">
                                🗑️
                            </span>
                        </div>

                        <div style="margin-top:5px;">
                            เลขที่ : @item.ActivityNumber
                        </div>

                        <!-- Contact + Type -->
                        <div style="display:flex;
                                        justify-content:space-between;
                                        align-items:center;
                                        margin-top:5px;">

                            <span>@item.ContactName</span>

                            <span style="color:#666;">
                                @item.TypeContactName
                            </span>

                        </div>

                        <div style="margin-top:5px;">
                            ⏰ @item.CheckInDateTime - @item.CheckOutDateTime
                        </div>

                    </div>

                Next

            Else

                @<div style="height:100%;display:flex;align-items:center;justify-content:center;">
                    @If Not Model.IsTimeIn Then
                        @<span>กรุณาลงเวลาก่อน</span>
                    Else
                        @<span>ไม่พบข้อมูล</span>
                    End If
                </div>

            End If

        </div>
    </div>

    <div class="row g-0">

        @If Not Model.IsTimeIn Then
            @<div Class="col-6 no-padding">
                <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack()">
                    Back
                </Button>
            </div>

            @<div Class="col-6 no-padding">
                <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goTimeInOut()">
                    Time In
                </Button>
            </div>

        ElseIf Not Model.IsTimeOut Then

            @<div Class="col-4 no-padding">
                <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack()">
                    Back
                </Button>
            </div>

            @<div Class="col-4 no-padding">
                <Button Class="ui-btn btn-deny ui-icon-plus ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Add
                </Button>
            </div>

            @<div Class="col-4 no-padding">
                <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goTimeInOut()">
                    Time Out
                </Button>
            </div>

        Else

            'TimeIn&TimeOut ในวันแล้ว

            @<div Class="col-12 no-padding">
                <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack()">
                    Back
                </Button>
            </div>

        End If

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

                <button class="btn btn-danger"
                        data-dismiss="modal">

                    ปิด

                </button>

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
                    @<div>วันที่ออกงาน : @DateTime.Now.ToString("dd/MM/yyyy HH:mm")</div>
                End if

            </div>


            <div class="modal-body container-sm">

                @If Not Model.IsTimeIn Then
                    @<div class="row small">
                        <label>ทะเบียนรถ</label>
                        <select id="ddlVehicle" style="width:100% !important;">

                            <option value=""> --กรุณาเลือกทะเบียนรถ - -</option>

                            @If Model.VehicleList IsNot Nothing Then
                                For Each item In Model.VehicleList
                                    @<option value="@item.Value">
                                        @item.Text
                                    </option>
                                Next
                            End If

                        </select>
                    </div>

                    @<div class="row small">
                        <label>เลขไมล์เริ่มต้น</label>
                        <input type="number" id="txtOdometerStart" data-role="none" Class="formRow--input" style="width:100%" />
                    </div>

                ElseIf Not Model.IsTimeOut Then

                    @<div class="row small">
                        <label>ทะเบียนรถ</label>
                        <input type="text" value="@Model.VehicleLicensePlate" data-role="none" Class="formRow--input" style="width:100%" disabled />
                    </div>

                    @<div class="row small">
                        <label>เลขไมล์เริ่มต้น</label>
                        <input type="number" id="txtTimeOutOdometerStart" value=@Model.OdometerStart data-role="none" Class="formRow--input" style="width:100%" disabled />
                    </div>

                    @<div class="row small">
                        <label>เลขไมล์หลังใช้</label>
                        <input type="number" id="txtOdometerEnd" data-role="none" Class="formRow--input" style="width:100%" />
                    </div>

                End if


                <br />

                <div style="display:flex; justify-content:space-between; align-items:center;">

                    <label style="margin:0;">
                        รูปภาพ &#128247;
                    </label>

                    <label style="margin:0; cursor:pointer;" id="btnRetake">
                        🗑️ ล้างรูป
                    </label>

                </div>

                <div id="imageContainer" class="image-frame">

                    <div id="imagePlaceholder" class="image-placeholder">
                        <a href="#" style="text-decoration:none;"
                           id="btnOpenCamera"
                           onclick="ShowCameraModal()">

                            @If Not Model.IsTimeIn Then
                                @<span class="small">รูปไมล์รถก่อนใช้งาน &#128247;</span>
                            ElseIf Not Model.IsTimeOut Then
                                @<span class="small">รูปไมล์รถหลังใช้งาน &#128247;</span>
                            End if

                        </a>
                    </div>

                    <img id="previewImage"
                         style="width: 100%; display: none; margin-top: 0px; " />
                </div>

                <input type="hidden" id="Latitude" />
                <input type="hidden" id="Longitude" />

                <div class="row">

                    @If Not Model.IsTimeIn Then

                        @<div Class="col-6 no-padding">
                            <Button id="btnSaveTimeIn" Class="ui-btn btn-confirm" style="height: 60px;" disabled>
                                ยืนยัน
                            </Button>
                        </div>

                    ElseIf Not Model.IsTimeOut Then

                        @<div Class="col-6 no-padding">
                            <Button id="btnSaveTimeOut" Class="ui-btn btn-confirm" style="height: 60px;" disabled>
                                ยืนยัน
                            </Button>
                        </div>

                    End if

                    <div Class="col-6 no-padding">
                        <Button onclick="CloseTimeInModal()" data-dismiss="modal" Class="ui-btn btn-cancel" style="height: 60px;">
                            ยกเลิก
                        </Button>
                    </div>

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

                <button id="btnTakePhoto" Class="ui-btn btn-confirm" style="height: 60px;">
                    ถ่ายรูป
                </button>
                <button Class="ui-btn btn-cancel" style="height: 60px;"
                        data-dismiss="modal" onclick="CloseCameraModal()">
                    ยกเลิก
                </button>
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

        <div id="msgText" class="msg-text">
            ข้อความ
        </div>

        <button id="btnMsgOK" class="msg-btn">
            ตกลง
        </button>

    </div>

</div>
@*END Messagbox*@

@section Scripts

    <script>

        function goBack() {
                location.href='@Url.Action("Logout", "Home")'
        }

        function goTimeInOut() {
             
            /*clear ค่าเดิม*/
            photoBlob = null; 
            $("#previewImage").hide().attr("src", "");
            $("#imagePlaceholder").show();
            $("#Latitude").val("");
            $("#Longitude").val("");
            $("#txtOdometerStart,#txtOdometerEnd").val("");
            /*end clear ค่าเดิม*/

            $('#gpsLoadingModal').modal('show');

            navigator.geolocation.getCurrentPosition(

                function (position) {

                    $("#Latitude")
                        .val(position.coords.latitude);

                    $("#Longitude")
                        .val(position.coords.longitude);

                    $('#gpsLoadingModal').modal('hide');

                    $('#timeInModal').modal('show');

                },

                function (error) {

                    $('#gpsLoadingModal').modal('hide');

                    $('#gpsErrorModal').modal('show');

                },

                {
                    enableHighAccuracy: true,
                    timeout: 15000,
                    maximumAge: 0
                }
            );
        }

        function getLocation() {

            navigator.geolocation.getCurrentPosition(

                function (position) {

                    $("#Latitude").val(position.coords.latitude);
                    $("#Longitude").val(position.coords.longitude);

                    $("#lblLat").text(position.coords.latitude);
                    $("#lblLng").text(position.coords.longitude);

                },

                function () {

                    alert("กรุณาเปิด GPS ก่อน");

                }

            );

        }

        function CloseTimeInModal() {
            $('#gpsLoadingModal').modal('hide');
            $('#timeInModal').modal('hide');
        }
        function ShowCameraModal() {
            StartCamera();
            $('#timeInModal').modal('hide');
            $('#CameraModal').modal('show');
        }

        function CloseCameraModal() {
            $('#timeInModal').modal('show');
            $('#CameraModal').modal('hide');
        }


        let photoBlob = null;

        async function StartCamera() {

            const stream =
                await navigator.mediaDevices.getUserMedia({

                    video: {
                        facingMode: "environment"
                    }

                });

            document
                .getElementById("video")
                .srcObject = stream;

        }

        $("#btnTakePhoto").click(function () {

            let video = document.getElementById("video");
            let canvas = document.getElementById("canvas");

            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;

            let ctx = canvas.getContext("2d");
            ctx.drawImage(video, 0, 0);
            canvas.toBlob(function (blob) {

                photoBlob = blob;

                $("#previewImage")
                    .show()
                    .attr(
                        "src",
                        URL.createObjectURL(blob));

                ValidateTimeIn();
                ValidateTimeOut();

                $("#imagePlaceholder").hide();
                $('#CameraModal').modal('hide');
                $('#timeInModal').modal('show');

            }, "image/jpeg", 0.9);

        });

        $("#btnRetake").click(function () {

            photoBlob = null;

            $("#previewImage")
                .hide()
                .attr("src", "");

            $("#imagePlaceholder").show();

            ValidateTimeIn();
            ValidateTimeOut();

        });

        $("#btnSaveTimeIn").click(function () {

            //if ($("#btnSaveTimeIn").prop("disabled")) {
            //    return;
            //}

            if (!$("#ddlVehicle").val()) {
                ShowMessage("กรุณาเลือกทะเบียนรถ");
                return;
            }

            if (!$("#txtOdometerStart").val()) {
                ShowMessage("กรุณาระบุเลขไมล์");
                return;
            }

            if (!$("#previewImage").attr("src")) {
                ShowMessage("กรุณาถ่ายรูปไมล์รถ");
                return;
            }


            var formData = new FormData();

            formData.append(
                "SalesmanCode",
                $("#txtSalesmanCode").val());

            formData.append(
                "VehicleLicensePlate",
                $("#ddlVehicle").val());

            formData.append(
                "OdometerStart",
                $("#txtOdometerStart").val());

            formData.append(
                "Latitude",
                $("#Latitude").val());

            formData.append(
                "Longitude",
                $("#Longitude").val());

            formData.append(
                "PhotoFile",
                photoBlob,
                "TimeIn.jpg");

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
                        ShowMessage(res.Message,"มีข้อผิดพลาดเกิดขึ้น!");
                        /*alert(res.Message);*/

                    }

                },

                error: function (er) {

                    ShowMessage(er.Message, "Save Error.มีข้อผิดพลาดเกิดขึ้น!");
                    /*alert("Save Error");*/

                }

            });

        });


        $("#btnSaveTimeOut").click(function () {

              
           //if ($("#btnSaveTimeIn").prop("disabled")) {
           //    return;
           //}


            var odometerStart = Number($("#txtTimeOutOdometerStart").val());
            var odometerEnd = Number($("#txtOdometerEnd").val());
    

            if (!odometerEnd) {
                alert('กรุณาระบุเลขไมล์หลังใช้');
                ShowMessage("กรุณาระบุเลขไมล์หลังใช้");
                return;
            }

            if (odometerEnd < odometerStart) {
                alert('เลขไมล์หลังใช้ต้องมากกว่าหรือเท่ากับเลขไมล์เริ่มต้น');
                ShowMessage("เลขไมล์หลังใช้ต้องมากกว่าหรือเท่ากับเลขไมล์เริ่มต้น");
                return;
            }

            if (!$("#previewImage").attr("src")) {
                alert('กรุณาถ่ายรูปไมล์รถ');
                ShowMessage("กรุณาถ่ายรูปไมล์รถ");
                return;
            }

            alert('formData');

           var formData = new FormData();

           formData.append(
               "SalesmanCode",
               $("#txtSalesmanCode").val());

           formData.append(
               "TimeInDocumentNumber",
               $("#txtTimeInDocumentNumber").val());


            formData.append(
                "OdometerStart", odometerStart);

           formData.append(
               "OdometerEnd", odometerEnd);

            formData.append(
                "Latitude",
               $("#Latitude").val());

           formData.append(
               "Longitude",
               $("#Longitude").val());

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

                   alert("before reload = " + res.Success);

                   if (res.Success) {

                       $('#timeInModal')
                           .modal('hide');

                       alert("reload");

                       location.reload(); /*โหลดหน้าเดิม คือเรียก action index()*/

                   }
                   else {
                       ShowMessage(res.Message,"มีข้อผิดพลาดเกิดขึ้น!");
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


    @*Validate TimeIn*@
    <script>
        function ValidateTimeIn() {

            var vehicle = $("#ddlVehicle").val();
            var odometer = $("#txtOdometerStart").val();
            var hasImage = $("#previewImage").attr("src");

            var isValid =
                vehicle &&
                odometer &&
                odometer > 0 &&
                hasImage;

            $("#btnSaveTimeIn").prop("disabled", !isValid);

        }

        $("#ddlVehicle").change(function () {

            ValidateTimeIn();

        });
        $("#txtOdometerStart").on("input", function () {

            ValidateTimeIn();

        });
    </script>

    @*Validate TimeOut*@
    <script>
        function ValidateTimeOut() {

            var odometer = $("#txtOdometerEnd").val();
            var hasImage = $("#previewImage").attr("src");

            var isValid =
                odometer &&
                odometer > 0 &&
                hasImage;

            $("#btnSaveTimeOut").prop("disabled", !isValid);

        }


        $("#txtOdometerEnd").on("input", function () {

            ValidateTimeOut();

        });
    </script>


End Section
