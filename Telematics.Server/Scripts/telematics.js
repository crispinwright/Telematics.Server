var map;
var poly;
var animatedPolyLine;
var image = new google.maps.MarkerImage("/Telematics.Server/content/caricon.png", null, null, new google.maps.Point(18, 10));

var eventSnapTo = [];
var service = new google.maps.DirectionsService();
var lastVertex = 0;
var car;
var chart;
$(function () {
    var chat = $.connection.geoHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.sendGeoData = function (geodata) {
        // Html encode display name and message.
        // alert(geodata.Time);
        addGeodataMarker2(geodata);
    };
    $.connection.hub.start();
    // Get the user name and store it to prepend to messages.
});

function getDelta(targetPoint, sourcePoint) {
    //original & wrong//return Math.abs((targetPoint.latRadians() - sourcePoint.latRadians()) + (targetPoint.lngRadians() - sourcePoint.lngRadians()));
    var R = 6371; // km
    var d = Math.acos(Math.sin(targetPoint.lat()) * Math.sin(sourcePoint.lat()) +
        Math.cos(targetPoint.lat()) * Math.cos(sourcePoint.lat()) *
            Math.cos(sourcePoint.lng() - targetPoint.lng())) * R;

    return d;
}

function getNearestPoint(polyline, point) {
    var nearestPoint;
    var path = polyline.getPath();
    var currentDelta = 1000;

    for (var i = 0; i < path.getLength() ; i++) {
        delta = getDelta(current, point);
        if (delta < currentDelta) {
            currentDelta = delta;
            nearestPoint = path.getAt(i);
        }
    }

    return nearestPoint;
}

function createEventPolyline(origin, destination, wayPoints, color) {
    var finalPath = [];
    service.route({
        origin: origin,
        destination: destination,
        travelMode: google.maps.DirectionsTravelMode.DRIVING,
        waypoints: wayPoints
    }, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            finalPath = finalPath.concat(result.routes[0].overview_path);

            poly = new google.maps.Polyline();
            poly.setPath(finalPath);
            poly.setMap(map);
            //for (i = 0; i < path.length; i++) {
            // var nearestPoint = getNearestPoint(poly, destination.LatLong);
            eventSnapTo.push({ LatLng: current, EventID: 'asd' });
            //}
        }
    });
}

function createEventPolyline2(route, color) {
    var decodedPath = google.maps.geometry.encoding.decodePath(route);
    poly = new google.maps.Polyline();
    poly.setPath(decodedPath);
    poly.setMap(map);
}

function updatePoly(d) {
    var animatedPolyLineLength = animatedPolyLine.getPath().getLength();
    // Spawn a new polyline every 20 vertices, because updating a 100-vertex poly is too slow
    if (animatedPolyLineLength > 20) {
        animatedPolyLine = new google.maps.Polyline([poly.getPath().getAt(lastVertex - 1)]);
    }

    if (poly.GetIndexAtDistance(d) < lastVertex + 2) {
        if (animatedPolyLineLength > 1) {
            animatedPolyLine.getPath().removeAt(animatedPolyLineLength - 1);
        }
        animatedPolyLine.getPath().insertAt(animatedPolyLineLength, poly.GetPointAtDistance(d));
        animatedPolyLine.setMap(map);
    } else {
        animatedPolyLine.getPath().insertAt(animatedPolyLineLength, poly.getPath().getAt(lastVertex++));
    }

    if (eventSnapTo.length > 0) {
        delta = getDelta(animatedPolyLine.getPath().getAt(animatedPolyLineLength - 1), eventSnapTo[0].LatLng);

        if (delta < 5) {
            var vehicleEvent = eventSnapTo.shift();

            var marker = new google.maps.Marker({
                position: vehicleEvent.LatLng,
                map: map,
                title: "EventID: " + vehicleEvent.EventID + " Event Lat Long: " + vehicleEvent.LatLng + " Current Lat Lng :" + animatedPolyLine.getPath().getAt(animatedPolyLineLength - 1)
            });
        }
    }
}

function animate() {
    //createEventPolyline();

}

function startAnimation() {
    car = new google.maps.Marker({ icon: image, position: poly.getPath().getAt(0), animation: google.maps.Animation.DROP, map: map });
    //nearestPoint = current;
    //animatedPolyLine = new google.maps.Polyline({ path: [poly.getPath().getAt(0)], strokeColor: "#0000FF" });
    setTimeout("animate()", 2000); // Allow time for the initial map display
}

var current;

function initialize() {
    var origin = new google.maps.LatLng(-36.871616, 174.709610);
    var dest = new google.maps.LatLng(-36.872227, 174.705612);
    current = origin;
    var mapOptions = {
        center: origin,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map-canvas"),
        mapOptions);
    var myLatlng = new google.maps.LatLng(-36.871616, 174.709610);
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: "Hello World!"
    });

    //current = dest;
    setTimeout(function () {
        car = new google.maps.Marker({ icon: image, position: origin, animation: google.maps.Animation.DROP, map: map });
        var geo = new Object();
        geo.Lat = dest.lat();
        geo.Long = dest.lng();
        setTimeout(function () {
            addGeodataMarker(geo);
        }, 2000);
    }, 2000);
}

var addGeodataMarker = function (geodata) {
    var myLatlng = new google.maps.LatLng(geodata.Lat, geodata.Long);
    createEventPolyline(current, myLatlng, null, null);
    current = myLatlng;
    map.panTo(current);
    car.setPosition(current);
};


var addGeodataMarker2 = function (geoMain) {
    var geodata = geoMain.Points[0];
    var myLatlng = new google.maps.LatLng(geodata.Lat, geodata.Lon);
    createEventPolyline2(geodata.Route);
    current = myLatlng;
    map.panTo(current);
    car.setPosition(current);
};

google.maps.event.addDomListener(window, 'load', initialize);