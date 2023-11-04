function validate() {
    var isValid = true;
    if ($('#FirstName').val().trim() == "") {
        document.getElementById("firstname").innerHTML = "Please enter your firstname";
        $('#FirstName').css('border', 'Red 2.5px solid');
        isValid = false;
    }
    else {
        $('#FirstName').css('border', '#ced4da 1px solid');
        document.getElementById("firstname").innerHTML = "";
    }
    if ($('#LastName').val().trim() == "") {
        document.getElementById("lastname").innerHTML = "Please enter your lastname";
        $('#LastName').css('border', 'Red 2.5px solid');
        isValid = false;
    }
    else {
        $('#LastName').css('border', 'lightgrey');
        document.getElementById("lastname").innerHTML = "";
    }
    if ($('#Email').val().trim() == "") {
        document.getElementById("email").innerHTML = "Please enter a valid e-mail address";
        $('#Email').css('border', 'Red 2.5px solid');
        isValid = false;
    }
    else if ($('#Email').val().match(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)) {
        document.getElementById('email').innerHTML = "";
    }
    else {
        document.getElementById("email").innerHTML = "You have entered an invalid email address!";
        isValid = false;
    }
    //document.getElementById("email").innerHTML = "";
    if ($('#PhoneNumber').val().trim() == "") {
        document.getElementById("phone").innerHTML = "Please enter a valid phonenumber";
        $('#PhoneNumber').css('border', 'Red 2.5px solid');
        isValid = false;
    }
    else {
        $('#PhoneNumber').css('border', 'lightgrey');
        document.getElementById("phone").innerHTML = "";
    }
    return isValid;
}