
var globalWebSiteApiurl = websiteurlapi;
var addNotificationurl = websiteurl + "admin/notifications/add";
var currpageurl = window.location.href.toLowerCase();


//Set pages onload functions.
$(function () {
    // Set Curentpageurl.
    if (currpageurl.indexOf('notifications/add') > -1) {
       

    }
    else if (currpageurl.indexOf('notifications/view') > -1) {
        SetViewNotificationsPage();
    }
    if (currpageurl.indexOf('business/notification/viewnotifications') > -1) {
        SetViewNotificationsPage();
    }
});

//View Business Page.
function SetViewNotificationsPage() {
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

   
    var fr = SetQueryStDefaultVal("_f", "");
    $("#from").val(fr);

    var to = SetQueryStDefaultVal("_t", "");
    $("#to").val(to);

    var pi = SetQueryStDefaultVal("_pi", 1);
    var ps = SetQueryStDefaultVal("_ps", 25);
    $("#ddlpagesize").val(ps);


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
            window.location.href = GetUrlWithNoQueryStrings() + "?_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi;
        }
        SearchVsdisplay(errObj);
    });

    SetViewNotificationGrid(pi, ps);

    function SetViewNotificationGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/AdminGetNotifications", "POST", { AdminId: aid, FromDate: fr, ToDate: to, Pi: pageindex, Ps: pagesize }, function (response) {
            console.log(response);
            if (response.length > 0) {              
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    row += "<tr><td class='col-lg-2'>" + item.Title + "</td><td class='col-lg-4'>" + item.Text + "</td><td class='col-lg-2'>" + item.CreatedDateDisplay + "</td><td class='col-lg-2 text-center'>" + item.IsActiveText + "</td><td class='col-lg-2 menu-action text-center'>" +
                        "<a href='" + addNotificationurl + "?_n=" + item.NotificationId + "' data-original-title='view' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-eye'></i> </a>" +
                        "<a href='" + addNotificationurl + "?_n=" + item.NotificationId + "' data-original-title='edit' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-pencil'></i> </a>" + "</td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewNotificationGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewNotificationGrid(pageindex, pagesize);
    });
}

