var selectIds = "";
var selectNames = "";
var hasData = false;
var editbindIds_arr;
$(document).ready(function () {

  
 //   $('#ddladditionalservice2').empty();

    var ID = $('#ID').val();
    //if (ID != "0") {
    //    var editbindIds = $('#AdditionalServiceIds').val();
    //    editbindIds_arr = editbindIds.split("_");

    //    $("#ddladditionalservice").select2({
    //        data: data
    //    });
    //}

    BindGrid();

    
      
  
    //$('#ddlCarType').change(function () {

    //    var cartype = $(this).val();
    //    if (cartype != "") {
    //        $.ajax({
    //            type: "POST",
    //            url: bindserviceUrl,
    //            data: { "cartype": cartype },
    //            success: function (data) {
    //                var select = $('#ddladditionalservice').empty();
    //                $.each(data, function () {
    //                    select.append($("<option></option>").val(this['Id']).html(this['Name']));
    //                });
    //                select.trigger('change');
    //            }
    //        })
    //    }
    //});

    

    $('#btnCancel').click(function () {
        location.reload();
    });


    $('#btnSave').click(function () {
        ClearError();
        selectIds = "";
        selectNames = "";

        var Title = $('#Title').val();
        var ddlCarType = $('#ddlCarType').val();
        var PackagePrice = $('#PackagePrice').val();
        if (Title != "") {
            if (ddlCarType != "") {
                GetSelectedIds();
                GetSelectedNames();
                if (selectIds.length != 0) {
                    if (PackagePrice != "") {
                        $.ajax({
                            type: "POST",
                            url: saveUrl,
                            data: GetModel(),
                            success: function (data) {

                                showMessage(data.MessageType, data.Message);

                            }
                        });
                    }
                    else {
                        $('#PackagePrice').closest(".form-control").addClass("is-invalid");
                        $('#PackagePrice-error').show();
                    }
                }
                else {
                    $('#AdditionalServiceIds').closest(".form-control").addClass("is-invalid");
                    $('#AdditionalServiceIds-error').show();
                }
            }
            else {
                $('#ddlCarType').closest(".form-control").addClass("is-invalid");
                $('#ddlCarType-error').show();
            }
            
           
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
                        { "mData": "Title", "bSearchable": true, "bSortable": true, "width": "20%" },
                        { "mData": "CarType", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "AdditionalServiceNames", "bSearchable": true, "bSortable": true, "width": "20%" },
                        { "mData": "PackagePrice", "bSearchable": true, "bSortable": true, "width": "10%" },
                       
                        {
                            "mData": "ID", "bSearchable": false, "bSortable": false, "width": "20%",
                            "mRender": function (data) {

                                var actions = "";
                                var edit = "";
                                var del = "";

                                edit = "<a class='btn btn-warning btn-sm' onclick=Edit('" + data.toString() + "')><i class='fas fa-edit'></i></a>&nbsp;&nbsp;";
                                del = "<a class='btn btn-danger btn-sm' onclick=Delete('" + data.toString() + "')><i class='fas fa-trash'></i></a>";

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
                window.location = "/AdminSetup/MemberPackage";
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
        text: "Deleting may occur errors for existing records.",
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
                                window.location = "/AdminSetup/MemberPackage";
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
    model.AdditionalServiceIds = selectIds;
    model.AdditionalServiceNames = selectNames;
    model.PackagePrice = $('#PackagePrice').val();
    model.CarType = $('#ddlCarType').val();
    model.Photo = $('#photo_memberpackage').val();
    return model;
}


function ClearError() {
    $('#Title').closest(".form-control").removeClass("is-invalid");
    $('#AdditionalServiceIds').closest(".form-control").removeClass("is-invalid");
    $('#PackagePrice').closest(".form-control").removeClass("is-invalid");
    $('#ddlCarType').closest(".form-control").removeClass("is-invalid");
    $('#Title-error').hide();
    $('#AdditionalServiceIds-error').hide();
    $('#PackagePrice-error').hide();
    $('#ddlCarType-error').hide();
}

function GetSelectedIds() {
    $('#ddladditionalservice2 option:selected').each(function () {
        var $this = $(this);
        if ($this.length) {
            var selval = $this.val();
            selectIds += selval + "_";
        }
    });
}

function GetSelectedNames() {
    $('#ddladditionalservice2 option:selected').each(function () {

        var $this = $(this);
        if ($this.length) {
            var selText = $this.text();
            selectNames += selText + "_";
        }
    });
}