$(function () {
    $('.delete-item').on('click', function () {
        const itemid = $(this).data('id');
        $('#confirmdeletebtn').attr('href', `/user/contact/delete?id=${itemid}`); // cập nhật link với id
    });

    $(".temp-alert").fadeIn();
    var clearTime = setTimeout(function () {
        $('.temp-alert').fadeOut();
    }, 3000);
    clearTimeout(clearTime);

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
                    $(response).each(function (index, elementData) {
                        $("tr:not(.title-table)").each(function (index, elementTr) {
                            var fullName = $(elementTr).find("td.full-name").text().trim();
                            var phoneNumber = $(elementTr).find("td.phone-number").text().trim();
                            var groupContact = $(elementTr).find("td.group-contact").text().trim();

                            if ((elementData.FullName && fullName !== elementData.FullName) ||
                                (elementData.PhoneNumber && phoneNumber !== elementData.PhoneNumber) ||
                                (elementData.GroupNames && !elementData.GroupNames.includes(groupContact))) {
                                $(elementTr).remove();
                            }
                        });
                    });
                },
            });
        }
    })

})