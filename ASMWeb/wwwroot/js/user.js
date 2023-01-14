var dataTable;

$(document).ready(function () {
 
    loadList()
});
 

function loadList() {


    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/user/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "firstName", "width": "20%" },
            { "data": "lastName", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "phoneNumber", "width": "20%" },
           
            {
                "data": {id:"id",lockoutEnd:"lockoutEnd"},
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        // currently user  is locked
                        return `<div class="text-center" >
                            <a class="btn btn-danger text-white mx-2" style="cursor:pointer; width:100px" onclick=LockUnlock('${data.id}')>
                            <i class="bi bi-unlock"></i> Unlock </a> </div>`;
                    }
                    else {
                        // currently user is unlock
                        return `<div class="text-center" >
                            <a class="btn btn-success text-white mx-2" style="cursor:pointer; width:100px" onclick=LockUnlock('${data.id}')>
                            <i class="bi bi-lock"></i> Lock </a> </div>`;

                    }
  
                },

                "width": "15%"
            }
        ],
        "language": {
            "emptyTable":"no data found."
        },
        "width": "100%"
    });
}

function LockUnlock(id)
{
    alert(id);
    $.ajax({
        url: "/api/user",
        type: "POST",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
                //success notification
            }
            else {
                //failsure notification
                toastr.error(data.message);
            }
        }
    });
}