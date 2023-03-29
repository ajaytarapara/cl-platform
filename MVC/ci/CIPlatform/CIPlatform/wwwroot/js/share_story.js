$(document).ready(function () {
});

selectedmissionid = "";
Description = "";
PublishedAt = "";
Title = "";
console.log(selectedmissionid);
$("#submitbtnsharestory").on("click", function (e) {
    e.preventDefault();
    $.each($("#storydropdown option:checked"), function () {
        selectedmissionid = $(this).val();
         UserId = $('#userid').text();
         Title = $('#title').val();
         PublishedAt = $('#PublishedAt').val();
        Description = tinyMCE.activeEditor.getContent();
    });
    console.log("click",selectedmissionid);
    $.ajax({
        type: "Post",
        url: "/Story/Savestory",
        data: { userid: UserId, MissionId: selectedmissionid, Title: Title, PublishedAt: PublishedAt, Description: Description, StoryMedia: storyFileNames },
        success: function () {
            alert("story added succesfully");
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


$("#savebtnsharestory").on("click", function (e) {
    e.preventDefault();
    $.each($("#storydropdown option:checked"), function () {
        selectedmissionid = $(this).val();
        UserId = $('#userid').text();
        Title = $('#title').val();
        PublishedAt = $('#PublishedAt').val();
        Description = tinyMCE.activeEditor.getContent();
    });
    console.log("click", selectedmissionid);
    $.ajax({
        type: "Post",
        url: "/Story/Savestory",
        data: { userid: UserId, MissionId: selectedmissionid, Title: Title, PublishedAt: PublishedAt, Description: Description, StoryMedia: storyFileNames },
        success: function () {
            alert("story updated succesfully");
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


/*upload images*/
var storyFileNames = "";
$(function () {
    $("#dropSection").filedrop({
        fallback_id: 'btnUpload',
        fallback_dropzoneClick: true,

        url:'/Story/Upload',

        //allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif', 'application/pdf', 'application/doc'],
        allowedfileextensions: ['.doc', '.docx', '.pdf', '.jpg', '.jpeg', '.png', '.gif'],
        paramname: 'postedFiles',
        maxfiles: 5, //Maximum Number of Files allowed at a time.
        maxfilesize: 4, //Maximum File Size in MB.
        dragOver: function () {
            $('#dropSection').addClass('active');
        },
        dragLeave: function () {
            $('#dropSection').removeClass('active');
        },
        drop: function () {
            $('#dropSection').removeClass('active');
        },
        uploadFinished: function (i, file, response, time) {
            storyFileNames = storyFileNames.concat(file.name + ",");
            $('#uploadedFiles').append('<img src="/images/uploads/' + file.name + '" class="px-2" style="height:100px;width:100px;" />');
        },
        afterAll: function (e) {
            //To do some task after all uploads done.
        }
    })
})


