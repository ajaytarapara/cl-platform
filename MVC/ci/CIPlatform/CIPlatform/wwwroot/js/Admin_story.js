//=====================================================================================================
//search ajax in story page crud admin
//======================================================================================================
$(document).ready(function () {
    loadstory();
});
var searchstorytext = "";
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
        data: { searchText: searchstorytext },
        success: function (data) {
            var storydata = $("#storydatalist");
            storydata.html("");
            storydata.html(data);
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

