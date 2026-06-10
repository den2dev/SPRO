function ShowLoading(message) {

    if (message) {

        $("#loadingText").text(message);

    }

    $("#loadingOverlay").show();

}

function HideLoading() {

    $("#loadingOverlay").hide();

}