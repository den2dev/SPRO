
@ModelType List(Of String)



@If Model Is Nothing OrElse Not Model.Any() Then

    @<div class="alert alert-info">
        ยังไม่มีรูปภาพ
    </div>

Else

    @<div class="row">

        @For Each fileName In Model

            @<div class="col-4 mb-3">

                <div class="card photo-card"
                     style="margin:0;padding:0;border:1px solid #ddd;border-radius:0;">

                    <button type="button" 
                            class="btnDeletePhoto photo-delete-btn"
                            data-file="@fileName">

                        ✕

                    </button>

                    <img src="@Url.Content("~/Uploads/" & fileName)"
                         class="img-fluid photo-preview"
                         data-src="@Url.Content("~/Uploads/" & fileName)"
                         style="
                            width:100%;
                            height:100px;
                            object-fit:cover;
                            cursor:pointer;" />


                </div>

            </div>

        Next

    </div>

End If



@*<div class="card-body p-1" style="display:none">

    <button type="button"
            class="btn btn-danger btn-sm w-100 btnDeletePhoto"
            data-file="@fileName">

        ลบรูป

    </button>

</div>*@