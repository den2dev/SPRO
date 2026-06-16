@ModelType List(Of String)
@Code
'<div Class="col-12 mb-3">
'<div Class="col-6 mb-3">
End Code
@If Model Is Nothing OrElse Not Model.Any() Then

    @<div class="alert alert-info">
        ยังไม่มีรูปภาพ
    </div>

Else

    @<div class="row">

        @For Each fileName In Model

            @<div class="col-12 mb-3"> 

                <div class="card"
                     style="margin:0px;padding:0px;border:1px solid #ddd;border-radius:0px;">

                    <a href="@Url.Content("~/Uploads/" & fileName)"
                       target="_blank">

                        <img src="@Url.Content("~/Uploads/" & fileName)"
                             class="img-fluid"
                             style="width:95%;
                                    height:200px;
                                    object-fit:cover;" />

                    </a>

                    <div class="card-body p-1">

                        <button type="button"
                                class="btn btn-danger btn-sm w-100 btnDeletePhoto"
                                data-file="@fileName">

                            ลบรูป

                        </button>

                    </div>

                </div>

            </div>

        Next

    </div>

End If