//Front Page
$(function () {
    $("#btnUpdate").hide();
    $("#btnSave").show();

    
    FillYear();
    FillComponent();
    FillProvince();
    FillOrganization();

    $('.Province,#ddlDistrict,.Training').select2({ searchInputPlaceholder: 'Search...' });

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

    //Load Training
    $("#ddlYear").change(function () {
        $(".Component").trigger('change');
    });
    $(".Component").change(function () {
        if ($(this).val() == "-1") {
            $(".Training").attr("disabled", "disabled");
        }
        else {
            $(".Training").attr("disabled", false);
            $.ajax({
                type: "POST",
                url: "TrainingParticipants.aspx/GetTraining",
                data: '{YearID: ' + $('#ddlYear').val() + ',ComponentID: ' + $('.Component').val() + '}',
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('.Training').empty();
                    $.each(data.d, function (key, value) {
                        $('.Training').append($("<option></option>").val(this['Value']).html(this['Text']));

                    });
                    $('.Training').prepend("<option value='-1' selected='true'>-Select-</option>");
                    $('.Training option:first').attr("selected", "selected").trigger('change');

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
                url: "TrainingParticipants.aspx/GetDistrict",
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
    $(".Training").change(function () {
        ShowData();
    });


});
function EditDetail(obj) {
    var td = obj.parentNode;
    row = obj.parentNode.parentNode;
    var span = $(td).find('span');
    var ID = span.html();
    $('#hfParticipantID').val(ID);
    var obj = JSON.stringify({ ParticipantID: ID });
    $.ajax({
        type: "POST",
        url: "TrainingParticipants.aspx/GetFormByID",
        data: obj,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (r) {
            //$('#ddlYear').val(r.d.Year).trigger('change');
            //$('.Component').val(r.d.Component).trigger('change');
            //$('.Training').val(r.d.TrainingID).trigger('change');
            $('#txtName').val(r.d.Name);
            $('#txtFatherName').val(r.d.FatherName);
            $('#ddlGender').val(r.d.Gender).trigger('change');
            $('.Province').val(r.d.ProvinceID).trigger('change');
            $('#ddlDistrict').val(r.d.DistrictID).trigger('change');
            $('#ddlOrganization').val(r.d.OrganizationID).trigger('change');
            $('#txtPosition').val(r.d.Position);
            $('#txtMobile').val(r.d.MobileNo);
            $('#txtEmail').val(r.d.Email);
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
        url: "TrainingParticipants.aspx/GetFormDetail",
        data: '{TrainingID: ' + $('.Training').val() + '}',
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $table = "<table id='tbl'  width='100%' class='table table-xs nowrap table-striped scroll-horizontal table-hover'>";
            var header = "<thead><tr ><th>SN</th><th style='text-align:center;'>Name</th><th style='text-align:center;'>Father Name</th><th style='text-align:center;'>Gender</th><th style='text-align:center;'>Duty Station</th><th style='text-align:center;'>Organization</th><th style='text-align:center;'>Position</th><th style='text-align:center;'>Mobile No</th><th style='text-align:center;'>Email</th><th style='text-align:center;'></th></tr></thead>";
            $table += header;
            var count = 1;
            $.each(data.d, function (i, x) {
                
                var row = "<tr ><td style='text-align:center;'>" + count + "</td>" +
                   "<td style='text-align:center;'><span>" + x.Name + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.FatherName+ "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.Gender + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.ProvinceName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.OrganizationName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.Position + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.MobileNo + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.Email + "</span></td>" +
                   (x.Edit == true ? "<td><a onclick='EditDetail(this)' class='btn-sm btn-blue edit' href='#'>Edit</a><span style='display:none'>" + x.ParticipantID + "</span></td>" : "<td></td>") +
                "</tr>";
                $table += row;
                count++;
            });
            var footer = "<tfoot> <tr><td></td> <th>Name</th> <th>Father Name</th><th>Gender</th><th>Duty Station</th><th>Organization</th><th>Position</th><td></td><td></td><td></td></tr></tfoot>";
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
    a.TrainingID = $('.Training').val();
    a.Name = $('#txtName').val();
    a.FatherName = $('#txtFatherName').val();
    a.Gender = $('#ddlGender').val();
    a.ProvinceID = $('.Province').val();
    a.DistrictID = $('#ddlDistrict').val();
    a.OrganizationID = $('#ddlOrganization').val();
    a.Position = $('#txtPosition').val();
    a.MobileNo = $('#txtMobile').val();
    a.Email = $('#txtEmail').val();
    a.Remarks = $('#txtRemarks').val();

    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "TrainingParticipants.aspx/Save",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("The participant information is Successfully saved !", "TRA-MIS: Training !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The participant information is saved successfully.<br/> THANK YOU..!",
                    type: "success",
                    html: true
                });
                //Clear components
                ClearForm();
                ShowData();
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
    a.ParticipantID = $('#hfParticipantID').val();
    a.TrainingID = $('.Training').val();
    a.Name = $('#txtName').val();
    a.FatherName = $('#txtFatherName').val();
    a.Gender = $('#ddlGender').val();
    a.ProvinceID = $('.Province').val();
    a.DistrictID = $('#ddlDistrict').val();
    a.OrganizationID = $('#ddlOrganization').val();
    a.Position = $('#txtPosition').val();
    a.MobileNo = $('#txtMobile').val();
    a.Email = $('#txtEmail').val();
    a.Remarks = $('#txtRemarks').val();

    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "TrainingParticipants.aspx/Update",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("The participant information is Successfully updated !", "TRA-MIS: Training !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The training information is updated successfully.<br/> THANK YOU..!",
                    type: "success",
                    html: true
                });
                //Clear components
                ClearForm();
                ShowData();
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
    $('#txtName').val('');
    $('#txtFatherName').val('');
    $('#ddlGender').val('M').trigger('change');
    $('.Province').val('-1').trigger('change');
    $('#ddlDistrict').val('-1').trigger('change');
    $('#ddlOrganization').val('-1').trigger('change');
    $('#txtPosition').val('');
    $('#txtMobile').val('');
    $('#txtEmail').val('');
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

    if ($('.Training').val() == '-1') {
        $('.Training').parent().css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Training').parent().css('background-color', '#ffff'); }

    if ($('#txtName').val() == '') {
        $('#txtName').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtName').css('background-color', '#ffff'); }

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

    if ($('#ddlOrganization').val() == '-1') {
        $('#ddlOrganization').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#ddlOrganization').css('background-color', '#ffff'); }

    if ($('#txtPosition').val() == '') {
        $('#txtPosition').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtPosition').css('background-color', '#ffff'); }


    if (!validateEmail($('#txtEmail').val())) {
        $('#txtEmail').css('background-color', '#FFAAAA');
        flag = false;
    }
    else {
        $('#txtEmail').css('background-color', '#ffff');
    }

    if (flag) { return true } else {
        toastr.warning("Please enter valid informations in red background!", "TRA-MIS: Validation Alert !", { progressBar: !0 });
        return false;
    }
}
function FillProvince() {
    $.ajax({
        type: "POST",
        url: "TrainingParticipants.aspx/GetProvince",
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
        url: "TrainingParticipants.aspx/GetComponent",
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
        url: "TrainingParticipants.aspx/GetYear",
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
function FillOrganization() {
    $.ajax({
        type: "POST",
        url: "TrainingParticipants.aspx/GetOrganization",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#ddlOrganization').empty();
            var count = 1;
            $.each(data.d, function () {
                $('#ddlOrganization').append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            $('#ddlOrganization').prepend("<option value='-1' selected='true'>-Select-</option>");
            $('#ddlOrganization option:first').attr("selected", "selected").trigger('change');
        },
        error: function (data) {
            alert("error found");
        }

    });

}