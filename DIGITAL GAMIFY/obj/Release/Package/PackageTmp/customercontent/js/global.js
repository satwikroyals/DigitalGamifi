//var apiurl = 'http://localhost:1223/api/';
//var apiurl = 'http://151.106.38.222:92/api/';
//var apiurl = window.location.protocol + '//' + window.location.hostname + '/api/';
//var apiurl = location.protocol + '://' + location.hostname + '/api/';
$(document).ready(function () {
    gettempimg();
    $("input").attr("autocomplete", "off");
    //$(".sidebar-dropdown > a").click(function () {
    //    $(".sidebar-submenu").slideUp(200);
    //    if (
    //      $(this)
    //        .parent()
    //        .hasClass("active")
    //    ) {
    //        $(".sidebar-dropdown").removeClass("active");
    //        $(this)
    //          .parent()
    //          .removeClass("active");
    //    } else {
    //        $(".sidebar-dropdown").removeClass("active");
    //        $(this)
    //          .next(".sidebar-submenu")
    //          .slideDown(200);
    //        $(this)
    //          .parent()
    //          .addClass("active");
    //    }
    //});
    //$("#close-sidebar").click(function () {
    //    $(".page-wrapper").removeClass("toggled");
    //});
    //$("#show-sidebar").click(function () {
    //    $(".page-wrapper").addClass("toggled");
    //});
});

$('#Primarycolor').on('change', function () {
    $('#txtprimary').val(this.value);
});
$('#txtprimary').on('change', function () {
    $('#Primarycolor').val(this.value);
});
$('#Secondarycolor').on('change', function () {
    $('#txtsecondary').val(this.value);
});
$('#txtsecondary').on('change', function () {
    $('#Secondarycolor').val(this.value);
});
$('#Thirdcolor').on('change', function () {
    $('#txtthird').val(this.value);
});
$('#txtthird').on('change', function () {
    $('#Thirdcolor').val(this.value);
});
$('#Textcolor').on('change', function () {
    $('#Txtcolor').val(this.value);
});
$('#Txtcolor').on('change', function () {
    $('#Textcolor').val(this.value);
});

