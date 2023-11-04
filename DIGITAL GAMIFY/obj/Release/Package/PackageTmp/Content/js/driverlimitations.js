var globalWebSiteApiurl = websiteurlapi + "/global/";
var ownerWebSiteApiurl = websiteurlapi + "/owner";
var currpageurl = window.location.href.toLowerCase();

//Set pages onload functions.
$(function () {    
    
    // Set Curentpageurl.
    if (currpageurl.indexOf('settings/overspeed') > -1) {
       // validateNumber(document.getElementById("speed"), function (value) { return /^-?\d*$/.test(value); });
        SetOverSpeedPage();


    }
    else if (currpageurl.indexOf('settings/geofence') > -1) {
        //validateNumber(document.getElementById("radius"), function (value) { return /^-?\d*$/.test(value); });
        SetGeofencePage();

    }
});

function SetSearchddlDrivers() {
    $("#ddlsearchdriver").select2({ width: '100%' });
    gl.ajaxreq(globalWebSiteApiurl + "GetDdlDrivers?ownerid=" + oid, "GET", null, function (response) {
        var clientData = [];
        if (response != null && response.length > 0) {
            clientData.push({ id: '-1', text: "-- Select --" });
            $.each(response, function (i, item) {
                //item.text = item.text;
                clientData.push(item);
            });
            $("#ddlsearchdriver").select2({
                data: clientData,
                width: '100%'
            });
        }
        else {
            $("#ddlsearchdrivers").select2({
                data: [],
                width: '100%'
            });
        }
        var seldriver = querySt('_d') == null ? '-1' : querySt('_d');       
        $('#ddlsearchdriver').val(seldriver).trigger('change');      
    }, '', '', '', '', true, false);
}

//Bind Overspeed to edit.
function SetEditOverSpeed(did, speed) {
    $('#DriverId').val(did);
    $('#ddlsearchdriver').val(did).trigger('change');
    $('#speed').val(speed);
    $('#ddlsearchdriver').prop('disabled', true);
}

$(document.body).on("change", "#ddlsearchdriver", function () {
    if ($('#ddlsearchdriver').val() == '-1' || $('#ddlsearchdriver').val() == '0') {
        $('#DriverId').val('');
    }
    else {
        $('#DriverId').val($('#ddlsearchdriver').val());
    }
});

//OverSpeed Page.
function SetOverSpeedPage() { 
    var did = 0; 
    var pi = 1;
    var ps = 25;
    SetSearchddlDrivers();        
    //Search event handler.   
    SetOverSpeedGrid(pi, ps);
    function SetOverSpeedGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(ownerWebSiteApiurl + "/GetDriversOverSpeeds", "POST", { OwnerId: oid, DriverId: did, Pi: pageindex, Ps: pagesize }, function (response) {          
            if (response.length > 0) {
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].totalRecords;               
                $.each(response, function (i, item) {
                    row += "<tr><td class='col-lg-3'>" + item.name + "</td><td class='col-lg-3'>" + item.speedKmph + "</td><td class='col-lg-3 text-center'>" + item.createdDay + "</td><td class='col-lg-3  menu-action text-center'>" +                        
                        "<a onclick='SetEditOverSpeed(" + item.driverId + "," + item.speed + ")' data-original-title='edit' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey' data-action='backtop'> <i class='fa fa-pencil'></i> </a>" + "</td></tr>";
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
       // window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_pi=" + pageindex + "&_ps=" + pagesize);
        SetOverSpeedGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
       // window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_pi=" + pageindex + "&_ps=" + pagesize);
        SetOverSpeedGrid(pageindex, pagesize);
    });

}


//Bind Geofence to edit.
function SetEditGeofence(did,id) {   
    $('#DriverId').val(did);
    $('#ddlsearchdriver').val(did).trigger('change');
    $('#ddlsearchdriver').prop('disabled', true);
    gl.ajaxreqloader(ownerWebSiteApiurl + "/GetDriverGeofenceLimit", "POST", { DriverId: did,Id:id }, function (response) {        
        if (response != null) {            
            $('#geofencename').val(response.geofenceName);
            $('#location').val(response.location);
            $('#radius').val(response.radius);
            $('#Latitude').val(response.latitude);
            $('#Longitude').val(response.longitude);
            $('#Id').val(response.id);
        }
    }, '', '', '', '', false, false);
}

//Geofence Page.
function SetGeofencePage() {
    var did = 0;
    var pi = 1;
    var ps = 25;
    SetSearchddlDrivers();
    //Search event handler.   
    SeGeofenceGrid(pi, ps);
    function SeGeofenceGrid(pageindex, pagesize) {
        var row = '';
        var reccount = 0;
        gl.ajaxreqloader(ownerWebSiteApiurl + "/GetDriversGeofences", "POST", { OwnerId: oid, DriverId: did, Pi: pageindex, Ps: pagesize }, function (response) {
            if (response.length > 0) {
                $('#tblgrid').removeClass('hide');
                $('#norec').addClass('hide');
                reccount = response[0].totalRecords;
                $.each(response, function (i, item) {
                    row += "<tr><td class='col-lg-2'>" + item.name + "</td><td class='col-lg-3'>" + item.geofenceName + "</td><td class='col-lg-3'>" + item.location + "</td><td class='col-lg-2'>" + item.radiusinKm + "</td><td class='col-lg-2 text-center'>" + item.createdDay + "</td><td class='col-lg-1  menu-action text-center'>" +
                        "<a onclick=SetEditGeofence(" + item.driverId +","+ item.id +") data-original-title='edit' data-toggle='tooltip' data-placement='top' class='btn menu-icon vd_bd-grey vd_grey' data-action='backtop'> <i class='fa fa-pencil'></i> </a>" + "</td></tr>";
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
        // window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_pi=" + pageindex + "&_ps=" + pagesize);
        SeGeofenceGrid(pageindex, pagesize);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        // window.history.pushState(null, "", GetUrlWithNoQueryStrings() + "?_pi=" + pageindex + "&_ps=" + pagesize);
        SeGeofenceGrid(pageindex, pagesize);
    });

}