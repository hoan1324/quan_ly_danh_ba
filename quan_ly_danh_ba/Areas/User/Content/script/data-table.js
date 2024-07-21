$(function () {
    $('.delete-item').on('click', function () {
        const itemid = $(this).data('id');
        $('#confirmdeletebtn').attr('href', `/user/contact/delete?id=${itemid}`); // cập nhật link với id
    });

    $(document).on('keydown', '.form-control', function (e) {
        if (e.key == 'Enter') {

            let dataJson = {
                FullName: $("#fullname").val(),
                PhoneNumber: $("#phonenumber").val(),
                groupContact: $("#groupcontact").val()
            }
            $.ajax({
                url: "/User/Contact/DataJson",
                type: "GET",
                dataType: 'json',
                data: dataJson,
                success: function (response) {
                    console.log(response);
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        }
    })

})