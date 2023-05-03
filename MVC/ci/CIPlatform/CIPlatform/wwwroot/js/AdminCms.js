//=====================================================================================================
//search ajax in cms page crud admin
//======================================================================================================
$(document).ready(function () {
    loadcms();
});
var searchcmstext = "";
var pageSize = 3;
var pageNumber = 1;
$("#admin-cms-search-bar").on("keyup", function (e) {
    e.preventDefault();
    searchcmstext = $("#admin-cms-search-bar").val();
    console.log("VALUE:" + searchcmstext);
    if (searchcmstext.length > 2) {
        loadcms();
        
    }
    else {
        searchcmstext = "";
        loadcms();
       
    }
});

function loadcms() {
    searchcmstext = $("#admin-cms-search-bar").val();
    $.ajax({
        type: "POST",
        url: "/Admin/Cms_crud",
        dataType: "html",
        data: { searchText: searchcmstext, pageNumber: pageNumber, pageSize: pageSize },
        success: function (data) {
            $("#cmstablelist").html("");
            $("#cmstablelist").html(data);
            loadPagination();
            deleteUser();
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
        loadcms() ;
    });
}
//=====================================================================================================
//delete ajax in cms page crud admin
//======================================================================================================
function deleteUser() {
    var cmspageid = "";
    $(".deletecms_admin").on("click", function () {
        cmspageid = this.value;
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteCms_Admin",
            data: { cmsId: cmspageid },
            success: function (data) {
                loadcms();
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