$(document).ready(function () {
    getcountry();
    getcity();
    getskill();
    $("#avataruploadbtn").click(function (event) {
        event.preventDefault();
    });
});

function getcountry() {
    $.ajax({
        type: "GET",
        url: "/Account/GetCountries",
        data: "{}",
        success: function (data) {

            var str = "";
            //console.log(data);
            var releatedmission = $("#countrydropdown");
            for (var i = 0; i < data["data"].length; i++) {
                str += '<option value=' + data["data"][i].countryId
                    + '>' + data["data"][i].name + '</option>';
            }
            releatedmission.append(str);

        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}

function getcity() {
    $.ajax({
        type: "get",
        url: "/Account/GetCities",
        data: "{}",
        success: function (data) {
            var str = "";
            //console.log(data);
            var releatedmission = $("#citydropdown");
            for (i = 0; i < data["data"].length; i++) {

                str += '<option value=' + data["data"][i].cityId + '>' + data["data"][i].name + '</option>';
            }

            releatedmission.append(str);
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}


$("#countrydropdown").on("change", function () {
    var countryid = $("#countrydropdown").val();
    console.log(countryid);
    $.ajax({
        type: "Post",
        url: "/Account/GetCities",
        data: { countryid: countryid },
        success: function (data) {
            var str = "";
            //console.log(data);
            var releatedmission = $("#citydropdown");
            for (i = 0; i < data["data"].length; i++) {

                str += '<option value=' + data["data"][i].cityId + '>' + data["data"][i].name + '</option>';
            }
            $("#citydropdown").text("");
            releatedmission.append(str);
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
});





function getskill() {
    $.ajax({
        type: "GET",
        url: "/Account/GetSkills",
        data: "{}",
        success: function (data) {
            console.log(data);
            var str = "";
            //console.log(data);
            var releatedmission = $("#skilllistinmodal");
            for (i = 0; i < data["data"].length; i++) {
             
                str += '<li data-skill="' + data["data"][i].skillName + '" value="' + data["data"][i].skillId + '">' + data["data"][i].skillName + '</li>';
               
            }

            releatedmission.html(str);
            $(".popup-container .skill-list li").on("click", function () {
                console.log("clicked");
                $(this).toggleClass("selected");
            });
        },
        failure: function (response) {
            alert("failure");
        },
        error: function (response) {
            alert("Something went Worng");
        }

    });
}

$("#uimg").on("click", function (e) {
    $('.avatarbtn').trigger('click');
});

$(document).ready(function () {
    $('.avatarbtn').on("change", function () {
        var file = this.files[0];
        var reader = new FileReader();
        reader.onload = function (event) {
            $('#uimg').attr('src', event.target.result);
        };
        reader.readAsDataURL(file);
    });    
});



$(document).ready(function () {
    $(".popup-container").hide();
    $(".user-edit-add-skill-btn").on("click", function (event) {
        event.preventDefault();
        $(".popup-container").show();
    });

    $(".user-edit-close-btn").on("click", function () {
        $(".popup-container").hide();
    });

    $(".closebtnpopup").on("click", function () {
        $(".popup-container").hide();
    });


    $(".close-popup-btn").click(function () {
        $(".popup-container").fadeOut();
    });

    $(".add-skill").on("click", function () {
        var selectedSkill = $(".available-skills .skill-list li.selected");
        if (selectedSkill.length > 0) {
            selectedSkill.appendTo($(".selected-skills .skill-list"));
            selectedSkill.removeClass("selected");
        }
    });

    $(".remove-skill").on("click", function () {
        var selectedSkill = $(".selected-skills .skill-list li.selected");
        if (selectedSkill.length > 0) {
            selectedSkill.appendTo($(".available-skills .skill-list"));
            selectedSkill.removeClass("selected");
        }
    });

   
    var names = "";
    $(".user-edit-save-btn").on("click", function () {
        $(".user-edit-selected-skill").empty();
        $(".selected-skills .skill-list li").each(function () {
            var skillName = $(this).text().trim();
            names += skillName+"\n";
            /* $("<p>").text(skillName).appendTo($("#skilltext"));*/
            $("#skilltext").text(names);
        });
        $(".popup-container").hide();
    });
  });