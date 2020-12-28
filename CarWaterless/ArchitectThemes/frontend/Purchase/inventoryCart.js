// ***************************************************
// Shopping Cart functions

var inventoryCart = (function () {
    // Private methods and properties
    var cart = [];

    function Item(id, code, name, price, count, categoryid, categoryname) {
        this.id = id
        this.code = code
        this.name = name
        this.price = price
        this.count = count
        this.categoryid = categoryid
        this.categoryname = categoryname
     
       
    }

    function saveCart() {
        localStorage.setItem("inventoryCart", JSON.stringify(cart));
    }

    function loadCart() {
        cart = JSON.parse(localStorage.getItem("inventoryCart"));
        if (cart === null) {
            cart = []
        }
    }

    loadCart();



    // Public methods and properties
    var obj = {};

    obj.addItemToCart = function (id, code, name, price, count, categoryid, categoryname) {
        $(".checkout").attr('disabled', true);
        var output = "";
        var currencydata = "";
        $('#newitem').remove();
        $('.nettotal').remove();
        $('.checkout').remove();
      

        //alert(Currency);
        //if (Currency == "MMK") {
        //    output += '<tr id="tr_' + ID + '"><td style="width:200px">' + code + '</td><td style="width:200px">' + name + '</td><td style="width:200px"><select class="selectpicker form-control" data-live-search="true" id="load_account_' + ID + '"><option>Choose Account</option></select><input type="hidden" id="accid_' + ID + '" value="' + AccountID + '"></td><td style="width:200px"><input type="text" class="form-control Expected" id="ExpectedDate_' + ID + '" value="' + ExpectedDate + '" data-itemid="' + ID + '" autocomplete="off"></td><td><input id="txt_description" class="form-control" data-itemid="' + ID + '"  onkeyup="updatedescription(this)"></td><td style="width:200px"><input id="txt_price" onkeyup="updateprice(this)" class="form-control" data-itemid="' + ID + '" data-itemprice="' + price + '" value="' + price + '"></td><td style="width:200px"><select class="form-control" id="currency_' + ID + '" data-itemid="' + ID + '"><option>Select</option><option value="MMK">MMK</option><option value="USD">USD</option></select></td><td style="width:200px"><div class="qty"><button class="minus-btn button" type="button" name="button" data-itemid="' + ID + '" onclick="minus(this)">-</button><input type="number" min="1" id="qty" onkeyup="updateqty(this)" class="item-count plus_qty_' + ID + '" value="' + count + '" data-itemid="' + ID + '" data-itemprice="' + price + '"><button class="plus-btn button" type="button" name="button" data-itemid="' + ID + '" data-itemname="' + name + '" data-itemprice="' + price + '" onclick="plus(this)">+</button></div></td><td id="td_price_' + ID + '">' + price * count + '<span id="td_currency_' + ID + '"> KS</span><button class="delete-item button ml-20 pull-right" data-itemid="' + ID + '" onclick="removeitem(this)">X</button></td></tr>';

        //}
        //else {
        //    output += '<tr id="tr_' + ID + '"><td style="width:200px">' + code + '</td><td style="width:200px">' + name + '</td><td style="width:200px"><select class="selectpicker form-control" data-live-search="true" id="load_account_' + ID + '"><option>Choose Account</option></select><input type="hidden" id="accid_' + ID + '" value="' + AccountID + '"></td><td style="width:200px"><input type="text" class="form-control Expected" id="ExpectedDate_' + ID + '" value="' + ExpectedDate + '" data-itemid="' + ID + '" autocomplete="off"></td><td><input id="txt_description" class="form-control" data-itemid="' + ID + '"  onkeyup="updatedescription(this)"></td><td style="width:200px"><input id="txt_price" onkeyup="updateprice(this)" class="form-control" data-itemid="' + ID + '" data-itemprice="' + price + '" value="' + price + '"></td><td style="width:200px"><select class="form-control" id="currency_' + ID + '" data-itemid="' + ID + '"><option>Select</option><option value="MMK">MMK</option><option value="USD">USD</option></select></td><td style="width:200px"><div class="qty"><button class="minus-btn button" type="button" name="button" data-itemid="' + ID + '" onclick="minus(this)">-</button><input type="number" min="1" id="qty" onkeyup="updateqty(this)" class="item-count plus_qty_' + ID + '" value="' + count + '" data-itemid="' + ID + '" data-itemprice="' + price + '"><button class="plus-btn button" type="button" name="button" data-itemid="' + ID + '" data-itemname="' + name + '" data-itemprice="' + price + '" onclick="plus(this)">+</button></div></td><td id="td_price_' + ID + '">' + price * count + '<span id="td_currency_' + ID + '"> USD</span><button class="delete-item button ml-20 pull-right" data-itemid="' + ID + '" onclick="removeitem(this)">X</button></td></tr>';

        //}

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
                        <td id="qty_${id}">
                           <div class="qty">
                           <button class="minus-btn button" type="button" name="button"
                            data-stockid="${id}" onclick="minus(this)">-</button>

                           <input type="number" min="1" id="qty" onkeyup="updateqty(this)" onkeypress="return onlyNumberKey(event)"
                           class="item-count plus_qty_${id}" value="${count}" data-stockid="${id}" 
                           data-itemprice="${price}">      

                           <button class="plus-btn button" type="button" name="button" data-stockid="${id}" 
                           data-itemname="${name}" data-itemprice="${price}" onclick="plus(this)">+</button></div>
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
                     
                    </tr>
                    </tr>`
        var nettotal = `<tr class="nettotal">
                       <td class="td_remove"></td>
                        <td class="td_stock"></td>
                         
                           
                            <td></td>
                            <td></td>
                            <td>Net Total</td>
                            <td id="nettotal" class="text-right">${inventoryCart.totalCart()} MMK</td>
                                        </tr >`;
        //  var checkoutbutton = '<tr class="checkout"><td style="width:200px"></td><td></td><td></td><td></td><td style="width:200px"></td><td style="width:200px"></td><td style="width:200px"></td><td>Checkout Order</td><td><button class="btn btn-primary btn-xs btn-rounded checkout" onclick="checkout()">Submit</button></td></tr>';

        for (var i in cart) {
            if (cart[i].id === id) {
                cart[i].count += count;

                saveCart();

                $('.plus_qty_' + cart[i].id).val(cart[i].count);
                $('#td_price_' + id).empty().append(cart[i].price * cart[i].count);

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
                //inventoryCart.addExpectedDate(itemid, expecteddate);

                //  $('.Expected').change(function () {
                //    var expecteddate = $(this).val();
                //    var itemid = Number($(this).attr("data-itemid"));

                //    inventoryCart.addExpectedDate(itemid, expecteddate);

                //  });

              
                //$('#currency_'+ID).change(function () {
                //    alert("cu");

                //    var currencyvalue = $("#currency_"+ ID +" option:selected").text();
                   
                //    var itemid = Number($(this).attr("data-itemid"));

                //    inventoryCart.addCurrencyValue(itemid, currencyvalue);

                //});
              
                $('.tbody tr:last').after(newrow);
                $('.tbody tr:last').after(`<tr class="nettotal">
                         <td class="td_remove"></td>
                        <td class="td_stock"></td>
                            
                            <td></td>
                          
                            <td></td>
                            <td>Net Total</td>
                            <td id="nettotal" class="text-right">${inventoryCart.totalCart()} MMK</td>
                                        </tr >`);
               // $('.tbody tr:last').after(checkoutbutton);

                $(".select2_demo_1").select2();
                 
                GetStockList(catid);

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

        var item = new Item(id, code, name, price, count, categoryid, categoryname);
       
        cart.push(item);
        saveCart();
        $("#tbody_purchase").append(output);

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
        //inventoryCart.addExpectedDate(itemid, expecteddate);

      

        //  $('.Expected').change(function () {
        //    var expecteddate = $(this).val();
        //    var itemid = Number($(this).attr("data-itemid"));

        //    inventoryCart.addExpectedDate(itemid, expecteddate);

        //  });
      

        
        //$('#currency_'+ID).change(function () {
            
        //    var currencyvalue = $("#currency_"+ ID +" option:selected").text();
            
        //    var itemid = Number($(this).attr("data-itemid"));

        //    inventoryCart.addCurrencyValue(itemid, currencyvalue);

        //});
       // $('#currency_' + ID).val(Currency);
        $('.tbody tr:last').after(newrow);

        $('.tbody tr:last').after(`<tr class="nettotal">
                         <td class="td_remove"></td>
                        <td class="td_stock"></td>
                            <td></td>
                           
                           
                            <td></td>
                            <td>Net Total</td>
                            <td id="nettotal" class="text-right">${inventoryCart.totalCart()} MMK</td>
                                        </tr >`);
     //   $('.tbody tr:last').after(checkoutbutton);

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
        for (var i in cart) {
            if (cart[i].ID === ID) {
                cart[i].AccountName = accountname;
                cart[i].AccountID = accountid;
               

                saveCart();

                return;
            }
        }


        saveCart();
    };

    obj.addCurrencyValue = function (ID, currencyvalue) {
        for (var i in cart) {
            if (cart[i].ID === ID) {
                cart[i].Currency = currencyvalue;
             
                saveCart();

                return;
            }
        }


        saveCart();
    }


    obj.addExpectedDate = function (ID, ExpectedDate) {
        for (var i in cart) {
            if (cart[i].ID === ID) {
                cart[i].ExpectedDate = ExpectedDate;

                saveCart();


                return;
            }
        }


        saveCart();
    };




    obj.setCountForItem = function (ID, count) {
        for (var i in cart) {
            if (cart[i].id === ID) {
                cart[i].count = count;
                break;
            }
        }

        $('#td_price_' + ID).empty().append(cart[i].price * cart[i].count);



        saveCart();
     //   $('#nettotal').empty().append(inventoryCart.totalCart() + ' KS');

    };


    obj.changePrice = function (ID, price) {
        for (var i in cart) {
            if (cart[i].ID === ID) {
                cart[i].price = price;
                break;
            }
        }

        $('#td_price_' + ID).empty().append(cart[i].price * cart[i].count + ' KS<button class="delete-item button ml-20 pull-right" data-itemid="' + ID + '" onclick="removeitem(this)">X</button>');

        saveCart();
        $('#nettotal').empty().append(inventoryCart.totalCart() + ' MMK');

    };

    obj.changeDescription = function (ID, description) {
        for (var i in cart) {
            if (cart[i].ID === ID) {
                cart[i].Description = description;
                break;
            }
        }
        saveCart();
    }


    obj.removeItemFromCart = function (ID) { // Removes one item
        for (var i in cart) {
            if (cart[i].id === ID) { // "3" === 3 false
                cart[i].count--; // cart[i].count --
                $('.plus_qty_' + cart[i].id).val(cart[i].count);
                $('#td_price_' + ID).empty().append(cart[i].price * cart[i].count);

                if (cart[i].count === 0) {
                    $('#tr_' + cart[i].id).remove();
                    cart.splice(i, 1);

                }



                break;
            }
        }
        saveCart();
        $('#nettotal').empty().append(inventoryCart.totalCart() + ' MMK');

    };


    obj.removeItemFromCartAll = function (ID) { // removes all item name
        for (var i in cart) {
            if (cart[i].id === ID) {
                cart.splice(i, 1);
                $('#tr_' + ID).remove();
                break;
            }
        }

        saveCart();
        $('#nettotal').empty().append(inventoryCart.totalCart() + ' MMK');
    };


    obj.clearCart = function () {
        cart = [];
        saveCart();
    }


    obj.countCart = function () { // -> return total count
        var totalCount = 0;
        for (var i in cart) {
            totalCount += cart[i].count;
        }

        return totalCount;
    };

    obj.totalCart = function () { // -> return total cost
        var totalCost = 0;
        for (var i in cart) {
            totalCost += cart[i].price * cart[i].count;
        }
        return totalCost.toFixed(2);
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
        var cartCopy = JSON.parse(localStorage.getItem("inventoryCart"));
        return cartCopy;
    };

    // ----------------------------
    return obj;
})();




