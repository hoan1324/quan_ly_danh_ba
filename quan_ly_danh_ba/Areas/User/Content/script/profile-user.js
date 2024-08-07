$(function () {
    $('.closeButton').on('click', function () {
        $(".visible-img").addClass('d-none');
        $('#avatar').val("").show();
    })
    $('#avatar').on('change', function (event) {
        const file = event.target.files[0];
        if (file) {

            const reader = new FileReader();
            reader.onload = function (e) {
                const base64String = e.target.result;
                console.log(base64String);

                $('#image').attr('src', base64String);
                $(".visible-img").removeClass('d-none');
                $('.closeButton').removeClass('d-none');
                $('#avatar').hide();
            }
            reader.readAsDataURL(file);
        }
    });
});