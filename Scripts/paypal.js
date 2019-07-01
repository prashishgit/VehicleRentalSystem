var cartList = [];

/****

hideMessage();
                console.log(r);
                if (r != null) {
                    $("#cart-wrapper").hide();
                    $("#order-confirmation").hide();
                    $("#order-complete").hide();
                    $("#transaction-history").show();

                    if (r.payments <= 0) {
                        $("#transaction-list").html("<div class='info-message'>There are no payment records found.</div>");
                    } else {
                        var sb = "";
                        sb = sb + "<table class='tbl' cellpadding='0' cellspacing='0'>";
                        sb = sb + "<tr class='trHeader'>";
                        sb = sb + "<td>Date</td>";
                        sb = sb + "<td>Pay ID</td>";
                        sb = sb + "<td>Reference</td>";
                        sb = sb + "<td>Customer</td>";
                        sb = sb + "<td>Email</td>";
                        sb = sb + "<td>Amount Paid</td>";
                        sb = sb + "<td>Items</td>";
                        sb = sb + "<td>Status</td>";
                        sb = sb + "</tr>";

                        r.payments.forEach(function (payment) {
                            console.log(payment.transactions[0].related_resources[0], "???");
                            sb = sb + "<tr>";
                            sb = sb + "<td>" + moment(payment.create_time).format('MMMM Do YYYY, h:mm:ss') + "</td>";
                            sb = sb + "<td>" + payment.id + "</td>";
                            sb = sb + "<td>" + (payment.transactions[0].related_resources[0].sale != null ? payment.transactions[0].related_resources[0].sale.id : "") + "</td>";

                            sb = sb + "<td>" + (payment.payer.payer_info != null ? payment.payer.payer_info.first_name + " " + payment.payer.payer_info.last_name : payment.payer.funding_instruments[0].credit_card.first_name + " " + payment.payer.funding_instruments[0].credit_card.last_name) + "</td>";
                            sb = sb + "<td>" + (payment.payer.payer_info != null ? payment.payer.payer_info.email : "") + "</td>";
                            sb = sb + "<td>$" + payment.transactions[0].amount.total + "</td>";
                            sb = sb + "<td>";
                            if (payment.transactions[0].item_list != null) {
                                payment.transactions[0].item_list.items.forEach(function (item) {
                                    sb = sb + "<div>" + item.name + " (" + item.quantity + " X $" + item.price + ")" + "</div>";
                                });
                            }
                            sb = sb + "</td>";
                            sb = sb + "<td>" + payment.state + "</td>";
                            sb = sb + "</tr>";
                        });

                        sb = sb + "</table>";
                        $("#transaction-list").html(sb);
                    }
                }
				
				
				
				
				
				
				
				
				
				
				
				 hideMessage();
                if (r != null) {
                    if (r.state == "approved") {
                        $("#order-complete").show();
                        $("#order-confirmation").hide();
                        $("#cart-wrapper").hide();
                        $("#message-complete").html("<p>Thank you for your purchase. A copy of your purchase order and receipt has been sent to your email address. Your payment reference number is <span class='bold'>" + r.transactions[0].related_resources[0].sale.id + "</span></p>");
                        
                    } else {
                        $("#order-confirmation").show();
                        //build the payer information
                        var customer = r.payer.payer_info;
                        var sb = ""
                        sb = sb + "<table class='tbl' cellpadding='0' cellspacing='0'>";
                        sb = sb + "<tr><td class='td-label'>Customer Name</td><td>" + customer.first_name + " " + customer.last_name + "</td></tr>";

                        var billingAddress = customer.billing_address == null ? customer.shipping_address : customer.billing_address;
                        sb = sb + "<tr><td class='td-label'>Address</td><td>" + billingAddress.line1 + "<br/>" + (billingAddress.line2 == null ? "" : billingAddress.line2) + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>Suburb</td><td>" + billingAddress.city + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>State</td><td>" + billingAddress.state + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>Post Code</td><td>" + billingAddress.postal_code + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>Phone</td><td>" + (billingAddress.phone == null ? "" : billingAddress.phone) + "</td></tr>";
                        sb = sb + "</table>";
                        $("#billing-information").html(sb);

                        //shipping information
                        var shippingAddress = customer.shipping_address;
                        sb = ""
                        sb = sb + "<table class='tbl' cellpadding='0' cellspacing='0'>";
                        sb = sb + "<tr><td class='td-label'>Receipient Name</td><td>" + shippingAddress.recipient_name + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>Address</td><td>" + shippingAddress.line1 + "<br/>" + (shippingAddress.line2 == null ? "" : shippingAddress.line2) + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>Suburb</td><td>" + shippingAddress.city + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>State</td><td>" + shippingAddress.state + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>Post Code</td><td>" + shippingAddress.postal_code + "</td></tr>";
                        sb = sb + "<tr><td class='td-label'>Phone</td><td>" + (shippingAddress.phone == null ? "" : shippingAddress.phone) + "</td></tr>";
                        sb = sb + "</table>";

                        $("#shipping-information").html(sb);

                        if (r.transactions.length > 0) {
                            $("#divPaymentTo").html(r.transactions[0].payee.email);
                            $("#divInvoiceNumber").html(r.transactions[0].invoice_number);
                            $("#divOrderDescription").html(r.transactions[0].description);

                            var items = r.transactions[0].item_list.items;
                            sb = "";
                            sb = sb + "<tr class='trHeader'>";
                            sb = sb + "<td>Product Name</td>";
                            sb = sb + "<td>Unit Price</td>";
                            sb = sb + "<td>Qty</td>";
                            sb = sb + "<td>Sub Total</td>";
                            sb = sb + "</tr>";
                            items.forEach(function (item) {
                                var subTotal = parseInt(item.quantity) * parseFloat(item.price);
                                subTotal = Math.round(subTotal * 100) / 100;
                                sb = sb + "<tr><td>" + item.name + "</td><td>$" + parseFloat(item.price).toFixed(2) + "</td><td>" + item.quantity + "</td><td>$" + subTotal + "</td></tr>";
                            });

                            sb = sb + "<tr>";
                            sb = sb + "<td colspan='3'><div class='right bold'>Total:</div></td>";
                            sb = sb + "<td><div id='divSubTotal' class='bold'>$" + r.transactions[0].amount.details.subtotal + "</div></td>";
                            sb = sb + "</tr>";
                            sb = sb + "<tr>";
                            sb = sb + "<td colspan='3'><div class='right bold'>GST:</div></td>";
                            sb = sb + "<td><div id='divGST' class='bold'>$" + r.transactions[0].amount.details.tax + "</div></td>";
                            sb = sb + "</tr>";
                            sb = sb + "<tr>";
                            sb = sb + "<td colspan='3'><div class='right bold'>Shipping:</div></td>";
                            sb = sb + "<td><div id='divShipping' class='bold'>$" + r.transactions[0].amount.details.shipping + "</div></td>";
                            sb = sb + "</tr>";
                            sb = sb + "<tr>";
                            sb = sb + "<td colspan='3'><div class='right bold'>Final Amount:</div></td>";
                            sb = sb + "<td><div id='finalamount' class='bold'>$" + r.transactions[0].amount.total + "</div></td>";
                            sb = sb + "</tr>";
                            $("#tblCartConfirm").append(sb);
                        }
                    }
                }
***/
$(function () {

    $.ajax({
        type: "POST",
        url: "/API/PayPal/GetProducts",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        },
        success: function (responseData) {
            cartList = getCart();
            if (cartList == null || cartList != null && cartList.length == 0) {
                cartList = responseData.slice(0);
                saveCart();
            }
            buildProductList(cartList);

            //get params
            var params = getParams();
            if (Object.keys(params).length == 0) {
                //initial page load we want to show product list
                $(".btn-action, #btnViewCart").hide();
                $("#product-panel, #btnContinueShopping").show();
            } else {
                //confirmation payment mode
                if (params["payerid"] != null && params["token"] != null && params["paymentid"] != null) {
                    $(".panel-box, #btnCheckout").hide();
                    $("#confirmation-panel").show();
                    showMessage("<p><img src='/content/loading.gif'/></p><p>Please wait while we get the order information.</p>");

                    $.ajax({
                        type: "POST",
                        url: "/API/PayPal/GetPaymentDetails?paymentID=" + params["paymentid"],
                        error: function (xhr, status, error) {
                            console.log(xhr.responseText);
                        },
                        success: function (r) {
                            hideMessage();
                            if (r != null) {
                                if (r.state == "approved") {
                                    $(".panel-box, .buttons").hide();
                                    $("#complete-panel").show();
                                    $("#complete-box").html("<p>Thank you for your purchase. A copy of your purchase order and receipt has been sent to your email address. Your payment reference number is <span class='bold'>" + r.transactions[0].related_resources[0].sale.id + "</span></p>");

                                } else {
                                    $(".panel-box").hide();
                                    $("#confirmation-panel").show();
                                    $("#btnContinueShopping, #btnPay").show();
                                    //build the payer information
                                    var customer = r.payer.payer_info;
                                    var sb = ""
                                    sb = sb + "<table class='tbl' cellpadding='0' cellspacing='0'>";
                                    sb = sb + "<tr><td class='td-label'>Customer Name</td><td>" + customer.first_name + " " + customer.last_name + "</td></tr>";

                                    var billingAddress = customer.billing_address == null ? customer.shipping_address : customer.billing_address;
                                    sb = sb + "<tr><td class='td-label'>Address</td><td>" + billingAddress.line1 + "<br/>" + (billingAddress.line2 == null ? "" : billingAddress.line2) + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>Suburb</td><td>" + billingAddress.city + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>State</td><td>" + billingAddress.state + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>Post Code</td><td>" + billingAddress.postal_code + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>Phone</td><td>" + (billingAddress.phone == null ? "" : billingAddress.phone) + "</td></tr>";
                                    sb = sb + "</table>";
                                    $("#billing-information").html(sb);

                                    //shipping information
                                    var shippingAddress = customer.shipping_address;
                                    sb = ""
                                    sb = sb + "<table class='tbl' cellpadding='0' cellspacing='0'>";
                                    sb = sb + "<tr><td class='td-label'>Receipient Name</td><td>" + shippingAddress.recipient_name + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>Address</td><td>" + shippingAddress.line1 + "<br/>" + (shippingAddress.line2 == null ? "" : shippingAddress.line2) + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>Suburb</td><td>" + shippingAddress.city + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>State</td><td>" + shippingAddress.state + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>Post Code</td><td>" + shippingAddress.postal_code + "</td></tr>";
                                    sb = sb + "<tr><td class='td-label'>Phone</td><td>" + (shippingAddress.phone == null ? "" : shippingAddress.phone) + "</td></tr>";
                                    sb = sb + "</table>";

                                    $("#shipping-information").html(sb);

                                    if (r.transactions.length > 0) {
                                        $("#divPaymentTo").html(r.transactions[0].payee.email);
                                        $("#divInvoiceNumber").html(r.transactions[0].invoice_number);
                                        $("#divOrderDescription").html(r.transactions[0].description);

                                        var items = r.transactions[0].item_list.items;
                                        sb = "";
                                        sb = sb + "<tr class='trHeader'>";
                                        sb = sb + "<td>Product Name</td>";
                                        sb = sb + "<td>Unit Price</td>";
                                        sb = sb + "<td>Qty</td>";
                                        sb = sb + "<td>Sub Total</td>";
                                        sb = sb + "</tr>";
                                        items.forEach(function (item) {
                                            var subTotal = parseInt(item.quantity) * parseFloat(item.price);
                                            subTotal = Math.round(subTotal * 100) / 100;
                                            sb = sb + "<tr><td>" + item.name + "</td><td>$" + parseFloat(item.price).toFixed(2) + "</td><td>" + item.quantity + "</td><td>$" + subTotal + "</td></tr>";
                                        });

                                        sb = sb + "<tr>";
                                        sb = sb + "<td colspan='3'><div class='right bold'>Total:</div></td>";
                                        sb = sb + "<td><div id='divSubTotal' class='bold'>$" + r.transactions[0].amount.details.subtotal + "</div></td>";
                                        sb = sb + "</tr>";
                                        sb = sb + "<tr>";
                                        sb = sb + "<td colspan='3'><div class='right bold'>GST:</div></td>";
                                        sb = sb + "<td><div id='divGST' class='bold'>$" + r.transactions[0].amount.details.tax + "</div></td>";
                                        sb = sb + "</tr>";
                                        sb = sb + "<tr>";
                                        sb = sb + "<td colspan='3'><div class='right bold'>Shipping:</div></td>";
                                        sb = sb + "<td><div id='divShipping' class='bold'>$" + r.transactions[0].amount.details.shipping + "</div></td>";
                                        sb = sb + "</tr>";
                                        sb = sb + "<tr>";
                                        sb = sb + "<td colspan='3'><div class='right bold'>Final Amount:</div></td>";
                                        sb = sb + "<td><div id='finalamount' class='bold'>$" + r.transactions[0].amount.total + "</div></td>";
                                        sb = sb + "</tr>";
                                        $("#cart-confirm").append(sb);
                                    }
                                }
                            }
                        }
                    });
                }
            }

        }
    });

    $("#btnViewCart").off("click");
    $("#btnViewCart").on("click", function () {
        showCart(cartList);
    });

    $("#btnContinueShopping").off("click");
    $("#btnContinueShopping").on("click", function () {
        buildProductList(cartList);
    });

    $("#btnCheckout").off("click");
    $("#btnCheckout").on("click", function () {
        var jsonObject = {
            ProductList: getCart(),
            InvoiceNumber: "INV" + Math.floor((Math.random() * 999999) + 1000000),
            Currency: "USD",
            Tax: 0,
            ShippingFee: 0,
            OrderDescription: 'Sample Paypal Express Checkout',
            SiteURL: window.location.href.split('?')[0]
        }
        showMessage("<p><img src='/content/loading.gif'/></p><p>Please wait while we redirect you to PayPal website.</p>");
        $.ajax({
            type: "POST",
            url: "/Bookiing/PaymentWithPaypal",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(jsonObject),
            dataType: "json",
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            },
            success: function (responseData) {
                hideMessage();
                if (responseData.indexOf("ERROR") >= 0) {
                    $("#message").html("<div class='error-message'>" + responseData + "</div>");
                } else {
                    window.location.href = responseData;
                }
            }
        });
    });

    $("#btnBack").off("click");
    $("#btnBack").on("click", function () {
        $(".panel, #btnBack").hide();
        $("#btnShowTransactions").show();
        buildProductList(cartList);
    });

    $("#btnShowTransactions").off("click");
    $("#btnShowTransactions").on("click", function () {
        showMessage("<p><img src='/content/loading.gif'/></p>Please wait while we retrieve the payment transaction history from PayPal.");
        var jsonObject = {
            Count: 1000,
            StartID: "",
            StartIndex: "",
            EndTime: "",
            StartDate: "",
            PayeeEmail: "",
            PayeeID: "",
            SortBy: "create_time",
            SortOrder: "desc"
        }

        $.ajax({
            type: "POST",
            url: "/API/PayPal/GetPaymentHistory",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(jsonObject),
            dataType: "json",
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            },
            success: function (r) {
                hideMessage();
                if (r != null) {
                    $(".panel-box, .btn").hide();
                    $("#btnBack, #transaction-panel").hide();
                    if (r.payments <= 0) {
                        $("#transaction-box").html("<div class='info-message'>There are no payment transactions found.</div>");
                    } else {
                        $("#btnBack, #transaction-panel").show();
                        var sb = "";
                        sb = sb + "<table class='tbl' cellpadding='0' cellspacing='0'>";
                        sb = sb + "<tr class='trHeader'>";
                        sb = sb + "<td>Date</td>";
                        sb = sb + "<td>Pay ID</td>";
                        sb = sb + "<td>Reference</td>";
                        sb = sb + "<td>Customer</td>";
                        sb = sb + "<td>Email</td>";
                        sb = sb + "<td>Amount Paid</td>";
                        sb = sb + "<td>Items</td>";
                        sb = sb + "<td>Status</td>";
                        sb = sb + "</tr>";

                        r.payments.forEach(function (payment) {
                            console.log(payment.transactions[0].related_resources[0], "???");
                            sb = sb + "<tr>";
                            sb = sb + "<td>" + moment(payment.create_time).format('MMMM Do YYYY, h:mm:ss') + "</td>";
                            sb = sb + "<td>" + payment.id + "</td>";
                            sb = sb + "<td>" + (payment.transactions[0].related_resources[0].sale != null ? payment.transactions[0].related_resources[0].sale.id : "") + "</td>";

                            sb = sb + "<td>" + (payment.payer.payer_info != null ? payment.payer.payer_info.first_name + " " + payment.payer.payer_info.last_name : payment.payer.funding_instruments[0].credit_card.first_name + " " + payment.payer.funding_instruments[0].credit_card.last_name) + "</td>";
                            sb = sb + "<td>" + (payment.payer.payer_info != null ? payment.payer.payer_info.email : "") + "</td>";
                            sb = sb + "<td>$" + payment.transactions[0].amount.total + "</td>";
                            sb = sb + "<td>";
                            if (payment.transactions[0].item_list != null) {
                                payment.transactions[0].item_list.items.forEach(function (item) {
                                    sb = sb + "<div>" + item.name + " (" + item.quantity + " X $" + item.price + ")" + "</div>";
                                });
                            }
                            sb = sb + "</td>";
                            sb = sb + "<td>" + payment.state + "</td>";
                            sb = sb + "</tr>";
                        });

                        sb = sb + "</table>";
                        $("#transaction-box").html(sb);
                    }
                } else {
                    alert("Sorry there is an error getting the payment history transactions.");
                }
            }
        });
    });

    $("#btnPay").off("click");
    $("#btnPay").on("click", function () {
        if (confirm("Are you sure you want to make the payment for this order?")) {
            showMessage("<p><img src='/content/loading.gif'/></p>Please wait while we process the payment.");
            var params = getParams();
            var jsonObject = {
                PayerID: params["payerid"],
                PaymentID: params["paymentid"]
            }

            $.ajax({
                type: "POST",
                url: "/API/PayPal/ProcessPayment",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(jsonObject),
                dataType: "json",
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                },
                success: function (r) {
                    hideMessage();
                    $(".panel-box, .buttons").hide();
                    $("#complete-panel").show();
                    $("#complete-box").html("<p>Thank you for your purchase. A copy of your purchase order and receipt has been sent to your email address. Your payment reference number is <span class='bold'>" + r.transactions[0].related_resources[0].sale.id + "</span></p>");
                }
            });
        }
    });


});

