jQuery('#sign-in-form').validate({
    rules: {
        Email: {
            required: true,
            email: true
        },
        Password: {
            required: true,
            minlength: 8
        },
    },
    messages: {
        Email: {
            required: "Vui lòng nhập Email",
            email:"Tài khoản không đúng định dạng"
        },
        Password: {
            required: "Vui lòng nhập mật khẩu",
            minlength:"Mật khẩu chứa ít nhất 8 ký tự"
        }
    },
    errorPlacement: function (error, element) {
        if (element.attr("name") == "GroupNames" || element.attr("name") == "newGroupName") {
            error.insertAfter(".visible-error"); // Chèn thông báo lỗi ngay sau nhãn của "Tạo quan hệ mới"
        } else {
            error.insertAfter(element); // Chèn thông báo lỗi sau phần tử input khác
        }
    },
    submitHandler: function (form) {
        form.submit();
    }
});

$(function () {
    $(".visible-pass").on("click", function () {
        var input = $(this).siblings('.password');
        if (input.attr('type') === 'password') {
            $(this).addClass("fa-eye").removeClass("fa-eye-slash")
            input.attr('type', 'text');
        } else {
            $(this).removeClass("fa-eye").addClass("fa-eye-slash")
            input.attr('type', 'password');
        }
    });
});