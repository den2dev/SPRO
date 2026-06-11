
@Code
    ViewData("Title") = "Home"
End Code


<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index","DailyWork")'">
        Daily Report
    </Button>
</div>


<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Test
    </Button>
</div>