// ***************************************************
// Shopping Cart functions

var inventoryCartsale = (function () {
    // Private methods and properties
    var cart = [];

    function Item(stockid, stockname, unitsellprice, quantity, amount, discount) {
        this.stockid = stockid
        this.stockname = stockname
        this.unitsellprice = unitsellprice
        this.quantity = quantity
        this.amount = amount
        this.discount = discount
      
    }

    function saveCart() {
        localStorage.setItem("inventoryCartsale", JSON.stringify(cart));
    }

    function loadCart() {
        cart = JSON.parse(localStorage.getItem("inventoryCartsale"));
        if (cart === null) {
            cart = []
        }
    }

    loadCart();



    // Public methods and properties
    var obj = {};

    obj.addItemToCart = function (stockid, stockname, unitsellprice, quantity, amount, discount) {
        $(".checkout").attr('disabled', true);
        var output = "";
        var currencydata = "";
       
        $('#nettotal').remove();
        $('.checkout').remove();

        var dynamicitemname = stockname.replace(/ /g, '');
        output += `<tr id="tr_${dynamicitemname}">  
                       
                        <td class="td_stock">
                            ${stockname}   
                        </td>
                        <td class="alnright">
                            ${quantity}   
                         </td>
                         
                        <td class="alnright">
                            ${unitsellprice}
                        </td>
                        <td class="alnright">
                            ${unitsellprice * quantity}
                        </td>
                         <td class="alnright">
                            ${discount}
                        </td>
                       <td class="alncenter">
                              <button class="btn btn-info btn-icon"
                                                data-stockname="${stockname}"
                                                data-stockid="${stockid}"
                                               data-unitsellprice="${unitsellprice}"
                                                data-quantity="${quantity}"
                                                data-amount="${unitsellprice * quantity}"
                                                data-discount="${discount}"
                                             onclick="editItem(this)"><i class="fa fa-edit"></i></button>
                             <button class="btn btn-danger btn-icon" onclick="deleteItem('${stockname}')"><i class="fa fa-trash"></i></button>
                          
                          
                       </td>
            
                       
                    </tr>`;
       
        var nettotal = `<tr id="nettotal">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCartsale.totalCart()}</th>
                            <th class="alnright">${inventoryCartsale.discountCart()}</th>
                            <th></th>                   
                      </tr>`
     
        for (var i in cart) {
            if (cart[i].stockname === stockname) {
                //alert("Same Item")
                //saveCart();
                $('.tbody tr:last').after(nettotal);
                var otherchanges = $('#othercharges').val();
                if (otherchanges == "") {
                    otherchanges = 0;
                } else {
                    otherchanges = Number($('#othercharges').val());
                }
                //$('#netalltotal').val((inventoryCartsale.totalCart() - inventoryCartsale.discountCart()) + otherchanges);
                //$('#lblnetalltotal').text((inventoryCartsale.totalCart() - inventoryCartsale.discountCart()) + otherchanges);
                var netalltotal = Number(inventoryCartsale.totalCart()) - Number(inventoryCartsale.discountCart()) + otherchanges;
                $('#netalltotal').val(netalltotal);
                $('#lblnetalltotal').text(netalltotal);
                $('#TotalSellingAmount').val(inventoryCartsale.totalCart());
                $('#TotalDiscount').val(inventoryCartsale.discountCart());


                return;
            }
        }

        var item = new Item(stockid, stockname, unitsellprice, quantity, amount, discount);
       
        cart.push(item);
        saveCart();
        $("#tbody_invoice").append(output);

      
        var nettotal = `<tr id="nettotal">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCartsale.totalCart()}</th>
                            <th class="alnright">${inventoryCartsale.discountCart()}</th>
                            <th></th>                   
                      </tr>`

        $('.tbody tr:last').after(nettotal);

        var otherchanges = $('#othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        //$('#netalltotal').val((inventoryCartsale.totalCart() - inventoryCartsale.discountCart()) + otherchanges);
        //$('#lblnetalltotal').text((inventoryCartsale.totalCart() - inventoryCartsale.discountCart()) + otherchanges);
        var netalltotal = Number(inventoryCartsale.totalCart()) - Number(inventoryCartsale.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalSellingAmount').val(inventoryCartsale.totalCart());
        $('#TotalDiscount').val(inventoryCartsale.discountCart());


    };


    obj.editItemFromCart = function (stockid, stockname, quantity,unitsellprice,  amount, discount) {
        $(".checkout").attr('disabled', true);
        var output = "";
        var currencydata = "";

        $('#nettotal').remove();
        $('.checkout').remove();

        var dynamicitemname = stockname.replace(/ /g, '');
        output += `  <tr id = "tr_${dynamicitemname}" >                   
                        <td class="td_stock">
                            ${stockname}   
                        </td>
                        <td class="alnright">
                            ${quantity}   
                         </td class="alnright">
                         
                        <td class="alnright">
                            ${unitsellprice}
                        </td>
                        <td class="alnright">
                            ${unitsellprice * quantity}
                        </td>
                         <td class="alnright">
                            ${discount}
                        </td>
                       <td class="alncenter">
                              <button class="btn btn-info btn-icon"
                                                data-stockname="${stockname}"
                                                data-stockid="${stockid}"
                                                data-unitsellprice="${unitsellprice}"
                                                data-quantity="${quantity}"
                                                data-amount="${unitsellprice * quantity}"
                                                data-discount="${discount}"
                                        onclick="editItem(this)"><i class="fa fa-edit"></i></button>
                             <button class="btn btn-danger btn-icon" onclick="deleteItem('${stockname}')"><i class="fa fa-trash"></i></button>
                          
                          
                       </td>
                        </tr>
                       
                    `;

      

      
      
        for (var i in cart) {
            if (cart[i].stockname === stockname) {
                cart[i].stockname = stockname;
                cart[i].unitsellprice = unitsellprice
                cart[i].quantity = quantity
                cart[i].amount = amount
                cart[i].discount = discount
                var dynamicitemname = cart[i].stockname.replace(/ /g, '');
                saveCart();
                $('#tr_' + dynamicitemname).replaceWith(output);

                break;
            }
        }

        var nettotal = `<tr id="nettotal" class="alnright">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCartsale.totalCart()}</th>
                            <th class="alnright">${inventoryCartsale.discountCart()}</th>
                            <th></th>                   
                      </tr>`

        $('.tbody tr:last').after(nettotal);



        var otherchanges = $('#othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        var netalltotal = Number(inventoryCartsale.totalCart()) - Number(inventoryCartsale.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalSellingAmount').val(inventoryCartsale.totalCart());
        $('#TotalDiscount').val(inventoryCartsale.discountCart());

     

    };


    obj.removeItemFromCart = function (stockname) { // Removes one item
        $('#nettotal').remove();
        for (var i in cart) {
            if (cart[i].stockname === stockname) { // "3" === 3 false
                var dynamicitemname = cart[i].stockname.replace(/ /g, '');
                $('#tr_' + dynamicitemname).remove();
                cart.splice(i, 1);

                break;
            }
        }
        saveCart();
        var nettotal = `<tr id="nettotal" class="alnright">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCartsale.totalCart()}</th>
                            <th class="alnright">${inventoryCartsale.discountCart()}</th>
                            <th></th>                   
                      </tr>`



        $('.tbody tr:last').after(nettotal);
        var otherchanges = $('othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        var netalltotal = Number(inventoryCartsale.totalCart()) - Number(inventoryCartsale.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalSellingAmount').val(inventoryCartsale.totalCart());
        $('#TotalDiscount').val(inventoryCartsale.discountCart());

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
        $('#nettotal').empty().append(inventoryCartsale.totalCart() + ' MMK');

        var otherchanges = $('#othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        var netalltotal = Number(inventoryCartsale.totalCart()) - Number(inventoryCartsale.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalSellingAmount').val(inventoryCartsale.totalCart());
        $('#TotalDiscount').val(inventoryCartsale.discountCart());
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
            totalCost += cart[i].unitsellprice * cart[i].quantity;
        }
        return totalCost.toFixed(2);
    };

    obj.discountCart = function () { // -> return total cost
        var totalAmt = 0;
        for (var i in cart) {
            if (cart[i].discount != 0) {
                totalAmt += parseInt(cart[i].discount);
            }
         
        }
        return totalAmt.toFixed(2);
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
        var cartCopy = JSON.parse(localStorage.getItem("inventoryCartsale"));
        return cartCopy;
    };

    // ----------------------------
    return obj;
})();




