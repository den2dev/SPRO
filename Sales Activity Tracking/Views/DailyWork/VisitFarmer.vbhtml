@Modeltype VisitFarmerEditModeViewModel
@Code
    ViewData("Title") = "ตรวจเยี่ยมชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code



<style>
    #mainItems {
        height: calc(100vh - 139px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }

    .custom-input {
        border: 1px solid #ddd;
        background-color: white;
        width: 100%;
        height: 40px;
    }
</style>

<input type="hidden" id="txtgeolocation" />

<div class="row g-0">

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


        <div class="container">
            <div class="row g-0">
                <div class="col">

                    @*<button id="btnQuesn"
                            type="button"
                            class="mobile-btn"
                            onclick="location.href='@Url.Action("Questionnaire", New With {.fiano = Model.ActivityItem.ActivityNumber, .fcontcode = Model.Farmer.FarmerCode, .fqesntype = 1})'">

                        <div class="icon">📋</div>
                        <div class="text">แบบสอบถาม</div>

                    </button>*@


                    <a id="btnQuesn"
                       href="#"
                       onclick="location.href='@Url.Action("Questionnaire", New With {.fiano = Model.ActivityItem.ActivityNumber, .isnewfarmer = Model.Farmer.IsNewFarmer.ToString, .fcontcode = Model.Farmer.FarmerCode, .fqesntype = Model.Farmer.NewFarmerType})'"
                       class="ui-btn btn-style col no-padding">

                        <img src="@(StaticRootImgs)/bullets-black.png" alt="เปิดแบบสอบถาม" class="button-menu-icon" />
                        <span class="button-menu-label">
                            แบบสอบถาม
                        </span>
                    </a>


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


                <a  data-dismiss="modal"
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

          
            <a  id="btnCapture"
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

                @<a 
                    id="btnCheckout"
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

<div style="display:none;
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

            <div class="col">
                <Button id="btnIndex" type="button"
                        onclick="location.href='@Url.Action("index")'"
                        class="ui-btn btn-cancel ui-icon-back  ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;">
                    Activities
                </Button>
            </div>

            <div class="col"
                 style="display:none">
                <Button id="btnBack"
                        class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;"
                        onclick="history.back();">
                    Back
                </Button>
            </div>
            <div class="col"
                 style="display:none">
                <Button id="btnDelete" type="button"
                        class="ui-btn btn-cancel ui-icon-delete ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;"
                        onclick="location.href='@Url.Action("VisitItemDelete", New With {.fiano = Model.ActivityItem.ActivityNumber})'">
                    ลบ
                </Button>
            </div>

            <div class="col">
                <Button id="btnCapture" type="button"
                        class="ui-btn btn-confirm ui-icon-camera ui-btn-icon-top"
                        onclick="location.href='/DailyWork/VisitFarmerPhoto?activityNo=@Model.ActivityItem.ActivityNumber&ischeckout=@Model.ActivityItem.IsCheckOut.ToString';"
                        style="height: 60px; padding-top: 25px !important;">
                    @If Not Model.ActivityItem.IsCheckOut Then
                        @<span>ถ่ายรูป</span>
                    Else
                        @<span>รูปถ่าย</span>
                    End If
                </Button>
            </div>

            <div Class="col"
                 style="display:none">
                <Button id="btnSave"
                        Class="ui-btn btn-confirm ui-icon-check ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    บันทึก
                </Button>
            </div>

            @If Not Model.ActivityItem.IsCheckOut Then
                @<div Class="col">
                    <Button id="btnCheckout"
                            Class="ui-btn btn-deny ui-icon-location ui-btn-icon-top"
                            onclick="CallCheckOut('@Model.ActivityItem.ActivityNumber','@Model.Farmer.FarmerName')"
                            style="height: 60px; padding-top: 25px !important;">
                        Check Out
                    </Button>
                </div>
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


End Section

