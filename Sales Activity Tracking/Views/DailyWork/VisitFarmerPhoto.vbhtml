@ModelType ActivityPhotoViewModel
@Code
    ViewData("Title") = "รูปกิจกรรม"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code

@Html.HiddenFor(Function(m) m.ActivityNo)

<style>
    #mainItems {
        height: calc(100vh - 201px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }

    .zoom-image {
    max-width: 100%;
    max-height: 100%;
    animation: zoomIn .25s ease;
    }

    @@keyframes zoomIn {
        from {
            transform: scale(0.85);
            opacity: 0;
        }

        to {
            transform: scale(1);
            opacity: 1;
        }
    }

</style>
<div data-role="none" style="border:none;padding-top:74px">


    <div class="row g-0 text-center">
        <span class="small">Activity : @Model.ActivityNo</span>
    </div>
    <div class="row g-0" style="margin-left: 20px; margin-right:20px">

        @If Not Model.IsCheckOut Then
            @<button id="btnTakePhoto"
                     Class="btn btn-primary">

                📷 เพิ่มรูป

            </button>
        Else

            @<div style="
                height:52px;
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
            <div id="photoContainer" style="padding-left:5px;padding-right:5px">

                @Html.Partial(
                         "_PhotoList",
                         Model.Photos)

            </div>
        </div>
    </div>

    <input type="file"
           id="photoUpload"
           accept="image/*"
           capture="environment"
           multiple
           style="display:none;" />



</div>

@section Scripts

    <script>

        $(function () {

            // กดปุ่มเพิ่มรูป
            $("#btnTakePhoto").click(function () {

                $("#photoUpload").click();

            });

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

            <div class="modal-header border-0">

                <button type="button"
                        class="btn-close btn-close-white"
                        data-bs-dismiss="modal">
                </button>

            </div>

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