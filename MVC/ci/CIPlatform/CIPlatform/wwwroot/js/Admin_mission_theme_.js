//=====================================================================================================
//search ajax in mission theme page crud admin
//======================================================================================================
$(document).ready(function () {
    loadmissiontheme();
});
var search_mission_themetext = "";
var pageSize = 3;
var pageNumber = 1;
$("#search_mission_theme_adminbtn").on("keyup", function (e) {
    e.preventDefault();
    search_mission_themetext = $("#search_mission_theme_adminbtn").val();
    console.log("VALUE:" + search_mission_themetext);
    if (search_mission_themetext.length > 2) {
        loadmissiontheme();
    }
    else {
        search_mission_themetext = "";
        loadmissiontheme();
    }
});

function loadmissiontheme() {
    search_mission_themetext = $("#search_mission_theme_adminbtn").val();
    $.ajax({
        type: "POST",
        url: "/Admin/Admin_mission_theme",
        dataType: "html",
        data: { searchText: search_mission_themetext, pageNumber: pageNumber, pageSize: pageSize },
        success: function (data) {
            $("#themedatalist").html("");
            $("#themedatalist").html(data);
            loadPagination();
            //applicationapprove();
            Missionthemedelete();
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}
//========================================================================
//Pagination
//========================================================================
function loadPagination() {
    var totalPages = parseInt($(".Pagination-total")[0].id.slice(6));
    var paging = "";
    $("#pagination li a").on("click", function (e) {
        e.preventDefault();
        paging = $(this).text();
        if (!isNaN(paging)) {
            pageNumber = parseInt(paging);
        }
        else {
            if (paging == "<") {
                if (pageNumber != 1) {
                    --pageNumber;
                }
            }
            else if (paging == ">") {
                if (pageNumber != totalPages) {
                    ++pageNumber;
                }
            }
            else if (paging == ">>") {
                pageNumber = totalPages;
            }
            else if (paging == "<<") {
                pageNumber = 1;
            }
        }
        loadmissiontheme();
    });
}
//=====================================================================================================
// delete ajax in MS theme page crud admin
//======================================================================================================
var missionthemeidfordelete = "";
function Missionthemedelete() {
    $(".themedeletebtn").on("click", function () {
        missionthemeidfordelete = this.id;
        $.ajax({
            type: "POST",
            url: "/Admin/Admin_Delete_themes",
            data: { themeId: missionthemeidfordelete },
            success: function (data) {
                loadmissiontheme();
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