$(window).on("load",function(){function a(a){var b,c=[];if(a.venues.length>0){b=a.venues;for(var d=0;d<b.length;d++){var e=b[d];if(void 0!=e.location.lat&&void 0!=e.location.lng){var f="https://foursquare.com/img/categories/food/default.png";c.push({lat:e.location.lat,lng:e.location.lng,title:e.name,icon:{size:new google.maps.Size(32,32),url:f}})}}}h.addMarkers(c)}function b(a){$("#foursquare-results").text(JSON.stringify(a))}context_menu_map=new GMaps({div:"#context-menu",lat:-12.043333,lng:-77.028333,styles:[{stylers:[{hue:"#2c3e50"},{saturation:250}]},{featureType:"road",elementType:"geometry",stylers:[{lightness:50},{visibility:"simplified"}]},{featureType:"road",elementType:"labels",stylers:[{visibility:"off"}]}]}),context_menu_map.setContextMenu({control:"map",options:[{title:"Add marker",name:"add_marker",action:function(a){this.addMarker({lat:a.latLng.lat(),lng:a.latLng.lng(),title:"New marker"})}},{title:"Center here",name:"center_here",action:function(a){this.setCenter(a.latLng.lat(),a.latLng.lng())}}]}),geofences_map=new GMaps({div:"#geofences",lat:-12.043333,lng:-77.028333});var c=[],d=[[-12.040397656836609,-77.03373871559225],[-12.040248585302038,-77.03993927003302],[-12.050047116528843,-77.02448169303511],[-12.044804866577001,-77.02154422636042]];for(var e in d)latlng=new google.maps.LatLng(d[e][0],d[e][1]),c.push(latlng);polygon=geofences_map.drawPolygon({paths:c,strokeColor:"#BBD8E9",strokeOpacity:1,strokeWeight:3,fillColor:"#BBD8E9",fillOpacity:.6}),geofences_map.addMarker({lat:-12.043333,lng:-77.028333,draggable:!0,fences:[polygon],outside:function(a,b){alert("This marker has been moved outside of its fence")}}),custom_control_map=new GMaps({div:"#custom-controls",zoom:16,lat:-12.043333,lng:-77.028333,styles:[{featureType:"water",elementType:"geometry",stylers:[{hue:"#165c64"},{saturation:34},{lightness:-69},{visibility:"on"}]},{featureType:"landscape",elementType:"geometry",stylers:[{hue:"#b7caaa"},{saturation:-14},{lightness:-18},{visibility:"on"}]},{featureType:"landscape.man_made",elementType:"all",stylers:[{hue:"#cbdac1"},{saturation:-6},{lightness:-9},{visibility:"on"}]},{featureType:"road",elementType:"geometry",stylers:[{hue:"#8d9b83"},{saturation:-89},{lightness:-12},{visibility:"on"}]},{featureType:"road.highway",elementType:"geometry",stylers:[{hue:"#d4dad0"},{saturation:-88},{lightness:54},{visibility:"simplified"}]},{featureType:"road.arterial",elementType:"geometry",stylers:[{hue:"#bdc5b6"},{saturation:-89},{lightness:-3},{visibility:"simplified"}]},{featureType:"road.local",elementType:"geometry",stylers:[{hue:"#bdc5b6"},{saturation:-89},{lightness:-26},{visibility:"on"}]},{featureType:"poi",elementType:"geometry",stylers:[{hue:"#c17118"},{saturation:61},{lightness:-45},{visibility:"on"}]},{featureType:"poi.park",elementType:"all",stylers:[{hue:"#8ba975"},{saturation:-46},{lightness:-28},{visibility:"on"}]},{featureType:"transit",elementType:"geometry",stylers:[{hue:"#a43218"},{saturation:74},{lightness:-51},{visibility:"simplified"}]},{featureType:"administrative.province",elementType:"all",stylers:[{hue:"#ffffff"},{saturation:0},{lightness:100},{visibility:"simplified"}]},{featureType:"administrative.neighborhood",elementType:"all",stylers:[{hue:"#ffffff"},{saturation:0},{lightness:100},{visibility:"off"}]},{featureType:"administrative.locality",elementType:"labels",stylers:[{hue:"#ffffff"},{saturation:0},{lightness:100},{visibility:"off"}]},{featureType:"administrative.land_parcel",elementType:"all",stylers:[{hue:"#ffffff"},{saturation:0},{lightness:100},{visibility:"off"}]},{featureType:"administrative",elementType:"all",stylers:[{hue:"#3a3935"},{saturation:5},{lightness:-57},{visibility:"off"}]},{featureType:"poi.medical",elementType:"geometry",stylers:[{hue:"#cba923"},{saturation:50},{lightness:-46},{visibility:"on"}]}]}),custom_control_map.addControl({position:"top_right",content:"Geolocate",style:{margin:"5px",padding:"1px 6px",border:"solid 1px #717B87",background:"#fff"},events:{click:function(){GMaps.geolocate({success:function(a){custom_control_map.setCenter(a.coords.latitude,a.coords.longitude)},error:function(a){alert("Geolocation failed: "+a.message)},not_supported:function(){alert("Your browser does not support geolocation")}})}}}),infoWindow=new google.maps.InfoWindow({}),fusion_layers_map=new GMaps({div:"#fusion-table-layers",zoom:11,lat:41.850033,lng:-87.6500523}),fusion_layers_map.loadFromFusionTables({query:{select:"'Geocodable address'",from:"1mZ53Z70NsChnBMm-qEYmSDOvLXgrreLTkQUvvg"},suppressInfoWindows:!0,events:{click:function(a){infoWindow.setContent("You clicked here!"),infoWindow.setPosition(a.latLng),infoWindow.open(fusion_layers_map.map)}}}),infoWindow=new google.maps.InfoWindow({}),kml_layers_map=new GMaps({div:"#kml-layers",zoom:12,lat:40.65,lng:-73.95}),kml_layers_map.loadFromKML({url:"http://api.flickr.com/services/feeds/geo/?g=322338@N20&lang=en-us&format=feed-georss",suppressInfoWindows:!0,events:{click:function(a){infoWindow.setContent(a.featureData.infoWindowHtml),infoWindow.setPosition(a.latLng),infoWindow.open(kml_layers_map.map)}}}),g=new GMaps({div:"#map-types",lat:-12.043333,lng:-77.028333,mapTypeControlOptions:{mapTypeIds:["hybrid","roadmap","satellite","terrain","osm"]}}),g.addMapType("osm",{getTileUrl:function(a,b){return"https://a.tile.openstreetmap.org/"+b+"/"+a.x+"/"+a.y+".png"},tileSize:new google.maps.Size(256,256),name:"OpenStreetMap",maxZoom:18}),g.setMapTypeId("osm");var f=function(a,b,c){var d=c.createElement("div");return d.innerHTML=a,d.style.width=this.tileSize.width+"px",d.style.height=this.tileSize.height+"px",d.style.background="rgba(250, 250, 250, 0.55)",d.style.fontFamily="Monaco, Andale Mono, Courier New, monospace",d.style.fontSize="10",d.style.fontWeight="bolder",d.style.border="dotted 1px #aaa",d.style.textAlign="center",d.style.lineHeight=this.tileSize.height+"px",d};g=new GMaps({el:"#overlay-map-types",lat:-12.043333,lng:-77.028333}),g.addOverlayMapType({index:0,tileSize:new google.maps.Size(256,256),getTile:f}),panorama=GMaps.createPanorama({el:"#street-view-panoramas",lat:42.3455,lng:-71.0983});var g;$(document).on("submit",".edit_marker",function(a){a.preventDefault();var b=$(this).data("marker-index");$lat=$("#marker_"+b+"_lat").val(),$lng=$("#marker_"+b+"_lng").val();var c=$("#edit_marker_template").text(),d=c.replace(/{{index}}/g,b).replace(/{{lat}}/g,$lat).replace(/{{lng}}/g,$lng);g.markers[b].setPosition(new google.maps.LatLng($lat,$lng)),g.markers[b].infoWindow.setContent(d),$marker=$("#markers-with-coordinates").find("li").eq(0).find("a"),$marker.data("marker-lat",$lat),$marker.data("marker-lng",$lng)}),$(document).on("click",".pan-to-marker",function(a){a.preventDefault();var b,c,d=$(this).data("marker-index"),e=$(this).data("marker-lat"),f=$(this).data("marker-lng");if(void 0!=d){var h=g.markers[d].getPosition();b=h.lat(),c=h.lng()}else b=e,c=f;g.setCenter(b,c)}),g=new GMaps({div:"#interact-ui",lat:-12.043333,lng:-77.028333,styles:[{featureType:"water",elementType:"geometry",stylers:[{color:"#193341"}]},{featureType:"landscape",elementType:"geometry",stylers:[{color:"#2c5a71"}]},{featureType:"road",elementType:"geometry",stylers:[{color:"#29768a"},{lightness:-37}]},{featureType:"poi",elementType:"geometry",stylers:[{color:"#406d80"}]},{featureType:"transit",elementType:"geometry",stylers:[{color:"#406d80"}]},{elementType:"labels.text.stroke",stylers:[{visibility:"on"},{color:"#3e606f"},{weight:2},{gamma:.84}]},{elementType:"labels.text.fill",stylers:[{color:"#ffffff"}]},{featureType:"administrative",elementType:"geometry",stylers:[{weight:.6},{color:"#1a3541"}]},{elementType:"labels.icon",stylers:[{visibility:"off"}]},{featureType:"poi.park",elementType:"geometry",stylers:[{color:"#2c5a71"}]}]}),GMaps.on("marker_added",g,function(a){$("#markers-with-index").append('<li><a href="#" class="pan-to-marker" data-marker-index="'+g.markers.indexOf(a)+'">'+a.title+"</a></li>"),$("#markers-with-coordinates").append('<li><a href="#" class="pan-to-marker" data-marker-lat="'+a.getPosition().lat()+'" data-marker-lng="'+a.getPosition().lng()+'">'+a.title+"</a></li>")}),GMaps.on("click",g.map,function(a){var b=g.markers.length,c=a.latLng.lat(),d=a.latLng.lng(),e=$("#edit_marker_template").text(),f=e.replace(/{{index}}/g,b).replace(/{{lat}}/g,c).replace(/{{lng}}/g,d);g.addMarker({lat:c,lng:d,title:"Marker #"+b,infoWindow:{content:f}})});var h;$(document).on("click",".pan-to-marker",function(a){a.preventDefault();var b,c,d,e;e=$(this).data("marker-index"),b=h.markers[e].getPosition(),c=b.lat(),d=b.lng(),h.setCenter(c,d)}),h=new GMaps({div:"#json",lat:-12.043333,lng:-77.028333,styles:[{featureType:"administrative",elementType:"labels.text.fill",stylers:[{color:"#444444"}]},{featureType:"landscape",elementType:"all",stylers:[{color:"#f2f2f2"}]},{featureType:"poi",elementType:"all",stylers:[{visibility:"off"}]},{featureType:"road",elementType:"all",stylers:[{saturation:-100},{lightness:45}]},{featureType:"road.highway",elementType:"all",stylers:[{visibility:"simplified"}]},{featureType:"road.arterial",elementType:"labels.icon",stylers:[{visibility:"off"}]},{featureType:"transit",elementType:"all",stylers:[{visibility:"off"}]},{featureType:"water",elementType:"all",stylers:[{color:"#46bcec"},{visibility:"on"}]}]}),h.on("marker_added",function(a){var b=h.markers.indexOf(a);$("#results").append('<li><a href="#" class="pan-to-marker" data-marker-index="'+b+'">'+a.title+"</a></li>"),b==h.markers.length-1&&h.fitZoom()});var i=$.getJSON("robust-assets/demo-data/gmaps/foursquare.json?q[near]=Lima,%20PE&q[query]=Ceviche");i.done(b),i.done(a)});