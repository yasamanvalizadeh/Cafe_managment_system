
// Admin Side Navbar
$('.nav-item').click(function () {


    if ($(this).find(".submenu").css("display") == "none") {
        $(this).find(".submenu").css("display", "block");
    }
    else {
        $(this).find(".submenu").css("display", "none");
    }
});
 

//Sign up

function OnSuccessSignUp(data , status , xhr) {
    if (data = "succuss") {

        $(".f-Input").val("");
        $("#swal2-html-container").html("");

        Swal.fire({
            title: 'Sign Up Success',
            text: "Your account has been successfully created.",
            icon: 'success',
            confirmButtonText: 'Ok'
        })
    }
}
 

function onFailurSignUp(xhr) {
    ShowErrorInAlert(xhr.responseJSON);
}

function ShowErrorInAlert(errors) {
    Swal.fire({
        title: "Error",
        icon: 'error',
        confirmButtonText: 'Ok'
    })

    $("#swal2-html-container").html("<ul id='tErrors'></ul>");
    errors.forEach(error => {
        $("#tErrors").append("<li id='liError' style='text-align:justify;margin-bottom: 14px;'>" + error + "</li>");
    })
    $("#tErrors li:last-child").css("margin-bottom", "0px");
    $("#swal2-html-container").css("display", "inline-block");

}
 