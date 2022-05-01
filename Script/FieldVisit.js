//Front Page
$(function () {
    $("#btnUpdate").hide();
    $("#btnSave").show();

    $('#txtVisitDate').dateDropper({ format: "d-M-Y",dropWidth: 200, init_animation: "bounce", dropPrimaryColor: "#fa4420", dropBorder: "1px solid #fa4420", dropBorderRadius: "20", dropShadow: "0 0 10px 0 rgba(250, 68, 32, 0.6)" });
    
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

    $("#ddlYear").change(function () {
        ShowData();
    });

    //Load product or fruit
    $(".Component").change(function () {
        if ($(this).val() == "-1") {
            $("#ddlSubComponent").attr("disabled", "disabled");
        }
        else {
            $("#ddlSubComponent").attr("disabled", false);
            $.ajax({
                type: "POST",
                url: "FieldVisit.aspx/GetSubComponent",
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
                url: "FieldVisit.aspx/GetDistrict",
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
    $('#hfID').val(ID);
    var obj = JSON.stringify({ ID: ID });
    $.ajax({
        type: "POST",
        url: "FieldVisit.aspx/GetFormByID",
        data: obj,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (r) {
            $('#ddlYear').val(r.d.Year).trigger('change');
            $('#txtVisitDate').val(r.d.VisitDate);
            $('.Component').val(r.d.Component).trigger('change');
            $('#ddlSubComponent').val(r.d.SubComponent).trigger('change');
            $('#txtPurpose').val(r.d.VisitPurpose);
            $('.Province').val(r.d.ProvinceID).trigger('change');
            $('#ddlDistrict').val(r.d.DistrictID).trigger('change');
            $('#txtVisitConductedBy').val(r.d.VisitConductedBy);
            $('#txtRemarks').val(r.d.Remarks);

            $('#btnSave').hide();
            $('#btnUpdate').show();

            toastr.info("Please enter correct information !", "FV-MIS:  Alert !", { progressBar: !0 });
        },
        error: function (data) {
            alert("error found");
        }

    });
}
function ShowData() {
    $.ajax({
        type: "POST",
        url: "FieldVisit.aspx/GetFormDetail",
        data: '{YearID: ' + $('#ddlYear').val() + '}',
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $table = "<table id='tbl'  width='100%' class='table table-xs nowrap table-striped scroll-horizontal table-hover'>";
            var header = "<thead><tr ><th>SN</th><th style='text-align:center;'>Visit Date</th><th style='text-align:center;'>Purpose</th><th style='text-align:center;'>Component </th><th style='text-align:center;'>Province</th><th style='text-align:center;'>District</th><th style='text-align:center;'>Conducted By</th><th style='text-align:center;'></th></tr></thead>";
            $table += header;
            var count = 1;
            $.each(data.d, function (i, x) {
                
                var row = "<tr ><td style='text-align:center;'>" + count + "</td>" +
                    "<td style='text-align:center;'><span>" + x.VisitDate + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.VisitPurpose + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.ComponentName + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.ProvinceName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.DistrictName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.VisitConductedBy + "</span></td>" +
                   (x.Edit == true ? "<td><a onclick='EditDetail(this)' class='btn-sm btn-blue edit' href='#'>Edit</a><span style='display:none'>" + x.FieldVisitID + "</span></td>" : "<td></td>") +

                "</tr>";
                $table += row;
                count++;
            });
            var footer = "<tfoot> <tr><td></td> <th>Date</th><th>Purpose</th> <th>Component</th><th>Province</th><th>District</th><th>Conducted By</th><td></td></tr></tfoot>";
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
    a.Year = $('#ddlYear').val();
    a.VisitDate = $('#txtVisitDate').val();
    a.Component = $('.Component').val();
    a.SubComponent = $('#ddlSubComponent').val();
    a.VisitPurpose = $('#txtPurpose').val();
    a.ProvinceID = $('.Province').val();
    a.DistrictID = $('#ddlDistrict').val();
    a.VisitConductedBy = $('#txtVisitConductedBy').val();
    a.Remarks = $('#txtRemarks').val();

    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "FieldVisit.aspx/Save",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("The field visit information is Successfully saved !", "FV-MIS: Field Visit !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The field visit information is saved successfully.<br/> THANK YOU..!",
                    type: "success",
                    html: true
                });
                //Clear components
                ClearForm();
                ShowData();
            }  else {

                toastr.warning("Please read the error message..!", "FV-MIS: Error Alert !", { progressBar: !0 });
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
    a.FieldVisitID = $('#hfID').val();
    a.Year = $('#ddlYear').val();
    a.VisitDate = $('#txtVisitDate').val();
    a.Component = $('.Component').val();
    a.SubComponent = $('#ddlSubComponent').val();
    a.VisitPurpose = $('#txtPurpose').val();
    a.ProvinceID = $('.Province').val();
    a.DistrictID = $('#ddlDistrict').val();
    a.VisitConductedBy = $('#txtVisitConductedBy').val();
    a.Remarks = $('#txtRemarks').val();


    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "FieldVisit.aspx/Update",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("The field visit information is Successfully updated !", "FV-MIS: Training !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The field visit information is updated successfully.<br/> THANK YOU..!",
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
    $('#hfID').val('');
    //$('#ddlYear').val('-1').trigger('change');
    $('#txtVisitDate').val('');
    $('.Component').val('-1').trigger('change');
    $('#ddlSubComponent').val('-1').trigger('change');
    $('#txtPurpose').val('');
    $('.Province').val('-1').trigger('change');
    $('#ddlDistrict').val('-1').trigger('change');
    $('#txtVisitConductedBy').val('');
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

    if ($('#txtVisitDate').val() == '') {
        $('#txtVisitDate').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtVisitDate').css('background-color', '#ffff'); }

    if ($('.Component').val() == '-1') {
        $('.Component').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Component').css('background-color', '#ffff'); }

    if ($('#txtPurpose').val() == '') {
        $('#txtPurpose').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtPurpose').css('background-color', '#ffff'); }

    if ($('.Province').val() == '-1') {
        $('.Province').parent().css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Province').parent().css('background-color', '#ffff'); }

    if ($('#txtVisitConductedBy').val() == '') {
        $('#txtVisitConductedBy').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtVisitConductedBy').css('background-color', '#ffff'); }

    if (flag) { return true } else {
        toastr.warning("Please enter valid informations in red background OR You missed items !", "TRA-MIS: Validation Alert !", { progressBar: !0 });
        return false;
    }
}
function FillProvince() {
    $.ajax({
        type: "POST",
        url: "FieldVisit.aspx/GetProvince",
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
        url: "FieldVisit.aspx/GetComponent",
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
        url: "FieldVisit.aspx/GetYear",
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