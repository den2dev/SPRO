@ModelType DailyWorkViewModel

@Code
    ViewData("Title") = "Daily Report"
End Code

<div class="container">

    <div class="panel panel-primary">
        <h1 class="panel-title">
            @ViewData("Title")
        </h1>

        <div class="panel-body">

            <div class="row">
                <div class="col-4">
                    Employee
                </div>
                <div class="col-xs-8">
                    @Model.SalesmanName
                </div>
            </div>

            <hr />

            <div class="row">
                <div class="col-4">
                    Date
                </div>
                <div class="col-xs-8">
                    @DateTime.Now.ToString("dd/MM/yyyy")
                </div>
            </div>

            <hr />

            <div class="row">
                <div class="col-4">
                    Status :statusText
                </div>
            </div>

        </div>

    </div>



    <div class="row g-0">

        @If Not Model.IsCheckedIn Then
            @<div Class="col-6 no-padding">
                <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Back
                </Button>
            </div>

            @<div Class="col-6 no-padding">
                <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Time In
                </Button>
            </div>

        ElseIf Not Model.IsCheckedOut Then

            @<div Class="col-4 no-padding">
                <Button Class="ui-btn btn-cancel ui-icon-back ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Back
                </Button>
            </div>

            @<div Class="col-4 no-padding">
                <Button Class="ui-btn btn-deny ui-icon-plus ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Add
                </Button>
            </div>

            @<div Class="col-4 no-padding">
                <Button Class="ui-btn btn-confirm ui-icon-clock ui-btn-icon-top" style="height: 60px; padding-top: 25px !important;">
                    Time Out
                </Button>
            </div>

        End If

    </div>


</div>
