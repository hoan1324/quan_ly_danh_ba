$('.delete-link').on('click', function () {
    const itemid = $(this).data('id');
    $('#confirmdeletebtn').attr('href', `/user/contact/delete?id=${itemid}`); // cập nhật link với id
});

