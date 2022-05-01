//Front Page
$(function () {
    $("#btnUpdate").hide();
    $("#btnSave").show();

    //Display Data...!
    ShowData();

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
});
function EditDetail(obj) {
    var td = obj.parentNode;
    row = obj.parentNode.parentNode;
    var span0 = $(td).find('span:eq(0)');
    var ID = span0.html();
    var span1 = $(td).find('span:eq(1)');
    var Name = span1.html();
    var span2 = $(td).find('span:eq(2)');
    var NameLocal = span2.html();
    $('#hfComponentID').val(ID);

    $('#txtName').val(Name);
    $('#txtNameLocal').val(NameLocal);

    $('#btnSave').hide();
    $('#btnUpdate').show();
    // $('.delete').show();

    toastr.info("Please enter correct information !", "TRA-MIS:  Alert !", { progressBar: !0 });

}
function ShowData() {
    $.ajax({
        type: "POST",
        url: "ComponentSetup.aspx/GetFormDetail",
        data: '',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $table = "<table id='tbl'  width='100%' class='table table-xs nowrap table-striped scroll-horizontal table-hover'>";
            var header = "<thead><tr ><th>SN</th><th style='text-align:center;'>Name</th><th style='text-align:center;'>Local Name </th><th style='text-align:center;'></th></tr></thead>";
            $table += header;
            var count = 1;
            $.each(data.d, function (i, x) {
                var row = "<tr ><td style='text-align:center;'>" + count + "</td>" +
                   "<td style='text-align:center;'><span>" + x.Name + "  </span></td>" +
                   "<td style='text-align:center;'><span>" + x.NameLocal + "</span></td>" +
                   (x.Edit == true ? "<td><a onclick='EditDetail(this)' class='btn-sm btn-blue edit' href='#'>Edit</a><span style='display:none'>" + x.ComponentID + "</span><span style='display:none'>" + x.Name + "</span><span style='display:none'>" + x.NameLocal + "</span></td>" : "<td></td>") +
                "</tr>";
                $table += row;
                count++;
            });
            var footer = "<tfoot> <tr><td></td> <th>Name</th> <th>Local Name</th><td></td></tr></tfoot>";
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
    a.Name = $('#txtName').val();
    a.NameLocal = $('#txtNameLocal').val();


    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "ComponentSetup.aspx/Save",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("Component information is Successfully saved !", "TRA-MIS: Component !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The Component information is saved successfully.<br/> THANK YOU..!",
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
                    text: "This component has alreday exist.<br/>Please review the information.",
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
    a.ComponentID = $('#hfComponentID').val();
    a.Name = $('#txtName').val();
    a.NameLocal = $('#txtNameLocal').val();



    var jsonObject = JSON.stringify({ obj: a });

    $.ajax({
        type: "post",
        url: "ComponentSetup.aspx/Update",
        data: jsonObject,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == '0') {
                toastr.success("Component information is Successfully updated !", "TRA-MIS: Component !", { progressBar: !0 });
                swal({
                    title: "Success Alert!",
                    text: "The Component information is updated successfully.<br/> THANK YOU..!",
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
                    text: "This component has alreday exist.<br/>Please review the information.",
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
    $('#hfComponentID').val('');
    $('#txtName').val('');
    $('#txtNameLocal').val('');

    $('#btnSave').show();
    $('#btnUpdate').hide();
}
function formValidation(e) {

    var flag = true;


    if ($('#txtName').val() == '') {
        $('#txtName').css('background-color', '#FFAAAA');
        flag = false;
    }
    else { $('#txtName').css('background-color', '#ffff'); }

    if (flag) { return true } else {
        toastr.warning("Please enter valid informations in red backgrounds!", "TRA-MIS: Validation Alert !", { progressBar: !0 });
        return false;
    }
}