$(document).ready(function () {
   
    $("#otherLib").hide();
    $("#dateChange").change(function () {
        var today = new Date();
        var birthDate = $("#dateChange").val();
        birthDate.replace("-", "/");
        var age = today.getFullYear() - birthDate.substring(0, 4);
        var m = today.getMonth() - birthDate.substring(5, 7);
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
        $("#ageField").show();
        $("#age").val(age);
        $("#age").attr("disabled","disabled");
    });

    $(".deleteConfirmation").click(function () {
        var confimStatus = confirm("Do you really want to delete?");
        if (confimStatus) {
            $("#MemberDeleteForm").submit();
        }
    });

    
        $.ajax({
          type: "Get",
          url: "/api/membertype/getmembertypes",
          async: false,
          success: function (response) {
            if (response.length > 0) {
              $("#MemberTypeSelect").html("");
                var options = `<option hidden selected>Select one...</option>`;
              for (var i = 0; i < response.length; i++) {
                options +=
                  '<option value="'+response[i]["memberTypeId"]+'">'+response[i]["memberTypeName"]+"</option>";
              }
              $("#MemberTypeSelect").append(options);
            }
          },
          error: function (e) {
            $.notify("errror occured while fetching MemberTypes","warn");
          },
        });
      

    //$("#memberSubmit").click(function () {
    //    var lk = "";
    //    $('input[type=checkbox]').each(function () {
    //        if (this.checked){
    //         lk += ($(this).val() +"@");
    //        }
    //    });
    //    $("#languagesKnown").val(lk);
    //    $("#memberForm").submit();
    //});

    $("#countrySelect").change(function () {
        if ($("#countrySelect").val() == "USA") {
            $("#OtherCountry").hide();
            $("#SSNField").show();
        } else {
            $("#OtherCountry").show();
            $("#SSNField").hide();
        }
    });

});

function getRadioValue(obj) {
    var value = $(obj).val();
    if (value == "Yes") {
        $("#otherLib").show();
        $("#otherLib").attr("required");
    } else {
        $("#otherLib").hide();
        $("#otherLib").removeAttr("required");
    }
}



function bookAllocationHistory() {
   
    let x = $("#memberid").val();
    $.ajax({
        url: "/api/Data/BookAllocationHistory?id="+x,
        type: 'GET',
        success: function (res) {
            if (res.length > 0) {
            var content = `
<table class="table table-bordered table-secondary table-hover table-striped">
        <thead class="thead-dark">
            <tr>
                <th>
                    Book Id
                </th>
                <th>
                    Book Name
                </th>
                <th>
                    Book Submitted
                </th>
                <th>
                    Issue Date
                </th>
                <th>
                    Due Date
                </th>
                <th>
                    Return Date
                </th>
            </tr>
        </thead>
<tbody>
`;
           
                for (var i = 0; i < res.length; i++) {
                    var status = res[i]['isBookSubmitted'] ? "Yes" : "No";
                    content += `
                        <tr>
                                <td width="10%">${res[i]['bookId']}</td>
                                <td width="20%">${res[i]['bookName']}</td>
                                <td width="10%">${status}</td>
                                <td width="20%">${res[i]['issueDate']}</td>
                                <td width="20%">${res[i]['dueDate']}</td>
                                <td width="20%">${res[i]['returnDate']}</td>
                        </tr>
                    `;

                }
                content += `</tbody></table>`;
           
                $("#here_table").html(content);
            } else {
                $("#here_table").html( `<hr/><p class="text-center">No Data</p>`);

            }
        },
        error: function (res) {
            alert("error");
        }
    });
}


function selldata() {
   
        var lk = "";
        $('input[type=checkbox]').each(function () {
            if (this.checked) {
                lk += ($(this).val() + "@");
            }
        });
        $("#languagesKnown").val(lk);
        
    var data = {
        MemberName:$("#MemberName").val(),
        MemberGender: $("#MemberGender").val(),
        MemberDOB: $("#dateChange").val(),
        StreetAddress : $("#StreetAddress").val(),
        SSN : $("#SSN").val(),
        LibraryName: $("#LibraryName").val(),
        languagesKnown: $("#languagesKnown").val(),
        OtherCountry: $("#OtherCountry").val(),
        City: $("#City").val(),                                         
        Country: $("#countrySelect").val(),
        PhoneNumber: $("#PhoneNumber").val(),
        Email : $("#Email").val(),
        Profession: $("#Profession").val(),
        MemberTypeId: $("#MemberTypeSelect").val(),
    };

    $.ajax({
        url: "/Member/Create/",
        type: "post",
        data: data,
       // dataType: 'json',
       // contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function () {
            window.location.href = "/member/index";
        },
    });
}
