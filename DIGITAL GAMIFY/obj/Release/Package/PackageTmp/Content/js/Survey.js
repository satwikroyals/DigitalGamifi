﻿//1 - On , 0 - Off
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
function Attr2(){
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
            $(attri).append(" <div class='form-group row mb-3'><label class='col-sm-2 control-label'>" + row.AttributeName + "</label><input type=hidden id='" + attri.replace('#', '') + "_" + row.AttributeId + "' value=" + row.AttributeId + " name=" + attri.replace('#', '') + " /><div class='col-sm-5'><input type='text' class='form-control input-sm' id='" + attri.replace('#', '') + "Attrival_" + row.AttributeId + "' name='" + attri.replace('#', '') + "_" + row.AttributeId + "' placeholder=''></div></div>")
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
    if (pageurl.indexOf("viewsurveys") != -1) {
        $("#surtab-show").addClass('active');
    }
    if (pageurl.indexOf("createsurvey") != -1) {
        $("#surtab-add").addClass('active');
        $(document).on('change keyup paste keypress', '.txtq,.txta', function () {
            SetSurveyQuestionReview($('.qpreview').attr('curqid'));
        });
    }
});

function checkfromvalidate() {
    //isValid = true;
    var sid = $('#SurveyId').val();
    var sname = $('#surveyname').val();
    var smscode = $('#smscode').val();
    var error = [];
    error = formValidate();
    if (error.length == 0) {
        $('#error').addClass('hide');
        $('#error').removeClass('show');
        gl.ajaxreqloader(websiteurl + "api/CheckSurveyExist", "get", { sid: sid, sname: sname, smscode: smscode }, function (response) {
            if (response.StatusCode < 0) {
                var serror = response.StatusMessage.split(';');
                // error.push('Username already exist.');
                 $('#error').addClass('show');
                 $('#error').removeClass('hide');
                 var valerror = "<ul>";
                 $(serror).each(function (i, e) {
                   valerror += "<li>" + e + "</li>";
                 });
                 valerror += "</ul>";
                 document.getElementById("error").innerHTML = valerror;
                $('#error').focus();
                return false;
            }
            else
            {
                return false;
            }
        });
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

function SetViewSurveyPage() {
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

    SetViewSurveyGrid(pi, ps);

    function SetViewSurveyGrid(pageindex, pagesize) {
        var bid = $("#BusinessId").val();
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(globalWebSiteApiurl + "/GetAdminSurveyList", "get", { adminid: 3, bid: bid, pgindex: pageindex, pgsize: pagesize, FromDate: fr, ToDate: to, str: str }, function (response) {
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
                    //console.log(item);
                    row += "<tr><td>" + item.Status + "</td><td>" + item.SurveyName + "</td><td>" + item.CreatedDatestring + "</td><td><a href=" + item.GameLink + " target='blank' style='color: black;'>" + item.GameLink + "</a></td><td>" + item.StartDatestring + "<br/><b>to </b> " + item.EndDatestring + "</td><td><button onclick='getSurveyDetailsPage(" + item.SurveyId + ")'><i class='fa fa-pencil'></i></button></td><td><button onclick='DeleteSurvey(" + item.SurveyId + ")'><i class='fa fa-trash-o'></i></button></td></tr>";
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


function getSurveyPage() {
    var me = getUrlVars()["ft"];
    var pageindex = 1;
    var pagesize = 10;
    var sortby = 1;
    var searchby = "";
    if (me != undefined) {
        if (me != '') {
            me = String(me).replace('#', '');
            pageindex = String(me).split('|')[0];
            pagesize = String(me).split('|')[1];
            sortby = String(me).split('|')[2];
            searchby = String(me).split('|')[3];
            //$('#historydate').val(date);
            // $('#historydate').val(decodeURIComponent(date));
        }
    }
    $('#Searchstr').val('').val(searchby);
    GetSurveylist(pageindex, pagesize, 1, searchby)
    $(document).on("click", ".d-paging", function (event) {

        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var sortby = 1; //$('#SortBy').val();
        var searchby = '';//$('#Searchstr').val(); 
        GetSurveylist(pageindex, pagesize, sortby, searchby);
    });
    $(document).on("change", '#ddlpagesize', function (event) {

        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        var searchby = '';//$('#Searchstr').val();
        GetSurveylist(pageindex, pagesize, sortby, searchby);
    });
    $(document).on("keyup", '#Searchstr', function (event) {
        var pagesize = 10;// $('#ddlPageSize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        var searchby = $('#Searchstr').val();
        GetSurveylist(pageindex, pagesize, sortby, searchby);
        $('#Searchstr').focus();
        var $thisVal = $('#Searchstr').val();
        $('#Searchstr').val('').val($thisVal);

    });
}

function GetSurveylist(pageindex, pagesize, sortby, searchby) {
    //alert('dfr');
    var bid = $('#BusinessId').val();
    var pid = $('#pid').val();
    var gid = $('#gid').val();
    var row = '';
    var reccount = 0;
    var isloader = String(searchby).length == 0 ? true : false
    gl.ajaxreqloader(apiurl + "GetAdminSurveyList", "get", { bid: bid,pid:pid,gid:gid, pgindex: pageindex, pgsize: pagesize, sortby: sortby, str: searchby }, function (response) {
        if (response.length > 0) {
            reccount = response[0].TotalRecords;
            $.each(response, function (i, item) {
                if (item.Status <= 0) {
                    item.Status = "<i class='fas fa-square fa-sm' style='color:#FF0000 !important;margin-left: 25px;'></i>";
                }
                else {
                    item.Status = "<i class='fas fa-square fa-sm' style='color:#00aaad !important;margin-left: 25px;'></i>";
                }
                //console.log(item);
                row += "<tr><td>" + item.Status + "</td><td>" + item.SurveyName + "</td><td>" + item.CreatedDatestring + "</td><td>" + item.StartDatestring + "<br/><b>to </b> " + item.EndDatestring + "</td><td><button class='wrench-bg' onclick='getSurveyDetailsPage(" + item.SurveyId + ")'><i class='fas fa-wrench'></i></button></td><td><button class='times-bg' onclick='DeleteSurvey(" + item.SurveyId + ")'><i class='fas fa-times'></i></button></td></tr>";
            });
            $("#tbldata").html(row);
            setPagging(reccount, pageindex, pagesize);
            $('.norec').addClass('hide');
            $('.tblcontent,.filt').removeClass('hide');
        }
        else {
            if (String(searchby).length > 0) {
                $('.norec').addClass('hide');
                $('.tblcontent,.filt').removeClass('hide');
                $("#tbldata").html("<tr><td>No Data Found</td></td></tr>");
            }
            else {
                $('.norec').removeClass('hide');
                $('.tblcontent,.filt').addClass('hide');
            }
        }
    }, '', '', '', '', true, true, '.loader', '.tblcontent', 'text json', 'true');
    //var url = businessbaseurl + 'ViewSurveys?ft=' + pageindex + '|' + pagesize + '|' + sortby + '|' + searchby + '|'
    //setnavigationurl(url);
}

function getSurveyDetailsPage(id) {
    window.location.href = "CreateSurvey?id=" + id;
}

function readURLUnSuccessFullGame(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('.' + $(input).data("preview")).css('background-image', 'url(' + e.target.result + ')');
            $('.' + $(input).data("preview") + '-img').css('display', 'block').attr('src', e.target.result);
            // $('.' + $(input).data("preview") + '-img')
        };
        reader.readAsDataURL(input.files[0]);
        $('.txtunsucdefault').val('0');
    }    
}

function Confirmmodaldelete(colmname,input,mtype,id) {      
    $('#confirmation-modal .delmname').html($(input).parents('.table-row').find('.' + colmname + ' b').html() + ' ' + mtype + ' ?');
    $('#confirmation-modal .deldid').html(id);
    $('#confirmation-modal').modal('show');
}

function Clearconfirmmodaldelete() {
    $('#confirmation-modal .delmname').html('');
    $('#confirmation-modal .deldid').html('0');
}

function Confirmmodalstatus(colmname, input, mtype, id,status) {
    $('#confirmation-modal-status .delmname').html((status == 1 ? "inactive" : "active")+' '+$(input).parents('.table-row').find('.' + colmname + ' b').html() + ' ' + mtype + ' ?');
    $('#confirmation-modal-status .deldid').html(id);
    $('#confirmation-modal-status .deldstat').html(status);
    $('#confirmation-modal-status').modal('show');
}

function Clearconfirmmodalstatus() {
    $('#confirmation-modal .delmname').html('');
    $('#confirmation-modal .deldid').html('0');
    $('#confirmation-modal .deldstat').html('');
}

function Confirmmodaladd(gametypeid) {  
    if ($('.lblmarketinglimitcount').html() == "0") {
        if (window.location.href.toLowerCase().indexOf('wholesale') > -1) {
            window.location.href = "addwholesalegame.aspx";
        }
        else { window.location.href = gametypeid == 2 ? "addsurveygame.aspx" : gametypeid == 'commonprimaryprize' ? "addfirstprizegame.aspx" : "addgame.aspx"; }
    }
    else {       
        if (parseInt($('.lblmarketinglimitcount').html()) <= parseInt($('.lbltotalactivecount').html())) {
            $('#confirmation-modal-add').modal('show');
        }
        else {
            if (window.location.href.toLowerCase().indexOf('wholesale') > -1) {
                window.location.href = "addwholesalegame.aspx";
            }
            else { window.location.href = gametypeid==2 ?"addsurveygame.aspx" :gametypeid == 'commonprimaryprize' ? "addfirstprizegame.aspx":"addgame.aspx"; }
        }
    }
}

function GetSurveyQandA(sid, sname) {        
    $('.dsqsurveyname').html(sname);
    $('.dsquestionsgv').attr('data-sid', sid).attr('data-sname', sname);
    //$('.dsanseditsurvey').attr('href', editurl+"#surveyId");
    gl.ajaxreqloader(globalWebSiteApiurl + "GetAdminSurveybyId", "get", { sid: sid }, function (response) {
        //var resp = $.parseJSON(response.d);
        $.each(response, function (i, item) {
            var cnt = $('.divsquestionsdummy').clone();
            $('.dsqquestionname', cnt).html(item.question); 
            var respans = $.parseJSON(JSON.stringify(item.answers));                
            $.each(respans, function (j, itemans) {                 
                $('.dsanslist',cnt).append('<li class="answer"><p class="form-control-static">' + itemans.Answer + '</p></li>');
            });        
            $('.dsquestionsgv').append(cnt.html());
        });
    }, $('#lblsqppupres'), 'Loading Questions...', '', 'getting Questions.');
}

function GetSurveyQandAedit(sid) {
    $('.dsqadivedit').attr('data-sid', sid);
    var cnt = $('.divsqaeditdummy').clone();
    var qcount = 0;
    var acount = 0;
    var count = 0;
    if (sid == '0') {
        SetSurveyQandAEmptyedit(cnt);            
    }
    else {
        gl.ajaxreqloader(globalWebSiteApiurl + "GetAdminSurveybyId", "get", { sid: sid }, function (response) {
            //console.log(response);
            if (response == '') {
                SetSurveyQandAEmptyedit(cnt);                  
            }
            else {
                $('#surveyname').val(response.SurveyDetails.SurveyName);
                $('#SurveyCode').val(response.SurveyDetails.SurveyCode);
                $('#SmsCode').val(response.SurveyDetails.SmsCode);
                $('#ShortDescription').val(response.SurveyDetails.ShortDescription);
                $('#StartDate').val(response.SurveyDetails.StartDatestring);
                $('#EndDate').val(response.SurveyDetails.EndDatestring);
                $("#imgpreview").attr('src', response.SurveyDetails.Surveyimagepath);
                $('#Surveyimage').val(response.SurveyDetails.Surveyimage);
                $('#Status').val(response.SurveyDetails.Status);
                $("#IsAgeRequire").prop("checked", response.SurveyDetails.IsAgeRequire || 0);
                $("#FirstPrizeImage").val(response.SurveyDetails.FirstPrizeImage);
                $("#SecondPrizeImage").val(response.SurveyDetails.SecondPrizeImage);
                $("#FirstPrize").attr("src", response.SurveyDetails.GamePrizes[0].PrizeImage);
                $("#FirstPrizeText").val(response.SurveyDetails.FirstPrizeText);
                $("#SecondPrize").attr("src", response.SurveyDetails.GamePrizes[1].PrizeImage);
                $("#SecondPrizeText").val(response.SurveyDetails.SecondPrizeText);
                $("#PrizeExpiryDate").val(response.SurveyDetails.PrizeExpiryDate);
                $("#Conditions").val(response.SurveyDetails.Conditions);
                $("#IsComplimentary").prop("checked", response.SurveyDetails.IsComplimentary || 0);
                $('#AgeCondition').val(response.SurveyDetails.AgeCondition);
                $('#FirstPrizeCount').val(response.SurveyDetails.FirstPrizeCount);
                $('#SecondPrizeCount').val(response.SurveyDetails.SecondPrizeCount);
                $('#OnceIn').val(response.SurveyDetails.OnceIn);
                $('#TotalPlayed').val(response.SurveyDetails.TotalPlayed);
                $('#FirstPrizesLeft').html(response.SurveyDetails.FirstPrizesLeft);
                $('#SecondPrizesLeft').html(response.SurveyDetails.SecondPrizesLeft);
                if ($("#TotalPlayed").val() != 0) {
                    $('.game').prop('readonly', true);
                    $('.gamed').prop('disabled', true);
                    alert("Game is in play mode, Please don't make changes.");
                }
                $('#PhysicalPrize1').prop("checked", response.SurveyDetails.PhysicalPrize1 || 0);
                $('#PhysicalPrize2').prop("checked", response.SurveyDetails.PhysicalPrize2 || 0);
                Attr1();
                Attr2();
                var attri2 = response.SurveyDetails.Attributes1;
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
                var attri = response.SurveyDetails.Attributes2;
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
                if (response.SurveyDetails.QrcodePath == "") {
                    $('#qrcode').append('<div class="form-group vb">' +
                              '<div class="col-sm-12 controls pl-0 text-center">' +
                              ' <img id="imgqrcode" src="http://localhost:11328/Content/images/noqr.png" class="qrimg" /><br />' +
                              '</div>' +
                              '</div>');
                } else {
                    var jpgdownload = 'QRdownload("pdf","' + response.SurveyDetails.QRCode + '","' + response.SurveyDetails.QrcodePath + '","' + response.SurveyDetails.SurveyName + '","' + response.SurveyDetails.ShortDescription + '")';
                    $('#qrcode').append('<div class="form-group vb">' +
                             '<div class="col-sm-12 controls pl-0 text-center">' +
                             ' <img id="imgqrcode" src="' + response.SurveyDetails.QrcodePath + '" class="qrimg" /><br />' +
                             "<button class='btn btn-primary searchrowbtn mt-10 text-center' type='button' onclick='" + jpgdownload + "')'><i class='icon-ok'></i>Download QR Code</button>" +
                             '</div>' +
                             '</div>');
                }
                $.each(response.question, function (i, item) {
                    cnt = $('.divsqaeditdummy').clone();
                    qcount++;
                    if (i == 0) {
                        $('.survey-question > a:first', cnt).remove();
                      //  $('.qpreviewq', previewcnt).attr('id', 'txtq' + qcount.toString()).attr('data-qid', item.SurveyquetionId).html('Q) ' + item.Question);                        
                    }                   
                    $('.survey-question', cnt).addClass('question-' + qcount.toString()).attr('data-qid', item.questionId);
                    $('.txtq', cnt).attr('id', 'txtq' + qcount.toString()).attr('data-qid', item.questionId).text(item.Question);
                    $('.IsTextField', cnt).attr('id', 'IsTextField' + qcount.toString()).val(item.IsTextField);
                    $('.dsanslistedit', cnt).addClass('answer-fieldlist-' + qcount.toString());
                    $('.answers > button', cnt).addClass('addanswer-btn-' + qcount.toString()).attr('data-target', 'answer-fieldlist-' + qcount.toString());
                    //if (item.IsTextField == 1) {
                    //    //alert('#IsTextField' + qcount.toString());
                    //    $('#IsTextField' + qcount.toString()).attr("checked", "checked");
                    //}
                    //$('.IsTextField').each(function (e) {
                    //    if ($(this).val() == 1) {
                    //        $(this).attr("checked", "checked");
                    //    }
                    //});
                    $.each(item.answers, function (j, itemans) {
                        acount++;
                        if (j == 0) {                                
                            $('.dsanslistedit', cnt).append('<li class="answer col-xs-12 px-2">'+
                                '<div class="col-xs-12 px-0 mb-3">'+
                                '<div class="input-group">'+
                                '<input id="txta' + acount.toString() + '" type="text" class="txta width-70 width-110 myfilds mytxtfilds" name="email" placeholder="Enter your answer here" value="' + itemans.Answer + '" data-aid=' + itemans.SurveyanswerId + '>' +
                                '<span class="input-group-addon main-input nobg"></span></div></div>' +
                                '</li>');                           
                        }
                        else {
                            $('.dsanslistedit', cnt).append(
                                '<li class="answer col-xs-12 px-2">' +
                                '<div class="col-xs-12 px-0 mb-3">' +
                                '<div class="input-group">' +                               
                                '<input id="txta' + acount.toString() + '" type="text" class="txta width-70 width-110 myfilds mytxtfilds" name="email" placeholder="Enter your answer here" value="' + itemans.Answer + '" data-aid=' + itemans.SurveyanswerId + '>' +
                                '<span class="input-group-addon main-input"><i class="fa fa-times-circle" class="remove_field" onclick="RemoveSurveyAnswerfield(this)"></i></span></div></div>' +
                                '</li>');                           
                        }
                    });
                    $('.dsqadivedit').append(cnt.html());                   
                });
                $.each(response.question, function (i, item) {
                    count++;
                    if (item.IsTextField == 1) {
                        //console.log(count);
                        $('#IsTextField' + count.toString()).attr("checked", "checked");
                    }
                });
               //$('.qpreview').append(previewcnt.html());
                //SetSurveyQuestionReview();
            }
        }, '', 'Loading Questions...', '', 'getting Questions.');
    }
}

function surveygridpreview(id, displayqid) {
    displayqid = displayqid == undefined ? '1' : displayqid;
    $('.qpreview').html('');
    //$('.qpreviewnxt').show();
    if (displayqid == 1 || displayqid > 1) {
        $('.qpreviewnxt').show();
    }
    gl.ajaxreqloader(websiteurl + "api/GetAdminSurveybyId", "get", { sid: id }, function (response) {
        //console.log(response);
        if (response != null) {
           
            
            //var pcnt = '';
            $('.qrcode').attr('src', response.SurveyDetails.QrcodePath);
            $('.surveyurl').html(response.SurveyDetails.surveyurlpath);
            $('.survey-image').attr('src', response.SurveyDetails.Surveyimagepath);
            $('.smscode').html(response.SurveyDetails.SmsCode);
            $.each(response.question, function (i, item) {
                var pcnt = $('.qpreviewdummy').clone();
                $('.qpreviewdiv', pcnt).attr('displayqid', (parseInt(i) + 1).toString());
                //console.log(item);
                $('.qpreview').attr('tqcount',i+1);
                if (displayqid == i + 1) {
                    $('.qpreviewdiv', pcnt).removeAttr('style');
                }
                $('.qpreviewq', pcnt).attr('id', 'txtq' + item.questionId).attr('data-qid', item.questionId).html('Q) '+ item.Question);
                $.each(item.answers, function (j, itemans) {
                   
                    //console.log(itemans);
                    $('.qpreviewa', pcnt).append('<div class="paborder">' +
                                           '<div class="checkbox custom-checkbox my-0">' +
                                           '<label class="pl-2 prussian-txt"><input type="checkbox" id="' + itemans.SurveyanswerId+ '"  value="' + itemans.SurveyanswerId + '" data-aid=' + itemans.SurveyanswerId + '>' + itemans.Answer + '<span class="checkmarkone"></span></label>' +
                                           '</div></div>');
                });
                $('.qpreview').append(pcnt.html());
            });
          
        }
        });
}

function SurveygridPreviewNextPrev(action) {

    $('.qpreviewdiv').hide();
    var displayqid = $('.qpreview').attr('curqid');
    switch (action) {
        case '+1': displayqid = (parseInt(displayqid) + 1).toString(); break;
        case '-1': displayqid = (parseInt(displayqid) - 1).toString(); break;
        default: displayqid = 1; break;
    }

    $('.qpreviewdiv[displayqid=' + displayqid + ']').removeAttr('style');
    $('.qpreview').attr('curqid', displayqid);
    //if ($('.qpreviewnxt').html() == 'FINISH' && ($('.qpreviewcompleted').css('display')=='none')) {
    if (displayqid == (parseInt($('.qpreview').attr('tqcount'))+1).toString()) {
        $('.qpreview').hide();
        $('.qpreviewcompleted').show();
        $('.qpreviewnxt').hide();
    }
    else {
        $('.qpreviewcompleted').hide();
        $('.qpreview').show();
        $('.qpreviewnxt').show();
       
     
        if (displayqid == '1') {
            $('.qpreviewback').hide();
        }
        else { $('.qpreviewback').show(); }

        if (displayqid == $('.qpreview').attr('tqcount')) {
            $('.qpreviewnxt').html('FINISH');
        }
        else { $('.qpreviewnxt').html('NEXT'); }
    }

}

function DeleteSurvey(sid) {
    var ans = confirm("Are you sure you want to Delete this Survey?");
    if (ans) {
        $.ajax({
            url: globalWebSiteApiurl + "DeleteSurvey?sid=" + sid,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                SetViewSurveyPage();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function SetSurveyQandAEmptyedit(cnt) {
    $('.survey-question', cnt).addClass('question-1');
    $('.survey-question > a:first', cnt).remove();
    $('.txtq', cnt).attr('id', 'txtq1');
    $('#IsTextField', cnt).attr('id', 'IsTextField1');
    $('.dsanslistedit', cnt).addClass('answer-fieldlist-1');
    //$('.dsanslistedit', cnt).append('<li class="answer"><input type="text" id="txta1" runat="server" class="form-control txta" data-aid="0"></li>');

    $('.dsanslistedit', cnt).append('<li class="answer col-xs-12 px-2">' +
                                '<div class="col-xs-12 px-0 mb-3">' +
                                '<div class="input-group">' +
                                '<input id="txta1" type="text" class="txta width-70 width-110 myfilds mytxtfilds" name="email" placeholder="Enter your answer here" data-aid="0">' +
                                '<span class="input-group-addon main-input nobg"></span></div></div>' +
                                '</li>');
    $('.answers > button', cnt).addClass('addanswer-btn-1').attr('data-target', 'answer-fieldlist-1');
    $('.dsqadivedit').append(cnt.html());
    $('.qpreview').html('No Questions.');
    $('.qpreviewnxtdiv').hide();
    //m.SetSurveyQuestionReview();   
}


function AddorRemoveSurveyQuestionfield(){        
    var wrapper = $("." + $('.addquestion-btn').data('target')); //Fields wrapper
    var x = $('.dsqadivedit').children().length;       
    event.preventDefault();
    x++;
    //$(wrapper).append('<div class="survey-question question-' + x + ' well" style="display:none;"><a href="void(0)" class="remove_field">Remove <i class="fa fa-close"></i></a><div class="form-group"><label>Question </label><textarea name="" id="txtq' + x.toString() + '" runat="server" id="" cols="30" rows="10" class="form-control question-' + x.toString() + ' txtq" data-qid="0"></textarea></div><div class="answers"><label>Answer/s</label><ol class="answer-fieldlist-' + x + ' row dsanslistedit"><li class="answer"><input type="text" id="txta1" runat="server" class="form-control txta" data-aid="0"></li></ol><button type="button" class="addfield-btn' + x + '" data-target="answer-fieldlist-' + x + '" onclick="m.AddorRemoveSurveyAnswerfield(this)">+ Add answer</button></div></div>'); //add input box
    $(wrapper).append('<div class="my-5 survey-question question-' + x + '" style="display:none;" data-qid="0">' +
                       '<a href="void(0)" style="color: #ea0808" class="remove_field" onclick="RemoveSurveyQuestionfield(this)"><i class="fa fa-times-circle pull-right"></i></a>' +
                                            '<div class="col-xs-6 my-2 px-2">'+
                                                '<textarea class="form-control main-input question-' + x.toString() + ' txtq" data-qid="0" rows="12" id="txtq' + x.toString() + '" placeholder="Enter your question here"></textarea>' + ' <div class="form-group">' + ' <label class="col-sm-4 control-label">Allow UserText :<span class="vd_red"></span></label><div class="col-sm-5 controls"><input type="checkbox" name="IsTextField" class="IsTextField" id="IsTextField' + x.toString() + '" /></div>' + '</div>' +
                                            '</div>'+
                                            '<div class="col-xs-6 mb-3 answers">'+
                                                '<ol class="answer-fieldlist-' + x + ' row dsanslistedit">'+
                                                '<li class="answer col-xs-12 px-2">' +
 '<div class="col-xs-12 px-0 mb-3">'+
                            '<div class="input-group">'+
                            '<input id="txta1" type="text" class="txta width-70 width-110 myfilds mytxtfilds" name="email" placeholder="Enter your answer here" data-aid="0">' +
                            '<span class="input-group-addon main-input nobg"></span></div></div>' +                             
    '</li>'+
                                               '</ol>'+
                                                '<button type="button" class="adda addfield-btn' + x + '" data-target="answer-fieldlist-' + x + '" onclick="AddorRemoveSurveyAnswerfield(this)"><i class="fa fa-plus fa-1x"></i> Add answer here</button>' +
                                            '</div>'); //add input box
    $(wrapper).find('.survey-question.question-' + x).slideDown(200);        
    SetSurveyQuestionReview($('.qpreview').attr('curqid'));

    $(wrapper).on('click', '.remove_field', function (e) { //user click on remove text
        e.preventDefault();
        $(this).parent("div").slideUp(200, function () {
            $(this).remove();//x--;
            SetSurveyQuestionReview($('.qpreview').attr('curqid'));
        });
    });
}

function AddorRemoveSurveyAnswerfield(btnctrl) {
    var wrapacount = $(btnctrl).prev('.dsanslistedit').children().length;       
    var wrapansol = btnctrl;       
    var wrapper2 = $("." + $(wrapansol).data('target')); //Fields wrapper2        
    var add_button2 = $(wrapansol); //Add button ID        
    event.preventDefault();
    wrapacount++;        
    //$(wrapper2).append('<li class="answer" style="display:none;"><input type="text" id="txta' + wrapacount.toString() + '" runat="server" class="form-control txta" data-aid="0"/><a href="void(0)" class="remove_field"><i class="fa fa-close"></i></a></li>'); //add input box
    $(wrapper2).append(
         '<li style="display:none;" class="answer col-xs-12 px-2">' +
                            '<div class="col-xs-12 px-0 mb-3">' +
                            '<div class="input-group">' +
                            '<input id="txta' + wrapacount.toString() + '" type="text" class="txta width-70 width-110 myfilds mytxtfilds" name="email" placeholder="Enter your answer here" data-aid="0">' +
                            '<span class="input-group-addon main-input"><i class="fa fa-times-circle" class="remove_field" onclick="RemoveSurveyAnswerfield(this)"></i></span></div></div>' +
                            '</li>'); //add input box
    $(wrapper2).find('.answer:last').slideDown(200, function () {
        SetSurveyQuestionReview($('.qpreview').attr('curqid'));
    });        

    $(wrapper2).on('click', '.remove_field', function (e) { //user click on remove text
        e.preventDefault();          
        $(this).parent("li").slideUp(200, function () {
            $(this).remove();//x--;
            SetSurveyQuestionReview($('.qpreview').attr('curqid'));
        });
          
    });      
}

function RemoveSurveyAnswerfield(ctrl) {
    var aid = $(ctrl).parents('span').prev("input").attr('data-aid');
    if (aid == 0) {
        $(ctrl).parents("li").slideUp(200, function () {
            $(this).remove();//x--;
        });
    } else {
        event.preventDefault();
        $.ajax({
            url: globalWebSiteApiurl + "DeleteAnswers?aid=" + aid,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $(ctrl).parents("li").slideUp(200, function () {
                    $(this).remove();//x--;
                    SetSurveyQuestionReview($('.qpreview').attr('curqid'));
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    //$('.txtremovedataa').val($('.txtremovedataa').val() + ',' + $(ctrl).parents('span').prev("input").attr('data-aid'));      
}

function RemoveSurveyQuestionfield(ctrl) {
    var qid = $(ctrl).parent().attr('data-qid');
    //alert($(ctrl).parent().attr('data-qid'));
    event.preventDefault();
    if (qid != 0) {
        $.ajax({
            url: globalWebSiteApiurl + "DeleteQuestions?qid=" + qid,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $(ctrl).parent("div").slideUp(200, function () {
                    $(this).remove();//x--;
                    SetSurveyQuestionReview($('.qpreview').attr('curqid'));
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    } else {
        $(ctrl).parent("div").slideUp(200, function () {
            $(this).remove();//x--;
            SetSurveyQuestionReview($('.qpreview').attr('curqid'));
        });
    }
    // $('.txtremovedataq').val($('.txtremovedataq').val() + ',' + $(ctrl).parent().attr('data-qid'));             
}

function SetSurveyReview(id) {
    if (id != 0 || id==-1) {
        $('[data-ccoutput]').trigger('keyup');
        $('select[data-ccoutputddl]').trigger('change');
        m.setsurveyupdatepanelreview();
    }
    $('.open-closed-output').html($('ddlisclosed option:selected').text());
}

function setsurveyupdatepanelreview() {
    if ($('ddlDeals').val() == "0") {
        $('.attached-promo-output').html('<span class="text-danger small" id="reviewattachedpromo" runat="server"><b>No Promotion Attached</b></span>');
    }
    else {
        $('.attached-promo-output').text($('ddlDeals option:selected').text());
    }
    $('tempimagereview').attr('style', $('.imgpreview').attr('style'));
}

function SurveyPreviewNextPrev(action) {        
    switch (action) {
        case '+1': SetSurveyQuestionReview(parseInt($('.qpreview').attr('curqid')) + 1); break;
        case '-1': SetSurveyQuestionReview(parseInt($('.qpreview').attr('curqid')) - 1); break;
        default: SetSurveyQuestionReview('1'); break;
    }
    //  SetSurveyQuestionReview();
}

function SetSurveyQuestionReview(showqid) {
    if (($('#txtq1').val().length == 0)) {
        //if (($('#txtq1').val().length == 0) || ($('#txta1').val().length == 0)) {
        $('.qpreview').html('No Questions.');
        $('.qpreviewnxtdiv').hide();
    }
    else {
        $('.qpreviewnxtdiv').show();
        showqid = showqid == undefined ? '1' : showqid;
        $('.qpreview').attr('curqid', showqid);
        $('.qpreview').html('');
        var divedit = $('.dsqadivedit');
        var qcount = $('.dsqadivedit > div').length;
        if (qcount == 1 || qcount > 1) {
            $('.qpreviewnxt').show();
        }
        if (showqid == qcount) {
            $('.qpreviewnxt').html('FINISH');
        }
        else { $('.qpreviewnxt').html('NEXT'); }
        $('.dsqadivedit > div').each(function (i) {
            var qdivedit = this;
            var qid = $(qdivedit).attr('class');
            qid = qid.substring(qid.lastIndexOf('-') + 1).toString();
            var q = $(qdivedit).children('div').children('textarea').val();
            if (showqid == qcount + 1) {
                $('.qpreview').html('Survey completed successfully.');
                $('.qpreviewnxt').hide();
            }
            else { $('.qpreviewnxt').show(); }
            if (showqid == '1') {
                $('.qpreviewback').hide();
            }
            else { $('.qpreviewback').show(); }
            var pcnt = $('.qpreviewdummy').clone();

            if (showqid == qid) {
                $('.qpreviewdiv', pcnt).removeAttr('style');
            }

            //else {
            //    pcnt = $(pcnt).children('.qpreviewdiv').attr('style','display:none');
            //}
            $('.qpreviewq', pcnt).attr('id', 'txtq' + qid).attr('data-qid', qid).html('Q) ' + q);
            $('.answers > ol > li', qdivedit).each(function (j) {
                $('.qpreviewa', pcnt).append('<div class="paborder">' +
                                        '<div class="checkbox custom-checkbox my-0">' +
                                        '<label class="pl-2 prussian-txt"><input type="checkbox" id="' + $(this).find('.txta').attr('id') + '"  value="' + $(this).find('.txta').val() + '" data-aid=' + $(this).find('.txta').attr('data-aid') + '>' + $(this).find('.txta').val() + '<span class="checkmarkone"></span></label>' +
                                        '</div></div>');
            });
            $('.qpreview').append(pcnt.html());
        });
    }
}

function ClearSurveyQandAdetails() {
    $('.dsanslist').html('');
    $('.dsqsurveyname').html('');
    $('.dsquestionsgv').html('');
    $('.dsanseditsurvey').attr('href','#');
}

function AddSurveyQuestions(sid) {
    //alert(sid);
    var data = [];
    var qcount = 0;
    $('.dsqadivedit > div').each(function () {            
        var qcontainer = this;
        qcount++;
        //console.log(qcount);
        //alert(x);
        //alert('#IsTextField' + qcount.toString(), qcontainer);
        //if ($(('#IsTextField' + x.toString()), qcontainer).is(':checked'))
        if ($('#IsTextField' + qcount.toString(), qcontainer).is(':checked'))
        {
            //alert('l');
           var textfield = 1;
        } else {
            var textfield = 0;
        }
        if ($('.txtq', qcontainer).val() != '') {
            data.push('[' + $('.txtq', qcontainer).attr('data-qid') + ':' + $('.txtq', qcontainer).val() + ';' + textfield + '&^');
            var alength = $('.answers > ol > li', qcontainer).length-1;               
            $('.answers > ol > li', qcontainer).each(function (index) {                   
                if ($('.txta', this).val() != '') {
                    if (alength == 0) {
                        data.push('{' + $('.txta', this).attr('data-aid') + ':' + $('.txta', this).val() + '}&^]');
                    }                      
                    else if (index == alength) {                           
                        data.push('{' + $('.txta', this).attr('data-aid') + ':' + $('.txta', this).val() + '}&^]');
                    }
                    else {
                        data.push('{' + $('.txta', this).attr('data-aid') + ':' + $('.txta', this).val() + '}');
                    }
                }
            });
        }
    });
    //'|' + $('.IsTextField', this).is(':checked') ? 1 : 0 +
    $('.txtdataqa').val(data.toString());        
    // console.log(data.toString());
    //m.ajaxreq("AddSurveyQuestions", { id: sid, qdetails: data.toString() }, function (response) {

    //}, '', '', '', '');
}

//$('.checkexist').focusout(function () {
//    var sid = $('#SurveyId').val();
//    var sname = $('#surveyname').val();
//    var smscode = $('#smscode').val();
//    let error = [];

//    gl.ajaxreqloader(websiteurl + "api/CheckSurveyExist", "get", { sid: sid, sname: sname, smscode: smscode }, function (response) {
//        if (response.StatusCode == -1) {
//            error.push(response.StatusMessage);
//            $('#error').addClass('show');
//            $('#error').removeClass('hide');
//            var valerror = "<ul>";
//            $(error).each(function (i, e) {
//                valerror += "<li>" + e + "</li>";
//            });
//            valerror += "</ul>";
//            document.getElementById("error").innerHTML = valerror;
//            $('#error').focus();
//            return false;
//        }
//        else if (response.StatusCode == -2) {
//            error.push(response.StatusMessage);
//            $('#error').addClass('show');
//            $('#error').removeClass('hide');
//            var valerror = "<ul>";
//            $(error).each(function (i, e) {
//                valerror += "<li>" + e + "</li>";
//            });
//            valerror += "</ul>";
//            document.getElementById("error").innerHTML = valerror;
//            $('#error').focus();
//            return false;
//        }
//        else {
//            document.getElementById("error").innerHTML = '';
//            $('#error').addClass('hide');
//            $('#error').removeClass('show');
//            return true;
//        }
//    });

//});


