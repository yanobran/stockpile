import React from 'react';
import { Router, Route, hashHistory } from 'react-router';
import { Provider } from 'mobx-react';
import inventoryStore from './store/InventoryStore';
import StockPile from './stockpile';
import Product from './views/product';
import Products from './views/products';
import ShoppingCart from './views/ShoppingCart';
import Order from './views/Order';

module.exports = class App extends React.Component {
    render() {
        return (
            <Provider inventoryStore={inventoryStore}>
                <Router history={hashHistory}>
                    <Route path="/" component={StockPile}>
                        <Route path="products" component={Products} />
                        <Route path="product/:id" component={Product} />
                        <Route path="cart" component={ShoppingCart} />
                        <Route path="order" component={Order} />
                        <Route path="*" component={NoMatch} />
                    </Route>
                </Router>
            </Provider>
        );
    }
};

const NoMatch = class NoMatch extends React.Component {
    render() {
        return (<h5>Invalid link, sorry!</h5>);
    }
};
