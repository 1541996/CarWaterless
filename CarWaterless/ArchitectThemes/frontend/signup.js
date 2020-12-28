
$(document).ready(function () {

    $('#btnSignup').click(function () {
        var username = $('#UserName').val();
        var message = "";
        var type = 2;
        $.ajax({
            type: "POST",
            url: checkUserNameurl,
            data: { "username": username },
            dataType: "json",
            success: function (data) {
                alert(data)
                if (data.toString() != "0") {
                    message = "User name already exists!"
                    showMessage(message, type);
                }
                else {
                    var password = $('#Password').val();
                    var confirmpassword = $('#ConfirmPassword').val();
                    if (password != confirmpassword) {
                        message = "Passwords do not match!"
                        showMessage(message, type);
                        $('#Password').focus();
                        $('#ConfirmPassword').val('');
                    } else {
                        $.ajax({
                            type: "POST",
                            url: signUpurl,
                            data: GetModel(),
                            dataType: "json",
                            success: function (data) {
                                if (data.MessageType == 1) {
                                    showMessage(data.Message, 3);
                                }
                                else {
                                    showMessage("",2)
                                }
                            }
                        });
                    }
                }
            },
        });
    });

    //$(function () {
    //    $('#register-form').validate({
    //        errorClass: "help-block",
    //        rules: {
    //            UserName: {
    //                required: true,
    //                minlength: 2
    //            },
    //            FullName: {
    //                required: true,
    //                minlength: 2
    //            },
    //            Password: {
    //                required: true,
    //                confirmed: true
    //            },
    //            ConfirmPassword: {
    //                equalTo: password
    //            }
    //        },
    //        highlight: function (e) {
    //            $(e).closest(".form-group").addClass("has-error")
    //        },
    //        unhighlight: function (e) {
    //            $(e).closest(".form-group").removeClass("has-error")
    //        },
    //    });
    //});
});

function GetModel() {
    var modal = {};
    modal.UserName = $('#UserName').val();
    modal.FullName = $('#FullName').val();
    modal.Password = $('#Password').val();
    modal.UserRole = $('#UserRole').val();
    
    return modal
}

function showMessage(message, type) {
    if (type == 1) {
        Swal.fire({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }, function () {
            window.location = redirectUrl;
        });
    }
    else if(type==2) {
        Swal.fire({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).catch(swal.noop)
    }
    else {
        Swal.fire({
            title: message ,
            text: "Go to Login Page.",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }).then((result) => {
            if (result.value) {
                window.location = "/Login/Index";
            }
        });
    }
   
}