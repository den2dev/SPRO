@ModelType DailyWorkViewModel

@Code
    ViewData("Title") = "TestDesignDataModel"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>TestDesignDataModel</h2>
<hr />
<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Back
    </Button>
</div>
<hr />