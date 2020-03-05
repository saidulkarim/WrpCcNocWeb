jQuery(document).ready(function () {
    BindData();

    GetHydroSystemData();
    GetFloodFrequencyData();
    GetIrrigatedCropAreaData();
    GetAoFOData();
    GetDswpdData();
    GetEiaData();
    GetSiaData();
    GetEconFinAnaData();

    jQuery("#btnSaveGeneral").click(function () {
        GeneralInfoSave();
    });

    jQuery("#btnNext").click(function (e) {
        //block 1
        if (jQuery("#lnkTab1").hasClass('active')) {
            var selectedFormType = jQuery("#ddlTypeOfForms").val();

            if (selectedFormType === '') {
                Notification('warning', 'Required Field', 'Please select a form.');
                e.preventDefault();
                return false;
            }
        }
        //end block 1
    });

    jQuery("#btnPrevious").click(function () {

    });
});

jQuery(window).bind('keydown', function (event) {
    if (event.ctrlKey || event.metaKey) {
        switch (String.fromCharCode(event.which).toLowerCase()) {
            case 's':
                event.preventDefault();
                alert('ctrl-s');
                break;

            //case 'f':
            //    event.preventDefault();
            //    alert('ctrl-f');
            //    break;

            //case 'g':
            //    event.preventDefault();
            //    alert('ctrl-g');
            //    break;
        }
    }
});

jQuery("#DistrictGeoCode").change(function () {

});

jQuery("#UpazilaGeoCode").change(function () {

});

jQuery("input[type='radio'][name='YesNoStakeId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divDSDSPYesControls").removeClass('d-none fadeOut');
        jQuery("#divDSDSPYesControls").addClass('d-block fadeIn');
    } else {
        jQuery("#divDSDSPYesControls").removeClass('d-block fadeIn');
        jQuery("#divDSDSPYesControls").addClass('d-none fadeOut');
    }
});

jQuery("input[type='radio'][name='AnalyzeOptYesNoId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divAoFOYes").removeClass('d-none fadeOut');
        jQuery("#divAoFOYes").addClass('d-block fadeIn');
    } else {
        jQuery("#divAoFOYes").removeClass('d-block fadeIn');
        jQuery("#divAoFOYes").addClass('d-none fadeOut');
    }
});

jQuery("input[type='radio'][name='EnvAndSocialYesNoId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divEIAYes, #divSIAYes").removeClass('d-none fadeOut');
        jQuery("#divEIAYes, #divSIAYes").addClass('d-block fadeIn');
    } else {
        jQuery("#divEIAYes, #divSIAYes").removeClass('d-block fadeIn');
        jQuery("#divEIAYes, #divSIAYes").addClass('d-none fadeOut');
    }
});

jQuery("input[type='radio'][name='CompatNWPYesNoId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divCwnwpPolicy").removeClass('d-none fadeOut');
        jQuery("#divCwnwpPolicy").addClass('d-block fadeIn');
    } else {
        jQuery("#divCwnwpPolicy").removeClass('d-block fadeIn');
        jQuery("#divCwnwpPolicy").addClass('d-none fadeOut');
    }
});

jQuery("input[type='radio'][name='NWMPCompatYesNoId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divCNWMP").removeClass('d-none fadeOut');
        jQuery("#divCNWMP").addClass('d-block fadeIn');
    } else {
        jQuery("#divCNWMP").removeClass('d-block fadeIn');
        jQuery("#divCNWMP").addClass('d-none fadeOut');
    }
});

jQuery("input[type='radio'][name='SDGYesNoId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divCwsdg").removeClass('d-none fadeOut');
        jQuery("#divCwsdg").addClass('d-block fadeIn');
    } else {
        jQuery("#divCwsdg").removeClass('d-block fadeIn');
        jQuery("#divCwsdg").addClass('d-none fadeOut');
    }
});

jQuery("input[type='radio'][name='DeltaPlanYesNoId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divCwbdp2100").removeClass('d-none fadeOut');
        jQuery("#divCwbdp2100").addClass('d-block fadeIn');
    } else {
        jQuery("#divCwbdp2100").removeClass('d-block fadeIn');
        jQuery("#divCwbdp2100").addClass('d-none fadeOut');
    }
});

jQuery("input[type='radio'][name='GPWMYesNoId']").change(function () {
    if (jQuery(this).val() == 1) {
        jQuery("#divCwgpwm").removeClass('d-none fadeOut');
        jQuery("#divCwgpwm").addClass('d-block fadeIn');
    } else {
        jQuery("#divCwgpwm").removeClass('d-block fadeIn');
        jQuery("#divCwgpwm").addClass('d-none fadeOut');
    }
});

//start :: Hydrological System JS
jQuery("#HydroSystemCategoryId").change(function () {
    var name = jQuery("#HydroSystemCategoryId option:selected").text();
    var unit = jQuery("#HydroSystemCategoryId option:selected").attr('title');

    if (unit != '' && unit != undefined) {
        jQuery("#HydroSystemName").text(name);
        jQuery("#HydroSysCategoryUnit").parent('div').removeClass('d-none');
        jQuery("#HydroSysCategoryUnit").text(unit);
    } else {
        jQuery("#HydroSystemName").text('');
        jQuery("#HydroSysCategoryUnit").parent('div').addClass('d-none');
        jQuery("#HydroSysCategoryUnit").text('');
    }
});

