@ModelType ActivityPhotoViewModel
@Code
    ViewData("Title") = "รูปกิจกรรม"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")

End Code

<link href="~/Content/DailyWork/photo.css" rel="stylesheet" />


@Html.HiddenFor(Function(m) m.ActivityNo)

<style>
    #mainItems {
        height: calc(100vh - 205px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }

</style>

 

<div data-role="none" style="border:none;padding-top:74px">


    <div class="row g-0 text-center">
        <span class="small">Activity : @Model.ActivityNo</span>
    </div>
    <div class="row g-0" style="margin-left: 18px; margin-right: 18px">

        @If Not Model.IsCheckOut Then

            @<div> 
                    <div class="row g-0">
                        <a href="#"
                           id="btnTakePhoto"
                           class="ui-btn btn-style col no-padding">

                            <img src="@(StaticRootImgs)/camera-black.png" alt="Back" class="button-menu-icon" />
                            <span class="button-menu-label">เพิ่มรูป</span>

                        </a>

                    </div> 
            </div>
 
        Else

            @<div style="
                height:58px;
                display:flex;
                align-items:center;
                justify-content:center;
                color:#6c757d;
                font-style:italic;">

                ไม่สามารถเพิ่มรูปได้ (Check Out แล้ว)

            </div>

        End If
    </div> 

    <div class="row g-0">
        <div id="mainItems">
            <div id="photoContainer" style="padding-left: 5px; padding-right: 5px; padding-top:10px">

                @Html.Partial(
                         "_PhotoList",
                         Model.Photos)

            </div>
        </div>
    </div>

    @*<input type="file"
           id="photoUpload"
           accept="image/*"
           capture="environment"
           multiple
           style="display:none;" />*@




</div>

<div class="modal fade"
     id="cameraModal"
     tabindex="-1">

    <div class="modal-dialog modal-fullscreen">

        <div class="modal-content bg-dark">

            <div class="modal-body p-0">

                <video id="cameraVideo"
                       autoplay
                       playsinline
                       style="width:100%;height:97%;object-fit:cover;">
                </video>

                <canvas id="cameraCanvas"
                        style="display:none;">
                </canvas>


                <div class="button-menu-container">
                    <div class="container">
                        <div class="row g-0">

                            <a id="btnCapture" href="#"
                               class="ui-btn btn-style col no-padding">

                                <img src="@(StaticRootImgs)/camera-black.png"  alt="Capture"  class="button-menu-icon" />
                                <span class="button-menu-label">ถ่ายรูป</span>

                            </a> 

                            <a 
                               href="#"
                               class="ui-btn btn-style col no-padding"
                               data-bs-dismiss="modal">

                                <img src="@(StaticRootImgs)/back-black.png" alt="Back" class="button-menu-icon" />
                                <span class="button-menu-label">Back</span>

                            </a>

                        </div>
                    </div>
                </div>
                  

            </div>


         </div>

    </div>

</div>

@section Scripts

    <script>

        let stream;
        async function startCamera() {

            stream = await navigator.mediaDevices.getUserMedia({

                video: {

                    facingMode: "environment"

                },

                audio: false

            });

            document
                .getElementById("cameraVideo")
                .srcObject = stream;

        }
        function stopCamera() {

            if (!stream)
                return;

            stream.getTracks().forEach(function (t) {

                t.stop();

            });

        }
        $("#btnTakePhoto").click(async function () {

            var modal =
                new bootstrap.Modal(
                    document.getElementById("cameraModal")
                );

            modal.show();

            await startCamera();

        });
        $("#btnCapture").click(function () {

            var video =
                document.getElementById("cameraVideo");

            var canvas =
                document.getElementById("cameraCanvas");

            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;

            var ctx =
                canvas.getContext("2d");

            ctx.drawImage(
                video,
                0,
                0
            );

            canvas.toBlob(function (blob) {

                uploadBlob(blob);

            }, "image/jpeg", 0.9);

        });

        function uploadBlob(blob) {

            var formData =
                new FormData();

            formData.append(
                "activityNo",
                $("#ActivityNo").val()
            );

            formData.append(
                "file0",
                blob,
                "photo.jpg"
            );

            $.ajax({

                url: "/DailyWork/UploadPhoto",

                type: "POST",

                data: formData,

                processData: false,

                contentType: false,

                success: function (r) {

                    stopCamera();

                    bootstrap.Modal
                        .getInstance(
                            document.getElementById("cameraModal")
                        )
                        .hide();

                    loadPhotos();

                }

            });

        }
        $("#cameraModal").on(
            "hidden.bs.modal",
            function () {

                stopCamera();

            });



        $(function () {

            // กดปุ่มเพิ่มรูป
            //$("#btnTakePhoto").click(function () {

            //    $("#photoUpload").click();

            //});

            // เลือกรูปเสร็จ
            $("#photoUpload").change(function () {

                if (this.files.length === 0)
                    return;

                var formData = new FormData();

                formData.append(
                    "activityNo",
                    $("#ActivityNo").val()
                );

                for (var i = 0; i < this.files.length; i++) {

                    formData.append(
                        "file" + i,
                        this.files[i]
                    );

                }

                $.ajax({

                    url: '/DailyWork/UploadPhoto',

                    type: 'POST',

                    data: formData,

                    processData: false,

                    contentType: false,

                    success: function (r) {

                        if (r.success) {

                            loadPhotos();

                            // reset file input
                            $("#photoUpload").val("");

                        }
                        else {

                            alert(r.message);

                        }

                    },

                    error: function () {

                        alert("Upload ไม่สำเร็จ");

                    }

                });

            });

        });

        function loadPhotos() {

            $("#photoContainer").load(
                '/DailyWork/PhotoList?activityNo=' +
                $("#ActivityNo").val()
            );

        }

        // ใช้ Event Delegate เพราะปุ่มลบถูกสร้างใหม่หลัง Ajax Load
        $(document).on(
            "click",
            ".btnDeletePhoto",
            function () {

                if (!confirm("ต้องการลบรูปนี้ใช่หรือไม่ ?"))
                    return;

                var fileName =
                    $(this).data("file");

                $.post(
                    '/DailyWork/DeletePhoto',
                    {
                        fileName: fileName
                    },
                    function (r) {

                        if (r.success) {

                            loadPhotos();

                        }
                        else {

                            alert(r.message);

                        }

                    }
                );

            }
        );


        $(document).on(
            "click",
            ".photo-preview",
            function () {

                $("#modalImage")
                    .attr("src",
                        $(this).data("src"));

                var modal =
                    new bootstrap.Modal(
                        document.getElementById("photoModal")
                    );

                modal.show();

            });

    </script>

End Section

<div class="modal fade"
     id="photoModal"
     tabindex="-1">

    <div class="modal-dialog modal-fullscreen">

        <div class="modal-content bg-dark">

            <button type="button"
                    class="photo-close-btn"
                    data-bs-dismiss="modal">

                ✕

            </button>
 

            <div class="modal-body
                        d-flex
                        justify-content-center
                        align-items-center">

                <img id="modalImage"
                     src=""
                     class="zoom-image" />

            </div>

        </div>

    </div>

</div>


<!-- Bottom Buttons -->
<div class="button-menu-container">
    <div class="container">
        <div class="row g-0">
            <a href="#"
               class="ui-btn btn-style col no-padding"
               onclick="history.back()">

                <img src="@(StaticRootImgs)/back-black.png" alt="Back" class="button-menu-icon" />
                <span class="button-menu-label">Back</span>

            </a> 

        </div>
    </div>
</div>

<div style="display:none;
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
        </div>
    </div>
</div>


<!-- End Bottom Buttons -->