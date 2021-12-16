$(document).ready(function () {

    
    $(".deleteConfirmation").click(function () {
        var confimStatus = confirm("Do you really want to delete?");
        if (confimStatus) {
            $("#AuthorDeleteForm").submit();
        }
    });


});