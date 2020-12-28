
$(document).ready(function () {
    BindGrid();

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
        var BankPayNo = $('#BankPayNo').val();
        if (BankPayNo != "") {
            isvalid = true;
        }
        else {
            $('#BankPayNo').closest(".form-control").addClass("is-invalid");
            $('#BankPayNo-error').show();
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

    $('#btnSearch').click(function () {
        BindGrid();
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
                window.location = "/Consumer/Payment";
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

function BindGrid() {
    $(document).ready(function () {

        $('#overlay').fadeIn();

        $('#tbl').dataTable().fnDestroy();
        $.ajax({
            type: "POST",
            url: listUrl,
            data: GetFilterModel(),
            dataType: "json",
            success: function (data) {

                $('#overlay').fadeOut();

                oTable = $("#tbl").dataTable({
                    "columnDefs": [
                        {
                            className: "text-center", "targets": [4, 5]
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
                        { "mData": "ConsumerName", "bSearchable": true, "bSortable": true, "width": "20%" },
                        { "mData": "BankPayName", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "BankPayNo", "bSearchable": true, "bSortable": true, "width": "20%" },
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
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-warning btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
                                del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i></a>";

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

function GetModel() {
    var model = {};
    var id = $('#Id').val();
    if (id != "") {
        model.Id = id;
    }
    model.ConsumerId = $('#ConsumerId').val();
    model.BankPayId = $('#BankPayId').val();
    model.BankPayNo = $('#BankPayNo').val();
    if ($('#chkIsActive').prop("checked") == true) {
        model.IsActive = true;
    }
    else {
        model.IsActive = false;
    }
    return model;
}

function GetFilterModel() {
    var model = {};
    model.ConsumerId = $('#FConsumerId').val();
    model.BankPayId = $('#FBankPayId').val();
    if ($('#FchkIsActive').prop("checked") == true) {
        model.IsActive = true;
    }
    else {
        model.IsActive = false;
    }
    return model;
}

function Delete(id) {

    Swal.fire({
        title: "Are you sure to delete?",
        text: "Deleting bank/pay may occur errors for existing payments.",
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
                                window.location = "/Consumer/Payment";
                            }
                        });
                    }

                    BindGrid();
                }

            });


        }
    });
}

function Edit(id) {
    $.ajax({
        url: editUrl,
        type: 'POST',
        data: { "Id": id },
        success: function (data) {
            $("#Id").val(data.Id);
            $('#ConsumerId').val(data.ConsumerId);
            $('#BankPayId').val(data.BankPayId);
            $('#BankPayNo').val(data.BankPayNo);


            $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');

            //$('body, html').animate({ scrollTop: $(patientFormTop).offset().top }, 'slow');
            $('html, body').animate({
                scrollTop: 0
            }, 800);
        },
    })
}

function ClearError() {
    $('#BankPayNo').closest(".form-control").removeClass("is-invalid");
    $('#BankPayNo-error').hide();
}