jQuery("#btnSaveHydroSystem").click(function () {
    var HydroSystemId = jQuery("#HydroSystemCategoryId").val();
    var HydroSystemName = jQuery("#NameOfHydroSystem").val();
    var LengthArea = jQuery("#HydroSystemLengthArea").val();
    var unit = jQuery("#HydroSystemCategoryId option:selected").attr('title');

    if (HydroSystemId != '') {
        if (HydroSystemName != '') {
            if (LengthArea != '') {
                var ProjectId = GetHiddenValue('ProjectId');

                var hydroSystemDetail = {
                    "HydroSysDetailId": 0,
                    "ProjectId": ProjectId,
                    "HydroSystemCategoryId": HydroSystemId,
                    "NameOfHydroSystem": HydroSystemName,
                    "HydroSystemLengthArea": LengthArea,
                    "HydroSystemUnit": unit
                };

                var url = '@Url.Action("hsds", "form")';
                jQuery.ajax({
                    type: "POST",
                    url: url,
                    data: { _hsd: hydroSystemDetail },
                    dataType: "json",
                    success: function (data, status, jqXHR) {
                        if (data.status == 'success') {
                            Notification('success', 'Success', data.message);
                            GetHydroSystemData();
                            ClearHydroSystem();
                        } else {
                            Notification('error', 'Saving Error', data.message);
                        }
                    }, error: function (jqXHR, exception) {
                        var errorMsg = AjaxErrorHandle(jqXHR, exception);
                        Notification('error', 'Error Occured', errorMsg);
                    }
                });
            } else {
                Notification('warning', 'Required Field', 'Please enter length area!');
                jQuery("#HydroSystemLengthArea").focus();
            }
        } else {
            Notification('warning', 'Required Field', 'Please enter name!');
            jQuery("#NameOfHydroSystem").focus();
        }
    } else {
        Notification('warning', 'Required Field', 'Please select a hydrological system!');
        jQuery("#HydroSystemCategoryId").focus();
    }
});

jQuery("#btnClearHydroSystem").click(function () {
    ClearHydroSystem();
});

function ClearHydroSystem() {
    jQuery("#HydroSystemCategoryId").val('');
    jQuery("#NameOfHydroSystem").val('');
    jQuery("#HydroSystemLengthArea").val('');
    jQuery("#HydroSystemName").text('');
    jQuery("#HydroSysCategoryUnit").text('');
    jQuery("#HydroSysCategoryUnit").parent('div').addClass('d-none');
}

