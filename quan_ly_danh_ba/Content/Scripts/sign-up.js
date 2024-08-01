jQuery('#sign-up-form').validate({
    rules: {
        Email: {
            required: true,
            email: true
        },
        UserName: {
            required: true,
            minlength: 4,
            maxlength: 10
        },
        BirthDay: {
            required: true,
        },
        Password: {
            required: true,
            minlength: 8
        },
        ConfirmPassword: {
            required: true,
            equalTo: "#password"
        }
    },
    messages: {
        Email: {
            required: "Vui lòng nhập Email",
            email:"Tài khoản không đúng định dạng"
        },
        UserName: {
            required: "Vui lòng nhập tên tài khoản",
            minlength: "Tên tài khoản tối thiểu 4 ký tự",
            maxlength:"Tên tài khoản tối đa 10 ký tự "
        },
        BirthDay: {
            required: "Vui lòng nhập ngày sinh",
        },
        Password: {
            required: "Vui lòng nhập mật khẩu",
            minlength:"Mật khẩu chứa ít nhất 8 ký tự"
        }, 
         ConfirmPassword: {
            required: "Vui lòng nhập xác thực mật khẩu",
            equalTo: "Mật khẩu xác nhận không đồng nhất"
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