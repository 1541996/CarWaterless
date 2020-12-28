// ***************************************************
// Shopping Cart functions

var inventoryCart = (function () {
    // Private methods and properties
    var cart = [];

    function Item(rawmaterialid, rawname, unitprice, quantity, amount, discount) {
        this.rawmaterialid = rawmaterialid
        this.rawname = rawname
        this.unitprice = unitprice
        this.quantity = quantity
        this.amount = amount
        this.discount = discount
      
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

    obj.addItemToCart = function (rawmaterialid, rawname, unitprice, quantity, amount, discount) {
        $(".checkout").attr('disabled', true);
        var output = "";
        var currencydata = "";
       
        $('#nettotal').remove();
        $('.checkout').remove();

        var dynamicitemname = rawname.replace(/ /g, '');
        output += `<tr id="tr_${dynamicitemname}">  
                       
                        <td class="td_stock">
                            ${rawname}   
                        </td>
                        <td class="alnright">
                            ${quantity}   
                         </td>
                         
                        <td class="alnright">
                            ${unitprice}
                        </td>
                        <td class="alnright">
                            ${unitprice * quantity}
                        </td>
                         <td class="alnright">
                            ${discount}
                        </td>
                       <td class="alncenter">
                              <button class="btn btn-info btn-icon"
                                                data-rawname="${rawname}"
                                                data-rawmaterialid="${rawmaterialid}"
                                               data-unitprice="${unitprice}"
                                                data-quantity="${quantity}"
                                                data-amount="${unitprice * quantity}"
                                                data-discount="${discount}"
                                             onclick="editItem(this)"><i class="fa fa-edit"></i></button>
                             <button class="btn btn-danger btn-icon" onclick="deleteItem('${rawname}')"><i class="fa fa-trash"></i></button>
                          
                          
                       </td>
            
                       
                    </tr>`;
       
        var nettotal = `<tr id="nettotal">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCart.totalCart()}</th>
                            <th class="alnright">${inventoryCart.discountCart()}</th>
                            <th></th>                   
                      </tr>`
     
        for (var i in cart) {
            if (cart[i].rawname === rawname) {
                //alert("Same Item")
                //saveCart();
                $('.tbody tr:last').after(nettotal);
                var otherchanges = $('#othercharges').val();
                if (otherchanges == "") {
                    otherchanges = 0;
                } else {
                    otherchanges = Number($('#othercharges').val());
                }
                //$('#netalltotal').val((inventoryCart.totalCart() - inventoryCart.discountCart()) + otherchanges);
                //$('#lblnetalltotal').text((inventoryCart.totalCart() - inventoryCart.discountCart()) + otherchanges);
                var netalltotal = Number(inventoryCart.totalCart()) - Number(inventoryCart.discountCart()) + otherchanges;
                $('#netalltotal').val(netalltotal);
                $('#lblnetalltotal').text(netalltotal);
                $('#TotalAmount').val(inventoryCart.totalCart());
                $('#TotalDiscount').val(inventoryCart.discountCart());


                return;
            }
        }

        var item = new Item(rawmaterialid, rawname, unitprice, quantity, amount, discount);
       
        cart.push(item);
        saveCart();
        $("#tbody_invoice").append(output);

      
        var nettotal = `<tr id="nettotal">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCart.totalCart()}</th>
                            <th class="alnright">${inventoryCart.discountCart()}</th>
                            <th></th>                   
                      </tr>`

        $('.tbody tr:last').after(nettotal);

        var otherchanges = $('#othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        //$('#netalltotal').val((inventoryCart.totalCart() - inventoryCart.discountCart()) + otherchanges);
        //$('#lblnetalltotal').text((inventoryCart.totalCart() - inventoryCart.discountCart()) + otherchanges);
        var netalltotal = Number(inventoryCart.totalCart()) - Number(inventoryCart.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalAmount').val(inventoryCart.totalCart());
        $('#TotalDiscount').val(inventoryCart.discountCart());


    };


    obj.editItemFromCart = function (rawmaterialid, rawname, quantity,unitprice,  amount, discount) {
        $(".checkout").attr('disabled', true);
        var output = "";
        var currencydata = "";

        $('#nettotal').remove();
        $('.checkout').remove();

        var dynamicitemname = rawname.replace(/ /g, '');
        output += `  <tr id = "tr_${dynamicitemname}" >                   
                        <td class="td_stock">
                            ${rawname}   
                        </td>
                        <td class="alnright">
                            ${quantity}   
                         </td class="alnright">
                         
                        <td class="alnright">
                            ${unitprice}
                        </td>
                        <td class="alnright">
                            ${unitprice * quantity}
                        </td>
                         <td class="alnright">
                            ${discount}
                        </td>
                       <td class="alncenter">
                              <button class="btn btn-info btn-icon"
                                                data-rawname="${rawname}"
                                                data-rawmaterialid="${rawmaterialid}"
                                                data-unitprice="${unitprice}"
                                                data-quantity="${quantity}"
                                                data-amount="${unitprice * quantity}"
                                                data-discount="${discount}"
                                        onclick="editItem(this)"><i class="fa fa-edit"></i></button>
                             <button class="btn btn-danger btn-icon" onclick="deleteItem('${rawname}')"><i class="fa fa-trash"></i></button>
                          
                          
                       </td>
                        </tr>
                       
                    `;

      

      
      
        for (var i in cart) {
            if (cart[i].rawname === rawname) {
                cart[i].rawname = rawname;
                cart[i].unitprice = unitprice
                cart[i].quantity = quantity
                cart[i].amount = amount
                cart[i].discount = discount
                var dynamicitemname = cart[i].rawname.replace(/ /g, '');
                saveCart();
                $('#tr_' + dynamicitemname).replaceWith(output);

                break;
            }
        }

        var nettotal = `<tr id="nettotal" class="alnright">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCart.totalCart()}</th>
                            <th class="alnright">${inventoryCart.discountCart()}</th>
                            <th></th>                   
                      </tr>`

        $('.tbody tr:last').after(nettotal);



        var otherchanges = $('#othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        var netalltotal = Number(inventoryCart.totalCart()) - Number(inventoryCart.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalAmount').val(inventoryCart.totalCart());
        $('#TotalDiscount').val(inventoryCart.discountCart());

     

    };


    obj.removeItemFromCart = function (rawname) { // Removes one item
        $('#nettotal').remove();
        for (var i in cart) {
            if (cart[i].rawname === rawname) { // "3" === 3 false
                var dynamicitemname = cart[i].rawname.replace(/ /g, '');
                $('#tr_' + dynamicitemname).remove();
                cart.splice(i, 1);

                break;
            }
        }
        saveCart();
        var nettotal = `<tr id="nettotal" class="alnright">
                            <th colspan="3" class="alnright">Total</th>
                            <th class="alnright">${inventoryCart.totalCart()}</th>
                            <th class="alnright">${inventoryCart.discountCart()}</th>
                            <th></th>                   
                      </tr>`



        $('.tbody tr:last').after(nettotal);
        var otherchanges = $('othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        var netalltotal = Number(inventoryCart.totalCart()) - Number(inventoryCart.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalAmount').val(inventoryCart.totalCart());
        $('#TotalDiscount').val(inventoryCart.discountCart());

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

        var otherchanges = $('#othercharges').val();
        if (otherchanges == "") {
            otherchanges = 0;
        } else {
            otherchanges = Number($('#othercharges').val());
        }
        var netalltotal = Number(inventoryCart.totalCart()) - Number(inventoryCart.discountCart()) + otherchanges;
        $('#netalltotal').val(netalltotal);
        $('#lblnetalltotal').text(netalltotal);
        $('#TotalAmount').val(inventoryCart.totalCart());
        $('#TotalDiscount').val(inventoryCart.discountCart());
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
            totalCost += cart[i].unitprice * cart[i].quantity;
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
        var cartCopy = JSON.parse(localStorage.getItem("inventoryCart"));
        return cartCopy;
    };

    // ----------------------------
    return obj;
})();




