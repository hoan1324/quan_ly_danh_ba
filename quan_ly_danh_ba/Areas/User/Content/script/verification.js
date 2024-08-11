function getVerificationCode(dataElement) {
    $.ajax({
        url: "/User/VerificationPassword/DataVerificationJson",
        type: "Post",
        dataType: 'json',
        data: { id: $("#verification-input").data("userID") },
        success: function (response) {
            dataElement.attr('data-verification', response);


        },
    });

}
function setTimeBtn(timeElementVisible,time,timeOut) {
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
}
$(function () {

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

    $("#form-verification").on(submit, function (e) {
        var dataVerification = $(".verification").data("verification");
        if ($("#verification-input").val() == dataVerification) {
            var id = $("#verification-input").data("userID");
            window.location = `user/VerificationPassword/ChangePass?id=${id}`;
        }
        alert("Mã xác minh không chính xác");
        return;

    });
    $(".btn-verification").on("click", function () {
        setTimeBtn($(this), 60, 0);
        getVerificationCode($(".verification"))
    })
});