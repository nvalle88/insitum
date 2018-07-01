(function ($) {
    $(function () {

        var resultado;
        var MyMarker = null;
        var chatInput = $("#chat-input");
        var userName;
        var map;
        var chat = $.connection.recived;
        var chatWindow = $("#chat-window");
        var map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -0.225219, lng: -78.5248 },
            zoom: 15
        });
        geoLocation(map);

        var icon1 =
            {
                size: new google.maps.Size(96, 96),
                scaledSize: new google.maps.Size(48, 48), // scaled size
                origin: new google.maps.Point(0, 0), // origin
                anchor: new google.maps.Point(24, 48), // anchor
                url: "../../Imagenes/pushpin vendedor-01.png" // url
            };
        markers = [];
        markersDelete = [];

        a = 0;
        markersAgentes = [];

        var marker;
        //this is the function that's run when the "messageReceived" function is called from the server
        chat.client.messageReceived = function (livePositionRequest) {

            if (vendedor == livePositionRequest.AgenteId) {
                var pos = { lat: livePositionRequest.Lat, lng: livePositionRequest.Lon };
                marker = new google.maps.Marker({
                    position: pos,
                    map: map,
                    title: livePositionRequest.Nombre,
                    icon: icon1
                });
                placeMarkerAndPanTo(pos,map)

            }
        };

        function placeMarkerAndPanTo(latLng, map) {

            marker.setMap(null);
            if (MyMarker != null) {
                MyMarker.setMap(null);
            }

            var markerNuevo = new google.maps.Marker({
                position: latLng,
                icon: icon1,
                map: map,
            });

            MyMarker = markerNuevo;
            var latitud = MyMarker.position.lat();
            var longitud = MyMarker.position.lng();
            map.panTo(latLng);

        }

        function geoLocation(map) {
            var infoWindow = new google.maps.InfoWindow({ map: map });
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

                    infoWindow.setPosition(pos);
                    infoWindow.setContent('Posici&oacute;n actual');
                    map.setCenter(pos);
                }, function () {
                    handleLocationError(true, infoWindow, map.getCenter());
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false, infoWindow, map.getCenter());
            }
        }
        function handleLocationError(browserHasGeolocation, infoWindow, pos) {
            infoWindow.setPosition(pos);
            infoWindow.setContent(browserHasGeolocation ?
                'Error: The Geolocation service failed.' :
                'Error: Your browser doesn\'t support geolocation.');
        };

        $.connection.hub.start().done(function () {
            chatInput.keydown(function (e) {

                if (e.which === 13) {
                    var text = chatInput.val();

                    //empty the textbox
                    chatInput.val("");

                    //send the message to the server
                    chat.server.sendMessage(userName, text);

                    //focus cursor on the textbox for easy chatting!
                    self.focus();
                }
            });
        });
    });
})(jQuery);

