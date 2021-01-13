
$(document).ready(function () {

    BindGrid();

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {

        ClearError();

        var isvalid = false;
        var Name = $('#Name').val();
        var TownshipCode = $('#TownshipCode').val();
        if (Name != "") {
            if (TownshipCode != "") {
                isvalid = true;
            }
            else {
                $('#TownshipCode').closest(".form-control").addClass("is-invalid");
                $('#TownshipCode-error').show();
            }
        }
        else {
            $('#Name').closest(".form-control").addClass("is-invalid");
            $('#Name-error').show();
        }
        if (isvalid == true) {
            $.ajax({
                type: "POST",
                url: saveUrl,
                data: GetModel(),
                success: function (data) {
                    showMessage(data.MessageType, data.Message);
                    BindGrid();
                }
            })
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
            dataType: "json",
            success: function (data) {

                $('#overlay').fadeOut();

                oTable = $("#tbl").dataTable({
                    "columnDefs": [
                        {
                            className: "text-center", "targets": [3]
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
                        { "mData": "Name", "bSearchable": true, "bSortable": true, "width": "30%" },
                        { "mData": "TownshipCode", "bSearchable": true, "bSortable": true, "width": "20%" },

                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "30%",
                            "mRender": function (data) {

                                var actions = "";
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-warning' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
                                del = "<a class='btn btn-danger' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i></a>";

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
                window.location = "/AdminSetup/Township";
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
    $.ajax({
        url: editUrl,
        type: 'POST',
        data: { "Id": id },
        success: function (data) {
            $("#Id").val(data.Id);
            $('#Name').val(data.Name);
            $('#TownshipCode').val(data.TownshipCode);


            $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');
            $('html, body').animate({
                scrollTop: 0
            }, 800);
        },
    })
}

function Delete(id) {

    Swal.fire({
        title: "Are you sure to delete?",
        text: "Deleting Townships may occur errors for existing records.",
        type: 'warning',
        showCancelButton: true,
        cancelButtonTownship: '#ff6258',
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
                                window.location = "/AdminSetup/Township";
                            }
                        });
                    }

                    BindGird();
                }

            });


        }
    });
}

function GetModel() {
    var model = {};
    var id = $('#Id').val();
    if (id != "") {
        model.Id = id;
    }
    model.Name = $('#Name').val();
    model.TownshipCode = $('#TownshipCode').val();

    return model;
}


function ClearError() {
    $('#Name').closest(".form-control").removeClass("is-invalid");
    $('#Name-error').hide();
}