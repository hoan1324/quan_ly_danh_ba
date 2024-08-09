$(function () {
    const exampleModal = document.getElementById('exampleModalToggle2')
    if (exampleModal) {
        exampleModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget
            const recipient = button.getAttribute('data-bs-whatever')
           
            const typeSearch = exampleModal.querySelector('#type')
            const labelInput = exampleModal.querySelector('.modal-body label')
            const bodyInput = exampleModal.querySelector('#inputUser')
            typeSearch.value = recipient;
            if (recipient == "email") {
                labelInput.textContent="Tìm kiếm tài khoản bằng email"
                bodyInput.setAttribute('name', "Email");
            }
            else {
                labelInput.textContent = "Tìm kiếm tài khoản bằng số điện thoại"
                bodyInput.setAttribute('name', "PhoneNumber");
            }
        })
    }
});