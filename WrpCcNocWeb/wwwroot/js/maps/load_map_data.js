
//map.createPane('pane-llp').style.zIndex = 610;
//map.createPane('pane-llp').style.zIndex = 610;
//var markersB = createMarkerClusterGroup('myclusterB', 'paneB');

//function onmove() {
//    // Get the map bounds - the top-left and bottom-right locations.
//    var inBounds = [],
//        bounds = map.getBounds();
//    markers.eachLayer(function (marker) {
//        // For each marker, consider whether it is currently visible by comparing
//        // with the current map bounds.
//        if (bounds.contains(marker.getLatLng())) {
//            inBounds.push(marker.options.title);
//        }
//    });
//    // Display a list of markers.
//    document.getElementById('coordinates').innerHTML = inBounds.join('\n');
//}

var map,
    mapZoom = 7,
    dataCode,
    dataName,
    dataAdminCode,
    dataAdminName,
    //data_type = 1,
    map_data = [],
    other_data = [],
    //data_info = [],
    equipmentId,
    equipmentTypes = {
        "1": "Low Lift Pump (LLP)",
        "2": "Deep Tubewell (DTW)",
        "3": "Shallow Tubewell (STW)",
        "13": "Low Lift Pump (LLP)",
        "14": "Deep Tubewell (DTW)"
    },
    //baseUrl = "@Url.Action("Index", "SurveyInfoes")",
    noDataColor = "#FFFFFF",
    defaultTheme,
    dataLayer = null,
    otherLayer = null,
    mapLabels = new L.LayerGroup(),
    otherLabels = new L.LayerGroup(),
    mapLayers = {},
    equipmentClusters = {},
    //adminMapLabels = new L.LayerGroup(),
    polyDefaultOptions = {
        zIndex: 101,
        weight: 1.0,
        opacity: 0.9,
        color: "#D0C0A0",
        fillOpacity: 0.8
    },
    adminFocused = null;


$(function () {

    $("#admin_info").val("dist");
    $("#survey_info").val("DTW");

    dataAdminCode = $("#admin_info").val();
    dataAdminName = $("#admin_info option:selected").text();

    dataCode = $("#survey_info").val();
    dataName = $("#survey_info option:selected").text();
    equipmentId = $("#survey_info option:selected").attr("data-id");

    $("#map_label_opt").prop("checked", false);

    set_basic_opts();

    load_equipment_infos();

    set_survey_data();

    draggable_modal("option_title", "map_filter_content", "map_filter_bg", false);

    draggable_modal("data_title", "map_data_content", "map_data_bg", false);

});

function load_equipment_infos() {
    var dataUrl = "../MapViewer/GetEquipmentInfos";
    $.ajax({
        type: "Get",
        url: dataUrl,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $("#busy-indicator").fadeIn();
        },
        success: function (equipmentInfos) {
            if (equipmentInfos && equipmentInfos.length > 0) {
                equipmentTypes = {};
                equipmentInfos.map(function (equInfo) {
                    equipmentTypes[equInfo.equipmentId] = equInfo.nameOfEquipment;
                });
            }
        },
        error: function (e) {
            msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.responseText);
            $("#busy-indicator").fadeOut();
        },
        complete: function () {
            $("#busy-indicator").fadeOut();
        }

    });
}

function set_basic_opts() {

    if (typeof L == "undefined" || L == undefined) {
        legend_open_close("legend", "close", "right");
        $("#legend").css("right", "-1000px");
        legend_open_close("legend_info", "close", "left");
        $("#legend_info").css("left", "-1000px");

        msg.init("error", "Error... . .", "Map Loading Failed !");
        $("#busy-indicator").fadeOut();

        return false;
    }


    //drawnItems = new L.FeatureGroup();
    //Google Map;
    //Hybrid: s,h;
    //Satellite: s;
    //Streets: m;
    //Terrain: p;

    var blankUrl = "../images/blank.png",
        osmUrl = "http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
        googleUrlHy = "http://{s}.google.com/vt/lyrs=s,h&x={x}&y={y}&z={z}",
        googleUrlSa = "http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}",
        googleUrlSt = "http://{s}.google.com/vt/lyrs=m&x={x}&y={y}&z={z}",
        googleUrlTr = "http://{s}.google.com/vt/lyrs=p&x={x}&y={y}&z={z}",
        mapbUrl = "https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiYWxoYWRpLWNzZSIsImEiOiJjamx1azJ5emcwamlkM3ZxeHB0ajB0d3I1In0.k_DovXLLPpp7fQ_i685ocA",
        esriUrl = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}",
        mapAttrib = "Map data © <a href='https://www.cegisbd.com/' target='_blank'>CEGIS</a> & <a href='http://badc.gov.bd/' target='_blank'>BADC</a>";

    var openstreet = L.tileLayer(osmUrl, { id: 'MapID', minZoom: 0, maxZoom: 25, attribution: mapAttrib }),
        googleHy = L.tileLayer(googleUrlHy, { id: 'MapID', minZoom: 0, maxZoom: 25, subdomains: ["mt0", "mt1", "mt2", "mt3"], attribution: mapAttrib }),
        googleSa = L.tileLayer(googleUrlSa, { id: 'MapID', minZoom: 0, maxZoom: 25, subdomains: ["mt0", "mt1", "mt2", "mt3"], attribution: mapAttrib }),
        googleSt = L.tileLayer(googleUrlSt, { id: 'MapID', minZoom: 0, maxZoom: 25, subdomains: ["mt0", "mt1", "mt2", "mt3"], attribution: mapAttrib }),
        googleTr = L.tileLayer(googleUrlTr, { id: 'MapID', minZoom: 0, maxZoom: 25, subdomains: ["mt0", "mt1", "mt2", "mt3"], attribution: mapAttrib }),
        mapboxLi = L.tileLayer(mapbUrl, { id: 'MapID', minZoom: 0, maxZoom: 25, id: "mapbox.light", attribution: mapAttrib }),
        mapboxSt = L.tileLayer(mapbUrl, { id: 'MapID', minZoom: 0, maxZoom: 25, id: "mapbox.streets", attribution: mapAttrib }),
        esri = L.tileLayer(esriUrl, { id: 'MapID', minZoom: 0, maxZoom: 25, attribution: mapAttrib }),
        blank = L.tileLayer(blankUrl, { id: 'MapID', minZoom: 0, maxZoom: 25, attribution: mapAttrib });

    //L.mapbox.accessToken = "pk.eyJ1IjoiYWxoYWRpLWNzZSIsImEiOiJjamx1azJ5emcwamlkM3ZxeHB0ajB0d3I1In0.k_DovXLLPpp7fQ_i685ocA";
    //{id: 'MapID', attribution: mapboxAttribution}

    map = new L.Map("map",
        {
            center: new L.LatLng(23.737777, 90.537777),
            fullscreenControl: true,
            zoomControl: false,
            maxZoom: 20,
            minZoom: 2,
            zoom: 7.0,
            layers: [openstreet]
        });

    map.setView([23.737777, 90.537777], 7.0);


    var baseMaps = {
        "Open Street": openstreet,
        "Google Hybrid": googleHy,
        "Google Satellite": googleSa,
        "Google Streets": googleSt,
        "Google Terrain": googleTr,
        "Mapbox Light": mapboxLi,
        "Mapbox Streets": mapboxSt,
        "ESRI": esri,
        "None": blank
    };


    //map.createPane("data-layer").style.zIndex = 401;
    map.createPane("other-layer").style.zIndex = 403;
    map.createPane("admin-layer").style.zIndex = 405;

    //map.createPane("marker-layer").style.zIndex = 600;
    map.createPane("label-layer").style.zIndex = 601;

    //map.createPane("admin").style.zIndex = 455;

    //drawnItems.addTo(map);
    //drawnItems.addTo(map);

    L.tileLayer("", { attribution: mapAttrib }).addTo(map);
    //L.control.layers(baseMaps, { "Select Layer": drawnItems }, { position: "topright", collapsed: true }).addTo(map);

    L.control.scale().addTo(map);
    //L.control.pan().addTo(map);
    L.control.zoom().addTo(map);

    map.on("mousemove",
        function (evt) {
            $("#map_cord_info").html(evt.latlng.lat.toFixed(8) + ", " + evt.latlng.lng.toFixed(8));
        });

    $("#map_center").on("click",
        function (e) {
            map.setView([23.737777, 90.537777], 7.0);
        });

    //L.control.layers(null, baseMaps, { collapsed: false }).addTo(map);
    var bglayers = L.control.layers(baseMaps, null, { collapsed: false });

    bglayers.addTo(map);
    //$("#bglayers_content").append(bglayers.getContainer());

    var bglayers_content = $(bglayers.getContainer());
    bglayers_content.find("a.leaflet-control-layers-toggle").remove();
    bglayers_content.find("form.leaflet-control-layers-list").css("display", "block");
    $("#map_bglayers").append(bglayers_content);


    //add_admin_boundary("div");


    $("#map_label_opt").on("change",
        function () {
            map_label_show_hide(this.checked);
        });

    $("#map_full_screen").on("click",
        function () {
            if (!confirm("Are you sure to close full screen ?"))
                return;

            $("#map_info").toggleClass("map-full-screen");
        });

    $("input[type='checkbox'].multi-chkbx.admin").each(function () {
        $(this).on("change",
            function () {

                if (!$(this).prop("id").replace("admin_", ""))
                    return;

                var adminCode = $(this).prop("id").replace("admin_", "");

                $("#busy-indicator").fadeIn();

                if (this.checked)
                    add_admin_boundary(adminCode);
                else
                    remove_admin_boundary(adminCode);

                $("#busy-indicator").fadeOut();
            });
    });

    $("input[type='checkbox'].multi-chkbx.other_layer").each(function () {
        $(this).on("change",
            function () {
                if (!$(this).prop("id"))
                    return;

                var layerCode = $(this).prop("id"),
                    layerDataCode = $(this).attr("data");

                $("#busy-indicator").fadeIn();

                if (this.checked) {

                    if ($("#" + layerCode + "_data_code").length) {

                        $("#" + layerCode + "_data_code").show(250);

                        if ($("#" + layerCode + "_data_code").val()) {
                            layerDataCode = $("#" + layerCode + "_data_code").val();
                        } else {
                            $("#" + layerCode + "_data_code").val(layerDataCode);
                        }

                        $("#" + layerCode + "_data_code").on("change", function (e) { resetOtherDataLayerStyle(layerCode, $(this).val()); });

                        //setOtherDataLayerStyle(layerCode, $(this).val());
                        //console.log(mapLayers["other_" + layerCode].layer.onEachFeature);
                    }

                    add_other_layer(layerCode, layerDataCode);

                }
                else {
                    remove_other_layer(layerCode);

                    if ($("#" + layerCode + "_data_code").length) {
                        $("#" + layerCode + "_data_code").hide(250);
                        $("#" + layerCode + "_data_code").off("change");
                    }
                }

                $("#busy-indicator").fadeOut();
            });
    });

    $("input[type='checkbox'].multi-chkbx.admin-label").each(function () {
        $(this).on("change",
            function () {
                if (!$(this).prop("id").replace("admin_label_", ""))
                    return;

                var adminCode = $(this).prop("id").replace("admin_label_", "");

                $("#busy-indicator").fadeIn();

                add_remove_admin_label(adminCode, this.checked);

                $("#busy-indicator").fadeOut();
            });
    });

    $("#admin_info").on("change",
        function () {
            dataAdminCode = $("#admin_info").val();
            dataAdminName = $("#admin_info option:selected").text();

            //dataCode = $("#survey_info").val();
            //dataName = $("#survey_info option:selected").text();
            //equipmentId = $("#survey_info option:selected").attr("data-id");

            set_survey_data();
            return false;
        });


    $("#data_year").on("change",
        function () {
            set_survey_data();
            return false;
        });


    $("#survey_info").on("change",
        function () {

            if ($("#survey_info").val()) {
                //dataAdminCode = $("#admin_info").val();
                //dataAdminName = $("#admin_info option:selected").text();

                dataCode = $("#survey_info").val();
                dataName = $("#survey_info option:selected").text();
                equipmentId = $("#survey_info option:selected").attr("data-id");

                set_survey_data();
            }
        });

    $("#legend_theme").on("change",
        function () {
            set_survey_data();
        });

    //$("#legend_theme").on("change", function () {
    //    map_init($("#admin_info").val());
    //});

    $("#legend_info_opt").change(function () {
        if (this.checked) {
            legend_open_close("legend_info", "open", "left");
        } else {
            legend_open_close("legend_info", "close", "left");
        }
        return false;
    });

    $("#legend_opt").change(function () {
        if (this.checked) {
            legend_open_close("legend", "open", "right");
        } else {
            legend_open_close("legend", "close", "right");
        }
        return false;
    });


    $("#basic_survey").on("change", function () { set_data_layer(this.checked); });

    $("#eqp_loc").on("change", function () { set_equipment_locations(this.checked); });


    //tt();	
    //add_admin_boundary("div");

    return true;
}


//if ($("#basic_survey").prop("checked")) {
//    add_data_layer();
//} else {
//    dataAdminCode = "union";

//    $("#admin_div").prop({ "checked": false, "disabled": false });
//    $("#admin_dist").prop({ "checked": false, "disabled": false });
//    $("#admin_upaz").prop({ "checked": false, "disabled": false });
//    $("#admin_union").prop({ "checked": false, "disabled": false });
//}

//{
//    "equipment_type": "Low Lift Pump (LLP)",
//        "address": "NSS, Pallabi Residential Area, Haji Bari Mosjid",
//        "road_or_village": "Pallabi Residential Area",
//        "mobile_no": "01712-795359",
//        "image_name": "680_1.jpg",
//        "lat": 22.1380571,
//        "long": 90.2302515,
//        "district_code": 1004,
//        "upazila_code": 100409,
//        "union_code": 10040902,
//        "mauza_code": 10040903400,
//        "district_name": "Barguna",
//        "upazila_name": "Amtali",
//        "union_name": "Ward No-02",
//        "mauza_name": "Kontakata(Dakshin)"
//}


//var markerClusters = new L.MarkerClusterGroup({
//    maxClusterRadius: 125,
//    //iconCreateFunction: function (cluster) {
//    //    var markers = cluster.getAllChildMarkers();
//    //    var n = 0;
//    //    for (var i = 0; i < markers.length; i++) {
//    //        n += markers[i].number;
//    //    }
//    //    return L.divIcon({ html: n, className: "mycluster", iconSize: L.point(40, 40) });
//    //},
//    ////Disable all of the defaults:
//    //spiderfyOnMaxZoom: false, showCoverageOnHover: false, zoomToBoundsOnClick: false
//});

//var eqp_loc = null;



//map.createPane("pane1").style.zIndex = 610;


