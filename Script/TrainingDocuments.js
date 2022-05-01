//Front Page
$(function () {
    $("#btnSave").show();

    FillYear();
    FillComponent();
    //FillProvince();

    $('.Component,.Training').select2({ searchInputPlaceholder: 'Search...' });

    //btn Save
    $("#btnSave").click(function (e) {
        e.preventDefault();
        if (formValidation(e)) {
            Save();
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
                url: "TrainingDocuments.aspx/GetTraining",
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
        ShowData();
    });


});
function ShowData() {
    $.ajax({
        type: "POST",
        url: "TrainingDocuments.aspx/GetFormDetail",
        data: '{TrainingID: ' + $('.Training').val() + '}',
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $table = "<table id='tbl'  width='100%' class='table table-xs nowrap table-striped scroll-horizontal table-hover'>";
            var header = "<thead><tr ><th>SN</th><th style='text-align:center;'>Remark/Comment</th><th style='text-align:center;'>Uploaded By</th><th style='text-align:center;'>Uploaded Date</th><th style='text-align:center;'></th><th style='text-align:center;'></th></tr></thead>";
            $table += header;
            var count = 1;
            $.each(data.d, function (i, x) {
                var row = "<tr ><td style='text-align:center;'>" + count + "</td>" +
                   "<td style='text-align:center;'><span>" + x.Remarks + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.InsertedBy + "</span></td>" +
                   "<td style='text-align:center;'><span>" + x.InsertedDate + "</span></td>" +
                   "<td><a class='btn-sm btn-success' href='/HTTPHandler/Download.ashx?file=" + x.DocPath + "'>Download</a></td>" +
                   (x.Delete == true ? "<td><a onclick='Delete(this)' class='btn-sm btn-danger delete' href='#'>Delete</a><span style='display:none'>" + x.ID + "</span></td>" : "<td></td>") +
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
function Delete(obj) {
    var td = obj.parentNode;

    var span0 = $(td).find('span:eq(0)');
    var ID = span0.html();

    swal({  
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel please!",
        closeOnConfirm: false,
        closeOnCancel: true
    },
    function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "post",
                url: "TrainingDocuments.aspx/DeleteDoc",
                data: '{DocID: ' + ID + '}',
                datatype: "json",
                contentType: "Application/json; charset=utf-8",
                success: function (data) {
                    if (data.d == '0') {
                        toastr.success("The file is Successfully deleted!", "TRA-MIS: Training !", { progressBar: !0 });
                        swal("Deleted!", "The file has been deleted.", "success");
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
                    alert('Something went wrong...!');
                }

            });
        }
    });

    
}
function Save() {
    if (window.FormData !== undefined) {
        var formDetails = new FormData();
        formDetails.append("action", "SAVE");
        formDetails.append("TrainingID", $('.Training').val());
        formDetails.append("Remarks", $('#txtRemarks').val());
        
        var fileUpload = $("#FileUpload1").get(0);
        var files = fileUpload.files;
        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                formDetails.append(files[i].name, files[i]);
            }
        }

        $.ajax({
            url: '../HTTPHandler/TrainingDocUpload.ashx',
            data: formDetails,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (data) {
                
                if (data == '0') {
                    toastr.success("The document(s) is Successfully uploaded !", "TRA-MIS: Training !", { progressBar: !0 });
                    swal({
                        title: "Uploaded!",
                        text: "The docuement(s) has been uploaded.<br/> THANK YOU..!",
                        type: "success",
                        html: true
                    });
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
            error: function (data) {
                alert("error found");
            }

        });
    }
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

    if (flag) { return true } else {
        toastr.warning("Please enter valid informations in red background OR You missed documents !", "TRA-MIS: Validation Alert !", { progressBar: !0 });
        return false;
    }
}
function FillComponent() {
    $.ajax({
        type: "POST",
        url: "TrainingDocuments.aspx/GetComponent",
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
        url: "TrainingDocuments.aspx/GetYear",
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