
$(document).ready(function () {

    $('select').select2();

    var hdmonth = $('#hdMonth').val();
    $('#ddlMonth').val(hdmonth);
    $('#ddlMonth').trigger('change');

    var ispostback = $('#hdIsPostBack').val();
    var year = $('#hdYear').val();
    if (ispostback == "True") {
        if (year == 0) {
            $('#Year').closest(".form-group").addClass("has-error");
            $('#Year-error').show();
        }
    }

   
    $('#ddlMonth').change(function () {
        $('#Month').val($(this).val());
    });

    $('#btnSave').click(function () {
        $.ajax({
            type: "POST",
            url: saveUrl,
            data: GetModel(),
            success: function (data) {
                showMessage(data.MessageType, data.Message);
            }
        })
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
                window.location = "/Finance/Index";
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
    
    model.Month = $('#hdMonth').val();
    model.Year = $('#hdYear').val();
    model.TotalExpense = $('#hdTotalExpense').val();
    model.TotalIncome = $('#hdTotalIncome').val();
    model.Profit = $('#hdProfit').val();
    model.TotalSaleCount = $('#hdTotalSaleCount').val();

    return model;
}


function ClearError() {
    $('#Year').closest(".form-group").removeClass("has-error");
    $('#Year-error').hide();
}

function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed 
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
} 