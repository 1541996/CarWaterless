
$(document).ready(function () {

    var id = $('#Id').val();
    if (id != "") {
        $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');
        var isactive = $('#IsActive').val();
        if (isactive.toString() == "True") {
            $("#chkIsActive").prop("checked", true);
        }
        else {
            $("#chkIsActive").prop("checked", false);
        }
        var prt = $('#PaymentTypeRole').val();
        $('#ddlPaymentTypeRole').val(prt);
    }

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {

        ClearError();

        var isvalid = false;
        var Name = $('#Name').val();
        var PhoneNo = $('#PhoneNo').val();
        var Address = $('#Address').val();
        if (Name != "") {
            if (PhoneNo != "") {
                if (Address != "") {
                    isvalid = true;
                }
                else {
                    $('#Address').closest(".form-control").addClass("is-invalid");
                    $('#Address-error').show();
                }
            }
            else {
                $('#PhoneNo').closest(".form-control").addClass("is-invalid");
                $('#PhoneNo-error').show();
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
                window.location = "/Consumer/Index";
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
    model.PhoneNo = $('#PhoneNo').val();
    model.CompanyName = $('#CompanyName').val() == null ? "" : $('#CompanyName').val();
    model.Address = $('#Address').val();
    model.CreateUserId = $('#CreateUserId').val();
    if ($('#chkIsActive').prop("checked") == true) {
        model.IsActive = true;
    }
    else{
        model.IsActive = false;
    }
    return model;
}


function ClearError() {
    $('#Name').closest(".form-control").removeClass("is-invalid");
    $('#Name-error').hide();
    $('#PhoneNo').closest(".form-control").removeClass("is-invalid");
    $('#PhoneNo-error').hide();
    $('#Address').closest(".form-control").removeClass("is-invalid");
    $('#Address-error').hide();
}