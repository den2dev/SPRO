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

function getLocation() {

    /*clear ค่าเดิม*/
    photoBlob = null;
    $("#previewImage").hide().attr("src", "");
    $("#imagePlaceholder").show();
    $("#txtgeolocation").val("");
    $("#txtOdometerStart,#txtOdometerEnd").val("");
    /*end clear ค่าเดิม*/

    $('#gpsLoadingModal').modal('show');

    navigator.geolocation.getCurrentPosition(

        function (position) {

            var lat = position.coords.latitude;
            var lng = position.coords.longitude;

            var latDirection = lat >= 0 ? "N" : "S";
            var lngDirection = lng >= 0 ? "E" : "W";

            var gpsText =
                latDirection + Math.abs(lat) +
                " " +
                lngDirection + Math.abs(lng);

            $("#txtgeolocation")
                .val(gpsText);


            $('#gpsLoadingModal').modal('hide');

            $('#timeInModal').modal('show');

        },

        function (error) {

            $('#gpsLoadingModal').modal('hide');

            $('#gpsErrorModal').modal('show');

        },

        {
            enableHighAccuracy: true,
            timeout: 15000,
            maximumAge: 0
        }
    );
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


let photoBlob = null;

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

$("#btnTakePhoto").click(function () {

    let video = document.getElementById("video");
    let canvas = document.getElementById("canvas");

    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;

    let ctx = canvas.getContext("2d");
    ctx.drawImage(video, 0, 0);
    canvas.toBlob(function (blob) {

        photoBlob = blob;

        $("#previewImage")
            .show()
            .attr(
                "src",
                URL.createObjectURL(blob));

        ValidateTimeIn();
        ValidateTimeOut();

        $("#imagePlaceholder").hide();
        $('#CameraModal').modal('hide');
        $('#timeInModal').modal('show');

    }, "image/jpeg", 0.9);

});

$("#btnRetake").click(function () {

    photoBlob = null;

    $("#previewImage")
        .hide()
        .attr("src", "");

    $("#imagePlaceholder").show();

    ValidateTimeIn();
    ValidateTimeOut();

});
 
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