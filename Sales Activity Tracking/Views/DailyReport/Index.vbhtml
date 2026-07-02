@ModelType List(Of DailyReportViewModel)
@Code
    ViewData("Title") = "Daily Report"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
    Dim HomeURL = ConfigurationManager.AppSettings("HomeURL")
End Code

<style>
    #TIOsContainer {
        height: calc(100vh - 210px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
        width: 100%
    }
</style>

<div class="row g-0" style="padding-top:66px">

    <div class="container col-12 text-center small">
        <a href="#"
           id="btnSearch"
           class="ui-btn btn-style col no-padding">

            <img src="@(StaticRootImgs)/search-black.png" alt="ค้นหา" class="button-menu-icon" />
            <span class="button-menu-label">ค้นหาข้อมูล</span>

        </a>
    </div>


    @* Show Popup Filter *@
    <div class="modal fade"
         id="FilterModal"
         tabindex="-1"
         data-bs-backdrop="static"
         data-bs-keyboard="false"
         aria-hidden="true">

        <div class="modal-dialog modal-dialog-centered">

            <div class="modal-content">

                <!-- Header -->
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title">
                        กล่องค้นหา
                    </h5>
                </div>

                <!-- Body -->
                <div class="modal-body">

                    <div class="container-fluid">

                        <div class="row mb-3 align-items-center">
                            <div class="col-4 text-end">
                                วันที่เริ่มต้น :
                            </div>
                            <div class="col-8">
                                <input type="date"
                                       id="StartDate"
                                       name="StartDate"
                                       class="form-control form-control-sm"
                                       value="@Date.Today.ToString("yyyy-MM-dd")" />
                            </div>
                        </div>

                        <div class="row mb-3 align-items-center">
                            <div class="col-4 text-end">
                                ถึงวันที่ :
                            </div>
                            <div class="col-8">
                                <input type="date"
                                       id="EndDate"
                                       name="EndDate"
                                       class="form-control form-control-sm"
                                       value="@Date.Today.ToString("yyyy-MM-dd")" />
                            </div>
                            <div class="row mt-2">
                                <div class="col-12 small text-center">
                                    <span id="alert" style="color:red;font-size:13px;"></span>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

                <!-- Footer -->

                <div class="row g-0" style="padding-bottom:20px">
                    <button id="btnOk" href="#" class="ui-btn btn-style col no-padding" data-bs-dismiss="modal">
                        <img src="@(StaticRootImgs)/check-black.png" alt="ตกลง" class="button-menu-icon" />
                        <span class="button-menu-label">OK</span>
                    </button>
                    <a href="#" class="ui-btn btn-style col no-padding" data-bs-dismiss="modal">
                        <img src="@(StaticRootImgs)/back-black.png" alt="ยกเลิก" class="button-menu-icon" />
                        <span class="button-menu-label">Cancel</span>
                    </a>
                </div>

            </div>

        </div>

    </div>
    @* END Popup Filter *@



<div class="row g-0" style="padding-top:10px">

    <div class="container" style="display:flex; justify-content:space-between; align-items:center; margin-top:5px;">

        <div>
            ข้อมูล ณ วันที่
            <span id="lblStartDate"></span>
            ถึงวันที่
            <span id="lblEndDate"></span>
        </div>

        <div style="color:#666;">
            จำนวน : @ViewBag.tio_couns รายการ
        </div>

    </div>

    <div class="col-12 small text-start" style="padding-left:20px;display:none">
        ข้อมูล ณ วันที่
        <span id="lblStartDate"></span>
        ถึงวันที่
        <span id="lblEndDate"></span>
        จำนวน : ViewBag.tio_couns
    </div>
    <div id="TIOsContainer" style="padding-left:10px;padding-right:10px">

        @Html.Partial("_TIOList", Model)

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



@section Scripts
    <script>

        $(document).ready(function () {

            validateDate();

            $("#StartDate,#EndDate").on("change", function () {
                validateDate();
            });

            updateDateLabel();

            $("#btnSearch").click(async function () {

                var modal =
                    new bootstrap.Modal(
                        document.getElementById("FilterModal")
                    );
                modal.show();

            });

            $("#btnOk").on("click", function () {

                bootstrap.Modal
                    .getInstance(document.getElementById("FilterModal"))
                    .hide();

                loadTIOs();
                updateDateLabel();


            });

        });
        function showLoading(text) {

            $("#loadingText").text(text);
            $("#loadingOverlay").show();

        }

        function hideLoading() {

            $("#loadingOverlay").hide();

        }

        function loadTIOs() {

            showLoading("กำลังค้นหาข้อมูล...");

            $("#TIOsContainer").load(
                '/DailyReport/TIOList?startdate=' +
                $("#StartDate").val() +
                '&enddate=' +
                $("#EndDate").val(),

                function (response, status, xhr) {

                    hideLoading();

                    if (status == "error") {
                        alert("เกิดข้อผิดพลาดในการค้นหาข้อมูล");
                    }

                });

        }

        function validateDate() {

            var startDate = $("#StartDate").val();
            var endDate = $("#EndDate").val();

            if (startDate == "" || endDate == "") {
                $("#btnOk").prop("disabled", true);
                $("#alert").text("กรุณาเลือกวันที่");
                return;
            }

            if (startDate > endDate) {

                $("#btnOk").prop("disabled", true);
                $("#alert").text("วันที่สิ้นสุดต้องไม่น้อยกว่าวันที่เริ่มต้น");

            } else {

                $("#btnOk").prop("disabled", false);
                $("#alert").text("");

            }
        }

        function updateDateLabel() {
            $("#lblStartDate").text(formatDate($("#StartDate").val()));
            $("#lblEndDate").text(formatDate($("#EndDate").val()));
        }

        function formatDate(dateValue) {

            if (!dateValue)
                return "";

            var arr = dateValue.split("-");

            return arr[2] + "/" + arr[1] + "/" + arr[0];

        }
    </script>
End Section





<!-- Bottom Buttons -->

<div class="button-menu-container">
    <div class="container">
        <div class="row g-0">

            <a href="#"
               class="ui-btn btn-style col no-padding"
               onclick="location.href='@Url.Action("index", "DailyWork")'">

                <img src="@(StaticRootImgs)/back-black.png" alt="Back to Activities" class="button-menu-icon" />
                <span class="button-menu-label">Daily Activities</span>

            </a>

        </div>
    </div>
</div>

<!-- End Bottom Buttons -->