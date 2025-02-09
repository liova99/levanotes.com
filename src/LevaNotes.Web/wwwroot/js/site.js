// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let toTopButton = document.getElementById("toTopBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        /*toTopButton.style.display = "block";*/
        toTopButton.classList.add("showBtn")
    } else {
        //toTopButton.style.display = "none";
        toTopButton.classList.remove("showBtn")
    }
}

function scrollToTop() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For normal browsers
}
