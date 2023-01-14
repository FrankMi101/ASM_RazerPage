var dataTable;
$(document).ready(function () {

  //  var panel = window.location.search;
  //  panel = panel.replace("?panel=", "");
  //  GetSchoolByPanel(panel);

    var searchBy = getQuerystring("type"); // window.location.search;
    var value = getQuerystring("value"); // window.location.search;

    GetSchoolBySearch(searchBy, value);


    $("#SearchValueDDL").change(function () {
        $('#DT_load').val("");
        var searchby = $("#SearchByDDL").val();
        var searchValue = $("#SearchValueDDL").val();
        GetSchoolBySearch(searchby, searchValue);
    });

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
function GetSchoolByPanel(panel) {

    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/School/" + panel,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "unitCode", "width": "5%" },
            { "data": "unitName", "width": "25%" },
            { "data": "bsid", "width": "10%" },
            { "data": "principalID", "width": "10%" },
            { "data": "areaCode", "width": "10%" },
            { "data": "typeCode", "width": "10%" },
            { "data": "districtCode", "width": "10%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" >
                            <a href="/Admin/Schools/Edit?id=${data}"  class="btn btn-success text-white mx-2">
                            <i class="bi bi-pencil-square"></i>  </a>
                            <a onClick=Delete('/api/School/'+${data})  class="btn btn-danger text-white mx-2">
                             <i class="bi bi-trash-fill"></i>  </a>
                            </div>`
                },

                "width": "15%"
            }
        ],
        "width": "100%"
    });
}

function GetSchoolBySearch(searchBy, searchValue) {
   alert(searchBy + "  " + searchValue);
   
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/School/" + searchBy + "/" + searchValue,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "unitCode", "width": "5%" },
            { "data": "unitName", "width": "25%" },
            { "data": "bsid", "width": "10%" },
            { "data": "principalID", "width": "10%" },
            { "data": "areaCode", "width": "10%" },
            { "data": "typeCode", "width": "10%" },
            { "data": "districtCode", "width": "10%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" >
                            <a href="/Admin/Schools/Edit?id=${data}"  class="btn btn-success text-white mx-2">
                            <i class="bi bi-pencil-square"></i>  </a>
                            <a onClick=Delete('/api/School/'+${data})  class="btn btn-danger text-white mx-2">
                             <i class="bi bi-trash-fill"></i>  </a>
                            </div>`
                },

                "width": "15%"
            }
        ],
        "width": "100%",
        "order":[[1,"asc"]],
        "retrieve": true,
    });
}

function Delete(url) {
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