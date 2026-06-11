@Code
    ViewData("Title") = "TestGPS"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>TestGPS</h2>

<hr />

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Back
    </Button>
</div>

<hr />

<button onclick="testGPS()">
    Test GPS
</button>

<input id="Latitude" />
<input id="Longitude" />
 

@*Show Popup Progress*@
<div id="loadingOverlay" class="loading-overlay">

    <div class="loading-box">

        <div class="loading-spinner"></div>

        <div id="loadingText" class="loading-text">
            กำลังประมวลผล...
        </div>
    </div>
</div>
@*END Show Popup Progress*@



@section scripts

    <script>

        function testGPS() {

            ShowLoading("กำลังตรวจสอบ GPS...");

            navigator.geolocation.getCurrentPosition(

                function (position) {

                    alert(
                        "Lat : " + position.coords.latitude +
                        "\nLng : " + position.coords.longitude
                    );

                    /*$.mobile.loading("hide");*/
                    HideLoading();

                },

                function (error) {

                    alert(error.message);

                    /*$.mobile.loading("hide");*/
                    HideLoading();

                }

            );

        }

    </script>


End Section