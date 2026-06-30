$(document).ready(function () {

    /*            window.location.replace('/DailyWork/Index');*/

    /*   typeof bootstrap*/

    $("#vehicleSection").hide();
    $("#txtVehicleno").hide();
    $("#ddlVehicle").hide();

    $("#ddlVehicletype").change(function () {

        var vehicleType = $(this).val();

        $("#vehicleSection").hide();
        $("#txtVehicleno").hide();
        $("#ddlVehicle").hide();

        if (vehicleType === "0") {
            $("#vehicleSection").show();
            $("#ddlVehicle").show();
        }
        else if (vehicleType === "1") {
            $("#vehicleSection").show();
            $("#txtVehicleno").show();
        }
    });

});

function goTimeIn() {
    getLocation();
}



function goTimeOut() {

    var isallcheckout = $("#chkallcheckout").prop("checked");
    console.log(isallcheckout);

    if (!isallcheckout) {
        /* alert('กรุณาระบุเลขไมล์หลังใช้');*/
        console.log("พบรายการที่ยังไม่ CheckOut!");
        ShowMessage("พบรายการที่ยังไม่ CheckOut!\nกรุณา CheckOut ให้ครบทุกรายการ");
        return;

    } else {

        getLocation();

    }

}


 
function getLocation_Test() {

    navigator.geolocation.getCurrentPosition(

        function (position) {

            $("#Latitude").val(position.coords.latitude);
            $("#Longitude").val(position.coords.longitude);

            $("#lblLat").text(position.coords.latitude);
            $("#lblLng").text(position.coords.longitude);

        },

        function () {

            alert("กรุณาเปิด GPS ก่อน");

        }

    );

}


function CloseTimeInModal() {
    $('#gpsLoadingModal').modal('hide');
    $('#timeInModal').modal('hide');
}
function ShowCameraModal() {
    StartCamera();
    $('#timeInModal').modal('hide');
    $('#CameraModal').modal('show');
}

function CloseCameraModal() {
    $('#timeInModal').modal('show');
    $('#CameraModal').modal('hide');
}

function ShowAddClickModal() {
    $('#AddClickModal').modal('show');
}

function CloseAddClickModal() {
    $('#AddClickModal').modal('hide');
}



async function StartCamera() {

    const stream =
        await navigator.mediaDevices.getUserMedia({

            video: {
                facingMode: "environment"
            }

        });

    document
        .getElementById("video")
        .srcObject = stream;

}
 
function ValidateTimeIn() {

    var vehicleType = $("#ddlVehicletype").val();
    var odometer = $("#txtOdometerStart").val();
    var hasImage = $("#previewImage").attr("src");

    var isVehicleValid = false;

    if (vehicleType === "0") {
        // รถบริษัท
        isVehicleValid = $("#ddlVehicle").val();
    }
    else if (vehicleType === "1") {
        // รถตนเอง
        isVehicleValid = $.trim($("#txtVehicleno").val()) !== "";
    }

    var isValid =
        vehicleType &&
        isVehicleValid &&
        odometer &&
        Number(odometer) > 0 &&
        hasImage;

    $("#btnSaveTimeIn").prop("disabled", !isValid);

    $("#btnSaveTimeIn").toggleClass("disabled-link", !isValid);
} 
$("#ddlVehicletype").change(ValidateTimeIn);
$("#ddlVehicle").change(ValidateTimeIn);
$("#txtVehicleno").on("keyup change", ValidateTimeIn);
$("#txtOdometerStart").on("keyup change", ValidateTimeIn);


function ValidateTimeOut() {

    var odometer = $("#txtOdometerEnd").val();
    var hasImage = $("#previewImage").attr("src");

    var isValid =
        odometer &&
        odometer > 0 &&
        hasImage;

    $("#btnSaveTimeOut").prop("disabled", !isValid);

    $("#btnSaveTimeOut").toggleClass("disabled-link", !isValid);

}  
$("#txtOdometerEnd").on("input", function () {

    ValidateTimeOut();

});


function DeleteAtivityItem(doc) {

    ShowConfirm(

        "ต้องการลบข้อมูลนี้ " + doc + " หรือไม่ ?",

        function () {

            console.log("ลบรายการ " + doc);

            /*    alert("ลบรายการ"); */

            $.ajax({
                url: '/DailyWork/DeleteActivity',
                type: 'POST',
                data: {
                    activityNo: doc
                },
                success: function (res) {

                    if (res.Success) {

                        /*  alert("reload"); */

                        location.reload(); /*โหลดหน้าเดิม คือเรียก action index()*/

                    }
                    else {
                        ShowMessage(res.Message, "มีข้อผิดพลาดเกิดขึ้น!");
                        /*alert(res.Message);*/

                    }

                },

                error: function (er) {

                    ShowMessage(er.Message, "Save Error.มีข้อผิดพลาดเกิดขึ้น!");
                    /*alert("Save Error");*/

                }
            });
        },

        function () {
            console.log("ยกเลิก " + doc);
            /*  alert("ยกเลิก " + doc);*/

        },

        "ยืนยันการลบ"

    );

}