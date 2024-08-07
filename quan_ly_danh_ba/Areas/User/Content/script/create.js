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
    $('#avatar').on('change', function (event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const base64String = e.target.result.split(',')[1]; // Lấy chuỗi Base64
                $('#output').text(base64String);
                console.log(isBase64(base64String))
                console.log(base64String); // In ra console
            };
            reader.readAsDataURL(file); // Đọc file dưới dạng DataURL
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
    $('#form-create').on('submit', function (e) {
        var fileInput = $('#avatar').files[0];
        var maxSize = 10 * 1024 * 1024; // 10 MB

        if (fileInput && fileInput.size > maxSize) {
            alert('Tệp quá lớn. Kích thước tối đa là 10MB.');
            e.preventDefault(); // Ngăn chặn việc gửi form
        }
    });
  
});
