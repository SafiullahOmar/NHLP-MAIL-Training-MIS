//Front Page
$(function () {
    $("#btnSave").show();
    $('#btnUpdate').hide();

    FillYear();
    FillComponent();
    
    $('.Component,.Training').select2({ searchInputPlaceholder: 'Search...' });

    //btn Save
    $("#btnSave").click(function (e) {
        e.preventDefault();
        if (formValidation(e)) {
            Save();
        }
    });

    //btn Update
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
                url: "TrainingAttendance.aspx/GetTraining",
                data: '{YearID: ' + $('#ddlYear').val() + ',ComponentID: ' + $('.Component').val() + '}',
                dataType: "json",
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
    //Load Uploaded files...
    $(".Training").change(function () {
        //Filling Days..!
        $.ajax({
            type: "POST",
            url: "TrainingAttendance.aspx/GetTrainingDays",
            data: '{TrainingID: ' + $(this).val() + '}',
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('.Day').empty();
                var count = 1;
                $.each(data.d, function () {
                    $('.Day').append($("<option></option>").val(this['Value']).html(this['Text']));
                });

                $('.Day').prepend("<option value='-1' selected='true'>-Select-</option>");
                $('.Day option:first').attr("selected", "selected").trigger('change');
            },
            error: function (data) {
                alert("error found");
            }

        });
        ShowParticipant();
        ShowData();
    });

    $('#tblData').DataTable({ "sScrollX": "100%", "responsive": true, "bPaginate": true, "bLengthChange": true, "bFilter": true, "bSort": true, "bInfo": true, "bAutoWidth": true, });

    $("#chkCheckUncheckAll").click(function () {
            if ($("#chkCheckUncheckAll").is(':checked')) {
                $(".checkboxclass").each(function () {
                    $(this).prop("checked", true);
                });
            } else {
                $(".checkboxclass").each(function () {
                    $(this).prop("checked", false);
                });
            }
    });
    
});
function EditDetail(obj) {
    var td = obj.parentNode;
    row = obj.parentNode.parentNode;
    var span = $(td).find('span');
    var ID = span.html();
    $('#hfAttMstID').val(ID);
    
    var obj = JSON.stringify({ MstID: ID, TrainingID: $('.Training').val() });
    $.ajax({
        type: "POST",
        url: "TrainingAttendance.aspx/GetParticipantListByMstID",
        data: obj,
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('#tblData').DataTable().destroy();
            $("#tblData tbody tr").remove();
            
            var count = 1;
            $.each(data.d, function (i, x) {
                $('.Day').val(x.TrainingDay).trigger('change');
                var row = "<tr ><td style='text-align:center;'>" + count + "<span style='display:none'>" + x.ParticipantID + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.Name + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.FatherName + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.Gender + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.ProvinceName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.OrganizationName + "</span></td>" +
                   (x.PresentStatus ==true ? "<td style='text-align:center;'><input id='chkPresent' name='Attendance' checked class='checkboxclass' type='checkbox' /></td>": "<td style='text-align:center;'><input id='chkPresent' name='Attendance' class='checkboxclass' type='checkbox' /></td>") +
                "</tr>";
                $("#tblData tbody").append(row);
                count++;
            });

            $('#tblData').DataTable({ "sScrollX": "100%", "responsive": true, "bPaginate": true, "bLengthChange": true, "bFilter": true, "bSort": true, "bInfo": true, "bAutoWidth": true, });

            $('.checkboxclass').click(function () {
                //alert('Heell');
                var totalCheckboxes = $(".checkboxclass:checkbox").size();
                var checkedCheckboxes = $(".checkboxclass:checkbox:checked").size();
                $("#chkCheckUncheckAll").prop('checked', totalCheckboxes == checkedCheckboxes);

            });
            $('#btnSave').hide();
            $('#btnUpdate').show();

            toastr.info("Please enter correct information !", "TRA-MIS:  Alert !", { progressBar: !0 });
        },
        error: function (data) {
            alert("error found");
        }

    });
}
function ShowParticipant() {
    $.ajax({
        type: "POST",
        url: "TrainingAttendance.aspx/GetParticipantList",
        data: '{TrainingID: ' + $('.Training').val() + '}',
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            
            $('#tblData').DataTable().destroy();
            $("#tblData tbody tr").remove();

            var count = 1;
            $.each(data.d, function (i, x) {

                var row = "<tr ><td style='text-align:center;'>" + count + "<span style='display:none'>" + x.ParticipantID + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.Name + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.FatherName + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.Gender + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.ProvinceName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.OrganizationName + "</span></td>" +
                   "<td style='text-align:center;'><input id='chkPresent' name='Attendance' class='checkboxclass' type='checkbox' /></td>" +
                "</tr>";

                $("#tblData tbody").append(row);
                count++;
            });

            $('#tblData').DataTable({ "sScrollX": "100%", "responsive": true, "bPaginate": true, "bLengthChange": true, "bFilter": true, "bSort": true, "bInfo": true, "bAutoWidth": true, });
            
            $('.checkboxclass').click(function () {
                //alert('Heell');
                var totalCheckboxes = $(".checkboxclass:checkbox").size();
                var checkedCheckboxes = $(".checkboxclass:checkbox:checked").size();
                $("#chkCheckUncheckAll").prop('checked', totalCheckboxes == checkedCheckboxes);
                
            });
        },
        error: function (data) {
            alert("error found");
        }

    });
}
function ShowData() {
    $.ajax({
        type: "POST",
        url: "TrainingAttendance.aspx/GetFormDetail",
        data: '{TrainingID: ' + $('.Training').val() + '}',
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $table = "<table id='tbl'  width='100%' class='table table-xs nowrap table-striped scroll-horizontal table-hover'>";
            var header = "<thead><tr ><th>SN</th><th style='text-align:center;'>Name of Training</th><th style='text-align:center;'>Year</th><th style='text-align:center;'>Component</th><th style='text-align:center;'>Province</th><th style='text-align:center;'>Day</th><th style='text-align:center;'></th></tr></thead>";
            $table += header;
            var count = 1;
            $.each(data.d, function (i, x) {
                var row = "<tr ><td style='text-align:center;'>" + count + "</td>" +
                   "<td style='text-align:center;'><span>" + x.TrainingTitle + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.YearName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.ComponentName + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.Province + "</span></td>" +
                   "<td style='text-align:center;'><span>Day - " + x.TrainingDay + "</span></td>" +
                   (x.Edit == true ? "<td><a onclick='EditDetail(this)' class='btn-sm btn-blue edit' href='#'>Edit</a><span style='display:none'>" + x.AttMstID + "</span></td>" : "<td></td>") +
                "</tr>";
                $table += row;
                count++;
            });
            $('#dtTableDetail').html($table);
            $table += "</table>";
            var table = $('#tbl').DataTable({ "sScrollX": "100%", "responsive": true, "bPaginate": true, "bLengthChange": true, "bFilter": true, "bSort": true, "bInfo": true, "bAutoWidth": true, });

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
    a.TrainingDay = $('.Day').val();
    
    var lst = new Array();
    $('#tblData tbody tr').each(function () {
        var detail = {};
        var tr = $(this).closest('tr');
        var ParticipantID = $(tr).find('span:eq(0)');
        var Attendance = $(tr).find('input[name="Attendance"]');

        detail.Participant = $(ParticipantID).html();
        if ($(Attendance).is(":checked")) {
            detail.PresentStatus = true;
        }
        else {
            detail.PresentStatus = false;
        }

        lst.push(detail);
    });
    a.lstDetail = lst;
    
    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "TrainingAttendance.aspx/Save",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("The attendance information is Successfully saved !", "TRA-MIS: Training !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The attendance information is saved successfully.<br/> THANK YOU..!",
                    type: "success",
                    html: true
                });
                //Clear components
                //ClearForm();
                ShowData();
            } else if (data.d == '1') {
                toastr.warning("Please read the message..!", "TRA-MIS: Error Alert !", { progressBar: !0 });
                swal({
                    title: "STOP!",
                    text: "The attendance for this day has alreday exist.<br/>Please review the information.",
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
    a.AttMstID = $('#hfAttMstID').val();
    a.TrainingID = $('.Training').val();
    a.TrainingDay = $('.Day').val();

    var lst = new Array();
    $('#tblData tbody tr').each(function () {
        var detail = {};
        var tr = $(this).closest('tr');
        var ParticipantID = $(tr).find('span:eq(0)');
        var Attendance = $(tr).find('input[name="Attendance"]');

        detail.Participant = $(ParticipantID).html();
        if ($(Attendance).is(":checked")) {
            detail.PresentStatus = true;
        }
        else {
            detail.PresentStatus = false;
        }

        lst.push(detail);
    });
    a.lstDetail = lst;

    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "TrainingAttendance.aspx/Update",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("The attendance information is Successfully updated !", "TRA-MIS: Training !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The attendance information is updated successfully.<br/> THANK YOU..!",
                    type: "success",
                    html: true
                });
                //Clear components
                //ClearForm();
                ShowData();
            } else if (data.d == '1') {
                toastr.warning("Please read the message..!", "TRA-MIS: Error Alert !", { progressBar: !0 });
                swal({
                    title: "STOP!",
                    text: "The attendance for this day has alreday exist.<br/>Please review the information.",
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
function formValidation(e) {

    var flag = true;

    if ($('#ddlYear').val() == '-1') {
        $('#ddlYear').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#ddlYear').css('background-color', '#ffff'); }

    if ($('.Component').val() == '-1') {
        $('.Component').parent().css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Component').parent().css('background-color', '#ffff'); }

    if ($('.Training').val() == '-1') {
        $('.Training').parent().css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Training').parent().css('background-color', '#ffff'); }

    if ($('.Day').val() == '-1') {
        $('.Day').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('.Day').css('background-color', '#ffff'); }

    if (flag) { return true } else {
        toastr.warning("Please enter valid informations in red background !", "TRA-MIS: Validation Alert !", { progressBar: !0 });
        return false;
    }
}
function FillComponent() {
    $.ajax({
        type: "POST",
        url: "TrainingAttendance.aspx/GetComponent",
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
        url: "TrainingAttendance.aspx/GetYear",
        data: "{}",
        dataType: "json",
        async: false,
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
