$(document).ready(function () {
    editModalTime();
});

var MissionId = $("#missionidgetdiv").val();
var DateVolunteered = $("#getdatediv").val();
var Notes = $("#missionidgetdiv").val();
var hours = $("#gethourdiv").val();
var minutes = $("#getminitediv").val();
//=========================================================================================
//add timesheet for time type mission
//=========================================================================================
$("#addtimesheet").on("click", function () {
     MissionId = $("#missionidgetdiv").val();
     DateVolunteered = $("#getdatediv").val();
    Notes = $("#getmsgdiv").val();
    hours = $("#gethourdiv").val();
    minutes = $("#getminitediv").val();
    $.ajax({
        type: "POST",
        url: '/Account/addtimesheet',
        data: { MissionId: MissionId, DateVolunteered: DateVolunteered, Notes: Notes, hours: hours, minutes: minutes },

        success: function (data) {
            if (data["status"] == 1) {
                location.reload();
            }
            //else if (data["status"] == 0)
            //{
            //    alert("time is required");
            //}
            //else if (data["status"] == 2) {
            //    alert("date volunteer is not valid");
            //}
            //else if (data["status"] == 3) {
            //    alert("you have not applied at any mission");
            //}
            /*location.reload();*/
         
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});



//=========================================================================================
//add timesheet for goal type mission
//=========================================================================================

var MissionIds = $("#missionidgetdivgoal").val();
var Action = $("#actiongoalgetdiv").val();
var Notes = $("#msggoalgetdiv").val();
var DateVolunteered = $("#dategoalgetdiv").val();
$("#addgoaltimesheetbtn").on("click", function () {
    MissionId = $("#missionidgetdivgoal").val();
    DateVolunteered = $("#dategoalgetdiv").val();
    Notes = $("#msggoalgetdiv").val();
    Action = $("#actiongoalgetdiv").val();
    $.ajax({
        type: "POST",
        url: '/Account/addtimesheetgoal',
        data: { MissionId: MissionIds, DateVolunteered:DateVolunteered, Notes:Notes, Action: Action },
        success: function (data) {
            if (data["status"] == 1)
            {
                alert("your goal time sheet addded  successfully");
                location.reload();
            }
            else if (data["status"] == 0)
            {
                alert("data is not valid");
            }
            //else if (data["status"] == 2) {
            //    alert("date volunteer is not valid");
            //}
            else if (data["status"] == 3) {
                alert("youR DETAIL IS NOT VALID");
            }
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});

//==========================================================
//delete time sheet
//=========================================================

var timesheetid = $(".timesheetidget").val();
$(".timesheetdeletebtn").on("click", function () {
    timesheetid =this.id;
    $.ajax({
        type: "POST",
        url: '/Account/deletetimesheet',
        data: {timesheetid: timesheetid},
        success: function (data) {
            alert("your time sheet deleted successfully");
            location.reload();
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});


var timesheetid = $(".timesheetidgetgoal").val();
$(".timesheetdeletebtngoal").on("click", function () {
    timesheetid = this.id;
    $.ajax({
        type: "POST",
        url: '/Account/deletetimesheetgoal',
        data: { timesheetid: timesheetid },
        success: function (data) {
            alert("your time sheet deleted successfully");
            location.reload();
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});


////==========================================================
////edit time sheeet timebased
////=========================================================


var Missionide = $("#missionidgetdiv-edit").val();
var datevole = $("#getdatediv-edit").val();
var houre = $("#gethourdiv-edit").val();
var minutese = $("#getminitediv-edit").val();
var msge = $("#getmsgdiv-edit").val();
var timesheetidse = "";
var flags = 1;
function editModalTime() {

$(".editmodalopentime").click( function (e) {
    e.preventDefault();
    timesheetidse = this.id;
    $.ajax({
        type: "Get",
        url: '/Account/edittimesheet',
        data: { timesheetid: timesheetidse },
        success: function (data) {
            console.log(data);
            $("#missionidgetdiv-edit").val(data["data"].missionId);
            console.log(data["data"].dateVolunteered.slice(0, 10));
            $("#getdatediv-edit").val(data["data"].dateVolunteered.slice(0,10));
            $("#gethourdiv-edit").val(data["data"].hours);
            $("#getminitediv-edit").val(data["data"].minutes);
            $("#getmsgdiv-edit").val(data["data"].notes);
            $("#missiontitletimesheetedit").val(data["data"].missionId);
            $("#timesheetidtimebased").val(data["data"].timesheetid);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })
   
});
}



var MissionIdseg = "";
var Actioneg = "";
var Noteseg = "";
var DateVolunteeredeg = "";
var timesheetidseg = "";
$(".editmodalopengoal").on("click", function (e) {
    e.preventDefault();
    timesheetidseg = this.id;
    $.ajax({
        type: "Get",
        url: '/Account/edittimesheetgoal',
        data: { timesheetid: timesheetidseg },
        success: function (data) {
            console.log(data);
            $("#getmissionid-goaledit").val(data["data"].missionId);
            $("#actiongoalgetdiv-edit").val(data["data"].action);
            $("#msggoalgetdiv-edit").val(data["data"].notes);
            $("#dategoalgetdiv-edit").val(data["data"].dateVolunteered.slice(0, 10));
            $("#getmissionid-goaledit").val(data["data"].missionId);
            $("#timesheetidgoalbased").val(data["data"].timesheetid);

        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })
});
//$("#editgoaltimesheetbtn").on("click", function (e) {
//    e.preventDefault();
//    MissionIdseg = $("#getmissionid-goaledit").val();
//    Actioneg = $("#actiongoalgetdiv-edit").val();
//    console.log(Actioneg);
//    Noteseg = $("#msggoalgetdiv-edit").val();
//    console.log(Noteseg);
//    DateVolunteeredeg = $("#dategoalgetdiv-edit").val();
//    $.ajax({
//        type: "POST",
//        url: '/Account/edittimesheetgoal',
//        data: { timesheetid: timesheetidseg, MissionId: MissionIdseg, DateVolunteered: DateVolunteeredeg, Notes: Noteseg, Action: Actioneg },
//        success: function (data) {
//            if (data["status"] == 1) {
//                alert("your time sheet updated successfully");
//            }
//            else
//            {
//                alert("data is not valid");
//            }
//        },
//        error: function (xhr, status, error) {
//            // Handle error
//            console.log(error);
//        }
//    })

//});


