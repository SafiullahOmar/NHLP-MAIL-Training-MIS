$(window).on("load",function(){require.config({paths:{echarts:"robust-assets/js/plugins/charts/echarts"}}),require(["echarts","echarts/chart/bar","echarts/chart/line"],function(a){var b=a.init(document.getElementById("stacked-column"));chartOptions={grid:{x:40,x2:40,y:45,y2:25},tooltip:{trigger:"axis",axisPointer:{type:"shadow"}},legend:{data:["Direct access","Email marketing","Advertising alliance","Video ads","Search engine","Google","Safari","Opera","Firefox"]},color:["#99B898","#FECEA8","#FF847C","#E84A5F","#F05053","#5A5050","#547A82","#3EACA8","#A2D4AB"],calculable:!0,xAxis:[{type:"category",data:["Mon","Tue","Wed","Thu","Fri","Sat","Sun"]}],yAxis:[{type:"value"}],series:[{name:"Direct access",type:"bar",data:[320,332,301,334,390,330,320]},{name:"Email marketing",type:"bar",stack:"advertising",data:[120,132,101,134,90,230,210]},{name:"Advertising alliance",type:"bar",stack:"advertising",data:[220,182,191,234,290,330,310]},{name:"Video ads",type:"bar",stack:"advertising",data:[150,232,201,154,190,330,410]},{name:"Search engine",type:"bar",data:[862,1018,964,1026,1679,1600,1570],markLine:{itemStyle:{normal:{lineStyle:{type:"dashed"}}},data:[[{type:"min"},{type:"max"}]]}},{name:"Google",type:"bar",barWidth:12,stack:"search engine",data:[620,732,701,734,1090,1130,1120]},{name:"Safari",type:"bar",stack:"search engine",data:[120,132,101,134,290,230,220]},{name:"Opera",type:"bar",stack:"search engine",data:[60,72,71,74,190,130,110]},{name:"Firefox",type:"bar",stack:"search engine",data:[62,82,91,84,109,110,120]}]},b.setOption(chartOptions),$(function(){function a(){setTimeout(function(){b.resize()},200)}$(window).on("resize",a),$(".menu-toggle").on("click",a)})})});