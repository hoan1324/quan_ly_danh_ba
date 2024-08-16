jQuery('#sign-up-form').validate({
    rules: {

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

        Password: {
            required: "Vui lòng nhập mật khẩu",
            minlength: "Mật khẩu chứa ít nhất 8 ký tự"
        },
        ConfirmPassword: {
            required: "Vui lòng nhập xác thực mật khẩu",
            equalTo: "Mật khẩu xác nhận không đồng nhất"
        }
    },

    submitHandler: function (form) {
        form.submit();
    }
});
$(function () {


    const exampleModal = document.getElementById('exampleModal')
    if (exampleModal) {
        exampleModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget
            const recipient = button.getAttribute('data-bs-whatever')
            const typeSearch = exampleModal.querySelector('#type')
            if (typeSearch.value) {
                typeSearch.value = typeSearch.value + "," + recipient;
            }




        })
    }
});