function GetHydroSystemData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_hsd", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.hydroSystemCategory + '</td><td>' + item.nameOfHydroSystem + '</td><td>' + item.hydroSystemLengthArea + ' (' + item.hydroSystemUnit + ')</td><td class="text-center"><button title="' + item.hydroSysDetailId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.hydroSysDetailId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodyHydrologicalSystem").html(rows);
            } else {
                jQuery("#tbodyHydrologicalSystem").html('<tr><td colspan="5" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodyHydrologicalSystem").html('<tr><td colspan="5" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Flood Frequency JS
jQuery("#FloodFrequencyId").change(function () {
    var name = jQuery("#FloodFrequencyId option:selected").text();
    //var unit = jQuery("#FloodFrequencyId option:selected").attr('title');
});

jQuery("#btnSaveFloodFrequency").click(function () {
    var FloodFrequencyId = jQuery("#FloodFrequencyId option:selected").val();
    var FloodFrequencyLevel = jQuery("#FloodFrequencyLevel").val();

    if (FloodFrequencyId != '') {
        if (FloodFrequencyLevel != '') {
            var ProjectId = GetHiddenValue('ProjectId');
            var floodFrequencyDetail = {
                "FloodFrequencyDetailId": 0,
                "ProjectId": ProjectId,
                "FloodFrequencyId": FloodFrequencyId,
                "FloodFrequencyLevel": FloodFrequencyLevel
            };

            var url = '@Url.Action("ffds", "form")';
            jQuery.ajax({
                type: "POST",
                url: url,
                data: { _ffd: floodFrequencyDetail },
                dataType: "json",
                success: function (data, status, jqXHR) {
                    if (data.status == 'success') {
                        Notification('success', 'Success', data.message);
                        GetFloodFrequencyData();
                        ClearFloodFrequency();
                    } else {
                        Notification('error', 'Saving Error', data.message);
                    }
                }, error: function (jqXHR, exception) {
                    var errorMsg = AjaxErrorHandle(jqXHR, exception);
                    Notification('error', 'Error Occured', errorMsg);
                }
            });
        } else {
            Notification('warning', 'Required Field', 'Please enter frequency level!');
            jQuery("#FloodFrequencyLevel").focus();
        }
    } else {
        Notification('warning', 'Required Field', 'Please select a flood frequency!');
        jQuery("#FloodFrequencyId").focus();
    }
});

jQuery("#btnClearFloodFrequency").click(function () {
    ClearFloodFrequency();
});

function ClearFloodFrequency() {
    jQuery("#FloodFrequencyId").val('');
    jQuery("#FloodFrequencyLevel").val('');
}

function GetFloodFrequencyData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_ffd", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.floodFrequency + '</td><td>' + item.floodFrequencyLevel + '</td><td class="text-center"><button title="' + item.floodFrequencyDetailId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.floodFrequencyDetailId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodyFloodFrequency").html(rows);
            } else {
                jQuery("#tbodyFloodFrequency").html('<tr><td colspan="4" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodyFloodFrequency").html('<tr><td colspan="4" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Irrigated Crop Area JS
jQuery("#HydroSystemCategoryId").change(function () {
    var name = jQuery("#HydroSystemCategoryId option:selected").text();
});

jQuery("#btnSaveIrrigatedCropArea").click(function () {
    var CropName = jQuery("#CropName").val();
    var Area = jQuery("#Area").val();

    if (CropName != '') {
        if (Area != '') {
            var ProjectId = GetHiddenValue('ProjectId');
            var irrigCropAreaDetail = {
                "IrrigatedCropId": 0,
                "ProjectId": ProjectId,
                "CropName": CropName,
                "Area": Area
            };

            var url = '@Url.Action("icas", "form")';
            jQuery.ajax({
                type: "POST",
                url: url,
                data: { _ica: irrigCropAreaDetail },
                dataType: "json",
                success: function (data, status, jqXHR) {
                    if (data.status == 'success') {
                        Notification('success', 'Success', data.message);

                        GetIrrigatedCropAreaData();
                        ClearIrrigatedCropArea();
                    } else {
                        Notification('error', 'Saving Error', data.message);
                    }
                }, error: function (jqXHR, exception) {
                    var errorMsg = AjaxErrorHandle(jqXHR, exception);
                    Notification('error', 'Error Occured', errorMsg);
                }
            });
        } else {
            Notification('warning', 'Required Field', 'Please enter area!');
            jQuery("#Area").focus();
        }
    } else {
        Notification('warning', 'Required Field', 'Please enter crop name!');
        jQuery("#CropName").focus();
    }
});

jQuery("#btnClearIrrigatedCropArea").click(function () {
    ClearIrrigatedCropArea();
});

function ClearIrrigatedCropArea() {
    jQuery("#CropName").val('');
    jQuery("#Area").val('');
}

function GetIrrigatedCropAreaData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_ica", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.cropName + '</td><td>' + item.area + '</td><td class="text-center"><button title="' + item.irrigatedCropId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.irrigatedCropId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodyIrrigatedCropArea").html(rows);
            } else {
                jQuery("#tbodyIrrigatedCropArea").html('<tr><td colspan="4" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodyIrrigatedCropArea").html('<tr><td colspan="4" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Analyze options to fulfill objective
jQuery("#btnSaveAoFOYes").click(function () {
    var OptionNumber = jQuery("#OptionNumber").val();
    var AnalyzeDescription = jQuery("#AnalyzeDescription").val();
    var AnalyzeRemarks = jQuery("#AnalyzeRemarks").val();

    if (AnalyzeDescription != '') {
        var ProjectId = GetHiddenValue('ProjectId');
        var analyzeOptionsDetail = {
            "AnalyzeOptionsId": 0,
            "ProjectId": ProjectId,
            "OptionNumber": OptionNumber,
            "AnalyzeDescription": AnalyzeDescription,
            "AnalyzeRemarks": AnalyzeRemarks
        };

        var url = '@Url.Action("aofos", "form")';
        jQuery.ajax({
            type: "POST",
            url: url,
            data: { _aofd: analyzeOptionsDetail },
            dataType: "json",
            success: function (data, status, jqXHR) {
                if (data.status == 'success') {
                    Notification('success', 'Success', data.message);

                    GetAoFOData();
                    ClearAoFO();
                } else {
                    Notification('error', 'Saving Error', data.message);
                }
            }, error: function (jqXHR, exception) {
                var errorMsg = AjaxErrorHandle(jqXHR, exception);
                Notification('error', 'Error Occured', errorMsg);
            }
        });
    } else {
        Notification('warning', 'Required Field', 'Please enter analyze description!');
        jQuery("#CropName").focus();
    }
});

jQuery("#btnClearAoFOYes").click(function () {
    ClearAoFO();
});

function ClearAoFO() {
    jQuery("#OptionNumber").val('');
    jQuery("#AnalyzeDescription").val('');
    jQuery("#AnalyzeRemarks").val('');
}

function GetAoFOData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_aofo", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.optionNumber + '</td><td>' + item.analyzeDescription + '</td><td>' + item.analyzeRemarks + '</td><td class="text-center"><button title="' + item.analyzeOptionsId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.analyzeOptionsId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodyAnalyzeOptFulfillObj").html(rows);
            } else {
                jQuery("#tbodyAnalyzeOptFulfillObj").html('<tr><td colspan="5" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodyAnalyzeOptFulfillObj").html('<tr><td colspan="5" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Design submitted with project document JS
jQuery("#btnSaveDSWPD").click(function () {
    var DesignSubmittedParameterId = jQuery("#DesignSubmittedParameterId option:selected").val();
    var DswpYesNoId = jQuery("input:radio[name='dswpdYN']:checked").val();
    DswpYesNoId = (DswpYesNoId == '') ? 0 : DswpYesNoId;
    var DesignSubmitApplicantCmt = jQuery("#DesignSubmitApplicantCmt").val();
    var DesignSubmitAuthorityCmt = jQuery("#DesignSubmitAuthorityCmt").val();

    if (AnalyzeDescription != '') {
        var ProjectId = GetHiddenValue('ProjectId');
        var designSubParamDetail = {
            "DesignSubmittedId": 0,
            "ProjectId": ProjectId,
            "DesignSubmittedParameterId": DesignSubmittedParameterId,
            "YesNoId": DswpYesNoId,
            "DesignSubmitApplicantCmt": DesignSubmitApplicantCmt,
            "DesignSubmitAuthorityCmt": DesignSubmitAuthorityCmt
        };

        console.log(designSubParamDetail);
        console.log(DswpYesNoId);

        var url = '@Url.Action("dswpds", "form")';
        jQuery.ajax({
            type: "POST",
            url: url,
            data: { _dspd: designSubParamDetail },
            dataType: "json",
            success: function (data, status, jqXHR) {
                if (data.status == 'success') {
                    Notification('success', 'Success', data.message);

                    GetDswpdData();
                    ClearDswpd();
                } else {
                    Notification('error', 'Saving Error', data.message);
                }
            }, error: function (jqXHR, exception) {
                var errorMsg = AjaxErrorHandle(jqXHR, exception);
                Notification('error', 'Error Occured', errorMsg);
            }
        });
    } else {
        Notification('warning', 'Required Field', 'Please enter analyze description!');
        jQuery("#CropName").focus();
    }
});

jQuery("#btnClearDSWPD").click(function () {
    ClearDswpd();
});

function ClearDswpd() {
    jQuery("#DesignSubmittedParameterId").val('');
    jQuery("#DesignSubmitApplicantCmt").val('');
    jQuery("#DesignSubmitAuthorityCmt").val('');
    //jQuery("input:radio[name='dswpdYN']").val('');
    //jQuery("input:radio[name='dswpdYN']:checked").prop('checked', false);
}

function GetDswpdData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_dswpd", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.parameterName + '</td><td>' + item.dswpdYN + '</td><td>' + item.designSubmitApplicantCmt + '</td><td>' + item.designSubmitAuthorityCmt + '</td><td class="text-center"><button title="' + item.designSubmittedId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.designSubmittedId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodyDesignSubProjDoc").html(rows);
            } else {
                jQuery("#tbodyDesignSubProjDoc").html('<tr><td colspan="6" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodyDesignSubProjDoc").html('<tr><td colspan="6" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Environmental Impact Assessment
jQuery("#btnSaveEIAYes").click(function () {
    var EIAParameterId = jQuery("#EIAParameterId option:selected").val();
    var PreProjectSituation = jQuery("#PreProjectSituation").val();
    var PostProjectSituation = jQuery("#PostProjectSituation").val();
    var PositiveNegativeImpact = jQuery("#PositiveNegativeImpact").val();
    var MitigationPlan = jQuery("#MitigationPlan").val();

    if (EIAParameterId != '') {
        var ProjectId = GetHiddenValue('ProjectId');
        var eiaDetail = {
            "EIAId": 0,
            "ProjectId": ProjectId,
            "EIAParameterId": EIAParameterId,
            "PreProjectSituation": PreProjectSituation,
            "PostProjectSituation": PostProjectSituation,
            "PositiveNegativeImpact": PositiveNegativeImpact,
            "MitigationPlan": MitigationPlan
        };

        var url = '@Url.Action("eiads", "form")';
        jQuery.ajax({
            type: "POST",
            url: url,
            data: { _eia: eiaDetail },
            dataType: "json",
            success: function (data, status, jqXHR) {
                if (data.status == 'success') {
                    Notification('success', 'Success', data.message);

                    GetEiaData();
                    ClearEIA();
                } else {
                    Notification('error', 'Saving Error', data.message);
                }
            }, error: function (jqXHR, exception) {
                var errorMsg = AjaxErrorHandle(jqXHR, exception);
                Notification('error', 'Error Occured', errorMsg);
            }
        });
    } else {
        Notification('warning', 'Required Field', 'Please select an option from EIA parameter!');
        jQuery("#EIAParameterId").focus();
    }
});

jQuery("#btnClearEIAYes").click(function () {
    ClearEIA();
});

function ClearEIA() {
    jQuery("#EIAParameterId").val('');
    jQuery("#PreProjectSituation").val('');
    jQuery("#PostProjectSituation").val('');
    jQuery("#PositiveNegativeImpact").val('');
    jQuery("#MitigationPlan").val('');
}

function GetEiaData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_eiad", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.parameterName + '</td><td>' + item.preProjectSituation + '</td><td>' + item.postProjectSituation + '</td><td>' + item.positiveNegativeImpact + '</td><td>' + item.mitigationPlan + '</td><td class="text-center"><button title="' + item.eiaId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.eiaId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodyEIADetail").html(rows);
            } else {
                jQuery("#tbodyEIADetail").html('<tr><td colspan="7" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodyEIADetail").html('<tr><td colspan="7" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Social Impact Assessment
jQuery("#btnSaveSIAYes").click(function () {
    var SIAParameterId = jQuery("#SIAParameterId option:selected").val();
    var SiaPreProjectSituation = jQuery("#SiaPreProjectSituation").val();
    var SiaPostProjectSituation = jQuery("#SiaPostProjectSituation").val();
    var SiaPositiveNegativeImpact = jQuery("#SiaPositiveNegativeImpact").val();
    var SiaMitigationPlan = jQuery("#SiaMitigationPlan").val();

    if (SIAParameterId != '') {
        var ProjectId = GetHiddenValue('ProjectId');
        var siaDetail = {
            "SIAId": 0,
            "ProjectId": ProjectId,
            "SIAParameterId": SIAParameterId,
            "PreProjectSituation": SiaPreProjectSituation,
            "PostProjectSituation": SiaPostProjectSituation,
            "PositiveNegativeImpact": SiaPositiveNegativeImpact,
            "MitigationPlan": SiaMitigationPlan
        };

        var url = '@Url.Action("siads", "form")';
        jQuery.ajax({
            type: "POST",
            url: url,
            data: { _sia: siaDetail },
            dataType: "json",
            success: function (data, status, jqXHR) {
                if (data.status == 'success') {
                    Notification('success', 'Success', data.message);

                    GetSiaData();
                    ClearSIA();
                } else {
                    Notification('error', 'Saving Error', data.message);
                }
            }, error: function (jqXHR, exception) {
                var errorMsg = AjaxErrorHandle(jqXHR, exception);
                Notification('error', 'Error Occured', errorMsg);
            }
        });
    } else {
        Notification('warning', 'Required Field', 'Please select an option from SIA parameter!');
        jQuery("#EIAParameterId").focus();
    }
});

jQuery("#btnClearSIAYes").click(function () {
    ClearSIA();
});

function ClearSIA() {
    jQuery("#SIAParameterId").val('');
    jQuery("#SiaPreProjectSituation").val('');
    jQuery("#SiaPostProjectSituation").val('');
    jQuery("#SiaPositiveNegativeImpact").val('');
    jQuery("#SiaMitigationPlan").val('');
}

function GetSiaData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_siad", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.siaParameterName + '</td><td>' + item.preProjectSituation + '</td><td>' + item.postProjectSituation + '</td><td>' + item.positiveNegativeImpact + '</td><td>' + item.mitigationPlan + '</td><td class="text-center"><button title="' + item.siaId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.siaId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodySIADetail").html(rows);
            } else {
                jQuery("#tbodySIADetail").html('<tr><td colspan="7" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodySIADetail").html('<tr><td colspan="7" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Economical & Financial Analysis
jQuery("#btnSaveEconFinAna").click(function () {
    var EcoAndFinancialParamId = jQuery("#EcoAndFinancialParamId option:selected").val();
    var EcoAndFinParamUnit = jQuery("#EcoAndFinancialParamUnit option:selected").val();
    var EcoAndFinApplicantCmt = jQuery("#EcoAndFinancialApplicantCmt").val();
    var EcoAndFinAuthorityCmt = jQuery("#EcoAndFinancialAuthorityCmt").val();

    if (EcoAndFinancialParamId != '') {
        var ProjectId = GetHiddenValue('ProjectId');
        var ecoFinAnaDetail = {
            "EconomicalAndFinancialId": 0,
            "ProjectId": ProjectId,
            "EcoAndFinancialParamId": EcoAndFinancialParamId,
            "EcoAndFinancialParamUnit": EcoAndFinParamUnit,
            "EcoAndFinancialApplicantCmt": EcoAndFinApplicantCmt,
            "EcoAndFinancialAuthorityCmt": EcoAndFinAuthorityCmt
        };

        var url = '@Url.Action("efas", "form")';
        jQuery.ajax({
            type: "POST",
            url: url,
            data: { _efas: ecoFinAnaDetail },
            dataType: "json",
            success: function (data, status, jqXHR) {
                if (data.status == 'success') {
                    Notification('success', 'Success', data.message);

                    GetEconFinAnaData();
                    ClearEconFinAna();
                } else {
                    Notification('error', 'Saving Error', data.message);
                }
            }, error: function (jqXHR, exception) {
                var errorMsg = AjaxErrorHandle(jqXHR, exception);
                Notification('error', 'Error Occured', errorMsg);
            }
        });
    } else {
        Notification('warning', 'Required Field', 'Please select an option!');
        jQuery("#EcoAndFinancialParamId").focus();
    }
});

jQuery("#btnClearEconFinAna").click(function () {
    ClearEconFinAna();
});

function ClearEconFinAna() {
    jQuery("#EcoAndFinancialParamId").val('');
    jQuery("#EcoAndFinancialParamUnit").val('');
    jQuery("#EcoAndFinancialApplicantCmt").val('');
    jQuery("#EcoAndFinancialAuthorityCmt").val('');
}

function GetEconFinAnaData() {
    var ProjectId = GetHiddenValue('ProjectId');
    var url = '@Url.Action("get_efad", "form")';

    jQuery.ajax({
        type: "GET",
        url: url,
        data: { project_id: ProjectId },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.length > 0) {
                console.log(data);
                var rows = '';

                var count = 0;
                jQuery.each(data, function (i, item) {
                    ++count;
                    rows += '<tr><td scope="row" class="bold">' + count + '</td><td>' + item.ecoAndFinancialParamName + '</td><td>' + item.ecoAndFinancialParamUnit + '</td><td>' + item.ecoAndFinancialApplicantCmt + '</td><td>' + item.ecoAndFinancialAuthorityCmt + '</td><td class="text-center"><button title="' + item.economicalAndFinancialId + '" type="button" class="btn btn-success waves-effect waves-light"><i class="mdi mdi-lead-pencil"></i></button> <button title="' + item.economicalAndFinancialId + '" type="button" class="btn btn-danger waves-effect waves-light"><i class="mdi mdi-delete"></i></button></td></tr>';
                });

                jQuery("#tbodyEcoFinAna").html(rows);
            } else {
                jQuery("#tbodyEcoFinAna").html('<tr><td colspan="6" class="text-center text-red"><div class="alert alert-warning" role="alert"> Sorry, no data found </div></td></tr>');
                Notification('error', 'Data Fetch Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            jQuery("#tbodyEcoFinAna").html('<tr><td colspan="6" class="text-center text-red"><div class="alert alert-danger" role="alert">' + errorMsg + '</div></td></tr>');
            Notification('error', 'Error Occured', errorMsg);
        }
    });
}
//end

//start :: Technical Information JS
jQuery("#btnSaveTechInfo").click(function () {
    var FinalObj = [];
    var hydroRegions = [], bdp2100HotSpot = [], typesOfFlood = [];
    var ProjectId = GetHiddenValue('ProjectId');

    $.each($("input[name='hydrological_region']:checked"), function () {
        var _hr = {
            "PrjHydroRegionDetailId": 0,
            "ProjectId": ProjectId,
            "HydroRegionId": $(this).val()
        };

        hydroRegions.push(_hr);
    });

    $.each($("input[name='bdp2100_hot_spot']:checked"), function () {
        var _hsp = {
            "HotSpotDetailId": 0,
            "ProjectId": ProjectId,
            "DeltaPlanHotSpotId": $(this).val()
        };

        bdp2100HotSpot.push(_hsp);
    });

    $.each($("input[name='types_of_flood']:checked"), function () {
        var _tof = {
            "FloodTypeDetailId": 0,
            "ProjectId": ProjectId,
            "FloodTypeId": $(this).val()
        };

        typesOfFlood.push(_tof);
    });

    var ProjectCommonDetail = {
        "ProjectId": ProjectId,
        "AnnualRainFallLast1Year": GetTextValue('AnnualRainFallLast1Year'),
        "AnnualRainFallLast2Years": GetTextValue('AnnualRainFallLast2Years'),
        "AnnualRainFallLast3Years": GetTextValue('AnnualRainFallLast3Years'),
        "AnnualRainFallLast4Years": GetTextValue('AnnualRainFallLast4Years'),
        "AnnualRainFallLast5Years": GetTextValue('AnnualRainFallLast5Years'),
        "IssueChallageProblem": GetTextValue('IssueChallageProblem'),
        "YesNoStakeId": GetRadioValue('YesNoStakeId'),
        "DiscussWithStakeApplicantCmt": GetTextValue('DiscussWithStakeApplicantCmt'),
        "DiscussWithStakeAuthorityCmt": GetTextValue('DiscussWithStakeAuthorityCmt'),
        "DiscussWithStakePosFeedback": GetTextValue('DiscussWithStakePosFeedback'),
        "DiscussWithStakeNegFeedback": GetTextValue('DiscussWithStakeNegFeedback'),
        //"DiscussWithStakeParticipntLst": , //file
        //"DiscussWithStakeMeetingMin": , //file
        "AnalyzeOptYesNoId": GetRadioValue('AnalyzeOptYesNoId'),
        "AnalyzeOptionsApplicantCmt": GetTextValue('AnalyzeOptionsApplicantCmt'),
        "AnalyzeOptionsAuthorityCmt": GetTextValue('AnalyzeOptionsAuthorityCmt'),
        "EnvAndSocialYesNoId": GetRadioValue('EnvAndSocialYesNoId'),
        "EnvAndSocialApplicantCmt": GetTextValue('EnvAndSocialApplicantCmt'),
        "EnvAndSocialAuthorityCmt": GetTextValue('EnvAndSocialAuthorityCmt')
    };

    var Project31IndvDetail = {
        "Project31IndvId": 0,
        "ProjectId": ProjectId,
        "ConnectivityAmidWaterland": GetTextValue('ConnectivityAmidWaterland'),
        "CatchmentArea": GetTextValue('CatchmentArea'),
        "HighestFloodLevel": GetTextValue('HighestFloodLevel'),
        "MaximumDischarge": GetTextValue('MaximumDischarge'),
        "DrainageConditionId": GetRadioValue('drainage_condition'),
        "WaterSalinity": GetTextValue('WaterSalinity'),
        "WaterDO": GetTextValue('WaterDO'),
        "WaterTDS": GetTextValue('WaterTDS'),
        "WaterPhLevel": GetTextValue('WaterPhLevel'),
        "HighLandPercent": GetTextValue('HighLandPercent'),
        "MediumHighLandPercent": GetTextValue('MediumHighLandPercent'),
        "MediumLowLandPercent": GetTextValue('MediumLowLandPercent'),
        "LowLandPercent": GetTextValue('LowLandPercent'),
        "VeryLowLandPercent": GetTextValue('VeryLowLandPercent'),
        "CultivableCrops": GetTextValue('CultivableCrops'),
        "CropProduction": GetTextValue('CropProduction'),
        "FishProduction": GetTextValue('FishProduction'),
        "FishDiversity": GetTextValue('FishDiversity'),
        "FishMigration": GetTextValue('FishMigration'),
        "FloraAndFauna": GetTextValue('FloraAndFauna'),
        "LandLessPeoplePercentage": GetTextValue('LandLessPeoplePercentage'),
        "SmallFarmerPercentage": GetTextValue('SmallFarmerPercentage'),
        "AvgMonthlyIncome": GetTextValue('AvgMonthlyIncome'),
        "UseOfToolsYesNoId": GetRadioValue('UseOfToolsYesNoId'),
        "ToolsApplicantComments": GetTextValue('ToolsApplicantComments'),
        "ToolsAuthorityComments": GetTextValue('ToolsAuthorityComments')
    };

    var _Form31FinalObj = {
        "CommonDetail": ProjectCommonDetail,
        "Project31Indv": Project31IndvDetail,
        "HydroRegion": hydroRegions,
        "BDP2100HotSpot": bdp2100HotSpot,
        "TypesOfFlood": typesOfFlood
    }

    console.log(_Form31FinalObj); //jQuery('#ProjectId').val()

    var url = '@Url.Action("f31tiotos", "form")';
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { _form31TechInfo: _Form31FinalObj },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.status == 'success') {
                Notification('success', 'Success', data.message);
            } else {
                Notification('error', 'Saving Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            Notification('error', 'Error Occured', errorMsg);
        }
    });
});

jQuery("#btnClearTechInfo").click(function () {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        type: "warning",
        allowEscapeKey: false,
        allowOutsideClick: false,
        showCancelButton: !0,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sure"
    }).then(function (t) {
        t.value && Swal.fire("Cleared!", "Your entry data has been cleared successfully.", "success")
    });
});
//end

//start :: Deed Obligatory JS
jQuery("#btnSaveDeed").click(function () {
    var _finalDeedObj = [];
    var nwpArticles = [], prjCompatNWMP = [], sdgGoals = [],
        sdgIndicators = [], dp2100Goals = [], gpwmGroupType = [];
    var ProjectId = GetHiddenValue('ProjectId');
    var Project31IndvId = GetHiddenValue('Project31IndvId');

    $.each($("input[name='NationalWaterPolicyArticleId']:checked"), function () {
        var _nwpa = {
            "PrjCompatNWPId": 0,
            "ProjectId": ProjectId,
            "NationalWaterPolicyArticleId": $(this).val()
        };

        nwpArticles.push(_nwpa);
    });

    $.each($("input[name='PrjCompatNWMPId']:checked"), function () {
        var _nwmpa = {
            "PrjCompatNWMPId": 0,
            "ProjectId": ProjectId,
            "NWMPProgrammeId": $(this).val()
        };

        prjCompatNWMP.push(_nwmpa);
    });

    $.each($("input[name='SDGGoalId']:checked"), function () {
        var _sdg = {
            "SDGCompabilityId": 0,
            "ProjectId": ProjectId,
            "SDGGoalId": $(this).val()
        };

        sdgGoals.push(_sdg);
    });

    $.each($("input[name='SDGIndicatorId']:checked"), function () {
        var _sdgi = {
            "SDGIndicatorDetailId": 0,
            "ProjectId": ProjectId,
            "SDGIndicatorId": $(this).val()
        };

        sdgIndicators.push(_sdgi);
    });

    $.each($("input[name='DeltPlan2100GoalId']:checked"), function () {
        var _dp2100g = {
            "DeltaGoalDetailId": 0,
            "ProjectId": ProjectId,
            "DeltPlan2100GoalId": $(this).val()
        };

        dp2100Goals.push(_dp2100g);
    });

    $.each($("input[name='GPWMGroupTypeId']:checked"), function () {
        var _gpwmg = {
            "GPWMGroupTypeDetailId": 0,
            "ProjectId": ProjectId,
            "GPWMGroupTypeId": $(this).val()
        };

        gpwmGroupType.push(_gpwmg);
    });

    var ProjectCommonDetail = {
        "ProjectId": ProjectId,
        "CompatNWPYesNoId": GetRadioValue('CompatNWPYesNoId'),
        "CompatibilityNWPApplicantCmt": GetTextValue('CompatibilityNWPApplicantCmt'),
        "CompatibilityNWPAuthorityCmt": GetTextValue('CompatibilityNWPAuthorityCmt'),
        //CompatibilityNWPDocLink //file
        "NWMPCompatYesNoId": GetRadioValue('NWMPCompatYesNoId'),
        "NWMPApplicantCmt": GetTextValue('NWMPApplicantCmt'),
        "NWMPAuthorityCmt": GetTextValue('NWMPAuthorityCmt'),
        //NWMPDocLink //file
        "FYPYesNoId": GetRadioValue('FYPYesNoId'),
        "FYPApplicantCmt": GetTextValue('FYPApplicantCmt'),
        "FYPAuthorityCmt": GetTextValue('FYPAuthorityCmt'),
        "SDGYesNoId": GetRadioValue('SDGYesNoId'),
        "SDGApplicantCmt": GetTextValue('SDGApplicantCmt'),
        "SDGAuthorityCmt": GetTextValue('SDGAuthorityCmt'),
        //SDGDocLink //file
        "DeltaPlanYesNoId": GetRadioValue('DeltaPlanYesNoId'),
        "DeltaPlan2100ApplicantCmt": GetTextValue('DeltaPlan2100ApplicantCmt'),
        "DeltaPlan2100AuthorityCmt": GetTextValue('DeltaPlan2100AuthorityCmt'),
        //DeltaPlan2100DocLink //file
        "CostalZoneYesNoId": GetRadioValue('CostalZoneYesNoId'),
        "CostalZoneApplicantCmt": GetTextValue('CostalZoneApplicantCmt'),
        "CostalZoneAuthorityCmt": GetTextValue('CostalZoneAuthorityCmt'),
        //CostalZoneDocLink //file.
        "AgriculturalYesNoId": GetRadioValue('AgriculturalYesNoId'),
        "AgriApplicantCmt": GetTextValue('AgriApplicantCmt'),
        "AgriAuthorityCmt": GetTextValue('AgriAuthorityCmt'),
        //AgriDocLink //file
        "FisheriesYesNoId": GetRadioValue('FisheriesYesNoId'),
        "FisheriesApplicantCmt": GetTextValue('FisheriesApplicantCmt'),
        "FisheriesAuthorityCmt": GetTextValue('FisheriesAuthorityCmt'),
        //FisheriesDocLink //file
        "IWRMYesNoId": GetRadioValue('IWRMYesNoId'),
        "IWRMApplicantCmt": GetTextValue('IWRMApplicantCmt'),
        "IWRMAuthorityCmt": GetTextValue('IWRMAuthorityCmt'),
        "GPWMYesNoId": GetRadioValue('GPWMYesNoId'),
        "GPWMApplicantCmt": GetTextValue('GPWMApplicantCmt'),
        "GPWMAuthorityCmt": GetTextValue('GPWMAuthorityCmt'),
        "FeasibilityYesNoId": GetRadioValue('FeasibilityYesNoId'),
        "FeasibilityApplicantCmt": GetTextValue('FeasibilityApplicantCmt'),
        "FeasibilityAuthorityCmt": GetTextValue('FeasibilityAuthorityCmt'),
        "SocialIssuesYesNoId": GetRadioValue('SocialIssuesYesNoId'),
        "SocialIssuesApplicantCmt": GetTextValue('SocialIssuesApplicantCmt'),
        "SocialIssuesAuthorityCmt": GetTextValue('SocialIssuesAuthorityCmt')
    };

    var Project31IndvDetail = {
        "Project31IndvId": Project31IndvId,
        "ProjectId": ProjectId,
        "DuplicatYesNoId": GetRadioValue('DuplicatYesNoId'),
        "DuplicationApplicantComments": GetTextValue('DuplicationApplicantComments'),
        "DuplicationAuthorityComments": GetTextValue('DuplicationAuthorityComments')
    };

    var _finalDeedObj = {
        "CommonDetail": ProjectCommonDetail,
        "Project31Indv": Project31IndvDetail,
        "CompatNWPDetail": nwpArticles,
        "CompatNWMPDetail": prjCompatNWMP,
        "CompatSDGDetail": sdgGoals,
        "CompatSDGIndiDetail": sdgIndicators,
        "BDP2100GoalDetail": dp2100Goals,
        "GPWMGroupTypeDetail": gpwmGroupType
    }

    console.log(_finalDeedObj); //jQuery('#ProjectId').val()

    var url = '@Url.Action("f31dootos", "form")';
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { _form31DeedObli: _finalDeedObj },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.status == 'success') {
                Notification('success', 'Success', data.message);
            } else {
                Notification('error', 'Saving Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            Notification('error', 'Error Occured', errorMsg);
        }
    });
});

jQuery("#btnClearDeed").click(function () {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        type: "warning",
        allowEscapeKey: false,
        allowOutsideClick: false,
        showCancelButton: !0,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sure"
    }).then(function (t) {
        t.value && Swal.fire("Cleared!", "Your entry data has been cleared successfully.", "success")
    });
});
//end

