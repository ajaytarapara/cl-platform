
$(document).ready(function () {
    releatedmission();
    addtofavi(abc, flag);
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



function addtofavi(abc, flag) {

    var val=$(abc).val();
    $.ajax({
        type: "POST",
        url: '/Mission/addToFavourites',
        data: { missionid: val.toString(), fav: flag },
        success: function (data) {
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log("favourite");

            loadData();
            console.log(error);
        }
    })
};
