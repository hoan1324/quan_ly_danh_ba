
$(function () {
   

   
    $("input[type=checkbox]").on("change", function () {
        if ($("input[type=checkbox]:checked").length > 0) {
            $(".delete-group").removeClass("disabled");
        } else {
            $(".delete-group").addClass("disabled");
        }
    });

    $(".delete-group").on('click', function () {
        var array = $("input[type=checkbox]:checked").map(function () {
            return this.value;
        }).get();

        // Chuyển đổi mảng thành chuỗi
        var arrayString = array.join(",");
        $('#confirmdeletebtn').attr('href', `/user/groupcontact/delete?GroupNames=${arrayString}`);
    });
})