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

function GenerarFavoritos() {
    let component = `<h1>Favoritos</h1>`
    return component;
}

$(document).ready(function () {
    GenerarInicio();
    song.pause();

    $(".link_container").on("click", function () {
        let selector = $(".color_seleccion");
        num = $(".link_container").index(this)
        let mover = num * getSize();
        $(selector).css("top", mover)

    })

    $(window).on("resize", function () {
        $(".color_seleccion").css("top", num * getSize())

    });

    //Generar Elementos con el menu

    $(".menu").on("click", function () {
        $(this).toggleClass("hidden");
        $("nav").toggleClass("hidden");
        $("main").toggleClass("hidden")
    })

    $("#Favoritos").on("click", function () {

        $("main").addClass("animateUpwards");
        $("main").on("animationend", function () {
            $("main").empty();
            $("main").append(GenerarFavoritos());
            $("main").removeClass("animateUpwards");

            
        });


    })
    $("#Inicio").on("click", function () {

        $("main").addClass("animateUpwards");
        $("main").on("animationend", function () {
            $("main").empty();
            $("main").append(GenerarInicio());
        });
    })

})


let progress = document.getElementById("progresion");
let song = document.getElementById("cancion");
let ctrlIcon = document.getElementById("ctrlIcon");

song.onloadedmetadata = function () {
    progresion.max = song.duration;
    progresion.valor = song.currentTime;


}
function playPause() {
    if (ctrlIcon.classList.contains("fa-pause")) {
        song.pause();
        ctrlIcon.classList.remove("fa-pause");
        ctrlIcon.classList.add("fa-play");

    } else {
        song.play();
        ctrlIcon.classList.add("fa-pause");
        ctrlIcon.classList.remove("fa-play");

    }
}

if (song.play()) {
    setInterval(() => {
        progress.value = song.currentTime;
    }, 500);
}
progress.onchange = function () {
    song.play();
    song.currentTime = progress.value;
    ctrlIcon.classList.add("fa-pause");
    ctrlIcon.classList.remove("fa-play");
}





