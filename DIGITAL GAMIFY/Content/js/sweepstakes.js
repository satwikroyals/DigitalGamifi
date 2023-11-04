﻿var globalWebSiteApiurl = websiteurlapi;
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
        if (type == 'text' || type == 'textarea' || type == 'password') {
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

function SetViewsweepstakesPage() {
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

    //var bid = SetQueryStDefaultVal("_b", 0);
    //bid = bid == -1 ? 0 : bid;

    var fr = SetQueryStDefaultVal("_f", "");
    $("#from").val(fr);
    var to = SetQueryStDefaultVal("_t", "");
    $("#to").val(to);
    var str = SetQueryStDefaultVal("_str", "");
    $("#search").val(str);

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
        if (dateerror != "") {
            hasErrors = true;
            errObj.push(dateerror);
        }
        if (hasErrors == false) {
            window.location.href = GetUrlWithNoQueryStrings() + "?_b=" + $("#BusinessId").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi + "&_str=" + $("#search").val();
        }
        SearchVsdisplay(errObj);
    });

    SetViewSweepstakesGrid(pi, ps);

    function SetViewSweepstakesGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/GetAdminSweepstakesList", "get", { adminid: 0, bid: bid, pgindex: pageindex, pgsize: pagesize, FromDate: fr, ToDate: to, str: str }, function (response) {
            if (response.length > 0) {
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    if (item.Status <= 0) {
                        item.Status = "<i class='fa fa-circle' style='color:#FF0000 !important;margin-left: 25px;'></i>";
                    }
                    else {
                        item.Status = "<i class='fa fa-circle' style='color:#00aaad !important;margin-left: 25px;'></i>";
                    }
                    row += "<tr><td>" + item.Status + "</td><td><img src='" + item.GameImagepath + "' class='grimg'/></td><td>" + item.GameName + "</td><td><a href=" + item.GameLink + " target='blank' style='color: black;'>" + item.GameLink + "</a></td><td>" + item.ModifiedDatestring + "</td><td>" + item.StartDatestring + "<br/><b>to </b> " + item.EndDatestring + "</td><td><button class='wrench-bg' onclick='quizdetails(" + item.GameId + ")'><i class='fa fa-wrench'></i></button></td><td><button class='times-bg' onclick='DeleteQuiz(" + item.GameId + ")'><i class='fa fa-trash-o'></i></button></td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#BusinessId").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "_str=" + $("#search").val());
        SetViewSweepstakesGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#BusinessId").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "_str=" + $("#search").val());
        SetViewSweepstakesGrid(pageindex, pagesize);
    });

}

function quizdetails(id) {
    window.location.href = "AddSweepstakes?id=" + id;
}

function GetquizQandAedit() {
    var sid = $("#GameId").val();
    gl.ajaxreqloader(globalWebSiteApiurl + "GetSweepstakesById", "get", { gid: sid }, function (response) {
        $("#GameName").val(response.GameName);
        $("#StartDate").val(response.StartDatestring);
        $("#EndDate").val(response.EndDatestring);
        $("#ShortDescription").val(response.ShortDescription);
        $("#IsAgeRequire").prop("checked", response.IsAgeRequire || 0);
        $("#imgpreview").attr("src", response.GameImagepath);
        $("#GameImage").val(response.GameImage);
        $("#Status").val(response.Status);
        $("#Conditions").val(response.Conditions);
        $('#AgeCondition').val(response.AgeCondition);
        if (response.QrcodePath == "") {
            $('#qrcode').append('<div class="form-group vb">' +
                      '<div class="col-sm-12 controls pl-0 text-center">' +
                      ' <img id="imgqrcode" src="http://localhost:11328/Content/images/noqr.png" class="qrimg" /><br />' +
                      '</div>' +
                      '</div>');
        } else {
            var jpgdownload = 'QRdownload("pdf","' + response.QRCode + '","' + response.QrcodePath + '","' + response.GameName + '","' + response.ShortDescription + '")';
            $('#qrcode').append('<div class="form-group vb">' +
                     '<div class="col-sm-12 controls pl-0 text-center">' +
                     ' <img id="imgqrcode" src="' + response.QrcodePath + '" class="qrimg" /><br />' +
                     "<button class='btn btn-primary searchrowbtn mt-10 text-center' type='button' onclick='" + jpgdownload + "')'><i class='icon-ok'></i>Download QR Code</button>" +
                     '</div>' +
                     '</div>');
        }
    }, '', 'Loading Questions...', '', 'getting Questions.');
}



