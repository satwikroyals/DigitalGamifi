
var globalWebSiteApiurl = websiteurlapi;
var addSwipeandwinUrl = websiteurl + "business/swipeandwin/add";
var currpageurl = window.location.href.toLowerCase();

function Attr1() {
    if ($("#PhysicalPrize1").is(":checked")) {
        getprizeattributes('#attributes');
        $('#attri1').css("display", "block");
    }
    else {
        $('#attri1').css("display", "none");
    }
}
function Attr2(){
    if ($("#PhysicalPrize2").is(":checked")) {
        getprizeattributes('#attributes2');
        $('#attri2').css("display", "block");
    }
    else {
        $('#attri2').css("display", "none");
    }
}
function Attr3(){
    if ($("#PhysicalPrize3").is(":checked")) {
        getprizeattributes('#attributes3');
        $('#attri3').css("display", "block");
    }
    else {
        $('#attri3').css("display", "none");
    }
}
function getprizeattributes(attri) {
    var ptid = $('#Partnertypeid').val();
    $(attri).empty();
    gl.ajaxreq(globalWebSiteApiurl + "GetAttributesByPrizeTypeId", "get", { ptid: 0 }, function (response) {
        $.each(response, function (index, row) {
            $(attri).append("<div class='form-group row mb-3'><label class='col-sm-2 control-label'>" + row.AttributeName + "</label><input type=hidden id='" + attri.replace('#', '') + "_" + row.AttributeId + "' value=" + row.AttributeId + " name=" + attri.replace('#', '') + " /><div class='col-sm-5'><input type='text' class='width-70 width-110 myfilds mytxtfilds' id='" + attri.replace('#', '') + "Attrival_" + row.AttributeId + "' name='" + attri.replace('#', '') + "_" + row.AttributeId + "' placeholder=''></div></div>")
        });
    }, '', '', '', '', false, false);
}
//Set pages onload functions.
$(function () {
    // Set Curentpageurl.
    if (currpageurl.indexOf('swipeandwin/add') > -1) {
        validateNumber(document.getElementById("FirstPrizeCount"), function (value) { return /^-?\d*$/.test(value); });
        validateNumber(document.getElementById("SecondPrizeCount"), function (value) { return /^-?\d*$/.test(value); });
        validateNumber(document.getElementById("ThirdPrizeCount"), function (value) { return /^-?\d*$/.test(value); });
        validateNumber(document.getElementById("OnceIn"), function (value) { return /^-?\d*$/.test(value); });

        //$('#StartDate,#EndDate').daterangepicker({
        //    defaultDate: null,
        //    singleDatePicker: true,
        //    showDropdowns: true,
        //    minDate: moment().subtract(10, 'years'),
        //    //locale: {
        //    //    format: 'DD MMM, YYYY'
        //    //}
        //}, function (chosen_date) {
        //    // $('#' + this.element[0].id).val(chosen_date.format('DD MMM, YYYY'));
        //});

    }
    else if (currpageurl.indexOf('swipeandwin/view') > -1) {     
        SetSearchddlBusiness();
        SetViewSwipeanWinPage();
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

//View SwipeanWin Page.
function SetViewSwipeanWinPage() {
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
            window.location.href = GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pi + "&_str=" + $("#search").val();
        }
        SearchVsdisplay(errObj);
    });

    SetViewSwipeanWinGrid(pi, ps);

    function SetViewSwipeanWinGrid(pageindex, pagesize) {      
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/AdminGetSwipeandWin", "POST", { AdminId: aid, BusinessId: bid, FromDate: fr, ToDate: to, Pi: pageindex, Ps: pagesize, str: str }, function (response) {
            if (response.length > 0) {
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].TotalRecords;
                $.each(response, function (i, item) {
                    if (item.IsActive <= 0) {
                        item.IsActive = "<i class='fa fa-circle' style='color:#FF0000 !important;margin-left: 25px;'></i>";
                    }
                    else {
                        item.IsActive = "<i class='fa fa-circle' style='color:#00aaad !important;margin-left: 25px;'></i>";
                    }
                    row += "<tr><td class='col-lg-1'>" + item.IsActive + "</td><td class='col-lg-3'>" + item.Title + "</td><td class='col-lg-2'><img src=" + item.ImagePath + " class='grimg' /></td><td><a href=" + item.GameLink + " target='blank' style='color: black;'>" + item.GameLink + "</a></td><td class='col-lg-2'>" + item.StartDateDisplay + "</td><td class='col-lg-2'>" + item.EndDateDisplay + "</td></td><td class='col-lg-2 menu-action text-center'>" +
                        "<a href='" + addSwipeandwinUrl + "?_g=" + item.GameId + "' data-original-title='view' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-eye'></i> </a>" +
                        "<a href='" + addSwipeandwinUrl + "?_g=" + item.GameId + "' data-original-title='edit' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey'> <i class='fa fa-pencil'></i> </a>" + "</td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "_str=" + $("#search").val());
        SetViewSwipeanWinGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#ddlsearchbusiness").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize + "_str=" + $("#search").val());
        SetViewSwipeanWinGrid(pageindex, pagesize);
    });

}


function GetSwipeandWin() {
    var id = $("#GameId").val();
    gl.ajaxreqloader(globalWebSiteApiurl + "GetSwipeandWinById", "get", { Id: id }, function (response) {
        $('#PhysicalPrize1').prop("checked", response.PhysicalPrize1 || 0);
        $('#PhysicalPrize2').prop("checked", response.PhysicalPrize2 || 0);
        $('#PhysicalPrize3').prop("checked", response.PhysicalPrize3 || 0);
        Attr3();
        Attr1();
        Attr2();
        var attri = response.Attributes1;
        if (attri != null) {
            attri = attri.replace(/;/g, ',');
            //console.log(attri);
            var attribute = attri.split(']');
            if (attribute != null) {
                $.each(attribute, function (id, item) {
                    var id = item.split('[')[0];
                    var link = item.split('[')[1];
                    //alert(link);
                    $("#attributes_" + id).val(id);
                    $("#attributesAttrival_" + id).val(link);
                });
            }
        }
        var attri2 = response.Attributes2;
        if (attri2 != null) {
            attri2 = attri2.replace(/;/g, ',');
            //console.log(attri);
            var attribute2 = attri2.split(']');
            if (attribute2 != null) {
                $.each(attribute2, function (id, item) {
                    var id = item.split('[')[0];
                    var link = item.split('[')[1];
                    //alert(link);
                    $("#attributes2_" + id).val(id);
                    $("#attributes2Attrival_" + id).val(link);
                });
            }
        }
        var attri3 = response.Attributes3;
        if (attri3 != null) {
            attri3 = attri.replace(/;/g, ',');
            //console.log(attri);
            var attribute3 = attri3.split(']');
            if (attribute3 != null) {
                $.each(attribute3, function (id, item) {
                    var id = item.split('[')[0];
                    var link = item.split('[')[1];
                    //alert(link);
                    $("#attributes3_" + id).val(id);
                    $("#attributes3Attrival_" + id).val(link);
                });
            }
        }
    }, '', 'Loading Questions...', '', 'getting Questions.');
}
