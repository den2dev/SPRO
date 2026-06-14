
@Code
    ViewData("Title") = "Home"
End Code


<div Class="col-12 no-padding">
    <a Class="ui-btn btn-confirm" style="height: 60px;" href="/Home/Landing?FSMCODE=0101">
        Daily Report
    </a>
</div>


<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Test
    </Button>
</div>