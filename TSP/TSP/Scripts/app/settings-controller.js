angular.module('TSPApp', [])
    .directive('convertToNumber', function() {
        return {
            require: 'ngModel',
            link: function(scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function(val) {
                    return val != null ? parseInt(val, 10) : null;
                });
                ngModel.$formatters.push(function(val) {
                    return val != null ? '' + val : null;
                });
            }
        };
    })
    .controller('SettingsController', function ($scope, $http) {
        var self = this;
        //self.geocoder = new google.maps.Geocoder;
        $scope.config = {};
        $scope.startPointCoords = "";

        $scope.load = function () {
            $http.get('/api/Settings', $scope.config)
            .success(function (data) {
                $scope.config = data;
            });

            var locationSearchInput = document.getElementById('startPoint');
            var autocomplete = new google.maps.places.Autocomplete(locationSearchInput);
            autocomplete.addListener('place_changed', function () {
                var place = autocomplete.getPlace();
                var name = place.name; //place.address_components[0];
                var location = place.geometry.location;
                var newTarget = {
                    name: name,
                    location: { 'latitude': location.lat(), 'longitude': location.lng() }
                };
                $scope.config.startingPoint = newTarget;
            });
        };

        $scope.save = function () {
            $http.put('/api/Settings', $scope.config)
            .success(function() {
                    
            });
        };
        
        $scope.reset = function () {
            $http.delete('/api/Settings')
            .success(function () {
                $scope.load();
            });
        };
    });