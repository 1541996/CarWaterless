var selectIds = "";
var selectNames = "";
var hasData = false;
$(document).ready(function () {

    $('select').select2();
    $('#ddladditionalservice').empty();

    BindGrid();

    $('#ddlCarType').change(function () {

        var cartype = $(this).val();
        if (cartype != "") {
            $.ajax({
                type: "POST",
                url: bindserviceUrl,
                data: { "cartype": cartype },
                success: function (data) {
                    var select = $('#ddladditionalservice').empty();
                    $.each(data, function () {
                        select.append($("<option></option>").val(this['Id']).html(this['Name']));
                    });
                    select.trigger('change');
                }
            })
        }
    });

    

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

function Edit(id) {
    $.ajax({
        url: editUrl,
        type: 'POST',
        data: { "Id": id },
        success: function (data) {
            $("#Id").val(data.Id);
            $('#Title').val(data.Title);
            $('#ddlCarType').val(data.CarType);
            $('#ddlCarType').trigger('change');
            $('#PackagePrice').val(data.PackagePrice);
            $('#imagePreview').attr('src','http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/MemberPackage/' + data.Photo);
            

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
    var id = $('#Id').val();
    if (id != "") {
        model.Id = id;
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
    $('#ddladditionalservice option:selected').each(function () {
        var $this = $(this);
        if ($this.length) {
            var selval = $this.val();
            selectIds += selval + ",";
        }
    });
}

function GetSelectedNames() {
    $('#ddladditionalservice option:selected').each(function () {

        var $this = $(this);
        if ($this.length) {
            var selText = $this.text();
            selectNames += selText + ",";
        }
    });
}