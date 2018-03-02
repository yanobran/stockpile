import React from 'react';
import { inject, observer } from 'mobx-react';
import { hashHistory } from 'react-router';

@inject('cartStore') 
@inject('inventoryStore') 
@observer class Products extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.addToCart = this.addToCart.bind(this);
    }
    
    truncate(str, size) {
        if(str.length > size) {
            return str.substring(0, Math.min(size, str.length)) + '...';
        }
        return str;
    }

    addToCart(product) {
        this.props.cartStore.addToCart(product);
        hashHistory.push('/cart');
    }

    render() {
        const inventory = [];
        
        if(this.props.inventoryStore.products) {
            for(var i = 0; i < this.props.inventoryStore.products.length; i++) {
                const product = this.props.inventoryStore.products[i];
                const temp = this.props.inventoryStore.brands.filter((b) => b.Id == product.Brand);
                const brand = temp[0] || { Name: '' };
                inventory.push(
                    <div className="col s12 m6 l4" key={product.Id}>
                        <div className="card">
                            <div className="card-image">
                                <img src={"/images" + product.ImageUrl + ".png"}/>
                            </div>
                            <div className="card-content">
                                <div className="row">
  	                                <div className="col s7">
                                        <h5>{product.Name}</h5>
                                    </div>
                                    <div className="col s5 right-align red-text text-darken-2">
                                        <h5>${product.Stock.Price}</h5>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col s12">
                                        <em>{'BRAND:    ' + brand.Name}</em>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col s12">
                                        <em>{'CATEGORY: ' + product.Category}</em>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col s12">{this.truncate(product.Description, 50)}</div>
                                </div>
                            </div>
                            <div className="card-action">
                                <div className="row" style={{marginBottom:"0"}}>
                                    <span className="col s6 offset-s6 right-align">
                                        <button className="waves-effect waves-light btn" onClick={(evt) => this.addToCart(product)}>+ Add to Cart</button>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                );
            }
        }

        return (
            <div>
                <div className="row">
                    {inventory}
                </div>
            </div>
        );
    }
}

module.exports = Products;
