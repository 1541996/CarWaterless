var saleid;
var role="";

$(document).ready(function () {
    saleid = $('#Id').val();

    $('#txt_qty').val("");
    $('#txt_unitsellprice').val("");
    $('#txt_discount').val("");
    $('#lblnetalltotal').text($('#netalltotal').val());

    //first tab
    $('#btnNextTab1').click(function () {
        $('#ConsumerId').closest(".form-control").removeClass("is-invalid");
        $('#ConsumerId-error').hide();
        $('#ddlPaymentTypeRole').closest(".form-control").removeClass("is-invalid");
        $('#ddlPaymentTypeRole-error').hide();
        $('#SaleNo').closest(".form-control").removeClass("is-invalid");
        $('#SaleNo-error').hide();

        var customerid = $('#ConsumerId').val();
        var ddlPaymentTypeRole = $('#ddlPaymentTypeRole').val();
        var SaleNo = $('#SaleNo').val();
        if (customerid != "") {
            if (ddlPaymentTypeRole != "0") {
                if (SaleNo != "") {
                    $('#tab-c1-0').removeClass('active');
                    $('#tab-c1-1').removeClass('disabled');
                    $('#tab-animated1-0').removeClass('active');

                    $('#tab-c1-0').addClass('disabled');
                    $('#tab-c1-1').addClass('active');
                    $('#tab-animated1-1').addClass('active');
                }
                else {
                    $('#SaleNo').closest(".form-control").addClass("is-invalid");
                    $('#SaleNo-error').show();
                }
            }
            else {
                $('#ddlPaymentTypeRole').closest(".form-control").addClass("is-invalid");
                $('#ddlPaymentTypeRole-error').show();
            }
        }
        else {
            $('#ConsumerId').closest(".form-control").addClass("is-invalid");
            $('#ConsumerId-error').show();
        }
    });
    //end first tab

    //second tab
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

    $('#btnBackTab2').click(function () {
        $('#tab-c1-1').removeClass('active');
        $('#tab-c1-0').removeClass('disabled');
        $('#tab-animated1-1').removeClass('active');

        $('#tab-c1-1').addClass('disabled');
        $('#tab-c1-0').addClass('active');
        $('#tab-animated1-0').addClass('active');

        $('#Quantity-exceed').hide();
        $('#Available-alert').hide();
        //$('#sSaleDate').val("");
        //$('#txt_qty').val("");
        //$('#txt_unitsellingprice').val("");
        //$('#othercharges').val("");
        //inventoryCartsale.clearCart();
        //displayCart();
        ClearError();
    });

    $('#btnNextTab2').click(function () {
        ClearError();
        $('#Quantity-exceed').hide();
        $('#Available-alert').hide();
        var isvalid = false;

        var sSaleDate = $('#sSaleDate').val();
        if (sSaleDate != "") {
            if (inventoryCartsale.listCart().length > 0) {
                isvalid = true;
            }
            else {
                Swal.fire({
                    title: "Please Add Item",
                    text: "",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-primary",
                    type: "error"
                }).then((result) => {
                    ClearTextBox();
                });
            }
        }
        else {
            $('#sSaleDate').closest(".form-control").addClass("is-invalid");
            $('#sSaleDate-error').show();
        }

        if (isvalid == true) {
            role = $('#ddlPaymentTypeRole').val();

            if (role == "1") {
                $('#credit_div').hide();
                $('#consignment_div').hide();
                $('#cashdown_div').show();

                $('#lbltab3').text("Cash down Form");
                $('#CashdownTotalAmount').val($('#netalltotal').val());
               

            } else if (role == "2") {
                $('#credit_div').hide();
                $('#cashdown_div').hide();
                    $('#consignment_div').show();
                    

                    $('#lbltab3').text("Consignment Form");
                    $('#ConsignmentSaleDate').val($('#sSaleDate').val());
                    $('#ConsignmentTotalAmount').val($('#netalltotal').val());
                    //var totalamount = Number($('#netalltotal').val());
                    //var receivedamount = Number($('#ConsignmentReceivedAmount').val());
                    //var leftamount = Number(totalamount - receivedamount);
                    //$('#ConsignmentLeftAmount').val(leftamount);
                }
                else if (role == "3") {
                    $('#consignment_div').hide();
                    $('#cashdown_div').hide();
                $('#credit_div').show();

                    $('#lbltab3').text("Credit Form");
                    $('#CreditSaleDate').val($('#sSaleDate').val());
                    $('#CreditTotalAmount').val($('#netalltotal').val());
                    //var totalamount = Number($('#netalltotal').val());
                    //var receivedamount = Number($('#CreditReceivedAmount').val());
                    //var leftamount = Number(totalamount - receivedamount);
                    //$('#CreditLeftAmount').val(leftamount);
                }
                else {

                }

                $('#tab-c1-1').removeClass('active');
                $('#tab-c1-2').removeClass('disabled');
                $('#tab-animated1-1').removeClass('active');

                $('#tab-c1-1').addClass('disabled');
                $('#tab-c1-2').addClass('active');
                $('#tab-animated1-2').addClass('active');
            }
            
        
    });
    //end second tab

    //third tab
    $('#btnBackTab3').click(function () {
        $('#tab-c1-2').removeClass('active');
        $('#tab-c1-1').removeClass('disabled');
        $('#tab-animated1-2').removeClass('active');

        $('#tab-c1-2').addClass('disabled');
        $('#tab-c1-1').addClass('active');
        $('#tab-animated1-1').addClass('active');
    });

    $('#btnNextTab3').click(function () {
        if (role == "1") {
            ClearErrorCashdown();
            var percentage = $('#DiscountPercentage').val();
            var isvalid = false;
            if (percentage != "") {
                isvalid = true;
            } else {
                $('#DiscountPercentage').closest(".form-control").addClass("is-invalid");
                $('#DiscountPercentage-error').show();
            }
            if (isvalid == true) {

                $('#lblCashdownConfirmConsumerName').text($("#ConsumerId option:selected").text());
                $('#lblCashdownConfirmPaymentName').text($("#ddlPaymentTypeRole option:selected").text());
                $('#lblCashdownConfirmSaleNo').text($("#SaleNo").val());
                $('#lblCashdownConfirmSaleDate').text($('#sSaleDate').val());
                $('#lblCashdownConfirmTotalAmount').text($('#netalltotal').val());
                $('#lblCashdownConfirmCommissionPercentage').text($('#DiscountPercentage').val() + "%");
                $('#lblCashdownConfirmCommissionAmount').text($('#CashdownCommissionAmount').val());
                $('#lblCashdownConfirmFinalAmount').text($('#CashdownFinalAmount').val());

                

                $('#tab-c1-2').removeClass('active');
                $('#tab-c1-3').removeClass('disabled');
                $('#tab-animated1-2').removeClass('active');
                $('#tab-c1-2').addClass('disabled');
                $('#tab-c1-3').addClass('active');
                $('#tab-animated1-3').addClass('active');

                $('#cashdown_confirm').show();
                $('#credit_confirm').hide();
                $('#consignment_confirm').hide();
                $('#informalvoucher_confirm').hide();
            }
        }
        else if (role == "2") {
            ClearErrorConsignment();
            var finaldate = $('#sConsignmentFinalDate').val();
            var receivedamount = $('#ConsignmentReceivedAmount').val();
            var percentage = $('#ConsignmentPercentage').val();
            var isvalid = false;
            if (finaldate != "") {
                if (receivedamount != "") {
                    if (percentage != "") {
                        isvalid = true;
                    } else {
                        $('#ConsignmentPercentage').closest(".form-control").addClass("is-invalid");
                        $('#ConsignmentPercentage-error').show();
                    }
                } else {
                    $('#ConsignmentReceivedAmount').closest(".form-control").addClass("is-invalid");
                    $('#ConsignmentReceivedAmount-error').show();
                }
            } else {
                $('#sConsignmentFinalDate').closest(".form-control").addClass("is-invalid");
                $('#sConsignmentFinalDate-error').show();
            }
            if (isvalid == true) {
                $('#lblConsignConfirmConsumerName').text($("#ConsumerId option:selected").text());
                $('#lblConsignConfirmPaymentName').text($("#ddlPaymentTypeRole option:selected").text());
                $('#lblConsignConfirmSaleNo').text($("#SaleNo").val());
                $('#lblConsignConfirmSaleDate').text($('#sSaleDate').val());
                $('#lblConsignConfirmFinalDate').text($('#sConsignmentFinalDate').val());
                $('#lblConsignConfirmTotalAmount').text($('#netalltotal').val());
                $('#lblConsignConfirmReceivedAmount').text($('#ConsignmentReceivedAmount').val());
                $('#lblConsignConfirmCommissionPercentage').text($('#ConsignmentPercentage').val() +"%");
                $('#lblConsignConfirmCommissionAmount').text($('#ConsignmentCommissionAmount').val());
                $('#lblConsignConfirmFinalAmount').text($('#ConsignmentFinalAmount').val());
                $('#lblConsignConfirmLeftAmount').text($('#ConsignmentLeftAmount').val());

                $('#tab-c1-2').removeClass('active');
                $('#tab-c1-3').removeClass('disabled');
                $('#tab-animated1-2').removeClass('active');

                $('#tab-c1-2').addClass('disabled');
                $('#tab-c1-3').addClass('active');
                $('#tab-animated1-3').addClass('active');

                $('#cashdown_confirm').hide();
                $('#credit_confirm').hide();
                $('#consignment_confirm').show();
                $('#informalvoucher_confirm').hide();
            }

            

        }
        else if (role == "3") {
            ClearErrorCredit();
            var finaldate = $('#sCreditFinalDate').val();
            var percentage = $('#CreditPercentage').val();
            var receivedamount = $('#CreditReceivedAmount').val();
            var isvalid = false;
            if (finaldate != "") {
                if (percentage != "") {
                    if (receivedamount != "") {
                        isvalid = true;
                    } else {
                        $('#CreditReceivedAmount').closest(".form-control").addClass("is-invalid");
                        $('#CreditReceivedAmount-error').show();
                    }
                } else {
                    $('#CreditPercentage').closest(".form-control").addClass("is-invalid");
                    $('#CreditPercentage-error').show();
                }
            } else {
                $('#sCreditFinalDate').closest(".form-control").addClass("is-invalid");
                $('#sCreditFinalDate-error').show();
            }

            if (isvalid == true) {
                $('#lblCreditConfirmConsumerName').text($("#ConsumerId option:selected").text());
                var rtype = $("input[name='rdoRemarkType']:checked").val();
                if (rtype == "credit") {
                    $('#lblCreditConfirmPaymentName').text($("#ddlPaymentTypeRole option:selected").text());
                } else {
                    $('#lblCreditConfirmPaymentName').text($("#ddlPaymentTypeRole option:selected").text() + "(Informal Voucher)");
                }
                $('#lblCreditConfirmSaleNo').text($("#SaleNo").val());
                $('#lblCreditConfirmSaleDate').text($('#sSaleDate').val());
                $('#lblCreditConfirmFinalDate').text($('#sCreditFinalDate').val());
                $('#lblCreditConfirmTotalAmount').text($('#netalltotal').val());
                $('#lblCreditConfirmReceivedAmount').text($('#CreditReceivedAmount').val());
                $('#lblCreditConfirmLeftAmount').text($('#CreditLeftAmount').val());
                $('#lblCreditConfirmCommissionPercentage').text($('#CreditPercentage').val() + "%");
                $('#lblCreditConfirmCommissionAmount').text($('#CreditCommissionAmount').val());
                $('#lblCreditConfirmFinalAmount').text($('#CreditFinalAmount').val());

                $('#tab-c1-2').removeClass('active');
                $('#tab-c1-3').removeClass('disabled');
                $('#tab-animated1-2').removeClass('active');

                $('#tab-c1-2').addClass('disabled');
                $('#tab-c1-3').addClass('active');
                $('#tab-animated1-3').addClass('active');

                $('#cashdown_confirm').hide();
                $('#credit_confirm').show();
                $('#consignment_confirm').hide();
                $('#informalvoucher_confirm').hide();
            }

        }
        else {
            ClearErrorBrokenVoucher();
            var finaldate = $('#sBrokenVoucherFinalDate').val();
            var receivedamount = $('#BrokenVoucherReceivedAmount').val();
            var isvalid = false;
            if (finaldate != "") {
                if (receivedamount != "") {
                    isvalid = true;
                } else {
                    $('#BrokenVoucherReceivedAmount').closest(".form-control").addClass("is-invalid");
                    $('#BrokenVoucherReceivedAmount-error').show();
                }
            } else {
                $('#sBrokenVoucherFinalDate').closest(".form-control").addClass("is-invalid");
                $('#sBrokenVoucherFinalDate-error').show();
            }
            if (isvalid == true) {
                $('#lblConsignConfirmConsumerName').text($("#ConsumerId option:selected").text());
                $('#lblConsignConfirmPaymentName').text($("#ddlPaymentTypeRole option:selected").text());
                $('#lblConsignConfirmSaleDate').text($('#sSaleDate').val());
                $('#lblConsignConfirmFinalDate').text($('#sConsignmentFinalDate').val());
                $('#lblConsignConfirmTotalAmount').text($('#netalltotal').val());
                $('#lblConsignConfirmReceivedAmount').text($('#ConsignmentReceivedAmount').val());
                $('#lblConsignConfirmLeftAmount').text($('#ConsignmentLeftAmount').val());

                $('#tab-c1-2').removeClass('active');
                $('#tab-c1-3').removeClass('disabled');
                $('#tab-animated1-2').removeClass('active');

                $('#tab-c1-2').addClass('disabled');
                $('#tab-c1-3').addClass('active');
                $('#tab-animated1-3').addClass('active');

                $('#cashdown_confirm').hide();
                $('#credit_confirm').hide();
                $('#consignment_confirm').show();
                $('#informalvoucher_confirm').hide();
            }


        }
        
    });
    //end third tab

    

    //final tab
    $('#btnBackTab4').click(function () {
        $('#tab-c1-3').removeClass('active');
        $('#tab-c1-2').removeClass('disabled');
        $('#tab-animated1-3').removeClass('active');

        $('#tab-c1-3').addClass('disabled');
        $('#tab-c1-2').addClass('active');
        $('#tab-animated1-2').addClass('active');

    });

    $('#btnCheckOut').click(function () {
        if (role == "1") {
            checkout();
        }
        else if(role == "2"){
            checkoutconsignment();
        }
        else if (role == "3") {
            checkoutcredit();
        }
        else if (role == "4") {
            checkoutbrokenvoucher();
        }
    });
    //end final tab

    $('#btnSave').click(function () {
        ClearError();

        var sPurchaseDate = $('#sPurchaseDate').val();
        if (sPurchaseDate != "") {
            $.ajax({
                type: "POST",
                url: savePurchaseUrl,
                data: GetModel(),
                success: function (data) {
                    showMessage(data.MessageType, data.Message);

                }
            })
        }
        else {
            $('#sPurchaseDate').closest(".form-control").addClass("is-invalid");
            $('#sPurchaseDate-error').show();
        }


    });

    $('#btnEdit').click(function () {
        ClearError();

        var unitprice = $('#txt_unitprice').val();
        var quantity = $('#txt_qty').val();
        var discount = $('#txt_discount').val();
        if (quantity != "") {
            if (unitprice != "") {
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
                $('#UnitPrice-error').show();
            }
        }
        else {
            $('#txt_qty').closest(".form-control").addClass("is-invalid");
            $('#Quantity-error').show();
        }


    });

    $('#OtherCharges').focusout(function () {
        updatenettotal();
    });

    $('#ConsignmentReceivedAmount').focusout(function () {
        var totalamount = Number($('#netalltotal').val());
        var final = Number($('#ConsignmentFinalAmount').val());
        var receivedamount = Number($('#ConsignmentReceivedAmount').val());
        var leftamount = Number(final - receivedamount);
        $('#ConsignmentLeftAmount').val(leftamount);
    });

    $('#CreditPercentage').focusout(function () {
        var totalamount = Number($('#netalltotal').val());
        var percentage = Number($(this).val());
        var commissionamount = totalamount * percentage * 0.01;
        $('#CreditCommissionAmount').val(commissionamount);

        var finalamount = totalamount - commissionamount;
        $('#CreditFinalAmount').val(finalamount);

    });

    $('#DiscountPercentage').focusout(function () {
        var totalamount = Number($('#netalltotal').val());
        var percentage = Number($(this).val());
        var commissionamount = totalamount * percentage * 0.01;
        $('#CashdownCommissionAmount').val(commissionamount);

        var finalamount = totalamount - commissionamount;
        $('#CashdownFinalAmount').val(finalamount);

    });

    $('#ConsignmentPercentage').focusout(function () {
        var totalamount = Number($('#netalltotal').val());
        var percentage = Number($(this).val());
        var commissionamount = totalamount * percentage * 0.01;
        $('#ConsignmentCommissionAmount').val(commissionamount);

        var finalamount = totalamount - commissionamount;
        $('#ConsignmentFinalAmount').val(finalamount);

    });

    $('#CreditReceivedAmount').focusout(function () {
        var finalamount = Number($('#CreditFinalAmount').val());
        var receivedamount = Number($('#CreditReceivedAmount').val());
        var leftamount = Number(finalamount - receivedamount);
        $('#CreditLeftAmount').val(leftamount);

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
            data: { "saleid": saleid },
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
                        { "mData": "RawMaterialName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "Quantity", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "UnitPrice", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "Amount", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "Discount", "bSearchable": true, "bSortable": true, "width": "10%" },

                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "20%",
                            "mRender": function (data) {

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
            $('#ddl_rawmaterialid').val(data.RawMaterialId);
            $('#txt_qty').val(data.Quantity);
            $('#txt_unitprice').val(data.UnitPrice);
            $('#txt_discount').val(data.Discount);

            $('#btnEdit').text('<i class="fa fa-plus"></i> Edit');
        },
    })
}

function Delete(id) {
    Swal.fire({
        title: "Are you sure to delete?",
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
    model.SupplierId = $('#SupplierId').val();
    model.sPurchaseDate = $('#sPurchaseDate').val();
    model.OtherCharges = $('#OtherCharges').val();
    model.NetAmount = $('#netalltotal').val();

    return model;
}


function ClearError() {
    $('#sPurchaseDate').closest(".form-control").removeClass("is-invalid");
    $('#sPurchaseDate-error').hide();

    $('#txt_unitprice').closest(".form-control").removeClass("is-invalid");
    $('#txt_unitprice-error').hide();
    $('#ddl_stockid').closest(".form-control").removeClass("is-invalid");
    $('#ddl_stockid-error').hide();
    $('#txt_qty').closest(".form-control").removeClass("is-invalid");
    $('#txt_qty-error').hide();
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

    modal.PurchaseId = $('#Id').val();
    modal.Id = parseInt($('#hddetailId').val());
    modal.RawMaterialId = parseInt($('#ddl_rawmaterialid').val());
    modal.Discount = parseFloat($('#txt_discount').val() == "" ? "0" : $('#txt_discount').val());
    modal.Quantity = parseInt($('#txt_qty').val());
    modal.UnitPrice = $('#txt_unitprice').val();
    modal.Amount = 0;
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

function ClearErrorConsignment() {
    $('#sConsignmentFinalDate').closest(".form-control").removeClass("is-invalid");
    $('#sConsignmentFinalDate-error').hide();
    $('#ConsignmentReceivedAmount').closest(".form-control").removeClass("is-invalid");
    $('#ConsignmentReceivedAmount-error').hide();
    $('#ConsignmentPercentage').closest(".form-control").removeClass("is-invalid");
    $('#ConsignmentPercentage-error').hide();
}

function ClearErrorCredit() {
    $('#sCreditFinalDate').closest(".form-control").removeClass("is-invalid");
    $('#sCreditFinalDate-error').hide();
    $('#CreditReceivedAmount').closest(".form-control").removeClass("is-invalid");
    $('#CreditReceivedAmount-error').hide();
    $('#CreditPercentage').closest(".form-control").removeClass("is-invalid");
    $('#CreditPercentage-error').hide();
}

function ClearErrorBrokenVoucher() {
    $('#sBrokenVoucherFinalDate').closest(".form-control").removeClass("is-invalid");
    $('#sBrokenVoucherFinalDate-error').hide();
    $('#BrokenVoucherReceivedAmount').closest(".form-control").removeClass("is-invalid");
    $('#BrokenVoucherReceivedAmount-error').hide();
}

function ClearErrorCashdown() {
    $('#DiscountPercentage').closest(".form-control").removeClass("is-invalid");
    $('#DiscountPercentage-error').hide();
}