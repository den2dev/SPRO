@Modeltype VisitFarmerEditModeViewModel
@Code
    ViewData("Title") = "ตรวจเยี่ยมชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code

<link href="~/Content/DailyWork/photo.css" rel="stylesheet" />


<style>
    #mainItems {
        height: calc(100vh - 124px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }

    #photoContainer {
        height: calc(100vh - 398px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }
    #mainQues {
        height: calc(100vh -100px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 0px;
    }

    .custom-input {
        border: 1px solid #ddd;
        background-color: white;
        width: 100%;
        height: 40px;
    }
</style>

<input type="hidden" id="txtgeolocation" />

<div class="row g-0" style="padding-top:66px">

    <div id="mainItems">


        <!-- Farmer Info -->
        <div class="card"
             style="margin:10px;padding:12px;border:1px solid #ddd;border-radius:2px;">


            <div style="display:flex;justify-content:space-between;align-items:center;">
                <div>
                    <strong>
                        @(If(Model.Farmer.IsNewFarmer, "ชาวไร่ใหม่", Model.Farmer.FarmerCode))
                        - @Model.Farmer.FarmerName
                    </strong>
                </div>

                <span id="btnDeleteAtivity"
                      onclick="DeleteAtivityItem('@Model.ActivityItem.ActivityNumber')"
                      style="color:red;cursor:pointer;font-size:16px;">
                    🗑️
                </span>
            </div>
            <div>
                เบอร์โทร. @Model.Farmer.MobileNo
            </div>

            <hr />


            <div id="farmerHeader"
                 style="cursor:pointer;">
        
                <input type="hidden" id="ActivityNo" value="@Model.ActivityItem.ActivityNumber"/>
                <span id="collapsefarmerdetail" style="font-weight:bold;color:#0d6efd;"> ▼ รายละเอียดชาวไร่</span>

            </div>

            <div id="farmerDetail"
                 style="display:none;">

                <div class="container" style="padding-bottom:5px;padding-top:5px;">
                    <div class="row">บ้านเลขที่</div>
                    <div class="row">
                        <input type="text" value="@Model.Farmer.AddressNo"
                               data-role="none" class="custom-input" disabled />

                    </div>
                </div>
                <div class="container" style="padding-bottom:5px;padding-top:5px;">
                    <div class="row">หมู่ที่</div>
                    <div class="row">
                        <input type="text" value="@Model.Farmer.Moo"
                               data-role="none" class="custom-input" disabled />

                    </div>
                </div>
                <div class="container" style="padding-bottom:5px;padding-top:5px;">
                    <div class="row">ชื่อหมู่บ้าน</div>
                    <div class="row">
                        <input type="text"
                               value="@Model.Farmer.VillageName"
                               data-role="none" class="custom-input" disabled />
                    </div>
                </div>
                <div class="container" style="padding-bottom:5px;padding-top:5px;">
                    <div class="row">ตำบล</div>
                    <div class="row">
                        <input type="text"
                               value="@Model.Farmer.SubDistrict"
                               data-role="none"
                               class="custom-input" disabled />
                    </div>
                </div>
                <div class="container" style="padding-bottom:5px;padding-top:5px;">
                    <div class="row">อำเภอ</div>
                    <div class="row">
                        <input type="text"
                               value="@Model.Farmer.District"
                               data-role="none"
                               class="custom-input" disabled />
                    </div>
                </div>
                <div class="container" style="padding-bottom:5px;padding-top:5px;">
                    <div class="row">จังหวัด</div>
                    <div class="row">
                        <input type="text"
                               value="@Model.Farmer.Province"
                               data-role="none"
                               class="custom-input" disabled />
                    </div>
                </div>
                <div class="container" style="padding-bottom:5px;padding-top:5px;">
                    <div class="row">เลขที่สัญญาเดิม</div>
                    <div class="row">
                        <input type="text"
                               value="@Model.Farmer.ContractNo"
                               data-role="none"
                               class="custom-input" disabled />
                    </div>
                </div>







            </div>

        </div>

        <!-- Questionnaire -->
        @*@Html.Partial("_Questionnaire", Model.Questionnaire)*@


        <div class="row g-0" style="margin-left: 18px; margin-right: 18px">

            @If Not Model.ActivityItem.IsCheckOut Then

                @<div>
                    <div class="row g-0">
                        <a href="#"
                           id="btnTakePhoto"
                           class="ui-btn btn-style col no-padding">

                            <img src="@(StaticRootImgs)/camera-black.png" alt="Back" class="button-menu-icon" />
                            <span class="button-menu-label">เพิ่มรูป</span>

                        </a>

                    </div>
                </div>

            Else

                @<div style="
                height:58px;
                display:flex;
                align-items:center;
                justify-content:center;
                color:#6c757d;
                font-style:italic;">

                    ไม่สามารถเพิ่มรูปได้ (Check Out แล้ว)

                </div>

            End If
        </div>

        <div class="row g-0">  </div>

        <div id="photoContainer" style="padding-left: 10px; padding-right: 15px; padding-top:15px">

            @Html.Partial(
                           "_PhotoList",
                           Model.Photos)

        </div>


        <div class="container">
            <div class="row g-0">
                <div class="col">

                    <!--
                    <a id="btnQuesn"
                        href="#"-->
                    @*onclick="location.href='@Url.Action("Questionnaire", New With {.fiano = Model.ActivityItem.ActivityNumber, .isnewfarmer = Model.Farmer.IsNewFarmer.ToString, .fcontcode = Model.Farmer.FarmerCode, .fqesntype = Model.Farmer.NewFarmerType})'"*@
                    <!--onclick="openQuestionaire()"
                        class="ui-btn btn-style col no-padding">

                         <img src="@(StaticRootImgs)/bullets-black.png" alt="เปิดแบบสอบถาม" class="button-menu-icon" />
                         <span class="button-menu-label">
                             แบบสอบถาม
                         </span>
                     </a>
                    -->
                    <button onclick="openQuestionaire()" class="ui-btn btn-style col no-padding">

                        <img src="@(StaticRootImgs)/bullets-black.png" alt="เปิดแบบสอบถาม" class="button-menu-icon" />
                        <span class="button-menu-label">
                            แบบสอบถาม
                        </span>
                    </button>

                </div>
            </div>
        </div>

    </div>

