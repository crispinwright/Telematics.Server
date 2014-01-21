var map;
var poly;
var animatedPolyLine;
var image = new google.maps.MarkerImage(appRoot+"content/caricon.png", null, null, new google.maps.Point(18, 10));

var eventSnapTo = [];
var service = new google.maps.DirectionsService();
var lastVertex = 0;
var car;
var chart;
var colors = ["#00FF00", "#FF0000","#000000"];
$(function () {
    var chat = $.connection.geoHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.sendGeoData = function (geodata) {
        // Html encode display name and message.
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
    var stepSize = 2;
    var len = decodedPath.length / stepSize;
    var temporaryPoly = new google.maps.Polyline();
    temporaryPoly.setPath(decodedPath);
    var atdistance;
    //cut it into 5 metre segments for rendering
    if (temporaryPoly.Distance() > 5) {
        atdistance = temporaryPoly.GetPointsAtDistance(5);
        atdistance.splice(0, 0, decodedPath[0]);
        atdistance.push(decodedPath[decodedPath.length - 1]);
    } else {
        atdistance = decodedPath;
    }
    var temp = colors[0];
    if (color != null && color > 1) {
        temp = '#'+ shadeColor('00FF00', (color - 1) * 100);
    }
    var poly = new google.maps.Polyline({
        strokeColor: temp
    });
    
    //var firstTwo = set.slice(0, 2);
    //poly.setPath(firstTwo);
    poly.setMap(map);
    var path = poly.getPath();
    renderLoop(path, atdistance, 0);
    return decodedPath;
}
function renderLoop(path,points, index) {
    setTimeout(function () {
        render(path, new google.maps.LatLng(points[index].d, points[index].e));
        index = index + 1;
        if (index == points.length)
            return;
        renderLoop(path, points, index);
    }, 0);
}
function render(path,point) {
    //setTimeout(function() {
        path.push(point);
        map.panTo(point);
        car.setPosition(point);
        //setTimeout
    //}, 1000);
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
    var origin = new google.maps.LatLng(-36.845577, 174.76068);
    var dest = new google.maps.LatLng(-36.872227, 174.705612);
    current = origin;
    var mapOptions = {
        center: origin,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map-canvas"),
        mapOptions);
}

var isInitialized = false;
var initialize2 = function (dest) {
    //var decodedPath =;
    //var dest = new google.maps.LatLng(dest.Lat, dest.Lon);
    if (dest.Route == null)
        return;
    var dest = google.maps.geometry.encoding.decodePath(dest.Route)[0];
    map.panTo(dest);
    car = new google.maps.Marker({ icon: image, position: dest, animation: google.maps.Animation.DROP, map: map });
    isInitialized = true;
};

var addGeodataMarker = function (geodata) {
    var myLatlng = new google.maps.LatLng(geodata.Lat, geodata.Long);
    createEventPolyline(current, myLatlng, null, null);
    current = myLatlng;
    map.panTo(current);
    car.setPosition(current);
};

var addGeodataMarker2 = function (geoMain) {
    for (var i = 0; i < geoMain.Points.length; i++) {
        if (!isInitialized) {
            initialize2(geoMain.Points[i]);
          //  geoMain.Points.splice(0, 1);
            setTimeout(function () {
                addGeodataMarker2(geoMain);
            }, 2000);
            return;
        } else {
            var geodata = geoMain.Points[i];
            if (geodata.Route == null)
                continue;
            var decodedRoute = createEventPolyline2(geodata.Route, geodata.SpeedPercentage);
            current = decodedRoute[decodedRoute.length - 1];
//            map.panTo(current);
//            car.setPosition(current);
        }
        
    }
};

function shadeColor(color, percent) {
    var num = parseInt(color, 16),
    amt = Math.round(2.55 * percent),
    R = (num >> 16) + amt*2,
    G = (num >> 8 & 0x00FF) - amt*3,
    B = (num & 0x000000) ;
    return (0x1000000 + (R < 255 ? R < 1 ? 0 : R : 255) * 0x10000 + (G < 255 ? G < 1 ? 0 : G : 255) * 0x100 + (B < 255 ? B < 1 ? 0 : B : 255)).toString(16).slice(1);
}

google.maps.event.addDomListener(window, 'load', initialize);