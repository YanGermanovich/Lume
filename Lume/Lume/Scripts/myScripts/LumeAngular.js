var fileReader = function ($q, $log) {

    var onLoad = function (reader, deferred, scope) {
        return function () {
            scope.$apply(function () {
                deferred.resolve(reader.result);
            });
        };
    };

    var onError = function (reader, deferred, scope) {
        return function () {
            scope.$apply(function () {
                deferred.reject(reader.result);
            });
        };
    };

    var onProgress = function (reader, scope) {
        return function (event) {
            scope.$broadcast("fileProgress",
                {
                    total: event.total,
                    loaded: event.loaded
                });
        };
    };

    var getReader = function (deferred, scope) {
        var reader = new FileReader();
        reader.onload = onLoad(reader, deferred, scope);
        reader.onerror = onError(reader, deferred, scope);
        reader.onprogress = onProgress(reader, scope);
        return reader;
    };

    var readAsDataURL = function (file, scope) {
        var deferred = $q.defer();

        var reader = getReader(deferred, scope);
        reader.readAsDataURL(file);

        return deferred.promise;
    };

    return {
        readAsDataUrl: readAsDataURL
    };
};

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
    .controller('UploadPhotoController', ["$scope", '$http', "$timeout", 'fileReader', function ($scope, $http, $timeout, fileReader) {
        $scope.uploadFile = function (files) {
            if (files[0].type.indexOf('image') != -1)
                $scope.file = files[0];
            else
                document.getElementById("uploadFile").value = null;
        };

        $scope.getFile = function () {
            fileReader.readAsDataUrl($scope.file, $scope)
                .then(function (result) {
                    $scope.TempSrc = result;
                });

            $scope.$on("fileProgress", function (e, progress) {
                $scope.progress = progress.loaded / progress.total;
            });
            $scope.AddImage = function () {
                fd = new FormData();
                $scope.loading = true;
                fd.append('file', $scope.file);
                fd.append('description', $scope.TempName);
                    $http({
                        url: "UploadPhoto",
                        method: 'POST',
                        data: fd,
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    })
                    .then(function () {
                        $scope.loading = false;
                    })
                $scope.TempName = "";
            }
        }
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
    .directive("ngFileSelect", function () {

        return {
            link: function ($scope, el) {

                el.bind("change", function (e) {

                    $scope.file = (e.srcElement || e.target).files[0];
                    $scope.getFile();
                })

            }

        }


    })
    .factory("fileReader", ["$q", "$log", fileReader])