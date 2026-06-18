@Code
    ViewData("Title") = "Home"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code


 

<div class="button-menu-container" >

    <a id="btnCapture" href="/Home/Landing?FSMCODE=0101" class="ui-btn btn-style" style="width:100%">
        <img src="@StaticRootImgs/bars-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">Daily Report</span>
    </a>
 
    <a id="btnAccept" href="@Url.Action("Index", "Test")" class="ui-btn btn-style" style="width:100%">
        <img src="~/Content/images/eye-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">Test</span>
    </a>

</div>

<div class="button-menu-container" style="display:none">


    @*<a id="btnHome" href="#" class="ui-btn btn-style">
            <img src="@(StaticRootImgs)/home-black.png" alt="Back" class="button-menu-icon" />
            <span class="button-menu-label">Home</span>
        </a>

        <a id="btnNew" href="#" class="ui-btn btn-style">
            <img src="@StaticRootImgs/plus-black.png" alt="Back" class="button-menu-icon" />
            <span class="button-menu-label">New</span>
        </a>

        <a id="btnBack" href="#" class="ui-btn btn-style">
            <img src="@StaticRootImgs/back-black.png" alt="Back" class="button-menu-icon" />
            <span class="button-menu-label">Back</span>
        </a>

        <a id="btnConfirm" href="#" class="ui-btn btn-style">
            <img src="@StaticRootImgs/arrow-r-black.png" alt="Back" class="button-menu-icon" />
            <span class="button-menu-label">Confirm</span>
        </a>
        <a id="btnGPS" href="#" class="ui-btn btn-style">
            <img src="@StaticRootImgs/location-black.png" alt="Back" class="button-menu-icon" />
            <span class="button-menu-label">GPS</span>
        </a>*@
    <a id="btnCapture" href="#" class="ui-btn btn-style">
        <img src="@StaticRootImgs/camera-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">Capture</span>
    </a>
    <a id="btnAccept" href="#" class="ui-btn btn-style">
        <img src="@StaticRootImgs/check-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">Accept</span>
    </a>
    <a id="btnCheck" href="#" class="ui-btn btn-style">
        <img src="@StaticRootImgs/recycle-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">Check ID</span>
    </a>
    <a id="btnAccept" href="#" class="ui-btn btn-style">
        <img src="~/Content/images/home-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">Accept</span>
    </a>

</div> 