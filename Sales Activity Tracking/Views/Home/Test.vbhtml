@Code
    ViewData("Title") = "Test"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Test</h2>

<button onclick="testGPS()">
    Test GPS
</button>

<script>

    function testGPS() {

        navigator.geolocation.getCurrentPosition(

            function (position) {

                alert(
                    "Lat = " + position.coords.latitude +
                    "\nLng = " + position.coords.longitude
                );

            },

            function (error) {

                alert('Err#' + error.message);

            }

        );

    }

</script>



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



<button id="btnTest">
    Test Database
</button>

<div id="result"></div>


@*Location*@
<button id="btnCheckIn">
    Check In
</button>

<input type="hidden" id="Latitude" />
<input type="hidden" id="Longitude" />

@***ถ่ายรูป***@
<video id="camera" autoplay playsinline></video>
<canvas id="canvas" style="display:none;"></canvas>

<button type="button" id="btnTakePhoto">
    ถ่ายรูป
</button>
@******ถ่ายรูป******@

@section scripts

    <script>

        $(document).ready(function () {

            $("#btnTest").click(function () {

                //$.mobile.loading("show", {
                //    text: "กำลังประมวลผล...",
                //    textVisible: true
                //});

                ShowLoading("ทดสอบเชื่อมต่อ Database...");

                // Disable ปุ่ม
                $("#btnTest").prop("disabled", true);

                $.ajax({

                    url: '/Home/TestDB',
                    type: 'GET',

                    success: function (data) {

                        $("#result").html(data);

                    },

                    error: function () {

                        $("#result").html("Error");

                    },

                    complete: function () {

                        // เปิดปุ่มกลับ ไม่ว่าจะ Success หรือ Error
                        $("#btnTest").prop("disabled", false);

                        HideLoading();

                        /*$.mobile.loading("hide");*/
                    }

                });

            });



            $("#btnCheckIn").click(function () {

                //$.mobile.loading("show", {
                //    text: "กำลังตรวจสอบ GPS...",
                //    textVisible: true
                //});

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

            });



            async function startCamera() {

                const stream = await navigator.mediaDevices.getUserMedia({
                    video: {
                        facingMode: "environment"
                    }
                });

                document.getElementById("camera").srcObject = stream;
            }

            startCamera();

            $("#btnTakePhoto").click(function () {

                const video = document.getElementById("camera");
                const canvas = document.getElementById("canvas");

                canvas.width = video.videoWidth;
                canvas.height = video.videoHeight;

                const ctx = canvas.getContext("2d");
                ctx.drawImage(video, 0, 0);

                const imageBase64 = canvas.toDataURL("image/jpeg", 0.9);

                console.log(imageBase64); 

            });

        });

    </script>

End Section