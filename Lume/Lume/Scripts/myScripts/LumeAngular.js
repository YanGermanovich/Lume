angular.module('LumeAngular', ['ui.bootstrap'])
    .controller('LoginController', ["$scope", '$http', "$timeout", function ($scope, $http, $timeout) {
        $scope.loginData = {};
        $scope.login = function () {
            $scope.loading = true;
            $http({
                method: 'POST',
                url: 'Login',
                data: $scope.loginData
            }).then(function (responce) {
                if (responce.data.error) {
                    $scope.error = responce.data.error;
                    $timeout(function () {
                        $scope.error = false;
                    }, 4000);
                }
                else {
                    $scope.success = responce.data.success;
                }
                $scope.loading = false;
            });
        }
    }])
    .controller('MutualController', ["$scope", function ($scope) {

    }])