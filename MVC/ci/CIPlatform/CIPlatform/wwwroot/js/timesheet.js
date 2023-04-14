$(document).ready(function () {
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
                alert("your time type time sheet addded  successfully");
            }
            else
            {
                alert("data is not valid");
            }
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
            }
            else
            {
                alert("data is not valid");
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
var timesheetidse ="";
$(".editmodalopentime").on("click", function (e) {
    e.preventDefault();
    timesheetidse = this.id;
});
$("#edittimesheet").on("click", function (e) {
    e.preventDefault();
     Missionide = $("#missionidgetdiv-edit").val();
     datevole = $("#getdatediv-edit").val();
     houre = $("#gethourdiv-edit").val();
     minutese = $("#getminitediv-edit").val();
     msge = $("#getmsgdiv-edit").val();
   $.ajax({
        type: "POST",
        url: '/Account/edittimesheet',
        data: { timesheetid: timesheetidse, MissionId: Missionide, DateVolunteered: datevole, hours: houre, minutes: minutese, Notes: msge },
        success: function (data) {
           
            if (data["status"] == 1) {
                alert("your time sheet updated successfully");
            }
            if (data["status"] == 2) {
                alert("enter minutes less than 60" );
            }
            else
            {
                alert("data is not valid");
            }
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
       }
    })

});


var MissionIdseg = "";
var Actioneg = "";
var Noteseg = "";
var DateVolunteeredeg = "";
var timesheetidseg = "";
$(".editmodalopengoal").on("click", function (e) {
    e.preventDefault();
    timesheetidseg = this.id;
});
$("#editgoaltimesheetbtn").on("click", function (e) {
    e.preventDefault();
    MissionIdseg = $("#getmissionid-goaledit").val();
    Actioneg = $("#actiongoalgetdiv-edit").val();
    console.log(Actioneg);
    Noteseg = $("#msggoalgetdiv-edit").val();
    console.log(Noteseg);
    DateVolunteeredeg = $("#dategoalgetdiv-edit").val();
    $.ajax({
        type: "POST",
        url: '/Account/edittimesheetgoal',
        data: { timesheetid: timesheetidseg, MissionId: MissionIdseg, DateVolunteered: DateVolunteeredeg, Notes: Noteseg, Action: Actioneg },
        success: function (data) {
            if (data["status"] == 1) {
                alert("your time sheet updated successfully");
            }
            else
            {
                alert("data is not valid");
            }
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});


