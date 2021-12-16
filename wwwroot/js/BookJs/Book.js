$(document).ready(function () {


    $(".deleteConfirmation").click(function () {

        var confirmStatus = confirm("Are you really want to delete?");
        if (confirmStatus) {
            $("#BookDeleteForm").submit();
        }
    });


});