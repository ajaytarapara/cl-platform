
//==============================================================================
//Ajax calls
//==============================================================================
$(document).ready(function () {
    loadCountry();
    loadCity();
    loadTheme();
    loadSkill();
    loadMissionTitle();
    loadMissionDiscription();

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
        failure: function (response) {           alert("failure");
       },
       error: function (response) {
           alert("Something went Worng");
        }

  });
}


//function loadMissionTitle() {
//    $.ajax({
//        type: "GET",
//        url: "/Home/GetMissionThemestitle",
//        data: "{}",
//        success: function (data) {
//            var str = "";
//            var card-title= $(".card-body .card-title");
//            for (var j = 0; j < data["data"].length; j++) {
//                str += data["data"][j].card-title;
//            }
//            card-title.append(str);
//        },
//        failure: function (response) {
//            alert("failure");
//        },
//        error: function (response) {
//            alert("Something went Worng");
//        }

//    });
//}


function loadMissionDiscription() {
   $.ajax({
        type: "GET",
       url: "/Home/GetMissionDiscription",
       
       success: function (data)
       {
           console.log(data);
           debugger;
           var str = "";
           var loadMissionDiscription = $(".card-body");
           for (var j = 0; j < data["data"].length; j++) {
               str += '<p class="card-text"> ' + data["data"][j].loadMissionDiscription + '</p>';
            }
           loadMissionDiscription.append(str);


        },
        failure: function (response) {
           alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}