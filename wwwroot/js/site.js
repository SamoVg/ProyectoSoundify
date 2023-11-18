// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var num = 0

function getSize() {
    let nav = document.getElementById("nav")
    let travel = nav.offsetHeight / 6;

    return travel
}

//Subir los elementos para que desaparezcan

//Genera los tags html
function GenerarInicio() {
    let component = `
    <div class="container">
    <div class="reproductor">
        <section>
            <div class="circle" >
                <i class="fa-solid fa-chevron-left"></i>
            </div>
            <div class="circle">
                <i class="fa-solid fa-bars" color="black"></i>
            </div>
        </section>
        <img src="../img/login_fondo.png" class="cancion-img">
        <h1>Mojabi Ghost</h1>
        <p>Bad Bunny, Tainy</p>

        <audio id="cancion">
            <source src="Preview_Corporate Loop 01.mp3" type="audio/mpeg">
        </audio>
        <input type="range" value="0" id="progresion">

        <div class="controles">
            <div><i class="fa-solid fa-backward"></i></div>
            <div onclick="playPause()"><i class="fa-solid fa-play" id="ctrlIcon"></i></div>
            <div><i class="fa-solid fa-forward"></i></div>
        </div>

    </div>
    
</div>
    
    `
    return component;
}



$(".menu").on("click", function () {
    $(this).toggleClass("hidden");
    $("nav").toggleClass("hidden");
    $("main").toggleClass("hidden")
    console.log("Prueba")
})
$(".link_container").on("click", function () {
    let selector = $(".color_seleccion");
    num = $(".link_container").index(this)
    let mover = num * getSize();
    $(selector).css("top", mover)

})


$(window).on("resize", function () {
    $(".color_seleccion").css("top", num * getSize())

});


$(document).ready(function () {
    GenerarInicio();
    song.pause();

    

   
})
