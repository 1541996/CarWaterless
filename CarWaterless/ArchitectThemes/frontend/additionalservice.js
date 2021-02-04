
$(document).ready(function () {

    
    BindGrid();

    

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {
        $('#btnloading').show();
        ClearError();

        var isvalid = false;
        var Name = $('#Name').val();
        var ddlCarType = $('#ddlCarType').val();
        var Price = $('#Price').val();
        var DiscountPrice = $('#DiscountPrice').val();
        if (Name != "") {
            if (ddlCarType != "") {
                if (Price != "") {
                    if (DiscountPrice != "") {
                        isvalid = true;
                    }
                    else {
                        $('#DiscountPrice').closest(".form-control").addClass("is-invalid");
                        $('#DiscountPrice-error').show();
                    }
                }
                else {
                    $('#Price').closest(".form-control").addClass("is-invalid");
                    $('#Price-error').show();
                }
            }
            else {
                $('#ddlCarType').closest(".form-control").addClass("is-invalid");
                $('#ddlCarType-error').show();
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
                    $('#btnloading').hide();
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
                            className: "text-center", "targets": [6]
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
                        { "mData": "Name", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "CarType", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "Price", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "DiscountPrice", "bSearchable": true, "bSortable": true, "width": "10%" },
                        {
                            "mData": "IsDailyHot", "bSearchable": true, "bSortable": true, "width": "10%",
                            "mRender": function (data) {
                                if (data.toString() == "true") {
                                    return "<div class='mb-2 mr-2 badge badge-success'>Activate</div>";
                                } else {
                                    return "<div class='mb-2 mr-2 badge badge-danger'>De-Activate</div>";
                                }
                            }
                        },
                       
                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "20%",
                            "mRender": function (data, type, full) {
                                var dh = full.IsDailyHot.toString();

                                var actions = "";
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-warning btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
                                del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i></a>";
                                if (dh == "true") {
                                    del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=DeAc('" + data.toString() + "','" + dh +"')><i class='fas fa-ban'></i> Off</a>";

                                }
                                else {
                                    del = "<a class='btn btn-success btn-sm' href='javascript:void(0)' onclick=DeAc('" + data.toString() + "','" + dh +"')><i class='fas fa-check'></i> On</a>";

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
                window.location = "/AdminSetup/AdditionalService";
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
            $('#Price').val(data.Price);
            $('#DiscountPrice').val(data.DiscountPrice);
            $('#ddlCarType').val(data.CarType);
            

            $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');

            //$('body, html').animate({ scrollTop: $(patientFormTop).offset().top }, 'slow');
            $('html, body').animate({
                scrollTop: 0
            }, 800);
        },
    })
}

function Delete(id) {

    Swal.fire({
        title: "Are you sure to delete?",
        text: "Deleting services may occur errors for existing records.",
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
                                window.location = "/AdminSetup/AdditionalService";
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
    model.CarType = $('#ddlCarType').val();
    model.Price = $('#Price').val();
    model.DiscountPrice = $('#DiscountPrice').val();
    
    return model;
}


function ClearError() {
    $('#Name').closest(".form-control").removeClass("is-invalid");
    $('#ddlCarType').closest(".form-control").removeClass("is-invalid");
    $('#Price').closest(".form-control").removeClass("is-invalid");
    $('#DiscountPrice').closest(".form-control").removeClass("is-invalid");
    $('#Name-error').hide();
    $('#ddlCarType-error').hide();
    $('#DiscountPrice-error').hide();
    $('#Price-error').hide();
}

function DeAc(id,sts) {
    var flag = true;
    if (sts == "true") {
        flag = true;
    }
    else {
        flag = false;
    }

    $.ajax({
        url: deacUrl,
        type: 'POST',
        data: { "id": id, "currentflag": flag },
        success: function (data) {
            if (data) {
                $('#hdMessageType').val(data.MessageType);
                $('#hdMessage').val(data.Message);

                Swal.fire(
                    'Operation Success.',
                    'success'
                ).then((result) => {
                    if (result.value) {
                        window.location = "/AdminSetup/AdditionalService";
                    }
                });
            }

        }

    });
}
