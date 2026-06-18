$(document).ready(function () {


    $("#GeoLocation").val("");

    typeof bootstrap

    var gpsLoadingModal = new bootstrap.Modal(
        document.getElementById('gpsLoadingModal')
    );

    gpsLoadingModal.show();

    /*     $('#gpsLoadingModal').modal('show');*/



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

            $("#GeoLocation")
                .val(gpsText);

            gpsLoadingModal.hide();
            /*$('#gpsLoadingModal').modal('hide');*/

            $("#btnAddNew").prop("disabled", false);

            $("#btnAddNew").removeClass("disabled-link");

        },

        function (error) {

            gpsLoadingModal.hide();
            /*$('#gpsLoadingModal').modal('hide');*/

            var gpsErrorModal = new bootstrap.Modal(
                document.getElementById('gpsErrorModal')
            );
            gpsErrorModal.show();
            /*$('#gpsErrorModal').modal('show');*/

        },

        {
            enableHighAccuracy: true,
            timeout: 15000,
            maximumAge: 0
        }
    );
});

function CloseGPSMessage() {

    var gpsErrorModal = new bootstrap.Modal(
        document.getElementById('gpsErrorModal')
    );

    gpsErrorModal.hide();
}

$(function () {

    $("#ddlProvince").change(function () {

        var provinceCode = $(this).val();

        // ล้างอำเภอและตำบลเดิม
        $("#ddlDistrict").empty();
        $("#ddlSubDistrict").empty();

        // ใส่ค่าเริ่มต้น
        $("#ddlDistrict").append(
            $("<option>")
                .val("")
                .text("-- เลือกอำเภอ --")
        );

        $("#ddlSubDistrict").append(
            $("<option>")
                .val("")
                .text("-- เลือกตำบล --")
        );

        $.getJSON(
            '/DailyWork/GetDistrictList',
            {
                provinceCode: provinceCode
            },
            function (data) {

                $.each(data, function (i, item) {

                    $("#ddlDistrict").append(
                        $("<option>")
                            .val(item.Value)
                            .text(item.Text)
                    );

                });

            });

    });


    $("#ddlDistrict").change(function () {

        var provinceCode = $("#ddlProvince").val();
        var districtCode = $(this).val();

        // ล้างตำบลเดิม
        $("#ddlSubDistrict").empty();

        $("#ddlSubDistrict").append(
            $("<option>")
                .val("")
                .text("-- เลือกตำบล --")
        );

        $.getJSON(
            '/DailyWork/GetSubDistrictList',
            {
                provinceCode: provinceCode,
                districtCode: districtCode
            },
            function (data) {

                $.each(data, function (i, item) {

                    $("#ddlSubDistrict").append(
                        $("<option>")
                            .val(item.Value)
                            .text(item.Text)
                    );

                });

            });

    });

});