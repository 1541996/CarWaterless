var purchaseid;
$(document).ready(function () {
    purchaseid = $('#Id').val();

    $('#Quantity').val("");
    $('#UnitPrice').val("");
    $('#lblnetalltotal').text($('#netalltotal').val());


    BindGrid();

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
});

function BindGrid() {
    $(document).ready(function () {

        $('#overlay').fadeIn();

        $('#tbl').dataTable().fnDestroy();
        $.ajax({
            type: "POST",
            url: listUrl,
            dataType: "json",
            data: { "purchaseid": purchaseid },
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
                            "mRender": function (data,type,full) {

                                var actions = "";
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-warning' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i>&nbsp;Edit</a>&nbsp;&nbsp;";
                                del = "<a class='btn btn-danger' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i>&nbsp;Delete</a>";

                                if (full.RawProductionEditAllow == "Yes") {
                                    actions += edit;
                                    actions += del;
                                }
                                else {
                                    actions = "<p>Already in production</p>";
                                }
                               

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

            $('#btnEdit').html('<i class="fa fa-edit"></i> Edit');
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
    modal.Id = $('#hddetailId').val();
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
    var nettotal = Number($('#TotalAmount').val()) + Number(otherchanges);
    $('#netalltotal').val(nettotal);
    $('#lblnetalltotal').text(nettotal);

}