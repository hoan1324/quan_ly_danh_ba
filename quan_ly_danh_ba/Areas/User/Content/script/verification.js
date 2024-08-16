function getVerificationCode(dataElement) {
    $.ajax({
        url: "/User/VerificationPassword/DataVerificationJson",
        type: "Post",
        dataType: 'json',
        data: { id: $("#user-id").val() },
        success: function (response) {
        },
    });

}

    function setTimeBtn(timeElementVisible, time, timeOut) {
        let index = time;
        timeElementVisible.attr("disabled", true);

        let timeInterval = setInterval(() => {
            index--;
            timeElementVisible.text(index);

            if (index === timeOut) {
                clearInterval(timeInterval);
                timeElementVisible.text("Nhận mã");
                timeElementVisible.attr("disabled", false);
            }
        }, 1000);
    }

$(function () {
    $(".temp-alert").fadeOut(3000);
    setTimeBtn($(".btn-verification"), 60, 0);
    $("#verification-input").focus();
    $(".verification").on("click", function () {
        $("#verification-input").focus()
    })
    $("#verification-input").on("input", function () {
        $(".verification").children().each((index, element) => {
            element.textContent = $(this).val()[index];
        })
    })

    
    $(".btn-verification").on("click", function () {
        setTimeBtn($(this), 60, 0);
        getVerificationCode($(".verification"))
    })
});