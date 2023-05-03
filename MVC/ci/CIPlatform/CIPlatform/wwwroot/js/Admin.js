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
var pageSize = 3;
var pageNumber = 1;
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
        dataType: "html",
        data: { searchtext: searchusertext, pageNumber: pageNumber, pageSize: pageSize },
        success: function (data) {
            $("#usertablelist").html("");
            $("#usertablelist").html(data);
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
        loadusers();
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

//function editData() {
//    $(".Editbtnusermodalopen").on("click", function () {
//        UserId = this.id;
//        console.log(UserId);
//        $.ajax({
//            type: "get",
//            url: '/Admin/editUser_crud',
//            dataType: "html",
//            data: { UserId: UserId },
//            success: function (data) {
//                $("#editusermodal").html();
//                $("#editusermodal").html(data);
//            },
//            error: function (xhr, status, error) {
//                // Handle error
//                console.log(error);
//            }
//        })
//    });
//}
//}
//    $(".editbtnuser").on("click", function () {
//        Firstname = $("#firstnameedit").val();
//        console.log(Firstname);
//        LastName = $("#lastnamenameedit").val();
//        console.log(LastName);
//        Password = $("#passwordedit").val();
//        Department = $("#departmentedit").val();
//        console.log(Department);
//        EmployeeId = $("#employeeidedit").val();
//        console.log(EmployeeId);
//        PhoneNumber = $("#phonenumberedit").val();
//        console.log(PhoneNumber);
//        Email = $("#emailedit").val();
//        $.ajax({
//            type: "POST",
//            url: "/Admin/editUser_crud",
//            data: { UserId: UserId, Firstname: Firstname, LastName: LastName, Password: Password, Department: Department, EmployeeId: EmployeeId, PhoneNumber: PhoneNumber, Email: Email },
//            success: function (data) {
//                loadusers();
//            },
//            failure: function (response) {
//                alert("failure");
//            },
//            error: function (response) {
//                alert("Something went Worng");
//            }

//        });

//    });
//}