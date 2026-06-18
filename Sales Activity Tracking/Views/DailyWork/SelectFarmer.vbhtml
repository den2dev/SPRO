@ModelType SelectFarmerViewModel
@Code
    ViewData("Title") = "ระบุชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code

<style>
    #mainItems {
        height: calc(100vh - 184px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }
</style>

<span style="
    position:fixed;
    top:12px;
    right:10px;
    font-size:24px;
    z-index:10;
    cursor:pointer;">
    🔔
</span>


<div class="container" style="padding-bottom:5px;padding-top:0px;">
    <div class="row">
        <input type="text" data-role="none"
               id="txtSearch" style="border: 1px solid #ddd; background-color: white; width: 100%; height: 40px "
               placeholder="ค้นหารหัส, ชื่อ หรือเบอร์โทร" />
    </div>
</div>




<input type="hidden" id="txtgeolocation" />

<div class="row g-0">

    <div id="mainItems">

        <div id="farmerList">

            @For Each item In Model.FarmerList
                @<div Class="card farmer-item"
                      onclick="CallCheckIn('@item.FarmerCode','@item.FarmerName')"
                      data-code="@item.FarmerCode"
                      data-name="@item.FarmerName"
                      data-mobile="@item.MobileNo"
                      style="margin:8px;padding:12px;border:1px solid #ddd;border-radius:2px;cursor:pointer;">

                    <div>
                        <strong>👨‍🌾 @item.FarmerName</strong>
                    </div>

                    <div>
                        รหัส :
                        @If item.IsNewFarmer Then
                            @<span>ชาวไร่รายใหม่</span>
                        Else
                            @<span>@item.FarmerCode</span>
                        End If
                    </div>

                    <div>
                        โทร : @item.MobileNo
                    </div>

                </div>
            Next

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

                <button class="btn btn-danger"
                        data-dismiss="modal" onclick="$('#gpsErrorModal').modal('hide');">

                    ปิด

                </button>

            </div>

        </div>

    </div>

</div>

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
            </button>

            <button id="btnConfirmNo" class="msg-btn btn-cancel">
                ยกเลิก
            </button>*@

            <a id="btnConfirmYes"
               href="#"
               class="ui-btn btn-style col no-padding">

                <img src="@(StaticRootImgs)/check-black.png" alt="ok" class="button-menu-icon" />
                <span class="button-menu-label">ตกลง</span>

            </a>

            <a id="btnConfirmNo"
               href="#"
               class="ui-btn btn-style col no-padding">

                <img src="@(StaticRootImgs)/back-black.png" alt="cancel" class="button-menu-icon" />
                <span class="button-menu-label">ยกเลิก</span>

            </a>

        </div>

    </div>

</div>
@*End Confirm Messagbox*@

@*Show Popup Messgaebox*@
<div id="msgOverlay" class="msg-overlay">

    <div class="msg-box">

        <div id="msgTitle" class="msg-title">
            แจ้งเตือน
        </div>

        <div id="msgText" class="msg-text">
            ข้อความ
        </div>

        @*<button id="btnMsgOK" class="msg-btn">
            ตกลง
        </button>*@

        <a id="btnMsgOK"
           href="#"
           class="ui-btn btn-style col no-padding">

            <img src="@(StaticRootImgs)/back-black.png" alt="ปิด" class="button-menu-icon" />
            <span class="button-menu-label">ตกลง</span>

        </a>

    </div>

</div>
@*END Messagbox*@

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


@section Scripts
     
    <script>

        $(document).ready(function () {

            $("#txtSearch").on("keyup", function () {

                var keyword = $(this).val().toLowerCase();

                $(".farmer-item").each(function () {

                    var text =
                        ($(this).data("code") + " " +
                            $(this).data("name") + " " +
                            $(this).data("mobile"))
                            .toLowerCase();

                    $(this).toggle(text.indexOf(keyword) > -1);

                });

            });

        });

        function CallCheckIn(farmercode,farmername){

            ShowConfirm(

                farmername +
                "\n\n ใช่หรือไม่ ?",

                function () {

                    console.log("เพิ่มรายการบันทึกตรวจเยี่ยม " + farmername);

                    CreateCheckIn(farmercode, farmername);
                },

                function () {
                    console.log("click ยกเลิก ");
                },

                "เพิ่มรายการตรวจเยี่ยม"

            );

        }

        function CreateCheckIn(farmercode, farmername) {

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

                        ShowLoading("Checking In...");
                        $('#gpsLoadingModal').modal('hide');

                        var formData = new FormData();
                        formData.append("farmercode", farmercode);
                        formData.append("farmername", farmername);
                        formData.append( "GeoLocation", $("#txtgeolocation").val());

                        $.ajax({

                            url: '@Url.Action("CheckInSave", "DailyWork")',

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


<!-- Bottom Buttons -->
<div class="button-menu-container">
    <div class="container">
        <div class="row g-0">

            <a 
               href="#"
               class="ui-btn btn-style col no-padding"
               onclick="location.href='@Url.Action("index")'">

                <img src="@(StaticRootImgs)/tag-black.png" alt="Activities" class="button-menu-icon" />
                <span class="button-menu-label">Activities</span>

            </a>
            <a id="btnHome"
               href="#"
               class="ui-btn btn-style col no-padding"
               onclick="location.href='@Url.Action("NewFarmer")'">

                <img src="@(StaticRootImgs)/plus-black.png" alt="เพิ่มชาวไร่รายใหม่" class="button-menu-icon" />
                <span class="button-menu-label">ชาวไร่รายใหม่</span>

            </a> 

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

            <div Class="col no-padding">
                <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;"
                         onclick="location.href='@Url.Action("index")'">
                    Activities
                </Button>
            </div>

            <div Class="col no-padding">
                <Button Class="ui-btn btn-deny ui-icon-plus ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;"
                        onclick="location.href='@Url.Action("NewFarmer")'">
                    ชาวไร่รายใหม่
                </Button>
            </div>

        </div>
    </div>
</div>


<!-- End Bottom Buttons -->