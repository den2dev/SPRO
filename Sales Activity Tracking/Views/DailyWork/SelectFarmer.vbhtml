@ModelType SelectFarmerViewModel
@Code
    ViewData("Title") = "Daily Report"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="page-header">

    <div class="row align-items-center">

        <div class="col-2 text-start">
            <a href="javascript:history.back();"
               class="ui-btn ui-btn-inline ui-icon-back ui-btn-icon-notext">
                Back
            </a>
        </div>

        <div class="col-8 text-center">
            <h4 style="margin:0;">
                ระบุชาวไร่
            </h4>
        </div>

        <div class="col-2 text-end">
            <span style="font-size:24px;">
                🔔
            </span>
        </div>

    </div>

</div>

<div style="padding:10px;">

    <input type="text"
           id="txtSearch"
           placeholder="ค้นหารหัส, ชื่อ หรือเบอร์โทร" />

</div>

<div id="farmerList">

    @For Each item In Model.FarmerList

        @<div class="card farmer-item"
              onclick="location.href='@Url.Action("VisitFarmer","DailyWork")?farmerCode=@item.FarmerCode'"
              data-code="@item.FarmerCode"
              data-name="@item.FarmerName"
              data-mobile="@item.MobileNo"
              style="margin:8px;padding:12px;border:1px solid #ddd;border-radius:10px;cursor:pointer;">

            <div>
                <strong>@item.FarmerName</strong>
            </div>

            <div>
                รหัส : @item.FarmerCode
            </div>

            <div>
                โทร : @item.MobileNo
            </div>

        </div>

    Next

</div>

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

    </script>

End Section


<!-- Bottom Buttons -->

<div style="
    position:fixed;
    bottom:0;
    left:0;
    width:100%;
    background:#fff;
    border-top:1px solid #ddd;
    z-index:999;">

    <div class="row g-0">
        <div class="col">
            <button id="btnBack"
                    class="ui-btn btn-deny ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                Back
            </button>
        </div>
    </div>
</div>