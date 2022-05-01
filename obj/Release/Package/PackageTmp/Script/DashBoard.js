//Front Page
$(function () {
    
    FillYear();
    $("#ddlYear option:contains('" + (new Date()).getFullYear() + "')").prop('selected', 'selected');
    $("#ddlYear").change(function () {
        $(".active").parent().trigger("click");
    });
    //Draw Training By Province
    $.fn.drawTrainingByProvince = function () {
        var a = $(window).scrollTop();
        $.ajax({
            type: "POST",
            url: "DashBoard.aspx/GetTrainingByProvince",
            data: '{YearID: ' + $('#ddlYear').val() + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                cnt = []; Account = []; S1 = []; S2 = []; S3 = []; S4 = []; S5 = [];
                $.each(data.d, function (i, x) {
                    Account.push(x.Title);
                    S1.push(x.sample1);
                    S2.push(x.sample2);
                    S3.push(x.sample3);
                    S4.push(x.sample4);
                    S5.push(x.sample5);
                });

            },
            failure: function (response) {
                alert(response.d);
            }
        });

        $('#containerChart').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: 'TRAINING INFORMATION'
            },
            subtitle: {
                text: 'Total number of training hold by component and by province'
            },
            xAxis: {
                categories: Account,
                crosshair: true

            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            tooltip: {
                headerFormat: '<span style="font-weight:bold;">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>' + addCommas('{point.y}') + '</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            yAxis: {
                min: 0,
                min: 0,
                labels: {
                    style: {
                        color: "black",
                        fontWeight: "bold",
                        fontSize: "9px"
                    }
                },
                stackLabels: {
                    enabled: !0,
                    style: {
                        fontWeight: "bold",
                        fontSize: "11px",
                        color: "black"
                    }
                },
                title: {
                    text: 'Number of Training'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            series: [{
                type: 'column',
                name: 'Horticulture',
                data: S1,
                color: "#61E965"
            }, {
                type: 'column',
                name: 'Livestock',
                data: S2,
                color: "#FC9641"
            },
            {
                type: 'column',
                name: 'ESMF',
                data: S3,
                color: "#1BDEC7"
            },
            {
                type: 'column',
                name: 'Gender',
                data: S4,
                color: "#DF6EEA"
            }, {
                type: 'column',
                name: 'Marketing',
                data: S5,
                color: "#F9071D"
            }, ],
            plotOptions: {
                series: {
                    stacking: 'normal'
                }
            }

        });
        $(window).scrollTop(a)
    }

    //Draw Training By Region
    $.fn.drawTrainingByRegion = function () {
        var a = $(window).scrollTop();
        $.ajax({
            type: "POST",
            url: "DashBoard.aspx/GetTrainingByRegion",
            data: '{YearID: ' + $('#ddlYear').val() + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                cnt = []; Account = []; S1 = []; S2 = []; S3 = []; S4 = []; S5 = [];
                $.each(data.d, function (i, x) {
                    Account.push(x.Title);
                    S1.push(x.sample1);
                    S2.push(x.sample2);
                    S3.push(x.sample3);
                    S4.push(x.sample4);
                    S5.push(x.sample5);
                });

            },
            failure: function (response) {
                alert(response.d);
            }
        });

        $('#containerChart').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: 'TRAINING INFORMATION'
            },
            subtitle: {
                text: 'Total number of training hold by component and by region'
            },
            xAxis: {
                categories: Account,
                crosshair: true

            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            tooltip: {
                headerFormat: '<span style="font-weight:bold;">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>' + addCommas('{point.y}') + '</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            yAxis: {
                min: 0,
                min: 0,
                labels: {
                    style: {
                        color: "black",
                        fontWeight: "bold",
                        fontSize: "9px"
                    }
                },
                stackLabels: {
                    enabled: !0,
                    style: {
                        fontWeight: "bold",
                        fontSize: "11px",
                        color: "black"
                    }
                },
                title: {
                    text: 'Number of Training'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            series: [{
                type: 'column',
                name: 'Horticulture',
                data: S1,
                color: "#61E965"
            }, {
                type: 'column',
                name: 'Livestock',
                data: S2,
                color: "#FC9641"
            },
            {
                type: 'column',
                name: 'ESMF',
                data: S3,
                color: "#1BDEC7"
            },
            {
                type: 'column',
                name: 'Gender',
                data: S4,
                color: "#DF6EEA"
            }, {
                type: 'column',
                name: 'Marketing',
                data: S5,
                color: "#F9071D"
            }, ],
            plotOptions: {
                column: {
                    dataLabels: {
                        enabled: !0,
                        rotation: 0,
                        softConnector: !0,
                        y: -8,
                        verticalAlign: "Top"
                    }
                }
            }

        });
        $(window).scrollTop(a)
    }

    //Draw Participant By Province
    $.fn.drawParticipantByProvince = function () {
        var a = $(window).scrollTop();
        $.ajax({
            type: "POST",
            url: "DashBoard.aspx/GetParticipantByProvince",
            data: '{YearID: ' + $('#ddlYear').val() + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                cnt = []; Account = []; S1 = []; S2 = [];
                $.each(data.d, function (i, x) {
                    Account.push(x.Title);
                    S1.push(x.sample1);
                    S2.push(x.sample2);
                });

            },
            failure: function (response) {
                alert(response.d);
            }
        });

        $('#containerChart').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: 'TRAINING PARTICIPANT INFORMATION'
            },
            subtitle: {
                text: 'Total number of participant by gender and by province'
            },
            xAxis: {
                categories: Account,
                crosshair: true

            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            tooltip: {
                headerFormat: '<span style="font-weight:bold;">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>' + addCommas('{point.y}') + '</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            yAxis: {
                min: 0,
                min: 0,
                labels: {
                    style: {
                        color: "black",
                        fontWeight: "bold",
                        fontSize: "9px"
                    }
                },
                stackLabels: {
                    enabled: !0,
                    style: {
                        fontWeight: "bold",
                        fontSize: "11px",
                        color: "black"
                    }
                },
                title: {
                    text: 'Number of Participant'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            series: [{
                type: 'column',
                name: 'Male',
                data: S1,
                color: "#61E965"
            }, {
                type: 'column',
                name: 'Female',
                data: S2,
                color: "#FC9641"
            } ],
            plotOptions: {
                series: {
                    stacking: 'normal'
                }
            }

        });
        $(window).scrollTop(a)
    }

    //Draw Participant By Component
    $.fn.drawParticipantByComponent= function () {
        var a = $(window).scrollTop();
        $.ajax({
            type: "POST",
            url: "DashBoard.aspx/GetParticipantByComponent",
            data: '{YearID: ' + $('#ddlYear').val() + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                cnt = []; Account = []; S1 = []; S2 = [];
                $.each(data.d, function (i, x) {
                    Account.push(x.Title);
                    S1.push(x.sample1);
                    S2.push(x.sample2);
                });

            },
            failure: function (response) {
                alert(response.d);
            }
        });

        $('#containerChart').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: 'TRAINING PARTICIPANT INFORMATION'
            },
            subtitle: {
                text: 'Total number of participant by component'
            },
            xAxis: {
                categories: Account,
                crosshair: true

            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            tooltip: {
                headerFormat: '<span style="font-weight:bold;">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>' + addCommas('{point.y}') + '</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            yAxis: {
                min: 0,
                min: 0,
                labels: {
                    style: {
                        color: "black",
                        fontWeight: "bold",
                        fontSize: "9px"
                    }
                },
                stackLabels: {
                    enabled: !0,
                    style: {
                        fontWeight: "bold",
                        fontSize: "11px",
                        color: "black"
                    }
                },
                title: {
                    text: 'Number of Participant'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            series: [{
                type: 'column',
                name: 'Male',
                data: S1,
                color: "#61E965"
            }, {
                type: 'column',
                name: 'Female',
                data: S2,
                color: "#FC9641"
            }],
            plotOptions: {
                series: {
                    stacking: 'normal'
                }
            }

        });
        $(window).scrollTop(a)
    }

    //Draw Participant By Region
    $.fn.drawParticipantByRegion = function () {
        var a = $(window).scrollTop();
        $.ajax({
            type: "POST",
            url: "DashBoard.aspx/GetParticipantByRegion",
            data: '{YearID: ' + $('#ddlYear').val() + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                cnt = []; Account = []; S1 = []; S2 = [];
                $.each(data.d, function (i, x) {
                    Account.push(x.Title);
                    S1.push(x.sample1);
                    S2.push(x.sample2);
                });

            },
            failure: function (response) {
                alert(response.d);
            }
        });

        $('#containerChart').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: 'TRAINING PARTICIPANT INFORMATION'
            },
            subtitle: {
                text: 'Total number of participant by region'
            },
            xAxis: {
                categories: Account,
                crosshair: true

            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            tooltip: {
                headerFormat: '<span style="font-weight:bold;">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>' + addCommas('{point.y}') + '</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            yAxis: {
                min: 0,
                min: 0,
                labels: {
                    style: {
                        color: "black",
                        fontWeight: "bold",
                        fontSize: "9px"
                    }
                },
                stackLabels: {
                    enabled: !0,
                    style: {
                        fontWeight: "bold",
                        fontSize: "11px",
                        color: "black"
                    }
                },
                title: {
                    text: 'Number of Participant'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            series: [{
                type: 'column',
                name: 'Male',
                data: S1,
                color: "#61E965"
            }, {
                type: 'column',
                name: 'Female',
                data: S2,
                color: "#FC9641"
            }],
            plotOptions: {
                column: {
                    dataLabels: {
                        enabled: !0,
                        rotation: 0,
                        softConnector: !0,
                        y: -8,
                        verticalAlign: "Top"
                    }
                }
            }

        });
        $(window).scrollTop(a)
    }

    $.fn.clearActive = function () {
        $('#chartnav li a').each(function () { $(this).removeClass('active'); });
    }

    $('#TrainingByProvince').click(function () {
        $.fn.clearActive();
        $('#tab1').addClass('active');
        $.fn.drawTrainingByProvince();
    });

    $('#TrainingByRegion').click(function () {
        $.fn.clearActive();
        $('#tab2').addClass('active');
        $.fn.drawTrainingByRegion();
    });

    $('#ParticipantByProvince').click(function () {
        $.fn.clearActive();
        $('#tab3').addClass('active');
        $.fn.drawParticipantByProvince();
    });

    $('#ParticipantByComponent').click(function () {
        $.fn.clearActive();
        $('#tab4').addClass('active');
        $.fn.drawParticipantByComponent();
    });

    $('#ParticipantByRegion').click(function () {
        $.fn.clearActive();
        $('#tab5').addClass('active');
        $.fn.drawParticipantByRegion();
    });

    $("#TrainingByProvince").trigger('click');
});
function FillYear() {
    $.ajax({
        type: "POST",
        url: "DashBoard.aspx/GetYear",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
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