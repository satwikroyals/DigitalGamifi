function validate() {
    //isValid = true;
    var error = [];
    error = formValidate();
    if (error.length == 0) {
        $('#error').addClass('hide');
        $('#error').removeClass('show');
        return true;
    }
    else {
        $('#error').addClass('show');
        $('#error').removeClass('hide');
        var valerror = "<ul>";
        $(error).each(function (i, e) {
            valerror += "<li>" + e + "</li>";
        });
        valerror += "</ul>";
        document.getElementById("error").innerHTML = valerror;
        $('#error').focus();
        return false;
    }
}
function formValidate() {

    var error = []
    $('.isvalidate').each(function () {
        var type = $(this).prop('type');
        // alert(type);
        if (type == 'text' || type == 'textarea' || type == 'password'||$("#chkedcustomers").val() == "") {
            var value = $(this).val();

            if (value.trim() == '' || value == undefined) {
                error.push($(this).attr('errormsg'));
            }
            else {
                var isemail = $(this).hasClass('email');
                if (isemail) {
                    if (!validateEmail(value)) {
                        error.push('Enter Correct Email.');
                    }
                }
            }
        }
        if (type == 'select-one') {
            var value = $(this).val();
            var defult = $(this).attr('default');

            if (value == defult) {
                error.push($(this).attr('errormsg'));
            }
        }
        if (type == 'file') {
            var file = $(this).val();

            if (file == '') {
                error.push($(this).attr('errormsg'));
                //error.push('please select File');
            }
            else {
                var acceptfiles = $(this).attr('fileformates').split(',');
                var fextension = file.split('.')[1];
                if (acceptfiles.indexOf(fextension) == -1) {
                    error.push('incorrect file formate');
                }
            }
        }
    });

    return error;
}
function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }

    else {
        return false;

    }

}
function SetCustomersPage() {
    var bid = $('#BusinessId').val();
    var AllcusIds = GetAllCustomerIds(bid);

    var str = SetQueryStDefaultVal("_str", "");
    $("#search").val(str);

    var age = SetQueryStDefaultVal("_age", 0);
    age = age == -1 ? 0 : age;

    var gender = SetQueryStDefaultVal("_g", 0);
    gender = gender == -1 ? 0 : gender;

    var gtid = SetQueryStDefaultVal("_gt", 0);
    gtid = gtid == -1 ? 0 : gtid;

    var pi = SetQueryStDefaultVal("_pi", 1);
    var ps = SetQueryStDefaultVal("_ps", 25);
    $("#ddlpagesize").val(ps);


    $('#btnShowAll').on("click", function (event) {
        window.location.href = GetUrlWithNoQueryStrings();
    });

    $('#btnShow').on("click", function (event) {
        var dateerror = CheckDatesDiff($('#from').val(), $('#to').val(), 10, 0);
        var errObj = [];
        var hasErrors = false;
        //if ($("#ddlsearchbusiness").val() == null || $("#ddlsearchbusiness").val() == '-1') {
        //    hasErrors = true;
        //    errObj.push('Please select Business');
        //}
        if (dateerror != "") {
            hasErrors = true;
            errObj.push(dateerror);
        }
        if (hasErrors == false) {
            window.location.href = GetUrlWithNoQueryStrings() + "?_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi + "&_str=" + $("#search").val() + "&_age=" + $("#Agegroup").val() + "&_g=" + $("#gender").val() + "&_gt=" + $("#gtid").val();
        }
        SearchVsdisplay(errObj);
    });

    $(document).on('change', '.allpgchk', function () {
        var chk = $(this).is(':checked');
        var checkedids = String($('#chkedcustomers').val()).split(',');
        if (chk) {
            $('#chkedcustomers').val((AllcusIds));
            $('.chkbox').prop('checked', 'checked');
        }
        else {
            $('#chkedcustomers').val('');
            $('.chkbox').prop('checked', false);
        }

    });

    $(document).on('change', '.curpgchk', function () {
        var chk = $(this).is(':checked');
        var checkedids = String($('#chkedcustomers').val()).split(',');
        if (chk) {
            // $('#chkedcustomers').val();

            $('.chkbox').each(function () {
                var id = String($(this).prop('id')).split('_')[1];
                var index = checkedids.indexOf(id);
                if (index == -1) {
                    checkedids.push(id);
                }
                $(this).prop('checked', 'checked');
            });

        }
        else {
            // var checkedids = String($('#chkedcustomers').val()).split(',');
            $('#chkedcustomers').val('');
            $('.chkbox').each(function () {
                var id = String($(this).prop('id')).split('_')[1];

                var index = checkedids.indexOf(id);
                checkedids.splice(index, 1);
                $(this).prop('checked', false);
            });
        }
        $('#chkedcustomers').val(String(checkedids));
    });
    $(document).on('change', '.chkbox', function () {
        var checkedids = String($('#chkedcustomers').val()).split(',');
        var chk = $(this).is(':checked');
        if (chk) {
            var id = String($(this).prop('id')).split('_')[1];
            var index = checkedids.indexOf(id);
            if (index == -1) {

                checkedids.push(id);
            }
        }
        else {
            var id = String($(this).prop('id')).split('_')[1];

            var index = checkedids.indexOf(id);
            checkedids.splice(index, 1);

        }
        $('#chkedcustomers').val(String(checkedids));
    });

    SetViewMembersGrid(pi, ps);



    function SetViewMembersGrid(pageindex, pagesize) {
        var bid = $("#BusinessId").val();
        var row = '';
        var reccount = 0;
        var game = '';
        var gtype = '';
        var ids = $('#chkedcustomers').val().split(',');
        gl.ajaxreqloader(websiteurlapi + "/GetGuestCheckInByBusiness", "POST", { BusinessId: bid, Pi: pageindex, Ps: pagesize, age: age, str: str, gender: gender, gtid: gtid }, function (response) {
            if (response.length > 0) {
                $('#tbldata').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    var indx = ids.indexOf(String(item.CustomerId));
                    if (item.ZipCode == null) {
                        item.ZipCode = 'N/A'
                    }
                    if (indx != -1) {
                        row += "<tr><td><input class='chkbox' id='chkid_" + item.CustomerId + "' checked='checked'  type='checkbox'></td><td class='col-lg-2'>" + item.GameTitle + '</br>GameType:' + item.GameTypestring + "</td><td class='col-lg-2'>" + item.FirstName + ' ' + item.LastName + "</td><td class='col-lg-4'>" + item.Mobile + "</td><td class='col-lg-2'>" + item.Email + "</td><td class='col-lg-2'>" + item.Agestring + "</td><td class='col-lg-2'>" + item.Genderstring + "</td><td class='col-lg-2'>" + item.ZipCode + "</td></tr>";
                    }
                    else {
                        row += "<tr><td><input class='chkbox' id='chkid_" + item.CustomerId + "' type='checkbox'></td><td class='col-lg-2'>" + item.GameTitle + '</br>GameType:' + item.GameTypestring + "</td><td class='col-lg-2'>" + item.FirstName + ' ' + item.LastName + "</td><td class='col-lg-4'>" + item.Mobile + "</td><td class='col-lg-2'>" + item.Email + "</td><td class='col-lg-2'>" + item.Agestring + "</td><td class='col-lg-2'>" + item.Genderstring + "</td><td class='col-lg-2'>" + item.ZipCode + "</td></tr>";

                    }
                });
                $("#tbldata").html(row);
                setPaging(reccount, pageindex, pagesize);
            }
            else {
                $('#norec').removeClass('hide');
                $('#tblgrid').addClass('hide');
            }

        }, '', '', '', '', false, false);
    }

    $(document).on("click", ".d-paging", function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "&_str=" + $("#search").val() + "&_age=" + $("#Agegroup").val() + "&_g=" + $("#gender").val() + "&_gt=" + $("#gtid").val());
        SetViewMembersGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "&_str=" + $("#search").val() + "&_age=" + $("#Agegroup").val() + "&_g=" + $("#gender").val() + "&_gt=" + $("#gtid").val());
        SetViewMembersGrid(pageindex, pagesize);
    });
}

function sendcommunication(id) {
    $('#' + id).submit();
}

function GetAllCustomerIds(bid) {
    var ids="";
    gl.ajaxreqloader(websiteurlapi + "GetAllCustomerIds", "get", { bid: bid }, function (response) {
        $.each(response, function (i, item) {
            ids += ',' + item.CustomerId;
        });

    }, '', '', '', '', false, false, '.loader', '.tblcontent', 'text json', 'true');
    return ids;

}