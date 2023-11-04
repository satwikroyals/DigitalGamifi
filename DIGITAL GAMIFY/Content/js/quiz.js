var globalWebSiteApiurl = websiteurlapi;
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
function Attr1() {
    if ($("#PhysicalPrize1").is(":checked")) {
        getprizeattributes('#attributes');
        $('#attri1').css("display", "block");
    }
    else {
        $('#attri1').css("display", "none");
    }
}
function Attr2() {
    if ($("#PhysicalPrize2").is(":checked")) {
        getprizeattributes('#attributes2');
        $('#attri2').css("display", "block");
    }
    else {
        $('#attri2').css("display", "none");
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
$(document).ready(function () {
    var pageurl = window.location.href.toLowerCase();
    if (pageurl.indexOf("viewquiz") != -1) {
        $("#quizshow-tab").addClass('active');
    }
    if (pageurl.indexOf("addquiz") != -1) {
        $("#quizadd-tab").addClass('active');
    }
});

function SetViewquizPage() {
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

    SetViewQuizGrid(pi, ps);

    function SetViewQuizGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/GetAdminQuizList", "get", { adminid: 3, bid: bid, pgindex: pageindex, pgsize: pagesize, FromDate: fr, ToDate: to, str: str }, function (response) {
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
                    row += "<tr><td>" + item.Status + "</td><td><img src='" + item.QuizImagepath + "' class='grimg'/></td><td>" + item.QuizName + "</td><td><a href=" + item.GameLink + " target='blank' style='color: black;'>" + item.GameLink + "</a></td><td>" + item.ModifiedDatestring + "</td><td>" + item.StartDatestring + "<br/><b>to </b> " + item.EndDatestring + "</td><td><button class='wrench-bg' onclick='quizdetails(" + item.QuizId + ")'><i class='fa fa-wrench'></i></button></td><td><button class='times-bg' onclick='DeleteQuiz(" + item.QuizId + ")'><i class='fa fa-trash-o'></i></button></td></tr>";
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
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#BusinessId").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize+"_str=" + $("#search").val());
        SetViewSwipeanWinGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_b=" + $("#BusinessId").val() + "&_f=" + $("#from").val() + "&_t=" + $("#to").val() + "&_pi=" + pageindex + "&_ps=" + pagesize+"_str=" + $("#search").val());
        SetViewSwipeanWinGrid(pageindex, pagesize);
    });

}

