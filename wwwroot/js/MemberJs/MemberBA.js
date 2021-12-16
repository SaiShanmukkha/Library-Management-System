var NotificationCount = 0;
$(document).ready(function () {


    $("#MemberTypeId").click(function () {
        $.ajax({
            type: "Get",
            url: "/api/Data/membertype/getmembertypes",
            async: false,
            success: function (response) {
                if (response.length > 0) {
                    $('#MemberTypeId').html('');
                    var options = '';
                    options += '<option value="Select">Select</option>';
                    for (var i = 0; i < response.length; i++) {
                        options += '<option value="' + response[i][""] + '">' + response[i][] + '</option>';
                    }
                    $('#countryDropDownList').append(options);
                },
                error: function (e) {
                    alert("errror occured");
                }
            });
    });
    GetNotify();
   
});



function GetNotify() {
    NotificationCount = 0;
    var x = $("#memberid").val();
    getOverDue(x);
    getPastDue(x);
     $("#NotificationCount").text(NotificationCount);
}

// Incomplete
function GoBack() {
    window.location.href = "/Member/Index"
}

function getPastDue(x) {
    $.ajax({
        type: "get",
        url: "/api/Data/GetPastDue/" + x,
        async: false,
        success: function (count) {
            if (count > 0) {
                NotificationCount += count;
                $.notify(`Books Past Due Date: ${count}`, "warn");
            }
        },
        error: function (e) {
            $.notify("some error occured", "error");
        }
    });
    
}

function getOverDue(x) {
    $.ajax({
        type: "get",
        url: "/api/Data/GetOverDue/" + x,
        async: false,
        success: function (count) {
            if (count > 0) {
                NotificationCount += count;
                $.notify(`Books nears Due Date: ${count}`, "info");
            }
        },
        error: function (e) {
            $.notify("some error occured", "error");
        }
    });
}