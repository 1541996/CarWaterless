
$(document).ready(function () {

    $('#mainsidebar').addClass('closed-sidebar');

    BindGrid();

    $('#btnSearch').click(function () {
        BindGrid();
    });

    $('#btnSave').click(function () {
        $('#txtPaidAmount').closest(".form-control").removeClass("is-invalid");
        $('#txtPaidAmount-error').hide();
        $('#txtPaidAmount-error-max').hide();

        var txtPaidAmount = $('#txtPaidAmount').val();
        var max = Number($('#hdModalleft').val());
        if (txtPaidAmount != "") {
            var itxtPaidAmount = Number(txtPaidAmount)
            if (itxtPaidAmount <= max) {
                $.ajax({
                    url: saveModalUrl,
                    type: 'POST',
                    data: { "id": $('#hdModalId').val(), "txtPaidAmount": txtPaidAmount },
                    success: function (data) {
                        if (data.MessageType == 1) {
                            Swal.fire({
                                title: data.Message,
                                text: "",
                                buttonsStyling: false,
                                confirmButtonClass
                                    : "btn btn-primary",
                                type: "success"
                            }).then((result) => {
                                if (result.value) {
                                    window.location = "/ConsumerPayment/Credit";
                                }
                            });
                        }
                        else {
                            Swal.fire({
                                title: data.Message,
                                text: "",
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-primary",
                                type: "error"
                            }).catch(swal.noop)
                        }


                        BindGrid();
                    }

                });
            }
            else {
                $('#txtPaidAmount').closest(".form-control").addClass("is-invalid");
                $('#txtPaidAmount-error-max').show();
            }

        }
        else {
            $('#txtPaidAmount').closest(".form-control").addClass("is-invalid");
            $('#txtPaidAmount-error').show();
        }

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
                            className: "text-center", "targets": [9,10]
                        },
                        {
                            className: "text-right", "targets": [4,5,6,7,8]
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
                            "mData": "No", "bSearchable": false, "bSortable": false, "width": "2%",
                            "mRender": function (data) {
                                return "<center>" + data.toString() + "</center>";
                            }
                        },
                        { "mData": "ConsumerName", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "sSaleDate", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "sCreditFinalDate", "bSearchable": true, "bSortable": true, "width": "10%" },


                        { "mData": "TotalAmount", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "CommissionAmount", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "FinalAmount", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "ReceivedAmount", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "LeftAmount", "bSearchable": true, "bSortable": true, "width": "5%" },
                        {
                            "mData": "IsFullyPaid", "bSearchable": true, "bSortable": true, "width": "5%",
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
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "15%",
                            "mRender": function (data) {

                                var actions = "";
                                var pay = "";
                                var edit = "";
                                var confirm = "";

                                pay = "<a class='btn btn-info btn-sm' href='javascript:void(0)' data-toggle='modal' data-target='#exampleModalPay' onclick=Pay('" + data.toString() + "')><i class='fas fa-dollar-sign'></i></a>&nbsp;&nbsp;";
                                edit = "<a class='btn btn-warning btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
                                confirm = "<a class='btn btn-success btn-sm' href='javascript:void(0)' onclick=Confirm('" + data.toString() + "')><i class='fas fa-check-square'></i></a>";

                                actions += pay;
                                actions += edit;
                                actions += confirm;

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

function Pay(id) {

    $.ajax({
        url: viewUrl,
        type: 'POST',
        data: { "id": id },
        success: function (data) {
            $('#hdModalId').val(id);

            $('#lblTotalAmount').text(data.TotalAmount);
            $('#lblReceivedAmount').text(data.ReceivedAmount);
            $('#lblLeftAmount').text(data.LeftAmount);
            $('#hdModalleft').val(data.LeftAmount);

            $('#lblTotalDiscount').text(data.TotalDiscount);
            $('#lblOtherCharges').text(data.OtherCharges);
            $('#lblNetAmount').text(data.NetAmount);

           
        },
    })

}

function Edit(id) {
    window.location = "/ConsumerPayment/CreditEdit?id=" + id;
}

function Confirm(id) {

    Swal.fire({
        title: "Fully Paid Confirmed?",
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
                url: confirmUrl,
                type: 'POST',
                data: { "Id": id },
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
                                window.location = "/ConsumerPayment/Credit";
                            }
                        });
                    }
                    else if (data.MessageType == 2) {
                        Swal.fire({
                            title: message,
                            text: "",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-primary",
                            type: "error"
                        }).catch(swal.noop)
                    }

                    BindGrid();
                }

            });


        }
    });
}

function GetModel() {
    var model = {};

    model.sFromDate = $('#sFromDate').val();
    model.sToDate = $('#sToDate').val();
    model.ConsumerId = $('#ConsumerId').val();
    model.FullyPaid = $("input[name='rdoFullyPaid']:checked").val();
    model.IsOverTime = false;
    return model;
}

function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}
