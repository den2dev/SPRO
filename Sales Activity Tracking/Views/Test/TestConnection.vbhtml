@Code
    ViewData("Title") = "TestConnection"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Test Connect Database</h2>

<hr /> 

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Back
    </Button>
</div>

<hr />

<button id="btnTest">
    Test Connection
</button>

<div id="result"></div>

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

                    url: '/Test/TestConnectDB',
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
        });

    </script>

End Section