@Code
    ViewData("Title") = "TestMessagebox"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>TestMessagebox</h2>
<hr />
<div Class="col-12 no-padding">
    <Button Class="ui-btn btn-confirm" style="height: 60px;" onclick="location.href='@Url.Action("Index", "Test")'">
        Back
    </Button>
</div>
<hr />

@*Show Popup Messgaebox*@
<div id="msgOverlay" class="msg-overlay">

    <div class="msg-box">

        <div id="msgTitle" class="msg-title">
            แจ้งเตือน
        </div>

        <div id="msgText" class="msg-text">
            ข้อความ
        </div>

        <button id="btnMsgOK" class="msg-btn">
            ตกลง
        </button>

    </div>

</div>
@*END Messagbox*@

@*Confirm Messagbox*@
<div id="confirmOverlay" class="msg-overlay">

    <div class="msg-box">

        <div id="confirmTitle" class="msg-title">
            ยืนยันรายการ
        </div>

        <div id="confirmText" class="msg-text">
        </div>

        <div class="confirm-buttons">

            <button id="btnConfirmYes" class="msg-btn">
                ตกลง
            </button>

            <button id="btnConfirmNo" class="msg-btn btn-cancel">
                ยกเลิก
            </button>

        </div>

    </div>

</div>
@*End Confirm Messagbox*@

<button id="btnTest">
    Test MessageBox
</button>

<button id="btnTestConfirm">
    Test Confirm MessageBox
</button>



@section scripts

    <script>

        $(document).ready(function () {

            $("#btnTest").click(function () {
                ShowMessage(
                    "ไม่สามารถเชื่อมต่อฐานข้อมูลได้",
                    "เกิดข้อผิดพลาด"
                );

            });

            $("#btnTestConfirm").click(function () {
                ShowConfirm(

                    "ต้องการลบข้อมูลนี้หรือไม่ ?",

                    function () {
                        console.log("ผู้ใช้กดลบ");
                        /*callfunction();*/

                    },

                    function () {

                       /* callfunction();*/
                        console.log("ผู้ใช้กดยกเลิก");


                    },

                    "ยืนยันการลบ"

                ); 

            });


        });

    </script>

End Section

