$(document).ready(function () {
    $('select').select2();

    BindGrid();

});

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
                            'targets': [0,4,5,6],
                            'createdCell': function (td, cellData, rowData, row, col) {
                                $(td).attr('data-tableexport-xlsxformatid', 1);
                            }
                        },

                        {
                            className: "text-right", "targets": [4, 5, 6]
                        }
                    ],
                    "pagingType": "full_numbers",
                    "lengthMenu": [
                        [-1,10, 25, 50,],
                        ["All",10, 25, 50, ]
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

                        { "mData": "Name", "bSearchable": true, "bSortable": true, "width": "20%" },
                        { "mData": "CategoryName", "bSearchable": true, "bSortable": true, "width": "12%" },
                        { "mData": "Code", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "AvailableQuantity", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "UnitPurchasePrice", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "UniqueTotalPurchaseAmount", "bSearchable": true, "bSortable": true, "width": "12%" },
                    ],
                    "footerCallback": function (tfoot, data, start, end, display) {
                        var api = this.api();
                        var p = api.column(6).data().reduce(function (a, b) {
                            return a + b;
                        }, 0)
                        $(api.column(0).footer()).html("Total");
                        $(api.column(6).footer()).html(p);
                    },
                   

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

function GetFilterModel() {
    var model = {};
    model.CategoryId = 0;
    model.SupplierId =0;
    model.BrandId = 0;
    model.Name = "";
    model.Code = "";
    model.AvailableStatus = "Instock";
    return model;
}

function Export() {
    $.ajax({
        type: "POST",
        url: listUrl,
        data: GetFilterModel(),
        dataType: "json",
        success: function (result) {
            $('#div_excel_stock').empty().append(result)
        },
        complete: function () {


            doExport('#tbl', { type: 'xlsx', htmlHyperlink: 'content' });


            //$('#div_excel_stock').empty();

            //$('#tb_purchase').table2excel({
            //    exclude: ".excludeThisClass",
            //    name: "Worksheet Name",
            //    filename: "Purchase Report",
            //    extension: ".xlsx" //do not include extension
            //});

        }
    });
}

function doExport(selector, params) {
    var exportdate = moment().format('DD-MM-YYYY');
    var options = {
        tableName: 'Balance Report',
        fileName: 'Stock Balance Report(' + exportdate + ')',
    };

    jQuery.extend(true, options, params);

    $(selector).tableExport(options);
}