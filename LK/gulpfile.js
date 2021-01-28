const
    gulp = require('gulp'),
    sass = require('gulp-sass'),
    uglify = require('gulp-uglify'),
    cssMin = require('gulp-minify-css')
    sourceMaps = require('gulp-sourcemaps'),
    watch = require('gulp-watch'),
    rename = require('gulp-rename'),
    concat = require('gulp-concat'),
    filter = require('gulp-filter'),

    rootPath = './wwwroot',
    srcScss = './wwwroot/scss/**/*.scss';

var libScripts = [
    // highlight.min.js', will be added in _Post.cshtml for performance reasons
    'wwwroot/lib/jquery/dist/jquery.min.js',
    //'wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.min.js',
]

var siteScripts = [
    'wwwroot/js/site.js'
]


gulp.task('scripts', async function () {

    var scripts = [];
    scripts = scripts.concat(libScripts, siteScripts);

    const filterMin = filter(['!*min.js'], { restore: true });

    return gulp
        .src(scripts)
        .pipe(sourceMaps.init())
        .pipe(filterMin)
        .pipe(uglify())
        .pipe(filterMin.restore)
        .pipe(concat('site.js'))
        .pipe(rename({ suffix: '.min' }))
        .pipe(sourceMaps.write('./'))
        .pipe(gulp.dest('./wwwroot/js/'))
});


gulp.task('sass-compile', function () {
    // in root dir -> wwwwroot -> scss -> "afile.scss" (** means we don't specify a file name)
    //-> .* scss(all files with .scss extension)
    return gulp
        .src(srcScss)
        .pipe(sourceMaps.init())
        .pipe(sass().on('error', sass.logError))
        .pipe(cssMin({ keepBreaks: false }))
        .pipe(rename({ suffix: '.min' }))
        .pipe(sourceMaps.write('./'))
        .pipe(gulp.dest('./wwwroot/css/'))
});

gulp.task('watch', function () {
    const srcJs = './wwwroot/js/**/*.js';
    const filterJs = '!./wwwroot/js/**/*min.js';

    gulp.watch(srcScss, gulp.series('sass-compile'));
    gulp.watch([srcJs, filterJs ], gulp.series('scripts'));
});