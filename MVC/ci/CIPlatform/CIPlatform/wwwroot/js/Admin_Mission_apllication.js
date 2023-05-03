//=====================================================================================================
//search ajax in mission application page crud admin
//======================================================================================================
$(document).ready(function () {
    loadmissionapplication();
});
var searchapplicationtext = "";
var pageSize = 3;
var pageNumber = 1;
$("#searchapplicationadmin").on("keyup", function (e) {
    e.preventDefault();
    searchapplicationtext = $("#searchapplicationadmin").val();
    console.log("VALUE:" + searchapplicationtext);
    if (searchapplicationtext.length > 2) {
        loadmissionapplication();
    }
    else {
        searchapplicationtext = "";
        loadmissionapplication();
    }
});

function loadmissionapplication() {
    searchapplicationtext = $("#searchapplicationadmin").val();
    $.ajax({
        type: "POST",
        url: "/Admin/Admin_mission_application",
        dataType: "html",
        data: { searchText: searchapplicationtext, pageNumber: pageNumber, pageSize: pageSize },
        success: function (data) {
            $("#applicationdatalist").html("");
            $("#applicationdatalist").html(data);
            loadPagination();
            applicationapprove();
            applicationdelete();
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
        loadmissionapplication();
    });
}
//=====================================================================================================
// approve ajax in MS APPLI page crud admin
//======================================================================================================
var Missionappid = "";
function applicationapprove() {
    $(".adminapplicationapprovebtn").on("click", function () {
        Missionappid = this.id;
        console.log("call", Missionappid);
        $.ajax({
            type: "POST",
            url: "/Admin/ApproveAdmin_mission_application",
            data: { missionAppId: Missionappid },
            success: function (data) {
                loadmissionapplication();
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
// delete ajax in MS APPLI page crud admin
//======================================================================================================
var msappfordelete = "";
function applicationdelete() {
    $(".adminapplicationdeletebtn").on("click", function () {
        msappfordelete = this.id;
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteAdmin_mission_application",
            data: { missionAppId: msappfordelete },
            success: function (data) {
                loadmissionapplication();
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