function getquizresultlist() {
    //getquizddl();
    GetQuizResult(1, 10, 1, '');
    $(document).on("click", "#QuizId", function (event) {
        GetQuizResult(1, 10, 1, '');
    });
    $(document).on("click", ".d-paging", function (event) {

        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var sortby = 1; //$('#SortBy').val();
        var searchby = $('#Searchstr').val();
        GetQuizResult(pageindex, pagesize, sortby, searchby);
    });
    $(document).on("change", '#ddlpagesize', function (event) {

        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        var searchby = $('#Searchstr').val();
        GetQuizResult(pageindex, pagesize, sortby, searchby);
    });
    $(document).on("keyup", '#Searchstr', function (event) {

        var pagesize = 10;// $('#ddlPageSize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        var searchby = $('#Searchstr').val();
        var CustomerId = $('#ddlCustomerID').val();
        var StationLocation = $('#ddlLocation option:selected').text();
        GetstoreSignalData(pageindex, pagesize, sortby, searchby, CustomerId, StationLocation);
        $('#Searchstr').focus();
        // this.focus();
        var $thisVal = $('#Searchstr').val();
        $('#Searchstr').val('').val($thisVal);

    });
    $(document).on("change", '#SortBy', function (event) {
        var pagesize = $('#ddlPageSize').val();
        var pageindex = 1;
        var sortby = $('#SortBy').val();
        var searchby = $('#Searchstr').val();
        var region = $("#ddlsearchregion").val();
        var zone = $("#ddlsearchzone").val();
        var division = $("#ddlsearchdevision").val();
        var depo = $("#ddlsearchdepot").val();
        var service = $("#ddlsearchservice").val();
        var date = $("#historydate").val();

        var data = { DepotId: depo, ZoneId: zone, RegionId: region, ServiceDetailsId: service, PageSize: pagesize, PageIndex: pageindex, Searchstr: searchby, SortBy: sortby, Date: decodeURIComponent(date) }
        // setArrivalDeparturePunctualityGrid(data);
        SetCustomGrid(reportsWebsiteUrl + "_ArrivalDepartureReportData", data, 'none', showrepotgridid, 'TSRTC');
        $('#ddlPageSize').val(pagesize);
        $('#SortBy').html($('#dummysortby').html());
        $('#SortBy').val(sortby);
        var url = reportsWebsiteUrl + 'ArrivalDepaturePunctualityReport?rpss=' + pageindex + '|' + pagesize + '|' + sortby + '|' + zone + '|' + region + '|' + division + '|' + depo + '|' + searchby + '|' + decodeURIComponent(date) + '|' + service
        setnavigationurl(url);
    });
}

function GetQuizResult(pageindex, pagesize, sortby, searchby) {
    //  $('.pageloader').removeClass("hide");
    //alert('dfr');
    var orgid = $('#orgid').val();
    var searchby = $('#Searchstr').val();

    var grpid = $('#groupid').val();
    var partid = $('#partnerid').val();
    var quizid = $('#QuizId').val();
    var row = '';
    var reccount = 0;
    var isloader = String(searchby).length == 0 ? true : false
    gl.ajaxreqloader(apiurl + "GetQuizResult", "get", { quizid: quizid, pgindex: pageindex, pgsize: pagesize, sortby: sortby, str: searchby }, function (response) {
        if (response.length > 0) {
            $("#quizname").html(response[0].QuizName);
            //console.log(response[0]);
            reccount = response[0].TotalRecords;
            $.each(response, function (i, item) {
                row += "<tr><td>" + item.CustomerName + "</td><td>" + item.Mobile + "</td><td>" + item.AnsweredCount + "</td><td>" + item.CorrectAnswerCount + "</td><td>" + item.DurationString + "</td></tr>";
            });
            $("#tbldata").html(row);
            setPagging(reccount, pageindex, pagesize);
            $('.norec').addClass('hide');
            $('.tblcontent').removeClass('hide');
        }
        else {
            if (String(searchby).length > 0) {
                $('.norec').addClass('hide');
                $('.tblcontent').removeClass('hide');
                $("#tbldata").html("<tr><td>No Data Found</td></td></tr>");
            }
            else {
                $('.norec').removeClass('hide');
                $('.tblcontent').addClass('hide');
            }
        }
    }, '', '', '', '', true, true, '.loader', '.tblcontent', 'text json', 'true');
}



function DeleteQuiz(qid) {
    var ans = confirm("Are you sure you want to Delete this Game?");
    if (ans) {
        $.ajax({
            url: globalWebSiteApiurl + "DeleteSweepstakes?gid=" + qid,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                SetViewsweepstakesPage();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}