<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Sales Activity Tracking</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
 
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

    <div class="container p-3 my-0 border">
        <h1>
            @ViewData("Title")
        </h1>
    </div>

    @*class="container body-content small"*@
    <div class="container small" style="padding-top:10px">

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
