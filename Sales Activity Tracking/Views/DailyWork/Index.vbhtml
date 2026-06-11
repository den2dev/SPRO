@ModelType DailyWorkViewModel
@Code
    ViewData("Title") = "Daily Report"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

SessionSaleCode : @Session("salescode")
<br>
TempLogin : @Session("Temp_login")
<br>

<div class="container">

    <div class="panel panel-primary">

        <div class="panel-body">

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

        @If Not Model.IsTimeIn Then
            @<div Class="col-6 no-padding">
                <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack()">
                    Back
                </Button>
            </div>

            @<div Class="col-6 no-padding">
                <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goTimeIn()">
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
                <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Time Out
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
                    Time In
                </h1>
            </div>
            <div class="text-center">
                <input type="text" id="txtSalesmanCode" value="@Session("Temp_login")" />
                <label>วันที่เข้าใช้งาน : @DateTime.Now.ToString("dd/MM/yyyy HH:mm")</label>
            </div>

            <div class="modal-body">
                 
                <label>ทะเบียนรถ</label>  
                <select id="ddlVehicle" style="width:100% !important;">

                    <option value="">-- กรุณาเลือกทะเบียนรถ --</option>

                    @If Model.VehicleList IsNot Nothing Then

                        For Each item In Model.VehicleList

                            @<option value="@item.Value">
                                @item.Text
                            </option>

                        Next

                    End If

                </select>

                <div>
                    <label>เลขไมล์</label>
                    <input type="number" id="txtOdometer" data-role="none" class="formRow--input" style="width:100%" />
                </div>

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
                            รูปไมล์รถก่อนใช้งาน &#128247;
                        </a>
                    </div>

                    <img id="previewImage"
                         style="width: 100%; display: none; margin-top: 0px; " />
                </div>

                <input type="hidden" id="Latitude" />
                <input type="hidden" id="Longitude" />

                <div class="row">

                    <div Class="col-6 no-padding">
                        <Button id="btnSaveTimeIn" Class="ui-btn btn-confirm" style="height: 60px;" disabled>
                            ยืนยัน
                        </Button>
                    </div>

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

@section Scripts

    <script>

        function goBack() {
                location.href='@Url.Action("Index", "Home")'
        }

        function goTimeIn() {

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

        });

        $("#btnSaveTimeIn").click(function () {

            //if ($("#btnSaveTimeIn").prop("disabled")) {
            //    return;
            //}

            if (!$("#ddlVehicle").val()) {
                ShowMessage("กรุณาเลือกทะเบียนรถ");
                return;
            }

            if (!$("#txtOdometer").val()) {
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
                $("#txtOdometer").val());

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

                        alert(res.Message);

                    }

                },

                error: function () {

                    alert("Save Error");

                }

            });

        });

    </script>


    @*Validate*@
    <script>
        function ValidateTimeIn() {

            var vehicle = $("#ddlVehicle").val();
            var odometer = $("#txtOdometer").val();
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
        $("#txtOdometer").on("input", function () {

            ValidateTimeIn();

        });
    </script>

End Section