$(document).ready(function () {
    console.log("call");
    loadusers();
});
var UserId = "";
var Firstname = "";
var LastName = "";
var Password = "";
var Department = "";
var EmployeeId = "";
var PhoneNumber = "";
var Email = "";
//=====================================================================================================
//search ajax in user crud admin
//======================================================================================================
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
//=====================================================================================================
//load user ajax in user crud admin
//======================================================================================================
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
            editData();
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
//delete user ajax in user crud admin
//======================================================================================================
function deleteUser() {
    var userid = "";
    $(".deletebtnuser").on("click", function () {
        userid = this.value;
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
//=====================================================================================================
//edit user ajax in user crud admin
//======================================================================================================
function editData() {

    $(".Editbtnusermodalopen").on("click", function () {
        UserId = this.id;

        console.log(UserId);
    });

    $(".editbtnuser").on("click", function () {
        Firstname = $("#firstnameedit").val();
        console.log(Firstname);
        LastName = $("#lastnamenameedit").val();
        console.log(LastName);
        Password = $("#passwordedit").val();
        Department = $("#departmentedit").val();
        console.log(Department);
        EmployeeId = $("#employeeidedit").val();
        console.log(EmployeeId);
        PhoneNumber = $("#phonenumberedit").val();
        console.log(PhoneNumber);
        Email = $("#emailedit").val();
        $.ajax({
            type: "POST",
            url: "/Admin/editUser_crud",
            data: { UserId: UserId, Firstname: Firstname, LastName: LastName, Password: Password, Department: Department, EmployeeId: EmployeeId, PhoneNumber: PhoneNumber, Email: Email },
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
