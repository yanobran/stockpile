var path = require('path');
module.exports = {
    context: path.join(__dirname, '_src'),
    entry: {
        server: './server',
        client: './client'
    },
    output: {
        path: path.join(__dirname, 'wwwroot/js'),
        filename: '[name].bundle.js'
    },
    module: {
        loaders: [
            // Transform JSX in .jsx files
            {
                test: /\.jsx$/,
                loader: 'babel-loader',
                query: {
                    plugins: [
                        'transform-class-properties',
                        'transform-decorators-legacy'
                    ],
                    presets: ['es2015', 'react']
                }
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                query: {
                    plugins: [
                        'transform-class-properties',
                        'transform-decorators-legacy'
                    ],
                    presets: ['es2015']
                }
            }
        ],
    },
    resolve: {
        extensions: ['.js', '.jsx']
    }
};