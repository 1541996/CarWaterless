$(document).ready(function () {

    $(document).keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {

            ClearError();

            var isvalid = false;
            var UserName = $('#UserName').val();
            var Password = $('#Password').val();
            if (UserName != "") {
                if (Password != "") {
                    isvalid = true;
                }
                else {
                    $('#Password').closest(".form-control").addClass("is-invalid");
                    $('#Password-error').show();
                }
            }
            else {
                $('#UserName').closest(".form-control").addClass("is-invalid");
                $('#UserName-error').show();
            }
            if (isvalid == true) {
                $.ajax({
                    type: "POST",
                    url: loginUrl,
                    data: GetModel(),
                    success: function (data) {
                        if (data.MessageType == 1) {
                            window.location = "/Home/Index";
                        }
                        else {
                            showMessage(data.MessageType, data.Message);
                        }

                    }
                })
            }

        }
    });


    $('#btnLogin').click(function () {

        ClearError();

        var isvalid = false;
        var UserName = $('#UserName').val();
        var Password = $('#Password').val();
        if (UserName != "") {
            if (Password != "") {
                isvalid = true;
            }
            else {
                $('#Password').closest(".form-control").addClass("is-invalid");
                $('#Password-error').show();
            }
        }
        else {
            $('#UserName').closest(".form-control").addClass("is-invalid");
            $('#UserName-error').show();
        }
        if (isvalid == true) {
            $.ajax({
                type: "POST",
                url: loginUrl,
                data: GetModel(),
                success: function (data) {
                    if (data.MessageType == 1) {
                        window.location = "/Home/Index";
                    }
                    else {
                        showMessage(data.MessageType, data.Message);
                    }
                   
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
                window.location = "/Login/Index";
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
        }).then((result) => {
            if (result.value) {
                window.location = "/Login/Index";
            }
        });
    }
}


function GetModel() {
    var model = {};
    model.UserName = $('#UserName').val();
    model.Password = $('#Password').val();

    return model;
}


function ClearError() {
    $('#UserName').closest(".form-control").removeClass("is-invalid");
    $('#UserName-error').hide();
    $('#Password').closest(".form-control").removeClass("is-invalid");
    $('#Password-error').hide();
}