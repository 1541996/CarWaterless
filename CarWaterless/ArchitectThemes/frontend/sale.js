
$(document).ready(function () {

    $('#mainsidebar').addClass('closed-sidebar');
    BindGrid();

    $('#btnSearch').click(function () {
        BindGrid();
    });
});

function BindGrid() {
    $(document).ready(function () {

        $('#overlay').fadeIn();

        $('#tbl').dataTable().fnDestroy();
        $.ajax({
            type: "POST",
            url: listUrl,
            data: GetModel(),
            dataType: "json",
            success: function (data) {

                $('#overlay').fadeOut();

                oTable = $("#tbl").dataTable({
                    "columnDefs": [
                        {
                            className: "text-center", "targets": [5,6]
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
                            "mData": "No", "bSearchable": false, "bSortable": false, "width": "3%",
                            "mRender": function (data) {
                                return "<center>" + data.toString() + "</center>";
                            }
                        },
                        { "mData": "ConsumerName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "SaleNo", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "PaymentTypeRoleName", "bSearchable": true, "bSortable": true, "width": "10%" },


                        { "mData": "NetAmount", "bSearchable": true, "bSortable": true, "width": "10%" },
                        {
                            "mData": "IsActive", "bSearchable": true, "bSortable": true, "width": "5%",
                            "mRender": function (data, type, full) {
                                if (data.toString() == "true") {
                                    return "<i class='fa fa-check-circle' style='color:green'></i>";
                                }
                                else {
                                    return "<i class='fa fa-times-circle' style='color:red'></i>";
                                }
                            }
                        },

                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "20%",
                            "mRender": function (data, type, full) {

                                var actions = "";
                                var view = "";
                                var edit = "";
                                var del = "";

                                var role = full.PaymentTypeRole;
                                if (role == 1) {
                                    view = "<a class='btn btn-info btn-sm' href='javascript:void(0)' data-toggle='modal' data-target='#exampleModalCashdown' onclick=ViewCashdown('" + data.toString() + "')><i class='fas fa-eye'></i></a>&nbsp;&nbsp;";
                                } else if (role == 2) {
                                    view = "<a class='btn btn-info btn-sm' href='javascript:void(0)' data-toggle='modal' data-target='#exampleModalConsignment' onclick=ViewConsignment('" + data.toString() + "')><i class='fas fa-eye'></i></a>&nbsp;&nbsp;";
                                } else if (role == 3) {
                                    view = "<a class='btn btn-info btn-sm' href='javascript:void(0)' data-toggle='modal' data-target='#exampleModalCredit' onclick=ViewCredit('" + data.toString() + "')><i class='fas fa-eye'></i></a>&nbsp;&nbsp;";
                                } else {
                                    view = "<a class='btn btn-info btn-sm' href='javascript:void(0)' data-toggle='modal' data-target='#exampleModalBrokenVoucher' onclick=ViewBrokenVoucher('" + data.toString() + "')><i class='fas fa-eye'></i></a>&nbsp;&nbsp;";
                                }

                                edit = "<a class='btn btn-warning btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "'," + full.PaymentTypeRole + ")><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";

                                del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i></a>";

                                actions += view;
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
                window.location = "/RawMaterial/Index";
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

function ViewCashdown(id) {
    $.ajax({
        url: viewUrl,
        type: 'POST',
        data: { "id": id },
        success: function (data) {
            $('#lblSaleNo').text(data.SaleNo);
            $('#lblSaleDate').text(data.sSaleDate);
            $('#lblConsumerName').text(data.ConsumerName);
            $('#lblTotalSellingAmount').text(data.TotalSellingAmount);
            $('#lblTotalDiscount').text(data.TotalDiscount);
            $('#lblOtherCharges').text(data.OtherCharges);
            $('#lblNetAmount').text(data.NetAmount);
            $('#lblCommissionAmountCashdown').text(data.CommissionAmount);
            $('#lblFinalAmountCashdown').text(data.FinalAmount);

            var lstDetail = {};
            lstDetail = data.lstDetailModel;
            for (var i in lstDetail) {
                var no = Number(i) + 1;
                var output;
                output += `<tr>
                        <td>
                            ${no}
                        </td>
                        <td>
                            ${lstDetail[i].StockName}
                        </td>
                        <td>
                            ${lstDetail[i].Quantity}
                         </td> 
                        <td>
                            ${lstDetail[i].UnitSellingPrice}
                         </td>
                        <td>
                            ${lstDetail[i].TotalSellingPrice}
                         </td>
                        <td>
                            ${lstDetail[i].Discount}
                         </td>
                    </tr>`;

            }

            $("#tbody_info").empty().append(output);
        },
    })

}

function ViewConsignment(id) {
    $.ajax({
        url: viewConsignmentUrl,
        type: 'POST',
        data: { "id": id },
        success: function (data) {
            $('#lblSaleNoConsignment').text(data.SaleNo);
            $('#lblSaleDateConsignment').text(data.sSaleDate);
            $('#lblConsumerNameConsignment').text(data.ConsumerName);
            $('#lblTotalSellingAmountConsignment').text(data.TotalSellingAmount);
            $('#lblTotalDiscountConsignment').text(data.TotalDiscount);
            $('#OtherChargesConsignment').text(data.OtherCharges);
            $('#lblNetAmountConsignment').text(data.NetAmount);
            $('#lblConsignmentFinalDateConsignment').text(data.saleConsignmentViewModel.sConsignmentFinalDate);
            $('#lblIsFullyPaidConsignment').text(data.saleConsignmentViewModel.FullyPaid);
            $('#lblReceivedAmountConsignment').text(data.saleConsignmentViewModel.ReceivedAmount);
            $('#lblLeftAmountConsignment').text(data.saleConsignmentViewModel.LeftAmount);
            $('#lblCommissionAmountConsignment').text(data.DiscountAmount);

            var lstDetail = {};
            lstDetail = data.lstDetailModel;
            for (var i in lstDetail) {
                var no = Number(i) + 1;
                var output;
                output += `<tr>
                        <td>
                            ${no}
                        </td>
                        <td>
                            ${lstDetail[i].StockName}
                        </td>
                        <td>
                            ${lstDetail[i].Quantity}
                         </td> 
                        <td>
                            ${lstDetail[i].UnitSellingPrice}
                         </td>
                        <td>
                            ${lstDetail[i].TotalSellingPrice}
                         </td>
                        <td>
                            ${lstDetail[i].Discount}
                         </td>
                    </tr>`;

            }

            $("#tbody_info_consignment").empty().append(output);
        },
    })

}

function ViewCredit(id) {
    $.ajax({
        url: viewCreditUrl,
        type: 'POST',
        data: { "id": id },
        success: function (data) {
            $('#lblSaleNoCredit').text(data.SaleNo);
            $('#lblSaleDateCredit').text(data.sSaleDate);
            $('#lblConsumerNameCredit').text(data.ConsumerName);
            $('#lblTotalSellingAmountCredit').text(data.TotalSellingAmount);
            $('#lblTotalDiscountCredit').text(data.TotalDiscount);
            $('#OtherChargesCredit').text(data.OtherCharges);
            $('#lblNetAmountCredit').text(data.NetAmount);
            $('#lblCreditFinalDateCredit').text(data.saleCreditViewModel.sCreditFinalDate);
            $('#lblIsFullyPaidCredit').text(data.saleCreditViewModel.FullyPaid);
            $('#lblReceivedAmountCredit').text(data.saleCreditViewModel.ReceivedAmount);
            $('#lblCommissionAmountCredit').text(data.saleCreditViewModel.CommissionAmount + "(" + data.saleCreditViewModel.Percentage+"%)");
            $('#lblFinalAmountCredit').text(data.saleCreditViewModel.FinalAmount);

            var lstDetail = {};
            lstDetail = data.lstDetailModel;
            for (var i in lstDetail) {
                var no = Number(i) + 1;
                var output;
                output += `<tr>
                        <td>
                            ${no}
                        </td>
                        <td>
                            ${lstDetail[i].StockName}
                        </td>
                        <td>
                            ${lstDetail[i].Quantity}
                         </td> 
                        <td>
                            ${lstDetail[i].UnitSellingPrice}
                         </td>
                        <td>
                            ${lstDetail[i].TotalSellingPrice}
                         </td>
                        <td>
                            ${lstDetail[i].Discount}
                         </td>
                    </tr>`;

            }

            $("#tbody_info_Credit").empty().append(output);
        },
    })

}

function Edit(id, role) {
    window.location = "/Sale/Edit?id=" + id + "&role=" + role;
}


function Delete(id) {

    Swal.fire({
        title: "Are you sure to delete?",
        text: "",
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
                data: { "Id": id },
                success: function (data) {
                    if (data) {
                        $('#hdMessageType').val(data.MessageType);
                        $('#hdMessage').val(data.Message);

                        Swal.fire(
                            'Deleted!',
                            'success'
                        ).then((result) => {
                            if (result.value) {
                                window.location = "/Sale/Index";
                            }
                        });
                    }

                    BindGrid();
                }

            });


        }
    });
}

function GetModel() {
    var model = {};

    model.sFromSaleDate = $('#sFromSaleDate').val();
    model.sToSaleDate = $('#sToSaleDate').val();
    model.ConsumerId = $('#ConsumerId').val();
    model.PaymentTypeRole = $('#ddlPaymentTypeRole').val();
    model.FullyPaidFlag = $("input[name='rdoFullyPaid']:checked").val();
    
    return model;
}

function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}
