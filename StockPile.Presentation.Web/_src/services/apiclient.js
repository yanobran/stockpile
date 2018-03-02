'use strict';

// TODO - refactor, pull the API URL and inject environment specific config file, wire up in build with gulp

const GetBrands = () => {
    return new Promise((resolve, reject) => {
        $.ajax('http://localhost:57339/api/inventory/brands', {
            method: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                Materialize.toast('Successfully retrieved brands!', 4000, 'green');
                return resolve(data);
            },
            error: () => {
                Materialize.toast('Error retrieving brands! :(', 4000, 'red');
                return reject();
            }
        });
    });
};

const GetCategories = () => {
    return new Promise((resolve, reject) => {
        $.ajax('http://localhost:57339/api/inventory/categories', {
            method: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                Materialize.toast('Successfully retrieved categories!', 4000, 'green');
                return resolve(data);
            },
            error: () => {
                Materialize.toast('Error retrieving categories! :(', 4000, 'red');
                return reject();
            }
        });
    });
};

const GetProducts = (qry) => {
    var path = '';
    var parameters = [];
    if(qry.name !== null) {
        parameters.push('name=' + qry.name);
    }
    if(qry.category !== null) {
        parameters.push('category=' + qry.category);
    }
    if(qry.brand !== null) {
        parameters.push('brand=' + qry.brand);
    }

    path = parameters.join('&');
    if (path !== '') {
        path = '?' + path;
    }
    
    return new Promise((resolve, reject) => {
        $.ajax('http://localhost:57339/api/inventory/products' + path, {
            method: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                Materialize.toast('Successfully retrieved products!', 4000, 'green');
                return resolve(data);
            },
            error: () => {
                Materialize.toast('Error retrieving products! :(', 4000, 'red');
                return reject();
            }
        });
    });
}

const SubmitCart = (cart) => {
    return new Promise((resolve, reject) => {
        $.ajax('http://localhost:57339/api/fulfillment/submit', {
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(cart),
            success: (data) => {
                Materialize.toast('Successfully submitted cart!', 4000, 'green');
                return resolve(data);
            },
            error: () => {
                Materialize.toast('Error submitting cart! :(', 4000, 'red');
                return reject();
            }
        });
    });
};

module.exports = {
    GetBrands,
    GetCategories,
    GetProducts,
    SubmitCart
};
