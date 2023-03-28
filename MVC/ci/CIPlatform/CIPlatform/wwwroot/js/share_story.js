$(document).ready(function () {
});

selectedmissionid = "";
Description = "";
PublishedAt = "";
Title = "";
console.log(selectedmissionid);
$("#savebtnsharestory").on("click", function (e) {
    e.preventDefault();
    $.each($("#storydropdown option:checked"), function () {
        selectedmissionid = $(this).val();
         UserId = $('#userid').text();
         Title = $('#title').val();
         PublishedAt = $('#PublishedAt').val();
        Description = $('#myTextarea').val();
    });
    console.log("click",selectedmissionid);
    $.ajax({
        type: "Post",
        url: "/Story/Savestory",
        data: { userid: UserId, MissionId: selectedmissionid, Title: Title, PublishedAt: PublishedAt, Description: Description },
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
