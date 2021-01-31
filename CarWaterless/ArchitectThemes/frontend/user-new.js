
$(document).ready(function () {


    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {

        ClearError();

        var isvalid = false;
        var UserName = $('#UserName').val();
        var FullName = $('#FullName').val();
        var Password = $('#Password').val();
        if (UserName != "") {
            if (FullName != "") {
                if (Password != "") {
                    isvalid = true;
                }
                else {
                    $('#Password').closest(".form-control").addClass("is-invalid");
                    $('#Password-error').show();
                }
            }
            else {
                $('#FullName').closest(".form-control").addClass("is-invalid");
                $('#FullName-error').show();
            }
        }
        else {
            $('#UserName').closest(".form-control").addClass("is-invalid");
            $('#UserName-error').show();
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
                window.location = "/AdminUser/Index";
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
    if (id != 0) {
        model.Id = id;
    }
    model.UserName = $('#UserName').val();
    model.FullName = $('#FullName').val();
    model.Password = $('#Password').val();
    model.UserRole = $('#ddlUserRole').val();
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
    $('#UserName').closest(".form-control").removeClass("is-invalid");
    $('#UserName-error').hide();
    $('#FullName').closest(".form-control").removeClass("is-invalid");
    $('#FullName-error').hide();
    $('#Password').closest(".form-control").removeClass("is-invalid");
    $('#Password-error').hide();
}


function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}