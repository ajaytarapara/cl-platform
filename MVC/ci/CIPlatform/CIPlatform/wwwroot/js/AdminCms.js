//=====================================================================================================
//search ajax in cms page crud admin
//======================================================================================================
$(document).ready(function () {
    loadcms();
});
var searchcmstext = "";
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
        data: { searchText: searchcmstext },
        success: function (data) {
            var cmsdata = $("#cmstablelist");
            cmsdata.html("");
            cmsdata.html(data);
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