</div>

@*ปุ่ม แบบสอบถาม*@
<style>
    .mobile-btn {
        width: 100%;
        height: 60px;
        border: none;
        background: #0d6efd;
        color: white;
        border-radius: 8px;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        font-size: 14px;
    }

        .mobile-btn .icon {
            font-size: 18px;
            line-height: 18px;
        }

        .mobile-btn .text {
            font-size: 12px;
        }

        .mobile-btn:active {
            transform: scale(0.98);
        }
</style>

@*Show Popup แบบสอบถาม*@ 
<div class="modal fade"
     id="QnModal"
     tabindex="-1"> 
    <div class="modal-dialog modal-fullscreen">

        <div class="modal-content bg-dark">

            <div class="modal-body p-0">
                @*<divid="mainQues"  style="height:100%">

        </div>*@
                <iframe src="@ViewBag.QuestionnaireUrl" style="height: 100%;width: 100%; padding-top: 0px ">
                </iframe>

                <div class="button-menu-container">
                    <div class="container">
                        <div class="row g-0">

                            <a href="#"
                               class="ui-btn btn-style col no-padding"
                               data-bs-dismiss="modal">

                                <img src="@(StaticRootImgs)/back-black.png" alt="ปิด" class="button-menu-icon" />
                                <span class="button-menu-label">Close</span>

                            </a>

                        </div>
                    </div>
                </div>


            </div>


        </div>

    </div>

</div> 
@*END Popup แบบสอบถาม*@
 

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


                <a data-dismiss="modal"
                   href="#"
                   onclick="$('#gpsErrorModal').modal('hide');"
                   class="ui-btn btn-style col no-padding">

                    <img src="@(StaticRootImgs)/info-black.png" alt="close" class="button-menu-icon" />
                    <span class="button-menu-label">
                        ปิด
                    </span>
                </a>

            </div>

        </div>

    </div>

