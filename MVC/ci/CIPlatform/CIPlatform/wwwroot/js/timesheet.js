$(document).ready(function () {
});

var MissionId = $("#missionidgetdiv").val();
var DateVolunteered = $("#getdatediv").val();
var Notes = $("#missionidgetdiv").val();
var hours = $("#gethourdiv").val();
var minutes = $("#getminitediv").val();

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
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    })

});
