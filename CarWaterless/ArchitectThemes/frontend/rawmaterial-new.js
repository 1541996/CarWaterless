
$(document).ready(function () {

    var id = $('#Id').val();
    if (id != 0) {
        $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');
        var isactive = $('#IsActive').val();
        if (isactive.toString() == "True") {
            $("#chkIsActive").prop("checked", true);
        }
        else {
            $("#chkIsActive").prop("checked", false);
        }
    }

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {

        ClearError();

        var isvalid = false;
        var Name = $('#Name').val();
        var CodeNo = $('#CodeNo').val();
        var UnitPrice = $('#UnitPrice').val();
        if (Name != "") {
            if (CodeNo != "") {
                if (UnitPrice != "") {
                    isvalid = true;
                }
                else {
                    $('#UnitPrice').closest(".form-control").addClass("is-invalid");
                    $('#UnitPrice-error').show();
                }
            }
            else {
                $('#CodeNo').closest(".form-control").addClass("is-invalid");
                $('#CodeNo-error').show();
            }
        }
        else {
            $('#Name').closest(".form-control").addClass("is-invalid");
            $('#Name-error').show();
        }
        if (isvalid == true) {
            $.ajax({
                type: "POST",
                url: saveUrl,
                data: GetModel(),
                success: function (data) {
                    showMessage(data.MessageType, data.Message);

                }
            })
        }
    });


});

function showMessage(messagetype, message) {

    if (messagetype == 1) {
        Swal.fire({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }).then((result) => {
            if (result.value) {
                window.location = "/RawMaterial/Index";
            }
        });
    }
    else if (messagetype == 2) {
        Swal.fire({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).catch(swal.noop)
    }
}

function GetModel() {
    var model = {};
    var id = $('#Id').val();
    if (id != "") {
        model.Id = id;
    }
    model.Name = $('#Name').val();
    model.CodeNo = $('#CodeNo').val();
    model.UnitId = $('#UnitId').val();
    model.UnitPrice = $('#UnitPrice').val();
    model.CreateUserId = $('#CreateUserId').val();
    if ($('#chkIsActive').prop("checked") == true) {
        model.IsActive = true;
    }
    else {
        model.IsActive = false;
    }
    return model;
}


function ClearError() {
    $('#Name').closest(".form-control").removeClass("is-invalid");
    $('#Name-error').hide();
    $('#CodeNo').closest(".form-control").removeClass("is-invalid");
    $('#CodeNo-error').hide();
    $('#UnitPrice').closest(".form-control").removeClass("is-invalid");
    $('#UnitPrice-error').hide();
}