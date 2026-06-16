@Code
    ViewData("Title") = "ตรวจเยี่ยมชาวไร่"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    #mainItems {
        height: calc(100vh - 157px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }
</style>

<div id="mainItems">
    <iframe src="@ViewBag.QuestionnaireUrl"
            style="width:100%; height:100%; border:none;">
    </iframe>
</div>


<!-- Bottom Buttons -->

<div style="
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