var Saleid;
var role;
$(document).ready(function () {

    var startstockid = $('#ddl_stockid').val();
    StockIdChangeEvent(startstockid);

    Saleid = $('#Id').val();

    $('#Quantity').val("");
    $('#UnitSellingPrice').val("");
    $('#lblnetalltotal').text($('#netalltotal').val());

    $('#ddl_stockid').change(function () {
        $('#txt_qty').val('');
        $('#hdstockqty').val('')
        $('#Available-alert').hide();
        var ddl_stockid = $(this).val();
        if (ddl_stockid != "") {
            $.ajax({
                type: "GET",
                url: getStockUrl,
                data: { "id": ddl_stockid },
                success: function (data) {
                    $('#hdstockqty').val(data.AvailableQuantity);
                    $('#Available-alert').text(data.AvailableQuantity + " Available");
                    var UnitSellingPrice = data.UnitSellingPrice;
                    $('#txt_unitsellprice').val(UnitSellingPrice);
                    $('#Available-alert').show();
                }
            })
            $('#ddl_stockid').closest(".form-control").removeClass("is-invalid");
            $('#ddl_stockid-error').hide();
        }
    });

    $('#txt_qty').focusout(function () {
        var qty = Number($(this).val());
        var available = Number($('#hdstockqty').val());
        if (qty > available) {
            $(this).val('');
            $('#Quantity-exceed').show();
            $(this).focus();
        }
        else {
            $('#Quantity-exceed').hide();
        }
    });

    BindGrid();

    $('#btnSave').click(function () {
        
        role = $('#PaymentTypeRole').val();
        alert(role)
        if (role == 1) {
            ClearError();
            var sSaleDate = $('#sSaleDate').val();
            if (sSaleDate != "") {
                $.ajax({
                    type: "POST",
                    url: saveSaleUrl,
                    data: GetModel(),
                    success: function (data) {
                        showMessage(data.MessageType, data.Message);

                    }
                })
            }
            else {
                $('#sSaleDate').closest(".form-control").addClass("is-invalid");
                $('#sSaleDate-error').show();
            }
        }
        else if (role == 2) {
            ClearErrorConsignment();
            var sSaleDate = $('#sSaleDate').val();
            var sConsignmentFinalDate = $('#sConsignmentFinalDate').val();
            var ConsignmentPercentage = $('#ConsignmentPercentage').val();
            var ConsignmentReceivedAmount = $('#ConsignmentReceivedAmount').val();
            if (sSaleDate != "") {
                if (sConsignmentFinalDate != "") {
                    if (ConsignmentPercentage != "") {
                        if (ConsignmentReceivedAmount != "") {
                            $.ajax({
                                type: "POST",
                                url: saveSaleUrl,
                                data: GetModel(),
                                success: function (data) {
                                    showMessage(data.MessageType, data.Message);

                                }
                            })
                        }
                        else {
                            $('#ConsignmentReceivedAmount').closest(".form-control").addClass("is-invalid");
                            $('#ConsignmentReceivedAmount-error').show();
                        }
                    }
                    else {
                        $('#ConsignmentPercentage').closest(".form-control").addClass("is-invalid");
                        $('#ConsignmentPercentage-error').show();
                    }
                }
                else {
                    $('#sConsignmentFinalDate').closest(".form-control").addClass("is-invalid");
                    $('#sConsignmentFinalDate-error').show();
                }
            }
            else {
                $('#sSaleDate').closest(".form-control").addClass("is-invalid");
                $('#sSaleDate-error').show();
            }
        }
        else {
            ClearErrorCredit();
            var sSaleDate = $('#sSaleDate').val();
            var sCreditFinalDate = $('#sCreditFinalDate').val();
            var CreditPercentage = $('#CreditPercentage').val();
            var CreditReceivedAmount = $('#CreditReceivedAmount').val();
            if (sSaleDate != "") {
                if (sCreditFinalDate != "") {
                    if (CreditPercentage != "") {
                        if (CreditReceivedAmount != "") {
                            $.ajax({
                                type: "POST",
                                url: saveSaleUrl,
                                data: GetModel(),
                                success: function (data) {
                                    showMessage(data.MessageType, data.Message);

                                }
                            })
                        }
                        else {
                            $('#CreditReceivedAmount').closest(".form-control").addClass("is-invalid");
                            $('#CreditReceivedAmount-error').show();
                        }
                    }
                    else {
                        $('#CreditPercentage').closest(".form-control").addClass("is-invalid");
                        $('#CreditPercentage-error').show();
                    }
                }
                else {
                    $('#sCreditFinalDate').closest(".form-control").addClass("is-invalid");
                    $('#sCreditFinalDate-error').show();
                }
            }
            else {
                $('#sSaleDate').closest(".form-control").addClass("is-invalid");
                $('#sSaleDate-error').show();
            }
        }
        

        


    });

    $('#btnEdit').click(function () {
        ClearError();

        var UnitSellingPrice = $('#txt_unitprice').val();
        var quantity = $('#txt_qty').val();
        var discount = $('#txt_discount').val();
        if (quantity != "") {
            if (UnitSellingPrice != "") {
                if (discount == "") {
                    discount = 0;
                }
                $.ajax({
                    type: "POST",
                    url: saveUrl,
                    data: { "model": GetAddModel() },

                    //contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        showMessage(data.MessageType, data.Message);

                    }
                })
            }
            else {
                $('#txt_unitprice').closest(".form-control").addClass("is-invalid");
                $('#UnitSellingPrice-error').show();
            }
        }
        else {
            $('#txt_qty').closest(".form-control").addClass("is-invalid");
            $('#Quantity-error').show();
        }


    });

    $('#OtherCharges').focusout(function () {
        //updatenettotal();
        if (role != 1) {
            var otherchanges;
            if ($('#OtherCharges').val().length == 0) {
                otherchanges = 0;
            }
            else {
                otherchanges = Number($('#OtherCharges').val());
            }

            var nettotal = Number($('#netalltotal').val()) + Number(otherchanges);
            $('#lblnetalltotal').text(nettotal);

            if (role == 2) {
                $('#CreditTotalAmount').val(nettotal);
                var totalamount = $('#CreditTotalAmount').val();
                var percentage = Number($('#CreditPercentage').val());
                var commissionamount = Number(totalamount) * percentage * 0.01;
                $('#CreditCommissionAmount').val(commissionamount);
                var finalamount = Number(totalamount) - Number(commissionamount);
                $('#CreditFinalAmount').val(finalamount);
                var receivedamount = Number($('#CreditReceivedAmount').val());
                var left = Number(finalamount) - Number(receivedamount);
                $('#CreditLeftAmount').val(left);
            }
            else {
                $('#ConsignmentTotalAmount').val(nettotal);
                var totalamount = $('#ConsignmentTotalAmount').val();
                var percentage = Number($('#ConsignmentPercentage').val());
                var commissionamount = Number(totalamount) * percentage * 0.01;
                $('#ConsignmentCommissionAmount').val(commissionamount);
                var finalamount = Number(totalamount) - Number(commissionamount);
                $('#ConsignmentFinalAmount').val(finalamount);
                var receivedamount = Number($('#ConsignmentReceivedAmount').val());
                var left = Number(finalamount) - Number(receivedamount);
                $('#ConsignmentLeftAmount').val(left);

            }
        }
        

    });

    $('#ConsignmentPercentage').focusout(function () {
        var otherchanges;
        if ($('#OtherCharges').val().length == 0) {
            otherchanges = 0;
        }
        else {
            otherchanges = Number($('#OtherCharges').val());
        }
        var nettotal = Number($('#netalltotal').val()) + Number(otherchanges);
        //var totalamount = Number($('#netalltotal').val());
        var percentage = Number($(this).val());
        var commissionamount = nettotal * percentage * 0.01;
        $('#ConsignmentCommissionAmount').val(commissionamount);

        var finalamount = nettotal - commissionamount;
        $('#ConsignmentFinalAmount').val(finalamount);
        var receivedamount = Number($('#ConsignmentReceivedAmount').val());
        var left = Number(finalamount) - Number(receivedamount);
        $('#ConsignmentLeftAmount').val(left);
    });

    $('#ConsignmentReceivedAmount').focusout(function () {

        var final = Number($('#ConsignmentFinalAmount').val());
        var receivedamount = Number($('#ConsignmentReceivedAmount').val());
        var leftamount = Number(final - receivedamount);
        $('#ConsignmentLeftAmount').val(leftamount);
        var receivedamount = Number($('#ConsignmentReceivedAmount').val());
        var left = Number(finalamount) - Number(receivedamount);
        $('#ConsignmentLeftAmount').val(left);
    });

    $('#CreditPercentage').focusout(function () {
        var otherchanges;
        if ($('#OtherCharges').val().length == 0) {
            otherchanges = 0;
        }
        else {
            otherchanges = Number($('#OtherCharges').val());
        }
        var nettotal = Number($('#netalltotal').val()) + Number(otherchanges);
        //var totalamount = Number($('#netalltotal').val());
        var percentage = Number($(this).val());
        var commissionamount = nettotal * percentage * 0.01;
        $('#CreditCommissionAmount').val(commissionamount);

        var finalamount = nettotal - commissionamount;
        $('#CreditFinalAmount').val(finalamount);
        var receivedamount = Number($('#CreditReceivedAmount').val());
        var left = Number(finalamount) - Number(receivedamount);
        $('#CreditLeftAmount').val(left);
    });

    $('#CreditReceivedAmount').focusout(function () {

        var final = Number($('#CreditFinalAmount').val());
        var receivedamount = Number($('#CreditReceivedAmount').val());
        var leftamount = Number(final - receivedamount);
        $('#CreditLeftAmount').val(leftamount);
        var receivedamount = Number($('#CreditReceivedAmount').val());
        var left = Number(finalamount) - Number(receivedamount);
        $('#CreditLeftAmount').val(left);
    });

});

