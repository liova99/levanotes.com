const gulp = require('gulp');
const sass = require('gulp-sass');
const sourceMaps = require('gulp-sourcemaps');
const watch = require('gulp-watch');
const gulpSrc = './wwwroot/scss/**/*.scss';

// you can choose a name ex gulp.task('sdfasdfasd')
gulp.task('sass-compile', function () {
    // in root dir -> wwwwroot -> scss -> "a" file (** means we don't specify a file name)
    //-> .* scss(all files with .scss extension)
    return gulp.src(gulpSrc)
        // conect the moduls
        .pipe(sourceMaps.init())
        .pipe(sass().on('error', sass.logError))
        .pipe(sourceMaps.write('./'))
        .pipe(gulp.dest('./wwwroot/css/'))
});

gulp.task('watch', function () {
    gulp.watch(gulpSrc, gulp.series('sass-compile'))
});