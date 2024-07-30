
$(function () {
    $(".temp-alert").fadeIn();

    setTimeout(function () {
        $('.temp-alert').fadeOut();
    }, 3000);
    $("input[type=checkbox]").on("change", function () {
        if ($("input[type=checkbox]:checked").length > 0) {
            $(".delete-group").removeClass("disabled");
        } else {
            $(".delete-group").addClass("disabled");
        }
    });

    $(".delete-group").on('click', function () {
        var array = Array.from($("input[type=checkbox]:checked", checkbox => checkbox.value));
        $('#confirmdeletebtn').attr('href', `/user/groupcontact/delete?GroupNames=${array}`);
    });


    



})