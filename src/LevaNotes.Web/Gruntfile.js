/// <binding />
// <binding ProjectOpened='watch:tasks' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
var sass = require('sass');
module.exports = function (grunt) {
    grunt.initConfig({
        uglify: { //minify task
            js: {
                files: {
                    './wwwroot/js/site.min.js': './wwwroot/js/site.js' // 'destination': 'source'
                }
            }
        },
        cssmin: {
            css: {
                src: './wwwroot/css/site.css',
                dest: './wwwroot/css/site.min.css'
            }
        },
        sass: {                              
            dist: {                            
                options: {                      
                    style: 'expanded',
                    implementation: sass,
                    sourceMap: true,
                },
                files: {                         
                    './wwwroot/css/site.css': './wwwroot/scss/site.scss'  // 'destination': 'source'      
                }
            },
        },
        watch: { //watching these files for changes
            js: {

                files: ['./wwwroot/js/site.js'],
                tasks: ['uglify']
            },
            css: {
                files: ['./wwwroot/scss/**/*.scss'],
                tasks: ['sass', 'cssmin']
            },
            //cssmin: {
            //    files: ['./wwwroot/css/site.css'],
            //    tasks: ['cssmin']
            //}
        }
    });

    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
};
