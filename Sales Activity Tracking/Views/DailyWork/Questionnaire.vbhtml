@Code
    ViewData("Title") = "ตรวจเยี่ยมชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code

<style>
    #mainItems {
        height: calc(100vh - 56px);
        width:100%;
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }
</style>

@*URL=@ViewBag.QuestionnaireUrl*@
<input type="hidden" value="@ViewBag.QuestionnaireUrl" />
@*<div id="mainItems">

</div>*@
<iframe id="mainItems" src="@ViewBag.QuestionnaireUrl" style="padding-top:66px">
</iframe>

<!-- Bottom Buttons -->

<div class="button-menu-container">
    <div class="container">
        <div class="row g-0">

            <a href="#"
               class="ui-btn btn-style col no-padding"
               onclick="history.back();">

                <img src="@(StaticRootImgs)/back-black.png" alt="กลับ" class="button-menu-icon" />
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

            <div class="col">
                <Button id="btnBack"
                        class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top"
                        style="height: 60px; padding-top: 25px !important;"
                        onclick="history.back();">
                    Back
                </Button>
            </div>

        </div>


    </div>
</div>


<!-- End Bottom Buttons -->