function BindGrid() {
    $(document).ready(function () {

        $('#overlay').fadeIn();

        $('#tbl').dataTable().fnDestroy();
        $.ajax({
            type: "POST",
            url: listUrl,
            dataType: "json",
            data: { "Saleid": Saleid },
            success: function (data) {

                $('#overlay').fadeOut();

                oTable = $("#tbl").dataTable({
                    "columnDefs": [
                        {
                            className: "text-center", "targets": [6]
                        },
                        {
                            className: "text-center", "targets": [2, 3, 4, 5]
                        }
                    ],
                    "pagingType": "full_numbers",
                    "lengthMenu": [
                        [10, 25, 50, -1],
                        [10, 25, 50, "All"]
                    ],
                    responsive: true,
                    data: data,
                    "aaSorting": [[0, "asc"]],
                    "aoColumns": [
                        {
                            "mData": "No", "bSearchable": false, "bSortable": false, "width": "5%",
                            "mRender": function (data) {
                                return "<center>" + data.toString() + "</center>";
                            }
                        },
                        { "mData": "StockName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "Quantity", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "UnitSellingPrice", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "TotalSellingPrice", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "Discount", "bSearchable": true, "bSortable": true, "width": "10%" },

                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "20%",
                            "mRender": function (data, type, full) {

                                var actions = "";
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-warning' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i>&nbsp;Edit</a>&nbsp;&nbsp;";
                                del = "<a class='btn btn-danger' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i>&nbsp;Delete</a>";

                                actions += edit;
                                actions += del;

                                return actions;
                            }
                        },
                    ]
                });

                //oTable.on('click', 'tbody tr td:not(:last-child)', function (e) {
                //    var row = $(this).closest('tr');
                //    var data = oTable.fnGetData(row);

                //    View(data.Id);

                //});
                $('#tbl_wrapper').css("width", "100%");
            }
        });
    });
}

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
                location.reload();
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

