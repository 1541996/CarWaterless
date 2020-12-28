
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
        var CodeNo = $('#StockNo').val();
        var UnitBasicPrice = $('#UnitBasicPrice').val();
        var UnitSellingPrice = $('#UnitSellingPrice').val();
        if (Name != "") {
            if (CodeNo != "") {
                if (UnitBasicPrice != "") {
                    if (UnitSellingPrice != "") {
                        isvalid = true;
                    }
                    else {
                        $('#UnitSellingPrice').closest(".form-control").addClass("is-invalid");
                        $('#UnitSellingPrice-error').show();
                    }
                }
                else {
                    $('#UnitBasicPrice').closest(".form-control").addClass("is-invalid");
                    $('#UnitBasicPrice-error').show();
                }
            }
            else {
                $('#StockNo').closest(".form-control").addClass("is-invalid");
                $('#StockNo-error').show();
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
    model.Name = $('#Name').val();
    model.StockNo = $('#StockNo').val();
    model.CategoryId = $('#CategoryId').val();
    model.ColorId = $('#ColorId').val();
    model.UnitBasicPrice = $('#UnitBasicPrice').val();
    model.UnitSellingPrice = $('#UnitSellingPrice').val();
    model.Description = $('#Description').val();
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
    $('#StockNo').closest(".form-control").removeClass("is-invalid");
    $('#StockNo-error').hide();
    $('#UnitBasicPrice').closest(".form-control").removeClass("is-invalid");
    $('#UnitBasicPrice-error').hide();
    $('#UnitSellingPrice').closest(".form-control").removeClass("is-invalid");
    $('#UnitSellingPrice-error').hide();
}