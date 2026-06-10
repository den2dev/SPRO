@ModelType CheckOutViewModel
@Code
    ViewData("Title") = "Check Out"
    ViewBag.Title = "Check Out"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="container">

    ```
    <h3>Check Out</h3>

    <div class="card p-3 mb-3">

        <h5>ข้อมูล Check In</h5>

        <div>
            เวลา :
            @Model.CheckInDateTime
        </div>

        <div>
            Latitude :
            @Model.CheckInLat
        </div>

        <div>
            Longitude :
            @Model.CheckInLng
        </div>

        <img src="@Model.CheckInPhoto"
             style="width:100%;margin-top:10px;" />

    </div>

    <div class="card p-2 mb-2">

        <div>
            Latitude :
            <span id="lblLat">Loading...</span>
        </div>

        <div>
            Longitude :
            <span id="lblLng">Loading...</span>
        </div>

    </div>

    <video id="video"
           autoplay
           playsinline
           style="width:100%;border:1px solid #ccc;">
    </video>

    <canvas id="canvas"
            style="display:none;">
    </canvas>

    <img id="previewImage"
         style="width:100%;display:none;" />

    <button type="button"
            id="btnTakePhoto"
            class="btn btn-primary mt-2">
        Take Photo
    </button>

    <button type="button"
            id="btnRetake"
            class="btn btn-warning mt-2">
        Retake
    </button>

    <button type="button"
            id="btnCheckOut"
            class="btn btn-danger mt-3">
        Check Out
    </button>
    ```

</div>

<input type="hidden"
       id="RecordID"
       value="@Model.ID" />

<input type="hidden" id="Latitude" />
<input type="hidden" id="Longitude" />

@section scripts

    <script>

        let photoBlob = null;

        $(document).ready(function () {

            getLocation();
            startCamera();

        });

        function getLocation() {

            navigator.geolocation.getCurrentPosition(

                function (position) {

                    $("#Latitude").val(position.coords.latitude);
                    $("#Longitude").val(position.coords.longitude);

                    $("#lblLat").text(position.coords.latitude);
                    $("#lblLng").text(position.coords.longitude);

                },

                function () {

                    alert("กรุณาเปิด GPS");

                }

            );

        }

        async function startCamera() {

            const stream =
                await navigator.mediaDevices.getUserMedia({

                    video: {
                        facingMode: "environment"
                    }

                });

            document
                .getElementById("video")
                .srcObject = stream;

        }

        $("#btnTakePhoto").click(function () {

            let video =
                document.getElementById("video");

            let canvas =
                document.getElementById("canvas");

            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;

            let ctx =
                canvas.getContext("2d");

            ctx.drawImage(video, 0, 0);

            canvas.toBlob(function (blob) {

                photoBlob = blob;

                $("#previewImage")
                    .show()
                    .attr(
                        "src",
                        URL.createObjectURL(blob));

            });

        });

        $("#btnRetake").click(function () {

            photoBlob = null;

            $("#previewImage")
                .hide()
                .attr("src", "");

        });

        $("#btnCheckOut").click(function () {

            if (photoBlob == null) {

                alert("กรุณาถ่ายรูป");

                return;
            }

            let formData =
                new FormData();

            formData.append(
                "ID",
                $("#RecordID").val());

            formData.append(
                "Latitude",
                $("#Latitude").val());

            formData.append(
                "Longitude",
                $("#Longitude").val());

            formData.append(
                "PhotoFile",
                photoBlob,
                "checkout.jpg");

            $.ajax({

                url: "/CheckIn/SaveCheckOut",

                type: "POST",

                data: formData,

                processData: false,

                contentType: false,

                success: function () {

                    alert("Check Out สำเร็จ");

                    location.href =
                        "/Home";

                }

            });

        });

    </script>

End Section
