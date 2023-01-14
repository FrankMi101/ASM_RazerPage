var dataTable;

$(document).ready(function () {
 

    var searchBy = getQuerystring("panel"); // window.location.search;
    var value = getQuerystring("value"); // window.location.search;

   // alert(para);
   // panel = panel.replace("?panel=", "");

    GetSearchStaffs(value, searchBy);
});

function getQuerystring(key) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == key) {
            return pair[1];
        }
    }
}

function GetbyStaffBySchool(schoolcode) {
    alert(schoolcode);
  //  GetSearchStaffs(schoolcode, "School");
}

function GetSearchStaffs(searchValue, searchBy) {

    alert(searchBy + "  " + searchValue);

    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/Staff/" + searchValue + "/" + searchBy,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "userID", "width": "5%" },
            { "data": "employeeID", "width": "5%" },
            { "data": "firstName", "width": "10%" },
            { "data": "lastName", "width": "10%" },
            { "data": "unitID", "width": "10%" },
            { "data": "position", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "status", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" >
                            <a href="/AppUser/Staffs/Edit?id=${data}"  class="btn btn-success text-white mx-2">
                            <i class="bi bi-pencil-square"></i>  </a>
                            <a onClick=Delete('/api/Staff/'+${data})  class="btn btn-danger text-white mx-2">
                             <i class="bi bi-trash-fill"></i>  </a>
                            </div>`
                },

                "width": "15%"
            }
        ],
        "width": "100%"
    });
}

function Delete(url)  
{
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //failsure notification
                        toastr.error(data.message);
                    }
                }
            })
        }
    })

}