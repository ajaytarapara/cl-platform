
$(document).ready(function () {

    /*    loadgetgridview();*/
    console.log("hello");
    storylist();

});



var a = $.ajax({
    type: "GET",
    url: "/Story/GetCountries",
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
    url: "/Story/GetCities",
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
    url: "/Story/GetThemes",
    data: "{}",
    success: function (data) {
        var str = "";
        var themeDropDown = $("#themeDropDownList");
        for (var j = 0; j < data["data"].length; j++) {
            str += '<li class="p-1"><a class="dropdown-item" href = "#"> <input type="checkbox" name="theme" value="' + data["data"][j].title + '"/> ' + data["data"][j].title + '</a></li>';
        }
        themeDropDown.append(str);
        console.log(data);
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
    url: "/Story/GetSkills",
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

$.when(a, b, c, d).done(function () {
    intializeChips();

});


/*chips*/
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
        });



        /*filter*/
        //========================================================================================================================================================================================
        //        //country filters
        //========================================================================================================================================================================================
        selectedCountries = "";
        $.each($("#countryDropDownList li a input:checkbox:checked"), function () {
            selectedCountries += $(this).val() + ",";

        });
        //========================================================================================================================================================================================
        //        //city filters
        //========================================================================================================================================================================================
        selectedCities = "";
        $.each($("#cityDropDownList li a input:checkbox:checked"), function () {
            selectedCities += $(this).val() + ",";
        });
        //        ==========================================================================================================================================================================================
        ////            //theme filters
        ////==================================================================================================================================================================================
        selectedThemes = "";
        $.each($("#themeDropDownList li a input:checkbox:checked"), function () {
            selectedThemes += $(this).val() + ",";
        });
        //==========================================================================================================================================================================================
        //            //skill filters
        //==================================================================================================================================================================================
        selectedSkills = "";
        $.each($("#skillDropDownList li a input:checkbox:checked"), function () {
            selectedSkills += $(this).val() + ",";
        });

  

    });


}

/*pagination*/

function storylist(paging) {
    if (!paging)
       paging = 1;
    $.ajax({
        type: "Get",
        url: "/Story/Storydata",
        data: { pageNumber: paging},
        success: function (data) {
            var html = "";
            //console.log(data);
            var griddata = $("#storylist");

            griddata.html("");
            griddata.html(data);
            loadPagination();
       
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
        console.log("in page");
        e.preventDefault();
        paging = $(this).text();
        storylist(paging);
    })
}