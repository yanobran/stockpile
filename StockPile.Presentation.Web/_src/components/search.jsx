import React from 'react';
import { inject, observer } from 'mobx-react';
import { hashHistory } from 'react-router';

@inject('inventoryStore')
@observer class Search extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.onFilterChanged = this.onFilterChanged.bind(this);
        this.submitSearch = this.submitSearch.bind(this);
    }

    componentWillReact() {
        console.log("I will re-render, since the todo has changed!");
    }
    
    onFilterChanged(evt, prop) {
        this.props.inventoryStore.updateFilter(prop, evt.target.value);
    }

    submitSearch() {
        this.props.inventoryStore.loadProducts().then(() => {
            hashHistory.push('/products');
        });
    }

    render() {
        return (
            <form>
                <h5 className="center-align">SEARCH</h5>
                <div className="row">
                    <div className="col s12">
                        <input id="prod_name" type="text" style={{ margin: 0 }} onChange={(evt) => this.onFilterChanged(evt, 'name')} />
                        <label htmlFor="prod_name" style={{ marginBottom: 20 }}>Product Name</label>
                    </div>
                </div>
                <div className="row">
                    <div className="col s12">
                        <label htmlFor="category">Select Category</label>
                        <select id="category" className="browser-default" onChange={(evt) => this.onFilterChanged(evt, 'category')}>
                            <option value="" defaultValue>All</option>
                            {this.props.inventoryStore.categories.map(cat => {
                                return <option value={cat} key={cat}>{cat}</option>
                            })}
                        </select>
                    </div>
                </div>
                <div className="row">
                    <div className="col s12">
                        <label htmlFor="brand">Select Brand</label>
                        <select id="brand" className="browser-default" onChange={(evt) => this.onFilterChanged(evt, 'brand')} style={{ marginBottom: 20 }}>
                            <option value="" defaultValue>All</option>
                            {this.props.inventoryStore.brands.map(brand => {
                                return <option value={brand.Id} key={brand.Id}>{brand.Name}</option>
                            })}
                        </select>

                        <br />
                    </div>
                </div>
                <div className="row">
                    <a className="waves-effect btn-flat grey lighten-2" onClick={(evt) => this.submitSearch()}>SEARCH</a>
                </div>
            </form>
        );
    }
}

module.exports = Search;
