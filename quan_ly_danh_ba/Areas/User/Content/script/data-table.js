﻿$(function () {
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
                    $(response).each(function (index, elementData) {
                        $("tr:not(.title-table)").each(function (index, elementTr) {
                            var $td = $(elementTr).children();
                            var fullName = $td.find(".full-name").text();
                            var phoneNumber = $td.find(".phone-number").text();
                            var groupContact = $td.find(".group-contact").text();

                            if (fullName !== elementData.FullName &&
                                phoneNumber !== elementData.PhoneNumber &&
                                groupContact.indexOf(elementData.groupContact[0].GroupName) === -1) {
                                $td.remove();
                            }
                        });
                    });
                },
            });
        }
    })

})