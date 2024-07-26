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
                        var fullName = elementData.FullName ? elementData.FullName.toLowerCase() : null;
                        var phoneNumber = elementData.PhoneNumber ? elementData.PhoneNumber.toLowerCase() : null;
                        var groupNames = elementData.GroupNames ? elementData.GroupNames.map(name => name.toLowerCase()) : [];

                        $("tr:not(.title-table)").each(function (index, elementTr) {
                            var trFullName = $(elementTr).find("td.full-name").text().trim().toLowerCase();
                            var trPhoneNumber = $(elementTr).find("td.phone-number").text().trim().toLowerCase();
                            var trGroupContact = $(elementTr).find("td.group-contact").text().trim().toLowerCase();

                            var matchFullName = fullName ? trFullName === fullName : true;
                            var matchPhoneNumber = phoneNumber ? trPhoneNumber === phoneNumber : true;
                            var matchGroupContact = groupNames.length > 0 ? groupNames.includes(trGroupContact) : true;

                            if (matchFullName && matchPhoneNumber && matchGroupContact) {
                                $(elementTr).addClass("search-done");
                            }
                        });
                    });
                    $("tr:not(.title-table,search-done)").remove();
                },
            });
        }
    })

})