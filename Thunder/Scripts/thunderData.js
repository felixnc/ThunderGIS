var RealTimethunder;
var map;
require([
  "esri/map", "esri/dijit/BasemapGallery", "esri/arcgis/utils",
  "dojo/parser",

  "dijit/layout/BorderContainer", "dijit/layout/ContentPane", "dijit/TitlePane",
  "dojo/domReady!"
], function (
  Map, BasemapGallery, arcgisUtils,
  parser
) {
    parser.parse();

    map = new Map("map", {
        //basemap: "topo",
        center: [103.35, 34.8],
        zoom: 5
    });
    var basemap = new esri.layers.ArcGISTiledMapServiceLayer("http://cache1.arcgisonline.cn/ArcGIS/rest/services/ChinaOnlineCommunity/MapServer");
    map.addLayer(basemap);
    //add the basemap gallery, in this case we'll display maps from ArcGIS.com including bing maps
    var basemapGallery = new BasemapGallery({
        showArcGISBasemaps: true,
        map: map
    }, "basemapGallery");
    basemapGallery.startup();

    basemapGallery.on("error", function (msg) {
        console.log("basemap gallery error:  ", msg);
    });

    RealTimethunder = $.connection.thunderCon;
    RealTimethunder.client.showdata = function showdata(str)
    {
        alert(str);
    }
    // Everything is ready, now start the connection
    //$.connection.hub.start();
   

});

function btnUpdateUserName_Click() {
    RealTimethunder.server.showdata("ceshiccc");
}