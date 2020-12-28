
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
                            className: "text-center", "targets": [4,5]
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
                        { "mData": "SupplierName", "bSearchable": true, "bSortable": true, "width": "20%" },
                        { "mData": "PurchaseNo", "bSearchable": true, "bSortable": true, "width": "15%" },
                       

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
                            "mRender": function (data) {

                                var actions = "";
                                var view = "";
                                var edit = "";
                                var del = "";

                                view = "<a class='btn btn-info btn-sm' href='javascript:void(0)' data-toggle='modal' data-target='#exampleModal' onclick=View('" + data.toString() + "')><i class='fas fa-eye'></i></a>&nbsp;&nbsp;";
                                edit = "<a class='btn btn-warning btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
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

function View(id) {

    $.ajax({
        url: viewUrl,
        type: 'POST',
        data: { "id": id },
        success: function (data) {
            $('#lblPurchaseNo').text(data.PurchaseNo);
            $('#lblSupplierName').text(data.SupplierName);
            $('#lblTotalAmount').text(data.TotalAmount);
            $('#lblTotalDiscount').text(data.TotalDiscount);
            $('#lblOtherCharges').text(data.OtherCharges);
            $('#lblNetAmount').text(data.NetAmount);

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
                            ${lstDetail[i].RawMaterialName}
                        </td>
                        <td>
                            ${lstDetail[i].Quantity}
                         </td> 
                        <td>
                            ${lstDetail[i].UnitPrice}
                         </td>
                        <td>
                            ${lstDetail[i].Amount}
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

function Edit(id) {
    window.location = "/Purchase/Edit?id=" + id;
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
                                window.location = "/Purchase/Index";
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

    model.sFromPurchaseDate = $('#sFromPurchaseDate').val();
    model.sToPurchaseDate = $('#sToPurchaseDate').val();
    model.SupplierId = $('#SupplierId').val();
    
    return model;
}

function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}
