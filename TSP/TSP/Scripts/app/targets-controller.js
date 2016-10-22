angular.module('TSPApp', [])
    .controller('TargetsController', function ($scope, $http) {
        $scope.targets = [];
        $scope.addTarget = function () {
            $http.post('/api/Targets', { name: $scope.location }).success(function () {
                $scope.location = "";
                $scope.getTargets();
            })
        };
        $scope.getTargets = function () {
            $http.get('/api/Targets')
                .success(function (data, status, headers, config) {
                    $scope.targets = data;
                });
        };
    });