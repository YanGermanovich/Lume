$(document).ready(function () {
    Initialize();
});
var marker;
// Where all the fun happens 
function Initialize() {

    var myLatlng = new google.maps.LatLng(53.90301904723439, 27.55883505550067);
    var mapOptions = {
        zoom: 15,
        center: myLatlng
    }
    var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

    // Place a draggable marker on the map
     marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        draggable: true,
        title: "Drag me!"
    });
}
