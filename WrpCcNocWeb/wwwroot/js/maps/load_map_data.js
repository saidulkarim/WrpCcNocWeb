
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

    dataAdminCode = $("#admin_info").val();
    dataAdminName = $("#admin_info option:selected").text();

    dataCode = $("#survey_info").val();
    dataName = $("#survey_info option:selected").text();
    equipmentId = $("#survey_info option:selected").attr("data-id");

    $("#map_label_opt").prop("checked", false);

    set_basic_opts();

    load_equipment_infos();


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
                            },
                            getLatLng = function (lat, lng) {
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
                            icon.color = "red";
                            icon.fillColor = "green";


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

                            map.addLayer(location);

                            //equipmentClusters[el.equipment_type].addLayer(location);


                            //console.log(icon.fillColor);
                            //console.log(location.icon);

                        });

                        //console.log(tc);
                        //map.addLayer(markerClusters);
                        //map.addLayer(locations);
                        //console.log(markerClusters);
                        //map.addLayer(equipmentClusters);

                        //map.addLayer(equipmentClusters[1]);
                        //map.addLayer(equipmentClusters[2]);
                        //map.addLayer(equipmentClusters[3]);
                        //map.addLayer(equipmentClusters[7]);
                        //map.addLayer(equipmentClusters[14]);


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
                            Project Locations +
                        "</label> <br /><div>");

                        //$("#map_legend_colors").append(
                        //    "<div id='map_legend_loc_2'><label class='map_legend_label'>" +
                        //    "<label class='map_legend_color map_legend_circle' style='border-color:#e43; background-color:#3e2;'></label>" +
                        //    equipmentTypes[2] +
                        //    "</label> <br /><div>");

                        //$("#map_legend_colors").append(
                        //    "<div id='map_legend_loc_3'><label class='map_legend_label'>" +
                        //    "<label class='map_legend_color map_legend_circle' style='border-color:#34e; background-color:#e32;'></label>" +
                        //    equipmentTypes[3] +
                        //    "</label> <br /><div>");

                        //$("#map_legend_colors").append(
                        //    "<div id='map_legend_loc_7'><label class='map_legend_label'>" +
                        //    "<label class='map_legend_color map_legend_circle' style='border-color:#23e; background-color:#5e8;'></label>" +
                        //    equipmentTypes[7] +
                        //    "</label> <br /><div>");

                        //$("#map_legend_colors").append(
                        //    "<div id='map_legend_loc_14'><label class='map_legend_label'>" +
                        //    "<label class='map_legend_color map_legend_circle' style='border-color:#111; background-color:#21e;'></label>" +
                        //    equipmentTypes[14] +
                        //    "</label> <br /><div>");



                        //$("#eqp_types").slideDown(350);
                        //$("#eqp_types").find("input[type='checkbox']").each(function () {
                        //    $(this).prop("checked", true);
                        //    $(this).prop("disabled", false);

                        //    $(this).on("change",
                        //        function () {

                        //            if ($("#eqp_types").find("input[type='checkbox']").length === $("#eqp_types").find("input[type='checkbox']:checked").length) {
                        //                $("#eqp_loc").prop("indeterminate", false);
                        //                $("#eqp_loc").prop("checked", true);
                        //            } else if ($("#eqp_types").find("input[type='checkbox']:checked").length > 0) {
                        //                $("#eqp_loc").prop("indeterminate", true);
                        //                $("#eqp_loc").prop("checked", false);
                        //            } else {
                        //                $("#eqp_loc").prop("indeterminate", false);
                        //                $("#eqp_loc").prop("checked", false);
                        //            }

                        //            if (!$(this).prop("id").replace("eqp_", "") || !equipmentClusters)
                        //                return;

                        //            var eqpId = $(this).prop("id").replace("eqp_", "");

                        //            if (!equipmentClusters[eqpId])
                        //                return;

                        //            if (this.checked) {
                        //                if (!map.hasLayer(equipmentClusters[eqpId]))
                        //                    map.addLayer(equipmentClusters[eqpId]);

                        //                if ($("#map_legend_loc_" + eqpId).length)
                        //                    $("#map_legend_loc_" + eqpId).show();
                        //            } else {
                        //                if (map.hasLayer(equipmentClusters[eqpId]))
                        //                    map.removeLayer(equipmentClusters[eqpId]);

                        //                if ($("#map_legend_loc_" + eqpId).length)
                        //                    $("#map_legend_loc_" + eqpId).hide();
                        //            }
                        //        });
                        //});

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
        //for (var ei = 1; ei <= 3; ei++) {
        //    if (map.hasLayer(equipmentClusters[ei]))
        //        map.removeLayer(equipmentClusters[ei]);

        //    if ($("#map_legend_loc_" + ei).length)
        //        $("#map_legend_loc_" + ei).remove();
        //}

        //equipmentClusters = {};

        //$("#eqp_types").find("input[type='checkbox']").each(function () {
        //    $(this).prop("checked", false);
        //    $(this).prop("disabled", true);
        //});

        //$("#eqp_types").slideUp(350);

    }
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


