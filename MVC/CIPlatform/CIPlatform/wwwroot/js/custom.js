﻿
//==============================================================================
//Ajax calls
//==============================================================================
$(document).ready(function () {
    loadCountry();
    loadCity();
    loadTheme();
    loadSkill();
    loadgetgridview();
});
function loadCountry() {
    $.ajax({
        type: "GET",
        url: "/Home/GetCountries",
        data: "{}",
        success: function (data) {
            var str = "";
            var countryDropDown = $("#countryDropDownList");
            for (var j = 0; j < data["data"].length; j++) {
                str += '<li class="p-1"><a class= "dropdown-item" href = "#" > <input type="checkbox" name="country"/> ' + data["data"][j].name + '</a></li>';
            }
            countryDropDown.append(str);
           

        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}
function loadCity() {
    $.ajax({
        type: "GET",
        url: "/Home/GetCites",
        data: "{}",
        success: function (data) {
            var str = "";
            var cityDropDown = $("#cityDropDownList");
            for (var j = 0; j < data["data"].length; j++) {
                str += '<li class="p-1"><a class= "dropdown-item" href = "#" > <input type="checkbox" name="country"/> ' + data["data"][j].name + '</a></li>';
            }
            cityDropDown.append(str);
    

        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}
function loadTheme() {
    $.ajax({
        type: "GET",
        url: "/Home/GetThemes",
        data: "{}",
        success: function (data) {
            var str = "";
            var themeDropDown = $("#themeDropDownList");
            for (var j = 0; j < data["data"].length; j++) {
                str += '<li class="p-1"><a class= "dropdown-item" href = "#" > <input type="checkbox" name="country"/> ' + data["data"][j].title + '</a></li>';
            }
            themeDropDown.append(str);
    

        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}

function loadSkill() {
    $.ajax({
        type: "GET",
        url: "/Home/GetSkills",
        data: "{}",
        success: function (data) {
            var str = "";
           console.log(data);
           var skillDropDown = $("#skillDropDownList");
            for (var j = 0; j < data["data"].length; j++) {
               str += '<li class="p-1"><a class= "dropdown-item" href = "#" > <input type="checkbox" name="country"/> ' + data["data"][j].skillName + '</a></li>';
           }            skillDropDown.append(str);


       },
        failure: function (response) {
            alert("failure");
       },
       error: function (response) {
           alert("Something went Worng");
        }

  });
}


function loadgetgridview() {
    $.ajax({
        type: "GET",
        url: "/Home/GetMission",
        data: "{}",
        success: function (data) {
            var data = JSON.parse(data["data"]);
            var str = "";
            var missiongrid = $("#grid");

            for (var j = 0; j < data.length; j++) {
                str += '<div class="col-lg-4 col-md-6 col-sm-12 mt-1 mission"> <div class="card mt-2 shadow"> <div class="list-image-part position-relative"><img src="/' + data[j].MissionMedia.Path + '+ "/" +' + data[j].MissionMedia.Name + '' +"." +data[j].MissionMedia.Type + '" class="list-img" alt="" /><h5 class="list-card-theme-title bg-white rounded-5 p-2">' + data[j].Theme.Title + '</h5><div id="grid-card-image-content" class="h-100 d-flex flex-column position-absolute  p-2"><div id="list-location-image" class="list-img-content p-2 rounded-5"><img src="/images/pin.png" alt="">' + data[j].City.Name + '</div><div class="d-flex flex-column align-items-end"><div id="list-img-heart" class="list-img-content p-2 rounded-5 m-1"><img src="/images/heart.png" alt=""></div><div id="list-user-img" class="list-img-content p-2 rounded-5 m-1"><img src="/images/user.png" alt=""></div></div></div></div><div class="card-body"><h5 class="card-title">' + data[j].Title + '</h5><p class="card-text">' + data[j].ShortDescription + ' </p><div style="justify-content:space-between;" class="d-flex "><span>' + data[j].OrganizationName + '</span><div style="color: orange; "><span class="fa fa-star checked"></span><span class="fa fa-star checked"></span><span class="fa fa-star checked"></span><span class="fa fa-star"></span><span class="fa fa-star"></span></div></div><div CLASS="mt-3 mb-3 d-flex missiondate"><div style="width:15%"><hr></div><div style="width:70% ;justify-content: center;border: 1px solid gray;border-radius: 18px;;"class="d-flex">From ' + data[j].StartDate.substring(0, 10) + ' untill ' + data[j].EndDate.substring(0, 10) + '</div><div style="width:15%"><hr></div></div><div style="justify-content:space-between;" class="d-flex pt-1 "><div><img src="/images/Seats-left.png" alt=""><span>10 <br> seats left</span></div><div><img style="height: 25px;" src="/images/hours.png" alt=""><span>09/01/2019 <br>Deadline</span> </div></div><hr><div style="justify-content: center;" class="d-flex pt-2"> <button style="background: #FFFFFF 0% 0% no-repeat padding-box; border: 2px solid #F88634; border-radius: 24px;"><a style="color: #F88634; text-decoration:href="http://">Apply</a><img src="/images/right-arrow.png" alt=""></button></div></div></div> </div>';
            }

           missiongrid.append(str);


        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}
