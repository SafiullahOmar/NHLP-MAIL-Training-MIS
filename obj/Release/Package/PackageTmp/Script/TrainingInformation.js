//Front Page
$(function () {
    $("#btnUpdate").hide();
    $("#btnSave").show();

    $('#txtStartDate').dateDropper({ dropWidth: 200, init_animation: "bounce", dropPrimaryColor: "#fa4420", dropBorder: "1px solid #fa4420", dropBorderRadius: "20", dropShadow: "0 0 10px 0 rgba(250, 68, 32, 0.6)" });
    $('#txtEndDate').dateDropper({ dropWidth: 200, init_animation: "bounce", dropPrimaryColor: "#fa4420", dropBorder: "1px solid #fa4420", dropBorderRadius: "20", dropShadow: "0 0 10px 0 rgba(250, 68, 32, 0.6)" });

    FillYear();
    FillComponent();
    FillProvince();
    
    $('.Province,#ddlDistrict').select2({ searchInputPlaceholder: 'Search...' });

    //btn Save
    $("#btnSave").click(function (e) {
        e.preventDefault();
        if (formValidation(e)) {
            Save();
        }
    });
    //btn update
    $("#btnUpdate").click(function (e) {
        e.preventDefault();
        if (formValidation(e)) {
            Update();
        }
    });

    //Load product or fruit
    $(".Component").change(function () {
        if ($(this).val() == "-1") {
            $("#ddlSubComponent").attr("disabled", "disabled");
            ShowData();
        }
        else {
            $("#ddlSubComponent").attr("disabled", false);
            ShowData();
            $.ajax({
                type: "POST",
                url: "TrainingInformation.aspx/GetSubComponent",
                data: '{ComponentID: ' + $('.Component').val() + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    $('#ddlSubComponent').empty();
                    $.each(data.d, function (key, value) {
                        $('#ddlSubComponent').append($("<option></option>").val(this['Value']).html(this['Text']));

                    });
                    $('#ddlSubComponent').prepend("<option value='-1' selected='true'>-Select-</option>");
                    $('#ddlSubComponent option:first').attr("selected", "selected").trigger('change');

                },
                error: function (data) {
                    alert("error found");
                }

            });


        }
    });
    //Load District
    $(".Province").change(function () {
        if ($(this).val() == "-1") {
            $("#ddlDistrict").attr("disabled", "disabled");
        }
        else {
            $("#ddlDistrict").attr("disabled", false);
            $.ajax({
                type: "POST",
                url: "TrainingInformation.aspx/GetDistrict",
                data: '{ProvinceID: ' + $('.Province').val() + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    $('#ddlDistrict').empty();
                    $.each(data.d, function (key, value) {
                        $('#ddlDistrict').append($("<option></option>").val(this['Value']).html(this['Text']));

                    });
                    $('#ddlDistrict').prepend("<option value='-1' selected='true'>-Select-</option>");
                    $('#ddlDistrict option:first').attr("selected", "selected").trigger('change');

                },
                error: function (data) {
                    alert("error found");
                }

            });


        }
    });

    
});
function EditDetail(obj) {
    var td = obj.parentNode;
    row = obj.parentNode.parentNode;
    var span = $(td).find('span');
    var ID = span.html();
    $('#hfTrainingID').val(ID);
    var obj = JSON.stringify({ TrainingID: ID });
    $.ajax({
        type: "POST",
        url: "TrainingInformation.aspx/GetFormByID",
        data: obj,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (r) {
            $('#ddlYear').val(r.d.Year).trigger('change');
            $('.Component').val(r.d.Component).trigger('change');
            $('#ddlSubComponent').val(r.d.SubComponent).trigger('change');
            $('#txtTrainingTitle').val(r.d.TrainingTitle);
            $('.Province').val(r.d.ProvinceID).trigger('change');
            $('#ddlDistrict').val(r.d.DistrictID).trigger('change');
            $('#txtTrainingLocation').val(r.d.TrainingLocation);
            $('#txtStartDate').val(ConvertDate(r.d.StartDate));
            $('#txtEndDate').val(ConvertDate(r.d.EndDate));
            $('#txtTrainer').val(r.d.Trainer);
            $('#txtRemarks').val(r.d.Remarks);
            
            $('#btnSave').hide();
            $('#btnUpdate').show();
            
            toastr.info("Please enter correct information !", "TRA-MIS:  Alert !", { progressBar: !0 });
        },
        error: function (data) {
            alert("error found");
        }

    });
}
function ShowData() {
    $.ajax({
        type: "POST",
        url: "TrainingInformation.aspx/GetFormDetail",
        data: '{YearID: ' + $('#ddlYear').val() + ',ComponentID: ' + $('.Component').val() + '}',
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $table = "<table id='tbl'  width='100%' class='table table-xs nowrap table-striped scroll-horizontal table-hover'>";
            var header = "<thead><tr ><th>SN</th><th style='text-align:center;'>Training Title</th><th style='text-align:center;'>Component </th><th style='text-align:center;'>Province</th><th style='text-align:center;'>District</th><th style='text-align:center;'>Location</th><th style='text-align:center;'>Start Date</th><th style='text-align:center;'>End Date</th><th style='text-align:center;'>Conducted By</th><th style='text-align:center;'></th><th style='text-align:center;'></th></tr></thead>";
            $table += header;
            var count = 1;
            $.each(data.d, function (i, x) {
                var d1 = ConvertDate(x.StartDate);
                var d2 = ConvertDate(x.EndDate);
                var row = "<tr ><td style='text-align:center;'>" + count + "</td>" +
                   "<td style='text-align:center;'><span>" + x.TrainingTitle + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.ComponentName + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.ProvinceName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.DistrictName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.TrainingLocation + "</span></td>" +
                   "<td style='text-align:center;'><span>" + d1 + "</span></td>" +
                   "<td style='text-align:center;'><span>" + d2 + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.Trainer + "</span></td>" +
                   (x.Uploaded != 'No' ? "<td><a class='btn-sm btn-success' href='/HTTPHandler/DownloadAll.ashx?ID=" + x.TrainingID + "'>Download</a></td>" : "<td>Not Uploaded</td>") +
                   (x.Edit == true ? "<td><a onclick='EditDetail(this)' class='btn-sm btn-blue edit' href='#'>Edit</a><span style='display:none'>" + x.TrainingID + "</span></td>" : "<td></td>") +
                "</tr>";
                $table += row;
                count++;
            });
            var footer = "<tfoot> <tr><td></td> <th>Title</th> <th>Component</th><th>Province</th><th>District</th><th>Location</th><th>Start</th><th>End</th><td></td><td></td><td></td></tr></tfoot>";
            $table += footer;
            $('#dtTableDetail').html($table);
            $table += "</table>";
            $('#tbl tfoot th').each(function () {
                var title = $(this).text();
                $(this).html('<input type="text" style="font-size:11px; width: 100%;" class="form-control" placeholder="' + title + '" />');
            });
            var table = $('#tbl').DataTable({ "sScrollX": "100%", "responsive": true, "bPaginate": true, "bLengthChange": true, "bFilter": true, "bSort": true, "bInfo": true, "bAutoWidth": true, });
            table.columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change', function () {
                    if (that.search() !== this.value) {
                        that
                    .search(this.value)
                    .draw();
                    }
                });
            });
            $('#dtTableDetail').show();
        },
        error: function (data) {
            alert("error found");
        }

    });
}
function Save() {

        var a = {};
        a.TrainingTitle = $('#txtTrainingTitle').val();
        a.Year = $('#ddlYear').val();
        a.Component = $('.Component').val();
        a.SubComponent = $('#ddlSubComponent').val();
        a.ProvinceID = $('.Province').val();
        a.DistrictID = $('#ddlDistrict').val();
        a.TrainingLocation = $('#txtTrainingLocation').val();
        a.StartDate = $('#txtStartDate').val();
        a.EndDate = $('#txtEndDate').val();
        a.Trainer = $('#txtTrainer').val();
        a.Remarks = $('#txtRemarks').val();

        var jsonObject = JSON.stringify({ obj: a });

        $.ajax({
            type: "post",
            url: "TrainingInformation.aspx/Save",
            data: jsonObject,
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.d == '0') {
                    toastr.success("The training information is Successfully saved !", "TRA-MIS: Training !", { progressBar: !0 });
                    swal({
                        title: "Success Alert!",
                        text: "The training information is saved successfully.<br/> THANK YOU..!",
                        type: "success",
                        html: true
                    });
                    //Clear components
                    ClearForm();
                    ShowData();
                } else if (data.d == '1') {
                    toastr.warning("Please read the message..!", "TRA-MIS: Error Alert !", { progressBar: !0 });
                    swal({
                        title: "STOP!",
                        text: "The training having the following details has alreday exist.<br/>Please review the information.",
                        type: "error",
                        html: true
                    });
                } else {

                    toastr.warning("Please read the error message..!", "TRA-MIS: Error Alert !", { progressBar: !0 });
                    swal({
                        title: "Error Alert!",
                        text: "Error message <span style='font-weight:700; color:Red;'>( " + data.d + " )</span> <br>Please contact the MIS component.",
                        type: "error",
                        html: true
                    });
                }
            },
            error: function () {
                alert("error found");
            }

        });
}
function Update() {

    var a = {};
    a.TrainingID = $('#hfTrainingID').val();
    a.TrainingTitle = $('#txtTrainingTitle').val();
    a.Year = $('#ddlYear').val();
    a.Component = $('.Component').val();
    a.SubComponent = $('#ddlSubComponent').val();
    a.ProvinceID = $('.Province').val();
    a.DistrictID = $('#ddlDistrict').val();
    a.TrainingLocation = $('#txtTrainingLocation').val();
    a.StartDate = $('#txtStartDate').val();
    a.EndDate = $('#txtEndDate').val();
    a.Trainer = $('#txtTrainer').val();
    a.Remarks = $('#txtRemarks').val();


    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "TrainingInformation.aspx/Update",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("The training information is Successfully updated !", "TRA-MIS: Training !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The training information is updated successfully.<br/> THANK YOU..!",
                    type: "success",
                    html: true
                });
                //Clear components
                ClearForm();
                ShowData();
            } else if (data.d == '1') {
                toastr.warning("Please read the message..!", "TRA-MIS: Error Alert !", { progressBar: !0 });
                swal({
                    title: "STOP!",
                    text: "The training having the following details has alreday exist.<br/>Please review the information.",
                    type: "error",
                    html: true
                });
            } else {

                toastr.warning("Please read the error message..!", "TRA-MIS: Error Alert !", { progressBar: !0 });
                swal({
                    title: "Error Alert!",
                    text: "Error message <span style='font-weight:700; color:Red;'>( " + data.d + " )</span> <br>Please contact the MIS component.",
                    type: "error",
                    html: true
                });
            }
        },
        error: function () {
            alert("error found");
        }

    });

}
function ClearForm() {
    $('#hfTrainingID').val('');
    //$('#ddlYear').val('-1').trigger('change');
    //$('.Component').val('-1').trigger('change');
    $('#ddlSubComponent').val('-1').trigger('change');
    $('#txtTrainingTitle').val('');
    $('.Province').val('-1').trigger('change');
    $('#ddlDistrict').val('-1').trigger('change');
    $('#txtTrainingLocation').val('');
    $('#txtStartDate').val('');
    $('#txtEndDate').val('');
    $('#txtTrainer').val('');
    $('#txtRemarks').val('');

    $('#btnSave').show();
    $('#btnUpdate').hide();
}
function formValidation(e) {

    var flag = true;

    if ($('#ddlYear').val() == '-1') {
        $('#ddlYear').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#ddlYear').css('background-color', '#ffff'); }

    if ($('.Component').val() == '-1') {
        $('.Component').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Component').css('background-color', '#ffff'); }

    if ($('#txtTrainingTitle').val() == '') {
        $('#txtTrainingTitle').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtTrainingTitle').css('background-color', '#ffff'); }

    if ($('.Province').val() == '-1') {
        $('.Province').parent().css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Province').parent().css('background-color', '#ffff'); }

    if ($('#ddlDistrict').val() == '-1') {
        $('#ddlDistrict').parent().css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#ddlDistrict').parent().css('background-color', '#ffff'); }

    if ($('#txtTrainingLocation').val() == '') {
        $('#txtTrainingLocation').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtTrainingLocation').css('background-color', '#ffff'); }
    
    if ($('#txtStartDate').val() == '') {
        $('#txtStartDate').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtStartDate').css('background-color', '#ffff'); }

    if ($('#txtEndDate').val() == '') {
        $('#txtEndDate').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtEndDate').css('background-color', '#ffff'); }

    if ($('#txtTrainer').val() == '') {
        $('#txtTrainer').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtTrainer').css('background-color', '#ffff'); }

    if (flag) { return true } else {
        toastr.warning("Please enter valid informations in red background OR You missed items !", "MAR-MIS: Validation Alert !", { progressBar: !0 });
        return false;
    }
}
function FillProvince() {
    $.ajax({
        type: "POST",
        url: "TrainingInformation.aspx/GetProvince",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('.Province').empty();
            var count = 1;
            $.each(data.d, function () {
                $('.Province').append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            $('.Province').prepend("<option value='-1' selected='true'>-Select-</option>");
            $('.Province option:first').attr("selected", "selected").trigger('change');
        },
        error: function (data) {
            alert("error found");
        }

    });

}
function FillComponent() {
    $.ajax({
        type: "POST",
        url: "TrainingInformation.aspx/GetComponent",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            $('.Component').empty();
            var count = 1;
            $.each(data.d, function () {
                $('.Component').append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            $('.Component').prepend("<option value='-1' selected='true'>-Select-</option>");
            $('.Component option:first').attr("selected", "selected").trigger('change');
        },
        error: function (data) {
            alert("error found");
        }

    });

}
function FillYear() {
    $.ajax({
        type: "POST",
        url: "TrainingInformation.aspx/GetYear",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#ddlYear').empty();
            var count = 1;
            $.each(data.d, function () {
                $('#ddlYear').append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            $('#ddlYear').prepend("<option value='-1' selected='true'>-Select-</option>");
            $('#ddlYear option:first').attr("selected", "selected").trigger('change');
        },
        error: function (data) {
            alert("error found");
        }

    });

}