/*ShowLoading*/
function ShowLoading(message) {

    if (message) {

        $("#loadingText").text(message);

    }

    $("#loadingOverlay").show();

}
function HideLoading() {

    $("#loadingOverlay").hide();

}

/*ShowMessage*/
function ShowMessage(message, title) {

    $("#msgTitle").text(title || "แจ้งเตือน");
    $("#msgText").text(message);

    $("#msgOverlay").show();
}
function HideMessage() {

    $("#msgOverlay").hide();
}
$(document).on("click", "#btnMsgOK", function () {
    HideMessage();
});




/*Confirm MessageBox*/
let confirmYesCallback = null;
let confirmNoCallback = null;

function ShowConfirm(message, yesCallback, noCallback, title) {
    $("#confirmTitle").text(title || "ยืนยันรายการ");
    $("#confirmText").text(message);
    confirmYesCallback = yesCallback;
    confirmNoCallback = noCallback;
    $("#confirmOverlay").show();
}

function HideConfirm() {
    $("#confirmOverlay").hide();
    confirmYesCallback = null;
    confirmNoCallback = null;
}

$(document).on("click", "#btnConfirmYes", function () {

    HideConfirm();

    if (confirmYesCallback) {
        confirmYesCallback();
    }

});

$(document).on("click", "#btnConfirmNo", function () {

    HideConfirm();

    if (confirmNoCallback) {
        confirmNoCallback();
    }

});