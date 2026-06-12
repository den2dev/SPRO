@ModelType VisitFarmerViewModel

@Code
    ViewData("Title") = "ตรวจเยี่ยมชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<div style="padding-bottom:90px;">

    <!-- Farmer Info -->
    <div class="card"
         style="margin:10px;padding:12px;border:1px solid #ddd;border-radius:10px;">

        <div>
            <strong>@Model.Farmer.FarmerCode - @Model.Farmer.FarmerName</strong>
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

            <div>บ้านเลขที่ : @Model.Farmer.AddressNo</div>
            <div>หมู่ที่ : @Model.Farmer.Moo</div>
            <div>ตำบล : @Model.Farmer.SubDistrict</div>
            <div>อำเภอ : @Model.Farmer.District</div>
            <div>จังหวัด : @Model.Farmer.Province</div>
            <div>เลขที่สัญญาเดิม : @Model.Farmer.ContractNo</div>

        </div>

    </div>

    <!-- Questionnaire -->
    @Html.Partial("_Questionnaire", Model.Questionnaire)


</div>

 

<!-- Bottom Buttons -->

<div style="
    position:fixed;
    bottom:0;
    left:0;
    width:100%;
    padding-left:14px;
    padding-right:14px;
    background:#fff;
    border-top:1px solid #ddd;
    z-index:999;">
    <div class="container">

        <div class="row g-0">
            <div class="col">
                <button id="btnBack"
                        class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="history.back();">
                    Back
                </button>
            </div>
            <div class="col">
                <button id="btnDelete"
                        class="ui-btn btn-cancel ui-icon-delete ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    ลบ
                </button>
            </div>

            <div class="col">
                <button id="btnCapture"
                        class="ui-btn btn-confirm ui-icon-camera ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    ถ่ายรูป
                </button>
            </div>

            <div class="col">
                <button id="btnSave"
                        class="ui-btn btn-confirm ui-icon-check ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    บันทึก
                </button>
            </div>

            <div class="col">
                <button id="btnCheckout"
                        class="ui-btn btn-deny ui-icon-location ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Check Out
                </button>
            </div>

        </div>

         
    </div>
</div>


<!-- End Bottom Buttons -->


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

    </script>
End Section