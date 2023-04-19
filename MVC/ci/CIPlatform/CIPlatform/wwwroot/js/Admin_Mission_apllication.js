//=====================================================================================================
//search ajax in mission application page crud admin
//======================================================================================================
$(document).ready(function () {
    loadmissionapplication();
});
var searchapplicationtext = "";
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
        data: { searchText: searchapplicationtext },
        success: function (data) {
            var applicationdata = $("#applicationdatalist");
            applicationdata.html("");
            applicationdata.html(data);
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