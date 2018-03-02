import React from 'react';
import { inject, observer } from 'mobx-react';
import { hashHistory } from 'react-router';

@inject('cartStore') 
@inject('inventoryStore') 
@observer class ShoppingCart extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            total: 0,
            update: false
        };

        this.goBack = this.goBack.bind(this);
        this.updateQty = this.updateQty.bind(this);
        this.checkout = this.checkout.bind(this);
        this.removeItem = this.removeItem.bind(this);
    }

    goBack() {
        hashHistory.goBack();
    }
    
    updateQty(evt, productId) {
        console.log('>>>updating ' + productId);
        this.props.cartStore.changeQty(productId, evt.target.value);
        this.setState({ update: true });
    }

    removeItem(productId) {
        this.props.cartStore.removeItem(productId);
        this.setState({ update: true });
    }

    checkout() {
        this.props.cartStore.submitCart().then((data) => {
            // TODO: navigate to orders and display pending/completed orders
            hashHistory.push('/products');
        }).catch((err) => {
            // handle errors
        });
    }

    render() {
        var cart = this.props.cartStore.cart;
        var ttl = 0;

        var cartTbl = [];

        for(var prop in cart) {
            var item = cart[prop];
            ttl += item.Price * item.Qty;

            const id = item.Id;

            cartTbl.push(
                <tr key={item.Id}>
                    <td>{item.Name}</td>
                    <td>${item.Price}</td>
                    <td>
                        <div className="input-field inline">
                            <input type="number" step="1" value={item.Qty} onChange={(evt) => { this.updateQty(evt, id) }} />
                        </div>
                    </td>
                    <td>${item.Total}</td>
                    <td>
                        <button className="btn-floating btn waves-effect waves-light red" onClick={(evt) => { this.removeItem(id) }}>X</button>
                    </td>
                </tr>
            );
        }

        ttl = ttl.toFixed(2);

        return (
            <div className="container">
                <div className="row">
                    <h1><i className="small material-icons">shopping_cart</i> Shopping Cart</h1>
                </div>
                <div className="row">
                    <div className="col s6">
                        <button className="waves-effect waves-light btn" onClick={(evt) => this.goBack()}>Go back</button>
                    </div>
                    <div className="col s6 right-align">
                        <button className="waves-effect waves-light btn" onClick={(evt) => this.checkout()}>Checkout</button>
                    </div>
                </div>
                <div className="row">
                    <table>
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Price</th>
                                <th>Quantity</th>
                                <th>Total</th>
                                <th>Remove</th>
                            </tr>
                        </thead>

                        <tbody>
                            {cartTbl}
                        </tbody>
                        <tfoot>
                            <tr style={{fontWeight: "bold", borderTop: "1px solid lightgrey"}}>
                                <td>Total</td>
                                <td></td>
                                <td></td>
                                <td>${ttl}</td>
                                <td></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        );
    }
}

module.exports = ShoppingCart;