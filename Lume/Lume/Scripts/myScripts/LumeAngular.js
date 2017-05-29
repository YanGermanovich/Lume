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
function Initialize(N,E) {

    var myLatlng = new google.maps.LatLng(N, E);

    var mapOptions = {
        zoom: 15,
        center: myLatlng
    }
    var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

    // Place a draggable marker on the map
    marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
    });
}
angular.module('LumeAngular', ['ui.bootstrap', 'ngCookies'])
    .controller('MutualController', ["$scope", '$http', "$cookies", function ($scope, $http, $cookies) {
        $scope.UserName = "";
        if ($cookies.get('userName') == null) {
            $scope.loading = true;
            $http.get("../Account/GetName")
                .then(function (response) {
                    if (response.data.IsAuthenticated) {
                        $cookies.put('userName', response.data.UserName);
                        $scope.UserName = response.data.UserName;
                    }
                    $scope.loading = false;
                });
        }
        else {
            $scope.UserName = $cookies.get('userName');
        }
        $scope.Logout = function () {
            $http.get("Account/LogOut")
                .then(function (response) {
                    window.location = response.data.Url
                });
        }

    }])
    .controller('PopularController', ["$scope", '$http', function ($scope, $http) {
        $scope.popularImages = {};
        $http.get("../Home/GetPopular")
            .then(function (response) {
                $scope.popularImages = response.data;
            });
    }])
    .controller('LoginController', ["$scope", '$http', "$timeout", "$cookies", function ($scope, $http, $timeout, $cookies) {
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
                    $cookies.put('userName', response.data.UserName)
                    $timeout(function () {
                        $scope.success = false;
                        window.location = response.data.Url
                    }, 1000);
                }
                $scope.loading = false;
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
        $scope.coutrySelect = function () {
            $scope.cities = {};
            $scope.SelectedCity = false;
            for (i = 0; i < $scope.allCities.length; i++) {
                if ($scope.allCities[i].idCountry == $scope.SelectedCoutry.Id) {
                    $scope.cities[i] = $scope.allCities[i];
                    if (!$scope.SelectedCity) {
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
        }
        $scope.$on("fileProgress", function (e, progress) {
            $scope.progress = progress.loaded / progress.total;
        });
        $scope.AddImage = function () {
            var lat = marker.getPosition().lat();
            var lng = marker.getPosition().lng();
            fd = new FormData();
            $scope.loading = true;
            fd.append('file', $scope.file);
            fd.append('description', $scope.TempName);
            fd.append("N", lat)
            fd.append("E", lng)
            $http({
                url: "Home/UploadPhoto",
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
        $scope.validImage = false;
        $scope.onFileSelect = function ($event) {
            var ext = $event.target.files[0].name.match(/\.(.+)$/)[1];
            $scope.validImage = angular.lowercase(ext) === 'jpg';
        }
    }])
    .controller('ViewAllController', ["$scope", '$http', "$timeout", function ($scope, $http, $timeout) {
        $scope.loading = true;
        $scope.allImages = {};
        $http({
            method: 'GET',
            url: 'Home/GetAllImages',
        }).then(function (data) {
            $scope.allImages = data.data;
            $scope.loading = false;
        });
        $scope.deleteImage = function ($id) {
            $http({
                method: 'GET',
                url: 'Home/DeletePhoto?id=' + $id,
            }).then(function (data) {
                angular.forEach($scope.allImages, function (value, key) {
                    if (value.Id == $id) {
                        $scope.allImages.splice(key, 1);
                    }
                });
                $scope.modalShown = false;
            })
        }
        $scope.onlyMine = false;
        $scope.imageFilter = function ($image) {
            if (!$image.IsConfirmed) {
                return false;
            }
            if ($scope.onlyMine) {
                return $image.isMy;
            }
            return true;
        }
        $scope.onlyMineChange = function () {
            $scope.onlyMine = !$scope.onlyMine;
        }
        $scope.selecteImage = "";
        $scope.modalShown = false;
        $scope.toggleModal = function ($id) {
            angular.forEach($scope.allImages, function (value, key) {
                if (value.Id == $id) {
                    $scope.selecteImage = value;
                }
            });
            Initialize($scope.selecteImage.N, $scope.selecteImage.E);
            $scope.modalShown = !$scope.modalShown;
        };
        $scope.hideModal = function () {
            $scope.modalShown = !$scope.modalShown;
        }
        $scope.Edit_State = "Редакировать";
        $scope.isEdit = false;
        $scope.editClick = function () {
            if ($scope.Edit_State == "Редакировать") {
                $scope.isEdit = true;
                $scope.Edit_State = "Сохранить";
            }
            else {
                $scope.isEdit = false;
                $scope.Edit_State = "Редакировать"
                $http({
                    method: 'GET',
                    url: 'Home/UpdateImage?id=' + $scope.selecteImage.Id + "&desc=" + $scope.selecteImage.description,
                });
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
    .directive("ngUploadChange", function () {
        return {
            scope: {
                ngUploadChange: "&"
            },
            link: function ($scope, $element, $attrs) {
                $element.on("change", function (event) {
                    $scope.ngUploadChange({ $event: event })
                })
                $scope.$on("$destroy", function () {
                    $element.off();
                });
            }
        }
    })
    .directive('modalDialog', function () {
        return {
            restrict: 'E',
            scope: {
                show: '='
            },
            replace: true, // Replace with the template below
            transclude: true, // we want to insert custom content inside the directive
            link: function (scope, element, attrs) {
                scope.dialogStyle = {};
                if (attrs.width)
                    scope.dialogStyle.width = attrs.width;
                if (attrs.height)
                    scope.dialogStyle.height = attrs.height;
                scope.hideModal = function () {
                    scope.show = false;
                };
            },
            template: "<div class='ng-modal' ng-show='show'><div class='ng-modal-overlay' ng-click='hideModal()'></div><div class='ng-modal-dialog' ng-style='dialogStyle'><div class='ng-modal-close' ng-click='hideModal()'>X</div><div class='ng-modal-dialog-content' ng-transclude></div></div></div>"
        };
    })
    .factory("fileReader", ["$q", "$log", fileReader])
  
