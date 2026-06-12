@ModelType NewFarmerViewModel
@Code
    ViewData("Title") = "เพิ่มชาวไร่รายใหม่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


@Using Html.BeginForm("NewFarmer", "DailyWork", FormMethod.Post, New With {.id = "frmNewFarmer"})

    @Html.ValidationSummary(False, "", New With {.class = "text-danger"})

    @<div style = "padding:10px;padding-bottom:90px;" >
        <label> ชื่อ-นามสกุล *</label>
        @Html.TextBoxFor(Function(m) m.FarmerName,
                         New With {
                             .placeholder = "ระบุชื่อ-นามสกุล"
                         })

        <label> เบอร์โทร *</label>
        @Html.TextBoxFor(Function(m) m.MobileNo,
                         New With {
                             .placeholder = "08xxxxxxxx"
                         })

        <label> บ้านเลขที่</label>
        @Html.TextBoxFor(Function(m) m.AddressNo)

        <label> หมู่ที่</label>
        @Html.TextBoxFor(Function(m) m.Moo)

        <label> หมู่บ้าน</label>
        @Html.TextBoxFor(Function(m) m.VillageName)

        <label> จังหวัด *</label>
        @Html.DropDownListFor(
                                     Function(m) m.ProvinceCode,
                                     Model.ProvinceList,
                                     "-- เลือกจังหวัด --",
                                     New With {.id = "ddlProvince"})

    <label>อำเภอ *</label>
    @Html.DropDownListFor(
                        Function(m) m.DistrictCode,
                        Model.DistrictList,
                        "-- เลือกอำเภอ --",
                        New With {.id = "ddlDistrict"})

    <label>ตำบล *</label>
    @Html.DropDownListFor(
                        Function(m) m.SubDistrictCode,
                        Model.SubDistrictList,
                        "-- เลือกตำบล --",
                        New With {.id = "ddlSubDistrict"})

</div>

@<div style="
    position:fixed;
    bottom:0;
    left:0;
    width:100%;
    padding-left:14px;
    padding-right:14px;
    background:#fff;
    border-top:1px solid #ddd;
    z-index:999;">

    <div class="row g-0">

        <div class="col no-padding">
            <button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="history.back();">
                Back
            </button>
        </div>

        <div class="col no-padding">
            <button type="submit"
                    Class="ui-btn btn-deny ui-icon-plus ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                บันทึก
            </button>
        </div>

    </div>

</div>

End Using


@section Scripts
    <script>
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