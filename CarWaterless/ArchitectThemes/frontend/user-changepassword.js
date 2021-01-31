$(document).ready(function () {

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {
        ClearError();
        var txtCurrentPassword = $('#txtCurrentPassword').val();
        var Password = $('#Password').val();
        var txtConfirmPassword = $('#txtConfirmPassword').val();
        if (txtCurrentPassword != "") {
            if (Password != "") {
                if (txtConfirmPassword != "") {
                    if (Password == txtConfirmPassword) {
                        $.ajax({
                            type: "POST",
                            url: saveUrl,
                            data: GetModel(),
                            success: function (data) {
                                showMessage(data.Message, data.MessageType);
                            }
                        })
                    }
                    else {
                        $('#Password').closest(".form-group").addClass("has-error");
                        $('#txtConfirmPassword').closest(".form-group").addClass("has-error");
                        $('#NotCorrect-error').show();
                    }
                }
                else {
                    $('#txtConfirmPassword').closest(".form-group").addClass("has-error");
                    $('#txtConfirmPassword-error').show();
                }
            }
            else {
                $('#Password').closest(".form-group").addClass("has-error");
                $('#Password-error').show();
            }
        }
        else {
            $('#txtCurrentPassword').closest(".form-group").addClass("has-error");
            $('#txtCurrentPassword-error').show();
        }
        
    });

});

function GetModel() {
    var model = {};
    model.Id = $('#Id').val();
    model.Password = $('#Password').val();
    model.CurrentPassword = $('#txtCurrentPassword').val();

    return model;
}

function ClearError() {
    $('#txtCurrentPassword').closest(".form-group").removeClass("has-error");
    $('#Password').closest(".form-group").removeClass("has-error");
    $('#txtConfirmPassword').closest(".form-group").removeClass("has-error");

    $('#txtCurrentPassword-error').hide();
    $('#Password-error').hide();
    $('#txtConfirmPassword-error').hide();
    $('#NotCorrect-error').hide();
}


function showMessage(message, type) {
    if (type == 1) {
        Swal.fire({
            title: message,
            text: "Please Login again",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }).then((result) => {
            if (result.value) {
                window.location = "/Login/Index";
            }
        });
    }
    else if (type == 2) {
        Swal.fire({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).catch(swal.noop)
    }
    else if (type == 3) {
        Swal.fire({
            title: message,
            text: "Go to Login Page.",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).then((result) => {
            if (result.value) {
                window.location = "/AdminUser/ChangePassword";
            }
        });
    }
    else if (type == 4) {
        Swal.fire({
            title: message,
            text: "Go to Login Page.",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).then((result) => {
            if (result.value) {
                window.location = "/Login/Index";
            }
        });
    }

}
