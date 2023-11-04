
var globalWebSiteApiurl = websiteurlapi;
var addTravelAgencyUrl = websiteurl + "admin/travelagency/add";
var addPackageUrl = websiteurl + "admin/packages/add";
var currpageurl = window.location.href.toLowerCase();


//Set pages onload functions.
$(function () {
    // Set Curentpageurl.
    if (currpageurl.indexOf('travelagency/add') > -1) {
        validateNumber(document.getElementById("Mobile"), function (value) { return /^-?\d*$/.test(value); });
    }
    else if (currpageurl.indexOf('travelagency/view') > -1) {        
        SetViewTravelAgencyPage();
    }


    if (currpageurl.indexOf('packages/add') > -1) {
       
    }
    else if (currpageurl.indexOf('packages/view') > -1) {       

        $('#ddlsearchtravelagency').on('change', function () {          
            SetSearchddlPacakge($(this).val());
        });

        SetViewPacakgesPage();

    }

});

function SetSearchddlTravelAgency() {
    $("#ddlsearchtravelagency").select2({ width: '100%' });
    gl.ajaxreq(globalWebSiteApiurl + "GetDdlTravelAgencies?adminid=" + aid, "GET", null, function (response) {
        var clientData = [];
        if (response != null && response.length > 0) {
            clientData.push({ id: '-1', text: "-- Select --" });
            $.each(response, function (i, item) {
                //item.text = item.text;
                clientData.push(item);
            });
            $("#ddlsearchtravelagency").select2({
                data: clientData,
                width: '100%'
            });
        }
        else {
            $("#ddlsearchtravelagency").select2({
                data: [],
                width: '100%'
            });
        }
        var seltravelagency = querySt('_ta') == null ? '-1' : querySt('_ta');        
        $('#ddlsearchtravelagency').val(seltravelagency).trigger('change');
        if (currpageurl.indexOf('packages/view') > -1) {
            SetSearchddlPacakge(seltravelagency);
        }

    }, '', '', '', '', true, false);
}

function SetSearchddlPacakge(agencyid) {
    $("#ddlsearchpackage").empty();
    $("#ddlsearchpackage").select2({ width: '100%' });
    gl.ajaxreq(globalWebSiteApiurl + "GetDdlPackages?agencyId=" + agencyid, "GET", null, function (response) {
        var clientData = [];   
        if (response != null && response.length > 0) {
            clientData.push({ id: '-1', text: "-- Select --" });
            $.each(response, function (i, item) {
                //item.text = item.text;
                clientData.push(item);
            });
            $("#ddlsearchpackage").select2({
                data: clientData,
                width: '100%'
            });
        }
        else {
            $("#ddlsearchpackage").select2({
                data: [],
                width: '100%'
            });
        }
        var selpackage = querySt('_p') == null ? '-1' : querySt('_p');
        $('#ddlsearchpackage').val(selpackage).trigger('change');
    }, '', '', '', '', true, false);
}

