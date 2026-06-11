@Code
    ViewData("Title") = "TestCamera"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>TestCamera</h2>
<hr />
<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Back
    </Button>
</div>
 
<hr />

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