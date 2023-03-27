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







$(document).ready(function () {
    releatedmission();
    var val = $("#heartbtn").val();
    addtofavi(val, flag);
    var missionId = $("#post-comment-btn").val();
    listComment(missionId);
    RecentVolunteer(missionid);
    //addtofavi(abc, flag);
});

var missiontitle = $('#missiontheme').text();
var city = $('#missioncity').text();
console.log(city);
var country = "";
function releatedmission() {
    $.ajax({
        type: "get",
        url: "/Mission/GetReleatedMission",
        data: { missiontitle: missiontitle, city:city,country: country},
        success: function (data) {
            var html = "";
            //console.log(data);
            var releatedmission = $("#releatedmissions");

            releatedmission.html("");
            releatedmission.html(data);
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}
//==============================================================================================
//ADD TO FAVOURITE
//=============================================================================================
function addtofavi(abc, flag) {

    var val=$(abc).val();
    $.ajax({
        type: "POST",
        url: '/Mission/addToFavourites',
        data: { missionid: val, fav: flag },
        success: function (data) {
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })
};

//========================================================================================
//POST COMMENT
//========================================================================================
$("#post-comment-btn").on("click", function (e) {
    e.preventDefault();

    var missionid = $(this).val()
    var comments = document.getElementById("commentText").value;
    var val = missionid;
    $.ajax({
        type: "POST",
        url: '/Mission/PostComments',
        dataType: "html",
        data: { MissionID: val, comment: comments.toString() },
        success: function (data) {

            //$('#CommentsList').html("");
            //$('#CommentsList').html(data);
            listComment(missionid);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});
//===============================================================================================
//RETRIVE COMMENT
//================================================================================================
function listComment(missionid) {
    //var comments = $("#commentText").text();
    var comments = document.getElementById("commentText").value;
    var val = missionid;
    $.ajax({
        type: "GET",
        url: '/Mission/ListComments',
        dataType: "html",
        data: { MissionID: val, comment: comments.toString() },
        success: function (data) {

            $('#CommentsList').html("");
            $('#CommentsList').html(data);

        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })
};
//================================================================================================
//RECOMMANDED TO CO WORKER
//=================================================================================================
$("#btn-recommend").on("click", function (e) {
    e.preventDefault();

    var missionid = $(this).val();
    var co_email = document.getElementById("receipient-email").value;
    var val = missionid;
    $.ajax({
        type: "POST",
        url: '/Mission/recommendedtocoworker',
        dataType: "html",
        data: { Missionid: val, cow_email: co_email.toString() },
        success: function (data) {
        
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});

var missionid = $("#applymission").val();
$("#applybtn").on("click", function (e) {
    e.preventDefault();
    $.ajax({
        type: "POST",
        url: '/Mission/ApplyApplication',
        dataType: "html",
        data: { Missionid: missionid},
        success: function (data) {
            alert('you have applied mission.')
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});


/*ratings*/

$("#rating .rating").click(function () {
    alert("Hello Rating");
    //var rating = $(".user-rating").val();
    var rating = $("#add-rating").serialize();
    //var rating = rating1.rate.val();

    const rateValue = rating.substring(rating.length, rating.lastIndexOf("=") + 1)
    $.ajax({
        url: '/Mission/MissionUserRating',
        type: 'POST',
        data: { ratingCount: rateValue, missionid: missionid },
        success: function (CountryResult) {
            alert('Rating submitted.');
        },
        error: function () {
            alert('Rating Failed!');
        }
    });
});


/*recent volunteer*/
var missionid = $("#applymission").val();
function RecentVolunteer(missionid) {
    $.ajax({
        type: "GET",
        url: '/Mission/GetRecentVolunteer',
        dataType: "html",
        data: { missionid: missionid},
        success: function (data) {

            $('#paginated-list').html("");
            $('#paginated-list').html(data);

        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })
};


