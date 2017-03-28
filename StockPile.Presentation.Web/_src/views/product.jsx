import React from 'react';

module.exports = class Product extends React.Component {
    render() {
        return (
            <h1>Product {this.props.params.id}</h1>
        );
    }
}
