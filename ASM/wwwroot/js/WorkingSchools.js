var dataTable;
$(document).ready(function () {

    var area = getQuerystring("district"); // window.location.search;
    var employeeID = getQuerystring("id");
    var searchby = "District";
    var userId = $("#hfUserID").val();
  
    GetSchoolBySearch(userId,searchby, area, employeeID);

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

function GetSchoolBySearch(userId,searchBy, searchValue, employeeID) {
  //  alert( "user id= " + userId +  " by=" + searchBy + "  value=" + searchValue + " id =" +employeeID);

    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/WorkingSchools/" + userId + "/" + searchBy + "/" + searchValue + "/" + employeeID,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "unitCode", "width": "5%" },
            { "data": "unitName", "width": "30%" },
            { "data": "areaCode", "width": "10%" },
            { "data": "districtCode", "width": "8%" },
            { "data": "appRole", "width": "12%" },
            { "data": "startDate", "width": "13%" },
            { "data": "endDate", "width": "12%" },
            { "data": "selected", "width": "5%" },
            { "data": "actions", "width":"5%"} 
             
        ],
        "width": "100%",
        "order": [[1, "asc"]],
        "retrieve": true,
    });
}

function Select(unitID, groupType, employeeID,actionUser,action) {
    var para = {
        Operate: "EditFromSchoolList",
        Action: action,
        UserID: actionUser,
        GroupType: groupType,
        GroupValue: unitID,
        StaffUserID: employeeID,
        AppID: $("#DDLApps").val(),
        AppRole: $("#DDLUserRole").val(),
        StartDate: new Date($("#StartDate").val()),
        EndDate:new Date( $("#EndDate").val())
    }
     var JSONStr = JSON.stringify(para);
    var myUrl = "/api/WorkingSchools";
    alert(JSONStr);
   
    $.ajax({
        url: myUrl,
        type: "POST",
        data: JSONStr,
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload(); 
            }
            else {
                 toastr.error(data.message);
            }
        }
    });
}