function SetAdminData(dataAdminLevel, dataAdminInfo, dataViewLevel) {

    $("#map_data_content > #data_title > span.modal-title-txt").empty();
    $("#map_data_content > #data_content").empty();

    var slno = 0,
        dataCode = "",
        unionName = "",
        upazName = "",
        distName = "",
        divName = "",
        dataTitle = "▣ Survey Information",
        dataViewLevel = dataViewLevel && dataViewLevel == '1' ? 1 : 0;


    baseUrl = baseUrl ? baseUrl : "../SurveyInfoes/Index",
        dataUrl = "../MapViewer/GetSummaryData",
        queryStr = "?equipmentId=" +
        ((equipmentId && 0 < equipmentId && equipmentId < 4) ? equipmentId : "") +
        "&adminLevel=" + dataAdminLevel,
        table = $("<table>").addClass("table data-table no-select"),
        summarize_data = {};


    switch (dataAdminLevel) {
        case "div":
            dataCode = dataAdminInfo["div_code"];
            divName = dataAdminInfo["div_name"];

            dataTitle = "▣ Division Wise Survey Information";
            queryStr += "&divCode=" + dataCode;
            break;

        case "dist":
            dataCode = dataAdminInfo["dist_code"];
            distName = dataAdminInfo["dist_name"];
            divName = dataAdminInfo["div_name"];

            dataTitle = "▣ District Wise Survey Information";
            queryStr += "&distCode=" + dataCode;
            break;

        case "upaz":
            dataCode = dataAdminInfo["upaz_code"];
            upazName = dataAdminInfo["upaz_name"];
            distName = dataAdminInfo["dist_name"];
            divName = dataAdminInfo["div_name"];

            dataTitle = "▣ Upazila Wise Survey Information";
            queryStr += "&upazCode=" + dataCode;
            break;

        case "union":
            dataCode = dataAdminInfo["union_code"];
            unionName = dataAdminInfo["union_name"];
            upazName = dataAdminInfo["upaz_name"];
            distName = dataAdminInfo["dist_name"];
            divName = dataAdminInfo["div_name"];

            dataTitle = "▣ Union Wise Survey Information";
            queryStr += "&unionCode=" + dataCode;
            break;

        default:
            break;
    }

    dataTitle += " <span style='text-align:center; font-size:11px; white-space:nowrap;'><a class='data-base' target='_blank' href='" +
        baseUrl + queryStr +
        "' title='Survey data source'>data source</a></span>";

    $("#map_data_content > #data_title > span.modal-title-txt").html(dataTitle);

    $("#map_data_content > #data_content").append(table);


    if (divName) {
        table.append("<tr><td style='width:195px; font-weight:600;'>0" + ++slno + ". Division</td><td style='width:5px; font-weight:600;'>:</td><td style='width:auto;'>" + divName + "</td></tr>");
    }
    if (distName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". District</td><td style='font-weight:600;'>:</td><td>" + distName + "</td></tr>");
    }
    if (upazName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Upazila</td><td style='font-weight:600;'>:</td><td>" + upazName + "</td></tr>");
    }
    if (unionName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Union</td><td style='font-weight:600;'>:</td><td>" + unionName + "</td></tr>");
    }


    try {
        $.ajax({
            type: "Get",
            url: dataUrl + queryStr,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#busy-indicator").fadeIn();
            },
            success: function (allData) {
                summarize_data = allData;
            },
            error: function (e) {
                msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.responseText);
                $("#busy-indicator").fadeOut();
            },
            complete: function () {
                if (summarize_data) {

                    if (summarize_data.waterbodiesInfo && summarize_data.waterbodiesInfo.length > 0) {
                        summarize_data.waterbodiesInfo.map(function (wbInfo) {
                            table.append("<tr><td style='font-weight:600; vertical-align:top;'>0" + ++slno + ". Rivers Name</td><td style='font-weight:600; vertical-align:top;'>:</td><td style='text-align:justify; vertical-align:top;'>" +
                                (wbInfo.riversNames ? wbInfo.riversNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Rivers Length(Km)</td><td style='font-weight:600;'>:</td><td style='font-weight:600;'>" +
                                (wbInfo.totalRiversLength ? number_formatter(wbInfo.totalRiversLength / 1000, true, 2) + " (Km)" : "") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600; vertical-align:top;'>0" + ++slno + ". Waterbodies Name</td><td style='font-weight:600; vertical-align:top;'>:</td><td style='text-align:justify; vertical-align:top;'>" +
                                (wbInfo.waterbodiesNames ? wbInfo.waterbodiesNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Waterbodies Area(ha)</td><td style='font-weight:600;'>:</td><td style='font-weight:600;'>" +
                                (wbInfo.totalWaterbodiesArea ? number_formatter(wbInfo.totalWaterbodiesArea, true, 2) + " (ha)" : "") +
                                "</td></tr>");
                        });
                    }

                    var table1 = $("<table>").addClass("table data-table no-select");

                    $("#map_data_content > #data_content").append(table1);

                    if (summarize_data.surveyInfo && summarize_data.surveyInfo.length > 0) {

                        if (dataViewLevel == 1) {
                            table1.append("<tr><th rowspan='2'><p style='width:150px;'>Equipment Name</p></th><th rowspan='2'>Equipment (Count)</th><th colspan='8'>Irrigated Area (Acre)</th><th colspan='6'>Irrigation Cost (Tk/acre)</th><th colspan='3'>Benefited Farmer (Count)</th><th colspan='3'>Benefited Labour (Count)</th><th colspan='5'>Canal Length (Km)</th><th rowspan='2'>Conveyance Efficiency</th><th colspan='2'>Power Consumption</th><th colspan='2'>Abstraction of Water (M m<sup>3</sup>)</th></tr>" +
                                "<tr><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Mustard</th><th>Veg (W)</th><th>Other<sup>4</sup></th><th>Total</th><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Veg (W)</th><th>Other<sup>4</sup></th> <th>Male</th><th>Female</th><th>Total</th> <th>Male</th><th>Female</th><th>Total</th> <th>Pacca</th><th>Buried Pipe</th><th>Fita Pipe</th><th>Kacha</th><th>Total</th><th>Diesel (M ton)</th><th>Electricity (MW)</th><th>GW</th><th>SW</th></tr>");
                        } else {
                            table1.append("<tr><th rowspan='2'><p style='width:150px;'>Equipment Name</p></th><th rowspan='2'>Equipment (Count)</th><th colspan='8'>Irrigated Area (Acre)</th><th colspan='6'>Irrigation Cost (Tk/acre)</th><th rowspan='2'>Benefited Farmer (Count)</th><th rowspan='2'>Benefited Labour (Count)</th><th colspan='5'>Canal Length (Km)</th><th rowspan='2'>Conveyance Efficiency</th><th colspan='2'>Power Consumption</th><th colspan='2'>Abstraction of Water (M m<sup>3</sup>)</th></tr>" +
                                "<tr><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Mustard</th><th>Veg (W)</th><th>Other<sup>4</sup></th><th>Total</th><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Veg (W)</th><th>Other<sup>4</sup></th> <th>Pacca</th><th>Buried Pipe</th><th>Fita Pipe</th><th>Kacha</th><th>Total</th><th>Diesel (M ton)</th><th>Electricity (MW)</th><th>GW</th><th>SW</th></tr>");
                        }

                        var totalData = {
                            noOfEquipment: 0,

                            totalBoroArea: 0,
                            totalWheatArea: 0,
                            totalPotatoArea: 0,
                            totalMaizeArea: 0,
                            totalMustardArea: 0,
                            totalVegWinterArea: 0,
                            totalOthersArea: 0,
                            totalArea: 0,

                            avgBoroCost: 0,
                            avgWheatCost: 0,
                            avgPotatoCost: 0,
                            avgMaizeCost: 0,
                            avgVegWinterCost: 0,
                            avgOthersCost: 0,

                            totalBenefitedFarmer: 0,
                            totalBenefitedFarmerFemale: 0,
                            totalBenefitedFarmerMale: 0,

                            totalBenefitedAgricultureLabour: 0,
                            totalBenefitedAgricultureLabourFemale: 0,
                            totalBenefitedAgricultureLabourMale: 0,

                            totalCanalLengthPacca: 0,
                            totalCanalLengthBuriedPipe: 0,
                            totalCanalLengthFitaPipe: 0,
                            totalCanalLengthKacha: 0,
                            totalCanalLength: 0,

                            totalDistributionEfficiencyWeight: 0,

                            totalPowerConsumptionDiesel: 0,
                            totalPowerConsumptionElectric: 0,

                            totalAbstractionGW: 0,
                            totalAbstractionSW: 0,
                        };

                        summarize_data.surveyInfo.map(function (data) {
                            table1.append("<tr><td>" +
                                equipmentTypes[data.equipmentId] +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.noOfEquipment, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBoroArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalWheatArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPotatoArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMaizeArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMustardArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalVegWinterArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalOthersArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgBoroCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgWheatCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgPotatoCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgMaizeCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgVegWinterCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgOthersCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                (dataViewLevel == 1 ?
                                    number_formatter(data.totalBenefitedFarmerMale, true, 0) +
                                    "</td><td style='text-align:right;'>" +
                                    number_formatter(data.totalBenefitedFarmerFemale, true, 0) +
                                    "</td><td style='text-align:right;'>" : "") +
                                number_formatter(data.totalBenefitedFarmer, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                (dataViewLevel == 1 ?
                                    number_formatter(data.totalBenefitedAgricultureLabourMale, true, 0) +
                                    "</td><td style='text-align:right;'>" +
                                    number_formatter(data.totalBenefitedAgricultureLabourFemale, true, 0) +
                                    "</td><td style='text-align:right;'>" : "") +
                                number_formatter(data.totalBenefitedAgricultureLabour, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthPacca / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthBuriedPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthFitaPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthKacha / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLength / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.distributionEfficiency, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionDiesel, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionElectric, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionGW / 1000000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionSW / 1000000, true, 2) +
                                "</td></tr>");

                            if (!equipmentId || 1 > equipmentId || equipmentId > 3) {
                                totalData.noOfEquipment += data.noOfEquipment;

                                totalData.totalBoroArea += data.totalBoroArea;
                                totalData.totalWheatArea += data.totalWheatArea;
                                totalData.totalPotatoArea += data.totalPotatoArea;
                                totalData.totalMaizeArea += data.totalMaizeArea;
                                totalData.totalMustardArea += data.totalMustardArea;
                                totalData.totalVegWinterArea += data.totalVegWinterArea;
                                totalData.totalOthersArea += data.totalOthersArea;
                                totalData.totalArea += data.totalArea;

                                totalData.avgBoroCost += data.avgBoroCost * data.totalBoroArea;
                                totalData.avgWheatCost += data.avgWheatCost * data.totalWheatArea;
                                totalData.avgPotatoCost += data.avgPotatoCost * data.totalPotatoArea;
                                totalData.avgMaizeCost += data.avgMaizeCost * data.totalMaizeArea;
                                totalData.avgVegWinterCost += data.avgVegWinterCost * data.totalVegWinterArea;
                                totalData.avgOthersCost += data.avgOthersCost * data.totalOthersArea;

                                totalData.totalBenefitedFarmer += data.totalBenefitedFarmer;
                                totalData.totalBenefitedFarmerFemale += data.totalBenefitedFarmerFemale;
                                totalData.totalBenefitedFarmerMale += data.totalBenefitedFarmerMale;

                                totalData.totalBenefitedAgricultureLabour += data.totalBenefitedAgricultureLabour;
                                totalData.totalBenefitedAgricultureLabourFemale += data
                                    .totalBenefitedAgricultureLabourFemale;
                                totalData.totalBenefitedAgricultureLabourMale += data.totalBenefitedAgricultureLabourMale;

                                totalData.totalCanalLengthPacca += data.totalCanalLengthPacca;
                                totalData.totalCanalLengthBuriedPipe += data.totalCanalLengthBuriedPipe;
                                totalData.totalCanalLengthFitaPipe += data.totalCanalLengthFitaPipe;
                                totalData.totalCanalLengthKacha += data.totalCanalLengthKacha;
                                totalData.totalCanalLength += data.totalCanalLength;

                                totalData.totalDistributionEfficiencyWeight += data.distributionEfficiency *
                                    data.totalCanalLength;

                                totalData.totalPowerConsumptionDiesel += data.totalPowerConsumptionDiesel;
                                totalData.totalPowerConsumptionElectric += data.totalPowerConsumptionElectric;

                                totalData.totalAbstractionGW += data.totalAbstractionGW;
                                totalData.totalAbstractionSW += data.totalAbstractionSW;
                            }

                        });

                        if (!equipmentId || 1 > equipmentId || equipmentId > 3) {

                            table1.append("<tr><td style='font-weight:600;'>" +
                                "All Equipments" +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.noOfEquipment, true, 0) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalBoroArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalWheatArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalPotatoArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalMaizeArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalMustardArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalVegWinterArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalOthersArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalArea, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (totalData.totalBoroArea ? number_formatter(totalData.avgBoroCost / totalData.totalBoroArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (totalData.totalWheatArea ? number_formatter(totalData.avgWheatCost / totalData.totalWheatArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (totalData.totalPotatoArea ? number_formatter(totalData.avgPotatoCost / totalData.totalPotatoArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (totalData.totalMaizeArea ? number_formatter(totalData.avgMaizeCost / totalData.totalMaizeArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (totalData.totalVegWinterArea ? number_formatter(totalData.avgVegWinterCost / totalData.totalVegWinterArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (totalData.totalOthersArea ? number_formatter(totalData.avgOthersCost / totalData.totalOthersArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (dataViewLevel == 1 ?
                                    number_formatter(totalData.totalBenefitedFarmerMale, true, 0) +
                                    "</td><td style='text-align:right; font-weight:600;'>" +
                                    number_formatter(totalData.totalBenefitedFarmerFemale, true, 0) +
                                    "</td><td style='text-align:right; font-weight:600;'>" : "") +
                                number_formatter(totalData.totalBenefitedFarmer, true, 0) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (dataViewLevel == 1 ?
                                    number_formatter(totalData.totalBenefitedAgricultureLabourMale, true, 0) +
                                    "</td><td style='text-align:right; font-weight:600;'>" +
                                    number_formatter(totalData.totalBenefitedAgricultureLabourFemale, true, 0) +
                                    "</td><td style='text-align:right; font-weight:600;'>" : "") +
                                number_formatter(totalData.totalBenefitedAgricultureLabour, true, 0) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalCanalLengthPacca / 1000, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalCanalLengthBuriedPipe / 1000, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalCanalLengthFitaPipe / 1000, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalCanalLengthKacha / 1000, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalCanalLength / 1000, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                (totalData.totalCanalLength ? number_formatter(totalData.totalDistributionEfficiencyWeight / totalData.totalCanalLength, true, 2) : "0.00") +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalPowerConsumptionDiesel, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalPowerConsumptionElectric, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalAbstractionGW / 1000000, true, 2) +
                                "</td><td style='text-align:right; font-weight:600;'>" +
                                number_formatter(totalData.totalAbstractionSW / 1000000, true, 2) +
                                "</td></tr>");
                        }
                    }
                } else {
                    $("#map_data_content > #data_content").html("<p class='error'>Survey data not available !!</p>");
                }

                $("#busy-indicator").fadeOut();
            }

        });
    } catch (e) {
        msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.error);
        $("#busy-indicator").fadeOut();
    }


    map_data_open_close("map_data", true);

    //legend_open_close("legend_info", "open", "left");

    return;
}

/*

//SetAdminData(dataAdminCode, dataCode, unionName, upazName, distName, divName)
function SetAdminData(dataAdminLevel, dataAdminInfo) {

    $("#legend_info_title").empty();
    $("#map_legend_infos").empty();

    $("#legend_info_title").text("Union Wise Survey Info.");

    var table = $("<table>").addClass("table data-table");

    $("#map_legend_infos").append(table);


    var slno = 0,
        summarize_data = {},
        dataCode = "",
        unionName = "",
        upazName = "",
        distName = "",
        divName = "",
        url = "../MapViewer/GetSummaryData?equipmentId="
            + ((equipmentId && 0 < equipmentId && equipmentId < 4) ? equipmentId : "")
            + "&adminLevel=" + dataAdminLevel;

    console.log(equipmentId);

    switch (dataAdminLevel) {
        case "div":
            dataCode = dataAdminInfo["div_code"];
            divName = dataAdminInfo["div_name"];

            url += "&divCode=" + dataCode;
            break;

        case "dist":
            dataCode = dataAdminInfo["dist_code"];
            distName = dataAdminInfo["dist_name"];
            divName = dataAdminInfo["div_name"];

            url += "&distCode=" + dataCode;
            break;

        case "upaz":
            dataCode = dataAdminInfo["upaz_code"];
            upazName = dataAdminInfo["upaz_name"];
            distName = dataAdminInfo["dist_name"];
            divName = dataAdminInfo["div_name"];

            url += "&upazCode=" + dataCode;
            break;

        case "union":
            dataCode = dataAdminInfo["union_code"];
            unionName = dataAdminInfo["union_name"];
            upazName = dataAdminInfo["upaz_name"];
            distName = dataAdminInfo["dist_name"];
            divName = dataAdminInfo["div_name"];

            url += "&unionCode=" + dataCode;
            break;

        default:
            break;
    }


    if (divName) {
        table.append("<tr><td style='width:195px; font-weight:600;'>0" + ++slno + ". Division</td><td style='width:5px; font-weight:600;'>:</td><td style='width:auto;'>" + divName + "</td></tr>");
    }
    if (distName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". District</td><td style='font-weight:600;'>:</td><td>" + distName + "</td></tr>");
    }
    if (upazName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Upazila</td><td style='font-weight:600;'>:</td><td>" + upazName + "</td></tr>");
    }
    if (unionName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Union</td><td style='font-weight:600;'>:</td><td>" + unionName + "</td></tr>");
    }


    try {
        $.ajax({
            type: "Get",
            url: url,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#busy-indicator").fadeIn();
            },
            success: function (allData) {
                summarize_data = allData;
                console.log(summarize_data);
                console.log(summarize_data.waterbodiesInfo);
            },
            error: function (e) {
                msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.responseText);
                $("#busy-indicator").fadeOut();
            },
            complete: function () {
                if (summarize_data) {

                    if (summarize_data.waterbodiesInfo && summarize_data.waterbodiesInfo.length > 0) {
                        summarize_data.waterbodiesInfo.map(function (wbInfo) {
                            table.append("<tr><td style='font-weight:600; vertical-align:top;'>0" + ++slno + ". Rivers Name</td><td style='font-weight:600; vertical-align:top;'>:</td><td style='text-align:justify; vertical-align:top;'>" +
                                (wbInfo.riversNames ? wbInfo.riversNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Rivers Length(Km)</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.totalRiversLength ? number_formatter(wbInfo.totalRiversLength / 1000, true, 2) + " (Km)" : "") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600; vertical-align:top;'>0" + ++slno + ". Waterbodies Name</td><td style='font-weight:600; vertical-align:top;'>:</td><td style='text-align:justify; vertical-align:top;'>" +
                                (wbInfo.waterbodiesNames ? wbInfo.waterbodiesNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Waterbodies Area(ha)</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.totalWaterbodiesArea ? number_formatter(wbInfo.totalWaterbodiesArea, true, 2) + " (ha)" : "") +
                                "</td></tr>");
                        });
                    }

                    var table1 = $("<table>").addClass("table data-table");

                    $("#map_legend_infos").append(table1);

                    if (summarize_data.surveyInfo && summarize_data.surveyInfo.length > 0) {

                        table1.append("<tr><th rowspan='2'><p style='width:150px;'>Equipment Name</p></th><th rowspan='2'>Equipment (Number)</th><th colspan='8'>Irrigated Area (Acre)</th><th colspan='6'>Irrigation Cost (Tk/acre)</th><th colspan='3'>Benefited Farmer</th><th colspan='3'>Benefited Labour</th><th colspan='5'>Canal Length (Km)</th><th rowspan='2'>Conveyance Efficiency</th><th colspan='2'>Power Consumption</th><th colspan='2'>Abstraction of Water (M m<sup>3</sup>)</th></tr>" +
                            "<tr><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Mustard</th><th>Veg (W)</th><th>Other<sup>4</sup></th><th>Total</th><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Veg (W)</th><th>Other<sup>4</sup></th> <th>Male</th><th>Female</th><th>Total</th> <th>Male</th><th>Female</th><th>Total</th> <th>Pacca</th><th>Buried Pipe</th><th>Fita Pipe</th><th>Kacha</th><th>Total</th><th>Diesel(M ton)</th><th>Electricity (MW)</th><th>GW</th><th>SW</th></tr>");

                        var totalData = {
                            noOfEquipment: 0,

                            totalBoroArea: 0,
                            totalWheatArea: 0,
                            totalPotatoArea: 0,
                            totalMaizeArea: 0,
                            totalMustardArea: 0,
                            totalVegWinterArea: 0,
                            totalOthersArea: 0,
                            totalArea: 0,

                            avgBoroCost: 0,
                            avgWheatCost: 0,
                            avgPotatoCost: 0,
                            avgMaizeCost: 0,
                            avgVegWinterCost: 0,
                            avgOthersCost: 0,

                            totalBenefitedFarmer: 0,
                            totalBenefitedFarmerFemale: 0,
                            totalBenefitedFarmerMale: 0,

                            totalBenefitedAgricultureLabour: 0,
                            totalBenefitedAgricultureLabourFemale: 0,
                            totalBenefitedAgricultureLabourMale: 0,

                            totalCanalLengthPacca: 0,
                            totalCanalLengthBuriedPipe: 0,
                            totalCanalLengthFitaPipe: 0,
                            totalCanalLengthKacha: 0,
                            totalCanalLength: 0,

                            totalDistributionEfficiencyWeight: 0,

                            totalPowerConsumptionDiesel: 0,
                            totalPowerConsumptionElectric: 0,

                            totalAbstractionGW: 0,
                            totalAbstractionSW: 0,
                        };

                        summarize_data.surveyInfo.map(function (data) {
                            table1.append("<tr><td>" +
                                equipmentTypes[data.equipmentId] +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.noOfEquipment, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBoroArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalWheatArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPotatoArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMaizeArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMustardArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalVegWinterArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalOthersArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgBoroCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgWheatCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgPotatoCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgMaizeCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgVegWinterCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgOthersCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmerMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmerFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmer, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabourMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabourFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabour, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthPacca / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthBuriedPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthFitaPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthKacha / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLength / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.distributionEfficiency, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionDiesel, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionElectric, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionGW / 1000000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionSW / 1000000, true, 2) +
                                "</td></tr>");

                            if (!equipmentId || 1 > equipmentId || equipmentId > 3) {
                                totalData.noOfEquipment += data.noOfEquipment;

                                totalData.totalBoroArea += data.totalBoroArea;
                                totalData.totalWheatArea += data.totalWheatArea;
                                totalData.totalPotatoArea += data.totalPotatoArea;
                                totalData.totalMaizeArea += data.totalMaizeArea;
                                totalData.totalMustardArea += data.totalMustardArea;
                                totalData.totalVegWinterArea += data.totalVegWinterArea;
                                totalData.totalOthersArea += data.totalOthersArea;
                                totalData.totalArea += data.totalArea;

                                totalData.avgBoroCost += data.avgBoroCost * data.totalBoroArea;
                                totalData.avgWheatCost += data.avgWheatCost * data.totalWheatArea;
                                totalData.avgPotatoCost += data.avgPotatoCost * data.totalPotatoArea;
                                totalData.avgMaizeCost += data.avgMaizeCost * data.totalMaizeArea;
                                totalData.avgVegWinterCost += data.avgVegWinterCost * data.totalVegWinterArea;
                                totalData.avgOthersCost += data.avgOthersCost * data.totalOthersArea;

                                totalData.totalBenefitedFarmer += data.totalBenefitedFarmer;
                                totalData.totalBenefitedFarmerFemale += data.totalBenefitedFarmerFemale;
                                totalData.totalBenefitedFarmerMale += data.totalBenefitedFarmerMale;

                                totalData.totalBenefitedAgricultureLabour += data.totalBenefitedAgricultureLabour;
                                totalData.totalBenefitedAgricultureLabourFemale += data
                                    .totalBenefitedAgricultureLabourFemale;
                                totalData.totalBenefitedAgricultureLabourMale += data.totalBenefitedAgricultureLabourMale;

                                totalData.totalCanalLengthPacca += data.totalCanalLengthPacca;
                                totalData.totalCanalLengthBuriedPipe += data.totalCanalLengthBuriedPipe;
                                totalData.totalCanalLengthFitaPipe += data.totalCanalLengthFitaPipe;
                                totalData.totalCanalLengthKacha += data.totalCanalLengthKacha;
                                totalData.totalCanalLength += data.totalCanalLength;

                                totalData.totalDistributionEfficiencyWeight += data.distributionEfficiency *
                                    data.totalCanalLength;

                                totalData.totalPowerConsumptionDiesel += data.totalPowerConsumptionDiesel;
                                totalData.totalPowerConsumptionElectric += data.totalPowerConsumptionElectric;

                                totalData.totalAbstractionGW += data.totalAbstractionGW;
                                totalData.totalAbstractionSW += data.totalAbstractionSW;
                            }

                        });

                        if (!equipmentId || 1 > equipmentId || equipmentId > 3) {

                            table1.append("<tr><td>" +
                                "All Equipments" +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.noOfEquipment, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalBoroArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalWheatArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalPotatoArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalMaizeArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalMustardArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalVegWinterArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalOthersArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                (totalData.totalBoroArea ? number_formatter(totalData.avgBoroCost / totalData.totalBoroArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right;'>" +
                                (totalData.totalWheatArea ? number_formatter(totalData.avgWheatCost / totalData.totalWheatArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right;'>" +
                                (totalData.totalPotatoArea ? number_formatter(totalData.avgPotatoCost / totalData.totalPotatoArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right;'>" +
                                (totalData.totalMaizeArea ? number_formatter(totalData.avgMaizeCost / totalData.totalMaizeArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right;'>" +
                                (totalData.totalVegWinterArea ? number_formatter(totalData.avgVegWinterCost / totalData.totalVegWinterArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right;'>" +
                                (totalData.totalOthersArea ? number_formatter(totalData.avgOthersCost / totalData.totalOthersArea, true, 2) : "0.00") +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalBenefitedFarmerMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalBenefitedFarmerFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalBenefitedFarmer, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalBenefitedAgricultureLabourMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalBenefitedAgricultureLabourFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalBenefitedAgricultureLabour, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalCanalLengthPacca / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalCanalLengthBuriedPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalCanalLengthFitaPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalCanalLengthKacha / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalCanalLength / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                (totalData.totalCanalLength ? number_formatter(totalData.totalDistributionEfficiencyWeight / totalData.totalCanalLength, true, 2) : "0.00") +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalPowerConsumptionDiesel, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalPowerConsumptionElectric, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalAbstractionGW / 1000000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(totalData.totalAbstractionSW / 1000000, true, 2) +
                                "</td></tr>");
                        }
                    }
                } else {
                    $("#map_legend_infos").append("<p class='error'>Survey data not available !!</p>");
                }

                $("#busy-indicator").fadeOut();
            }

        });
    } catch (e) {
        msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.error);
        $("#busy-indicator").fadeOut();
    }


    map_data_open_close("map_data", true);

    legend_open_close("legend_info", "open", "left");

    return;
}

function SetAdminData_bk(dataAdminCode, dataCode, unionName, upazName, distName, divName) {

    $("#legend_info_title").empty();
    $("#map_legend_infos").empty();

    $("#legend_info_title").text("Union Wise Survey Info.");

    var table = $("<table>").addClass("table data-table");

    $("#map_legend_infos").append(table);

    var slno = 1;

    if (divName) {
        table.append("<tr><td style='width:195px; font-weight:600;'>0" + ++slno + ". Division</td><td style='width:5px; font-weight:600;'>:</td><td style='width:auto;'>" + divName + "</td></tr>");
        //++slno;
    }
    if (distName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". District</td><td style='font-weight:600;'>:</td><td>" + distName + "</td></tr>");
        ++slno;
    }
    if (upazName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Upazila</td><td style='font-weight:600;'>:</td><td>" + upazName + "</td></tr>");
        //++slno;
    }
    if (unionName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Union</td><td style='font-weight:600;'>:</td><td>" + unionName + "</td></tr>");
        //++slno;
    }



    var survey_summary = {},
        url = "../MapViewer/GetSummaryData?adminLevel=" + dataAdminCode;
    //url = "../MapViewer/GetSummaryData?unionCode=" + dataCode;

    switch (dataAdminCode) {
        case "div":
            url = "../MapViewer/GetSummaryData?adminLevel=" + dataAdminCode + "&divCode=" + dataCode;
            break;

        case "dist":
            url = "../MapViewer/GetSummaryData?adminLevel=" + dataAdminCode + "&distCode=" + dataCode;
            break;

        case "upaz":
            url = "../MapViewer/GetSummaryData?adminLevel=" + dataAdminCode + "&upazCode=" + dataCode;
            break;

        case "union":
            url = "../MapViewer/GetSummaryData?adminLevel=" + dataAdminCode + "&unionCode=" + dataCode;
            break;

        default:
            break;
    }

    //return;
    try {
        $.ajax({
            type: "Get",
            //url: "../MapViewer/GetMapData?equipmentId=" + equipmentId + "&adminLevel="+dataAdminCode+"&divCode="++"&distCode="++"&upazCode=" + condition,
            //url: "../MapViewer/GetUnionWiseMapData?equipmentId=" + equipmentId + "&adminLevel=" + dataAdminCode,
            url: url,
            //url: "../MapViewer/GetUnionWiseMapData?unionCode=" + dataCode,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#busy-indicator").fadeIn();
            },
            success: function (allData) {
                survey_summary = allData;
                console.log(survey_summary.waterbodiesInfo);
            },
            error: function (e) {
                //console.log(e.responseText);
                msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.responseText);
                $("#busy-indicator").fadeOut();
            },
            complete: function () {
                if (survey_summary) {

                    if (survey_summary.waterbodiesInfo && survey_summary.waterbodiesInfo.length > 0) {
                        survey_summary.waterbodiesInfo.map(function (wbInfo) {
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Rivers Name</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.riversNames ? wbInfo.riversNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Rivers Length(Km)</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.totalRiversLength ? number_formatter(wbInfo.totalRiversLength / 1000, true, 2) + " (Km)" : "") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Waterbodies Name</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.waterbodiesNames ? wbInfo.waterbodiesNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Waterbodies Area(ha)</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.totalWaterbodiesArea ? number_formatter(wbInfo.totalWaterbodiesArea, true, 2) + " (ha)" : "") +
                                "</td></tr>");
                        });
                    }

                    var table1 = $("<table>").addClass("table data-table");

                    $("#map_legend_infos").append(table1);

                    if (survey_summary.surveyInfo && survey_summary.surveyInfo.length > 0) {

                        table1.append("<tr><th rowspan='2'><p style='width:150px;'>Equipment Name</p></th><th rowspan='2'>Equipment (Number)</th><th colspan='8'>Irrigated Area (Acre)</th><th colspan='6'>Irrigation Cost (Tk/acre)</th><th colspan='3'>Benefited Farmer</th><th colspan='3'>Benefited Labour</th><th colspan='5'>Canal Length (Km)</th><th rowspan='2'>Conveyance Efficiency</th><th colspan='2'>Power Consumption</th><th colspan='2'>Abstraction of Water (M m<sup>3</sup>)</th></tr>" +
                            "<tr><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Mustard</th><th>Veg (W)</th><th>Other<sup>4</sup></th><th>Total</th><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Veg (W)</th><th>Other<sup>4</sup></th> <th>Male</th><th>Female</th><th>Total</th> <th>Male</th><th>Female</th><th>Total</th> <th>Pacca</th><th>Buried Pipe</th><th>Fita Pipe</th><th>Kacha</th><th>Total</th><th>Diesel(M ton)</th><th>Electricity (MW)</th><th>GW</th><th>SW</th></tr>");

                        var totalData = {
                            noOfEquipment: 0,

                            totalBoroArea: 0,
                            totalWheatArea: 0,
                            totalPotatoArea: 0,
                            totalMaizeArea: 0,
                            totalMustardArea: 0,
                            totalVegWinterArea: 0,
                            totalOthersArea: 0,
                            totalArea: 0,

                            avgBoroCost: 0,
                            avgWheatCost: 0,
                            avgPotatoCost: 0,
                            avgMaizeCost: 0,
                            avgVegWinterCost: 0,
                            avgOthersCost: 0,

                            totalBenefitedFarmer: 0,
                            totalBenefitedFarmerFemale: 0,
                            totalBenefitedFarmerMale: 0,

                            totalBenefitedAgricultureLabour: 0,
                            totalBenefitedAgricultureLabourFemale: 0,
                            totalBenefitedAgricultureLabourMale: 0,

                            totalCanalLengthPacca: 0,
                            totalCanalLengthBuriedPipe: 0,
                            totalCanalLengthFitaPipe: 0,
                            totalCanalLengthKacha: 0,
                            totalCanalLength: 0,

                            totalDistributionEfficiencyWeight: 0,

                            totalPowerConsumptionDiesel: 0,
                            totalPowerConsumptionElectric: 0,

                            totalAbstractionGW: 0,
                            totalAbstractionSW: 0,
                        };

                        survey_summary.surveyInfo.map(function (data) {
                            //console.log(data);
                            table1.append("<tr><td>" +
                                equipmentTypes[data.equipmentId] +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.noOfEquipment, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBoroArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalWheatArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPotatoArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMaizeArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMustardArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalVegWinterArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalOthersArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgBoroCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgWheatCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgPotatoCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgMaizeCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgVegWinterCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgOthersCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmerMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmerFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmer, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabourMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabourFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabour, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthPacca / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthBuriedPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthFitaPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthKacha / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLength / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.distributionEfficiency, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionDiesel, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionElectric, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionGW / 1000000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionSW / 1000000, true, 2) +
                                "</td></tr>");


                            totalData.noOfEquipment += data.noOfEquipment;

                            totalData.totalBoroArea += data.totalBoroArea;
                            totalData.totalWheatArea += data.totalWheatArea;
                            totalData.totalPotatoArea += data.totalPotatoArea;
                            totalData.totalMaizeArea += data.totalMaizeArea;
                            totalData.totalMustardArea += data.totalMustardArea;
                            totalData.totalVegWinterArea += data.totalVegWinterArea;
                            totalData.totalOthersArea += data.totalOthersArea;
                            totalData.totalArea += data.totalArea;

                            totalData.avgBoroCost += data.avgBoroCost * data.totalBoroArea;
                            totalData.avgWheatCost += data.avgWheatCost * data.totalWheatArea;
                            totalData.avgPotatoCost += data.avgPotatoCost * data.totalPotatoArea;
                            totalData.avgMaizeCost += data.avgMaizeCost * data.totalMaizeArea;
                            totalData.avgVegWinterCost += data.avgVegWinterCost * data.totalVegWinterArea;
                            totalData.avgOthersCost += data.avgOthersCost * data.totalOthersArea;

                            totalData.totalBenefitedFarmer += data.totalBenefitedFarmer;
                            totalData.totalBenefitedFarmerFemale += data.totalBenefitedFarmerFemale;
                            totalData.totalBenefitedFarmerMale += data.totalBenefitedFarmerMale;

                            totalData.totalBenefitedAgricultureLabour += data.totalBenefitedAgricultureLabour;
                            totalData.totalBenefitedAgricultureLabourFemale += data.totalBenefitedAgricultureLabourFemale;
                            totalData.totalBenefitedAgricultureLabourMale += data.totalBenefitedAgricultureLabourMale;

                            totalData.totalCanalLengthPacca += data.totalCanalLengthPacca;
                            totalData.totalCanalLengthBuriedPipe += data.totalCanalLengthBuriedPipe;
                            totalData.totalCanalLengthFitaPipe += data.totalCanalLengthFitaPipe;
                            totalData.totalCanalLengthKacha += data.totalCanalLengthKacha;
                            totalData.totalCanalLength += data.totalCanalLength;

                            totalData.totalDistributionEfficiencyWeight += data.distributionEfficiency * data.totalCanalLength;

                            totalData.totalPowerConsumptionDiesel += data.totalPowerConsumptionDiesel;
                            totalData.totalPowerConsumptionElectric += data.totalPowerConsumptionElectric;

                            totalData.totalAbstractionGW += data.totalAbstractionGW;
                            totalData.totalAbstractionSW += data.totalAbstractionSW;
                        });

                        table1.append("<tr><td>" +
                            "All Equipments" +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.noOfEquipment, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBoroArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalWheatArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalPotatoArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalMaizeArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalMustardArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalVegWinterArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalOthersArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalBoroArea ? number_formatter(totalData.avgBoroCost / totalData.totalBoroArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalWheatArea ? number_formatter(totalData.avgWheatCost / totalData.totalWheatArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalPotatoArea ? number_formatter(totalData.avgPotatoCost / totalData.totalPotatoArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalMaizeArea ? number_formatter(totalData.avgMaizeCost / totalData.totalMaizeArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalVegWinterArea ? number_formatter(totalData.avgVegWinterCost / totalData.totalVegWinterArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalOthersArea ? number_formatter(totalData.avgOthersCost / totalData.totalOthersArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedFarmerMale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedFarmerFemale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedFarmer, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedAgricultureLabourMale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedAgricultureLabourFemale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedAgricultureLabour, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthPacca / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthBuriedPipe / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthFitaPipe / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthKacha / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLength / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalCanalLength ? number_formatter(totalData.totalDistributionEfficiencyWeight / totalData.totalCanalLength, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalPowerConsumptionDiesel, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalPowerConsumptionElectric, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalAbstractionGW / 1000000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalAbstractionSW / 1000000, true, 2) +
                            "</td></tr>");
                    }
                } else {
                    $("#map_legend_infos").append("<p class='error'>Survey data not available !!</p>");
                }

                $("#busy-indicator").fadeOut();
            }

        });
    } catch (e) {
        msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.error);
        $("#busy-indicator").fadeOut();
    }

    return;
}

//function SetUnionData() {
function SetUnionData_bk(unionCode, unionName, distName, divName) {
    //console.log(unionCode);
    //console.log(unionName);
    //console.log(distName);
    //console.log(divName);
    //$("#map_legend_infos").html();
    //console.log($("#map_legend_infos"));

    $("#legend_info_title").empty();
    $("#map_legend_infos").empty();

    $("#legend_info_title").text("Union Wise Survey Info.");

    var table = $("<table>").addClass("table data-table");

    $("#map_legend_infos").append(table);


    //var modal_content = $("<div>").addClass("modal-content");

    //modal_content.append("<div class='modal-title'><span class='modal-title-txt'>▣ " + "Union Wise Survey Info." + "</span></div>");
    //var data_content = $("<div>").addClass("option-content");

    //modal_content.append(data_content);

    //var table = $("<table>").addClass("table data-table");
    //data_content.append(table);

    table.append("<tr><td style='width:150px;'>Union</td><td style='width:10px;'>:</td><td style='width:85%;'>" + unionName + "</td></tr>");
    table.append("<tr><td>District</td><td>:</td><td>" + distName + "</td></tr>");
    table.append("<tr><td>Division</td><td>:</td><td>" + divName + "</td></tr>");

    //$("#map_legend_infos").append("<p> Union Name : " +
    //    unionName +
    //    "</p>" +
    //    "<p> District Name : " +
    //    distName +
    //    "</p>" +
    //    "<p> Division Name : " +
    //    divName +
    //    "</p>");

    var union_info = {};
    //return;
    try {
        $.ajax({
            type: "Get",
            //url: "../MapViewer/GetMapData?equipmentId=" + equipmentId + "&adminLevel="+dataAdminCode+"&divCode="++"&distCode="++"&upazCode=" + condition,
            //url: "../MapViewer/GetUnionWiseMapData?equipmentId=" + equipmentId + "&adminLevel=" + dataAdminCode,
            //url: url,
            url: "../MapViewer/GetUnionWiseMapData?unionCode=" + unionCode,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#busy-indicator").fadeIn();
            },
            success: function (allData) {
                union_info = allData;
                //console.log(union_info);
            },
            error: function (e) {
                //console.log(e.responseText);
                msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.responseText);
                $("#busy-indicator").fadeOut();
            },
            complete: function () {
                if (union_info) {

                    if (union_info.waterbodiesInfo && union_info.waterbodiesInfo.length > 0) {
                        union_info.waterbodiesInfo.map(function (wbInfo) {
                            table.append("<tr><td>Rivers Names</td><td>:</td><td>" +
                                (wbInfo.riversNames ? wbInfo.riversNames : "") +
                                "</td></tr>");
                            table.append("<tr><td>Rivers Length</td><td>:</td><td>" +
                                (wbInfo.totalRiversLength ? number_formatter(wbInfo.totalRiversLength, true, 2) : "") +
                                "</td></tr>");
                            table.append("<tr><td>Waterbodies Names</td><td>:</td><td>" +
                                (wbInfo.waterbodiesNames ? wbInfo.waterbodiesNames : "") +
                                "</td></tr>");
                            table.append("<tr><td>Waterbodies Area(ha)</td><td>:</td><td>" +
                                (wbInfo.totalWaterbodiesArea ? number_formatter(wbInfo.totalWaterbodiesArea, true, 2) : "") +
                                "</td></tr>");
                        });
                    }

                    var table1 = $("<table>").addClass("table data-table"),
                        table2 = $("<table>").addClass("table data-table");

                    $("#map_legend_infos").append(table1);
                    $("#map_legend_infos").append(table2);
                    //data_content.append(table1);
                    //data_content.append(table2);

                    //surveyInfo
                    //waterbodiesInfo
                    //console.log(union_info.surveyInfo);

                    if (union_info.surveyInfo && union_info.surveyInfo.length > 0) {

                        table1.append("<tr><th rowspan='2'><p style='width:150px;'>Equipment Name</p></th><th rowspan='2'>Equipment (Number)</th><th colspan='7'>Irrigated Area (Acre)</th><th colspan='6'>Irrigation Cost (Tk/acre)</th><th rowspan='2'>Benefited Farmer</th><th rowspan='2'>Benefited Labour</th></tr><tr><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Mustard</th><th>Veg (W)</th><th>Other<sup>4</sup></th><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Veg (W)</th><th>Other<sup>4</sup></th></tr>");

                        table2.append("<tr><th rowspan='2'>Equipment Name</th><th colspan='4'>Canal Length (m)</th><th rowspan='2'>Conveyance Efficiency</th><th colspan='2'>Power Consumption</th><th colspan='2'>Abstraction of Water (m<sup>3</sup>)</th><th colspan='2'>Available Water Source</th></tr>  <tr><th>Pacca</th><th>Buried Pipe</th><th>Fita Pipe</th><th>Kacha</th><th>Diesel(M ton)</th><th>Electricity (MW)</th><th>GW</th><th>SW</th><th>River (Km)</th><th>Water bodies (Acre)</th></tr>");

                        var totalData = {
                            noOfEquipment: 0,

                            totalBoroArea: 0,
                            totalWheatArea: 0,
                            totalPotatoArea: 0,
                            totalMaizeArea: 0,
                            totalMustardArea: 0,
                            totalVegWinterArea: 0,
                            totalOthersArea: 0,
                            totalArea: 0,

                            avgBoroCost: 0,
                            avgWheatCost: 0,
                            avgPotatoCost: 0,
                            avgMaizeCost: 0,
                            avgVegWinterCost: 0,
                            avgOthersCost: 0,

                            totalBenefitedFarmer: 0,
                            totalBenefitedFarmerFemale: 0,
                            totalBenefitedFarmerMale: 0,

                            totalBenefitedAgricultureLabour: 0,
                            totalBenefitedAgricultureLabourFemale: 0,
                            totalBenefitedAgricultureLabourMale: 0,

                            totalCanalLengthPacca: 0,
                            totalCanalLengthBuriedPipe: 0,
                            totalCanalLengthFitaPipe: 0,
                            totalCanalLengthKacha: 0,
                            totalCanalLength: 0,

                            distributionEfficiency: 0,

                            totalPowerConsumptionDiesel: 0,
                            totalPowerConsumptionElectric: 0,

                            totalAbstractionGW: 0,
                            totalAbstractionSW: 0,
                        };

                        union_info.surveyInfo.map(function (data) {
                            //console.log(data);
                            table1.append("<tr><td>" +
                                equipmentTypes[data.equipmentId] +
                                "</td><td>" +
                                number_formatter(data.noOfEquipment, true, 0) +
                                "</td><td>" +
                                number_formatter(data.totalBoroArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalWheatArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalPotatoArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalMaizeArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalMustardArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalVegWinterArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalOthersArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalOthersArea, true, 2) +
                                "</td><td>" +
                                number_formatter(data.avgBoroCost, true, 2) +
                                "</td><td>" +
                                number_formatter(data.avgWheatCost, true, 2) +
                                "</td><td>" +
                                number_formatter(data.avgPotatoCost, true, 2) +
                                "</td><td>" +
                                number_formatter(data.avgMaizeCost, true, 2) +
                                "</td><td>" +
                                number_formatter(data.avgVegWinterCost, true, 2) +
                                "</td><td>" +
                                number_formatter(data.avgOthersCost, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalBenefitedFarmer, true, 0) +
                                "</td><td>" +
                                number_formatter(data.totalBenefitedAgricultureLabour, true, 0) +
                                "</td></tr>");

                            table2.append("<tr><td>" +
                                equipmentTypes[data.equipmentId] +
                                "</td><td>" +
                                number_formatter(data.totalCanalLengthPacca, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalCanalLengthBuriedPipe, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalCanalLengthFitaPipe, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalCanalLengthKacha, true, 2) +
                                "</td><td>" +
                                number_formatter(data.distributionEfficiency, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalPowerConsumptionDiesel, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalPowerConsumptionElectric, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalAbstractionGW, true, 2) +
                                "</td><td>" +
                                number_formatter(data.totalAbstractionSW, true, 2) +

                                "</td><td>" +
                                //data.avgWheatCost +
                                "</td><td>" +
                                //data.avgPotatoCost +
                                //"</td><td>" +
                                //data.avgMaizeCost +
                                //"</td><td>" +
                                //data.avgVegWinterCost +
                                //"</td><td>" +
                                //data.avgOthersCost +
                                //"</td><td>" +
                                //data.totalBenefitedFarmer +
                                //"</td><td>" +
                                //data.totalBenefitedAgricultureLabour +
                                "</td></tr>");


                            totalData.noOfEquipment += data.noOfEquipment;

                            totalData.totalBoroArea += data.totalBoroArea;
                            totalData.totalWheatArea += data.totalWheatArea;
                            totalData.totalPotatoArea += data.totalPotatoArea;
                            totalData.totalMaizeArea += data.totalMaizeArea;
                            totalData.totalMustardArea += data.totalMustardArea;
                            totalData.totalVegWinterArea += data.totalVegWinterArea;
                            totalData.totalOthersArea += data.totalOthersArea;
                            totalData.totalArea += data.totalArea;

                            totalData.avgBoroCost += data.avgBoroCost * data.totalBoroArea;
                            totalData.avgWheatCost += data.avgWheatCost * data.totalWheatArea;
                            totalData.avgPotatoCost += data.avgPotatoCost * data.totalPotatoArea;
                            totalData.avgMaizeCost += data.avgMaizeCost * data.totalMaizeArea;
                            totalData.avgVegWinterCost += data.avgVegWinterCost * data.totalVegWinterArea;
                            totalData.avgOthersCost += data.avgOthersCost * data.totalOthersArea;

                            totalData.totalBenefitedFarmer += data.totalBenefitedFarmer;
                            totalData.totalBenefitedFarmerFemale += data.totalBenefitedFarmerFemale;
                            totalData.totalBenefitedFarmerMale += data.totalBenefitedFarmerMale;

                            totalData.totalBenefitedAgricultureLabour += data.totalBenefitedAgricultureLabour;
                            totalData.totalBenefitedAgricultureLabourFemale += data.totalBenefitedAgricultureLabourFemale;
                            totalData.totalBenefitedAgricultureLabourMale += data.totalBenefitedAgricultureLabourMale;

                            totalData.totalCanalLengthPacca += data.totalCanalLengthPacca;
                            totalData.totalCanalLengthBuriedPipe += data.totalCanalLengthBuriedPipe;
                            totalData.totalCanalLengthFitaPipe += data.totalCanalLengthFitaPipe;
                            totalData.totalCanalLengthKacha += data.totalCanalLengthKacha;
                            totalData.totalCanalLength += data.totalCanalLength;

                            totalData.distributionEfficiency += data.distributionEfficiency;

                            totalData.totalPowerConsumptionDiesel += data.totalPowerConsumptionDiesel;
                            totalData.totalPowerConsumptionElectric += data.totalPowerConsumptionElectric;

                            totalData.totalAbstractionGW += data.totalAbstractionGW;
                            totalData.totalAbstractionSW += data.totalAbstractionSW;
                        });

                        table1.append("<tr><td>" +
                            "All Equipments" +
                            "</td><td>" +
                            number_formatter(totalData.noOfEquipment, true, 0) +
                            "</td><td>" +
                            number_formatter(totalData.totalBoroArea, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalWheatArea, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalPotatoArea, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalMaizeArea, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalMustardArea, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalVegWinterArea, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalOthersArea, true, 2) +
                            "</td><td>" +
                            (totalData.totalBoroArea ? number_formatter(totalData.avgBoroCost / totalData.totalBoroArea, true, 2) : "0.00") +
                            "</td><td>" +
                            (totalData.totalWheatArea ? number_formatter(totalData.avgWheatCost / totalData.totalWheatArea, true, 2) : "0.00") +
                            "</td><td>" +
                            (totalData.totalPotatoArea ? number_formatter(totalData.avgPotatoCost / totalData.totalPotatoArea, true, 2) : "0.00") +
                            "</td><td>" +
                            (totalData.totalMaizeArea ? number_formatter(totalData.avgMaizeCost / totalData.totalMaizeArea, true, 2) : "0.00") +
                            "</td><td>" +
                            (totalData.totalVegWinterArea ? number_formatter(totalData.avgVegWinterCost / totalData.totalVegWinterArea, true, 2) : "0.00") +
                            "</td><td>" +
                            (totalData.totalOthersArea ? number_formatter(totalData.avgOthersCost / totalData.totalOthersArea, true, 2) : "0.00") +
                            "</td><td>" +
                            number_formatter(totalData.totalBenefitedFarmer, true, 0) +
                            "</td><td>" +
                            number_formatter(totalData.totalBenefitedAgricultureLabour, true, 0) +
                            "</td></tr>");


                        table2.append("<tr><td>" +
                            "All Equipments" +
                            "</td><td>" +
                            number_formatter(totalData.totalCanalLengthPacca, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalCanalLengthBuriedPipe, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalCanalLengthFitaPipe, true, 2) +
                            "</td><td>" +
                            number_formatter(totalData.totalCanalLengthKacha, true, 2) +

                            "</td><td>" +

                            "</td><td>" +
                            "</td><td>" +

                            "</td><td>" +
                            "</td><td>" +

                            "</td><td>" +
                            "</td><td>" +
                            "</td></tr>");
                    }
                } else {
                    $("#map_legend_infos").append("<p class='error'>Survey data not available !!</p>");
                    //data_content.append("<p class='error'>Survey data not available !!</p>");
                }

                $("#busy-indicator").fadeOut();
            }

        });
    } catch (e) {
        msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.error);
        $("#busy-indicator").fadeOut();
    }
    return;
    //return modal_content;

    ///
    //var data = {
    //    avgBoroCost: 1138.35,
    //    avgMaizeCost: null,
    //    avgOthersCost: 948.25,
    //    avgPotatoCost: null,
    //    avgVegWinterCost: 12411.52,
    //    avgWheatCost: null,
    //    distributionEfficiency: 0.54,
    //    equipmentId: 1,
    //    id: 60177,
    //    noOfEquipment: 38,
    //    totalAbstractionGW: 0,
    //    totalAbstractionSW: 2887775.02,
    //    totalArea: 169.88,
    //    totalBenefitedAgricultureLabour: 13187,
    //    totalBenefitedAgricultureLabourFemale: 0,
    //    totalBenefitedAgricultureLabourMale: 13187,
    //    totalBenefitedFarmer: 1313,
    //    totalBenefitedFarmerFemale: 0,
    //    totalBenefitedFarmerMale: 1313,
    //    totalBoroArea: 148.9,
    //    totalCanalLength: 34371,
    //    totalCanalLengthBuriedPipe: 0,
    //    totalCanalLengthFitaPipe: 3400,
    //    totalCanalLengthKacha: 30971,
    //    totalCanalLengthPacca: 0,
    //    totalMaizeArea: 0,
    //    totalMustardArea: 0,
    //    totalOthersArea: 18.55,
    //    totalPotatoArea: 0,
    //    totalPowerConsumptionDiesel: 59.76,
    //    totalPowerConsumptionElectric: 0.27,
    //    totalVegWinterArea: 2.43,
    //    totalWheatArea: 0,
    //    unionCode: "20199410"
    //};

    ////SetUnionData(unionCode, unionName, distName, divName);

    //$("#map_legend_infos").append("<p> Union Name : " +
    //    unionName +
    //    "</p>" +
    //    "<p> District Name : " +
    //    distName +
    //    "</p>" +
    //    "<p> Division Name : " +
    //    divName +
    //    "</p>");

    //var table = $("<table>").addClass("table data-table");

    //$("#map_legend_infos").empty();
    //$("#map_legend_infos").append(table);

    ////table.append("<tr><th>" + dataAdminName + "</th> <th>" + dataName + "</th></tr>");
    ////table.append("<tr><td rowspan='2'>Equipment Name</td><td rowspan='2'>Equipment (Number)</td><td colspan='7'>Irrigated Area (Acre)</td><td colspan='7'>Irrigation Cost (Tk/acre)</td><td rowspan='2'>Benefited Farmer</td></tr><tr><td>Boro</td><td>Wheat</td><td>Potato</td><td>Maize</td><td>Mustard</td><td>Veg (W)</td><td>Other<sup>4</sup></td><td>Boro</td><td>Wheat</td><td>Potato</td><td>Maize</td><td>Mustard</td><td>Veg (W)</td><td>Other<sup>4</sup></td></tr><tr><td>DTW</td><td>Union Count</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Sum</td></tr><tr><td>STW</td><td>Union Count</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Sum</td></tr><tr><td>LLP</td><td>Union Count</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Avg</td><td>Sum</td></tr><tr><td>All</td><td>Un All Equi Count</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Sum</td><td>Wt. Avg</td><td>Wt. Avg</td><td>Wt. Avg</td><td>Wt. Avg</td><td>Wt. Avg</td><td>Wt. Avg</td><td>Wt. Avg</td><td>Sum</td></tr>");


    //table.append("<tr><th rowspan='2'>Equipment Name</th><th rowspan='2'>Equipment (Number)</th><th colspan='7'>Irrigated Area (Acre)</th><th colspan='6'>Irrigation Cost (Tk/acre)</th><th rowspan='2'>Benefited Farmer</th><th rowspan='2'>Benefited Labour</th></tr><tr><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Mustard</th><th>Veg (W)</th><th>Other<sup>4</sup></th><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Veg (W)</th><th>Other<sup>4</sup></th></tr>");

    ////" + data. + "
    //table.append("<tr><td>" + equipmentTypes[data.equipmentId] + "</td><td>" + data.noOfEquipment + "</td><td>" + data.totalBoroArea + "</td><td>" + data.totalWheatArea + "</td><td>" + data.totalPotatoArea + "</td><td>" + data.totalMaizeArea + "</td><td>" + data.totalMustardArea + "</td><td>" + data.totalVegWinterArea + "</td><td>" + data.totalOthersArea + "</td><td>" + data.avgBoroCost + "</td><td>" + data.avgWheatCost + "</td><td>" + data.avgPotatoCost + "</td><td>" + data.avgMaizeCost + "</td><td>" + data.avgVegWinterCost + "</td><td>" + data.avgOthersCost + "</td><td>" + data.totalBenefitedFarmer + "</td><td>" + data.totalBenefitedAgricultureLabour + "</td></tr>");
    ///
}


function SetUnionData(unionCode, unionName, upazName, distName, divName) {

    $("#legend_info_title").empty();
    $("#map_legend_infos").empty();

    $("#legend_info_title").text("Union Wise Survey Info.");

    var table = $("<table>").addClass("table data-table");

    $("#map_legend_infos").append(table);

    var slno = 1;

    if (divName) {
        table.append("<tr><td style='width:195px; font-weight:600;'>0" + ++slno + ". Division</td><td style='width:5px; font-weight:600;'>:</td><td style='width:auto;'>" + divName + "</td></tr>");
        //++slno;
    }
    if (distName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". District</td><td style='font-weight:600;'>:</td><td>" + distName + "</td></tr>");
        ++slno;
    }
    if (upazName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Upazila</td><td style='font-weight:600;'>:</td><td>" + upazName + "</td></tr>");
        //++slno;
    }
    if (unionName) {
        table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Union</td><td style='font-weight:600;'>:</td><td>" + unionName + "</td></tr>");
        //++slno;
    }

    var union_info = {};
    //return;
    try {
        $.ajax({
            type: "Get",
            //url: "../MapViewer/GetMapData?equipmentId=" + equipmentId + "&adminLevel="+dataAdminCode+"&divCode="++"&distCode="++"&upazCode=" + condition,
            //url: "../MapViewer/GetUnionWiseMapData?equipmentId=" + equipmentId + "&adminLevel=" + dataAdminCode,
            //url: url,
            url: "../MapViewer/GetUnionWiseMapData?unionCode=" + unionCode,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#busy-indicator").fadeIn();
            },
            success: function (allData) {
                union_info = allData;
                //console.log(union_info);
            },
            error: function (e) {
                //console.log(e.responseText);
                msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.responseText);
                $("#busy-indicator").fadeOut();
            },
            complete: function () {
                if (union_info) {

                    if (union_info.waterbodiesInfo && union_info.waterbodiesInfo.length > 0) {
                        union_info.waterbodiesInfo.map(function (wbInfo) {
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Rivers Name</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.riversNames ? wbInfo.riversNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Rivers Length(Km)</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.totalRiversLength ? number_formatter(wbInfo.totalRiversLength / 1000, true, 2) + " (Km)" : "") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Waterbodies Name</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.waterbodiesNames ? wbInfo.waterbodiesNames : "Unknown") +
                                "</td></tr>");
                            table.append("<tr><td style='font-weight:600;'>0" + ++slno + ". Waterbodies Area(ha)</td><td style='font-weight:600;'>:</td><td>" +
                                (wbInfo.totalWaterbodiesArea ? number_formatter(wbInfo.totalWaterbodiesArea, true, 2) + " (ha)" : "") +
                                "</td></tr>");
                        });
                    }

                    var table1 = $("<table>").addClass("table data-table");

                    $("#map_legend_infos").append(table1);

                    if (union_info.surveyInfo && union_info.surveyInfo.length > 0) {

                        table1.append("<tr><th rowspan='2'><p style='width:150px;'>Equipment Name</p></th><th rowspan='2'>Equipment (Number)</th><th colspan='8'>Irrigated Area (Acre)</th><th colspan='6'>Irrigation Cost (Tk/acre)</th><th colspan='3'>Benefited Farmer</th><th colspan='3'>Benefited Labour</th><th colspan='5'>Canal Length (Km)</th><th rowspan='2'>Conveyance Efficiency</th><th colspan='2'>Power Consumption</th><th colspan='2'>Abstraction of Water (M m<sup>3</sup>)</th></tr>" +
                            "<tr><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Mustard</th><th>Veg (W)</th><th>Other<sup>4</sup></th><th>Total</th><th>Boro</th><th>Wheat</th><th>Potato</th><th>Maize</th><th>Veg (W)</th><th>Other<sup>4</sup></th> <th>Male</th><th>Female</th><th>Total</th> <th>Male</th><th>Female</th><th>Total</th> <th>Pacca</th><th>Buried Pipe</th><th>Fita Pipe</th><th>Kacha</th><th>Total</th><th>Diesel(M ton)</th><th>Electricity (MW)</th><th>GW</th><th>SW</th></tr>");

                        var totalData = {
                            noOfEquipment: 0,

                            totalBoroArea: 0,
                            totalWheatArea: 0,
                            totalPotatoArea: 0,
                            totalMaizeArea: 0,
                            totalMustardArea: 0,
                            totalVegWinterArea: 0,
                            totalOthersArea: 0,
                            totalArea: 0,

                            avgBoroCost: 0,
                            avgWheatCost: 0,
                            avgPotatoCost: 0,
                            avgMaizeCost: 0,
                            avgVegWinterCost: 0,
                            avgOthersCost: 0,

                            totalBenefitedFarmer: 0,
                            totalBenefitedFarmerFemale: 0,
                            totalBenefitedFarmerMale: 0,

                            totalBenefitedAgricultureLabour: 0,
                            totalBenefitedAgricultureLabourFemale: 0,
                            totalBenefitedAgricultureLabourMale: 0,

                            totalCanalLengthPacca: 0,
                            totalCanalLengthBuriedPipe: 0,
                            totalCanalLengthFitaPipe: 0,
                            totalCanalLengthKacha: 0,
                            totalCanalLength: 0,

                            totalDistributionEfficiencyWeight: 0,

                            totalPowerConsumptionDiesel: 0,
                            totalPowerConsumptionElectric: 0,

                            totalAbstractionGW: 0,
                            totalAbstractionSW: 0,
                        };

                        union_info.surveyInfo.map(function (data) {
                            //console.log(data);
                            table1.append("<tr><td>" +
                                equipmentTypes[data.equipmentId] +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.noOfEquipment, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBoroArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalWheatArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPotatoArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMaizeArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalMustardArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalVegWinterArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalOthersArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalArea, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgBoroCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgWheatCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgPotatoCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgMaizeCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgVegWinterCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.avgOthersCost, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmerMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmerFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedFarmer, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabourMale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabourFemale, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalBenefitedAgricultureLabour, true, 0) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthPacca / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthBuriedPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthFitaPipe / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLengthKacha / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalCanalLength / 1000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.distributionEfficiency, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionDiesel, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalPowerConsumptionElectric, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionGW / 1000000, true, 2) +
                                "</td><td style='text-align:right;'>" +
                                number_formatter(data.totalAbstractionSW / 1000000, true, 2) +
                                "</td></tr>");


                            totalData.noOfEquipment += data.noOfEquipment;

                            totalData.totalBoroArea += data.totalBoroArea;
                            totalData.totalWheatArea += data.totalWheatArea;
                            totalData.totalPotatoArea += data.totalPotatoArea;
                            totalData.totalMaizeArea += data.totalMaizeArea;
                            totalData.totalMustardArea += data.totalMustardArea;
                            totalData.totalVegWinterArea += data.totalVegWinterArea;
                            totalData.totalOthersArea += data.totalOthersArea;
                            totalData.totalArea += data.totalArea;

                            totalData.avgBoroCost += data.avgBoroCost * data.totalBoroArea;
                            totalData.avgWheatCost += data.avgWheatCost * data.totalWheatArea;
                            totalData.avgPotatoCost += data.avgPotatoCost * data.totalPotatoArea;
                            totalData.avgMaizeCost += data.avgMaizeCost * data.totalMaizeArea;
                            totalData.avgVegWinterCost += data.avgVegWinterCost * data.totalVegWinterArea;
                            totalData.avgOthersCost += data.avgOthersCost * data.totalOthersArea;

                            totalData.totalBenefitedFarmer += data.totalBenefitedFarmer;
                            totalData.totalBenefitedFarmerFemale += data.totalBenefitedFarmerFemale;
                            totalData.totalBenefitedFarmerMale += data.totalBenefitedFarmerMale;

                            totalData.totalBenefitedAgricultureLabour += data.totalBenefitedAgricultureLabour;
                            totalData.totalBenefitedAgricultureLabourFemale += data.totalBenefitedAgricultureLabourFemale;
                            totalData.totalBenefitedAgricultureLabourMale += data.totalBenefitedAgricultureLabourMale;

                            totalData.totalCanalLengthPacca += data.totalCanalLengthPacca;
                            totalData.totalCanalLengthBuriedPipe += data.totalCanalLengthBuriedPipe;
                            totalData.totalCanalLengthFitaPipe += data.totalCanalLengthFitaPipe;
                            totalData.totalCanalLengthKacha += data.totalCanalLengthKacha;
                            totalData.totalCanalLength += data.totalCanalLength;

                            totalData.totalDistributionEfficiencyWeight += data.distributionEfficiency * data.totalCanalLength;

                            totalData.totalPowerConsumptionDiesel += data.totalPowerConsumptionDiesel;
                            totalData.totalPowerConsumptionElectric += data.totalPowerConsumptionElectric;

                            totalData.totalAbstractionGW += data.totalAbstractionGW;
                            totalData.totalAbstractionSW += data.totalAbstractionSW;
                        });

                        table1.append("<tr><td>" +
                            "All Equipments" +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.noOfEquipment, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBoroArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalWheatArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalPotatoArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalMaizeArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalMustardArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalVegWinterArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalOthersArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalArea, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalBoroArea ? number_formatter(totalData.avgBoroCost / totalData.totalBoroArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalWheatArea ? number_formatter(totalData.avgWheatCost / totalData.totalWheatArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalPotatoArea ? number_formatter(totalData.avgPotatoCost / totalData.totalPotatoArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalMaizeArea ? number_formatter(totalData.avgMaizeCost / totalData.totalMaizeArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalVegWinterArea ? number_formatter(totalData.avgVegWinterCost / totalData.totalVegWinterArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalOthersArea ? number_formatter(totalData.avgOthersCost / totalData.totalOthersArea, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedFarmerMale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedFarmerFemale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedFarmer, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedAgricultureLabourMale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedAgricultureLabourFemale, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalBenefitedAgricultureLabour, true, 0) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthPacca / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthBuriedPipe / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthFitaPipe / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLengthKacha / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalCanalLength / 1000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            (totalData.totalCanalLength ? number_formatter(totalData.totalDistributionEfficiencyWeight / totalData.totalCanalLength, true, 2) : "0.00") +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalPowerConsumptionDiesel, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalPowerConsumptionElectric, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalAbstractionGW / 1000000, true, 2) +
                            "</td><td style='text-align:right;'>" +
                            number_formatter(totalData.totalAbstractionSW / 1000000, true, 2) +
                            "</td></tr>");
                    }
                } else {
                    $("#map_legend_infos").append("<p class='error'>Survey data not available !!</p>");
                }

                $("#busy-indicator").fadeOut();
            }

        });
    } catch (e) {
        msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!\n" + e.error);
        $("#busy-indicator").fadeOut();
    }

    return;
}
*/


function set_data_layer(isShow) {

    if (!dataLayer) {
        add_data_layer();
        return;
    }

    if (isShow) {
        if (!map.hasLayer(dataLayer))
            map.addLayer(dataLayer);

        $("#admin_sban").prop("checked", true);
        //add_admin_boundary("sban");

        $("input[type='checkbox'].multi-chkbx.admin").each(function () {
            if (this.checked) {
                add_admin_boundary($(this).prop("id").replace("admin_", ""));
            }
        });

        $("input[type='checkbox'].multi-chkbx.other_layer").each(function () {
            if (this.checked) {
                add_other_layer($(this).prop("id"), $(this).attr("data"));
            }
        });

        map_label_show_hide($("#map_label_opt").prop("checked"));

        $("#map_legend_basic_data").slideDown(350);
        //$("#map_legend_basic_data").css("display", "");

        $("#legend_info_opt").prop("checked", true).trigger("change");
    } else {
        if (map.hasLayer(dataLayer))
            map.removeLayer(dataLayer);

        $("#admin_sban").prop("checked", false);
        remove_admin_boundary("sban");

        map_label_show_hide(false);

        $("#map_legend_basic_data").slideUp(350);
        //$("#map_legend_basic_data").css("display", "none");

        $("#legend_info_opt").prop("checked", false).trigger("change");
    }
}

function set_equipment_locations(isShow) {

    if (isShow) {
        try {

            $.ajax({
                type: "Get",
                url: "../MapViewer/GetEquipmentLocations", //+ equipmentId + "&adminLevel=" + dataAdminCode,// +  "&distCode=" + distCode + "&upazCode=" + upazCode,
                //url: "../MapViewer/GetEquipmentLocations?equipmentId=" + equipmentId + "&adminLevel=" + dataAdminCode + "&divCode=" + divCode + "&distCode=" + distCode + "&upazCode=" + upazCode,
                //url: "../MapViewer/GetMapDataPre?equipmentId=" + equipmentId + "&adminLevel=" + dataAdminCode,
                //url: url,
                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $("#busy-indicator").fadeIn();
                },
                success: function (allLocations) {

                    if (allLocations && allLocations.length > 0) {

                        var icon = {
                            pane: "label-layer",
                            radius: 5,
                            color: "blue",
                            weight: 2.25,
                            opacity: 0.6,
                            fillColor: "red",
                            fillOpacity: 0.8
                        };

                        equipmentClusters = {
                            "1": new L.MarkerClusterGroup({
                                disableClusteringAtZoom: 13,
                                spiderfyOnMaxZoom: false,
                                //pane: "marker-layer",
                                maxClusterRadius: 85,
                                iconCreateFunction: function (cluster) {
                                    var markerCount = cluster.getChildCount();
                                    return L.divIcon({
                                        html: "<div>" + markerCount + "</div>",
                                        className: "marker-cluster-eqp cluster-llp",
                                        iconSize: L.point(35, 35)
                                    });
                                },
                                ////clusterPane: "pane1",
                                //spiderLegPolylineOptions: { weight: 0 },
                                //clockHelpingCircleOptions: {
                                //    weight: .7,
                                //    opacity: 1,
                                //    color: 'black',
                                //    fillOpacity: 0,
                                //    dashArray: '10 5'
                                //},

                                //elementsPlacementStrategy: "original-locations",
                                //helpingCircles: true,

                                //spiderfyDistanceSurplus: 25,
                                //spiderfyDistanceMultiplier: 1,

                                //elementsMultiplier: 1.4,
                                //firstCircleElements: 8
                            }),
                            /*equipmentClusters[2] =*/
                            "2": new L.MarkerClusterGroup({
                                disableClusteringAtZoom: 15,
                                spiderfyOnMaxZoom: false,
                                //pane: "marker-layer",
                                maxClusterRadius: 85,
                                iconCreateFunction: function (cluster) {
                                    var markerCount = cluster.getChildCount();
                                    return L.divIcon({
                                        pane: "label-layer",
                                        html: "<div>" + markerCount + "</div>",
                                        className: "marker-cluster-eqp cluster-dtw",
                                        iconSize: L.point(35, 35)
                                    });
                                }
                            }),
                            /*equipmentClusters[3] =*/
                            "3": new L.MarkerClusterGroup({
                                disableClusteringAtZoom: 13,
                                spiderfyOnMaxZoom: false,
                                //pane: "marker-layer",
                                maxClusterRadius: 85,
                                iconCreateFunction: function (cluster) {
                                    //var markers = cluster.getAllChildMarkers();
                                    var markerCount = cluster.getChildCount();
                                    return L.divIcon({
                                        pane: "label-layer",
                                        html: "<div>" + markerCount + "</div>",
                                        className: "marker-cluster-eqp cluster-stw",
                                        iconSize: L.point(35, 35)
                                    });
                                },
                                ////clusterPane: "pane1",
                                //spiderLegPolylineOptions: { weight: 0 },
                                //clockHelpingCircleOptions: {
                                //    weight: .7,
                                //    opacity: 1,
                                //    color: 'black',
                                //    fillOpacity: 0,
                                //    dashArray: '10 5'
                                //},

                                //elementsPlacementStrategy: "original-locations",
                                //helpingCircles: true,

                                //spiderfyDistanceSurplus: 25,
                                //spiderfyDistanceMultiplier: 1,

                                //elementsMultiplier: 1.4,
                                //firstCircleElements: 8
                            }),
                            /*equipmentClusters[3] =*/
                            "7": new L.MarkerClusterGroup({
                                disableClusteringAtZoom: 13,
                                spiderfyOnMaxZoom: false,
                                //pane: "marker-layer",
                                maxClusterRadius: 85,
                                iconCreateFunction: function (cluster) {
                                    var markerCount = cluster.getChildCount();
                                    return L.divIcon({
                                        pane: "label-layer",
                                        html: "<div>" + markerCount + "</div>",
                                        className: "marker-cluster-eqp cluster-stw",
                                        iconSize: L.point(35, 35)
                                    });
                                }
                            }),
                            /*equipmentClusters[3] =*/
                            "14": new L.MarkerClusterGroup({
                                disableClusteringAtZoom: 13,
                                spiderfyOnMaxZoom: false,
                                //pane: "marker-layer",
                                maxClusterRadius: 85,
                                iconCreateFunction: function (cluster) {
                                    var markerCount = cluster.getChildCount();
                                    return L.divIcon({
                                        pane: "label-layer",
                                        html: "<div>" + markerCount + "</div>",
                                        className: "marker-cluster-eqp cluster-stw",
                                        iconSize: L.point(35, 35)
                                    });
                                }
                            })
                        };


                        var getLatLng = function (lat, lng) {
                            return new L.LatLng(lat, lng);
                        },
                            getInfo = function (el) {
                                if (!el)
                                    return "";

                                var img_id = "img_" + el.photo_id,
                                    img_name = el.equipment_name,
                                    img_src = "../images/EquipmentPhotos/" + el.dist_code + "/" + el.upaz_code + "/" + el.photo_id + ".jpg";

                                return "<div style='display:inline-block; overflow:auto; width:630px; max-width:754px; max-height:654px;'>" +
                                    "    <table style='width: 100%;'>" +
                                    "        <tr>" +
                                    "            <td colspan='2'>" +
                                    "                <h3><b>Equipment : </b><span style='color: #05c;'>" +
                                    el.equipment_name +
                                    "                </span></h3>" +
                                    "            </td>" +
                                    "        </tr>" +
                                    "        <tr>" +
                                    "            <td style='width: 100%; min-width:200px; vertical-align: top;'>" +
                                    "                <div style='min-width:200px;'>" +
                                    "<h4><b>Source of Power: </b> " +
                                    el.power_source +
                                    "</h4><h4><b>Owner Name: </b> " +
                                    el.owner_name +
                                    //"</h4><h4><b>Mobile No.: </b> " +
                                    //el.mobile_no +
                                    //"</h4><h4><b>Scheme/Mauza: </b> " +
                                    //el.mauza +
                                    "</h4><h4><b>Mauza: </b> " +
                                    el.mauza +
                                    "</h4><h4><b>Union: </b> " +
                                    el.union +
                                    "</h4><h4><b>Upazila: </b> " +
                                    el.upazila +
                                    "</h4><h4><b>District: </b> " +
                                    el.district +
                                    //"</h4><h4><b>Address : </b> " + el.address + "</h4>"
                                    "</h4><h4><b>Latitude: </b> " +
                                    el.lat +
                                    "</h4><h4><b>Longitude: </b> " +
                                    el.lng +
                                    "</h4>" +
                                    "         </div>" +
                                    "            </td>" +
                                    "            <td style='width:auto; vertical-align:top;'>" +
                                    "                <div style='height:340px; padding:0;'>" +
                                    //"                    <a id='" + img_id + "' href='" + img_src + "' class='pump_img_link' alt='" + img_name + "' title='" + img_name + "'>" +
                                    "                       <img src='" + img_src + "' class='pump_img' alt='" + img_name + "'>" +
                                    //"                    </a>" +
                                    "                </div>" +
                                    "            </td>" +
                                    "        </tr>" +
                                    "    </table>" +
                                    "</div>" + "";
                                //"<script>" +
                                //"    //$('#" + img_id + "').myPhoto({ hook: 'class', animation_speed: 'fast', slideshow: 7500, hideflash: true, social_tools: false }); //$(function () { }); " +
                                //"</script>";
                            };


                        //equipmentType = eqInfo.EquipmentInfo.NameOfEquipment,
                        //    powerSource = eqInfo.PowerSourceInfo.SourceOfPower,
                        //    ownerName = eqInfo.NameOfSchemeOrOwner,
                        //    mobileNo = eqInfo.OwnerOrManagerMobileNo,
                        //    photoId = eqInfo.PhotoId,
                        //    lng = eqInfo.Longitude,
                        //    lat = eqInfo.Latitude,
                        //    districtCode = eqInfo.DistCode,
                        //    upazilaCode = eqInfo.UpazCode,
                        //    unionCode = eqInfo.UnionCode,
                        //    mauzaCode = eqInfo.MauzaCode,
                        //    district = eqInfo.DistrictInfo.DistName,
                        //    upazila = eqInfo.UpazilaInfo.UpazName,
                        //    union = eqInfo.UnionInfo.UnionName,
                        //    mauza = eqInfo.MauzaName
                        //var tc = 0;

                        allLocations.forEach(function (el, i) {
                            //console.log(el);
                            //console.log(el.equipment_name);
                            //console.log(el.lat);
                            //console.log(el.lng);
                            //el.equipment_type = 1;

                            if (el.lat == 'undefined' || el.lng == 'undefined')
                                return;

                            //var location = new L.CircleMarker(getLatLng(el.lat, el.lng), icon)
                            //    .bindPopup(getInfo(el, i))
                            //    .on({
                            //        "mouseover": function () {
                            //            this.setStyle({
                            //                weight: 2.5,
                            //                opacity: 0.8
                            //            });
                            //        },
                            //        "mouseout": function () {
                            //            //e.target.resetStyle();

                            //            this.setStyle({
                            //                weight: 2.0,
                            //                opacity: 0.6
                            //            });
                            //        }
                            //    });

                            //console.log(location);
                            //console.log(el.equipment_type);

                            icon.radius = 5;
                            icon.weight = 2.25;
                            icon.fillOpacity = 0.8;

                            switch (el.equipment_type) {
                                case 1:
                                    {
                                        icon.color = "green";
                                        icon.fillColor = "blue";

                                        //location.icon = icon;
                                        //equipmentClusters[1].addLayer(location);
                                    }
                                    break;

                                case 2:
                                    {
                                        icon.color = "red";
                                        icon.fillColor = "green";

                                        //location.icon = icon;
                                        //equipmentClusters[2].addLayer(location);
                                    }
                                    break;

                                case 3:
                                    {
                                        icon.color = "blue";
                                        icon.fillColor = "red";

                                        //location.icon = icon;
                                        //equipmentClusters[3].addLayer(location);
                                    }
                                    break;

                                case 7:
                                    {
                                        //radius: 6,
                                        //color: "blue",
                                        //weight: 2.5,
                                        //opacity: 0.6,
                                        //fillColor: "red",
                                        //fillOpacity: 0.8

                                        icon.radius = 6.0;
                                        icon.weight = 2.5;
                                        icon.fillOpacity = 0.85;

                                        icon.color = "blue";
                                        icon.fillColor = "green";

                                        //location.icon = icon;

                                        //console.log(location);
                                        //location.icon.radius = 7;
                                        //location.icon.weight = 3.0;
                                        //location.icon.fillOpacity = 0.85;

                                        //equipmentClusters[7].addLayer(location);
                                    }
                                    break;

                                case 14:
                                    {
                                        icon.radius = 7.0;
                                        icon.weight = 3.0;
                                        icon.fillOpacity = 0.9;

                                        icon.color = "black";
                                        icon.fillColor = "blue";

                                        //location.icon = icon;


                                        //console.log(location);
                                        //equipmentClusters[14].addLayer(location);
                                    }
                                    break;

                                default:
                                    break;
                            }



                            var location = new L.CircleMarker(getLatLng(el.lat, el.lng), icon)
                                .bindPopup(getInfo(el, i))
                                .on({
                                    "mouseover": function () {
                                        this.setStyle({
                                            weight: 2.5,
                                            opacity: 0.8
                                        });
                                    },
                                    "mouseout": function () {
                                        //e.target.resetStyle();
                                        this.setStyle({
                                            weight: 2.0,
                                            opacity: 0.6
                                        });
                                    }
                                });

                            //map.addLayer(location);

                            equipmentClusters[el.equipment_type].addLayer(location);


                            //console.log(icon.fillColor);
                            //console.log(location.icon);

                        });

                        //console.log(tc);
                        //map.addLayer(markerClusters);
                        //map.addLayer(locations);
                        //console.log(markerClusters);
                        //map.addLayer(equipmentClusters);

                        map.addLayer(equipmentClusters[1]);
                        map.addLayer(equipmentClusters[2]);
                        map.addLayer(equipmentClusters[3]);
                        map.addLayer(equipmentClusters[7]);
                        map.addLayer(equipmentClusters[14]);


                        //console.log($.isFunction($(".pump_img_link").myPhoto));

                        //if (typeof $(".pump_img_link").myPhoto == 'function') {
                        //    console.log('yes...');
                        //} else {
                        //    console.log('no...');
                        //}


                        //$(function () {
                        //    console.log($(".pump_img_link").myPhoto());
                        //    //$(".pump_img_link").myPhoto({ hook: 'class', animation_speed: 'fast', slideshow: 7500, hideflash: true, social_tools: false });
                        //    $(".pump_img_link").myPhoto({ hook: 'class', animation_speed: 'fast', slideshow: 7500, hideflash: true, social_tools: false });
                        //});

                        $("#map_legend_colors").append(
                            "<div id='map_legend_loc_1'><label class='map_legend_label'>" +
                            "<label class='map_legend_color map_legend_circle' style='border-color:#1e1; background-color:#23e;'></label>" +
                            equipmentTypes[1] +
                            "</label> <br /><div>");

                        $("#map_legend_colors").append(
                            "<div id='map_legend_loc_2'><label class='map_legend_label'>" +
                            "<label class='map_legend_color map_legend_circle' style='border-color:#e43; background-color:#3e2;'></label>" +
                            equipmentTypes[2] +
                            "</label> <br /><div>");

                        $("#map_legend_colors").append(
                            "<div id='map_legend_loc_3'><label class='map_legend_label'>" +
                            "<label class='map_legend_color map_legend_circle' style='border-color:#34e; background-color:#e32;'></label>" +
                            equipmentTypes[3] +
                            "</label> <br /><div>");

                        $("#map_legend_colors").append(
                            "<div id='map_legend_loc_7'><label class='map_legend_label'>" +
                            "<label class='map_legend_color map_legend_circle' style='border-color:#23e; background-color:#5e8;'></label>" +
                            equipmentTypes[7] +
                            "</label> <br /><div>");

                        $("#map_legend_colors").append(
                            "<div id='map_legend_loc_14'><label class='map_legend_label'>" +
                            "<label class='map_legend_color map_legend_circle' style='border-color:#111; background-color:#21e;'></label>" +
                            equipmentTypes[14] +
                            "</label> <br /><div>");



                        $("#eqp_types").slideDown(350);
                        $("#eqp_types").find("input[type='checkbox']").each(function () {
                            $(this).prop("checked", true);
                            $(this).prop("disabled", false);

                            $(this).on("change",
                                function () {

                                    if ($("#eqp_types").find("input[type='checkbox']").length === $("#eqp_types").find("input[type='checkbox']:checked").length) {
                                        $("#eqp_loc").prop("indeterminate", false);
                                        $("#eqp_loc").prop("checked", true);
                                    } else if ($("#eqp_types").find("input[type='checkbox']:checked").length > 0) {
                                        $("#eqp_loc").prop("indeterminate", true);
                                        $("#eqp_loc").prop("checked", false);
                                    } else {
                                        $("#eqp_loc").prop("indeterminate", false);
                                        $("#eqp_loc").prop("checked", false);
                                    }

                                    if (!$(this).prop("id").replace("eqp_", "") || !equipmentClusters)
                                        return;

                                    var eqpId = $(this).prop("id").replace("eqp_", "");

                                    if (!equipmentClusters[eqpId])
                                        return;

                                    if (this.checked) {
                                        if (!map.hasLayer(equipmentClusters[eqpId]))
                                            map.addLayer(equipmentClusters[eqpId]);

                                        if ($("#map_legend_loc_" + eqpId).length)
                                            $("#map_legend_loc_" + eqpId).show();
                                    } else {
                                        if (map.hasLayer(equipmentClusters[eqpId]))
                                            map.removeLayer(equipmentClusters[eqpId]);

                                        if ($("#map_legend_loc_" + eqpId).length)
                                            $("#map_legend_loc_" + eqpId).hide();
                                    }
                                });
                        });

                        //add_data_layer();
                    }
                },
                error: function (ex) {
                    console.log(ex);
                    msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!");
                    $("#busy-indicator").fadeOut();
                },
                //done: function () {
                //    //$("#busy-indicator").fadeOut();
                //},
                complete: function () {
                    $("#busy-indicator").fadeOut();
                }

            });

        } catch (e) {
            msg.init("Error", "Error... . .", "Error trying to load/parse JSON data !! <br />" + e.message);
        }

    } else {
        for (var ei = 1; ei <= 3; ei++) {
            if (map.hasLayer(equipmentClusters[ei]))
                map.removeLayer(equipmentClusters[ei]);

            if ($("#map_legend_loc_" + ei).length)
                $("#map_legend_loc_" + ei).remove();
        }

        equipmentClusters = {};

        $("#eqp_types").find("input[type='checkbox']").each(function () {
            $(this).prop("checked", false);
            $(this).prop("disabled", true);
        });

        $("#eqp_types").slideUp(350);

    }
}

function set_survey_data() {

    //dataAdminCode = $("#admin_info").val();
    //dataAdminName = $("#admin_info option:selected").text();

    //dataCode = $("#survey_info").val();
    //dataName = $("#survey_info option:selected").text();
    //equipmentId = $("#survey_info option:selected").attr("data-id");

    //GetUnionWiseMapData

    map.closePopup();

    map_data = [];

    try {
        var dataModel = $("#survey_info option:selected").attr("data-model"),
            dataAction = $("#survey_info option:selected").attr("data-action"),
            dataId = $("#survey_info option:selected").attr("data-id"),
            dataYear = $("#data_year").val();

        dataModel = dataModel ? dataModel : "MapViewer";
        dataAction = dataAction ? dataAction : "GetSurveyData";

        var url = "../" +
            dataModel +
            "/" +
            dataAction +
            "?dataTypeId=" +
            dataId +
            "&adminLevel=" +
            dataAdminCode +
            "&dataYear=" +
            dataYear;

        switch (dataYear) {
            case "2018":
            case "2016":
            case "2005":
                $.ajax({
                    type: "Get",
                    //url: "../MapViewer/GetMapData?equipmentId=" + equipmentId + "&adminLevel="+dataAdminCode+"&divCode="++"&distCode="++"&upazCode=" + condition,
                    //url: "../MapViewer/GetMapDataPre?equipmentId=" + equipmentId + "&adminLevel=" + dataAdminCode,
                    url: url,
                    cache: false,
                    contentType: false,
                    processData: false,
                    beforeSend: function () {
                        $("#busy-indicator").fadeIn();
                    },
                    success: function (allData) {
                        //console.log(allData);
                        //console.log(allData.value);

                        if (allData && allData.length > 0) {
                            allData.map(function (d) {
                                map_data[d[dataAdminCode + "Code"]] = d["data"];
                                return;
                            });
                            //add_data_layer();
                        }
                    },
                    error: function () {
                        msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!");
                        $("#busy-indicator").fadeOut();
                    },
                    //done: function () {
                    //    //$("#busy-indicator").fadeOut();
                    //},
                    complete: function () {
                        add_data_layer();

                        $("#busy-indicator").fadeOut();
                    }

                });

                break;

            default:
                //console.log(dataYear);

                //$.getJSON("../js/maps/map_data/" + "all_data" + ".json",
                //        function(allData) {
                //            if (allData && allData.all_data.length > 0) {
                //                //console.log(allData.all_data);
                //                allData.all_data.some(function(ad) {
                //                    if (ad.adminCode == dataAdminCode) {
                //                        ad.data.map(function(d) {
                //                            map_data[d[dataAdminCode + "Code"]] = d["data"];
                //                            return;
                //                        });
                //                        return true;
                //                    }
                //                    return false;
                //                });
                //                //add_data_layer();
                //            }
                //        })
                //    .done(function() {
                //        add_data_layer();
                //        $("#busy-indicator").fadeOut();
                //    });

                //return;

                $.ajax({
                    dataType: "json",
                    url: "../js/maps/map_data/all_data.json",
                    beforeSend: function () {
                        $("#busy-indicator").fadeIn();
                    },
                    success: function (allData) {
                        //console.log(allData[dataAdminCode]);

                        if (allData && allData[dataAdminCode].length > 0) {

                            allData[dataAdminCode].map(function (d) {
                                map_data[d[dataAdminCode + "Code"]] = d[dataCode];
                                return;
                            });

                        }
                    },
                    error: function () {
                        msg.init("Error", "Error... . .", "Unable to load/parse JSON data !!");
                        $("#busy-indicator").fadeOut();
                    },
                    complete: function () {
                        add_data_layer();

                        $("#busy-indicator").fadeOut();
                    }
                });

                break;
        }


        //return;
        //$.getJSON("../js/maps/map_data/" + "all_data" + ".json", function (allData) {
        //    if (allData && allData.all_data.length > 0) {
        //        //console.log(allData.all_data);

        //        allData.all_data.some(function (ad, i) {
        //            if (ad.adminCode == dataAdminCode) {
        //                map_data = ad.data.map(function (d, i) {
        //                    return { geo_code: d[dataAdminCode + "Code"], data_value: d[dataCode] };
        //                });
        //                return true;
        //            }
        //            return false;
        //        });

        //        //add_data_layer();
        //    }
        //}).done(function (e) {
        //    add_data_layer();
        //});

    } catch (e) {
        map_data = [];
        msg.init("Error", "Error... . .", "Error trying to load/parse JSON data !! <br />" + e.message);
    }

}


//$("input[type='checkbox'].multi-chkbx.layer").each(function () {
//    if (this.checked && $(this).prop("id")) {
//        add_other_data_layer($(this).prop("id"), $(this).attr("data"));
//    }
//});

var isOpened = false, isDataOpened = false;

function map_filter_open_close(content, isOpen) {
    if (isOpened === isOpen) return;

    if (isOpen && !isOpened)
        modal_open(content, 50);
    else if (!isOpen && isOpened)
        modal_close(content);

    isOpened = isOpen;
}

function map_data_open_close(content, isOpen) {
    if (isDataOpened === isOpen) return;

    if (isOpen && !isDataOpened)
        modal_open(content, 25);
    else if (!isOpen && isDataOpened)
        modal_close(content);

    isDataOpened = isOpen;
}

function legend_open_close(legendId, openCloseOpt, propOpt) {
    if (openCloseOpt == "open") {
        $("#" + legendId + "_btn")
            .css(propOpt, "-" + ($("#" + legendId + "_btn").outerWidth(true) + 10) + "px");
        $("#" + legendId).css(propOpt, "-2px");

        $("#" + legendId + "_opt").prop("checked", true);
    } else if (openCloseOpt == "close") {
        $("#" + legendId).css(propOpt, "-" + ($("#" + legendId).outerWidth(true) + 5) + "px");
        $("#" + legendId + "_btn").css(propOpt, "-10px");

        $("#" + legendId + "_opt").prop("checked", false);
    }
}


function random_color(format) {
    var ran_int = Math.floor(0x100000000 * Math.random());
    switch (format) {
        case "hex":
            return "#" + ("00000" + ran_int.toString(16)).slice(-6).toUpperCase();
        case "hexa":
            return "#" + ("0000000" + ran_int.toString(16)).slice(-8).toUpperCase();
        case "rgb":
            return "rgb(" + (ran_int & 255) + "," + (ran_int >> 8 & 255) + "," + (ran_int >> 16 & 255) + ")";
        case "rgba":
            return "rgba(" + (ran_int & 255) + "," + (ran_int >> 8 & 255) + "," + (ran_int >> 16 & 255) + "," + (ran_int >> 24 & 255) / 255 + ")";
        default:
            return ran_int;
    }
}

function get_unique_theme(mapData) {

    if (!mapData || mapData.length < 1) return [];

    var theme = [], tc = 0;

    var unique_data = mapData.filter(function (itm, i, mapData) {
        return i == mapData.indexOf(itm);
    });

    unique_data.map(function (md, c) {
        if (typeof (md) == "undefined")
            return;

        theme.push({
            val: md,
            title: md,
            color: "#" + ("00000" + (Math.floor(0xFFFFFF * Math.random()) << 0).toString(16)).slice(-6).toUpperCase()
        });
    });

    theme.push({ val: "", title: "-", color: "#FFFFFF" });

    return theme;
}

function get_dynamic_theme(mapData) {
    var minVal = Infinity, maxVal = -Infinity, currVal, theme = [];

    if (!mapData || mapData.length < 1)
        mapData = map_data;

    if (!mapData || mapData.length < 1) return theme;

    var isFloat = false;

    for (var dc = 0; dc < mapData.length; dc++) {
        if (isNaN(parseFloat(mapData[dc])) || !$.isNumeric(mapData[dc]))
            continue;

        currVal = parseFloat(mapData[dc]);

        isFloat = isFloat || currVal % 1 != 0;

        if (minVal > currVal)
            minVal = currVal;
        if (maxVal < currVal)
            maxVal = currVal;
    }
    //var isFloat = minVal % 1 != 0 || maxVal % 1 != 0;

    if ((maxVal - minVal) == 0) {
        theme.push({ minVal: isFloat ? parseFloat(minVal).toFixed(2) : minVal, maxVal: "-", color: "#4A0000" });
        theme.push({ minVal: "noData", maxVal: "-", color: "#FFFFFF" });

        return theme;
    } else if ((maxVal - minVal) == 1) {
        theme.push({ minVal: "-", maxVal: isFloat ? parseFloat(minVal).toFixed(2) : minVal, color: "#FFFF80" });
        theme.push({ minVal: isFloat ? parseFloat(minVal - 0.01).toFixed(2) : minVal + 1, maxVal: "-", color: "#4A0000" });
        theme.push({ minVal: "noData", maxVal: "-", color: "#FFFFFF" });

        return theme;
    } else if ((maxVal - minVal) < 12) {
        theme.push({ minVal: "-", maxVal: isFloat ? parseFloat(minVal).toFixed(2) : minVal, color: "#FFFF80" });
        theme.push({ minVal: isFloat ? parseFloat(minVal + 0.01).toFixed(2) : minVal + 1, maxVal: isFloat ? parseFloat(maxVal - 0.01).toFixed(2) : maxVal - 1, color: "#F2A82F" });
        theme.push({ minVal: isFloat ? parseFloat(maxVal).toFixed(2) : maxVal, maxVal: "-", color: "#4A0000" });
        theme.push({ minVal: "noData", maxVal: "-", color: "#FFFFFF" });

        return theme;
    }

    //var clrClass = ["#FFEEDD", "#FEBBAA", "#FE9988", "#FA7766", "#FA5544", "#FA3322", "#FA1100"],
    var clrClass = ["#FFFF80", "#FAD155", "#F2A82F", "#AD5314", "#6A0000", "#4A0000"],
        delta = parseInt((maxVal - minVal) / clrClass.length),
        ci = 0;

    for (ci = 0; ci < clrClass.length; ci++) {
        if (ci === 0) {
            theme.push({ minVal: isFloat ? parseFloat(minVal).toFixed(2) : minVal, maxVal: isFloat ? parseFloat(minVal + delta).toFixed(2) : minVal + delta, color: clrClass[ci] });
        } else if (ci === clrClass.length - 1 || (delta < 2 && ci >= clrClass.length - 2)) {
            theme.push({ minVal: isFloat ? parseFloat(minVal + (delta * ci) + 0.01).toFixed(2) : minVal + (delta * ci) + 1, maxVal: isFloat ? parseFloat(maxVal).toFixed(2) : maxVal, color: clrClass[ci] });
        } else {
            theme.push({ minVal: isFloat ? parseFloat(minVal + (delta * ci) + 0.01).toFixed(2) : minVal + (delta * ci) + 1, maxVal: isFloat ? parseFloat(minVal + (delta * (ci + 1))).toFixed(2) : minVal + (delta * (ci + 1)), color: clrClass[ci] });
        }
    }

    theme.push({ minVal: "noData", maxVal: "-", color: "#FFFFFF" });
    //console.log(theme);
    return theme;
}

function get_theme(minVal, maxVal) {
    var theme = [];

    if (isNaN(minVal) || !$.isNumeric(minVal) || isNaN(maxVal) || !$.isNumeric(maxVal))
        return theme;

    if (maxVal === minVal) {
        theme.push({ minVal: minVal, maxVal: "-", color: "#4A0000" });
        theme.push({ minVal: "noData", maxVal: "-", color: "#FFFFFF" });

        return theme;
    }

    //var clrClass = ["#FFEEDD", "#FEBBAA", "#FE9988", "#FA7766", "#FA5544", "#FA3322", "#FA1100"],
    var clrClass = ["#AFFF80", "#AAD155", "#A2A82F", "#8D5314", "#5A0000", "#3A0000"],
        delta = parseInt((maxVal - minVal) / clrClass.length),
        ci;

    for (ci = 0; ci < clrClass.length; ci++) {
        if (ci === 0) {
            theme.push({ minVal: minVal, maxVal: minVal + delta, color: clrClass[ci] });
        } else if (ci === clrClass.length - 1) {
            theme.push({ minVal: minVal + (delta * ci) + 1, maxVal: maxVal, color: clrClass[ci] });
        } else {
            theme.push({ minVal: minVal + (delta * ci) + 1, maxVal: minVal + (delta * (ci + 1)), color: clrClass[ci] });
        }
    }

    theme.push({ minVal: "noData", maxVal: "-", color: "#FFFFFF" });

    return theme;
}


function map_label_show_hide(isShow) {
    if (!mapLabels)
        return;

    $("#map_label_opt").prop("checked", isShow);

    if (isShow) {
        if (!map.hasLayer(mapLabels))
            map.addLayer(mapLabels);
        //$(".map_label").fadeIn(500);
    } else {
        if (map.hasLayer(mapLabels))
            map.removeLayer(mapLabels);
        //$(".map_label").fadeOut(500);
    }

    return;
}


function add_data_layer() {

    $("#legend_title").empty();
    $("#legend_info_title").empty();
    $("#map_legend_infos").empty();
    $("#map_legend_colors").empty();

    dataAdminCode = $("#admin_info").val();

    if (!dataAdminCode || !map)
        return;

    if (dataLayer) {
        if (map.hasLayer(dataLayer))
            map.removeLayer(dataLayer);
        dataLayer = null;
    }

    if (mapLabels) {
        if (map.hasLayer(mapLabels))
            map.removeLayer(mapLabels);
        mapLabels = new L.LayerGroup();

        $("#map_label_opt").prop("checked", false);
    }

    $(".map_label").remove();

    //dataAdminName = $("#admin_info option:selected").text();
    //dataName = $("#survey_info option:selected").text();
    //equipmentId = $("#survey_info option:selected").attr("data-id");

    //defaultTheme = $("#legend_theme").val() == "dynamic"
    //    ? get_dynamic_theme(map_data)
    //    : legend_themes[dataAdminCode + "_" + $("#survey_info").val()];

    defaultTheme = $("#legend_theme").val() == "dynamic"
        ? get_dynamic_theme(map_data)
        : legend_themes[dataAdminCode + "_" + $("#survey_info").val()];
    //$.extend(true, [], legend_themes[dataAdminCode + "_" + $("#survey_info").val()]);
    //console.log(map_data);
    //console.log(defaultTheme);
    if (!defaultTheme) {
        defaultTheme = get_dynamic_theme(map_data);
        $("#legend_theme").val("dynamic");
    }

    if (defaultTheme && defaultTheme.length > 0) {
        noDataColor = defaultTheme[defaultTheme.length - 1].color;
        //defaultTheme.splice(-1, 1);
    }


    var dataLayerPath = "../js/maps/map_data/" + dataAdminCode + ".json",
        totalValue = 0,
        table = $("<table>").addClass("table data-table");

    //map_data.map(d => totalValue += parseFloat(d.data_value));
    map_data.map(d => totalValue += parseFloat(d));
    totalValue = (totalValue % 1 == 0) ? totalValue : totalValue.toFixed(2);

    $("#legend_title").append(dataName + " legend ");
    $("#legend_info_title").append(dataAdminName + " Wise " + dataName + " (" + totalValue + ")");

    $("#map_legend_infos").append(table);
    table.append("<tr><th>" + dataAdminName + "</th> <th>" + dataName + "</th></tr>");


    //L.geoJson(dataLayerPath, {
    //    style: function (feature) {
    //        return { color: feature.properties.color };
    //    }
    //}).bindPopup(function (layer) {
    //    return layer.feature.properties.description;
    //}).addTo(map);

    //var sorted = map_layer.sort(function (a, b) {
    //    if (a.a > b.a) {
    //        return 1;
    //    }
    //    if (a.a < b.a) {
    //        return -1;
    //    }

    //    return 0;
    //});

    $.getJSON(dataLayerPath, function (map_layer) {
        if (!map_layer) return;

        mapLabels = new L.LayerGroup();

        dataLayer = L.geoJson(map_layer,
            {
                style: function (feature) { return mapDataStyle(feature, defaultTheme); },
                onEachFeature: mapDataFeatures,
                filter: function (feature, layer) {
                    return (feature &&
                        feature.properties[dataAdminCode + "_code"] &&
                        !isNaN(map_data[feature.properties[dataAdminCode + "_code"]]));

                    //if (!feature || !feature.properties[dataCode]) {
                    //    return;
                    //}
                    //if (!feature || !feature.properties[dataAdminCode + "_code"] || !feature.properties[dataAdminCode + "_code"] || isNaN(map_data[feature.properties[dataAdminCode + "_code"]])) {
                    //    return;
                    //}
                },
                renderer: dataAdminCode == "union" ? new L.Canvas({ padding: 0.5 }) : null,
                //renderer: dataAdminCode == "union" ? (L.canvas() ? new L.Canvas({ padding: 0.5 }) : new L.SVG({ padding: 0.5 })) : null
            });

    }).done(function (e) {

        $("#basic_survey").prop("checked", true);
        map.addLayer(dataLayer);

        table.append(
            "<tr><td style='padding-right:8px; text-align:right; font-weight:600;'>Total: </td><td style='padding-right:8px; text-align:right; font-weight:600;'>" +
            totalValue +
            "</td></tr>");

        var $legendDiv = $("<div id='map_legend_basic_data'></div>");

        if (defaultTheme && defaultTheme.length > 0) {

            defaultTheme.map(function (d, i) {
                if (defaultTheme.length - 1 == i)
                    return;

                var legend;
                if (!$.isNumeric(d.minVal)) {
                    legend = " =< " + d.maxVal;
                } else if (!$.isNumeric(d.maxVal)) {
                    legend = " >= " + d.minVal;
                } else {
                    legend = d.minVal + " - " + d.maxVal;
                }

                $legendDiv.append("<label class='map_legend_label'>" +
                    "<label class='map_legend_color' style='background-color:" +
                    d.color +
                    ";'></label>" +
                    legend +
                    "</label><br/>");
            });
        }

        $legendDiv.append("<label class='map_legend_label'>" +
            "<label class='map_legend_color' style='border:1px solid #eee; background-color:" +
            noDataColor +
            ";'> </label>No data</label><br/>");

        $("#map_legend_colors").append($legendDiv);


        $("#admin_sban").prop("checked", true);
        //add_admin_boundary("sban");

        $("input[type='checkbox'].multi-chkbx.admin").each(function () {
            if (this.checked) {
                add_admin_boundary($(this).prop("id").replace("admin_", ""));
            }
        });

        $("input[type='checkbox'].multi-chkbx.other_layer").each(function () {
            if (this.checked) {
                add_other_layer($(this).prop("id"), $(this).attr("data"));
            }
        });

    });

}

function mapDataStyle(feature, legendTheme) {

    var adminFieldCode = dataAdminCode + "_code",
        polyOptions = $.extend(true, {}, polyDefaultOptions);

    //polyOptions.pane = "data-layer";
    //polyOptions.renderer = new L.Renderer

    if (!feature.properties[adminFieldCode] || !legendTheme || isNaN(map_data[feature.properties[adminFieldCode]])) {
        //return null;
        return polyOptions;
    }

    var dataValue = !isNaN(map_data[feature.properties[adminFieldCode]])
        ? map_data[feature.properties[adminFieldCode]]
        : "no data";

    if (!isNaN(dataValue) && $.isNumeric(dataValue)) {
        var color = noDataColor;
        legendTheme.some(th => ((!parseFloat(th.minVal) || dataValue >= parseFloat(th.minVal)) &&
            (!parseFloat(th.maxVal) || dataValue <= parseFloat(th.maxVal)))
            ? (color = th.color, true)
            : (color = noDataColor, false)
        );

        polyOptions.color = color ? color : noDataColor;
        polyOptions.fillColor = color ? color : noDataColor;
    } else {
        polyOptions.color = noDataColor;
        polyOptions.fillColor = noDataColor;
        polyOptions.fillOpacity = 0.8;
    }

    return polyOptions;
}

function mapDataFeatures(feature, layer) {

    var adminFieldCode = dataAdminCode + "_code",
        adminFieldName = dataAdminCode + "_name";

    if (!feature || !feature.properties[adminFieldCode] || !feature.properties[adminFieldName]) {
        //if (!feature || !feature.properties[adminFieldCode] || !feature.properties[adminFieldName] || isNaN(map_data[feature.properties[adminFieldCode]])) {
        //layer.remove();
        return;
    }


    var dataValue,
        polyCenter,
        offsetTop = 10,
        offsetLeft,
        labelClass,
        labelContent,
        labelInfo,
        popupContent,
        dataLink = "",
        baseUrl = baseUrl ? baseUrl : "../SurveyInfoes",
        queryStr = "?equipmentId=" + equipmentId,
        table = $("#map_legend_infos table.table");


    var currAdminName = feature.properties[adminFieldName],
        currAdminCode = feature.properties[adminFieldCode];

    dataValue = !isNaN(map_data[currAdminCode])
        ? "" + map_data[currAdminCode]
        : "no data";

    polyCenter = new L.LatLng(feature.properties["CNT_LAT"], feature.properties["CNT_LONG"]);

    offsetLeft = (currAdminName.length - 2 > dataValue.length)
        ? (285 * currAdminName.length) / 100
        : (325 * dataValue.length) / 100;


    labelClass = "map_label";
    if (feature.properties["div_code"]) {
        labelClass += " div_" + feature.properties["div_code"];
        queryStr += "&divCode=" + feature.properties["div_code"];
    }
    if (feature.properties["dist_code"]) {
        labelClass += " dist_" + feature.properties["dist_code"];
        queryStr += "&distCode=" + feature.properties["dist_code"];
    }
    if (feature.properties["upaz_code"]) {
        labelClass += " upaz_" + feature.properties["upaz_code"];
        queryStr += "&upazCode=" + feature.properties["upaz_code"];
    }
    if (feature.properties["union_code"]) {
        labelClass += " union_" + feature.properties["union_code"];
        queryStr += "&unionCode=" + feature.properties["union_code"];
    }


    labelContent = currAdminName + "<p style='text-align:center; font-weight:bold; color:#025;'>(" + dataValue + ")</p>";

    popupContent = "<p style='text-align:center; font-weight:bold; font-size:14px; color:#137; white-space:nowrap;'>" + currAdminName + " " + dataAdminName +
        "</p> <p style='text-align:center; font-weight:bold; font-size:13px; color:#024; white-space:nowrap;'>(" + dataValue + ")</p>";
    dataLink = "<p style='text-align:center; font-size:11px; white-space:nowrap;'><a target='_blank' href='" + baseUrl + queryStr + "'>data source</a></p>";


    labelInfo = new L.Marker(polyCenter, {
        pane: "label-layer",
        icon: L.divIcon({
            iconSize: null,
            className: labelClass,
            iconAnchor: [offsetLeft, offsetTop],
            html: labelContent
        })
    }).bindPopup(popupContent + dataLink)
        .on({
            "mouseover": function (e) {
                layer.setStyle({
                    weight: 2.5,
                    opacity: 1.0
                });
            },
            "mouseout": function (e) {
                layer.setStyle({
                    weight: 1.0,
                    opacity: 0.9
                });
            }
        });

    mapLabels.addLayer(labelInfo);

    layer.bindTooltip(popupContent, { sticky: true })
        .on({
            "click": function (e) {
                //if (dataAdminCode == "union") {
                //    var unionCode = feature.properties["union_code"],
                //        unionName = feature.properties["union_name"],
                //        upazName = feature.properties["upaz_name"],
                //        distName = feature.properties["dist_name"],
                //        divName = feature.properties["div_name"];

                //    SetUnionData(unionCode, unionName, upazName, distName, divName);
                //    SetUnionData(unionCode, unionName, upazName, distName, divName);

                //    return;
                //}

                SetAdminData(dataAdminCode, feature.properties);

                L.popup().setLatLng(polyCenter).setContent(popupContent + dataLink).openOn(map);
            },
            "mouseover": function (e) {
                this.setStyle({
                    weight: 2.5,
                    opacity: 1.0
                });
            },
            "mouseout": function (e) {
                this.setStyle({
                    weight: 1.0,
                    opacity: 0.9
                });
            }
        });

    table.append("<tr><td style='padding-left:8px; text-align:left;'>" +
        currAdminName +
        "</td> <td style='padding-right:8px; text-align:right;'>" +
        dataValue +
        "</td></tr>");

    return;
}



function remove_other_layer(layerCode) {
    if (!layerCode) return;

    if (mapLayers["other_" + layerCode] && map.hasLayer(mapLayers["other_" + layerCode].layer)) {
        map.removeLayer(mapLayers["other_" + layerCode].layer);

        $("#admin_label_" + layerCode).prop("disabled", true);

        if (map.hasLayer(mapLayers["other_" + layerCode].label))
            map.removeLayer(mapLayers["other_" + layerCode].label);
    }

    if (mapLayers["other_" + layerCode] && map.hasLayer(mapLayers["other_" + layerCode].layer)) {
        map.removeLayer(mapLayers["other_" + layerCode].layer);

        $("#admin_label_" + layerCode).prop("disabled", true);

        if (map.hasLayer(mapLayers["other_" + layerCode].label))
            map.removeLayer(mapLayers["other_" + layerCode].label);
    }

    if ($("div#map_legend_" + layerCode)) {
        $("div#map_legend_" + layerCode).slideUp(350);
    }

    return;
}

function add_other_layer(layerCode, dataCode) {

    if (mapLayers["other_" + layerCode]) {

        //if (map.hasLayer(mapLayers["other_" + layerCode].layer))
        //    map.removeLayer(mapLayers["other_" + layerCode].layer);

        if (!map.hasLayer(mapLayers["other_" + layerCode].layer))
            map.addLayer(mapLayers["other_" + layerCode].layer);

        //admin-label
        $("#admin_label_" + layerCode).prop("disabled", false);

        if ($("#admin_label_" + layerCode).prop("checked"))
            map.addLayer(mapLayers["other_" + layerCode].label);

        if ($("div#map_legend_" + layerCode)) {
            $("div#map_legend_" + layerCode).slideDown(350);
        }

        return;
    }

    var layerName = $("label[for='" + layerCode + "']") ? $("label[for='" + layerCode + "']").text() : "",
        otherLayerPath = "../js/maps/map_data/" + layerCode + ".json";

    $.getJSON(otherLayerPath,
        function (map_layer) {
            if (!map_layer) return;

            if (legend_themes[layerCode]) {
                defaultTheme = legend_themes[layerCode];
                //defaultTheme = $.extend(true, [], legend_themes[layerCode]);
            } else if (dataCode) {
                other_data = map_layer.features.map(function (f, c) {
                    if (f && f.properties[dataCode])
                        return f.properties[dataCode];
                });
                defaultTheme = get_unique_theme(other_data);
            } else {
                defaultTheme = null;
            }

            //defaultTheme = legend_themes[layerCode]
            //    ? legend_themes[layerCode]
            //    : get_unique_theme(other_data);

            if (defaultTheme && defaultTheme.length > 0) {
                noDataColor = defaultTheme[defaultTheme.length - 1].color;
                //defaultTheme.splice(-1, 1);
            }

            mapLabels = new L.LayerGroup();

            //if (layerCode == "wtr_body" || layerCode == "det_river" || layerCode == "mjr_river" || layerCode == "main_river") {
            if (layerCode == "wtr_body" || layerCode.indexOf("_river") > -1) {
                var sty = {
                    zIndex: 101,
                    weight: 1.0,
                    opacity: 0.9,
                    color: defaultTheme[0] && defaultTheme[0].color
                        ? defaultTheme[0].color
                        : (layerCode == "wtr_body" ? "#0EB6F8" : "#73DFFF"),
                    fillOpacity: 0.85
                };

                otherLayer = L.geoJson(map_layer,
                    {
                        style: function (feature) {
                            return sty;
                        },
                        onEachFeature: function (feature, layer) {
                            return mapOtherDataFeatures(feature, layer, layerName, dataCode);
                        },
                        //filter: function (feature, layer) {
                        //    if (!feature || !feature.properties[dataCode]) {
                        //        return;
                        //    }
                        //},
                        //renderer: new L.Canvas({ padding: 0.5, pane: "other-layer" })
                    });
                //console.log(otherLayer);
            } else {
                otherLayer = L.geoJson(map_layer,
                    {
                        style: function (feature) {
                            return otherDataStyle(feature, dataCode, defaultTheme);
                        },
                        onEachFeature: function (feature, layer) {
                            return mapOtherDataFeatures(feature, layer, layerName, dataCode);
                        },
                        //filter: function (feature, layer) {
                        //    if (!feature || !feature.properties[dataCode]) {
                        //        return;
                        //    }
                        //},
                        renderer: new L.Canvas({ padding: 0.5, pane: "other-layer" })
                    });
            }

        }).done(function (e) {

            mapLayers["other_" + layerCode] = { layer: otherLayer, label: otherLabels };

            map.addLayer(mapLayers["other_" + layerCode].layer);

            //map.fitBounds(mapLayers["other_" + layerCode].layer.getBounds());

            $("#admin_label_" + layerCode).prop("disabled", false);

            if ($("#admin_label_" + layerCode).prop("checked"))
                map.addLayer(mapLayers["other_" + layerCode].label);


            if ($("div#map_legend_" + layerCode)) {
                $("div#map_legend_" + layerCode).remove();
            }

            if (defaultTheme && defaultTheme.length > 0) {

                var $legendDiv = $("<div id='map_legend_" + layerCode + "' style='margin:8px auto auto 3px;'></div>");

                if (layerName) {
                    var layerDataName = $("#" + layerCode + "_data_code").length
                        ? " (" + $("#" + layerCode + "_data_code option:selected").text() + ")"
                        : "";

                    $legendDiv.append(
                        "<span class='map_legend_label legend_title'> ▣ " +
                        layerName +
                        layerDataName +
                        "</span><br/>");
                }

                defaultTheme.map(function (th, i) {
                    $legendDiv.append("<label class='map_legend_label'>" +
                        "<label class='map_legend_color' style='background-color:" +
                        th.color +
                        ";'></label>" +
                        th.title +
                        "</label><br/>");
                });

                $("#map_legend_colors").append($legendDiv);
            }

        });

}


//(feature, legendField, defaultTheme)
function otherDataStyle(feature, legendField, legendTheme) {

    var polyOptions = $.extend(true, {}, polyDefaultOptions);

    polyOptions.pane = "other-layer";

    //if (!feature || isNaN(feature.properties[legendField]) || !legendTheme) {
    if (!feature || typeof feature.properties[legendField] == "undefined" || !legendTheme) {
        return polyOptions;
    }

    var color = noDataColor, dataValue = feature.properties[legendField];

    legendTheme.some(th => (th.val && dataValue == th.val)
        ? (color = th.color, true)
        : (color = noDataColor, false)
    );

    polyOptions.color =
        polyOptions.fillColor = color ? color : noDataColor;

    polyOptions.opacity =
        polyOptions.fillOpacity = 0.95;

    return polyOptions;
}

function mapOtherDataFeatures(feature, layer, layerName, dataCode) {
    //console.log(feature.properties[dataCode]);
    if (!feature || !feature.properties[dataCode]) {
        return;
    }

    var popupContent = "<p style='text-align:center; font-weight:bold; font-size:14px; color:#137; white-space:nowrap;'>" + layerName +
        "</p> <p style='text-align:center; font-weight:bold; color:#024; white-space:nowrap;'>(" + feature.properties[dataCode] + ")</p>",
        tooltipContent = "<p style='text-align:center; font-size:13px; color:#135; white-space:nowrap;'>" + layerName +
            ": <span style='font-weight:bold; font-size:13px; color:#247;'>" + feature.properties[dataCode] + "</span></p>";

    layer.bindPopup(popupContent)
        .bindTooltip(tooltipContent, { sticky: true });

    return;
}

function resetOtherDataLayerStyle(layerCode, legendField) {
    if (mapLayers["other_" + layerCode] && mapLayers["other_" + layerCode].layer) {

        //console.log(mapLayers["other_" + layerCode].layer.features);
        //console.log(mapLayers["other_" + layerCode].layer.layers);

        var other_data = [],
            layerName = $("label[for='" + layerCode + "']") ? $("label[for='" + layerCode + "']").text() : "";

        if (layerName && $("div#map_legend_" + layerCode + " span.map_legend_label.legend_title").length) {
            var layerDataName = " (" + $("#" + layerCode + "_data_code option:selected").text() + ")";

            $("div#map_legend_" + layerCode + " span.map_legend_label.legend_title").text(" ▣ " + layerName + layerDataName);
        }

        mapLayers["other_" + layerCode].layer.eachLayer(function (layer) {
            if (!layer.feature || !layer.feature.properties[legendField]) {
                return;
            }

            other_data.push(layer.feature.properties[legendField]);
            // other_data[] = layer.feature.properties[legendField];

            var popupContent = "<p style='text-align:center; font-weight:bold; font-size:14px; color:#137; white-space:nowrap;'>" + layerName +
                "</p> <p style='text-align:center; font-weight:bold; color:#024; white-space:nowrap;'>(" + layer.feature.properties[legendField] + ")</p>",
                tooltipContent = "<p style='text-align:center; font-size:13px; color:#135; white-space:nowrap;'>" + layerName +
                    ": <span style='font-weight:bold; font-size:13px; color:#247;'>" + layer.feature.properties[legendField] + "</span></p>";

            layer.bindPopup(popupContent)
                .bindTooltip(tooltipContent, { sticky: true });

            return;
        });

        //console.log(other_data);

        if (legend_themes[layerCode]) {
            defaultTheme = legend_themes[layerCode];
        } else if (legendField) {
            defaultTheme = get_unique_theme(other_data);
        } else {
            defaultTheme = null;
        }

        mapLayers["other_" + layerCode].layer.setStyle(function (feature) {
            return otherDataStyle(feature, legendField, defaultTheme);
        });

        if (!map.hasLayer(mapLayers["other_" + layerCode].layer))
            map.addLayer(mapLayers["other_" + layerCode].layer);

    } else {
        add_other_layer(layerCode, legendField);
    }

    return;
}



function add_remove_admin_label(adminCode, isShow) {

    if (!adminCode || !mapLayers["admin_" + adminCode] || !mapLayers["admin_" + adminCode].label)
        return;

    if (isShow && !map.hasLayer(mapLayers["admin_" + adminCode].label)) {
        map.addLayer(mapLayers["admin_" + adminCode].label);
        return;
    }

    if (!isShow && map.hasLayer(mapLayers["admin_" + adminCode].label)) {
        map.removeLayer(mapLayers["admin_" + adminCode].label);
        return;
    }

    return;
}

function remove_admin_boundary(adminCode) {
    if (!adminCode) return;

    if (mapLayers["admin_" + adminCode] && map.hasLayer(mapLayers["admin_" + adminCode].layer)) {
        map.removeLayer(mapLayers["admin_" + adminCode].layer);

        $("#admin_label_" + adminCode).prop("disabled", true);

        if (map.hasLayer(mapLayers["admin_" + adminCode].label))
            map.removeLayer(mapLayers["admin_" + adminCode].label);
    }

    if ($("div#map_legend_" + adminCode)) {
        $("div#map_legend_" + adminCode).remove();
    }

    return;
}

function add_admin_boundary(adminCode) {
    if (!adminCode) return;

    var labelClass = "map_label",
        labelContent = "",
        offsetLeft = 0,
        lineStyle = {
            pane: "admin-layer",
            color: "#5A3322",
            weight: 1.0,
            opacity: 1,
            scale: 0.5,
            fill: false,
            fillColor: null,
            fillOpacity: 0.0
        },
        hoverStyle = {
            dashArray: null,
            zIndex: 9999,
            weight: 2.5,
            fill: false,
            color: "#FC4F3A",
            opacity: 1.0,
            fillColor: null,
            fillOpacity: 0.15
        },
        focusStyle = {
            dashArray: null,
            zIndex: 9999,
            weight: 3.0,
            fill: true,
            color: "#3587EA",
            opacity: 1.0,
            fillColor: "#35A3E8",
            fillOpacity: 0.15
        };

    switch (adminCode) {
        case "div":
            lineStyle.zIndex = 107;
            lineStyle.dashArray = "7 3 2 4 2 3";
            lineStyle.color = "#3A2211";
            lineStyle.weight = 1.5;
            lineStyle.scale = 1.5;

            hoverStyle.weight = 2.5;

            offsetLeft = 10;
            labelClass = "map_admin_label";

            labelContent = "<span style='font-size:15px;font-weight:500;color:#123;'>▣ ";

            $("#map_legend_colors").append("<div id='map_legend_div'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#3A2211'> <path stroke-width='1.5' stroke='#3A2211' stroke-linecap='round' stroke-dasharray='5 3 2 3 2 3' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:0; border: 1px dashed #3A2211;'></label> </svg>" +
                "▣ Division Boundary</label> <br /><div>");

            //$("#map_legend_colors").append("<div id='map_legend_div'><label class='map_legend_label'>" +
            //    "<label class='map_legend_color' style='height:0; border: 1px dashed #3A2211;'></label>" +
            //    "▣ Division Boundary</label> <br /><div>");
            break;

        case "dist":
            lineStyle.zIndex = 105;
            lineStyle.dashArray = "5 2 1 3 1 2";
            lineStyle.color = "#5A3322";
            lineStyle.weight = 1.0;
            lineStyle.scale = 1.0;

            hoverStyle.weight = 1.5;

            offsetLeft = 10;
            labelClass = "map_admin_label";

            labelContent = "<span style='font-size:13px;font-weight:400;color:#137;'>☐ ";

            $("#map_legend_colors").append("<div id='map_legend_dist'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#5A3322'> <path stroke-width='1.0' stroke='#5A3322' stroke-linecap='round' stroke-dasharray='5 2 1 3 1 2' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:0; border-top: 1px dashed #5A3322;'></label> </svg>" +
                "☐ District Boundary</label> <br /><div>");

            //$("#map_legend_colors").append("<div id='map_legend_dist'><label class='map_legend_label'>" +
            //    "<label class='map_legend_color' style='height:0; border-top: 1px dashed #5A3322;'></label>" +
            //    "☐ District Boundary</label> <br /><div>");
            break;

        case "upaz":
            lineStyle.zIndex = 103;
            lineStyle.dashArray = "3 2";
            lineStyle.color = "#4A0000";
            lineStyle.weight = 0.5;
            lineStyle.scale = 0.7;

            hoverStyle.weight = 1.0;

            labelContent = "⚀ ";

            $("#map_legend_colors").append("<div id='map_legend_upaz'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#4A0000'> <path stroke-width='0.7' stroke='#4A0000' stroke-linecap='round' stroke-dasharray='2 3' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:1px; background-color:#4A0000;'></label> </svg>" +
                "⚀ Upazila Boundary </label> <br /><div>");
            break;

        case "union":
            lineStyle.zIndex = 102;
            lineStyle.dashArray = "2 4";
            lineStyle.color = "#AA5533";
            lineStyle.weight = 0.5;
            lineStyle.scale = 0.5;

            hoverStyle.weight = 1.0;
            labelContent = "☉ ";

            $("#map_legend_colors").append("<div id='map_legend_union'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#AA5533'> <path stroke-width='0.5' stroke='#AA5533' stroke-linecap='round' stroke-dasharray='2 3' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:1px; background-color:#AA5533;'></label> </svg>" +
                "☉ Union Boundary </label> <br /><div>");
            break;

        case "sban":
            lineStyle.zIndex = 102;
            lineStyle.dashArray = "";
            lineStyle.color = "#228800";
            lineStyle.fill = "url(../images/mangrove.png)";
            lineStyle.fillColor = "#22BB00";
            lineStyle.fillOpacity = 1.0;
            lineStyle.weight = 0.5;
            lineStyle.scale = 0.5;

            $("#map_legend_colors").append("<div id='map_legend_sban'><label class='map_legend_label'>" +
                "<label class='map_legend_color' style='border:1px solid #3a0; background:#22AA00 url(../images/mangrove.png);'></label>" +
                "Sundarban Area </label> <br /><div>");
            break;

        default:
            hoverStyle.weight = 1.0;

            labelContent = "";
            break;
    }

    if (mapLayers["admin_" + adminCode]) {

        if (map.hasLayer(mapLayers["admin_" + adminCode].layer))
            map.removeLayer(mapLayers["admin_" + adminCode].layer);

        map.addLayer(mapLayers["admin_" + adminCode].layer);


        if ($("#admin_" + adminCode).length && !$("#admin_" + adminCode).prop("checked"))
            $("#admin_" + adminCode).prop("checked", true);

        //admin-label
        $("#admin_label_" + adminCode).prop("disabled", false);

        if ($("#admin_label_" + adminCode).length && $("#admin_label_" + adminCode).prop("checked"))
            map.addLayer(mapLayers["admin_" + adminCode].label);

        return;
    }

    var adminLayer = null,
        adminLabels = new L.LayerGroup(),
        adminLayerPath = "../js/maps/map_data/" + adminCode + ".json",
        selectedAdminCode = "30";

    adminLayer = L.geoJson(null, {
        style: lineStyle,
        onEachFeature: function (feature, layer) {
            getMapLabels(feature, layer, adminCode, labelClass, adminLabels, labelContent, offsetLeft, lineStyle, hoverStyle, focusStyle)
            //getMapLabels(feature, layer, adminCode, adminLabels, lineStyle);
        }
        //onEachFeature: getMapLabels,
        //filter: function (feature) {
        //    return setMapFilter(feature, adminCode, selectedAdminCode);
        //}
    });

    $.getJSON(adminLayerPath, function (adminLayerInfo) {
        if (!adminLayerInfo) return;

        adminLayer.addData(adminLayerInfo);
    }).done(function (e) {
        mapLayers["admin_" + adminCode] = { layer: adminLayer, label: adminLabels };

        map.addLayer(mapLayers["admin_" + adminCode].layer);


        if ($("#admin_" + adminCode).length && !$("#admin_" + adminCode).prop("checked"))
            $("#admin_" + adminCode).prop("checked", true);

        $("#admin_label_" + adminCode).prop("disabled", false);

        if (adminCode == "div" || adminCode == "dist") {
            $("#admin_label_" + adminCode).prop("checked", true);
        } else {
            var labelGroups = new L.MarkerClusterGroup({
                disableClusteringAtZoom: adminCode == "upaz" ? 10 : 11,
                pane: "label-layer"
            });
            labelGroups.addLayer(adminLabels);
            mapLayers["admin_" + adminCode]["label"] = labelGroups;
        }

        if ($("#admin_label_" + adminCode).prop("checked"))
            map.addLayer(mapLayers["admin_" + adminCode].label);

        //set_map_filter();
    });

    return;
}

function getMapLabels(feature, layer, adminCode, labelClass, adminLabels, labelContent, offsetLeftDefault, defaultStyle, hoverStyle, focusStyle) {

    var adminFieldName = adminCode + "_name";

    if (!feature || !feature.properties[adminFieldName]) {

        if (adminCode == "sban") {
            layer.bindTooltip("<span style='padding:3px 6px; font-size:15px;font-weight:600;color:#258;'>Sundarban Area</span>", { sticky: true });
        }
        return;
    }

    var currAdminName = feature.properties[adminFieldName],
        polyCenter = new L.LatLng(feature.properties["CNT_LAT"], feature.properties["CNT_LONG"]),
        offsetTop = 10,
        offsetLeft = ((285 * currAdminName.length) / 100) + offsetLeftDefault;

    defaultStyle = defaultStyle
        ? defaultStyle
        : { opacity: 1, scale: 0.5, color: "#5A3322", weight: 1.0, fill: false, fillColor: null, fillOpacity: 0.0 };
    labelContent = labelContent ? labelContent + currAdminName + "</span>" : currAdminName;

    //,
    //labelClass = "map_label",
    //labelContent,
    //hoverStyle = { dashArray: null, zIndex: 9999, weight: 2.5, fill: false, color: "#FC4F3A", opacity: 1.0, fillColor: null, fillOpacity: 0.15 },
    //focusStyle = { dashArray: null, zIndex: 9999, weight: 3.0, fill: true, color: "#3587EA", opacity: 1.0, fillColor: "#35A3E8", fillOpacity: 0.15 };

    //switch (adminCode) {
    //    case "div":
    //        defaultStyle.weight = 1.5;
    //        hoverStyle.weight = 2.5;

    //        offsetLeft += 10;
    //        labelClass = "map_admin_label";

    //        labelContent = "<span style='font-size:15px;font-weight:500;color:#123;'>▣ " + currAdminName + "</span>";
    //        break;

    //    case "dist":
    //        defaultStyle.weight = 1.0;
    //        hoverStyle.weight = 1.5;

    //        offsetLeft += 10;
    //        labelClass = "map_admin_label";

    //        labelContent = "<span style='font-size:13px;font-weight:400;color:#137;'>☐ " + currAdminName + "</span>";
    //        break;

    //    case "upaz":
    //        defaultStyle.weight = 0.5;
    //        hoverStyle.weight = 1.0;

    //        labelContent = "⚀ " + currAdminName;
    //        break;

    //    case "union":
    //        defaultStyle.weight = 0.25;
    //        hoverStyle.weight = 1.0;
    //        labelContent = "☉ " + currAdminName;
    //        break;

    //    default:
    //        defaultStyle.weight = 0.5;
    //        hoverStyle.weight = 1.0;

    //        labelContent = currAdminName;
    //        break;
    //}

    var labelInfo = new L.Marker(polyCenter, {
        pane: "label-layer",
        icon: L.divIcon({
            iconSize: null,
            className: labelClass,
            iconAnchor: [offsetLeft, offsetTop],
            html: labelContent
        })
    }).on({
        "mouseover": function (e) {
            var isFocus = layer.className && layer.className == "focused";
            layer.setStyle(isFocus ? focusStyle : hoverStyle);
        },
        "mouseout": function (e) {
            var isFocus = layer.className && layer.className == "focused";
            layer.setStyle(isFocus ? focusStyle : defaultStyle);
        }
    });

    layer.on({
        "click": function (e) {

            SetAdminData(adminCode, feature.properties);

            //if (adminCode == "union") {
            //    var unionCode = feature.properties["union_code"],
            //        unionName = feature.properties["union_name"],
            //        upazName = feature.properties["upaz_name"],
            //        distName = feature.properties["dist_name"],
            //        divName = feature.properties["div_name"];

            //    SetUnionData(unionCode, unionName, upazName, distName, divName);
            //}

            if (layer.className && layer.className == "focused") {
                layer.className = "";
                layer.setStyle(hoverStyle);
            }

            if (adminFocused && adminFocused.layer && adminFocused.layer == layer) {
                adminFocused.layer.className = "";
                adminFocused.layer.setStyle(defaultStyle);

                adminFocused = null;
            }
        }
    });
    //.on({
    //    "mouseover": function (e) {
    //        var isFocus = layer.className && layer.className == "focused";
    //        layer.setStyle(isFocus ? focusStyle : hoverStyle);
    //    },
    //    "mouseout": function (e) {
    //        var isFocus = layer.className && layer.className == "focused";
    //        layer.setStyle(isFocus ? focusStyle : defaultStyle);
    //    }
    //});


    if (adminCode == "div" || adminCode == "dist") {

        labelInfo.on("click", function (e) {

            if (adminFocused && adminFocused.layer && adminFocused.layer != layer) {
                adminFocused.layer.className = "";
                adminFocused.layer.setStyle(defaultStyle);
            }
            adminFocused = null;

            if (layer.className && layer.className == "focused") {
                layer.className = "";
                layer.setStyle(hoverStyle);

                //layer.setZIndex(400);
                //layer.setZIndexOffset(0);
                return;
            }

            layer.className = "focused";
            layer.setStyle(focusStyle);
            //layer.setZIndex(450);
            //layer.setZIndexOffset(250);

            adminFocused = { layer: layer, label: labelInfo };

            var geoCode = feature.properties[adminCode + "_code"],
                zoomLevel = (geoCode && (geoCode == 20 || geoCode == 30 || geoCode == 40)) ? 7.5 : 8.5;

            //zoomLevel = map.getZoom() > zoomLevel ? map.getZoom() : zoomLevel;
            //zoomLevel = adminCode == "dist" ? zoomLevel + 1.5 : zoomLevel;
            zoomLevel = map.getZoom() > zoomLevel
                ? map.getZoom()
                : adminCode == "dist"
                    ? zoomLevel + 1.5 : zoomLevel;

            map.setView(polyCenter, zoomLevel);
        });
    }



    //if (adminCode == "union") {

    labelInfo.on("click", function (e) {

        SetAdminData(adminCode, feature.properties);

        //var unionCode = feature.properties["union_code"],
        //    unionName = feature.properties["union_name"],
        //    upazName = feature.properties["upaz_name"],
        //    distName = feature.properties["dist_name"],
        //    divName = feature.properties["div_name"];

        //SetUnionData(unionCode, unionName, upazName, distName, divName);


        if (adminFocused && adminFocused.layer && adminFocused.layer != layer) {
            adminFocused.layer.className = "";
            adminFocused.layer.setStyle(defaultStyle);
        }
        adminFocused = null;

        if (layer.className && layer.className == "focused") {
            layer.className = "";
            layer.setStyle(hoverStyle);

            //layer.setZIndex(400);
            //layer.setZIndexOffset(0);
            return;
        }

        layer.className = "focused";
        layer.setStyle(focusStyle);
        //layer.setZIndex(450);
        //layer.setZIndexOffset(250);

        adminFocused = { layer: layer, label: labelInfo };

        //{"union_code":"10040903","div_name":"Barishal","dist_name":"Barguna","upaz_name":"Amtali","union_name":"Ward No-03","CNT_LAT":22.1323710898,"CNT_LONG":90.2388422647}},

        //var geoCode = feature.properties[adminCode + "_code"],
        //    zoomLevel = (geoCode && (geoCode == 20 || geoCode == 30 || geoCode == 40)) ? 7.5 : 8.5;

        ////zoomLevel = map.getZoom() > zoomLevel ? map.getZoom() : zoomLevel;
        ////zoomLevel = adminCode == "dist" ? zoomLevel + 1.5 : zoomLevel;
        //zoomLevel = map.getZoom() > zoomLevel
        //    ? map.getZoom()
        //    : adminCode == "dist"
        //    ? zoomLevel + 1.5 : zoomLevel;

        //map.setView(polyCenter, zoomLevel);
    });
    //}


    adminLabels.addLayer(labelInfo);

    return;
}


/*
 //// ok admin ////
function add_admin_boundary(adminCode) {
    if (!adminCode) return;

    var lineStyle = {
            pane: "admin-layer",
            color: "#5A3322",
            weight: 1.0,
            opacity: 1,
            scale: 0.5,
            fill: false,
            fillColor: null,
            fillOpacity: 0.0
        };

    switch (adminCode) {
        case "div":
            lineStyle.zIndex = 107;
            lineStyle.dashArray = "7 3 2 4 2 3";
            lineStyle.color = "#3A2211";
            lineStyle.weight = 1.5;
            lineStyle.scale = 1.5;

            $("#map_legend_colors").append("<div id='map_legend_div'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#3A2211'> <path stroke-width='1.5' stroke='#3A2211' stroke-linecap='round' stroke-dasharray='5 3 2 3 2 3' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:0; border: 1px dashed #3A2211;'></label> </svg>" +
                "▣ Division Boundary</label> <br /><div>");

            //$("#map_legend_colors").append("<div id='map_legend_div'><label class='map_legend_label'>" +
            //    "<label class='map_legend_color' style='height:0; border: 1px dashed #3A2211;'></label>" +
            //    "▣ Division Boundary</label> <br /><div>");
            break;

        case "dist":
            lineStyle.zIndex = 105;
            lineStyle.dashArray = "5 2 1 3 1 2";
            lineStyle.color = "#5A3322";
            lineStyle.weight = 1.0;
            lineStyle.scale = 1.0;

            $("#map_legend_colors").append("<div id='map_legend_dist'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#5A3322'> <path stroke-width='1.0' stroke='#5A3322' stroke-linecap='round' stroke-dasharray='5 2 1 3 1 2' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:0; border-top: 1px dashed #5A3322;'></label> </svg>" +
                "☐ District Boundary</label> <br /><div>");

            //$("#map_legend_colors").append("<div id='map_legend_dist'><label class='map_legend_label'>" +
            //    "<label class='map_legend_color' style='height:0; border-top: 1px dashed #5A3322;'></label>" +
            //    "☐ District Boundary</label> <br /><div>");
            break;

        case "upaz":
            lineStyle.zIndex = 103;
            lineStyle.dashArray = "3 2";
            lineStyle.color = "#4A0000";
            lineStyle.weight = 0.5;
            lineStyle.scale = 0.7;

            $("#map_legend_colors").append("<div id='map_legend_upaz'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#4A0000'> <path stroke-width='0.7' stroke='#4A0000' stroke-linecap='round' stroke-dasharray='2 3' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:1px; background-color:#4A0000;'></label> </svg>" +
                "⚀ Upazila Boundary </label> <br /><div>");
            break;

        case "union":
            lineStyle.zIndex = 102;
            lineStyle.dashArray = "2 4";
            lineStyle.color = "#AA5533";
            lineStyle.weight = 0.5;
            lineStyle.scale = 0.5;

            $("#map_legend_colors").append("<div id='map_legend_union'><label class='map_legend_label'>" +
                "<svg height='15' width='36'> <g fill='none' stroke='#AA5533'> <path stroke-width='0.5' stroke='#AA5533' stroke-linecap='round' stroke-dasharray='2 3' d='M7 11 l025 0' /> </g>" +
                "<label class='map_legend_color' style='height:1px; background-color:#AA5533;'></label> </svg>" +
                "☉ Union Boundary </label> <br /><div>");
            break;

        case "sban":
            lineStyle.zIndex = 102;
            lineStyle.dashArray = "";
            lineStyle.color = "#228800";
            lineStyle.fill = "url(../images/mangrove.png)";
            lineStyle.fillColor = "#22BB00";
            lineStyle.fillOpacity = 1.0;
            lineStyle.weight = 0.5;
            lineStyle.scale = 0.5;

            $("#map_legend_colors").append("<div id='map_legend_sban'><label class='map_legend_label'>" +
                "<label class='map_legend_color' style='border:1px solid #3a0; background:#22AA00 url(../images/mangrove.png);'></label>" +
                "Sundarban Area </label> <br /><div>");
            break;

        default:
            break;
    }

    if (mapLayers["admin_" + adminCode]) {

        if (map.hasLayer(mapLayers["admin_" + adminCode].layer))
            map.removeLayer(mapLayers["admin_" + adminCode].layer);

        map.addLayer(mapLayers["admin_" + adminCode].layer);


        if ($("#admin_" + adminCode).length && !$("#admin_" + adminCode).prop("checked"))
            $("#admin_" + adminCode).prop("checked", true);

        //admin-label
        $("#admin_label_" + adminCode).prop("disabled", false);

        if ($("#admin_label_" + adminCode).length && $("#admin_label_" + adminCode).prop("checked"))
            map.addLayer(mapLayers["admin_" + adminCode].label);

        return;
    }

    var adminLayer = null,
        adminLabels = new L.LayerGroup(),
        adminLayerPath = "../js/maps/map_data/" + adminCode + ".json",
        selectedAdminCode = "30";

    adminLayer = L.geoJson(null, {
        style: lineStyle,
        onEachFeature: function (feature, layer) {
            getMapLabels(feature, layer, adminCode, adminLabels, lineStyle);
        }
        //onEachFeature: getMapLabels,
        //filter: function (feature) {
        //    return setMapFilter(feature, adminCode, selectedAdminCode);
        //}
    });


    $.getJSON(adminLayerPath, function (adminLayerInfo) {
        if (!adminLayerInfo) return;

        adminLayer.addData(adminLayerInfo);
    }).done(function (e) {
        mapLayers["admin_" + adminCode] = { layer: adminLayer, label: adminLabels };

        map.addLayer(mapLayers["admin_" + adminCode].layer);


        if ($("#admin_" + adminCode).length && !$("#admin_" + adminCode).prop("checked"))
            $("#admin_" + adminCode).prop("checked", true);

        $("#admin_label_" + adminCode).prop("disabled", false);

        if (adminCode == "div" || adminCode == "dist") {
            $("#admin_label_" + adminCode).prop("checked", true);
        } else {
            var labelGroups = new L.MarkerClusterGroup({
                disableClusteringAtZoom: adminCode == "upaz" ? 10 : 11,
                pane: "label-layer"
            });
            labelGroups.addLayer(adminLabels);
            mapLayers["admin_" + adminCode]["label"] = labelGroups;
        }

        if ($("#admin_label_" + adminCode).prop("checked"))
            map.addLayer(mapLayers["admin_" + adminCode].label);

        //set_map_filter();
    });

    return;
}

function getMapLabels(feature, layer, adminCode, adminLabels, defaultStyle) {

    var adminFieldName = adminCode + "_name";

    if (!feature || !feature.properties[adminFieldName]) {

        if (adminCode == "sban") {
            layer.bindTooltip("<span style='padding:3px 6px; font-size:15px;font-weight:600;color:#258;'>Sundarban Area</span>", { sticky: true });
        }
        return;
    }

    defaultStyle = defaultStyle
        ? defaultStyle
        : { opacity: 1, scale: 0.5, color: "#5A3322", weight: 1.0, fill: false, fillColor: null, fillOpacity: 0.0 };

    var currAdminName = feature.properties[adminFieldName],
        polyCenter = new L.LatLng(feature.properties["CNT_LAT"], feature.properties["CNT_LONG"]),
        offsetTop = 10,
        offsetLeft = (285 * currAdminName.length) / 100,
        labelClass = "map_label",
        labelContent,
        hoverStyle = { dashArray: null, zIndex: 9999, weight: 2.5, fill: false, color: "#FC4F3A", opacity: 1.0, fillColor: null, fillOpacity: 0.15 },
        focusStyle = { dashArray: null, zIndex: 9999, weight: 3.0, fill: true, color: "#3587EA", opacity: 1.0, fillColor: "#35A3E8", fillOpacity: 0.15 };

    switch (adminCode) {
        case "div":
            defaultStyle.weight = 1.5;
            hoverStyle.weight = 2.5;

            offsetLeft += 10;
            labelClass = "map_admin_label";

            labelContent = "<span style='font-size:15px;font-weight:500;color:#123;'>▣ " + currAdminName + "</span>";
            break;

        case "dist":
            defaultStyle.weight = 1.0;
            hoverStyle.weight = 1.5;

            offsetLeft += 10;
            labelClass = "map_admin_label";

            labelContent = "<span style='font-size:13px;font-weight:400;color:#137;'>☐ " + currAdminName + "</span>";
            break;

        case "upaz":
            defaultStyle.weight = 0.5;
            hoverStyle.weight = 1.0;

            labelContent = "⚀ " + currAdminName;
            break;

        case "union":
            defaultStyle.weight = 0.25;
            hoverStyle.weight = 1.0;
            labelContent = "☉ " + currAdminName;
            break;

        default:
            defaultStyle.weight = 0.5;
            hoverStyle.weight = 1.0;

            labelContent = currAdminName;
            break;
    }

    var labelInfo = new L.Marker(polyCenter, {
        pane: "label-layer",
        icon: L.divIcon({
            iconSize: null,
            className: labelClass,
            iconAnchor: [offsetLeft, offsetTop],
            html: labelContent
        })
    }).on({
        "mouseover": function (e) {
            var isFocus = layer.className && layer.className == "focused";
            layer.setStyle(isFocus ? focusStyle : hoverStyle);
        },
        "mouseout": function (e) {
            var isFocus = layer.className && layer.className == "focused";
            layer.setStyle(isFocus ? focusStyle : defaultStyle);
        }
    });

    layer.on({
        "click": function (e) {

            SetAdminData(adminCode, feature.properties);

            //if (adminCode == "union") {
            //    var unionCode = feature.properties["union_code"],
            //        unionName = feature.properties["union_name"],
            //        upazName = feature.properties["upaz_name"],
            //        distName = feature.properties["dist_name"],
            //        divName = feature.properties["div_name"];

            //    SetUnionData(unionCode, unionName, upazName, distName, divName);
            //}

            if (layer.className && layer.className == "focused") {
                layer.className = "";
                layer.setStyle(hoverStyle);
            }

            if (adminFocused && adminFocused.layer && adminFocused.layer == layer) {
                adminFocused.layer.className = "";
                adminFocused.layer.setStyle(defaultStyle);

                adminFocused = null;
            }
        }
    });
    //.on({
    //    "mouseover": function (e) {
    //        var isFocus = layer.className && layer.className == "focused";
    //        layer.setStyle(isFocus ? focusStyle : hoverStyle);
    //    },
    //    "mouseout": function (e) {
    //        var isFocus = layer.className && layer.className == "focused";
    //        layer.setStyle(isFocus ? focusStyle : defaultStyle);
    //    }
    //});


    if (adminCode == "div" || adminCode == "dist") {

        labelInfo.on("click", function (e) {

            if (adminFocused && adminFocused.layer && adminFocused.layer != layer) {
                adminFocused.layer.className = "";
                adminFocused.layer.setStyle(defaultStyle);
            }
            adminFocused = null;

            if (layer.className && layer.className == "focused") {
                layer.className = "";
                layer.setStyle(hoverStyle);

                //layer.setZIndex(400);
                //layer.setZIndexOffset(0);
                return;
            }

            layer.className = "focused";
            layer.setStyle(focusStyle);
            //layer.setZIndex(450);
            //layer.setZIndexOffset(250);

            adminFocused = { layer: layer, label: labelInfo };

            var geoCode = feature.properties[adminCode + "_code"],
                zoomLevel = (geoCode && (geoCode == 20 || geoCode == 30 || geoCode == 40)) ? 7.5 : 8.5;

            //zoomLevel = map.getZoom() > zoomLevel ? map.getZoom() : zoomLevel;
            //zoomLevel = adminCode == "dist" ? zoomLevel + 1.5 : zoomLevel;
            zoomLevel = map.getZoom() > zoomLevel
                ? map.getZoom()
                : adminCode == "dist"
                    ? zoomLevel + 1.5 : zoomLevel;

            map.setView(polyCenter, zoomLevel);
        });
    }



    //if (adminCode == "union") {

    labelInfo.on("click", function (e) {

        SetAdminData(adminCode, feature.properties);

        //var unionCode = feature.properties["union_code"],
        //    unionName = feature.properties["union_name"],
        //    upazName = feature.properties["upaz_name"],
        //    distName = feature.properties["dist_name"],
        //    divName = feature.properties["div_name"];

        //SetUnionData(unionCode, unionName, upazName, distName, divName);


        if (adminFocused && adminFocused.layer && adminFocused.layer != layer) {
            adminFocused.layer.className = "";
            adminFocused.layer.setStyle(defaultStyle);
        }
        adminFocused = null;

        if (layer.className && layer.className == "focused") {
            layer.className = "";
            layer.setStyle(hoverStyle);

            //layer.setZIndex(400);
            //layer.setZIndexOffset(0);
            return;
        }

        layer.className = "focused";
        layer.setStyle(focusStyle);
        //layer.setZIndex(450);
        //layer.setZIndexOffset(250);

        adminFocused = { layer: layer, label: labelInfo };

        //{"union_code":"10040903","div_name":"Barishal","dist_name":"Barguna","upaz_name":"Amtali","union_name":"Ward No-03","CNT_LAT":22.1323710898,"CNT_LONG":90.2388422647}},

        //var geoCode = feature.properties[adminCode + "_code"],
        //    zoomLevel = (geoCode && (geoCode == 20 || geoCode == 30 || geoCode == 40)) ? 7.5 : 8.5;

        ////zoomLevel = map.getZoom() > zoomLevel ? map.getZoom() : zoomLevel;
        ////zoomLevel = adminCode == "dist" ? zoomLevel + 1.5 : zoomLevel;
        //zoomLevel = map.getZoom() > zoomLevel
        //    ? map.getZoom()
        //    : adminCode == "dist"
        //    ? zoomLevel + 1.5 : zoomLevel;

        //map.setView(polyCenter, zoomLevel);
    });
    //}


    adminLabels.addLayer(labelInfo);

    return;
}
*/

function setMapFilter(feature, adminCode, selectedAdminCode) {
    //return true;
    //console.log(feature.properties[adminCodeField]);

    var adminCodeField = adminCode + "_code";

    if (!feature || !feature.properties[adminCodeField] || feature.properties[adminCodeField] != selectedAdminCode)
        return false;
    else
        return true;

}


function set_map_filter() {

    var adminCode = "div",
        selectedAdminCode = "30";

    //console.log(adminCode);

    if (dataLayer && map.hasLayer(dataLayer)) {
        map.removeLayer(dataLayer);

        //console.log("dataLayer");
        dataLayer.filter = function (feature, layer) {
            var adminCode = "div",
                selectedAdminCode = "30";

            //adminCode = "div";
            //selectedAdminCode = "30";
            return setMapFilter(feature, adminCode, selectedAdminCode);
        };

        //map.addLayer(dataLayer);
    }


    if (mapLayers["admin_" + adminCode] && mapLayers["admin_" + adminCode].layer && map.hasLayer(mapLayers["admin_" + adminCode].layer)) {
        map.removeLayer(mapLayers["admin_" + adminCode].layer);

        //console.log("adminLayers");

        mapLayers["admin_" + adminCode].layer.filter = function (feature, layer) {
            return setMapFilter(feature, adminCode, selectedAdminCode);
        };

        //map.addLayer(mapLayers["admin_" + adminCode].layer);
    }

    return;
}

// GeoTIFF
function tt() {
    return;
    /*
    console.log("tif...");

    var tiff = "../js/maps/map_data/temp.tiff";
    var tif = "../js/maps/map_data/GW Level (m)1.tif";

    // Temperature and Geopotencial Height in GeoTIFF with 2 bands //
    d3.request(tiff).responseType('arraybuffer').get(
        function (error, tiffData) {

            // Geopotential height (BAND 0)
            //var geo = L.ScalarField.fromGeoTIFF(tiffData.response, bandIndex = 0);
            var geo = L.scalarField.fromGeoTIFF(tiffData.response, bandIndex = 0);

            var layerGeo = L.canvasLayer.scalarField(geo, {
                color: chroma.scale('RdPu').domain(geo.range),
                opacity: 0.65
            }).addTo(map);
            layerGeo.on('click', function (e) {
                console.log(e);
                if (e.value !== null) {
                    var v = e.value.toFixed(0);
                    var html = (`<span class="popupText">Geopotential height ${v} m</span>`);
                    var popup = L.popup()
                        .setLatLng(e.latlng)
                        .setContent(html)
                        .openOn(map);

                    console.log(html);
                }
            });

            // Temperature (BAND 1)
            var t = L.scalarField.fromGeoTIFF(tiffData.response, bandIndex = 1);
            var layerT = L.canvasLayer.scalarField(t, {
                color: chroma.scale('OrRd').domain(t.range),
                opacity: 0.65
            });
            layerT.on('click', function (e) {
                console.log(e);
                if (e.value !== null) {
                    var v = e.value.toFixed(1);
                    var html = (`<span class="popupText">Temperature ${e.value.toFixed(2)} ยบC</span>`);
                    var popup = L.popup()
                        .setLatLng(e.latlng)
                        .setContent(html)
                        .openOn(map);
                    console.log(html);
                }
            });

            L.control.layers({
                "Geopotential Height": layerGeo,
                "Temperature": layerT
            }, {}, {
                    position: 'bottomleft',
                    collapsed: false
                }).addTo(map);

            //map.fitBounds(layerGeo.getBounds());

        });

    //return;

    var gwLevel = L.leafletGeotiff(
        url = tiff,
        options = {
            band: 0,
            displayMin: 0,
            displayMax: 30,
            name: "Wind speed",
            colorScale: "rainbow",
            clampLow: false,
            clampHigh: true,
            //vector:true,
            arrowSize: 20,
        }
    ).addTo(map);


    fetch(tiff).then(r => r.arrayBuffer()).then(function (buffer) {
        console.log(buffer);

        var s = L.ScalarField.fromGeoTIFF(buffer),
            layer = L.canvasLayer.scalarField(s).addTo(map);
        console.log(s);
        console.log(layer);
        layer.on("click",
            function (e) {
                console.log(e);
                if (e.value !== null) {
                    var popup = L.popup()
                        .setLatLng(e.latlng)
                        //.setContent("${e.value}")
                        .setContent(`${e.value}`)
                        .openOn(map);
                }
            });

        map.fitBounds(layer.getBounds());
    });
    */
}

/*

var BigPointLayer = new L.CanvasLayer.extend({

    renderCircle: function (ctx, point, radius) {
        ctx.fillStyle = 'rgba(255, 60, 60, 0.2)';
        ctx.strokeStyle = 'rgba(255, 60, 60, 0.9)';
        ctx.beginPath();
        ctx.arc(point.x, point.y, radius, 0, Math.PI * 2.0, true, true);
        ctx.closePath();
        ctx.fill();
        ctx.stroke();
    },

    render: function () {
        var canvas = this.getCanvas();
        var ctx = canvas.getContext('2d');

        // clear canvas
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        // get center from the map (projected)
        var point = this._map.latLngToContainerPoint(new L.LatLng(0, 0));

        // render
        this.renderCircle(ctx, point, (1.0 + Math.sin(Date.now() * 0.001)) * 300);

        this.redraw();

    }
});

var layer = new BigPointLayer();
layer.addTo(map);

*/