function Edit(id) {

    ClearError()
    $.ajax({
        url: editUrl,
        type: 'POST',
        data: { "Id": id },
        success: function (data) {


            $('#hddetailId').val(data.Id);
            $('#ddl_stockid').val(data.StockId);
            $('#txt_qty').val(data.Quantity);
            $('#txt_unitprice').val(data.UnitSellingPrice);
            $('#txt_discount').val(data.Discount);

            $('#btnEdit').html('<i class="fa fa-edit"></i> Edit');

            var current = $('#hdstockqty').val();
            var editclickedstockqty = Number(current) + Number(data.Quantity);
            $('#Available-alert').text(editclickedstockqty+ " Available");
            $('#Available-alert').show();

            $('#hdstockqty').val(editclickedstockqty)

        },
    })
}

function Delete(id) {
    Swal.fire({
        title: "Are you sure to delete?",
        text: "Deleting may occur changes at other records",
        type: 'warning',
        showCancelButton: true,
        cancelButtonColor: '#ff6258',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        confirmButtonClass: "btn btn-primary",
        cancelButtonClass: "btn btn-danger",
    }).then((result) => {
        if (result.value) {

            $.ajax({
                url: deleteUrl,
                type: 'POST',
                data: { "id": id },
                success: function (data) {
                    if (data.MessageType == 1) {
                        Swal.fire({
                            title: data.Message,
                            text: "",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-primary",
                            type: "success"
                        }).then((result) => {
                            if (result.value) {
                                location.reload();
                            }
                        });
                    } else {
                        Swal.fire({
                            title: data.Message,
                            text: "",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-primary",
                            type: "error"
                        }).catch(swal.noop)
                    }

                }

            });


        }
    });
}

