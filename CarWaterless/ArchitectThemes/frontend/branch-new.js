
$(document).ready(function () {

    var id = $('#Id').val();
    if (id != 0) {
        $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');
       
        
    }

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {

        ClearError();

        var isvalid = false;
        var LocationName = $('#LocationName').val();
        var LocationPhoneNo = $('#LocationPhoneNo').val();
        var LocationAddress = $('#LocationAddress').val();
        if (LocationName != "") {
            if (LocationPhoneNo != "") {
                if (LocationAddress != "") {
                    isvalid = true;
                }
                else {
                    $('#LocationAddress').closest(".form-control").addClass("is-invalid");
                    $('#LocationAddress-error').show();
                }
            }
            else {
                $('#LocationPhoneNo').closest(".form-control").addClass("is-invalid");
                $('#LocationPhoneNo-error').show();
            }
        }
        else {
            $('#LocationName').closest(".form-control").addClass("is-invalid");
            $('#LocationName-error').show();
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
                window.location = "/AdminSetup/BranchList";
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
    model.LocationName = $('#LocationName').val();
    model.LocationPhoneNo = $('#LocationPhoneNo').val();
    model.TownshipId = $('#TownshipId').val();
    model.AdminAgentId = $('#AdminAgentId').val();
    model.CarLimit = $('#CarLimit').val();
    model.OpenTime = $('#OpenTime').val();
    model.CloseTime = $('#CloseTime').val();
    model.LocationAddress = $('#LocationAddress').val();
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
    $('#LocationName').closest(".form-control").removeClass("is-invalid");
    $('#LocationName-error').hide();
    $('#LocationPhoneNo').closest(".form-control").removeClass("is-invalid");
    $('#LocationPhoneNo-error').hide();
    $('#LocationAddress').closest(".form-control").removeClass("is-invalid");
    $('#LocationAddress-error').hide();
}


function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}