
function Register() {
    var gamefinishUrl = $('#url').val();
    if ($('#FirstName').val() == "") {
        alert("Enter FirstName");
        return;
    }
    if ($('#LastName').val() == "") {
        alert("Enter LastName");
        return;
    }
    if ($('#sweepgame').val() == 1 && $('#Address').val() == "") {
        alert("Enter Address");
        return;
    }
    if ($('#sweepgame').val() == 1 && $('#ZipCode').val() == "") {
        alert("Enter Zipcode");
        return;
    }
    if ($('#sweepgame').val() == 1 && $('#StateId').val() == "") {
        alert("Select State");
        return;
    }
    if ($('#sweepgame').val() == 1 && $('#City').val() == "") {
        alert("Enter City");
        return;
    }
    if ($('#sweepgame').val() == 1 && $('#ReferredBy').val() == "") {
        alert("How did you hear about Sweepstakes?");
        return;
    }
    function validate(s) {
        var rgx = /^[0-9]*\.?[0-9]*$/;
        return s.match(rgx);
    }
    if ($('#Mobile').val() == "") {
        alert("Enter Mobile");
        return;
    }
    //else if (!rgx.match($('#Mobile').val())) {
    //    alert("Only numbers allowed");
    //    return;
    //}
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if ($('#Email').val() == '') {
        alert("Enter Email");
        return;
    }
    else if (!emailReg.test($('#Email').val())) {
        alert("Entered Invalid Email");
    }
    //if ($('#Email').val().match(/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/)) {
    //    alert("Entered Invalid Email");
    //    return;
    //}

        //var res = validate();
        //if ($('#code').val() == "") {
        //    alert("Please Enter Code.")
        //}
        //if ($('#code').val() != $('#VerifyCode').val()) {
        //    alert("Incorrect Code.")
        //}
    else {
        var empObj = {
            FirstName: $('#FirstName').val(),
            LastName: $('#LastName').val(),
            Mobile: $('#Mobile').val(),
            Email: $('#Email').val(),
            Pin: $("#Pin").val(),
            Age: $("#Age").val(),
            Gender: $("#Gender").val(),
            DOB: $("#DOB").val(),
            ZipCode: $("#ZipCode").val(),
            BusinessId: $('#bid').val(),
            Address: $('#Address').val(),
            AddressLine2: $('#AddressLine2').val(),
            ReferredBy: $('#ReferredBy').val(),
            StateId: $('#StateId').val(),
            City: $('#City').val(),
            Guest: 1,
        };
        $.ajax({
            url: websiteurlapi + "RegisterGuest",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("input:text").val("");
                $("#Pin").val("");
                if (result.Id > 0) {
                    //console.log(result);
                    if (result.Id < 0) {
                        alert(result.Message);
                    }
                    //$("#customerid").val(data.Id);
                }
                //RedeemPrize();
                //window.location.href = gamefinishUrl;
                window.location.href = gamefinishUrl + '&cid=' + result.Id + '&_m=' + $('#Email').val();
                $("#Email").val("");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function EmailVerification() {
    if ($('#FirstName').val() == "") {
        alert("Enter FirstName")
    }
    if ($('#LastName').val() == "") {
        alert("Enter LastName")
    }
    if ($('#Mobile').val() == "") {
        alert("Enter Mobile")
    }
    if ($('#Email').val() == "") {
        alert("Enter Email")
    }
    else {
        $("#first").css("display", "none");
        $("#second").css("display", "block");
        //$("#second").fadeOut("fast", function () {
        //    $("#first").fadeIn("fast");
        //});
        var email = $("#Email").val();
        gl.ajaxreq(websiteurlapi + 'EmailVerification', 'get', { Email: email }, function (response) {
            if (response.StatusMessage != "") {
                $("#VerifyCode").val(response.StatusMessage);
            }
        }, '', '', '', '', false, false);
    }
}