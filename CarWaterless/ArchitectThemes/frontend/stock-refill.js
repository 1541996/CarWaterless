
$(document).ready(function () {

  
    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {

        ClearError();

        var isvalid = false;
        var AvailableQuantity = $('#AvailableQuantity').val();
        if (AvailableQuantity != "") {
            isvalid = true;
        }
        else {
            $('#AvailableQuantity').closest(".form-control").addClass("is-invalid");
            $('#AvailableQuantity-error').show();
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
                window.location = "/Stock/Index";
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
    model.CurrentQuantity = $('#CurrentQuantity').val();
    model.AvailableQuantity = $('#AvailableQuantity').val();
    
    
    return model;
}


function ClearError() {
    $('#AvailableQuantity').closest(".form-control").removeClass("is-invalid");
    $('#AvailableQuantity-error').hide();
}