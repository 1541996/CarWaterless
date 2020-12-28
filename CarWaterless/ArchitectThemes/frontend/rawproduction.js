
$(document).ready(function () {

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
                            className: "text-center", "targets": [7]
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
                        { "mData": "RawMaterialName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "Type", "bSearchable": false, "bSortable": false, "width": "10%" },
                        { "mData": "SentQuantity", "bSearchable": false, "bSortable": false, "width": "5%" },
                        { "mData": "UsedQuantity", "bSearchable": false, "bSortable": false, "width": "5%" },
                        { "mData": "LeftQuantity", "bSearchable": false, "bSortable": false, "width": "5%" },
                        {
                            "mData": "sOperationDate", "bSearchable": true, "bSortable": true, "width": "15%"
                        },

                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "10%",
                            "mRender": function (data, type, full) {

                                var actions = "";
                                var del = "";
                                del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i></a>";

                                if (full.Type == "Used") {
                                    actions += del;
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
                                window.location = "/RawMaterial/RawProduction";
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

    model.RawMaterialId = $('#RawMaterialId').val();
    model.Year = $('#Year').val();
    model.Month = $('#Month').val();
    model.Type = $('#ddlTypeProduction').val();
    

    return model;
}

