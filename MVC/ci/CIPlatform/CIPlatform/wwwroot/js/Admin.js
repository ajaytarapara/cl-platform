$(document).ready(function () {
    console.log("call");
    loadusers();
});

var searchusertext = "";
$("#admin-user-search-bar").on("keyup", function () {
    searchusertext = $("#admin-user-search-bar").val();
    console.log("VALUE:" + searchusertext);
    if (searchusertext.length > 2) {
        loadusers();
    }
    else {
        searchusertext = "";
        loadusers();
    }
});

function loadusers() {
    searchusertext = $("#admin-user-search-bar").val();
    $.ajax({
        type: "POST",
        url: "/Admin/User_crud",
        data: { searchtext: searchusertext },
        success: function (data) {
            var html = "";
            var griddata = $("#usertablelist");

            griddata.html("");
            griddata.html(data);
            deleteUser();
            EditUser();
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}

function deleteUser() {
    console.log($(".deletebtnuser"));

var userid = "";
$(".deletebtnuser").on("click",function() {
    userid = this.value;
    console.log("call", userid);
    $.ajax({
        type: "POST",
        url: "/Admin/DeleteUser_crud",
        data: { userid: userid },
        success: function (data) {
            loadusers();
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
var useridedit = "";
function EditUser() {
    $(".editbtnuser").on("click", function () {
        useridedit = $(".Editbtnuser").value;
        console.log("call", useridedit);
        $.ajax({
            type: "POST",
            url: "/Admin/editUser_crud",
            data: { userid: useridedit },
            success: function (data) {
                loadusers();
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