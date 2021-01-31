
$(document).ready(function () {

    
    BindGrid();


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
                            "mData": "No", "bSearchable": false, "bSortable": false, "width": "5%",
                            "mRender": function (data) {
                                return "<center>" + data.toString() + "</center>";
                            }
                        },
                        { "mData": "UserName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "FullName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "UserRoleName", "bSearchable": true, "bSortable": true, "width": "20%" },
                        {
                            "mData": "IsActive", "bSearchable": true, "bSortable": true, "width": "10%",
                            "mRender": function (data, type, full) {
                                if (data.toString() == "true") {
                                    return "<div class='mb-2 mr-2 badge badge-success'>Activate</div>";
                                }
                                else {
                                    return "<div class='mb-2 mr-2 badge badge-danger'>Deactivate</div>";
                                }
                            }
                        },
                       
                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "20%",
                            "mRender": function (data,type,full) {

                                var actions = "";
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-dark btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";

                                if (full.IsActive.toString() == "true") {
                                    del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=Deactivate('" + data.toString() + "')>Deactivate</a>";
                                }
                                else {
                                    del = "<a class='btn btn-success btn-sm' href='javascript:void(0)' onclick=Activate('" + data.toString() + "')>Activate</a>";
                                }
                                
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
                window.location = "/AdminUser/Index";
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
            $('#PrefixCode').val(data.PrefixCode);
            

            $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');

            //$('body, html').animate({ scrollTop: $(patientFormTop).offset().top }, 'slow');
            $('html, body').animate({
                scrollTop: 0
            }, 800);
        },
    })
}

function Deactivate(id) {
    
    Swal.fire({
        title: "Are you sure to deactivate?",
        text: "This account will no longer active.",
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
                url: deactivateUrl,
                type: 'POST',
                data: { "id": id },
                success: function (data) {
                    if (data) {
                        $('#hdMessageType').val(data.MessageType);
                        $('#hdMessage').val(data.Message);

                        Swal.fire(
                            'Deleted!',
                            'success'
                        ).then((result) => {
                            if (result.value) {
                                window.location = "/AdminUser/Index";
                            }
                        });
                    }

                    BindGird();
                }

            });


        }
    });
}


function Activate(id) {

    Swal.fire({
        title: "Are you sure to activate?",
        text: "This account will be able to use.",
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
                url: activateUrl,
                type: 'POST',
                data: { "id": id },
                success: function (data) {
                    if (data) {
                        $('#hdMessageType').val(data.MessageType);
                        $('#hdMessage').val(data.Message);

                        Swal.fire(
                            'Deleted!',
                            'success'
                        ).then((result) => {
                            if (result.value) {
                                window.location = "/AdminUser/Index";
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
    
    
    return model;
}
