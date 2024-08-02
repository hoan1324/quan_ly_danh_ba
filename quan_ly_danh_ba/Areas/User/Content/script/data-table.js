function printGroup(groupData, maxGroupsToShow) {
    let output = ``;
    groupData.forEach((element, index) => {
        if (index >= maxGroupsToShow) {
            output += `...`;
            return false; // Exit the loop
        }
        if (index > 0) {
            output += `, `; // Add comma separator for groups
        }
        output += `${element}`;
    });
    return output;
}

$(function () {
   

   
        $('.temp-alert').fadeOut(3000);
  
    $(document).on('click', '.delete-item', function () {
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
                    $("tr:not(.title-table").remove();
                    let j = 0;
                    $(response).each(function (index, elementData) {
                        var tr = `
                       <tr>
     <td class="text-start fs-6 p-2 border text-center">${++j}</td>
     <td class="text-start fs-6 p-2 border full-name">${elementData.FullName}</td>
     <td class="text-start fs-6 p-2 border phone-number">${elementData.PhoneNumber}</td>
     <td class="text-start fs-6 p-2 border">${elementData.Address ? elementData.Address : "Không có dữ liệu"}</td>
     <td class="text-start fs-6 p-2 border">${elementData.Email ? elementData.Email : "Không có dữ liệu"}</td>
     <td class="text-start fs-6 p-2 border group-contact">${printGroup(elementData.GroupNames, 3)}</td>
      <td class=" border function-data">
                        <div class="d-flex justify-content-center align-items-center flex-wrap">
                            <span class=" mx-1 rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Chi tiết"><a class="link-info" href="/user/contact/Detail?id=${elementData.ContactID}"><i class="fa-solid fa-circle-info"></i></a></span>
                            <span class=" mx-1 rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Sửa "><a class="link-warning" href="/User/Contact/Edit?id=${elementData.ContactID}"><i class="fa-solid fa-wrench"></i></a></span>
                            <span class=" mx-1 rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Xóa"><a data-bs-toggle="modal" data-bs-target="#exampleModal" class="link-danger delete-item" data-id="${elementData.ContactID}"><i class="fa-solid fa-circle-xmark"></i></a></span>
                        </div>
                    </td>
                        </tr>`

                        if (index == 0) {
                            $("tr.title-table").after(tr)
                        }
                        else {
                            $("tr:last").after(tr);
                        }
                        

                    });

                },
            });
        }
    })

})