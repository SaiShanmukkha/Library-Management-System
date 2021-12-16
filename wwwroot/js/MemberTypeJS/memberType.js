$(document).ready(function () {
  getMemberTypeTable();
});

function viewUpdateModal(memberTypeId){    
    $("#updateMemberTypeIdField").val(memberTypeId);
    $.ajax({
        url:"/api/membertype/getmembertype/"+memberTypeId,
        type:"get",
        success:function(res){
            $("#updateMemberTypeField").val(res.memberTypeName);
        },
        error:function(res){
            $("#updateModalCenter").modal('hide');
            $.notify("Error Occured");
        }
    });    
    $("#updateModalCenter").modal('show');
}

function updateCall() {
    var memberTypeId = $("#updateMemberTypeIdField").val();
    var memberTypeName = $("#updateMemberTypeField").val();
  if (memberTypeName.trim() != "" && memberTypeId > 0) {
    var data = {
      MemberTypeId: memberTypeId,
      MemberTypeName: memberTypeName
    };
    $.ajax({
      url: "/api/membertype/updateMemberType",
      type: "post",
      contentType:"application/json",
      data: JSON.stringify(data),
      success: function () {
        $.notify("Updated Successfully", "success");
        getMemberTypeTable();
      },
      error: function (data) {
        $.notify("Error Occured", "danger");
      },
    });
  }else{
      $.notify("Something went wrong,unable to Modify","Warning");
  }
  $("#updateModalCenter").modal("hide");  
}

function createCall() {
  var memberTypeName = $("#createMemberTypeField").val();
  if (memberTypeName.trim() != "") {
    var data = {
      MemberTypeName: memberTypeName
    };
    $.ajax({
      url: "/api/membertype/createMemberType",
      type: "post",
      contentType:"application/json",
      data: JSON.stringify(data),
      success: function () {
        $.notify("Created Successfully", "success");
        $("#createMemberTypeField").val("");
        getMemberTypeTable();
      },
      error: function (data) {
        $.notify("Error Occured", "danger");
      },
    });
  }
  $("#createModalCenter").modal("hide");
}

function deleteCall(id) {
  var isConfirm = confirm("Are you really want to delete?");
  if (isConfirm) {
    $.ajax({
      url: "/api/membertype/deleteMemberType/" + id,
      type: "delete",
      success: function () {
        $.notify("Deleted Successfully", "success");
        getMemberTypeTable();
      },
      error: function () {
        $.notify("Error Occured", "danger");
      },
    });
  }
}

function getMemberTypeTable() {
  $.ajax({
    type: "get",
    url: "/api/membertype/getmembertypes",
    success: function (res) {
      var memberTypeTable = `
                <table class="table table-bordered table-secondary table-striped small-font">
        <thead class="thead-dark text-center">
            <tr>
                <th style="width:60%">MemberType</th>
                <th style="width:30%">Actions</th>
            </tr>
        </thead>
        <tbody>`;
      if (res.length > 0) {
        for (var i = 0; i < res.length; i++) {
          memberTypeTable += `
                <tr>
                    <td>${res[i]["memberTypeName"]}</td>
                    <td class="text-center">
                        <button class="btn-sm btn-outline-success" type="button" onclick="viewUpdateModal(${res[i]["memberTypeId"]}, ${res[i]["memberTypeId"]});" value=${res[i]["memberTypeId"]} title="Update">
                            <span class="material-icons">
                                upgrade
                            </span>
                        </button>

                        <button class="btn-sm btn-outline-danger" type="button" onclick="deleteCall(${res[i]["memberTypeId"]});" value=${res[i]["memberTypeId"]} type="button" title="Delete">
                            <span class="material-icons">
                                delete_forever
                            </span>
                        </button>
                    </td>
                </tr>
`;
        }
        memberTypeTable += `</tbody></table>`;
        $("#memberTypeTable").html(memberTypeTable);
      } else {
        memberTypeTable += `</tbody></table><p class="text-center">No Data</p>`;
        $("#memberTypeTable").html(memberTypeTable);
      }
    },
    error: function (e) {
      alert("error");
    },
  });
}