//start :: Administrative JS
jQuery("#btnSaveAdministrative").click(function () {
    var _recDetail = {
        "ProjectId": GetHiddenValue('ProjectId'),
        "RecommendationId": jQuery('#RecommendationId option:selected').val(),
        "RecommandCmt": GetTextValue('RecommandCmt')
    };

    console.log(_recDetail);

    var url = '@Url.Action("f31rds", "form")';
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { _recDetail: _recDetail },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.status == 'success') {
                Notification('success', 'Success', data.message);
            } else {
                Notification('error', 'Saving Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            Notification('error', 'Error Occured', errorMsg);
        }
    });
});

jQuery("#btnClearAdministrative").click(function () {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        type: "warning",
        allowEscapeKey: false,
        allowOutsideClick: false,
        showCancelButton: !0,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sure"
    }).then(function (t) {
        t.value && Swal.fire("Cleared!", "Your entry data has been cleared successfully.", "success")
    });
});
//end

// Common Features
function GetTextValue(control) {
    return jQuery('#' + control).val();
}

function SetTextValue(control, val) {
    return jQuery('#' + control).val(val);
}

function GetHiddenValue(control) {
    return jQuery('#' + control).val();
}

function SetHiddenValue(control, val) {
    return jQuery('#' + control).val(val);
}

