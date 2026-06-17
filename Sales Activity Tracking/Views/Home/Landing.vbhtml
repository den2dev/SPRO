@Code
    ViewData("Title") = "Daily Report"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code

<style>
    #mainItems {
        height: calc(100vh - 170px);
        overflow-y: auto;
        background: white;
        border: 1px solid #ddd;
        padding: 10px;
    }

    .custom-input {
        border: 1px solid #ddd;
        background-color: white;
        width: 100%;
        height: 40px;
    }
</style>

<div class="row g-0">

    <div id="mainItems"> 

        <div style="height:100%;display:flex;align-items:center;justify-content:center;">
           
                 <h1 style="color:red">ไม่พบ FSMCODE ที่ส่งมา!</h1>
             
        </div>

    </div>

</div>



<!-- Bottom Buttons -->

<div class="button-menu-container">
    <div class="container">
        <div class="row g-0">
            <a href="#"
               class="ui-btn btn-style col no-padding"
               onclick="goBack();">

                <img src="@(StaticRootImgs)/Home-black.png" alt="Home" class="button-menu-icon" />
                <span class="button-menu-label">Home</span>

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
                <button id="btnBack"
                        class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;" onclick="goBack();">
                    Home
                </button>
            </div> 
        </div>
    </div>
</div>

<!-- End Bottom Buttons -->

@section Scripts
    <script>
         function goBack() {
                location.href='@Url.Action("index", "Home")'
        }
    </script>
End Section