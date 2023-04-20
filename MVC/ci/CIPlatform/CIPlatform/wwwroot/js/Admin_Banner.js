//=====================================================================================================
//search ajax in mission theme page crud admin
//======================================================================================================
$(document).ready(function () {
    loadbannner();
});
var searchbannertext = "";
var pageSize = 2;
var pageNumber = 1;
$("#search_Banner_adminbtn").on("keyup", function (e) {
    e.preventDefault();
    searchbannertext = $("#search_Banner_adminbtn").val();
    console.log("VALUE:" + searchbannertext);
    if (searchbannertext.length > 2) {
        loadbannner();
    }
    else {
        searchbannertext = "";
        loadbannner();
    }
});

function loadbannner() {
    searchbannertext = $("#search_Banner_adminbtn").val();
    $.ajax({
        type: "POST",
        url: "/Admin/Admin_Banner",
        dataType: "html",
        data: { searchText: searchbannertext, pageNumber: pageNumber, pageSize: pageSize },
        success: function (data) {
            $("#bannerdatalist").html("");
            $("#bannerdatalist").html(data);
            loadPagination();
            //applicationapprove();
            /*Missionthemedelete();*/
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
        loadbannner();
    });
}