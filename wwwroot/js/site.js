// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



$(".menu").on("click", function () {
    $(this).toggleClass("hidden");
    $("nav").toggleClass("hidden");
    $("main").toggleClass("hidden")
    console.log("Prueba")
})



