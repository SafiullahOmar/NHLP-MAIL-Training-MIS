$(window).on("load",function(){function a(a){a.x0=a.x,a.dx0=a.dx}function b(a){var b=d3.interpolate({x:a.x0,dx:a.dx0},a);return function(c){var d=b(c);return a.x0=d.x,a.dx0=d.dx,j(d)}}var c=d3.select("#sunburst-partition"),d=c.node().getBoundingClientRect().width,e=400,f=Math.min(d,e)/2,g=d3.scale.ordinal().range(["#99B898","#FECEA8","#FF847C","#E84A5F","#C06C84","#6C5B7B","#355C7D"]),h=c.append("svg").attr("width",d).attr("height",e).append("g").attr("transform","translate("+d/2+","+.52*e+")"),i=d3.layout.partition().sort(null).size([2*Math.PI,f*f]).value(function(a){return 1}),j=d3.svg.arc().startAngle(function(a){return a.x}).endAngle(function(a){return a.x+a.dx}).innerRadius(function(a){return Math.sqrt(a.y)}).outerRadius(function(a){return Math.sqrt(a.y+a.dy)});d3.json("robust-assets/demo-data/d3/circle/flare.json",function(c,d){if(c)throw c;var e=h.datum(d).selectAll("path").data(i.nodes).enter().append("path").attr("display",function(a){return a.depth?null:"none"}).attr("d",j).style("stroke","#fff").style("fill",function(a){return g((a.children?a:a.parent).name)}).style("fill-rule","evenodd").each(a);d3.selectAll("input").on("change",function(){var a="count"===this.value?function(){return 1}:function(a){return a.size};e.data(i.value(a).nodes).transition().duration(1500).attrTween("d",b)})}),d3.select(self.frameElement).style("height",e+"px")});