$(document).ready(function () {
    getcountry();
    getcity();
    getskill();
   
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
        type: "GET",
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

   

    $(".user-edit-save-btn").on("click", function () {
        $(".user-edit-selected-skill").empty();
        $(".selected-skills .skill-list li").each(function () {
            var skillName = $(this).text().trim();
            $("<li>").text(skillName).appendTo($(".user-edit-selected-skill"));
        });
        $(".popup-container").hide();
    });
  });


        $(document).ready(function() {
            $('#user-avatar').click(function () {
                var input = document.createElement('input');
                input.type = 'file';

                input.onchange = function () {
                    var file = this.files[0];
                    var reader = new FileReader();

                    reader.onload = function (event) {
                        $('#user-avatar').attr('src', event.target.result);
                    };

                    reader.readAsDataURL(file);
                };
                input.click();
            });
    });
