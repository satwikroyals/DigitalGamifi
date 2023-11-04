// Information
// -----------------------------------------------------------------
// FileName:       global.js
// Description:    This file is used to Admin Layout menu styles scriping
// Created Date:   19th june, 2017
// -----------------------------------------------------------------

$(window).load(function () {
    var currpageurl = window.location.href.toLowerCase();
  //  $('[data-action^="expand-all"]').trigger('click');

    //set Menu links active
    var menus = currpageurl.split('/').reverse();
    liactmenu = ".limenu" + menus[1];    
    lnkact = "#lnk" + menus[1] + menus[0].split('?')[0];   
    $(liactmenu+' a[data-action^="click-trigger"]').trigger('click');
    $(lnkact).addClass("cmenu-active");

    //Minmise menu on page load Trips page.
    if (currpageurl.indexOf('reports/trips') > -1) {
        $('.limenureports a').removeClass('open');
        $('.limenureports .child-menu').hide();
        //$('[data-action^="nav-left-medium"]').on('click', function () {
        //    $(liactmenu + ' a[data-action^="click-trigger"]').trigger('click');
        //});
    }
    
    //End Menulinks active.
});


$(document).on('focus', ':input', function () {
    $(this).attr('autocomplete', 'off');
});

function validateNumber(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    });
}

function setPaging(reccount, pageindex, pagesize) {
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
    //$('#ddlpagesize').empty();
    //var pagesizes = GetPageLengthArray(reccount);
    //// alert(pagesizes);
    //$(pagesizes).each(function () {
    //    $('#ddlpagesize').append('<option value=' + this + ' ' + (parseInt(this) == parseInt(pagesize) ? 'selected' : '') + '>' + (parseInt(this) == -1 ? 'All' : this) + '</option>');
    //});

    loadPagination(numoffpages, pageindex, fromDisplayNumber, toDisplayNumber);
    $('#totalrec').html(reccount);
    $('#showpageinfo').html('Displaying Page ' + pageindex + ' of ' + numoffpages);
}

