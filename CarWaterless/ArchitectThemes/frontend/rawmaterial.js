
$(document).ready(function () {

    BindGrid();

    $('#btnSearch').click(function () {
        BindGrid();
    });

    $('#btnReturn').click(function () {
        $('#txtreturnqty').closest(".form-control").removeClass("is-invalid");
        $('#txtreturnqty-error').hide();
        $('#txtreturnqty-error-max').hide();

        var txtreturnqty = $('#txtreturnqty').val();
        var max = Number($('#hdModalcqty').val());
        if (txtreturnqty != "") {
            var itxtreturnqty = Number(txtreturnqty)
            if (itxtreturnqty <= max) {
                $.ajax({
                    url: returnUrl,
                    type: 'POST',
                    data: { "id": $('#hdModalId').val(), "returnqty": txtreturnqty },
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
                                    window.location = "/RawMaterial/Index";
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

                        //if (data) {
                        //    $('#hdMessageType').val(data.MessageType);
                        //    $('#hdMessage').val(data.Message);

                        //    Swal.fire(
                        //        'Goods returned back',
                        //        'success'
                        //    ).then((result) => {
                        //        if (result.value) {
                        //            window.location = "/RawMaterial/Index";
                        //        }
                        //    });
                        //}

                        BindGrid();
                    }

                });
            }
            else {
                $('#txtreturnqty').closest(".form-control").addClass("is-invalid");
                $('#txtreturnqty-error-max').show();
            }
            
        }
        else {
            $('#txtreturnqty').closest(".form-control").addClass("is-invalid");
            $('#txtreturnqty-error').show();
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
                            className: "text-center", "targets": [5]
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
                        { "mData": "Name", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "CodeNo", "bSearchable": true, "bSortable": true, "width": "10%" },
                        {
                            "mData": "AvailableQuantity", "bSearchable": true, "bSortable": true, "width": "10%",
                            "mRender": function (data, type, full) {
                                return data + " " + full.UnitName;
                            }
                        },

                        { "mData": "UnitPrice", "bSearchable": true, "bSortable": true, "width": "10%" },
                        

                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "25%",
                            "mRender": function (data,type, full) {

                                var actions = "";

                                var returngoods = "";
                                var edit = "";
                                var del = "";

                                returngoods = "<a class='btn btn-info btn-sm' href='javascript:void(0)' data-toggle='modal' data-target='#exampleModalreturngoods' onclick=View('" + data.toString() + "','" + full.AvailableQuantity + "') title='Return Goods'><i class='fas fa-share-square'></i></a>&nbsp;&nbsp;";
                                edit = "<a class='btn btn-warning btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
                                del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i></a>";

                                if (full.AvailableQuantity != 0) {
                                    actions += returngoods;
                                    
                                }
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

function View(id,cqty) {
    $('#txtreturnqty').removeAttr('disabled');

    //$('#btnReturn').unbind('click')

    if (cqty == "0") {
        $("#txtreturnqty").attr("disabled", "disabled"); 

        //$('#btnReturn').bind('click', function (e) {
        //    e.preventDefault();
        //})
    }

    $('#hdModalId').val(id);
    $('#hdModalcqty').val(cqty);
    $('#lblcurrentqty').text(cqty);

}

function Edit(id) {
    window.location = "/RawMaterial/RawMaterial?id=" + id;
}

function Delete(id) {

    Swal.fire({
        title: "Are you sure to delete?",
        text: "Deleting Raw material may occur errors for existing payments.",
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
                                window.location = "/RawMaterial/Index";
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

    model.Name = $('#Name').val();
    model.UnitId = $('#UnitId').val();
    model.AvailableFlag = $("input[name='rdoAvailableQty']:checked").val();
    if ($('#chkIsActive').prop("checked") == true) {
        model.IsActive = true;
    }
    else {
        model.IsActive = false;
    }

    return model;
}


function ClearError() {
    $('#Name').closest(".form-group").removeClass("has-error");
    $('#Name-error').hide();
}

function Send(id,avaqty) {

    Swal.fire({
        title: "Are you sure to send to factory?",
        text: "",
        type: 'question',
        showCancelButton: true,
        cancelButtonColor: '#ff6258',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        confirmButtonClass: "btn btn-primary",
        cancelButtonClass: "btn btn-danger",
    }).then((result) => {
        if (result.value) {

            $.ajax({
                url: sendUrl,
                type: 'POST',
                data: { "Id": id, "avaqty": avaqty },
                success: function (data) {
                    if (data) {
                        $('#hdMessageType').val(data.MessageType);
                        $('#hdMessage').val(data.Message);

                        Swal.fire(
                            'Raw Materials Sent!',
                            'success'
                        ).then((result) => {
                            if (result.value) {
                                window.location = "/RawMaterial/Index";
                            }
                        });
                    }

                    BindGrid();
                }

            });


        }
    });
}

function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}