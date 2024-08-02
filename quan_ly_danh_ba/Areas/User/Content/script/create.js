$.validator.addMethod("requireOneOrInput", function (value, element, params) {
    var checkboxes = $(params.checkboxes);
    var input = $(params.input);
    // Kiểm tra nếu có ít nhất một checkbox được chọn hoặc input có giá trị khác rỗng
    return checkboxes.filter(":checked").length > 0 || input.val().trim() !== "";
}, "Vui lòng chọn ít nhất 1 lựa chọn ở mục quan hệ hoặc thêm mới nó");
jQuery('#form-create').validate({
    rules: {
        FullName: "required",
        PhoneNumber: {
            required: true,
            digits: true,
            minlength: 10
        },
        GroupNames: {
            requireOneOrInput: {
                checkboxes: 'input[name="GroupNames"]',
                input: 'input[name="newGroupName"]'
            }
        },
        newGroupName: {
            requireOneOrInput: {
                checkboxes: 'input[name="GroupNames"]',
                input: 'input[name="newGroupName"]'
            }
        },
        Email: {
            email: true
        }
    },
    messages: {
        FullName: "Vui lòng nhập tên",
        PhoneNumber: {
            required: "Vui lòng nhập số điện thoại",
            digits: "Vui lòng nhập định dạng là số",
            minlength: "Số tối thiểu có 10 chữ số"
        },
        Email: {
            email: "Vui lòng nhập đúng định dạng email"
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
    $('.temp-alert').fadeOut(3000);
    $(".btn").on("click", function (e) {
        e.preventDefault(); // Ngăn chặn hành vi submit form mặc định

        var fileInput = $("input[type=file]")[0];
        var file = fileInput.files[0];

        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var base64String = e.target.result.split(',')[1]; // Lấy chuỗi Base64 từ kết quả đọc file
                console.log(base64String); // In ra chuỗi Base64 để kiểm tra

                // Bạn có thể gửi chuỗi Base64 này đến server hoặc xử lý tiếp
                // $.post('your-server-url', { avatarBase64: base64String }, function(response) {
                //     // Xử lý phản hồi từ server
                // });
            };
            reader.readAsDataURL(file); // Đọc file dưới dạng Data URL
        }
    });
    
    $(".hide-default").hide();
    $("#diffirent-checkbox").on("click", function () {
        if ($(this).prop("checked")) {
            $(".show-default").hide(200);
            $(".hide-default").show(200);
        } else {
            $(".hide-default").hide(200);
            $(".show-default").show(200);
        }
    });
});
