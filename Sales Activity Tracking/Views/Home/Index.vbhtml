@Code
    ViewData("Title") = "Home"
    Dim StaticRootImgs = ConfigurationManager.AppSettings("StaticRootImages")
End Code


 

<div class="button-menu-container" >

    <a id="btnCapture" href="/Home/Landing?FSMCODE=0101&FUSERID=ZZY" class="ui-btn btn-style" style="width:100%">
        <img src="@StaticRootImgs/carat-r-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">เข้าใช้งาน</span>
    </a>
 
    <a id="btnAccept" href="@Url.Action("Index", "Test")" class="ui-btn btn-style" style="width:100%">
        <img src="~/Content/images/eye-black.png" alt="Back" class="button-menu-icon" />
        <span class="button-menu-label">Test</span>
    </a>

</div>

<div class="button-menu-container" style="display:none">

 
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