function showCart(data) {
    $(".panel-box").hide();
    $("#cart-panel").show();

    var sb = "";
    if (data != null && data.length > 0) {
        sb = sb + "<table cellpadding='0' cellspacing='0' class='tbl'>";
        sb = sb + "<tr class='trHeader'>";
        sb = sb + "<td>Product Name</td>";
        sb = sb + "<td>Description</td>";
        sb = sb + "<td>SKU</td>";
        sb = sb + "<td>Price</td>";
        sb = sb + "<td>Qty</td>";
        sb = sb + "<td>Sub Total</td>";
        sb = sb + "<td></td>";
        sb = sb + "</tr>";

        data.forEach(function (product, index) {
            if (product.OrderQty > 0) {
                sb = sb + "<tr>";
                sb = sb + "<td>" + product.Name + "</td>";
                sb = sb + "<td>" + product.Description + "</td>";
                sb = sb + "<td>" + product.SKU + "</td>";
                sb = sb + "<td>$" + product.UnitPrice.toFixed(2) + "</td>";
                sb = sb + "<td>" + product.OrderQty + "</td>";
                sb = sb + "<td>$" + (Math.round((product.UnitPrice * product.OrderQty) * 100) / 100).toFixed(2) + "</td>";
                sb = sb + "<td><input type='button' class='btnDelete btn' data-id='" + product.ProductID + "' value='Delete'/></td>";
                sb = sb + "</tr>";
            }
        });

        sb = sb + "</table>";
    } else {
        sb = "<div class='message-info'>There are no products available.</div>";
    }

    $("#cart-box").html(sb);

    $(".btnDelete").off("click");
    $(".btnDelete").on("click", function () {
        var id = $(this).attr("data-id");
        cartList.forEach(function (item) {
            if (item.ProductID == id) {
                item.OrderQty = 0;
            }
        });
        saveCart();
        showCartAmount();
    });
}

