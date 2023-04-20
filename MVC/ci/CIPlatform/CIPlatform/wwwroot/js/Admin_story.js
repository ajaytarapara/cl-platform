//=====================================================================================================
//search ajax in story page crud admin
//======================================================================================================
$(document).ready(function () {
    loadstory();
});
var searchstorytext = "";
var pageSize = 2;
var pageNumber = 1;
$("#searchstoryadmin").on("keyup", function (e) {
    e.preventDefault();
    searchcmstext = $("#searchstoryadmin").val();
    console.log("VALUE:" + searchcmstext);
    if (searchcmstext.length > 2) {
        loadstory();
    }
    else {
        searchcmstext = "";
        loadstory();
    }
});

function loadstory() {
    searchstorytext = $("#searchstoryadmin").val();
    $.ajax({
        type: "POST",
        url: "/Admin/Admin_story",
        dataType: "html",
        data: { searchText: searchstorytext, pageNumber: pageNumber, pageSize: pageSize },
        success: function (data) {
            $("#storydatalist").html("");
            $("#storydatalist").html(data);
            loadPagination();
            storyapprove();
            storydelete();
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
        loadstory();
    });
}
//=====================================================================================================
// approve ajax in story page crud admin
//======================================================================================================
var storyid = "";
function storyapprove() {
    $(".adminstoryapprovebtn").on("click", function () {
        storyid = this.id;
        console.log("call", storyid);
        $.ajax({
            type: "POST",
            url: "/Admin/ApproveAdmin_story",
            data: { storyId: storyid },
            success: function (data) {
                loadstory();
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
//=====================================================================================================
// delete ajax in story page crud admin
//======================================================================================================
var storyidfordelete = "";
function storydelete() {
    $(".admin-story-cancel-btn").on("click", function () {
        storyidfordelete = this.id;
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteAdmin_story",
            data: { storyId: storyidfordelete },
            success: function (data) {
                loadstory();
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

