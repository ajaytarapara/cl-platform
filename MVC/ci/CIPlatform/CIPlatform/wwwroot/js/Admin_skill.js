//==================================================================================================
//search ajax in mission application page crud admin
//=================================================================================================
$(document).ready(function () {
    loadskill();
});
var searchskilltext = "";
var pageSize = 2;
var pageNumber = 1;
$("#search_skill_adminbtn").on("keyup", function (e) {
    e.preventDefault();
    searchskilltext = $("#search_skill_adminbtn").val();
    console.log("VALUE:" + searchskilltext);
    if (searchskilltext.length > 2) {
        loadskill();
    }
    else {
        searchskilltext = "";
        loadskill();
    }
});

function loadskill() {
    searchskilltext = $("#search_skill_adminbtn").val();
    $.ajax({
        type: "POST",
        url: "/Admin/Admin_skill",
        dataType: "html",
        data: { searchText: searchskilltext, pageNumber: pageNumber, pageSize: pageSize  },
        success: function (data) {
            $("#skilldatalist").html("");
            $("#skilldatalist").html(data);
            loadPagination();
            //applicationapprove();
            //applicationdelete();
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
        loadskill();
    });
}