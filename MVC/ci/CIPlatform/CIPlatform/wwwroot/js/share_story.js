$(document).ready(function () {
});

selectedmissionid = "";

var UserId = $('#userid').text();
console.log(selectedmissionid);
$("#savebtnsharestory").on("click", function (e) {
    e.preventDefault();
    $.each($("#storydropdown option:checked"), function () {
        selectedmissionid = $(this).val();
    });
    console.log("click",selectedmissionid);
    $.ajax({
        type: "Post",
        url: "/Story/Savestory",
        data: { userid: UserId, MissionId: selectedmissionid },
        success: function () {
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (xhr, error, res) {
            alert("Something went Worng");
        }

    });
}
);
