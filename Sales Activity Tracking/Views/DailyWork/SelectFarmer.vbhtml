@ModelType SelectFarmerViewModel
@Code
    ViewData("Title") = "ระบุชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


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


<style>
    #mainItems {
        height: calc(100vh - 215px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }
</style>

<div class="row g-0">

    <div id="mainItems">

        <div id="farmerList">

            @For Each item In Model.FarmerList

                @<div class="card farmer-item"
                      onclick="location.href='@Url.Action("VisitFarmer","DailyWork")?isnewfarmer=@item.IsNewFarmer.ToString&farmerCode=@item.FarmerCode'"
                      data-code="@item.FarmerCode"
                      data-name="@item.FarmerName"
                      data-mobile="@item.MobileNo"
                      style="margin:8px;padding:12px;border:1px solid #ddd;border-radius:10px;cursor:pointer;">

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
                        onclick="history.back();">
                    Back
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