function buildProductList(data) {
    $(".panel-box").hide();
    $("#product-panel").show();

    var sb = "";
    if (data != null && data.length > 0) {
        sb = sb + "<table cellpadding='0' cellspacing='0' class='tbl'>";
        sb = sb + "<tr class='trHeader'>";
        sb = sb + "<td>Product Name</td>";
        sb = sb + "<td>Description</td>";
        sb = sb + "<td>SKU</td>";
        sb = sb + "<td>Price</td>";
        sb = sb + "<td>Qty</td>";
        sb = sb + "<td></td>";
        sb = sb + "</tr>";

        data.forEach(function (product, index) {
            sb = sb + "<tr>";
            sb = sb + "<td>" + product.Name + "</td>";
            sb = sb + "<td>" + product.Description + "</td>";
            sb = sb + "<td>" + product.SKU + "</td>";
            sb = sb + "<td>$" + product.UnitPrice.toFixed(2) + "</td>";
            sb = sb + "<td><input id='txtQty" + product.ProductID + "' type='number' class='txtQty' max-length='3' data-id='" + product.ProductID + "' value='1'/></td>";
            sb = sb + "<td><input type='button' class='btnAddToCart btn' data-id='" + product.ProductID + "' value='Add to Cart'/></td>";
            sb = sb + "</tr>";
        });

        sb = sb + "</table>";
    } else {
        sb = "<div class='message-info'>There are no products available.</div>";
    }
    $("#product-box").html(sb);
    showCartAmount();

    $(".btnAddToCart").off("click");
    $(".btnAddToCart").on("click", function () {
        var id = $(this).attr("data-id");
        var qty = $("#txtQty" + id).val();
        if (qty <= 0) {
            alert("Please enter valid quantity");
        } else {
            cartList.forEach(function (item) {
                if (item.ProductID == id) {
                    item.OrderQty = parseInt(item.OrderQty) + parseInt(qty);
                }
            });
            saveCart();
            showCart(cartList);
        }
    });
}

