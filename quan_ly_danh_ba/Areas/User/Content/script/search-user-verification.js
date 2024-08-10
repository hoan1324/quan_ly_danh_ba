jQuery('#search-form').validate({
    rules: {
      
        PhoneNumber: {
            required: true,
            digits: true,
            minlength: 10
        },
        Email: {
            required: true,
            email: true
        }
    },
    messages: {
       
        PhoneNumber: {
            required: "Vui lòng nhập số điện thoại",
            digits: "Vui lòng nhập định dạng là số",
            minlength: "Số tối thiểu có 10 chữ số"
        },
        Email: {
            email: "Vui lòng nhập đúng định dạng email",
            required:"Vui lòng nhập Email"
        }
    },
    submitHandler: function (form) {
        form.submit();
    }
});


$(function () {
    $(".temp-alert").fadeOut(3000);


    const exampleModal = document.getElementById('exampleModal')
    if (exampleModal) {
        exampleModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget
            const recipient = button.getAttribute('data-bs-whatever')

            const typeSearch = exampleModal.querySelector('#type')
            const labelInput = exampleModal.querySelector('#exampleModal .modal-body label')
            const bodyInput = exampleModal.querySelector('#inputUser')
            bodyInput.setAttribute('name', recipient);

            if (recipient == "Email") {
                labelInput.textContent = "Vui lòng nhập Email"
                typeSearch.value = "email"
            }
            else {
                labelInput.textContent = "Vui lòng nhập số điện thoại"
                typeSearch.value = "phone"

            }


        })
    }
});