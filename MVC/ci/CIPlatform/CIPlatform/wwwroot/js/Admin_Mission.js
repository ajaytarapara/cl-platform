////=====================================================================================================
//////search ajax in mission  page crud admin
//////======================================================================================================
$(document).ready(function () {
    loadmission();

});
var searchmissiontext = "";
var pageSize = 3;
var pageNumber = 1;
$("#search_Mission_adminbtn").on("keyup", function (e) {
    e.preventDefault();
    searchmissiontext = $("#search_Mission_adminbtn").val();
    console.log("VALUE:" + searchmissiontext);
    if (searchmissiontext.length > 2) {
        loadmission();
    }
    else {
        searchmissiontext = "";
        loadmission();
    }
});

function loadmission() {
    searchmissiontext = $("#search_Mission_adminbtn").val();
    $.ajax({
        type: "POST",
        url: "/Admin/Admin_mission",
        dataType: "html",
        data: { searchText: searchmissiontext, pageNumber: pageNumber, pageSize: pageSize },
        success: function (data) {
            $("#missiondatalist").html("");
            $("#missiondatalist").html(data);
            loadPagination();
            deleteMission();
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
        loadmission();
    });
}
//=====================================================================================================
//delete mission ajax in mission crud admin
//======================================================================================================
function deleteMission() {
    var missionid = "";
    $(".missiondeleteadminbtn").on("click", function () {
        missionid = this.id;
        console.log("call", missionid);
        $.ajax({
            type: "POST",
            url: "/Admin/Delete_Mission",
            data: { missionId: missionid },
            success: function (data) {
                loadmission();
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