function GetModel() {
    var model = {};

    model.Id = $('#Id').val();
    model.ConsumerId = $('#ConsumerId').val();
    model.sSaleDate = $('#sSaleDate').val();
    model.OtherCharges = $('#OtherCharges').val();
    model.NetAmount = $('#netalltotal').val();
    model.PaymentTypeRole = $('#PaymentTypeRole').val();

    return model;
}


function ClearError() {
    $('#sSaleDate').closest(".form-control").removeClass("is-invalid");
    $('#sSaleDate-error').hide();

    $('#txt_unitprice').closest(".form-control").removeClass("is-invalid");
    $('#txt_unitprice-error').hide();
    $('#txt_qty').closest(".form-control").removeClass("is-invalid");
    $('#txt_qty-error').hide();
}

function ClearErrorCredit() {
    $('#sSaleDate').closest(".form-control").removeClass("is-invalid");
    $('#sSaleDate-error').hide();

    $('#txt_unitprice').closest(".form-control").removeClass("is-invalid");
    $('#txt_unitprice-error').hide();
    $('#txt_qty').closest(".form-control").removeClass("is-invalid");
    $('#txt_qty-error').hide();

    $('#sCreditFinalDate').closest(".form-control").removeClass("is-invalid");
    $('#sCreditFinalDate-error').hide();

    $('#CreditPercentage').closest(".form-control").removeClass("is-invalid");
    $('#CreditPercentage-error').hide();
    $('#CreditReceivedAmount').closest(".form-control").removeClass("is-invalid");
    $('#CreditReceivedAmount-error').hide();
}

function ClearErrorConsignment() {
    $('#sSaleDate').closest(".form-control").removeClass("is-invalid");
    $('#sSaleDate-error').hide();

    $('#txt_unitprice').closest(".form-control").removeClass("is-invalid");
    $('#txt_unitprice-error').hide();
    $('#txt_qty').closest(".form-control").removeClass("is-invalid");
    $('#txt_qty-error').hide();

    $('#sConsignmentFinalDate').closest(".form-control").removeClass("is-invalid");
    $('#sConsignmentFinalDate-error').hide();

    $('#ConsignmentPercentage').closest(".form-control").removeClass("is-invalid");
    $('#ConsignmentPercentage-error').hide();
    $('#ConsignmentReceivedAmount').closest(".form-control").removeClass("is-invalid");
    $('#ConsignmentReceivedAmount-error').hide();
}

function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}

function GetAddModel() {
    var modal = {};

    modal.SaleId = $('#Id').val();
    modal.Id = $('#hddetailId').val();
    modal.StockId = parseInt($('#ddl_stockid').val());
    modal.Discount = parseFloat($('#txt_discount').val() == "" ? "0" : $('#txt_discount').val());
    modal.Quantity = parseInt($('#txt_qty').val());
    modal.UnitSellingPrice = $('#txt_unitprice').val();
    modal.TotalSellingPrice = 0;
    modal.SubAmount = 0;

    return modal;
}

function updatenettotal() {
    var otherchanges;
    if ($('#OtherCharges').val().length == 0) {
        otherchanges = 0;
    }
    else {
        otherchanges = Number($('#OtherCharges').val());
    }
    var nettotal = Number($('#TotalSellingAmount').val()) + Number(otherchanges);
    $('#netalltotal').val(nettotal);
    $('#lblnetalltotal').text(nettotal);

}

function StockIdChangeEvent(id) {
    $('#txt_qty').val('');
    $('#hdstockqty').val('')
    $('#Available-alert').hide();
    if (id != "") {
        $.ajax({
            type: "GET",
            url: getStockUrl,
            data: { "id": id },
            success: function (data) {
                $('#hdstockqty').val(data.AvailableQuantity);
                $('#Available-alert').text(data.AvailableQuantity + " Available");
                var UnitSellingPrice = data.UnitSellingPrice;
                $('#txt_unitsellprice').val(UnitSellingPrice);
                $('#Available-alert').show();
            }
        })
        $('#ddl_stockid').closest(".form-control").removeClass("is-invalid");
        $('#ddl_stockid-error').hide();
    }
}