@ModelType CheckInViewModel

@Code
    ViewData("Title") = "CheckIn"
    ViewBag.Title = "Check In"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="container">

    ```
    <h3>Check In</h3>

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
         style="width:100%;display:none;margin-top:10px;" />

    <div class="mt-2">

        <button type="button"
                id="btnTakePhoto"
                class="btn btn-primary">
            Take Photo
        </button>

        <button type="button"
                id="btnRetake"
                class="btn btn-warning">
            Retake
        </button>

    </div>

    <div class="mt-3">

        <button type="button"
                id="btnCheckIn"
                class="btn btn-success">
            Check In
        </button>

    </div>
    ```

</div>

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

                alert("กรุณาเปิด GPS ก่อน");

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

        }, "image/jpeg", 0.9);

    });

    $("#btnRetake").click(function () {

        photoBlob = null;

        $("#previewImage")
            .hide()
            .attr("src", "");

    });

    $("#btnCheckIn").click(function () {

        if (photoBlob == null) {

            alert("กรุณาถ่ายรูป");

            return;
        }

        let formData =
            new FormData();

        formData.append(
            "Latitude",
            $("#Latitude").val());

        formData.append(
            "Longitude",
            $("#Longitude").val());

        formData.append(
            "PhotoFile",
            photoBlob,
            "checkin.jpg");

        $.ajax({

            url: "/CheckIn/CheckIn",

            type: "POST",

            data: formData,

            processData: false,

            contentType: false,

            success: function (r) {

                alert("Check In สำเร็จ");

                location.href =
                    "/Home";

            }

        });

    });

</script>
     
    End Section