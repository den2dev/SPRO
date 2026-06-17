@Modeltype VisitFarmerEditModeViewModel
@Code
    ViewData("Title") = "ยืนยันลบรายการตรวจเยี่ยมชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code



<style>
    #mainItems {
        height: calc(100vh - 170px);
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
 
<div class="row g-0">

    <div id="mainItems">


        <!-- Farmer Info -->
        <div class="card"
             style="margin:10px;padding:12px;border:1px solid #ddd;border-radius:2px;">

            <div>
                <strong>
                    @(If(Model.Farmer.IsNewFarmer, "ชาวไร่ใหม่", Model.Farmer.FarmerCode))
                    - @Model.Farmer.FarmerName
                </strong>
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
                    <button id="btnQuesn"
                            class="ui-btn btn-cancel ui-btn-icon-top" style="height: 60px; padding-top: 10px !important;" onclick="history.back();">
                        <span>📋</span>
                        <br />แบบสอบถาม
                    </button>
                </div>
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

            <div class="col">
                <Button id="btnIndex" type="button"
                        onclick="location.href='@Url.Action("index")'"
                        class="ui-btn btn-cancel ui-icon-back  ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;">
                    Activities
                </Button>
            </div>

            <div class="col">

                @Using Html.BeginForm("DeleteVisitItem", "DailyWork", FormMethod.Post)

                    @Html.Hidden("activityNo", Model.ActivityItem.ActivityNumber)

                    @<button type="submit"
                            class="ui-btn btn-cancel ui-icon-delete ui-btn-icon-top"
                            style="height: 60px; padding-top: 25px !important;">
                        Delete
                    </button>

                End Using
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

