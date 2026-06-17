@ModelType ActivityPhotoViewModel
@Code
    ViewData("Title") = "รูปกิจกรรม"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code

@Html.HiddenFor(Function(m) m.ActivityNo)

<style>
    #mainItems {
        height: calc(100vh - 210px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px; 
    }
    

</style>
<div data-role="none" style="border:none;padding-top:3px">


    <div class="row g-0 text-center">
        <span class="small">Activity : @Model.ActivityNo</span>
    </div>
    <div class="row g-0">
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

    </script>

End Section


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