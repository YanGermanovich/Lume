angular.module('LumeAngular', ['ui.bootstrap'])
    .controller('LoginController', ["$scope", '$http', "$timeout", function ($scope, $http, $timeout) {
        $scope.loginData = {};
        $scope.login = function () {
            $scope.loading = true;
            $http({
                method: 'POST',
                url: 'Login',
                data: $scope.loginData
            }).then(function (response) {
                if (response.data.error) {
                    $scope.error = response.data.error;
                    $timeout(function () {
                        $scope.error = false;
                    }, 4000);
                }
                else {
                    $scope.success = response.data.success;
                    $timeout(function () {
                        $scope.success = false;
                        window.location = response.data.Url
                    }, 1000);
                }
                $scope.loading = false;
            });
        }
    }])
    .controller('MutualController', ["$scope", '$http', function ($scope, $http) {
        $scope.Logout = function () {
            $http.get("Account/LogOut")
                .then(function (response) {
                    window.location = response.data.Url
                });
        }
    }])
    .controller('RegisterController', ["$scope", '$http', "$timeout", function ($scope, $http, $timeout) {
        $scope.loading = true;
        $scope.registerData = {};
        $scope.register = function () {
            $scope.loading = true;
            $scope.registerData['Id_City'] = $scope.SelectedCity.Id;
            $http({
                method: 'POST',
                url: 'Register',
                data: $scope.registerData
            }).then(function (response) {
                if (response.data.error) {
                    $scope.error = response.data.error;
                    $timeout(function () {
                        $scope.error = false;
                    }, 4000);
                }
                else {
                    $scope.success = response.data.success;
                    $timeout(function () {
                        $scope.success = false;
                        window.location = response.data.Url
                    }, 1000);
                }
                $scope.loading = false;
            });
        }
        $scope.allCities = {};
        $scope.cities = {};
        $scope.countries = {};
        $scope.SelectedCity = {};
        $scope.SelectedCoutry = {};
        $scope.coutrySelect = function ()
        {
            $scope.cities = {};
            $scope.SelectedCity = false;
            for (i = 0; i < $scope.allCities.length; i++) {
                if ($scope.allCities[i].idCountry == $scope.SelectedCoutry.Id) {
                    $scope.cities[i] = $scope.allCities[i];
                    if (!$scope.SelectedCity)
                    {
                        $scope.SelectedCity = $scope.cities[i];
                    }
                }
            }
             

        }
        $http({
            method: 'GET',
            url: 'GetCities',
        }).then(function (response) {
            $scope.allCities = response.data.cities;
            $scope.countries = response.data.countries;
            $scope.cities = $scope.allCities;
            $scope.SelectedCoutry = $scope.countries[0];
            $scope.coutrySelect();
            $scope.loading = false;
        });
    }])

    .directive("ngMatch", ['$parse', function ($parse) {

        var directive = {
            link: link,
            restrict: 'A',
            require: '?ngModel'
        };
        return directive;

        function link(scope, elem, attrs, ctrl) {
            // if ngModel is not defined, we don't need to do anything
            if (!ctrl) return;
            if (!attrs["ngMatch"]) return;

            var firstPassword = $parse(attrs["ngMatch"]);

            var validator = function (value) {
                var temp = firstPassword(scope),
                    v = value === temp;
                ctrl.$setValidity('match', v);
                return value;
            }

            ctrl.$parsers.unshift(validator);
            ctrl.$formatters.push(validator);
            attrs.$observe("ngMatch", function () {
                validator(ctrl.$viewValue);
            });

        }
    }])