</div>

@*Show ShowLoading*@
<div id="loadingOverlay" class="loading-overlay">

    <div class="loading-box">

        <div class="loading-spinner"></div>

        <div id="loadingText" class="loading-text">
            กำลังประมวลผล...
        </div>
    </div>
</div>
@*END Show ShowLoading*@


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
           onclick="document.getElementById('msgOverlay').style.display='none';"
           class="ui-btn btn-style col no-padding">

            <img src="@(StaticRootImgs)/info-black.png" alt="close" class="button-menu-icon" />
            <span class="button-menu-label">
                ตกลง
            </span>
        </a>

    </div>

</div>
@*END Messagbox*@



<!-- Bottom Buttons -->
<div class="button-menu-container">
    <div class="container">
        <div class="row g-0">

            <a href="#"
               class="ui-btn btn-style col no-padding"
               onclick="location.href='@Url.Action("index")'">

                <img src="@(StaticRootImgs)/tag-black.png" alt="Activities" class="button-menu-icon" />
                <span class="button-menu-label">Activities</span>

            </a>


            <a id="btnGotoPhotoList" style="display:none"
               href="#"
               onclick="location.href='/DailyWork/VisitFarmerPhoto?activityNo=@Model.ActivityItem.ActivityNumber&ischeckout=@Model.ActivityItem.IsCheckOut.ToString';"
               class="ui-btn btn-style col no-padding">

                <img src="@(StaticRootImgs)/camera-black.png" alt="รูป" class="button-menu-icon" />
                <span class="button-menu-label">
                    @If Not Model.ActivityItem.IsCheckOut Then
                        @<span>ถ่ายรูป</span>
                    Else
                        @<span>รูปถ่าย</span>
                    End If
                </span>

            </a>

            @If Not Model.ActivityItem.IsCheckOut Then

                @<a id="btnCheckout"
                    href="#"
                    onclick="CallCheckOut('@Model.ActivityItem.ActivityNumber','@Model.Farmer.FarmerName')"
                    class="ui-btn btn-style col no-padding">

                    <img src="@(StaticRootImgs)/location-black.png" alt="Check Out" class="button-menu-icon" />
                    <span class="button-menu-label">
                        Check Out
                    </span>
                </a>

            End If

        </div>
    </div>
</div>

 

<!-- End Bottom Buttons -->
@*Confirm Messagbox*@
<div id="confirmOverlay" class="msg-overlay">

    <div class="msg-box">

        <div id="confirmTitle" class="msg-title">
            ยืนยันรายการ
        </div>

        <div id="confirmText" class="msg-text" style="white-space: pre-line;">
        </div>

        <div class="confirm-buttons">

            @*<button id="btnConfirmYes" class="msg-btn">
                    ตกลง
                </button>*@

            @*<button id="btnConfirmNo" class="msg-btn btn-cancel">
                ยกเลิก
                </button>*@

            <a id="btnConfirmYes"
               href="#"
               class="ui-btn btn-style col no-padding">

                <img src="@(StaticRootImgs)/check-black.png" alt="Ok" class="button-menu-icon" />
                <span class="button-menu-label">ตกลง</span>

            </a>

            <a id="btnConfirmNo"
               href="#"
               class="ui-btn btn-style col no-padding">

                <img src="@(StaticRootImgs)/back-black.png" alt="Cancel" class="button-menu-icon" />
                <span class="button-menu-label">ยกเลิก</span>

            </a>

        </div>

    </div>

</div>
@*End Confirm Messagbox*@

