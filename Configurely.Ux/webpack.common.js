module.exports = {
    entry: {
        app: './app/app.js',
    },
    resolve: {
        extensions: ['.js', '.jsx']
    },
    output: {
        path: __dirname + './../Configurely.App/scripts/react',
        filename: 'dynamic.js'
    },
    module: {
        rules: [
        {
            test: /\.(js|jsx)$/,
            exclude: /node_modules/,
            use: {
              loader: "babel-loader"
            }
        }]
    }
};