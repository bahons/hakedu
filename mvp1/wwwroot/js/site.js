// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.







/////////////////////////////////////////////////////////////
///////////////////// Location
/////////////////////////////////////////////////////////////


function GetLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(GetLocationMarket);
    } else {
        console.log("Разрешите доступ на геолокацию");
    }
}


function GetLocationMarketGenerator(id, name, add) {
    let section = document.getElementById("near-market");
    section.innerHTML = "";

    let a = document.createElement("a");
    a.href = "/Home/InMarket/" + id;

    let span1 = document.createElement("span");
    span1.className = "font-400 color-blue-dark";
    span1.innerHTML = name;

    let span2= document.createElement("span");
    span2.innerHTML = " | " + add;

    let i = document.createElement("i");
    i.className = "fa fa-lock-open";
    i.style = "color: green!important";

    a.appendChild(span1);
    a.appendChild(span2);
    a.appendChild(i);
    section.appendChild(a);
    console.log("generate");

    var nonemarket = document.getElementById(name);
    if (nonemarket !== null)
        nonemarket.style.display = "none";
}


function GetLocationMarket(position) {
    const urlFetch = window.location.origin + '/api/order/localmarket';
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "Lat": position.coords.latitude.toString(),
            "Lon": position.coords.longitude.toString()
        }),
    })
    .then(function (res) { return res.json(); })
    .then(function (data) {
        //console.log(data);
        if (data != null) {
            GetLocationMarketGenerator(data.id, data.marketName, data.address);
        }
        else {
            alert("По данному локацию не возможно найти Торговый точки");
        }
    })
    .catch((error) => { console.log(error) });
}

//////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////
/////////////////////// Market Order /////////////////////////
/////////////////////// In MArket ////////////////////////////

function AddProduct(code, name, unit, price, category) {
    ValidProduct(code);
    let modal = document.getElementById('modal-wizard-step-2');
    //document.getElementById('addcount').value
    document.getElementById('addproductcode').value = code;
    modal.className = "menu menu-box-modal rounded-m menu-active"
    document.getElementById('addproductname').innerHTML = name;
    document.getElementById('addproductprice').value = price;
    document.getElementById('addproductunit').value = unit;
    document.getElementById('productcategory').value = category;
    document.getElementById('addproductinfo').innerHTML = 'Код товара: ' + code;

    document.getElementsByClassName('menu-hider')[0].classList = 'menu-hider menu-active';
}

function ValidProduct(code) {
    let marketid = document.getElementById('marketname').value;

    const urlFetch = window.location.origin + '/api/order/valid';
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "MarketId": marketid,
            "ProductCode": code
        }),
    })
        .then(function (res) { return res.json(); })
        .then(function (data) {
            document.getElementById('addcount').value = data;
        })
        .catch((error) => { console.log("ValidProduct error") });
}

function AddProductClick() {
    document.getElementsByClassName('menu-hider')[0].classList = 'menu-hider';
    document.getElementById('modal-wizard-step-2').classList = 'menu menu-box-modal rounded-m';


    let marketid = document.getElementById('marketname').value;
    let code = document.getElementById('addproductcode').value;

    const urlFetch = window.location.origin + '/api/order/create';
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "MarketId": marketid,
            "ProductCode": code,
            "Count": document.getElementById('addcount').value,
            "ProductName": document.getElementById('addproductname').innerHTML,
            "Price": document.getElementById('addproductprice').value,
            "Unit": document.getElementById('addproductunit').value,
            "Category": document.getElementById('productcategory').value
        }),
    })
    .then(function (res) { return res.json(); })
    .then(function (data) {
        if (data == true) {
            BagCollect(code);
        }
        else {

        }
    })
    .catch((error) => { console.log("AddProductClick error") });
}

function BagCollect(code) {
    let section = document.getElementById('bag-product');
    var div1 = document.createElement("div");
    div1.className = "d-flex mb-0";

    var div2 = document.createElement("div");
    div2.className = "ms-3 w-100";

    var div3 = document.createElement("div");
    div3.className = "row";
    div3.style = "margin-bottom:4px!important;";

    var div4 = document.createElement("div");
    div4.className = "col";

    var h4 = document.createElement("h4");
    h4.innerHTML = document.getElementById('addproductname').innerHTML;

    var p = document.createElement("p");
    p.innerHTML = code;

    div4.appendChild(h4);
    div4.appendChild(p);

    var div5 = document.createElement("div");
    div5.className = "col";

    var div6 = document.createElement("div");
    div6.className = "stepper rounded-s scale-switch ms-n1 float-end";

    var a1 = document.createElement("a");
    a1.href = "#";
    a1.className = "stepper-sub";
    a1.id = code;
    a1.setAttribute("onclick", "EditOrder(-1, this.id)");

    var i1 = document.createElement("i");
    i1.className = "fa fa-minus color-theme opacity-40";
    a1.appendChild(i1);

    var input = document.createElement("input");
    input.type = "number";
    input.value = document.getElementById('addcount').value;

    var a2 = document.createElement("a");
    a2.href = "#";
    a2.className = "stepper-add";
    a2.id = code;
    a2.setAttribute("onclick", "EditOrder(1, this.id)");

    var i2 = document.createElement("i");
    i2.className = "fa fa-plus color-theme opacity-40";
    a2.appendChild(i2);

    div6.appendChild(a1);
    div6.appendChild(input);
    div6.appendChild(a2);
    div5.appendChild(div6);

    div3.appendChild(div4);
    div3.appendChild(div5);

    div2.appendChild(div3);
    div1.appendChild(div2);

    section.appendChild(div1)

    var hr = document.createElement("hr");
    section.appendChild(hr);


    var count = document.getElementById('bag-count-back').value;
    document.getElementById('bag-count-back').value = count;
    document.getElementById('bag-count').innerHTML = count + ' товар';
}


