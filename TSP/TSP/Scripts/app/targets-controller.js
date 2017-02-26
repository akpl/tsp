function findAddressComponent(address, component) {
    var components = address.address_components;
    for (var i = 0; i < components.length; ++i) {
        if (components[i].types.length >= 1 && components[i].types[0] == component) {
            return components[i].short_name;
        }
    }

    return '';
}

function findNameForPlace(geocodeResults) {
    if (!geocodeResults[0])
        return 'Nieznana lokalizacja';

    var address = geocodeResults[0];
    var street = findAddressComponent(address, 'route');
    var number = findAddressComponent(address, 'street_number');

    var result = street;
    if (number)
        result += ' ' + number;

    return result ? result : 'Nieznana lokalizacja';
}

angular.module('TSPApp', ['ngMap'])
    .controller('TargetsController', function ($scope, $http, NgMap) {
        $scope.targets = [];
        $scope.routingProgress = 0;
        $scope.route = [];
        $scope.wayPoints = [];

        var self = this;
        self.geocoder = new google.maps.Geocoder;
        NgMap.getMap().then(function (map) {
            self.map = map;
        });
        $scope.addTarget = function () {
            $http.post('/api/Targets', { name: $scope.location })
                .success(function() {
                    $scope.location = "";
                    $scope.getTargets();
                });
        };
        $scope.addTargetOnMap = function (e) {
            self.geocoder.geocode({ 'location': e.latLng },
                function(results, status) {
                    if (status === 'OK') {
                        var geocodedName = findNameForPlace(results);
                        var newTarget = {
                            name: geocodedName,
                            location: { 'latitude': e.latLng.lat(), 'longitude': e.latLng.lng() }
                        };
                        $http.post('/api/Targets', newTarget)
                            .success(function () {
                                $scope.getTargets();
                            });
                    } else {
                        window.alert('Odnalezienie adresu nie powiodło się: ' + status);
                    }
                });
            
        };
        $scope.deleteTarget = function (target) {
            var locationString = target.id;
            $http.delete('/api/Targets/' + locationString).success(function () {
                $scope.getTargets();
            });
        };
        $scope.getTargets = function () {
            $http.get('/api/Targets').success(function (data, status, headers, config) {
                $scope.targets = data;
            });
        };
        $scope.findRoute = function () {
            $scope.routingProgress = 50;
            $http.post('/api/Solution')
                .success(function (data) {
                    $scope.routingProgress = 75;

                    var firstElement = data.route.length > 0 ? data.route[0] : null;
                    data.start = firstElement.location.latitude + ',' + firstElement.location.longitude;
                    var lastElement = data.route.length > 1 ? data.route[data.route.length - 1] : firstElement;
                    data.end = lastElement.location.latitude + ',' + lastElement.location.longitude;
                
                    var wayPointsCount = data.route.length - 1;
                    var wayPoints = [];
                    for (var point = 1; point < wayPointsCount; ++point) {
                        wayPoints.push({
                            location: {
                                lat: data.route[point].location.latitude,
                                lng: data.route[point].location.longitude
                            },
                            stopover: true
                        });
                    }

                    data.wayPoints = wayPoints;
                    $scope.route = data;
                    $scope.routingProgress = 100;
                })
                .error(function (data) {
                    $scope.routingProgress = 100;
                    var error = ('exceptionMessage' in data)
                        ? data.exceptionMessage
                        : 'Nieznany błąd serwera.';
                    window.alert("Serwer aplikacji zwrócił błąd:\n\n" + error);
                });
        };
    });