function gettempimg() {
    $("#Image").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
    $("#background").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
    $("#banner").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
    $("#Menu").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#tempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#backtempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#bannertempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#menutempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function readMultiURL(input, tgresultdiv) {
    var imgs = [];
    $(tgresultdiv).html('');
    $(input.files).each(function () {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(tgresultdiv).append('<img src="' + e.target.result + '" class="fileupimgprev" style="padding:1px;border:1px #000;">');
        }
        reader.readAsDataURL(this);
    });

}
function Getgroupsddl() {
    $.get(apiurl + "getgroupsdropdown", { orgid: $("#orgid").val() }, function (response) {
        $(response).each(function (i, data) {
            //console.log(data);
            $("#grpid").append($('<option/>', { value: data.grpid }).html(data.GroupName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#grpid").attr('bindval');
    $("#grpid").val(defvalue);
}

function GetCountryddl() {
    gl.ajaxreq(apiurl + "GetCountries", "get", {}, function (response) {
        $(response).each(function (i, data) {
            $("#CountryId").append($('<option/>', { value: data.CountryId }).html(data.CountryName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#CountryId").attr('bindval');
    $("#CountryId").val(defvalue);
}

function GetStateddl() {
    var cnid = $('#CountryId').val();
    $("#StateId").empty();
    $("#StateId").append("<option value='0'>State</option>");
    gl.ajaxreq(apiurl + "GetStatebyCountryId", "get", { cnid: cnid }, function (response) {
        $.each(response, function (index, row) {
            $("#StateId").append("<option value='" + row.StateId + "'>" + row.State + "</option>")
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#StateId").attr('bindval');
    $("#StateId").val(defvalue);
}

function GetCityddl() {
    var cnid = $('#CountryId').val();
    $("#CityId").empty();
    $("#CityId").append("<option value='0'>Select City</option>");
    gl.ajaxreq(apiurl + "GetCitybyCountryId", "get", { cnid: cnid }, function (response) {
        $.each(response, function (index, row) {
            $("#CityId").append("<option value='" + row.CityId + "'>" + row.CityName + "</option>")
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#CityId").attr('bindval');
    $("#CityId").val(defvalue);
}

function Getgroupsddl() {
    $.get(apiurl + "getgroupsdropdown", { orgid: $("#orgid").val() }, function (response) {
        //$("#ParticipantId").append("<option value=0></option>");
        $(response).each(function (i, data) {
            //console.log(data);
            $("#grpid").append($('<option/>', { value: data.grpid }).html(data.GroupName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#grpid").attr('bindval');
    $("#grpid").val(defvalue);
}

function GetOrganizationsddl() {
    gl.ajaxreq(apiurl + "getorganizationddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#orgid").append($('<option/>', { value: data.OrgId }).html(data.OrgName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#orgid").attr('bindval');
    $("#orgid").val(defvalue);
    $("#orgid").trigger('change');
}

function GetGroupTypeddl() {
    gl.ajaxreq(apiurl + "GetGroupTypesddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#grouptypeid").append($('<option/>', { value: data.GroupTypeId }).html(data.GroupType));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#grouptypeid").attr('bindval');
    $("#grouptypeid").val(defvalue);
}

function GetPartnerTypeddl() {
    gl.ajaxreq(apiurl + "GetPartnerTypesddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#PartnerTypeId").append($('<option/>', { value: data.PartnerTypeId }).html(data.PartnerType));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#PartnerTypeId").attr('bindval');
    $("#PartnerTypeId").val(defvalue);
}
function Partnersddl() {
    var orgid = $('#orgid').val();
    gl.ajaxreq(apiurl + "GetddlPartners", "get", { orgid: orgid }, function (response) {

        $(response).each(function (i, data) {
            $("#PartnerId").append($('<option/>', { value: data.PartnerId }).html(data.PartnerName));
        });
    }, '', '', '', '', false, false);
    var defvalue =String($("#PartnerId").attr('bindvalue')).split(',');
    $("#PartnerId").val(defvalue);
}
function GetLocationddl() {
    var pid = $('#pid').val();
    gl.ajaxreq(apiurl + "GetddlBusinessLocations", "get", { pid: pid }, function (response) {

        $(response).each(function (i, data) {
            $("#ddLocations").append($('<option/>', { value: data.LocationId }).html(data.LocationName));
        });
    }, '', '', '', '', false, false);
    var defvalue = String($("#ddLocations").attr('bindvalue')).split(',');
    $("#ddLocations").val(defvalue);
}

function GetOrganizationTypeddl() {
    gl.ajaxreq(apiurl + "getorganizationtypes", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#orgtypeid").append($('<option/>', { value: data.OrgTypeId }).html(data.OrgType));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#orgtypeid").attr('bindval');
    $("#orgtypeid").val(defvalue);
}


function GetSearchTagddl() {
    gl.ajaxreq(apiurl + "getsearchtagddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#searchtagids").append($('<option/>', { value: data.SearchTagIds }).html(data.SearchTag));
        });
    }, '', '', '', '', false, false);
    var defvalue = String($("#searchtagids").attr('bindval')).split(',');
    $("#searchtagids").val(defvalue);
}

function GetFrequencyddl() {
    gl.ajaxreq(apiurl + "GetFrequencyddl", "get", {}, function (response) {
        $(response).each(function (i, data) {
            $("#gamefrequency").append($('<option/>', { value: data.GameFrequencyId }).html(data.Frequency));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#gamefrequency").attr('bindvalue');
    $("#gamefrequency").val(defvalue);
}

$(function () {
    if (window.jQuery().datetimepicker) {
        $('.datetime').datetimepicker({
            format: 'DD-MMM-YYYY hh:mm a',
            widgetPositioning: {
                horizontal: 'right',
                vertical: 'bottom'
            },
            // sideBySide: true,
            icons: {
                time: 'fa fa-clock',
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-check',
                clear: 'fa fa-trash',
                close: 'fa fa-times'
            }
        });

        $('.date').datetimepicker({
            format: 'DD-MMM-YYYY',
            widgetPositioning: {
                horizontal: 'right',
                vertical: 'bottom'
            },
            // sideBySide: true,
            icons: {
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-check',
                clear: 'fa fa-trash',
                close: 'fa fa-times'
            }
        });
    }
});

// Check html5 support
function IsHtml5Compatible() {
    var test_canvas = document.createElement("canvas");
    return test_canvas.getContext ? true : false;

}

function ExportToexcel(elementid, pagetitle) {
    $('#' + elementid + ' .tbldata  .hiddenprint').remove();
    var fname = pagetitle + '.xls';
    var tab_text = "<table border='1px'>";
    var textRange; var j = 0;
    var tab = document.getElementById('dataTable');
    //  tab = tab.getElementById('dataTable')[0];
    //alert(tab.rows.length);
    for (j = 0 ; j < tab.rows.length ; j++) {

        tab_text = tab_text + "<tr>" + tab.rows[j].innerHTML + "</tr>";
    }
    tab_text = tab_text + "</table>";

    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params
    //tab_text = tab_text.replace('class="hiddenprint"', 'style=display:none');
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        dumiframexls.document.open("txt/html", "replace");
        dumiframexls.document.write(tab_text);
        dumiframexls.document.close();
        dumiframexls.focus();
        sa = dumiframexls.document.execCommand("SaveAs", true, fname);
    }
    else {
        var data_type = 'data:application/vnd.ms-excel';
        var table_div = tab_text;
        var table_html = table_div.replace(/ /g, '%20');

        var link = document.getElementById('dumlnkxls');
        link.download = fname;
        link.href = data_type + ', ' + table_html;
        link.click();
    }

}

function setnavigationurl(url) {

    if (IsHtml5Compatible) {
        history.pushState("", "Protech", url);
    }
    else {
        window.location.replace(url);
    }
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function myFunction() {
    var d = new Date();
    var n = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });
    document.getElementById("time").innerHTML = n;
}

// Read a page's GET URL variables and return them as an associative array.


var gl = {
    ajaxreq: function (serviceurl, reqtype, data, OnSuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader) {
        try {
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: 'text json',
                async: isasync,
                beforeSend: function () {
                    // ajaxprocessindicator(resctrl, msg, 1, 'suc');
                },
                complete: function () {
                    // ajaxprocessindicator(resctrl, sucmsg, 0, 'suc');
                },
                success: OnSuccess,
                error: function (jqXHR, exception) {
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) {
            console.log(err.message);// ajaxprocessindicator(resctrl, errmsgprefix + errmsg, 0, 'err');
        }
    },
    ajaxreqloader: function (serviceurl, reqtype, data, OnSuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader, pageloaderdiv, pagecontentdiv, datatype, isloader) {
        try {
            var pageLoader = pageloaderdiv == undefined ? '.loader' : pageloaderdiv;
            var pageContent = pagecontentdiv == undefined ? '.tblcontent' : pagecontentdiv;
            $.ajax({
                url: serviceurl,
                type: reqtype,
                //headers:  //isheader ? { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: datatype == undefined ? 'text json' : datatype,
                async: isasync,
                beforeSend: function () {
                    if (isloader) {
                        $(pageLoader).removeClass('hide');
                        $(pageContent).hide();
                    }
                },
                complete: function () {
                    if (isloader) {
                        $(pageLoader).addClass('hide');
                        $(pageContent).show();
                    }
                },
                success: OnSuccess,
                error: function (jqXHR, exception) {
                    $(pageLoader).addClass('hide');
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) { console.log(err.message); }
    },

    ajaxpartialreq: function (serviceurl, reqtype, data, OnSuccess, isasync, isheader) {
        try {
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: 'html',
                async: isasync,
                beforeSend: function () {
                    // ajaxprocessindicator(resctrl, msg, 1, 'suc');
                },
                complete: function () {
                    // ajaxprocessindicator(resctrl, sucmsg, 0, 'suc');
                },
                success: OnSuccess,
                error: function (jqXHR, exception) {
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) {
            console.log(err.message);// ajaxprocessindicator(resctrl, errmsgprefix + errmsg, 0, 'err');
        }
    },

}


function GetPageLengthArray(reccount) {
    if (reccount <= 100) {
        return [10, 25, 50, 100, -1];
    }
    if (reccount <= 500) {
        return [10, 25, 50, 100, 200, 300, 400, 500, -1];
    }
    else {
        return [10, 25, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, -1];
    }
}
// to set custome pagging  -- reccount:TotalRecord Cound
function setPagging(reccount, pageindex, pagesize) {
    var fromDisplayNumber = 1;
    var toDisplayNumber = 1;
    var numoffpages = 1;
    if ((parseInt(reccount) % parseInt(pagesize)) == 0) {   // number of pages divides with pagesize: ex reccount 5 ,pagesize 2 then num of pages 5/2=2 + 1=3 ;if reccount 4 then 4%2==0 so 4/2=2
        numoffpages = parseInt(reccount / (parseInt(pagesize) == -1 ? parseInt(reccount) : parseInt(pagesize)));
    }
    else {
        numoffpages = parseInt(parseInt(reccount) / parseInt(pagesize)) + 1;
    }

    if (parseInt(numoffpages) < 5) {      // 5-4 --> page index links displayed
        fromDisplayNumber = 1;
        toDisplayNumber = numoffpages;
    }
    else {
        if (parseInt(pageindex) >= parseInt(numoffpages) - 3) {
            fromDisplayNumber = parseInt(numoffpages) - 3;
            toDisplayNumber = numoffpages;
        }
        else {
            fromDisplayNumber = (parseInt(pageindex) > 1) ? (parseInt(pageindex) - 1) : parseInt(pageindex);
            toDisplayNumber = (parseInt(pageindex) > 1) ? (parseInt(pageindex) + 2) : 4;
        }
    }
    // load page size dropdown
    $('#ddlpagesize').empty();
    var pagesizes = GetPageLengthArray(reccount);
    // alert(pagesizes);
    $(pagesizes).each(function () {
        $('#ddlpagesize').append('<option value=' + this + ' ' + (parseInt(this) == parseInt(pagesize) ? 'selected' : '') + '>' + (parseInt(this) == -1 ? 'All' : this) + '</option>');
    });

    loadPagination(numoffpages, pageindex, fromDisplayNumber, toDisplayNumber);
    $('#totalrec').html(reccount);
    $('#showpageinfo').html('Displaying Page ' + pageindex + ' of ' + numoffpages);
}

// to load pagination bar
function loadPagination(numOfPages, pageindex, fromDisplayNumber, toDisplayNumber) {
    // load pagenation ul.
    console.log(fromDisplayNumber);
    console.log(toDisplayNumber);
    console.log(numOfPages);
    console.log(pageindex);
    $('.pagination').html('');
    $('.pagination').append('<li class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == 1 ? 'avoid-clicks' : '') + '><a class="d-paging" href="javascript:;" _id="1"><i class="fa fa-angle-double-left" aria-hidden="true"></i></a></li>');
    $('.pagination').append('<li class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == 1 ? 'avoid-clicks' : '') + '><a class="d-paging" href="javascript:;" _id=' + (parseInt(pageindex) - 1) + '><i class="fa fa-angle-left" aria-hidden="true"></i></a></li>');
    for (var i = fromDisplayNumber; i <= toDisplayNumber; i++) {
        if (i == pageindex) {
            $('.pagination').append('<li class="active"><a href="#" _id=' + i + '>' + i + '</a></li>');

        }
        else {
            $('.pagination').append('<li><a class="d-paging" href="#" _id=' + i + '>' + i + '</a></li>');
        }
    }
    $('.pagination').append('<li class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == parseInt(numOfPages) ? 'avoid-clicks' : '') + '><a class="d-paging" href="#" _id=' + (parseInt(pageindex) + 1) + '><i class="fa fa-angle-right" aria-hidden="true"></i></a></li>');
    $('.pagination').append('<li class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == parseInt(numOfPages) ? 'avoid-clicks' : '') + '><a class="d-paging" href="#" _id=' + parseInt(numOfPages) + '><i class="fa fa-angle-double-right" aria-hidden="true"></i></a></li>');

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


                error.push('please select File');
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
//preview image
function readURL(input, tgresult) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(tgresult).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
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


function fileuploadimgpreview(inputfile, prvbox) {
    $.uploadPreview({
        input_field: inputfile,            // Default: .image-upload
        preview_box: prvbox,             // Default: .image-preview
        label_field: $(prvbox).children('label'),    // Default: .image-label
        label_default: "",   // Default: Choose File
        label_selected: "",  // Default: Change File
        no_label: false                 // Default: false
    }
    );

    $(prvbox).children('label').css("background-color", "transparent");
    var ccoutputimg = $(inputfile).attr('data-ccoutput');
    if (".background-image" != null) {
        $("#labelimg").hide();
    }

    if (ccoutputimg != undefined) {

        ccoutputprivewimage(inputfile, '.' + ccoutputimg);
    }
}
