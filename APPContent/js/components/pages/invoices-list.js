$(document).ready(function(){$(".row-grouping").DataTable({responsive:!0,rowReorder:!0,columnDefs:[{visible:!1,targets:5}],displayLength:25,drawCallback:function(a){var b=this.api(),c=b.rows({page:"current"}).nodes(),d=null;b.column(5,{page:"current"}).data().each(function(a,b){d!==a&&($(c).eq(b).before('<tr class="group"><td colspan="9">'+a+"</td></tr>"),d=a)})}});$(".row-grouping tbody").on("click","tr.group",function(){if("undefined"!=typeof table&&table.order()[0]){var a=table.order()[0];5===a[0]&&"asc"===a[1]?table.order([5,"desc"]).draw():table.order([5,"asc"]).draw()}}),$(".icheck input").iCheck({checkboxClass:"icheckbox_square-blue",radioClass:"iradio_square-blue"}),$("#invoices-list").on("draw.dt",function(){$(".icheck input").iCheck({checkboxClass:"icheckbox_square-blue",radioClass:"iradio_square-blue"})});var a=$("input.input-chk-all"),b=$("input.input-chk");a.on("ifChecked ifUnchecked",function(a){"ifChecked"==a.type?b.iCheck("check"):b.iCheck("uncheck")}),b.on("ifChanged",function(c){b.filter(":checked").length==b.length?a.prop("checked","checked"):a.removeProp("checked"),a.iCheck("update")})});