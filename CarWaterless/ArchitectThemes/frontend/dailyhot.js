var sts;
$(document).ready(function () {

   
    var ID = $('#ID').val();
   
    BindGrid();

 
    $('#btnCancel').click(function () {
        location.reload();
    });


    $('#btnSave').click(function () {
        ClearError();
       
        var Title = $('#Title').val();
        if (Title != "") {
            $.ajax({
                type: "POST",
                url: saveUrl,
                data: GetModel(),
                success: function (data) {
                    $('#divedit').hide();
                    showMessage(data.MessageType, data.Message);

                }
            });
        }
        else {
            $('#Title').closest(".form-control").addClass("is-invalid");
            $('#Title-error').show();
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
                        { "mData": "Title", "bSearchable": true, "bSortable": true, "width": "40%" },
                        {
                            "mData": "IsActive", "bSearchable": true, "bSortable": true, "width": "5%",
                            "mRender": function (data) {
                                if (data.toString() == "true") {
                                    return "<div class='mb-2 mr-2 badge badge-success'>Activate</div>";
                                } else {
                                    return "<div class='mb-2 mr-2 badge badge-danger'>De-Activate</div>";
                                }
                            }
                        },
                        
                        {
                            "mData": "ID", "bSearchable": false, "bSortable": false, "width": "20%",
                            "mRender": function (data, type, full) {
                                sts = full.IsActive.toString();

                                var actions = "";
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-warning btn-sm' href='javascript:void(0)' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
                                if (sts == "true") {
                                    del = "<a class='btn btn-danger btn-sm' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-ban'></i> Deactivate</a>";

                                }
                                else {
                                    del = "<a class='btn btn-success btn-sm' href='javascript:void(0)' onclick=Delete('" + data.toString() + "')><i class='fas fa-check'></i> Activate</a>";

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
                window.location = "/AdminSetup/DailyHot";
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
            $("#ID").val(data.ID);
            $('#Title').val(data.Title);
            $('#imagePreview').attr('src','http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/DailyHot/' + data.Photo);
            
            
            $('#btnSave').html('<i class="fa fa-edit"></i>&nbsp;Update');

            $('#divedit').show();
        },
    })
}

function Delete(id) {
    var mes = "Are you sure to ";
    var flag = true;
    if (sts == "true") {
        mes += "deactivate?";
        flag = true;
    }
    else {
        mes += "activate?";
        flag = false;
    }
    Swal.fire({
        title: mes,
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
                                window.location = "/AdminSetup/DailyHot";
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
    var id = $('#ID').val();
    if (id != "") {
        model.ID = id;
    }
    model.Title = $('#Title').val();
    model.Photo = $('#photo_DailyHot').val();
    return model;
}


function ClearError() {
    $('#Title').closest(".form-control").removeClass("is-invalid");
    $('#Title-error').hide();
}