function SetDropdownValue(control, val) {
    return jQuery('#' + control).val(val);
}

function GetRadioValue(control) {
    var value = jQuery("input[type='radio'][name='" + control + "']").val();
    return (value == undefined || value == '') ? 0 : value;
}

function GeneralInfoSave() {
    //region block 1
    var _gi = [];

    var CommonDetail = {
        "ProjectId": jQuery('#ProjectId').val(),
        "UserId": 0,
        "AppSubmissionId": 0,
        "ProjectTypeId": jQuery('#ProjectTypeId').val(),
        "ProjectName": jQuery('#ProjectName').val(),
        "BackgroundAndRationale": jQuery('#BackgroundAndRationale').val(),
        "ProjectTarget": jQuery('#ProjectTarget').val(),
        "ProjectObjective": jQuery('#ProjectObjective').val(),
        "ProjectActivity": jQuery('#ProjectActivity').val(),
        "ProjectStartDate": jQuery('#ProjectStartDate').val(),
        "ProjectCompletionDate": jQuery('#ProjectCompletionDate').val(),
        "ProjectEstimatedCost": jQuery('#ProjectEstimatedCost').val()
    };

    var LocationDetail = {
        "LocationId": 0,
        "ProjectId": GetHiddenValue('ProjectId'),
        "DistrictGeoCode": jQuery('#DistrictGeoCode').val(),
        "UpazilaGeoCode": jQuery('#UpazilaGeoCode').val(),
        "UnionGeoCode": jQuery('#UnionGeoCode').val()
        //"Latitude": $('#HydrologicalInformation_Other').val()
        //"Longitude": $('#HydrologicalInformation_Other').val()
    };

    var _gi = {
        "CommonDetail": CommonDetail,
        "LocationDetail": LocationDetail
    }

    console.log(_gi);

    var url = '@Url.Action("gis", "form")';
    jQuery.ajax({
        type: "POST",
        url: url,
        data: { _gi: _gi },
        dataType: "json",
        success: function (data, status, jqXHR) {
            if (data.status == 'success') {
                SetHiddenValue('ProjectId', data.id);
                Notification('success', 'Success', data.message);
            } else {
                Notification('error', 'Saving Error', data.message);
            }
        }, error: function (jqXHR, exception) {
            var errorMsg = AjaxErrorHandle(jqXHR, exception);
            Notification('error', 'Error Occured', errorMsg);
        }
    });
    //endregion block 1
}

