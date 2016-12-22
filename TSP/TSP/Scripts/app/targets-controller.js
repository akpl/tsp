angular.module('TSPApp', ['ngMap'])
    .controller('TargetsController', function ($scope, $http, NgMap) {
        $scope.targets = [];
        $scope.routingProgress = 0;
        $scope.route = [];
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
                        var geocodedName = results[0] ? results[0].formatted_address : 'Nieznana lokalizacja';
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
            var locationString = target.location.latitude + ', ' + target.location.longitude;
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
            $http.post('/api/Solution').success(function (data, status, headers, config) {
                $scope.routingProgress = 100;
                $scope.route = data;
            });
        };
    });