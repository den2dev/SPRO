@Code
    ViewData("Title") = "Daily Report"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    #mainItems {
        height: calc(100vh - 100px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }
</style>

<div id="msgOverlay" class="msg-overlay" style="display:flex">

    <div class="msg-box">

        <div class="msg-title">
            แจ้งเตือน
        </div>

        <div class="msg-text">
            @TempData("ErrorMessage")
        </div>

        <button type="button"
                class="msg-btn"
                onclick="history.back();">
            ตกลง
        </button>

    </div>

</div>


<div class="row g-0">

    <div id="mainItems">



    </div>
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

           

        </div>
    </div>
</div>


<!-- End Bottom Buttons -->