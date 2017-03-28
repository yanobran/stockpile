import React from 'react';
import { Link, hashHistory } from 'react-router';
import { inject, observer } from 'mobx-react';
import { autorun } from 'mobx';
import Search from './components/search';

@inject('inventoryStore')
@observer class StockPile extends React.Component {
    constructor(props, context) {
        super(props, context);
        
        this.state = {
            loading: true  
        };
    }
    
    componentDidMount() {
        if(this.state.loading) {
            var temp = this.props.inventoryStore.loadBrands();
            var temp2 = this.props.inventoryStore.loadCategories();
            temp.then(() => temp2).then(() => {
                setTimeout(() => {
                    this.setState({ loading: false })
                    hashHistory.push('/products');
                }, 1000);
            });
        };
    }
    componentDidUpdate() {
        $(".button-collapse").sideNav();
    }

    render() {
        if(this.state.loading) {
            return (
                <div style={{height:"500px"}} className="valign-wrapper">
                    <div className="row valign">
                        <div className="col S12 valign center-align">
                            <div className="row">
                                <h1>StockPile</h1>
                                <div className="progress">
                                    <div className="indeterminate"></div>
                                </div>
                                <i className="material-icons medium">receipt</i>
                            </div>
                        </div>
                    </div>
                </div>
            );
        }

        return (
            <div>
                <nav className="blue-grey">
                    <div className="nav-wrapper">
                        <a href="#" className="brand-logo"><i className="material-icons medium right">receipt</i>&nbsp;StockPile</a>
                        <a href="#" data-activates="side-nav-links" className="button-collapse"><i className="material-icons">menu</i></a>
                        <ul id="page-links" className="right hide-on-med-and-down">
                            <li><Link to="/products" activeClassName="active">Products</Link></li>
                            <li><Link to="/cart" activeClassName="active">Shopping Cart</Link></li>
                            <li><Link to="/profile" activeClassName="active">Profile</Link></li>
                        </ul>
                        <ul className="side-nav" id="side-nav-links">
                            <li><Link to="/products" activeClassName="active">Products</Link></li>
                            <li><Link to="/cart" activeClassName="active">Shopping Cart</Link></li>
                            <li><Link to="/profile" activeClassName="active">Profile</Link></li>
                            <li>
                                <div className="row">
                                    <div className="col black-text grey lighten-4 z-depth-1">
                                        <Search />
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </nav>
                <div>
                    <br />
                    <div className="row">
                        <div className="col s2 hide-on-med-and-down black-text grey lighten-4 z-depth-1">
                            <div className="row">
                                <Search />
                            </div>
                        </div>
                        <div className="col s12 m12 l10">
                            {this.props.children}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};

module.exports = StockPile;