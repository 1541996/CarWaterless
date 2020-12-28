// ***************************************************
// Shopping Cart functions

var saleCart = (function () {
    // Private methods and properties
    var scart = [];

    function Item(id, code, name, price, count, categoryid, categoryname, avaqty) {
        this.id = id
        this.code = code
        this.name = name
        this.price = price
        this.count = count
        this.categoryid = categoryid
        this.categoryname = categoryname
        this.avaqty = avaqty


    } txt_saledate
    function saveCart() {
        localStorage.setItem("saleCart", JSON.stringify(scart));
    }

    function loadCart() {
        scart = JSON.parse(localStorage.getItem("saleCart"));
        if (scart === null) {
            scart = []
        }
    }

    loadCart();



    // Public methods and properties
    var obj = {};

    obj.addItemToCart = function (id, code, name, price, count, categoryid, categoryname, avaqty) {
        $(".checkout").attr('disabled', true);
        var output = "";
        var currencydata = "";
        $('#newitem').remove();
        $('.nettotal').remove();
        $('.checkout').remove();


        output += `<tr id="tr_${id}">  
                      <td class="td_remove">

                           <button class="delete-item button ml-20 pull-right" data-stockid="${id}" onclick="removeitem(this)">X</button>
                        </td>
                        <td class="td_stock">
                            ${name}   
                        </td>
                        <td>
                            ${code}   
                         </td>
                         
                        <td>
                            ${price}
                        </td>
                         <td>
                            ${avaqty}
                        </td>
                        <td id="qty_${id}">
                           <div class="qty">
                           <button class="minus-btn button" type="button" name="button"
                            data-stockid="${id}" onclick="minus(this)">-</button>

                           <input type="number" min="1" id="qty" onkeyup="updateqty(this)" onkeypress="return onlyNumberKey(event)"
                           class="item-count plus_qty_${id}" value="${count}" data-stockid="${id}" data-avaqty="${avaqty}"
                           data-itemprice="${price}">      

                           <button class="plus-btn button" type="button" name="button" data-stockid="${id}" 
                           data-itemname="${name}"  data-avaqty="${avaqty}" data-itemprice="${price}" onclick="plus(this)">+</button></div>
                        </td>
                         <td class="text-right" id="td_price_${id}">
                            ${count * price} 
                        </td>
            
                       
                    </tr>`;

        var newrow = `<tr id="newitem">
                        <td class="td_remove">
                                    
                        
                        </td>
                        <td class="td_stock">
                      
                            <select class="form-control select2_demo_1 DD_Stock" id="DD_Stock">
                                           <option value="0" hidden>Choose Stock</option>
                            </select>
                       
                        </td>
                        <td></td>
                        <td></td>
                       
                        <td></td>
                        <td></td>
                        <td class="text-right"></td>
                      
                    </tr>
                    </tr>`
        var nettotal = `<tr class="nettotal">
                         <td class="td_remove"></td>
                        <td class="td_stock"></td>
                            <td></td>
                          
                            <td></td>
                            <td></td>
                            
                            <td>Net Total</td>
                            <td id="nettotal" class="text-right">${saleCart.totalCart()} MMK</td>
                                        </tr >`;
        //  var checkoutbutton = '<tr class="checkout"><td style="width:200px"></td><td></td><td></td><td></td><td style="width:200px"></td><td style="width:200px"></td><td style="width:200px"></td><td>Checkout Order</td><td><button class="btn btn-primary btn-xs btn-rounded checkout" onclick="checkout()">Submit</button></td></tr>';
        $("#txt_finalcost").val(saleCart.CalculateFinal());
        for (var i in scart) {
            if (scart[i].id === id) {

                if (scart[i].count < avaqty) {
                    scart[i].count += count;
                } else {

                 
                    Swal.fire(
                        'Fail to add!',
                        'danger'
                    ).then((result) => {
                        if (result.value) {
                            //  window.location = "/Setup/Category";
                              $('.tbody tr:last').after(newrow);
                            $('.tbody tr:last').after(`<tr class="nettotal">
                                <td class="td_remove"></td>
                                <td class="td_stock"></td>
                                   
                                    <td></td>
                                   
                                    <td></td>
                                    <td></td>
                                    <td>Net Total</td>
                                    <td id="nettotal" class="text-right">${saleCart.totalCart()} MMK</td>
                                                </tr >`);
                            $("#txt_finalcost").val(saleCart.CalculateFinal());
                            GetStockList();

                            $('.DD_Stock').on('change', function () {
                                //categoryname = $('#DD_Category :selected').text();
                                //if (categoryname == "Select Category") {
                                //    categoryname = "";
                                //} else {
                                //    categoryname = categoryname;
                                //}
                                //LoadMainList();

                                var stockid = $('.DD_Stock').val();
                                GetStockByID(stockid);
                            });

                        }
                    });
                    return;
                    
                }
                saveCart();

                $('.plus_qty_' + scart[i].id).val(scart[i].count);
                $('#td_price_' + id).empty().append(scart[i].price * scart[i].count);

                //  autocompleteaccountname(ID);

                //$('.Expected').daterangepicker({
                //    autoApply: true,
                //    singleDatePicker: true,
                //    locale: {
                //        format: 'DD MMM, YYYY'
                //    }
                //});
                //var expecteddate = $('.Expected').val();
                //var itemid = Number($('.Expected').attr("data-itemid"));
                //saleCart.addExpectedDate(itemid, expecteddate);

                //  $('.Expected').change(function () {
                //    var expecteddate = $(this).val();
                //    var itemid = Number($(this).attr("data-itemid"));

                //    saleCart.addExpectedDate(itemid, expecteddate);

                //  });


                //$('#currency_'+ID).change(function () {
                //    alert("cu");

                //    var currencyvalue = $("#currency_"+ ID +" option:selected").text();

                //    var itemid = Number($(this).attr("data-itemid"));

                //    saleCart.addCurrencyValue(itemid, currencyvalue);

                //});

                $('.tbody tr:last').after(newrow);
                $('.tbody tr:last').after(`<tr class="nettotal">
                         <td class="td_remove"></td>
                        <td class="td_stock"></td>
                            <td></td>
                           
                            <td></td>
                            <td></td>
                            
                            <td>Net Total</td>
                            <td id="nettotal" class="text-right">${saleCart.totalCart()} MMK</td>
                                        </tr >`);
                // $('.tbody tr:last').after(checkoutbutton);
                $("#txt_finalcost").val(saleCart.CalculateFinal());
                GetStockList();

                $('.DD_Stock').on('change', function () {
                    //categoryname = $('#DD_Category :selected').text();
                    //if (categoryname == "Select Category") {
                    //    categoryname = "";
                    //} else {
                    //    categoryname = categoryname;
                    //}
                    //LoadMainList();

                    var stockid = $('.DD_Stock').val();
                    GetStockByID(stockid);
                });



                return;
            }
        }

        var item = new Item(id, code, name, price, count, categoryid, categoryname, avaqty);

        scart.push(item);
        saveCart();
        $("#tbody_sale").append(output);

        //$('.Expected').daterangepicker({
        //    autoApply: true,
        //    singleDatePicker: true,
        //    locale: {
        //        format: 'DD MMM, YYYY'
        //    }
        //});
        //var expecteddate =$('.Expected').val();
        ////var itemid = Number($('.Expected').attr("data-itemid"));
        //var itemid = ID;
        //saleCart.addExpectedDate(itemid, expecteddate);



        //  $('.Expected').change(function () {
        //    var expecteddate = $(this).val();
        //    var itemid = Number($(this).attr("data-itemid"));

        //    saleCart.addExpectedDate(itemid, expecteddate);

        //  });



        //$('#currency_'+ID).change(function () {

        //    var currencyvalue = $("#currency_"+ ID +" option:selected").text();

        //    var itemid = Number($(this).attr("data-itemid"));

        //    saleCart.addCurrencyValue(itemid, currencyvalue);

        //});
        // $('#currency_' + ID).val(Currency);
        $('.tbody tr:last').after(newrow);

        $('.tbody tr:last').after(`<tr class="nettotal">
                         <td class="td_remove"></td>
                        <td class="td_stock"></td>
                          
                            <td></td>
                            <td></td>
                            <td></td>
                            
                            <td>Net Total</td>
                            <td id="nettotal" class="text-right">${saleCart.totalCart()} MMK</td>
                                        </tr >`);
        //   $('.tbody tr:last').after(checkoutbutton);

        $("#txt_finalcost").val(saleCart.CalculateFinal());

        GetStockList();

        $('.DD_Stock').on('change', function () {
            //categoryname = $('#DD_Category :selected').text();
            //if (categoryname == "Select Category") {
            //    categoryname = "";
            //} else {
            //    categoryname = categoryname;
            //}
            //LoadMainList();

            var stockid = $('.DD_Stock').val();
            GetStockByID(stockid);
        });




    };




    obj.addAccountName = function (ID, accountid, accountname) {
        for (var i in scart) {
            if (scart[i].ID === ID) {
                scart[i].AccountName = accountname;
                scart[i].AccountID = accountid;


                saveCart();

                return;
            }
        }


        saveCart();
    };

    obj.addCurrencyValue = function (ID, currencyvalue) {
        for (var i in scart) {
            if (scart[i].ID === ID) {
                scart[i].Currency = currencyvalue;

                saveCart();

                return;
            }
        }


        saveCart();
    }


    obj.addExpectedDate = function (ID, ExpectedDate) {
        for (var i in scart) {
            if (scart[i].ID === ID) {
                scart[i].ExpectedDate = ExpectedDate;

                saveCart();


                return;
            }
        }


        saveCart();
    };




    obj.setCountForItem = function (ID, count, avaqty) {
        var availableqty = parseInt(avaqty);
        var takenqty = parseInt(count);
        var extraqty;
        
        var nochangeqty;
        for (var i in scart) {
            if (scart[i].id === ID) {
                nochangeqty = scart[i].count;
                scart[i].count = takenqty;
                if (scart[i].count <= availableqty) {                 
                    scart[i].count = takenqty;
                    $('#td_price_' + ID).empty().append(scart[i].price * scart[i].count);
                    break;
                } else {
                    extraqty = takenqty - availableqty;
                   
                    Swal.fire({
                        title: "Can't add qty",
                        text: "Availeble qty: " + availableqty + "\n Your qty: " + takenqty + "\n Do you want to proceed to add: " + availableqty,
                        type: 'warning',
                     //   showCancelButton: true,
                        cancelButtonColor: '#ff6258',
                        confirmButtonText: 'Save',
                  //      cancelButtonText: 'No',
                        confirmButtonClass: "btn btn-primary",
                   //     cancelButtonClass: "btn btn-danger",
                    }).then((result) => {
                        if (result.value) {
                            scart[i].count = availableqty;
                            $('.plus_qty_' + scart[i].id).val(availableqty);

                            $('#td_price_' + ID).empty().append(scart[i].price * scart[i].count);

                            saveCart();
                            $('#nettotal').empty().append(saleCart.totalCart() + ' KS');    
                            $("#txt_finalcost").val(saleCart.CalculateFinal());
                        } else {
                           scart[i].count = nochangeqty;
                           $('.plus_qty_' + scart[i].id).val(scart[i].count);

                            $('#td_price_' + ID).empty().append(scart[i].price * scart[i].count);
                        
                        }
                    });
                }



                break;
            }
        }

     



        saveCart();



        $('#nettotal').empty().append(saleCart.totalCart() + ' KS');    
        $("#txt_finalcost").val(saleCart.CalculateFinal());
         
     //   $('#nettotal').empty().append(saleCart.totalCart() + ' KS');

    };


    obj.changePrice = function (ID, price) {
        for (var i in scart) {
            if (scart[i].ID === ID) {
                scart[i].price = price;
                break;
            }
        }

        $('#td_price_' + ID).empty().append(scart[i].price * scart[i].count + ' KS<button class="delete-item button ml-20 pull-right" data-itemid="' + ID + '" onclick="removeitem(this)">X</button>');

        saveCart();
        $('#nettotal').empty().append(saleCart.totalCart() + ' KS');
        $("#txt_finalcost").val(saleCart.CalculateFinal());
    };

    obj.changeDescription = function (ID, description) {
        for (var i in scart) {
            if (scart[i].ID === ID) {
                scart[i].Description = description;
                break;
            }
        }
        saveCart();
    }


    obj.removeItemFromCart = function (ID) { // Removes one item
        for (var i in scart) {
            if (scart[i].id === ID) { // "3" === 3 false
                scart[i].count--; // cart[i].count --
                $('.plus_qty_' + scart[i].id).val(scart[i].count);
                $('#td_price_' + ID).empty().append(scart[i].price * scart[i].count);

                if (scart[i].count === 0) {
                    $('#tr_' + scart[i].id).remove();
                    scart.splice(i, 1);

                }



                break;
            }
        }
        saveCart();
        $('#nettotal').empty().append(saleCart.totalCart() + ' MMK');
        $("#txt_finalcost").val(saleCart.CalculateFinal());
    };



    obj.removeItemFromCartAll = function (ID) { // removes all item name
        for (var i in scart) {
            if (scart[i].id === ID) {
                scart.splice(i, 1);
                $('#tr_' + ID).remove();
                break;
            }
        }

        saveCart();
        $('#nettotal').empty().append(saleCart.totalCart() + ' MMK');
        $("#txt_finalcost").val(saleCart.CalculateFinal());
    };


    obj.clearCart = function () {
        scart = [];
        saveCart();
    }


    obj.countCart = function () { // -> return total count
        var totalCount = 0;
        for (var i in scart) {
            totalCount += scart[i].count;
        }

        return totalCount;
    };

    obj.totalCart = function () { // -> return total cost
        var totalCost = 0;
        for (var i in scart) {
            totalCost += scart[i].price * scart[i].count;
        }
        return totalCost.toFixed(2);
    };
    obj.CalculateFinal = function () { // -> return final cost
        var finalCost = 0;
        var discount = $('#txt_discount').val();
        if (discount != null && discount != "") {
            finalCost = parseInt(saleCart.totalCart()) - parseInt(discount);
            return finalCost.toFixed(2);
        }
        else {
            return saleCart.totalCart();
        }
        
    };

    obj.listCart = function () { // -> array of Items
        //var cartCopy = [];
        //console.log("Listing cart");
        //console.log(cart);
        //for (var i in cart) {
        //    console.log(i);
        //    var item = cart[i];
        //    var itemCopy = {};
        //    for (var p in item) {
        //        itemCopy[p] = item[p];
        //    }
        //  //  itemCopy.total = (item.price * item.count).toFixed(2);
        //    cartCopy.push(itemCopy);
        //}
        var cartCopy = JSON.parse(localStorage.getItem("saleCart"));
        return cartCopy;
    };

    // ----------------------------
    return obj;
})();




