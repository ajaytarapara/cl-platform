
//===========================================================================================
//Ajax calls
//===========================================================================================
$(document).ready(function () {
    loadgetgrid();
    notificationcount();
    getnotification();
    getnotificationsetting();
});
var a = $.ajax({
    type: "GET",
    url: "/Home/GetCountries",
    data: "{}",
    success: function (data) {
        var str = "";
        var countryDropDown = $("#countryDropDownList");
        for (var j = 0; j < data["data"].length; j++) {
            str += '<li class="p-1"><a class="dropdown-item" href = "#"> <input type="checkbox" name="country" value="' + data["data"][j].name + '"/> ' + data["data"][j].name + '</a></li>';
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
var b = $.ajax({
    type: "GET",
    url: "/Home/GetCities",
    data: "{}",
    success: function (data) {
        var str = "";
        var cityDropDown = $("#cityDropDownList");
        for (var j = 0; j < data["data"].length; j++) {
            str += '<li class="p-1"><a class="dropdown-item" href = "#"> <input type="checkbox" name="city" value="' + data["data"][j].name + '"/> ' + data["data"][j].name + '</a></li>';
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
var c = $.ajax({
    type: "GET",
    url: "/Home/GetThemes",
    data: "{}",
    success: function (data) {
        var str = "";
        var themeDropDown = $("#themeDropDownList");
        for (var j = 0; j < data["data"].length; j++) {
            str += '<li class="p-1"><a class="dropdown-item" href = "#"> <input type="checkbox" name="theme" value="' + data["data"][j].title + '"/> ' + data["data"][j].title + '</a></li>';
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
var d = $.ajax({
    type: "GET",
    url: "/Home/GetSkills",
    data: "{}",
    success: function (data) {
        var str = "";

        var skillDropDown = $("#skillDropDownList");
        for (var j = 0; j < data["data"].length; j++) {
            str += '<li class="p-1"><a class="dropdown-item" href = "#"> <input type="checkbox" name="skill" value="' + data["data"][j].skillName + '"/> ' + data["data"][j].skillName + '</a></li>';;
        } skillDropDown.append(str);

    },
    failure: function (response) {
        alert("failure");
    },
    error: function (response) {
        alert("Something went Worng");
    }

});
$.when(a, b, c, d, toggleGrid).done(function () {
    intializeChips();
    citySelectFromCountry();
});

var selectedCountries = "";
var selectedCities = "";
var selectedThemes = "";
var selectedSkills = "";
var searchText = "";
var sorting = "";
var explore = "";
function loadgetgrid(paging) {
    if (!paging)
        paging = 1;
    $.ajax({
        type: "POST",
        url: "/Home/gridSP",
        data: { country: selectedCountries, city: selectedCities, theme: selectedThemes, skill: selectedSkills, searchtext: searchText, sorting: sorting, pageNumber: paging, explore: explore },
        success: function (data) {
            var html = "";
            var griddata = $("#grid");

            griddata.html("");
            griddata.html(data);
            toggleListGrid();
            loadPagination();
            var missiontext = $("#missioncount").text()
            $('#sort li a').on('click', function () {
                sorting = $(this).text();
                loadgetgrid();
            });
            $('#explore li a').on('click', function () {
                explore = $(this).text();
                loadgetgrid();
            });
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}
function loadPagination() {
    var paging = "";
    $("#pagination li a").on("click", function (e) {
        e.preventDefault();
        paging = $(this).text();
        loadgetgrid(paging);
    })
}
//====================================================================
///*chips*/
//=====================================================================
function intializeChips() {
    $(".rightheader li a").on("click", function (e) {
        $(".home-chips .chips").append(
            '<div class="chip">' +
            $(this).text() +
            '<span class="closebtn" onclick="this.parentElement.style.display=\'none\'">&times;</span>'
        );
        $(".close-chips").show();
        $(".no-filter-text").hide();
        $(".close-chips").show();
        $(".close-chips").click(function () {
            $(".chip").each(function () {
                $(this).hide();
                $(".no-filter-text").show();
                $(".close-chips").hide();
            });
            selectedCountries = "";
            $.each($("#countryDropDownList li a input:checkbox:checked"), function () {
                $(this).prop("checked", false);
            });
            //=============================================================================================================================================================
            //        //city filters
            //=============================================================================================================================================================
            selectedCities = "";
            $.each($("#cityDropDownList li a input:checkbox:checked"), function () {

                $(this).prop("checked", false);
            });
            //        ====================================================================================================================================
            ////            //theme filters
            ////==========================================================================================================================================
            selectedThemes = "";
            $.each($("#themeDropDownList li a input:checkbox:checked"), function () {
                $(this).prop("checked", false);
            });
            //==========================================================================================================================================
            //            //skill filters
            //==========================================================================================================================================
            selectedSkills = "";
            $.each($("#skillDropDownList li a input:checkbox:checked"), function () {
                $(this).prop("checked", false);
            });

            loadgetgrid();

        });



        /*filter*/
        //======================================================================================================================================================
        //        //country filters
        //======================================================================================================================================================
        selectedCountries = "";
        $.each($("#countryDropDownList li a input:checkbox:checked"), function () {
            selectedCountries += $(this).val() + ",";

        });
        //======================================================================================================================================================
        //        //city filters
        //======================================================================================================================================================
        selectedCities = "";
        $.each($("#cityDropDownList li a input:checkbox:checked"), function () {
            selectedCities += $(this).val() + ",";
        });
        //        ======================================================================================================================================================
        ////            //theme filters
        ////======================================================================================================================================================
        selectedThemes = "";
        $.each($("#themeDropDownList li a input:checkbox:checked"), function () {
            selectedThemes += $(this).val() + ",";
        });
        //======================================================================================================================================================
        //            //skill filters
        //======================================================================================================================================================
        selectedSkills = "";
        $.each($("#skillDropDownList li a input:checkbox:checked"), function () {
            selectedSkills += $(this).val() + ",";
        });

        loadgetgrid();

    });


}

//======================================================================================================================================================
///*searchbar using text*/
//======================================================================================================================================================
$("#search-input").on("keyup", function (e) {
    searchText = $("#search-input").val();
    if (searchText.length > 2) {
        loadgetgrid();
    }
    else {
        searchText = "";
        loadgetgrid();
    }
});

function citySelectFromCountry() {
    $("#countryDropDownList li a input:checkbox").on("change", function () {
        $.ajax({
            type: "GET",
            url: "/Home/GetCitiesFromCountry",
            dataType: "json",
            data: { country: selectedCountries },
            success: function (data) {
                var str = "";
                var cityDropDown = $("#cityDropDownList");
                var obj = JSON.parse(data["data"]);
                for (var j = 0; j < obj.length; j++) {
                    str += '<li class="p-1"><a class="dropdown-item" href = "#"> <input type="checkbox" name="city" value="' + obj[j].Name + '"/> ' + obj[j].Name + '</a></li>';
                }
                cityDropDown.html(str);

                $(".rightheader li a").on("click", function (e) {
                    $(".home-chips .chips").append(
                        '<div class="chip">' +
                        $(this).text() +
                        '<span class="closebtn" onclick="this.parentElement.style.display=\'none\'">&times;</span>'
                    );
                    $(".close-chips").show();
                    $(".no-filter-text").hide();
                    $(".close-chips").show();
                    $(".close-chips").click(function () {
                        $(".chip").each(function () {
                            $(this).hide();
                            $(".no-filter-text").show();
                            $(".close-chips").hide();
                        });
                    });
                    $.each($("#cityDropDownList li a input:checkbox:checked"), function () {
                        selectedCities += $(this).val() + ",";
                    });
                    loadgetgrid();

                });



            },
            failure: function (response) {
                alert("failure");
            },
            error: function (response) {
                alert("Something went Worng");
            }

        });
    });
}


var toggleGrid = true;
function toggleListGrid() {
    //console.log("toggle");
    const missionListView = document.getElementById("mission-list");
    //console.log(missionListView);
    const missionGridView = document.getElementById("mission-grid");
    const gridViewBtn = document.getElementById("grid-view-btn");
    const listViewBtn = document.getElementById("list-view-btn");

    listViewBtn.addEventListener("click", (e) => {
        e.preventDefault();
        toggleGrid = false;

        toggleListGrid();
    });
    gridViewBtn.addEventListener("click", (e) => {
        e.preventDefault();
        toggleGrid = true;
        toggleListGrid();
    });
    if (!(toggleGrid == true)) {

        missionListView.setAttribute("style", "display:block !important;");
        missionGridView.setAttribute("style", "display:none !important;");
    }
    else {

        missionListView.setAttribute("style", "display:none !important;");
        missionGridView.setAttribute("style", "display:flex !important;");
    }



}

//==========================================
//////favourite missions
/////======================================
function addtofavi(abc, flag) {
    var val = $(abc).val();
    $.ajax({
        type: "POST",
        url: '/Home/addToFavourites',
        data: { missionid: val.toString(), fav: flag },
        success: function (data) {
            loadgetgrid();
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })
};

//===============================================================================================
//Notification
//===============================================================================================
var SlectedList = new Array();
function getnotification() {
    SlectedList = new Array();
    $("input.notification-setting-check:checked").each(function () {
        SlectedList.push(this.id);
    });
    $.ajax({
        type: "post",
        url: "/Home/GetNotification",
        data: { nSetting: SlectedList },
        success: function (data) {
            var notificationdivRecently = $("#notification-dropdown");
            const list = document.getElementById("notification-dropdown");
            var childCount = list.childElementCount;
            for (var i = 1; i < childCount; i++) {
                if (list.hasChildNodes()) {
                    list.removeChild(list.children[1]);
                }
            }
            var str = '';
            var str2 = "";
            var notificationdata = "";
            notificationdata = data["data"];
            for (var j = 0; j < notificationdata.length; j++)

            {
                var yesterdayDate = new Date();
                yesterdayDate.setDate(yesterdayDate.getDate() - 1);
                var notificationDate = new Date(notificationdata[j].createdAt.slice(0, 10));
                if (yesterdayDate < notificationDate) {
                    str += '<li class="p-2 border border-1 d-flex justify-content-between align-items-center"><div><img style="height:43px;width:45px;border-radius:85px" src="' + notificationdata[j].avatar+ '"/><span class="mx-3">' + notificationdata[j].notificationType + '</span><br><span class="mx-5">' + notificationdata[j].notificationText + '</span></div>';
                    if (notificationdata[j].status !="seen") {

                        str += '<input style="accent-color:orange" class="notificationstatus" type="checkbox" id="' + notificationdata[j].notificationId + '" checked/></li>';
                    }
                    else {
                        str += '<input style="accent-color:gray" class="notificationstatus" type="checkbox" id="' + notificationdata[j].notificationId + '" onclick="return false" checked/></li>';

                    }

                }
                else
                {
                    str2 += '<li class="p-2 border border-1 d-flex justify-content-between align-items-center"><div><img style="height:30px;width:30px" src="' + notificationdata[j].avatar + '"/><span  class="mx-3">' + notificationdata[j].notificationType + '</span><br><span class="mx-5">' + notificationdata[j].notificationText + '</span></div>';
                    if (notificationdata[j].status != "seen") {

                        str2 += '<input style="accent-color:orange" class="notificationstatus" type="checkbox" id="' + notificationdata[j].notificationId +'" checked/></li>';
                    }
                    else {
                        str2 += '<input style="accent-color:gray" class="notificationstatus" type="checkbox" id="' + notificationdata[j].notificationId +'" onclick="return false" checked/></li>';

                    }

                }
            }
            str += '<li style="background-color:lightgray" class="p-2 border border-1"><span> Older</span ></li >'
            notificationdivRecently.append(str);
            notificationdivRecently.append(str2);
            var county = "";
            $("#totalnotification").empty();
            var countdiv = $("#totalnotification");
            county = data["data"].length;
            countdiv.append(county);
            statusofnotification();
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

});
}
$("#clearnotificationbutton").on("click", function () {

    $.ajax({
        type: "post",
        url: "/Home/clearnotification",
        data: "",
        success: function (data) {
            notificationcount();
            getnotification();
            $("#notification-button").trigger("click");
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }
       
    });
});

function notificationcount() {
    $.ajax({
        type: "post",
        url: "/Home/GetNotificationCount",
        data: "",
        success: function (data) {
            var count = "";
            $("#totalnotification").empty();
            var countdiv = $("#totalnotification");
            count=data["data"];
            countdiv.append(count);
          
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}
function statusofnotification() {
$(".notificationstatus").on("change", function () {
    var notificationid = "";
    notificationid=this.id;
    $.ajax({
        type: "post",
        url: "/Home/changenotificationstatus",
        data: { notyid: notificationid},
        success: function (data) {
            $("#totalnotification").empty();
            $("#notification-button").trigger("click");
            $("#notification-button").trigger("click");
            getnotification();
            notificationcount();
           
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
});
}

function getnotificationsetting() {
    $.ajax({
        type: "get",
        url: "/Home/NotificationSetting",
        data: "",
        success: function (data) {
            var notyset = "";
            notyset = data["data"];
            console.log(notyset)
            $("#RecommendedMission")[0].checked = notyset.recommandedFromMission;
            $("#StoryApproval")[0].checked = notyset.storyApproval;
            $("#NewMissionAdded")[0].checked = notyset.newMissionAdded;
            $("#RecommendedStory")[0].checked = notyset.recommandedFromStory;
            $("#MissionApplicationApproval")[0].checked = notyset.applicationApproval;
            getnotification();
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}

$("#save-notification-btn").on("click", function () {
    var notificationsettingsvalue = new Array();
    $(".notification-setting-check").each(function () {
        notificationsettingsvalue.push(this.checked);
       
    });
        $.ajax({
            type: "post",
            url: "/Home/NotificationSetting",
            data: { notificationsetting: notificationsettingsvalue },
            success: function (data) {
            },
            failure: function (response) {
                alert("failure");
            },
            error: function (response) {
                alert("Something went Worng");
            }

        });
    getnotification();
 
});
