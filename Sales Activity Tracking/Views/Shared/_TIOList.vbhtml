
@ModelType List(Of DailyReportViewModel)
 
@If Model Is Nothing OrElse Not Model.Any() Then

    @<div class="alert alert-info">
        ยังพบรายการ
    </div>

Else


    @For Each item In Model

        @<div class="card" style="margin-bottom: 10px; padding: 10px; border: 1px solid #ddd; border-radius: 2px;">

    <!-- Header -->
    <div style="cursor: pointer;"
         onclick="location.href='@Url.Action("activities", New With {.tiono = item.TIO_DocNumber})'">

        <div style="margin-top: 5px; font-weight: bold;">
            เจ้าหน้าที่ :  @item.SalesmanCode
        </div>
        <!-- Contact + Type -->
        <div style="display:flex;
                                      justify-content:space-between;
                                      align-items:center;
                                      margin-top:5px;">

            <span>ข้อมูลรถ : @item.VehicleNo</span>
            <span style="color:#666;">
                วันที่ (TimeIn-Date) : @item.TIO_Date
            </span>
        </div>
        <div style="margin-top:5px;">
            ประเภทรถ : @item.VehicleType
        </div>
        <div style="margin-top:5px;">
            เลขไมล์ก่อนใช้ : @item.OdometerStart  เลขไมล์หลังใช้ : @item.OdometerEnd
        </div>
        <div style="margin-top:5px;padding-top:8px" class="small">
            TIO Number :  @item.TIO_DocNumber
        </div>
        @If item.IsTimeOut Then
            @<div style="margin-top:5px;">
                ⏰ @item.TimeIn_DateTime - @item.TimeOut_DateTime
            </div>
        Else
            @<div style="margin-top:5px;">
                ⏰ @item.TimeIn_DateTime - <span style="color:red">Time Out!</span>
            </div>
        End If
    </div>

        @If item.ActivityItems.Count > 0 OrElse Not item.ActivityItems.Any() Then


            '<!-- ปุ่มแสดงรายการย่อย -->
            @<div class="mt-2">
                <a class="btn btn-sm btn-outline-primary"
                    data-bs-toggle="collapse"
                    href="#detail_@item.TIO_DocNumber">
                    ▼ แสดงรายการ
                </a>
            </div>

            '<!-- รายการย่อย -->
            @<div class="collapse mt-2" id="detail_@item.TIO_DocNumber">

            @For Each act In item.ActivityItems

                @<div class="border rounded p-2 mb-2">

                    <div class="container" style="display:flex; justify-content:space-between; align-items:center; margin-top:5px;">
                        <div>
                            <strong>@act.ActivityName</strong>
                        </div>

                        <div style="color:#666;">
                            @act.ActivityNumber
                        </div>
                    </div>

                    <div class="container" style="display:flex; justify-content:space-between; align-items:center; margin-top:5px;">
                        <div>
                            @act.ContactCode  @act.ContactName
                        </div>

                        <div style="color:#666;">
                            @act.TypeContactName
                        </div>
                    </div>

                    @If act.IsCheckOut Then
                        @<div style="margin-top:5px;">
                            ⏰ @act.CheckInDateTime - @act.CheckOutDateTime
                        </div>
                    Else
                        @<div style="margin-top:5px;">
                            ⏰ @act.CheckInDateTime - <span style="color:red">Check Out!</span>
                        </div>
                    End If

                </div>

            Next

        </div>

    End If



     </div>

    Next
End If

