$(window).on("load",function(){var a=$("#combo-bar-line-data-label"),b={responsive:!0,maintainAspectRatio:!1,scales:{xAxes:[{display:!0,gridLines:{color:"#f3f3f3",drawTicks:!1},scaleLabel:{display:!0}}],yAxes:[{display:!0,gridLines:{color:"#f3f3f3",drawTicks:!1},scaleLabel:{display:!0}}]},title:{display:!1,text:"Chart.js Combo Bar Line data label Chart"},animation:{onComplete:function(){var a=this.chart,b=a.ctx;b.textAlign="center",Chart.helpers.each(this.data.datasets.forEach(function(c,d){var e=a.controller.getDatasetMeta(d);Chart.helpers.each(e.data.forEach(function(a,d){b.fillText(c.data[d],a._model.x,a._model.y-10)}),this)}),this)}}},c={labels:["January","February","March","April","May","June","July"],datasets:[{type:"line",label:"My Second dataset",data:[28,48,40,19,86,27,90],backgroundColor:"rgba(255,87,34,.5)",borderColor:"transparent",borderWidth:2,pointBorderColor:"#FF5722",pointBackgroundColor:"#FFF",pointBorderWidth:2,pointHoverBorderWidth:2,pointRadius:4},{type:"bar",label:"My First dataset",data:[65,59,80,81,56,55,40],backgroundColor:"#673AB7",borderColor:"transparent",borderWidth:2},{type:"bar",label:"My Third dataset - No bezier",data:[45,25,16,36,67,18,76],backgroundColor:"#E91E63",borderColor:"transparent",borderWidth:2}]},d={type:"bar",options:b,data:c};new Chart(a,d)});