@ModelType NewFarmerViewModel
@Code
    ViewData("Title") = "เพิ่มชาวไร่รายใหม่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code

<style>
    #mainItems {
        height: calc(100vh - 124px);
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

<div style="padding-top:50px">
    @***Form*****@

    @Using Html.BeginForm("NewFarmerCheckInSave", "DailyWork", FormMethod.Post, New With {.id = "frmNewFarmer"})


        @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
        @<div Class="row g-0">
            <div id="mainItems">

                <input type="hidden" id="GeoLocation" name="GeoLocation" data-role="none" />

                <div class="container">
                    <div class="row">ประเภท *</div>
                    <div class="row">

                        @Html.DropDownListFor(
                                  Function(m) m.NewType,
                                 New List(Of SelectListItem) From {
                                     New SelectListItem With {.Value = "", .Text = "-- เลือกประเภท --"},
                                     New SelectListItem With {.Value = "1", .Text = "1-ปลูกอ้อยอย่างเดียว"},
                                     New SelectListItem With {.Value = "2", .Text = "2-ปลูกอ้อยและพืชอื่น"},
                                     New SelectListItem With {.Value = "3", .Text = "3-ปลูกพิชอื่น"}
                                 },
                                 New With {
                                     .data_role = "none",
                                     .class = "custom-select"
                                 })

                    </div>
                </div>

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

                <img src="@(StaticRootImgs)/back-black.png" alt="ตกลง" class="button-menu-icon" />
                <span class="button-menu-label">ตกลง</span>

            </a>

        </div>

    </div>
    @*END Messagbox*@

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

                    @*<button class="btn btn-danger"
                                data-dismiss="modal" onclick="$('#gpsErrorModal').modal('hide');">

                            ปิด

                        </button>*@

                    <a href="#"
                       class="ui-btn btn-style col no-padding"
                       data-dismiss="modal" onclick="$('#gpsErrorModal').modal('hide');">

                        <img src="@(StaticRootImgs)/back-black.png" alt="ปิด" class="button-menu-icon" />
                        <span class="button-menu-label">ปิด</span>

                    </a>

                </div>

            </div>

        </div>

    </div>

    <!-- Bottom Buttons -->

    <div class="button-menu-container">
        <div class="container">
            <div class="row g-0">
                <a href="#"
                   class="ui-btn btn-style col no-padding"
                   onclick="history.back()">

                    <img src="@(StaticRootImgs)/back-black.png" alt="Back" class="button-menu-icon" />
                    <span class="button-menu-label">Back</span>

                </a>

                <a id="btnAddNew"
                   href="#"
                   class="ui-btn btn-style col no-padding disabled-link"
                   onclick="$('#frmNewFarmer').submit();">

                    <img src="@(StaticRootImgs)/save-black.png" alt="save" class="button-menu-icon" />
                    <span class="button-menu-label">บันทึก</span>

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
                            onclick="history.back()">
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

        <script src="~/Scripts/DailyWork/NewFarmer.js"></script>

        <script>

            $("#frmNewFarmer").submit(function (e) {

                e.preventDefault();


                if ($("#NewType").val() == "") {
                    ShowMessage("กรุณาเลือกประเภท", null, "ตรวจสอบข้อมูล");
                    return false;
                }

                if ($("#FarmerName").val().trim() == "") {
                    ShowMessage("กรุณาระบุชื่อ-นามสกุล", null, "ตรวจสอบข้อมูล");
                    return false;
                }


                var mobile = $("#MobileNo").val();

                console.log('MobileNo' + mobile);

                if (mobile == "") {
                    ShowMessage("กรุณาระบุเบอร์โทร", null, "ตรวจสอบข้อมูล");
                    return false;
                }
                console.log("ผ่านตรวจเบอร์โทร");

                if ($("#ddlProvince").val() == "") {
                    ShowMessage("กรุณาเลือกจังหวัด", null, "ตรวจสอบข้อมูล");
                    return false;
                }
                console.log("ผ่านตรวจจังหวัด");

                if ($("#ddlDistrict").val() == "") {
                    ShowMessage("กรุณาเลือกอำเภอ", null, "ตรวจสอบข้อมูล");
                    return false;
                }
                console.log("ผ่านตรวจอำเภอ");

                if ($("#ddlSubDistrict").val() == "") {
                    ShowMessage("กรุณาเลือกตำบล", null, "ตรวจสอบข้อมูล");
                    return false;
                }
                console.log("ผ่านตรวจตำบล");

                $.ajax({
                    url: $(this).attr("action"),
                    type: "POST",
                    data: $(this).serialize(),
                    success: function (res) {

                        console.log(res);

                        console.log(res.RedirectUrl);

                        if (res.Success) {

                            window.location.href = res.RedirectUrl;

                        } else {

                            ShowMessage(
                                res.Message,
                                null,
                                "ข้อผิดพลาด"
                            );

                        }

                    }
                });

            });

        </script>
    End Section
