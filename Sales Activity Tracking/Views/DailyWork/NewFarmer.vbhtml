@ModelType NewFarmerViewModel
@Code
    ViewData("Title") = "เพิ่มชาวไร่รายใหม่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    #mainItems {
        height: calc(100vh - 170px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }

    #frmNewFarmer {
        border: none !important;
        box-shadow: none !important;
    }

    .custom-select {
        border: 1px solid #ddd !important;
        background-color: white !important;
        width: 100% !important;
        height: 40px !important;
    }

    .custom-input {
        border: 1px solid #ddd;
        background-color: white;
        width: 100%;
        height: 40px;
    }
</style>


@***Form*****@

@Using Html.BeginForm("NewFarmerCheckInSave", "DailyWork", FormMethod.Post, New With {.id = "frmNewFarmer"})


    @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
    @<div Class="row g-0">
        <div id="mainItems">

            <input type="hidden" id="GeoLocation" name="GeoLocation" data-role="none" />

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">ชื่อ-นามสกุล *</div>
                <div class="row">
                    @Html.TextBoxFor(
                                                                   Function(m) m.FarmerName,
                                                                   New With {
                                                                          .data_role = "none",
                                                                          .class = "custom-input",
                                                                          .placeholder = "ระบุชื่อ-นามสกุล"
                                                                   })
                </div>
            </div>

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">เบอร์โทร *</div>
                <div class="row">
                    @Html.TextBoxFor(
                                                                 Function(m) m.MobileNo,
                                                                 New With {
                                                                        .data_role = "none",
                                                                        .class = "custom-input",
                                                                        .placeholder = "กรอกเบอร์โทรติดต่อ"
                                                                 })
                </div>
            </div>

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">บ้านเลขที่</div>
                <div class="row">
                    @Html.TextBoxFor(
                                                               Function(m) m.AddressNo,
                                                               New With {
                                                                      .data_role = "none",
                                                                      .class = "custom-input",
                                                                      .placeholder = ""
                                                               })
                </div>
            </div>

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">หมู่ที่</div>
                <div class="row">
                    @Html.TextBoxFor(
                                                              Function(m) m.Moo,
                                                              New With {
                                                                     .data_role = "none",
                                                                     .class = "custom-input",
                                                                     .placeholder = ""
                                                              })
                </div>
            </div>

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">หมู่บ้าน</div>
                <div class="row">
                    @Html.TextBoxFor(
                                                             Function(m) m.VillageName,
                                                             New With {
                                                                    .data_role = "none",
                                                                    .class = "custom-input",
                                                                    .placeholder = ""
                                                             })
                </div>
            </div>

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">จังหวัด *</div>
                <div class="row">
                    @Html.DropDownListFor(
                                                          Function(m) m.ProvinceCode,
                                                          Model.ProvinceList,
                                                          "-- เลือกจังหวัด --",
                                                          New With {
                                                              .id = "ddlProvince",
                                                              .data_role = "none",
                                                              .class = "custom-select"
                                                         })
                </div>
            </div>

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">อำเภอ *</div>
                <div class="row">
                    @Html.DropDownListFor(
                                                              Function(m) m.DistrictCode,
                                                              Model.DistrictList,
                                                               "-- เลือกอำเภอ --",
                                                              New With {
                                                                  .id = "ddlDistrict",
                                                                  .data_role = "none",
                                                                  .class = "custom-select"
                                                             })
                </div>
            </div>

            <div class="container" style="padding-bottom:5px;padding-top:5px;">
                <div class="row">ตำบล *</div>
                <div class="row">
                    @Html.DropDownListFor(
                                                                  Function(m) m.SubDistrictCode,
                                                                  Model.SubDistrictList,
                                                                  "-- เลือกตำบล --",
                                                                  New With {
                                                                      .id = "ddlSubDistrict",
                                                                      .data_role = "none",
                                                                      .class = "custom-select"
                                                                 })
                </div>
            </div>

        </div>
    </div>
End Using
 
 