function BindData() {
    var projectId = '@ViewBag.ProjectCommonDetail.ProjectId';

    if (projectId != '') {
        console.log('ViewBag.ProjectCommonDetail.ProjectId: ' + projectId);

        //General Info
        SetHiddenValue('ProjectId', projectId);
        SetHiddenValue('Project31IndvId', '@ViewBag.ProjectIndvDetail31.Project31IndvId');
        SetHiddenValue('UserId', '@ViewBag.ProjectCommonDetail.UserId');
        SetHiddenValue('ProjectTypeId', '@ViewBag.ProjectCommonDetail.ProjectTypeId');
        SetTextValue('ProjectName', '@ViewBag.ProjectCommonDetail.ProjectName');
        SetTextValue('BackgroundAndRationale', '@ViewBag.ProjectCommonDetail.BackgroundAndRationale');
        SetTextValue('ProjectTarget', '@ViewBag.ProjectCommonDetail.ProjectTarget');
        SetTextValue('ProjectObjective', '@ViewBag.ProjectCommonDetail.ProjectObjective');
        SetTextValue('ProjectActivity', '@ViewBag.ProjectCommonDetail.ProjectActivity');
        SetTextValue('ProjectStartDate', '@ViewBag.ProjectCommonDetail.ProjectStartDate');
        SetTextValue('ProjectCompletionDate', '@ViewBag.ProjectCommonDetail.ProjectCompletionDate');
        SetTextValue('ProjectEstimatedCost', '@ViewBag.ProjectCommonDetail.ProjectEstimatedCost');

        //location
        SetDropdownValue('DistrictGeoCode', '@ViewBag.ProjectLocationDetail.DistrictGeoCode');
        SetDropdownValue('UpazilaGeoCode', '@ViewBag.ProjectLocationDetail.UpazilaGeoCode');
        SetDropdownValue('UnionGeoCode', '@ViewBag.ProjectLocationDetail.UnionGeoCode');
        //SetDropdownValue('Latitude', 'ViewBag.ProjectLocationDetail.Latitude');
        //SetDropdownValue('Longitude', 'ViewBag.ProjectLocationDetail.Longitude');
    }
}
