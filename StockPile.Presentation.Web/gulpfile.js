var util = require('gulp-util');
var gulp = require('gulp');
var webpack = require('webpack');
var webpackConfig = require('./webpack.config.js');

gulp.task('default', function (callback) {
    webpack(webpackConfig, function (err, stats) {
        if (err) util.log(err);
        else util.log('default', stats.toString({ colors: true }));
        callback();
    });
});

//gulp.task('default', function () {
//    console.log('\nUsage: gulp <command>');
//    console.log('\toptions for <command> are:');
//    console.log('\t  build');
//    console.log('\t  build-dev');
//    console.log();
//});

//gulp.task('build', function () {
//    var bundle = browserify({
//        entries: ['./Scripts/tutorial.jsx'],
//        debug: true
//    });

//    bundle
//        .transform('babelify', { presets: ['es2015', 'react'] })
//        .bundle()
//        .on('error', util.log)
//        .pipe(source('test.js'))
//        .pipe(gulp.dest('./public/'));
//});


//gulp.task('build-dev', function () {
//    console.log('building dev...');
//});

