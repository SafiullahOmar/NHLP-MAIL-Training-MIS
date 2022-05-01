$(window).on("load",function(){require.config({paths:{echarts:"robust-assets/js/plugins/charts/echarts"}}),require(["echarts","echarts/chart/radar","echarts/chart/chord"],function(a){var b=a.init(document.getElementById("multiple-radars"));chartOptions={tooltip:{trigger:"axis"},legend:{x:"center",data:["Certain software","A staple phone","The fruit of a mobile phone","precipitation","evaporation"]},polar:[{indicator:[{text:"Brands",max:100},{text:"Content",max:100},{text:"Availability",max:100},{text:"Features",max:100}],center:["20%",200],radius:150},{indicator:[{text:"Exterior",max:100},{text:"Photograph",max:100},{text:"System",max:100},{text:"Performance",max:100},{text:"Screen",max:100}],radius:150},{indicator:function(){for(var a=[],b=1;b<=12;b++)a.push({text:b+"month",max:100});return a}(),center:["80%",200],radius:150}],calculable:!0,series:[{type:"radar",tooltip:{trigger:"item"},itemStyle:{normal:{areaStyle:{type:"default"}}},data:[{value:[60,73,85,40],name:"Certain software"}]},{type:"radar",polarIndex:1,data:[{value:[85,90,90,95,95],name:"A staple phone"},{value:[95,80,95,90,93],name:"A mobile phone fruit"}]},{type:"radar",polarIndex:2,itemStyle:{normal:{areaStyle:{type:"default"}}},data:[{name:"Precipitation",value:[2.6,5.9,9,26.4,28.7,70.7,75.6,82.2,48.7,18.8,6,2.3]},{name:"Evaporation",value:[2,4.9,7,23.2,25.6,76.7,35.6,62.2,32.6,20,6.4,3.3]}]}]},b.setOption(chartOptions),$(function(){function a(){setTimeout(function(){b.resize()},200)}$(window).on("resize",a),$(".menu-toggle").on("click",a)})})});