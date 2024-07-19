$.validator.addMethod("requireOneOrInput", function (value, element, params) {
    var checkboxes = $(params.checkboxes);
    var input = $(params.input);
    // Kiểm tra nếu có ít nhất một checkbox được chọn hoặc input có giá trị khác rỗng
    return checkboxes.filter(":checked").length > 0 || input.val().trim() !== "";
}, "Please select at least one option or fill the input.");
jQuery('#form-edit').validate({
    rules: {
        FullName: "required",
        PhoneNumber: {
            required: true,
            digits: true,
            minlength: 10
        },
        GroupName: {
            requireOneOrInput: {
                checkboxes: 'input[name="GroupName"]',
                input: 'input[name="newGroupName"]'
            }
        },
        newGroupName: {
            requireOneOrInput: {
                checkboxes: 'input[name="GroupName"]',
                input: 'input[name="newGroupName"]'
            }
        }
    },
    messages: {
        FullName: "Vui lòng nhập tên",
        PhoneNumber: {
            required: "Vui lòng nhập số điện thoại",
            digits: "Vui lòng nhập định dạng là số",
            minlength: "Số tối thiểu có 10 chữ số"
        }
    },
    submitHandler: function (form) {
        form.submit();
    }
});
$(function () {
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
