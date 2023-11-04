var globalWebSiteApiurl = websiteurlapi;
function SetViewSwipegamePrizePage() {
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
            window.location.href = GetUrlWithNoQueryStrings() + "?_b=" + $("#BusinessId").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi;
        }
        SearchVsdisplay(errObj);
    });

    SetViewSwipeanWinPrizeGrid(pi, ps);

    function SetViewSwipeanWinPrizeGrid(pageindex, pagesize) {
        var status = $("#Status").val();
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/AdminGetSwipeandWinPrizes", "get", { bid: bid, status: status, pgindex: pageindex, pgsize: pagesize, str: '' }, function (response) {
            if (response.length > 0) {
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                 
                    row += "<tr><td>" + item.CustomerName + "</td><td><img src='" + item.PrizeImagePath + "' class='grimg'/></td><td>" + item.Mobile + "</td><td>" + item.PrizeText + "</td><td>" + item.RedeemCode + "</td><td>" + item.PrizeExpiryDateString + "</td><td><button onclick='DeliverPrize(" + item.ResultId + 1 + ")'><i class='fa fa-pencil'></i></button></td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewSwipeanWinGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize);
        SetViewSwipeanWinGrid(pageindex, pagesize);
    });

}


function DeliverPrize(rid,type) {
    gl.ajaxreqloader(globalWebSiteApiurl + "BusinessDeliverprizeactionbtn", "get", { resultid: rid, type: type ,action:1}, function (response) {
        if(response!=null)
        {
            alert('Delivered');
        }
    }, '', 'Loading Questions...', '', 'getting Questions.');
}