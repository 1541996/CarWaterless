
$(document).ready(function () {
    $('#Year').val('');
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
                        },
                        {
                            className: "text-right", "targets": [2,3,4,5]
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
                        {
                            "mData": "Year", "bSearchable": true, "bSortable": true, "width": "10%",
                            "mRender": function (data, type, full) {
                                return data + ", " + full.MonthName;
                            }
                        },
                        { "mData": "TotalExpense", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "TotalIncome", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "Profit", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "TotalSaleCount", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "sCalculatedDate", "bSearchable": true, "bSortable": true, "width": "10%" },
                        {
                            "mData": "IsPrevious", "bSearchable": true, "bSortable": true, "width": "5%",
                            "mRender": function (data, type, full) {
                                if (data == false) {
                                    return '<span class="badge badge-success badge-circle m-r-5 m-b-5"><i class="fa fa-check"></i></span>';
                                }
                                else {
                                    return '<span class="badge badge-danger badge-circle m-r-5 m-b-5"><i class="fa fa-times"></i></span>';
                                }
                            }
                        },

                    ]
                });

                //oTable.on('click', 'tbody tr td:not(:last-child)', function (e) {
                //    var row = $(this).closest('tr');
                //    var data = oTable.fnGetData(row);

                //    View(data.Id);

                //});

            }
        });
    });
}

function GetModel() {
    var model = {};
    model.Month = $('#ddlMonth').val();
    model.Year = $('#Year').val();

    if ($("#chkIsPrevious").prop("checked") == true) {
        model.IsPrevious = true;
    }
    else {
        model.IsPrevious = false;
    }

    return model;
}

function onlyNumberKey(evt) {

    // Only ASCII charactar in that range allowed 
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
} 