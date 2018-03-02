'use strict';
import { observable, action, computed, autorun } from 'mobx';
import apiClient from '../services/apiclient';

// mobx Best Practices: https://mobx.js.org/best/store.html
// mobx shows a great way to build a more scalable store

export default class CartStore {
    @observable cart = {};
    
    constructor() {
        this.cart = {};
    }

    @action addToCart(product) {
        if(this.cart[product.Id] === undefined) {
            this.cart[product.Id] = {
                Id: product.Id,
                Name: product.Name,
                Price: product.Stock.Price,
                Qty: 0,
                Total: 0
            };
        }
    }

    @action changeQty(productId, qty) {
        var item = this.cart[productId];
        if(item != null) {
            item.Qty = qty;
            item.Total = item.Price * qty;
            item.Total = item.Total.toFixed(2);
            this.cart[productId] = item;
        }
    }

    @action removeItem(productId) {

        if(this.cart[productId])
            delete this.cart[productId];        

    }

    @action submitCart() {
        //var products = [];
        var products = {};
        console.log('submitting!');
        Object.keys(this.cart).map(function(key) {
            //products.push({Key: key, Value: this.cart[key].Qty})
            products[key] = this.cart[key].Qty;
        }, this);
        
        var payload = {
            Products: products,
            UserId: null
        };
        console.log("payload: " + JSON.stringify(payload));

        return new Promise((resolve, reject) => {
            apiClient.SubmitCart(payload).then((data) => {    
                resolve(data);
            }).catch((err) => {
                reject();
            });
        });
    }

}