// to load pagination bar
function loadPagination(numOfPages, pageindex, fromDisplayNumber, toDisplayNumber) {
    // load pagenation ul.  
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


function SetQueryStDefaultVal(qs,defval) {
    var q = querySt(qs);    
    q = q== undefined ? defval : q == "" ? defval : q;   
    return q;
}
function CheckDatesDiff(from, to, days, datemandatory) {
    //tocheck dates mandatory.
    if (datemandatory == 1) {
        if (from == "") {
            return "Please select FromDate";
        }
        if (to == "") {
            return "Please select ToDate";
        }
    }    
    if (from == "" && to != "")
    {
       return "Please select FromDate";
    }
    if (to == "" && from != "")
    {
       return "Please select ToDate";
    }
  
    var fromdt = new Date(from);
    var todt = new Date(to);    
    // time difference
    var timeDiff = (todt.getTime() - fromdt.getTime());
    // days difference
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    // difference
    if (diffDays.toString().indexOf('-') != -1) {
        return "ToDate is greater than FromDate";
    }
    else {
        if (diffDays > days) {
            return 'Invalid date range selection (Only ' + days + ' days allowed)';
        }
        else { return ""; }
    }
}
//Json stringfy
function StringfyJson(object) {
    return JSON.stringify(object);
}
//ParseJson response with stringfy
function ParseJsonResponseStringfy(object) {
    return $.parseJSON(JSON.stringify(object));
}
//ParseJson response
function ParseJsonResponse(object) {
    return $.parseJSON(object);
}
//Create cookie
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + encodeURIComponent(value) + expires + "; path=/";
}
//Read cookie
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
}
//Read dictionarycookie.
function readDictionaryCookie(name, key) {
    var _ci = readCookie(name).toString();
    _ci = _ci.substring(_ci.indexOf(key + '=') + 4);
    _ci = _ci.substring(0, _ci.indexOf('&'));
    return _ci;
}
//Erase cookie
function eraseCookie(name) {createCookie(name, "", -1);}
//Get querystring value
function querySt(ji) {
    hu = window.location.search.substring(1);
    hu = decodeURI(hu);
    gy = hu.split("&");
    for (i = 0; i < gy.length; i++) {ft = gy[i].split("=");if (ft[0] == ji) {return ft[1]}}
}
//Get Url only without Querystrings.
function GetUrlWithNoQueryStrings(){return (location.protocol + '//' + location.host + location.pathname).toLowerCase();}
// Global js library
var gl = {
    ajaxreq: function (serviceurl, reqtype, data, OnSuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader) {
        try {
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Authorization': baseauthkey } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: 'text json',
                async: isasync,
                beforeSend: function () {
                    ajaxprocessindicator(resctrl, msg, 1, 'suc');
                },
                complete: function () {
                    ajaxprocessindicator(resctrl, sucmsg, 0, 'suc');
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
        catch (err) { console.log(err.message); ajaxprocessindicator(resctrl, errmsgprefix + errmsg, 0, 'err'); }
    },

    ajaxreqgrid: function (serviceurl, reqtype, data, OnSuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader) {
        try {
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Authorization': baseauthkey } : '',
                data: data,
                async: isasync,
                cache: false,
                beforeSend: function () {
                    ajaxprocessindicator(resctrl, msg, 1, 'suc');
                },
                complete: function () {
                    ajaxprocessindicator(resctrl, sucmsg, 1, 'suc');
                },
                success: OnSuccess,
            });
        }
        catch (err) { console.log(err.message); ajaxprocessindicator(resctrl, errmsgprefix + errmsg, 0, 'err'); }
    },

    ajaxreqloader: function (serviceurl, reqtype, data, OnSuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader,pageloaderdiv,pagecontentdiv) {
        try {            
            var pageLoader = pageloaderdiv == undefined ? '.pageloader' : pageloaderdiv;
            var pageContent = pagecontentdiv == undefined ? '.pagecontent' : pagecontentdiv;
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Authorization': baseauthkey } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: 'text json',
                async: isasync,
                beforeSend: function () {                    
                    $(pageLoader).removeClass('hide');
                    $(pageContent).hide();                 
                },
                complete: function () {
                    $(pageLoader).addClass('hide');
                    $(pageContent).show();
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
}
//Loading ajax process message.
function ajaxprocessindicator(lblctrl, text, type, restype) {
    if (lblctrl != '') {
        $(lblctrl).clearQueue();
        $(lblctrl).html(text);
        restype == 'suc' ? $(lblctrl).addClass('text-info') : $(lblctrl).addClass('text-danger');
    }
}
//textbox allow only numbers
function allownumber(txt) {
    //alert('sddd');
    $('#' + txt).keydown(function (event) {
        // Allow special chars + arrows 
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
            || event.keyCode == 27 || event.keyCode == 13
            || (event.keyCode == 65 && event.ctrlKey === true)
            || (event.keyCode >= 35 && event.keyCode <= 39)) {
            return;
        } else {
            // If it's not a number stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });
}
//ParseJson response with stringfy
function ParseJsonResponseStringfy(object) {return $.parseJSON(JSON.stringify(object));}
// Get value from datatable data object.
var dataobject = {key: function (n) {return this[Object.keys(this)[n]];}};
// Get key from datatable data object.
function key(obj, idx) {return dataobject.key.call(obj, idx);}
// Convert 12 hour am/pm time 24 hour format
function convertTo24Hour(time) {
    var hours = parseInt(time.substr(0, 2));
    if (time.indexOf('am') != -1 && hours == 12) {
        time = time.replace('12', '0');
    }
    if (time.indexOf('pm') != -1 && hours < 12) {
        time = time.replace(hours, (hours + 12));
    }
    return time.replace(/(am|pm)/, '');
}
//allow float
//textbox allow only float
function allowfloat(txt) {
    //alert('sddd');
    $('#' + txt).keydown(function (event) {
        // Allow special chars + arrows 
        //var c = (evt.which) ? evt.which : event.keyCode
        //console.log(event.keyCode);
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
            || event.keyCode == 27 || event.keyCode == 13
            || (event.keyCode == 65 && event.ctrlKey === true)
            || (event.keyCode >= 35 && event.keyCode <= 39) || event.keyCode == 190) {
            return;
        } else {
            // If it's not a number stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });
}
//Set to display search validation messages.
function SearchVsdisplay(errMsgs) {
    $('.valerr').html('');
    if (errMsgs.length > 0) {
        if ($('.valerr').has('ul').length == 0) {
            $('.valerr').append('<div class="search-valerr-head">Errors List</div>');
        }
        for (i = 0; i < errMsgs.length; i++) {
            if (i == 0) {
                $('.valerr').append('<ul class="search-valerr-li"><li>' + errMsgs[i] + '</li></ul>');
            }
            else { $('.valerr ul.search-valerr-li').append('<li>' + errMsgs[i] + '</li>'); }
        }
        $('.valerr').show();
    }
}
//Get Datatable PageLength Array.
//Params: reccount- total records count.
function GetPageLengthArray(reccount) {
    if (reccount <= 100) {
        return [[10, 25, 50, 100, -1], [10, 25, 50, 100, 'All']];
    }
    if (reccount <= 500) {
        return [[10, 25, 50, 100, 200, 300, 400, 500, -1], [10, 25, 50, 100, 200, 300, 400, 500, 'All']];
    }
    else {
        return [[10, 25, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 - 1], [10, 25, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 'All']];
    }
}
//Set Datatable for reports.
//Params: dt- tableid, _data- server data, cols- columns,coldefs- coldefs, emptyMsg- message to dispay on no records, pageSizeLength- total records count
//        isResponsive- Responsive,isAutoWidth- Autowidth,isScrollX- Scrollx direction,isModalGridMap - Modal popup splitter grid and map sidebyside, fnCreatedRow - any created row event else pass null.
function SetGridReports(dt, _data, cols, coldefs, emptyMsg, pageSizeLength, isResponsive, isAutoWidth, isScrollX, isModalGridMap, fnCreatedRow) {    
    $(dt).DataTable({
        "dom": isModalGridMap ? 'flBrtp' : 'flipBrtp',
        "buttons": [
            {
                extend: 'collection',
                text: 'Export',
                "buttons": [{
                    extend: 'copy',
                    text: '<i class="fa fa-files-o" style="color:blue;"> <span>Copy</span></i>',
                    titleAttr: 'Copy'
                },
                    {
                        extend: 'pdf',
                        title: 'Report',
                        text: '<i class="fa fa-file-pdf-o" style="color:red"> <span>Pdf</span></i>',
                        titleAttr: 'PDF'
                    },
                    {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"  style="color:green"> <span>Excel</span></i>',
                        titleAttr: 'Excel'
                    }
                ],
            }
        ], 
        "destroy": true,
        "responsive": isResponsive,        
        "autoWidth": isAutoWidth,
        "deferRender": true,
        "processing": true,
        "pageLength": isModalGridMap?100:25,
        "lengthChange": true,
        "lengthMenu": GetPageLengthArray(pageSizeLength),
        "pagingType": "full_numbers",
        "ordering": [],
        "sorting": [],
        "info": true,
        "recordsTotal": true,
        "scrollY": isModalGridMap? '380px' : '480px',
        "scrollCollapse": true,
        "scrollX": isScrollX,
        "language": {
            "search": "",
            "lengthMenu": "Show _MENU_",
            "zeroRecords": emptyMsg,
            "info": "Total: _TOTAL_ , page: _PAGE_ of _PAGES_",
            "infoEmpty": emptyMsg,
            "infoFiltered": "(filtered from _MAX_ total records)"
        },
        "data": _data,
        "columns": cols,
        "columnDefs": coldefs,
        "search":{
            "bSmart": false, 
            "bRegex": false,
            "sSearch": ""                
        },
        "drawCallback": function () {
            $('.dataTables_paginate > .pagination').addClass('pagination-sm');
            $('.dataTables_filter,.dataTables_info,.dataTables_length').css('font-size', '13px');
            isModalGridMap ? $('.dataTables_filter').addClass('col-lg-4').css('padding', '0') : $('.dataTables_filter').addClass('col-lg-2').css('padding', '0');
            $('.dataTables_filter label input').attr('placeholder', 'Search');
            $('.dataTables_info,.dataTables_length').addClass('col-lg-2');
            isModalGridMap ? $('.dataTables_paginate').addClass('col-lg-12').css('float', 'right').css('padding', '0') : $('.dataTables_paginate').addClass('col-lg-4').css('float', 'right').css('padding', '0');
            $('.paginate_button.first a').html('<i class="fa fa-angle-double-left" aria-hidden="true"></i>');
            $('.paginate_button.previous a').html('<i class="fa fa-angle-left" aria-hidden="true"></i>');
            $('.paginate_button.next a').html('<i class="fa fa-angle-right" aria-hidden="true"></i>');
            $('.paginate_button.last a').html('<i class="fa fa-angle-double-right" aria-hidden="true"></i>');
        },
        "createdRow": fnCreatedRow
    });
}
//Set Grid or No Recors display.
//Params: gridType- abstract(first)/summary(deatils), msg- any message todisplay,isRecords- 1,0 and -1(error).
function SetGridDisplay(gridType, msg, isRecords) {
    switch (isRecords) {
        case 1:
            $("#tbl" + gridType).removeClass("hide");
            $("#norec" + gridType).addClass("hide");
            break;
        case 0:
            $("#tbl" + gridType).addClass("hide");
            $("#norec" + gridType).html(msg).removeClass("hide");
            break;
        case -1:
            console.log(msg);            
            $("#tbl" + gridType).addClass("hide");
            $("#norec" + gridType).html('Some problem occured while getting data.').removeClass("hide");
            break;
    }   
}

function GetBusinessTypes(ctrl) {
    gl.ajaxreq(websiteurlapi + "GetBusinessTypes", "GET", null, function (response) {
        if (response != null && response.length > 0) {
            $.each(response, function (i, item) {
                $(ctrl).append('<option value="' + item.BusinessTypeId + '">' + item.BusinessType + '</option>');
            });
        }
    }, '', '', '', '', false, false);
}

function readURL(input, previewctrl) {
   
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            if (previewctrl != null)
            {
                $('#' + previewctrl).attr('src', e.target.result);
            }
                else
            {
                $('#imgpreview').attr('src', e.target.result);
            }
        }

        reader.readAsDataURL(input.files[0]); // convert to base64 string
    }
}

//function QRdownload(type, filename, qrurl) {
//    switch (type) {
//        case 'jpg':               
//            var a = document.createElement('a');
//            a.download = filename;
//            a.href = qrurl;
//            a.click();     
//            //m.ajaxreq("QrDownload", { qrimgurl: $(ctrl).parent().parent().find('.dqrimg').attr('src') }, function (response) { });
//            break;                
//        case 'pdf': window.print(); break;
//    }
//}

function QRdownload(type, filename, qrurl, title,info) {
    //alert(type);
    switch (type) {
        case 'jpg':
            var a = document.createElement('a');
            a.download = filename;
            a.href = qrurl;
            a.click();
            //m.ajaxreq("QrDownload", { qrimgurl: $(ctrl).parent().parent().find('.dqrimg').attr('src') }, function (response) { });
            break;
        case 'pdf':
            var mywindow = window.open('', 'PRINT', '');
            mywindow.document.write('<html><head><title></title>');
            mywindow.document.write('</head><body>');
            mywindow.document.write('<br/><br/>');
            mywindow.document.write('<p><center><h1>' + '<br/>' + title + '</h1><center></p>');
            mywindow.document.write('<br/><br/><br/>');
            mywindow.document.write('<center><img src="' + qrurl + '"/><center>');
            mywindow.document.write('<br/><br/><br/>');
            mywindow.document.write('<p><center><h1>' + info + '</h1><center></p>');
            //mywindow.document.write('<p><center><h1>' + "Scan the QR Code to be in the Draw to Play" + '</h1><center></p>');
            mywindow.document.write('</body></html>');
            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/
            mywindow.print();
            mywindow.close();
            //window.print();
            break;
    }
}


//$(document).on('click', '#ExportToExcel', function (e) {

//    var pagesize = -1;
//    var pageindex = 1;
//    var sortby = 1; //$('#SortBy').val();
//    var str = SetQueryStDefaultVal("_str", "");
//    $("#search").val(str);
//    var CustomerId = $('#ddlCustomerID').val();
//    var StationLocation = $('#ddlLocation option:selected').text();
//    var row = '';
//    var reccount = 0;
//    var isloader = String(str).length == 0 ? true : false
//    gl.ajaxreqloader(websiteurlapi + "/AdminGetSurveyPrizes", "get", { bid: bid, status: status, pgindex: pageindex, pgsize: pagesize, str: str }, function (response) {
//        if (response.length > 0) {
//            reccount = response[0].TotalRecords;
//            $.each(response, function (i, item) {
//                row += "<tr><td>" + item.SurveyName + "</td><td>" + item.CustomerName + "</td><td>" + item.PrizeText + "</td><td>" + item.RedeemCode + "</td></tr>";
//                //row += "<tr><td>" + item.EStatus + "</td><td>" + item.ESignal + "</td><td>" + item.StationId + "</td><td>" + item.StationName + "</td><td>" + item.StationLocation + "</td><td>" + item.STxnTime + "</td><td>" + item.RTxnDate + "</td><td>" + item.RTxnTime + "</td><td>" + item.XTxnDate + "</td><td>" + item.XTxnTime + "</td><td>" + item.YTxnDate + "</td><td>" + item.YTxnTime + "</td></tr>";
//                // row += "<tr><td><img src=" + item.StatusImage + " style='width:75px;'></td><td><img src=" + item.SignalImage + " style='width:30px;'></td><td>" + item.StationId + "</td><td>" + item.StationName + "</td><td>" + item.StationLocation + "</td><td>" + item.STxnTime + "</td><td>" + item.RTxnDate + "</td><td>" + item.RTxnTime + "</td><td>" + item.XTxnDate + "</td><td>" + item.XTxnTime + "</td><td>" + item.YTxnDate + "</td><td>" + item.YTxnTime + "</td></tr>";
//            });
//            $("#tbldata").html(row);
//            setPagging(reccount, pageindex, pagesize);
//            $('#no-data').addClass('hide');
//            $('#table-data').removeClass('hide');
//        }
//    }, '', '', '', '', false, true, '.pageloader', '.history', 'text json', isloader);
//    ExportToexcel('printgrid', 'Survey Data');
//});
//function ExportToexcel(elementid, pagetitle) {
//    $('#' + elementid + ' .tbldata  .hiddenprint').remove();
//    var fname = pagetitle + '.xls';
//    var tab_text = "<table border='1px'>";
//    var textRange; var j = 0;
//    var tab = document.getElementById('dataTable');
//    //  tab = tab.getElementById('dataTable')[0];
//    //alert(tab.rows.length);
//    for (j = 0 ; j < tab.rows.length ; j++) {

//        tab_text = tab_text + "<tr>" + tab.rows[j].innerHTML + "</tr>";
//    }
//    tab_text = tab_text + "</table>";

//    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
//    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
//    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params
//    //tab_text = tab_text.replace('class="hiddenprint"', 'style=display:none');
//    var ua = window.navigator.userAgent;
//    var msie = ua.indexOf("MSIE ");
//    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
//    {
//        dumiframexls.document.open("txt/html", "replace");
//        dumiframexls.document.write(tab_text);
//        dumiframexls.document.close();
//        dumiframexls.focus();
//        sa = dumiframexls.document.execCommand("SaveAs", true, fname);
//    }
//    else {
//        var data_type = 'data:application/vnd.ms-excel';
//        var table_div = tab_text;
//        var table_html = table_div.replace(/ /g, '%20');

//        var link = document.getElementById('dumlnkxls');
//        link.download = fname;
//        link.href = data_type + ', ' + table_html;
//        link.click();
//    }

//}