function getquizlist() {

    GetQuiz(1, 10, 1, '');

    $(document).on("click", ".d-paging", function (event) {

        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var sortby = 1; //$('#SortBy').val();
        var searchby = $('#Searchstr').val();
        GetQuiz(pageindex, pagesize, sortby, searchby);
    });
    $(document).on("change", '#ddlpagesize', function (event) {

        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        var searchby = $('#Searchstr').val();
        GetQuiz(pageindex, pagesize, sortby, searchby);
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

function GetQuiz(pageindex, pagesize, sortby, searchby) {
    //  $('.pageloader').removeClass("hide");
    //alert('dfr');
    var orgid = $('#orgid').val();
    var pid = $('#pid').val();
    var gid = $('#gid').val();
    var searchby = $('#Searchstr').val();
    var row = '';
    var reccount = 0;
    var isloader = String(searchby).length == 0 ? true : false
    gl.ajaxreqloader(apiurl + "GetAdminQuizList", "get", { orgid: orgid,pid:pid,gid:gid, pgindex: pageindex, pgsize: pagesize, sortby: sortby, str: searchby }, function (response) {
        if (response.length > 0) {
            reccount = response[0].TotalRecords;
            $.each(response, function (i, item) {
                if (item.Status <= 0) {
                    item.Status = "<i class='fas fa-square fa-sm' style='color:#FF0000 !important;margin-left: 25px;'></i>";
                }
                else {
                    item.Status = "<i class='fas fa-square fa-sm' style='color:#00aaad !important;margin-left: 25px;'></i>";
                }
                row += "<tr><td>" + item.Status + "</td><td><img src='" + item.QuizImagepath + "' class='tblimgrw'/></td><td>" + item.QuizName + "</td><td>" + item.ModifiedDatestring + "</td><td>" + item.StartDatestring + "<br/><b>to </b> " + item.EndDatestring + "</td><td><button class='wrench-bg' onclick='quizdetails(" + item.QuizId + ")'><i class='fas fa-wrench'></i></button></td><td><button class='times-bg' onclick='DeleteQuiz(" + item.QuizId + ")'><i class='fas fa-times'></i></button></td></tr>";
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

function quizdetails(id) {
    window.location.href = "AddQuiz?id=" + id;
}

function GetquizQandAedit() {
    var sid = $("#QuizId").val();
    gl.ajaxreqloader(globalWebSiteApiurl + "getAdminQuizById", "get", { sid: sid }, function (response) {
        $("#QuizName").val(response.QuizDetails.QuizName);
        $("#QuizCode").val(response.QuizDetails.QuizCode);
        $("#SmsCode").val(response.QuizDetails.SmsCode);
        $("#StartDate").val(response.QuizDetails.StartDatestring);
        $("#EndDate").val(response.QuizDetails.EndDatestring);
        $("#ShortDescription").val(response.QuizDetails.ShortDescription);
        $("#IsAgeRequire").prop("checked", response.QuizDetails.IsAgeRequire || 0);
        $("#imgpreview").attr("src", response.QuizDetails.QuizImagepath);
        $("#QuizImage").val(response.QuizDetails.QuizImage);
        $("#FirstPrize").attr("src", response.QuizDetails.GamePrizes[0].PrizeImage);
        $("#FirstPrizeText").val(response.QuizDetails.FirstPrizeText);
        $("#SecondPrize").attr("src", response.QuizDetails.GamePrizes[1].PrizeImage);
        $("#SecondPrizeText").val(response.QuizDetails.SecondPrizeText);
        $("#Status").val(response.QuizDetails.Status);
        $("#FirstPrizeImage").val(response.QuizDetails.FirstPrizeImage);
        $("#SecondPrizeImage").val(response.QuizDetails.SecondPrizeImage);
        $("#PrizeExpiryDate").val(response.QuizDetails.PrizeExpiryDate);
        $("#Conditions").val(response.QuizDetails.Conditions);
        $("#IsComplimentary").prop("checked", response.QuizDetails.IsComplimentary || 0);
        $('#AgeCondition').val(response.QuizDetails.AgeCondition);
        $('#FirstPrizeCount').val(response.QuizDetails.FirstPrizeCount);
        $('#SecondPrizeCount').val(response.QuizDetails.SecondPrizeCount);
        $('#OnceIn').val(response.QuizDetails.OnceIn);
        $('#FirstPrizesLeft').html(response.QuizDetails.FirstPrizesLeft);
        $('#SecondPrizesLeft').html(response.QuizDetails.SecondPrizesLeft);
        $('#PhysicalPrize1').prop("checked", response.QuizDetails.PhysicalPrize1 || 0);
        $('#PhysicalPrize2').prop("checked", response.QuizDetails.PhysicalPrize2 || 0);
        Attr1();
        Attr2();
        var attri2 = response.QuizDetails.Attributes1;
        if (attri2 != null) {
            attri2 = attri2.replace(/;/g, ',');
            //console.log(attri);
            var attribute2 = attri2.split(']');
            if (attribute2 != null) {
                $.each(attribute2, function (id, item) {
                    var id = item.split('[')[0];
                    var link = item.split('[')[1];
                    //alert(link);
                    $("#attributes_" + id).val(id);
                    $("#attributesAttrival_" + id).val(link);
                });
            }
        }
        var attri = response.QuizDetails.Attributes2;
        if (attri != null) {
            attri = attri.replace(/;/g, ',');
            //console.log(attri);
            var attribute = attri.split(']');
            if (attribute != null) {
                $.each(attribute, function (id, item) {
                    var id = item.split('[')[0];
                    var link = item.split('[')[1];
                    //alert(link);
                    $("#attributes2_" + id).val(id);
                    $("#attributes2Attrival_" + id).val(link);
                });
            }
        }
        if (response.QuizDetails.QrcodePath == "") {
            $('#qrcode').append('<div class="form-group vb">' +
                      '<div class="col-sm-12 controls pl-0 text-center">' +
                      ' <img id="imgqrcode" src="http://localhost:11328/Content/images/noqr.png" class="qrimg" /><br />' +
                      '</div>' +
                      '</div>');
        } else {
            var jpgdownload = 'QRdownload("pdf","' + response.QuizDetails.QRCode + '","' + response.QuizDetails.QrcodePath + '","' + response.QuizDetails.QuizName + '","' + response.QuizDetails.ShortDescription + '")';
            $('#qrcode').append('<div class="form-group vb">' +
                     '<div class="col-sm-12 controls pl-0 text-center">' +
                     ' <img id="imgqrcode" src="' + response.QuizDetails.QrcodePath + '" class="qrimg" /><br />' +
                     "<button class='btn btn-primary searchrowbtn mt-10 text-center' type='button' onclick='" + jpgdownload + "')'><i class='icon-ok'></i>Download QR Code</button>" +
                     '</div>' +
                     '</div>');
        }
        $.each(response.Question, function (j, item) {
            var i = j + 1;
            $("#QuizQuestionId_" + i).val(item.QuizQuestionId);
            $("#Question_" + i).val(item.Question);
            $("#CorrectAnswerId_" + i).val(item.CorrectAnswerId);
            $.each(item.answers, function (k, itemans) {
                var l = k + 1;
                $("#QuizAnswerId_" + i + "_" + l).val(itemans.QuizAnswerId);
                //$("#ansimg_" + i + "_" + l).css('background-image', "url(" + itemans.AnswerImagePath + ")");
                $("#Answer_" + i + "_" + l).val(itemans.Answer);
            });
            //    //$('.dsqadivedit').append(cnt.html());
        });
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
    var ans = confirm("Are you sure you want to Delete this Quiz?");
    if (ans) {
        $.ajax({
            url: globalWebSiteApiurl + "DeleteQuiz?qid=" + qid,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                getquizlist();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}