function EditOrder(number, elem) {
    const urlFetch = window.location.origin + '/api/order/editorder';
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "MarketId": document.getElementById('marketname').value,
            "ProductCode": elem,
            "Count" : number
        }),
    })
    .then(function (res) { return res.json(); })
    .then(function (data) {
        console.log(data);
        if (data !== true) {
            alert( data );
        }
    })
    .catch((error) => { console.log(error) });
}


function CheckPromo(code) {
    let chek = document.getElementById(code + '-sel');
    if (chek.checked == true) {
        fetch(window.location.origin + '/api/order/accpromo', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "MarketId": document.getElementById('marketname').value,
                "ProductCode": code
            }),
        })
        .then(function (res) { return res.json(); })
        .then(function (data) {
            console.log(data);
            if (data !== true) {
                alert(data);
            }
        })
        .catch((error) => { console.log(error) });
    }
    else {
        fetch(window.location.origin + '/api/order/delpromo', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "MarketId": document.getElementById('marketname').value,
                "ProductCode": code
            }),
        })
        .then(function (res) { return res.json(); })
        .then(function (data) {
            console.log(data);
            if (data !== true) {
                alert(data);
            }
        })
        .catch((error) => { console.log(error) });
    }
}

function DeleteOrder(id) {
    const urlFetch = window.location.origin + '/api/order/delete';
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "MarketId": document.getElementById('marketname').value,
            "ProductCode": id
        }),
    })
    .then(function (res) { return res.json(); })
    .then(function (data) {
        console.log(data);
        if (data !== true) {
            alert(data);
        }
        else if (data == true) {
            document.getElementById(id + '-del').remove();
            console.log("delete");
        }
    })
    .catch((error) => { console.log(error) });
}


function FixOrder() {

    let form = document.getElementById('fox-form').submit();

    //const urlFetch = window.location.origin + '/api/order/fix';
    //fetch(urlFetch, {
    //    method: 'POST',
    //    headers: {
    //        Accept: 'application/json',
    //        'Content-Type': 'application/json',
    //    },
    //    body: JSON.stringify({
    //        "Id": document.getElementById('marketname').value
    //    }),
    //})
    //    .then(function (res) { return res.json(); })
    //    .then(function (data) {
    //        console.log(data);
    //        if (data !== true) {
    //            alert(data);
    //        }
    //        else if (data == true) {
    //            //let fixa = document.getElementById('fix-state');
    //            //fixa.onclick;
    //            //setTimeout(function () { window.location.replace(window.location.origin + '/Home/Market') }, 3000)
    //            window.location.replace(window.location.origin + '/Home/Market');
    //        }
    //    })
    //    .catch((error) => { console.log(error) });
}

////////////////////////////////////////////////////////////////////
////////////////////// Search || Filter ///////////////////////////
////////////////////////////////////////////////////////////////////

function Category() {
    let psection = document.getElementById("prod-list");
    psection.innerHTML = "";
    document.getElementsByClassName('menu-hider')[0].className = 'menu-hider';
    document.getElementById('modal-wizard-step-1').className = 'menu menu-box-modal rounded-m';
    let catname = document.getElementById('catselect').value;
    const urlFetch = window.location.origin + '/api/order/product';
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "Category": catname,
            "MarketId": document.getElementById('marketname').value
        }),
    })
    .then(function (res) { return res.json(); })
    .then(function (data) {
        if (data !== null) {
            data.forEach(p => {
                let div = document.createElement("div");
                div.className = "search-result-list product-list";
                let h1 = document.createElement("h1");
                h1.innerHTML = p.products;
                div.appendChild(h1);
                let pp = document.createElement("p");
                pp.innerHTML = p.code;
                div.appendChild(pp);
                let a = document.createElement("a");
                a.href = "#";
                a.className = "bg-highlight"
                a.innerHTML = "+";
                a.addEventListener('click', function () {
                    AddProduct(p.code, p.products, p.unit, p.price, p.category);
                });
                div.appendChild(a);
                psection.appendChild(div);
                let hr = document.createElement("hr");
                hr.style = "margin: 0";
                psection.appendChild(hr);
            });
        }
        else {
            psection.innerHTML = "По данному категорию товары не найдены";
        }
    })
    .catch((error) => { console.log(error) });
}

function Search(e) {
    let psection = document.getElementById("prod-list");
    if (e.length >= 3) {
        psection.innerHTML = "";
        const urlFetch = window.location.origin + '/api/order/search';
        fetch(urlFetch, {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "Value": e
            }),
        })
        .then(function (res) { return res.json(); })
        .then(function (data) {
            if (data !== null) {
                data.forEach(p => {
                    let div = document.createElement("div");
                    div.className = "search-result-list product-list";
                    let h1 = document.createElement("h1");
                    h1.innerHTML = p.products;
                    div.appendChild(h1);
                    let pp = document.createElement("p");
                    pp.innerHTML = p.code;
                    div.appendChild(pp);
                    let a = document.createElement("a");
                    a.href = "#";
                    a.className = "bg-highlight";
                    a.innerHTML = "+";
                    a.addEventListener('click', function () {
                        AddProduct(p.code, p.products, p.unit, p.price);
                    });
                    div.appendChild(a);
                    psection.appendChild(div);
                    let hr = document.createElement("hr");
                    hr.style = "margin: 0";
                    psection.appendChild(hr);
                });
            }
            else {
                psection.innerHTML = "По данному названию товары не найдены";
            }
        })
        .catch((error) => { console.log(error) });
    }
    else {
        console.log(e);
    }
}


