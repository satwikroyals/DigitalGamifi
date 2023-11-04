function SetSurveyResultPage() {
    //$('#from,#to').daterangepicker({
    //    defaultDate: null,
    //    singleDatePicker: true,
    //    showDropdowns: true,
    //    minDate: moment().subtract(10, 'years'),
    //    "opens": "left",
    //    locale: {
    //        format: 'DD MMM, YYYY'
    //    }
    //}, function (chosen_date) {
    //    // $('#' + this.element[0].id).val(chosen_date.format('DD MMM, YYYY'));
    //});

    //var fr = SetQueryStDefaultVal("_f", "");
    //$("#from").val(fr);

    //var to = SetQueryStDefaultVal("_t", "");
    //$("#to").val(to);

    var str = SetQueryStDefaultVal("_str", "");
    $("#search").val(str);

    var sid = SetQueryStDefaultVal("_s", 0);
    sid = sid == -1 ? 0 : sid;


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
            window.location.href = GetUrlWithNoQueryStrings() + "?_pi=" + pi + "&_str=" + $("#search").val() + "&_s=" + $("#Survey").val();
        }
        SearchVsdisplay(errObj);
    });

    SetViewSurveyResultGrid(pi, ps);

    function SetViewSurveyResultGrid(pageindex, pagesize) {
        var bid = $("#BusinessId").val();
        var row = '';
        var reccount = 0;
        var game = '';
        var gtype = '';
        gl.ajaxreqloader(websiteurlapi + "/GetSurveyResult", "GET", { bid: bid, sid: sid, pgindex: pageindex, pgsize: pagesize, str: str,sortby:1 }, function (response) {
            if (response.length > 0) {
                $('#tbldata').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    row += "<tr><td>" + item.SurveyName + "</td><td>" + item.FirstName + ' ' + item.LastName + "</td><td>" + item.Mobile + "</td><td>" + item.Email + "</td><td>" + item.AnsweredCount + "</td><td>" + item.CreateDateString + "</td><td><button onclick='getSurveyAnswered(" + item.SurveyResultId + ")'><i class='fa fa-eye'></i></button></td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_pi=" + pageindex + "&_ps=" + pagesize + "&_str=" + $("#search").val() + "&_s=" + $("#Survey").val());
        SetViewNotificationGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_pi=" + pageindex + "&_ps=" + pagesize + "&_str=" + $("#search").val() + "&_s=" + $("#Survey").val());
        SetViewNotificationGrid(pageindex, pagesize);
    });
}

function getSurveyAnswered(srid) {
    $('#myModal').modal('show');
    var row = '';
    gl.ajaxreq(websiteurlapi + "GetSurveyResultByResultId", "GET", { SrId: srid }, function (response) {
        if (response.length > 0) {
            $.each(response, function (i, item) {
                if (item.TextAnswer == null) {
                    item.TextAnswer = "N/A";
                }
                //console.log(item);
                row += "<tr><td>" + item.QuestionNum + "</td><td>" + item.Question + "</td><td>" + item.Answer + "</td><td>" + item.TextAnswer + "</td></tr>";
            });
            $("#tblresultdata").html(row);
        }
        else {

        }
    }, '', '', '', '', false, false);
}

//function getSurveyddl() {
//    var row = '';
//    gl.ajaxreqloader(websiteurlapi + "/GetDdlSurveys", "get", { SrId: srid }, function (response) {
//        if (response.length > 0) {
//            $.each(response, function (i, item) {
//                //console.log(item);
//                row += "<tr><td>" + item.QuestionNum + "</td><td>" + item.Question + "</td><td>" + item.Answer + "</td></tr>";
//            });
//            $("#tblresultdata").html(row);
//        }
//        else {

//        }
//    }, '', '', '', '', true, true, '', '', 'text json', 'true');
//}

function getSurveyddl() {
    var bid = $("#BusinessId").val();
    gl.ajaxreq(websiteurlapi + "GetDdlSurveys", "GET", { bid: bid }, function (response) {
        if (response != null && response.length > 0) {
            $.each(response, function (i, item) {
                $("#Survey").append('<option value="' + item.SurveyId + '">' + item.SurveyName + '</option>');
            });
        }
    }, '', '', '', '', false, false);
}