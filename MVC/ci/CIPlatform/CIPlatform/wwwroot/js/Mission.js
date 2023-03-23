
$(document).ready(function () {
    releatedmission();
    var val = $("#heartbtn").val();
    addtofavi(val, flag);
    var missionId = $("#post-comment-btn").val();
    listComment(missionId);
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

            $('#Recentvol').html("");
            $('#Recentvol').html(data);

        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })
};


