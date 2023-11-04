var globalWebSiteApiurl = websiteurlapi;
var addSwipeandwinUrl = websiteurl + "business/customer/GameDetails";
function GetGameslist() {
    var row = '';
    var reccount = 0;
    var bid = $("#bid").val();
    //alert(bid);
    //var isloader = String(searchby).length == 0 ? true : false
    gl.ajaxreqloader(globalWebSiteApiurl + "AdminGetSwipeandWin", "POST", { AdminId: 1, BusinessId: bid, FromDate: ' ', ToDate: ' ', Pi: 1, Ps: 10 }, function (response) {
        if (response.length > 0) {
            reccount = response[0].TotalRecords;
            $.each(response, function (i, item) {
                //console.log(item);
                row += "<div class='col-md-12 col-sm-12 col-xs-12'><div class='finger-lightly-wrapper'><div class='col-md-4 col-sm-12 col-xs-12'><div class='welcome-image-wrapper inside-right'><img src=" + item.ImagePath + " class='welcome-wrapper'></div></div><div class='col-md-8 col-sm-12 col-xs-12'><div class='welcome-header inside-left'><h6 class='panel-wrapper'>" + item.Description + "</h6><div class=''><a href='" + addSwipeandwinUrl + "?gid=" + item.GameId + "' class='btn btn-info vb-btn px-5 rounded-0'> Enter</a></div></div></div></div></div>";
            });
            $("#games").html(row);
            //setPagging(reccount, pageindex, pagesize);
        }
    }, '', '', '', '', true, true,'text json', 'true');
    //var url = businessbaseurl + 'ViewGames?ft=' + pageindex + '|' + pagesize + '|' + sortby + '|' + searchby + '|'
    //setnavigationurl(url);
}

function getavailblegame() {
    var row = '';
    var bid = $("#bid").val();
    gl.ajaxreq(globalWebSiteApiurl + 'GetAvailableGames', 'get', { bid: bid,cid:0 }, function (response) {
        //$('#pagecontent').hide();
        //$('#noresultcontent').show();
        if (response.Status) {
            if (response.Success) {
                var data = response.Data;
                if (data == "")
                {
                    $("#nogames").css("display","block");
                }
                //console.log(data);
                $.each(data, function (i, item) {
                    //console.log(item.Description);
                    row += "<div class='col-md-12 col-sm-12 col-xs-12'><div class='finger-lightly-wrapper'><div class='col-md-4 col-sm-12 col-xs-12'><div class='welcome-image-wrapper inside-right'><img src=" + item.ImagePath + " class='welcome-wrapper'></div></div><div class='col-md-8 col-sm-12 col-xs-12'><div class='welcome-header inside-left'><h6 class='panel-wrapper'>" + item.Description + "</h6><div class=''><a href='" + addSwipeandwinUrl + "?gid=" + item.GameId + "&bid=" + item.BusinessId + "' class='btn btn-info vb-btn px-5 rounded-0'> Enter</a></div></div></div></div></div>";
                });
                $("#games").html(row);
                //$('#pagecontent').show();
                $('#noresultcontent').hide();
            }
            else {
                displaynocontentmessage('No availble Games');
            }
        }
        else {
            displaynocontentmessage('No availble Games');
        }
    }, '', '', '', '', false,true);
}
function displaynocontentmessage(message) {
    $('#noresultcontent').html(message);
}