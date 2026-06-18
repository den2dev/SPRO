<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Sales Activity Tracking</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")


    <style>

        /*.button-container {
            position: fixed;
            bottom: 0;
            width: 100%;
            display: flex;
            justify-content: space-between;
            white-space: nowrap;
            margin: 0px;*/ /* Adjust the margin as needed */
        /*height: 50px;
            background-color: white;
        }*/

        .button-menu-container {
            position: fixed;
            display: flex;
            bottom: 0;
            left: 0;
            width: 100%;
            padding-left: 5px;
            padding-right: 5px;
            border-top: 1px solid #ddd;
            z-index: 999;
            white-space: nowrap;
            background-color: white;
        }
        .button-menu-label {
            display: block;
            text-align: center;
            font-size: small;
            color: black
        }
        .button-menu-icon {
            display: block;
            margin: 0 auto;
            width: 15px;
            height: 15px
        }
        .disabled-link {
            pointer-events: none;
            opacity: 0.5;
        }
    </style>

</head>
<body>

    @*<nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
            <div class="container">
                @Html.ActionLink("Sales Activity Tracking", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
                <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Toggle navigation" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li>@Html.ActionLink("Home", "Index", "Home", New With {.area = ""}, New With {.class = "nav-link"})</li>
                        <li>@Html.ActionLink("About", "About", "Home", New With {.area = ""}, New With {.class = "nav-link"})</li>
                        <li>@Html.ActionLink("Contact", "Contact", "Home", New With {.area = ""}, New With {.class = "nav-link"})</li>
                    </ul>
                </div>
            </div>
        </nav>
    *@

    <div class="p-3 my-0 border header ui-header ui-bar-inherit ui-header-fixed slidedown">
        <h1 id="app-title" class="ui-title">
            @*style="margin-top:8px"*@
        @ViewData("Title")
    </h1>
</div>

@*class="container body-content small style="padding-top:66px""*@
<div class="small">

    @RenderBody()

</div>


@Styles.Render("~/Content/jquerymobile")

@Scripts.Render("~/bundles/jquery")
@Scripts.Render("~/bundles/jquerymobile")

<script src="~/Scripts/common.js"></script>

@Scripts.Render("~/bundles/bootstrap")


@RenderSection("scripts", required:=False)
</body>
</html>
