import React from 'react';
import { inject, observer } from 'mobx-react';

@inject('inventoryStore') 
@observer class Products extends React.Component {

    truncate(str, size) {
        if(str.length > size) {
            return str.substring(0, Math.min(size, str.length)) + '...';
        }
        return str;
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
                                <h5>{product.Name}</h5>
                                <div className="row">
                                    <em>{'BRAND:    ' + brand.Name}</em>
                                </div>
                                <div className="row">
                                    <em>{'CATEGORY: ' + product.Category}</em>
                                </div>
                                <p>{this.truncate(product.Description, 50)}</p>
                            </div>
                            <div className="card-action">
                                <div className="row" style={{marginBottom:"0"}}>
                                <span className="col s6">
                                    <a>{product.Stock.Quantity}</a>
                                </span>
                                <span className="col s6 right-align">
                                    <a>${product.Stock.Price}</a>
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