//View travelagency Page.
function SetViewTravelAgencyPage() {
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

    var taid = SetQueryStDefaultVal("_ta", 0);
    taid = taid == -1 ? 0 : taid;
    var fr = SetQueryStDefaultVal("_f", "");
    $("#from").val(fr);

    var to = SetQueryStDefaultVal("_t", "");
    $("#to").val(to);

    var pi = SetQueryStDefaultVal("_pi", 1);
    var ps = SetQueryStDefaultVal("_ps", 25);
    $("#ddlpagesize").val(ps);


    SetSearchddlTravelAgency();

    $('#btnShowAll').on("click", function (event) {
        window.location.href = GetUrlWithNoQueryStrings();
    });

    //Search event handler.
    $('#btnShow').on("click", function (event) {
        var dateerror = CheckDatesDiff($('#from').val(), $('#to').val(), 10, 0);
        var errObj = [];
        var hasErrors = false;       
        if (dateerror != "") {
            hasErrors = true;
            errObj.push(dateerror);
        }
        if (hasErrors == false) {
            window.location.href = GetUrlWithNoQueryStrings() + "?_ta=" + $("#ddlsearchtravelagency").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi;
        }
        SearchVsdisplay(errObj);
    });

    SetViewTravelAgencyGrid(pi, ps);

    function SetViewTravelAgencyGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/AdminGetTravelAgencies", "POST", { AdminId: aid, AgencyId: taid, FromDate: fr, ToDate: to, Pi: pageindex, Ps: pagesize }, function (response) {
            if (response.length > 0) {               
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    row += "<tr><td class='col-lg-2'>" + item.AgencyName + "</td><td class='col-lg-1'><img src=" + item.LogoPath + " class='grimg' /></td><td class='col-lg-2'>" + item.Email + "</td><td class='col-lg-2'>" + item.Mobile + "</td><td class='col-lg-2'>" + item.ModifiedDateDisplay + "</td><td class='col-lg-1 text-center'>" + item.IsActiveText + "</td><td class='col-lg-2 menu-action text-center'>" +
                        "<a href='" + addTravelAgencyUrl + "?_ta=" + item.AgencyId + "' data-original-title='view' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-eye'></i> </a>" +
                        "<a href='" + addTravelAgencyUrl + "?_ta=" + item.AgencyId + "' data-original-title='edit' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-pencil'></i> </a>" + "</td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_ta=" + $("#ddlsearchtravelagency").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewTravelAgencyGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_ta=" + $("#ddlsearchtravelagency").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewTravelAgencyGrid(pageindex, pagesize);
    });
}


//View packages Page.
function SetViewPacakgesPage() {
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

    var taid = SetQueryStDefaultVal("_ta", 0);
    taid = taid == -1 ? 0 : taid;  

    var pid = SetQueryStDefaultVal("_p", 0);
    pid = pid == -1 ? 0 : pid;

    var fr = SetQueryStDefaultVal("_f", "");
    $("#from").val(fr);
    var to = SetQueryStDefaultVal("_t", "");
    $("#to").val(to);

    var pi = SetQueryStDefaultVal("_pi", 1);
    var ps = SetQueryStDefaultVal("_ps", 25);
    $("#ddlpagesize").val(ps);


    SetSearchddlTravelAgency();

    $('#btnShowAll').on("click", function (event) {
        window.location.href = GetUrlWithNoQueryStrings();
    });

    //Search event handler.
    $('#btnShow').on("click", function (event) {
        var dateerror = CheckDatesDiff($('#from').val(), $('#to').val(), 10, 0);
        var errObj = [];
        var hasErrors = false;
        if (dateerror != "") {
            hasErrors = true;
            errObj.push(dateerror);
        }
        if (hasErrors == false) {
            window.location.href = GetUrlWithNoQueryStrings() + "?_ta=" + $("#ddlsearchtravelagency").val() + "&_p=" + $("#ddlsearchpackage").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi;
        }
        SearchVsdisplay(errObj);
    });

    SetViewPackagesGrid(pi, ps);

    function SetViewPackagesGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/AdminGetPackages", "POST", { PackageId: pid, AdminId: aid, AgencyId: taid, FromDate: fr, ToDate: to, Pi: pageindex, Ps: pagesize }, function (response) {
            if (response.length > 0) {              
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    row += "<tr><td class='col-lg-4'>" + item.Name + "</td><td class='col-lg-2'><img src=" + item.ImagePath + " class='grimg' /></td><td class='col-lg-2'>" + item.ModifiedDateDisplay + "</td><td class='col-lg-1 text-center'>" + item.IsActiveText + "</td><td class='col-lg-2 menu-action text-center'>" +
                        "<a href='" + addPackageUrl + "?_p=" + item.PackageId + "' data-original-title='view' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-eye'></i> </a>" +
                        "<a href='" + addPackageUrl + "?_p=" + item.PackageId + "' data-original-title='edit' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-pencil'></i> </a>" + "</td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_ta=" + $("#ddlsearchtravelagency").val() + "&_p=" + $("#ddlsearchpackage").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewPackagesGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_ta=" + $("#ddlsearchtravelagency").val() + "&_p=" + $("#ddlsearchpackage").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewPackagesGrid(pageindex, pagesize);
    });
}