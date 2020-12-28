$(document).ready(function () {
    $('select').select2();

    $(function () {

        var id = $('#Id').val();
        
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
                        $('#Password').closest(".form-group").addClass("has-error");
                        $('#Password-error').show();
                    }
                }
                else {
                    $('#FullName').closest(".form-group").addClass("has-error");
                    $('#FullName-error').show();
                }
            }
            else {
                $('#UserName').closest(".form-group").addClass("has-error");
                $('#UserName-error').show();
            }
            if (isvalid == true) {
                $.ajax({
                    type: "POST",
                    url: saveUrl,
                    data: GetModel(),
                    success: function (data) {
                        showMessage(data.Message, data.MessageType);
                    }
                })
            }
        });

        $('#btnCancel').click(function () {
            location.reload();
        });
    });



});

function GetModel() {
    var model = {};
    model.Id = $('#Id').val();
    model.UserName = $('#UserName').val();
    model.FullName = $('#FullName').val();
    model.Password = $('#Password').val();
    return model;
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
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).then((result) => {
            if (result.value) {
                window.location = "/User/EditProfile";
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

function ClearError() {
    $('#UserName').closest(".form-group").removeClass("has-error");
    $('#FullName').closest(".form-group").removeClass("has-error");
    $('#Password').closest(".form-group").removeClass("has-error");
    $('#UserName-error').hide();
    $('#FullName-error').hide();
    $('#Password-error').hide();
}