@Code
    ViewData("Title") = "TestLoading"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>TestLoading</h2>

<hr />

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Back
    </Button>
</div>

<hr />

<button id="btnTest">
    Test Loading
</button>


@*Show ShowLoading*@
<div id="loadingOverlay" class="loading-overlay">

    <div class="loading-box">

        <div class="loading-spinner"></div>

        <div id="loadingText" class="loading-text">
            กำลังประมวลผล...
        </div>
    </div>
</div>
@*END Show ShowLoading*@


@section scripts

    <script>

        $(document).ready(function () {

            $("#btnTest").click(function () {

                ShowLoading("กำลังทดสอบ...");

                setTimeout(function () {

                    HideLoading();

                    alert("เสร็จแล้ว");

                }, 3000);

            });

        });

    </script>

End Section

