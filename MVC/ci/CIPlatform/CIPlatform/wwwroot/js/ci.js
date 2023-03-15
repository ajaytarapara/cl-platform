var elements = document.getElementsByClassName("card");
var img = document.getElementsByClassName("card-img-top");
var cardbody = document.getElementsByClassName("card-body")

// Declare a loop variable
var i;

function gridView() {
    for (i = 0; i < elements.length; i++) {
        elements[i].style.width = "32%";
    }
}
let flag = 0;
function menubartoggle() {
    let p = document.getElementById("menubarimg");
    let q = document.getElementById("menubar");
    if (flag % 2 == 0) {
        q.setAttribute("style", "display:block");
    }
    else {
        q.setAttribute("style", "display:none");
    }
    console.log(flag);
    flag++;

}
function closemenu() {
    let p = document.getElementById("closingimg");
    let q = document.getElementById("menubar");
    q.setAttribute("style", "display:none");
}

let flagfilter = 0;
function filterbartoggle() {
    let p = document.getElementById("closingimg");
    let q = document.getElementById("filterbar");
    if (flagfilter % 2 == 0) {
        q.setAttribute("style", "display:block");
    }
    else {
        q.setAttribute("style", "display:none");
    }
    console.log(q);
    console.log(p);
    flagfilter++;
}
function closefilter() {
    let p = document.getElementById("closingimg");
    let q = document.getElementById("filterbar");
    q.setAttribute("style", "display:none");
}
// tabs in volunteer
function openCity(evt, cityName) {
    console.log(evt);
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}


//carosel img//
const carouselImages = document.querySelectorAll(".carousel-images");
const previewImage = document.getElementById("preview-image");
for (let i = 0; i < carouselImages.length; i++) {
    carouselImages[i].addEventListener("click", (e) => {
        e.preventDefault();
        previewImage.src = carouselImages[i].src;
    });
}

function openCity(evt, cityName) {
    console.log(evt);
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}



