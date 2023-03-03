var elements = document.getElementsByClassName("card");
var img = document.getElementsByClassName("card-img-top");
var cardbody = document.getElementsByClassName("card-body")

// Declare a loop variable
var i;
function listview() {
    for (i = 0; i < elements.length; i++) {
        elements[i].style.width = "100%";

    }
}
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


let tabcontent = document.getElementsByClassName("tabcontent");
let tablinks = document.getElementsByClassName("tablinks");
document.getElementById("Mission").style.display = "block";
tablinks[0].classList.add("active")


//volunteer pagination//
const paginationNumbers = document.getElementById("pagination-numbers");
const paginatedList = document.getElementById("paginated-list");
const listItems = paginatedList.querySelectorAll("li");
const nextButton = document.getElementById("next-button");
const prevButton = document.getElementById("prev-button");

const paginationLimit = 3;
const pageCount = Math.ceil(listItems.length / paginationLimit);
let currentPage = 1;

const disableButton = (button) => {
    button.classList.add("disabled");
    button.setAttribute("disabled", true);
};

const enableButton = (button) => {
    button.classList.remove("disabled");
    button.removeAttribute("disabled");
};

const handlePageButtonsStatus = () => {
    if (currentPage === 1) {
        disableButton(prevButton);
    } else {
        enableButton(prevButton);
    }

    if (pageCount === currentPage) {
        disableButton(nextButton);
    } else {
        enableButton(nextButton);
    }
};

const handleActivePageNumber = () => {
    document.querySelectorAll(".pagination-number").forEach((button) => {
        button.classList.remove("active");
        const pageIndex = Number(button.getAttribute("page-index"));
        if (pageIndex == currentPage) {
            button.classList.add("active");
        }
    });
};

const appendPageNumber = (index) => {
    const pageNumber = document.createElement("button");
    pageNumber.className = "pagination-number";
    pageNumber.innerHTML = index;
    pageNumber.setAttribute("page-index", index);
    pageNumber.setAttribute("aria-label", "Page " + index);

    paginationNumbers.appendChild(pageNumber);
};

const getPaginationNumbers = () => {
    for (let i = 1; i <= pageCount; i++) {
        appendPageNumber(i);
    }
};

const setCurrentPage = (pageNum) => {
    currentPage = pageNum;

    handleActivePageNumber();
    handlePageButtonsStatus();

    const prevRange = (pageNum - 1) * paginationLimit;
    const currRange = pageNum * paginationLimit;

    listItems.forEach((item, index) => {
        item.classList.add("hidden");
        if (index >= prevRange && index < currRange) {
            item.classList.remove("hidden");
        }
    });
};

window.addEventListener("load", () => {
    getPaginationNumbers();
    setCurrentPage(1);

    prevButton.addEventListener("click", () => {
        setCurrentPage(currentPage - 1);
    });

    nextButton.addEventListener("click", () => {
        setCurrentPage(currentPage + 1);
    });

    document.querySelectorAll(".pagination-number").forEach((button) => {
        const pageIndex = Number(button.getAttribute("page-index"));

        if (pageIndex) {
            button.addEventListener("click", () => {
                setCurrentPage(pageIndex);
            });
        }
    });
});

//carosel img//
const carouselImages = document.querySelectorAll(".carousel-images");
const previewImage = document.getElementById("preview-image");
for (let i = 0; i < carouselImages.length; i++) {
    console.log(carouselImages[i].src);
    carouselImages[i].addEventListener("click", (e) => {
        e.preventDefault();
        previewImage.src = carouselImages[i].src;
    });
}
console.log(carouselImages);
console.log(preview - image);

//aja//

$.ajax({
    type: "GET", // POST
    url: '/ControllerName/MethodName',
    data: { ApprovalCommentID: $(this).attr('#ajaxtest') },
    dataType: "html", // return datatype like JSON and HTML
    success: function (data) {

    },
    failure: function (e) {
        errorMessage(e.responseText);
    },
    error: function (e) {
        errorMessage(e.responseText);
    },
    complete: function (e) {

    }
});