@section Scripts
    <script>

        function openQuestionaire() {
            $('#QnModal').modal('show');
        }
       

        $(function () {

            $("#farmerHeader").click(function () {

                $("#farmerDetail").slideToggle(function () {

                    if ($(this).is(":visible")) {

                        $("#collapsefarmerdetail").text("▲ ซ่อนรายละเอียดชาวไร่");

                    } else {

                        $("#collapsefarmerdetail").text("▼ รายละเอียดชาวไร่");

                    }
                });
            });




        });


        $("#btnDelete").on("click", function (e) {
            e.preventDefault();
        });

    </script>


    @*DeleteAtivityItem*@
    <script>

        function DeleteAtivityItem(doc) {

            //alert("DeleteAtivityItem " + doc);
            console.log("DeleteAtivityItem " + doc);

            ShowConfirm(

                "ต้องการลบข้อมูลนี้ " + doc + " หรือไม่ ?",

                function () {

                    console.log("ลบรายการ " + doc);

                    /*     alert("ลบรายการ");*/

                    $.ajax({
                        url: '/DailyWork/DeleteActivity',
                        type: 'POST',
                        data: {
                            activityNo: doc
                        },
                        success: function (res) {

                            if (res.Success) {

                                /*  alert("reload"); */
                                /*  location.reload(); *//*โหลดหน้าเดิม คือเรียก action index()*/
                                window.location.href = res.RedirectUrl;

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
                },

                function () {
                    console.log("ยกเลิก " + doc);
                    /*  alert("ยกเลิก " + doc);*/

                },

                "ยืนยันการลบ"

            );

        }

    </script>

    @*CheckOut*@
    <script>

        function CallCheckOut(fiano, farmername){

            ShowConfirm(

                farmername +
                "\n\n ใช่หรือไม่ ?",

                function () {

                    console.log("CallCheckOut " + farmername);

                    CreateCheckOut(fiano);
                },

                function () {
                    console.log("click ยกเลิก ");
                },

                "Check Out?"

            );

        }

        function CreateCheckOut(fiano) {

            $("#txtgeolocation").val("");

            $('#gpsLoadingModal').modal('show');


   /*         alert('farmer code:' + farmercode);*/

            navigator.geolocation.getCurrentPosition(

                function (position) {

                    var lat = position.coords.latitude;
                    var lng = position.coords.longitude;

                    var latDirection = lat >= 0 ? "N" : "S";
                    var lngDirection = lng >= 0 ? "E" : "W";

                    var gpsText =
                        latDirection + Math.abs(lat) +
                        " " +
                        lngDirection + Math.abs(lng);

                        $("#txtgeolocation").val(gpsText);

                        ShowLoading("Checking Out...");
                        $('#gpsLoadingModal').modal('hide');

                        var formData = new FormData();
                        formData.append("fiano", fiano);
                        formData.append( "GeoLocation", $("#txtgeolocation").val());

                        $.ajax({

                            url: '@Url.Action("CheckOutSave", "DailyWork")',

                            type: 'POST',

                            data: formData,

                            processData: false,

                            contentType: false,

                            success: function (res) {

                                if (res.Success) {
                                    /*  alert("Time In Completed"); */

                                    window.location.href = res.RedirectUrl;


                                }
                                else {

                                    ShowMessage(res.Message,"มีข้อผิดพลาดเกิดขึ้น!");
                                    /*alert(res.Message);*/
                                    $('#gpsLoadingModal').modal('hide');
                                }

                            },

                            error: function (er) {

                                ShowMessage(er.Message, "Save Error.มีข้อผิดพลาดเกิดขึ้น!");

                                $('#gpsLoadingModal').modal('hide');

                            }

                        });

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

    </script>


    @*Photo list*@
    <script>

        loadPhotos();

        let stream;
        async function startCamera() {

            stream = await navigator.mediaDevices.getUserMedia({

                video: {

                    facingMode: "environment"

                },

                audio: false

            });

            document
                .getElementById("cameraVideo")
                .srcObject = stream;

        }
        function stopCamera() {

            if (!stream)
                return;

            stream.getTracks().forEach(function (t) {

                t.stop();

            });

        }
        $("#btnTakePhoto").click(async function () {

            var modal =
                new bootstrap.Modal(
                    document.getElementById("cameraModal")
                );

            modal.show();

            await startCamera();

        });
        $("#btnCapture").click(function () {

            var video =
                document.getElementById("cameraVideo");

            var canvas =
                document.getElementById("cameraCanvas");

            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;

            var ctx =
                canvas.getContext("2d");

            ctx.drawImage(
                video,
                0,
                0
            );

            canvas.toBlob(function (blob) {

                uploadBlob(blob);

            }, "image/jpeg", 0.9);

        });

        function uploadBlob(blob) {

            var formData =
                new FormData();

            formData.append(
                "activityNo",
                $("#ActivityNo").val()
            );

            formData.append(
                "file0",
                blob,
                "photo.jpg"
            );

            $.ajax({

                url: "/DailyWork/UploadPhoto",

                type: "POST",

                data: formData,

                processData: false,

                contentType: false,

                success: function (r) {

                    stopCamera();

                    bootstrap.Modal
                        .getInstance(
                            document.getElementById("cameraModal")
                        )
                        .hide();

                    loadPhotos();

                }

            });

        }
        $("#cameraModal").on(
            "hidden.bs.modal",
            function () {

                stopCamera();

            });



        $(function () {

            // กดปุ่มเพิ่มรูป
            //$("#btnTakePhoto").click(function () {

            //    $("#photoUpload").click();

            //});

            // เลือกรูปเสร็จ
            $("#photoUpload").change(function () {

                if (this.files.length === 0)
                    return;

                var formData = new FormData();

                formData.append(
                    "activityNo",
                    $("#ActivityNo").val()
                );

                for (var i = 0; i < this.files.length; i++) {

                    formData.append(
                        "file" + i,
                        this.files[i]
                    );

                }

                $.ajax({

                    url: '/DailyWork/UploadPhoto',

                    type: 'POST',

                    data: formData,

                    processData: false,

                    contentType: false,

                    success: function (r) {

                        if (r.success) {

                            loadPhotos();

                            // reset file input
                            $("#photoUpload").val("");

                        }
                        else {

                            alert(r.message);

                        }

                    },

                    error: function () {

                        alert("Upload ไม่สำเร็จ");

                    }

                });

            });

        });

        function loadPhotos() {

            $("#photoContainer").load(
                '/DailyWork/PhotoList?activityNo=' +
                $("#ActivityNo").val()
            );

        }

        // ใช้ Event Delegate เพราะปุ่มลบถูกสร้างใหม่หลัง Ajax Load
        $(document).on(
            "click",
            ".btnDeletePhoto",
            function () {

                if (!confirm("ต้องการลบรูปนี้ใช่หรือไม่ ?"))
                    return;

                var fileName =
                    $(this).data("file");

                $.post(
                    '/DailyWork/DeletePhoto',
                    {
                        fileName: fileName
                    },
                    function (r) {

                        if (r.success) {

                            loadPhotos();

                        }
                        else {

                            alert(r.message);

                        }

                    }
                );

            }
        );


        $(document).on(
            "click",
            ".photo-preview",
            function () {

                $("#modalImage")
                    .attr("src",
                        $(this).data("src"));

                var modal =
                    new bootstrap.Modal(
                        document.getElementById("photoModal")
                    );

                modal.show();

            });

    </script>

End Section





@*Photo list*@
<div class="modal fade"
     id="cameraModal"
     tabindex="-1">

    <div class="modal-dialog modal-fullscreen">

        <div class="modal-content bg-dark">

            <div class="modal-body p-0">

                <video id="cameraVideo"
                       autoplay
                       playsinline
                       style="width:100%;height:97%;object-fit:cover;">
                </video>

                <canvas id="cameraCanvas"
                        style="display:none;">
                </canvas>


                <div class="button-menu-container">
                    <div class="container">
                        <div class="row g-0">

                            <a id="btnCapture" href="#"
                               class="ui-btn btn-style col no-padding">

                                <img src="@(StaticRootImgs)/camera-black.png" alt="Capture" class="button-menu-icon" />
                                <span class="button-menu-label">ถ่ายรูป</span>

                            </a>

                            <a href="#"
                               class="ui-btn btn-style col no-padding"
                               data-bs-dismiss="modal">

                                <img src="@(StaticRootImgs)/back-black.png" alt="Back" class="button-menu-icon" />
                                <span class="button-menu-label">Back</span>

                            </a>

                        </div>
                    </div>
                </div>


            </div>


        </div>

    </div>

</div>
