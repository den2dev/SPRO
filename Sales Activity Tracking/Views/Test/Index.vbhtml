@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Index - Test</h2>
<hr />
<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Home")'">
        Home
    </Button>
</div>

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("TestConnection", "Test")'">
        Test Connect database
    </Button>
</div>

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("TestGPS", "Test")'">
        Test GPS
    </Button>
</div>

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("TestCamera", "Test")'">
        Test Camera
    </Button>
</div>

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("TestLoading", "Test")'">
        Test Popup Loading
    </Button>
</div>

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("TestMessagebox", "Test")'">
        Test Popup MessageBox
    </Button>
</div>

<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("TestDesignDataModel", "Test")'">
        Test DataModels
    </Button>
</div>