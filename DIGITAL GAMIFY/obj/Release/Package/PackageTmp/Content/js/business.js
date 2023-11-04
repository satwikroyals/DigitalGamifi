
var globalWebSiteApiurl = websiteurlapi;
var addBusinessUrl = websiteurl + "admin/restaurants/add";
var currpageurl = window.location.href.toLowerCase();
$(document).ready(function () {
    GetBusinessTypes('.ddlbusinesstypeid');
});

//Set pages onload functions.
$(function () {
    // Set Curentpageurl.
    if (currpageurl.indexOf('restaurants/add') > -1) {
        //GetBusinessTypes('.ddlbusinesstypeid');
        $('.ddlbusinesstypeid').val($('.txtbusinesstypeid').val());
        validateNumber(document.getElementById("Mobile"), function (value) { return /^-?\d*$/.test(value); });
        
        $('.ddlbusinesstypeid').on('change', function () {
            $('.txtbusinesstypeid').val($('.ddlbusinesstypeid').val());
        });

    }
    else if (currpageurl.indexOf('restaurants/view') > -1) {
        SetViewBusinessPage();
    }
});

function SetSearchddlBusiness() {
    $("#ddlsearchbusiness").select2({ width: '100%' });
    gl.ajaxreq(globalWebSiteApiurl + "GetDdlBusiness?adminid=" + aid, "GET", null, function (response) {
        var clientData = [];
        if (response != null && response.length > 0) {
            clientData.push({ id: '-1', text: "-- Select --" });
            $.each(response, function (i, item) {
                //item.text = item.text;
                clientData.push(item);
            });
            $("#ddlsearchbusiness").select2({
                data: clientData,
                width: '100%'
            });
        }
        else {
            $("#ddlsearchbusiness").select2({
                data: [],
                width: '100%'
            });
        }
        var selbusiness = querySt('_b') == null ? '-1' : querySt('_b');
        $('#ddlsearchbusiness').val(selbusiness).trigger('change');
    }, '', '', '', '', true, false);
}

//View Business Page.
function SetViewBusinessPage() {
    $('#from,#to').daterangepicker({
        defaultDate: null,
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment().subtract(10, 'years'),
        "opens": "left",
        locale: {
            format: 'DD MMM, YYYY'
        }
    }, function (chosen_date) {
        // $('#' + this.element[0].id).val(chosen_date.format('DD MMM, YYYY'));
    });

    var bid = SetQueryStDefaultVal("_b", 0);
    bid = bid == -1 ? 0 : bid;
    var fr = SetQueryStDefaultVal("_f", "");
    $("#from").val(fr);

    var to = SetQueryStDefaultVal("_t", "");
    $("#to").val(to);

    var str = SetQueryStDefaultVal("_str", "");
    $("#search").val(str);

    var btype = SetQueryStDefaultVal("_bt", 0);
    btype = btype == -1 ? 0 : btype;


    var pi = SetQueryStDefaultVal("_pi", 1);
    var ps = SetQueryStDefaultVal("_ps", 25);
    $("#ddlpagesize").val(ps);


    SetSearchddlBusiness();

    $('#btnShowAll').on("click", function (event) {
        window.location.href = GetUrlWithNoQueryStrings();
    });

    //Search event handler.
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
            window.location.href = GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi + "&_str=" + $("#search").val() + "&_bt=" + $("#BusinessTypeId").val();
        }
        SearchVsdisplay(errObj);
    });

    SetViewBusinessGrid(pi, ps);

    function SetViewBusinessGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/AdminGetBusiness", "POST", { AdminId: aid, BusinessId: bid, FromDate: fr, ToDate: to, Pi: pageindex, Ps: pagesize, str: str, BusinessTypeId: btype }, function (response) {
            if (response.length > 0) {
                
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    row += "<tr><td class='col-lg-2'>" + item.BusinessName + "</td><td class='col-lg-1'><img src=" + item.LogoPath + " class='grimg' /></td><td class='col-lg-2'>" + item.Email + "</td><td class='col-lg-2'>" + item.Mobile + "</td><td class='col-lg-2'>" + item.ModifiedDateDisplay + "</td><td class='col-lg-1 text-center'>" + item.IsActiveText + "</td><td class='col-lg-2 menu-action text-center'>" +
                        "<a href='" + addBusinessUrl + "?_b=" + item.BusinessId + "' data-original-title='view' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-eye'></i> </a>" +
                        "<a href='" + addBusinessUrl + "?_b=" + item.BusinessId + "' data-original-title='edit' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-pencil'></i> </a>" + "</td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "&_str=" + $("#search").val() + "&_bt=" + $("#BusinessTypeId").val());
        SetViewBusinessGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "&_str=" + $("#search").val() + "&_bt=" + $("#BusinessTypeId").val());
        SetViewBusinessGrid(pageindex, pagesize);
    });
}




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
    $('.mytxtfilds').each(function () {
        var type = $(this).prop('type');
        // alert(type);
        if (type == 'text' || type == 'textarea' || type == 'password') {
            var value = $(this).val();

            if (value.trim() == '' || value == undefined) {
                error.push($(this).attr('data-val-required'));
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
                error.push($(this).attr('data-val-required'));
            }
        }
        //if (type == 'file') {
        //    var file = $(this).val();

        //    if (file == '') {
        //        //error.push($(this).attr('data-val-required'));
        //        error.push('please select Image');
        //    }
        //    else {
        //        var acceptfiles = $(this).attr('fileformates').split(',');
        //        var fextension = file.split('.')[1];
        //        if (acceptfiles.indexOf(fextension) == -1) {
        //            error.push('incorrect file formate');
        //        }
        //    }
        //}
    });

    return error;
}
function Addbusiness() {
    var res = validate();
    if (res == false) {
        return false;
    } else {
        var empObj = {
            AdminId:1,
            FirstName: $('#FirstName').val(),
            LastName: $('#LastName').val(),
            Mobile: $('#Mobile').val(),
            Email: $('#Email').val(),
            BusinessName: $('#BusinessName').val(),
            BusinessTypeId: $('#BusinessTypeId').val(),
            ZipCode: $('#ZipCode').val(),
            Address: $('#Address').val(),
            Latitude: $('#Latitude').val(),
            Longitude: $('#Longitude').val(),
            IsActive: 0,
            UserName: $('#UserName').val(),
            Password: $('#Password').val(),
            //Image: $('#tempimg').attr('src')=="" ? 1 : $('#txtEnd').val().trim(),
            //Image: $('#hidbinary').val(),
            imgfile: $('#imgpreview').attr('src'),
            //imgfile: $('#imgfile').val(),
            
        };
        $.ajax({
            url: globalWebSiteApiurl + "BusinessRegister",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                //alert(result.StatusCode);
                $("input:text").val("");
                alert('Registered. we will contact you soon. Thank you');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