@if TempData("ErrorMessage") IsNot Nothing Then

    @<div id="msgOverlay" class="msg-overlay" style="display:flex">

        <div class="msg-box">

            <div class="msg-title">
                แจ้งเตือน
            </div>

            <div class="msg-text">
                @TempData("ErrorMessage")
            </div>

            <button type="button"
                    class="msg-btn"
                    onclick="location.href = '/DailyWork/NewFarmer';">
                ตกลง
            </button>

        </div>

    </div>

End If

@***หาพิกัด*****@
<div id="gpsLoadingModal"
     class="modal fade"
     data-backdrop="static"
     data-keyboard="false">

    <div class="modal-dialog modal-sm">

        <div class="modal-content">

            <div class="modal-body text-center">

                <h4> กำลังค้นหาตำแหน่ง</h4>

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

                <h4> ไม่สามารถระบุตำแหน่งได้</h4>

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

<!-- Bottom Buttons -->

<div style="
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
                <Button 
                        Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" 
                        style="height: 60px; padding-top: 25px !important;" 
                         onclick="location.href='@Url.Action("index")'">
                    Back
                </Button>
            </div>

            <div Class="col no-padding">
                <Button id="btnAddNew"
                        Class="ui-btn btn-deny ui-icon-plus ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;"
                        onclick="$('#frmNewFarmer').submit();"
                        disabled>
                    บันทึก
                </Button>
            </div>

        </div>
    </div>
</div>


<!-- End Bottom Buttons -->



@section Scripts

    <script>

        $(document).ready(function () {


            $("#GeoLocation").val("");

            typeof bootstrap

            var gpsLoadingModal = new bootstrap.Modal(
                document.getElementById('gpsLoadingModal')
            );

            gpsLoadingModal.show();

            /*     $('#gpsLoadingModal').modal('show');*/



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

                    $("#GeoLocation")
                        .val(gpsText);

                    gpsLoadingModal.hide();
                    /*$('#gpsLoadingModal').modal('hide');*/

                    $("#btnAddNew").prop("disabled", false);
                },

                function (error) {

                    gpsLoadingModal.hide();
                    /*$('#gpsLoadingModal').modal('hide');*/

                    var gpsErrorModal = new bootstrap.Modal(
                        document.getElementById('gpsErrorModal')
                    );
                    gpsErrorModal.show();
                    /*$('#gpsErrorModal').modal('show');*/

                },

                {
                    enableHighAccuracy: true,
                    timeout: 15000,
                    maximumAge: 0
                }
            );
        });

        function CloseGPSMessage() {

            var gpsErrorModal = new bootstrap.Modal(
                document.getElementById('gpsErrorModal')
            );

            gpsErrorModal.hide();
        }

      
        $(function () {

            $("#ddlProvince").change(function () {

                var provinceCode = $(this).val();

                // ล้างอำเภอและตำบลเดิม
                $("#ddlDistrict").empty();
                $("#ddlSubDistrict").empty();

                // ใส่ค่าเริ่มต้น
                $("#ddlDistrict").append(
                    $("<option>")
                        .val("")
                        .text("-- เลือกอำเภอ --")
                );

                $("#ddlSubDistrict").append(
                    $("<option>")
                        .val("")
                        .text("-- เลือกตำบล --")
                );

                $.getJSON(
                    '/DailyWork/GetDistrictList',
                    {
                        provinceCode: provinceCode
                    },
                    function (data) {

                        $.each(data, function (i, item) {

                            $("#ddlDistrict").append(
                                $("<option>")
                                    .val(item.Value)
                                    .text(item.Text)
                            );

                        });

                    });

            });


            $("#ddlDistrict").change(function () {

                var provinceCode = $("#ddlProvince").val();
                var districtCode = $(this).val();

                // ล้างตำบลเดิม
                $("#ddlSubDistrict").empty();

                $("#ddlSubDistrict").append(
                    $("<option>")
                        .val("")
                        .text("-- เลือกตำบล --")
                );

                $.getJSON(
                    '/DailyWork/GetSubDistrictList',
                    {
                        provinceCode: provinceCode,
                        districtCode: districtCode
                    },
                    function (data) {

                        $.each(data, function (i, item) {

                            $("#ddlSubDistrict").append(
                                $("<option>")
                                    .val(item.Value)
                                    .text(item.Text)
                            );

                        });

                    });

            });

        });
    </script>
End Section
