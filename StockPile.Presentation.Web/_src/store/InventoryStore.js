'use strict';
import { observable, action, computed, autorun } from 'mobx';
import apiClient from '../services/apiclient';

// mobx Best Practices: https://mobx.js.org/best/store.html
// mobx shows a great way to build a more scalable store

class InventoryStore {
    @observable brands = [];
    @observable products = [];
    @observable categories = [];
    @observable filter = { name: "", category: "", brand: "" };
    @observable loading = true;

    constructor() {
        this.brands = [];
        this.products = [];
        this.categories = [];
        this.filter = { name: "", category: "", brand: "" };
        this.loadProducts();
    }

    @action loadBrands() {
        return new Promise((resolve, reject) => {
            apiClient.GetBrands().then((data) => { 
                this.brands = data;
                resolve();
            });
        });
    }

    @action loadCategories() {
        return new Promise((resolve, reject) => {     
            apiClient.GetCategories().then((data) => {
                this.categories = data;
                resolve();
            });
        });
    }

    @action loadProducts() {
        return new Promise((resolve, reject) => {
            apiClient.GetProducts(this.filter).then((data) => {    
                this.products = data;
                resolve();
            });
        });
    }

    @action updateFilter(prop, value) {
        this.filter[prop] = value;
    }

    getCategories = () => {
        return this.categories;
    }

}



var store = window.store = new InventoryStore();


export default store;