function showCartAmount() {
    if (cartList != null && cartList.length > 0) {
        var qty = 0;
        var amount = 0;
        cartList.forEach(function (item) {
            if (item.OrderQty > 0) {
                qty = qty + item.OrderQty;
                amount = amount + item.UnitPrice * item.OrderQty;
            }
        });

        $("#btnCheckout, #btnViewCart").hide();
        if (qty > 0) {
            $("#btnCheckout, #btnViewCart").show();
        }

        $("#cart-amount").html((Math.round(amount * 100) / 100).toFixed(2));
        $("#no-of-items").html(qty);
    } else {
        $("#cart-amount").html("0.0");
        $("#no-of-items").html(0);
    }
}

function getParams() {
    var url = window.location.href;
    var params = {};
    var qs = url.indexOf("?") >= 0 ? url.split('?') : [];
    if (qs.length > 0) {
        var arr = qs[1].split('&');
        arr.forEach(function (entry, index) {
            var q = entry.split('=');
            if (q != null && q.length == 2) {
                params[q[0].toString().toLowerCase()] = q[1];
            }
        });
    }
    return params;
}

function saveCart() {
    localStorage.setItem("cart", JSON.stringify(cartList));
}

function getCart() {
    var currentCart = [];
    if (localStorage.getItem("cart") != null) {
        currentCart = JSON.parse(localStorage.getItem("cart"));
    }
    return currentCart;
}

function showMessage(message) {
    $("#transparent-bg").show();
    $("#loading-message").show();
    $("#loading-message").html(message);
}

function hideMessage() {
    $("#transparent-bg").hide();
    $("#loading-message").hide();
    $("#loading-message").html("");
}