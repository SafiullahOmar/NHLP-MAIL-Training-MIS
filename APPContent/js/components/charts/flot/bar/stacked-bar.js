$(window).on("load",function(){function a(){$.plot("#stacked-bar",[b,d,e],{series:{stack:f,lines:{show:h,fill:!0,steps:i,lineWidth:0},bars:{show:g,barWidth:.6,lineWidth:0,fill:1}},grid:{borderWidth:1,borderColor:"#e9e9e9",color:"#999",minBorderMargin:20,labelMargin:10,margin:{top:8,bottom:20,left:20}},colors:["#FFE57F","#FFD740","#FFC400"]})}for(var b=[],c=0;c<=10;c+=1)b.push([c,parseInt(30*Math.random())]);for(var d=[],c=0;c<=10;c+=1)d.push([c,parseInt(30*Math.random())]);for(var e=[],c=0;c<=10;c+=1)e.push([c,parseInt(30*Math.random())]);var f=0,g=!0,h=!1,i=!1;a()});