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
var map;
function Initialize(N, E) {
    var myLatlng = new google.maps.LatLng(N, E);
    if (!map){
        var mapOptions = {
            zoom: 15,
            center: myLatlng
        }
        map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    }
    else
    {
        map.setCenter(myLatlng);
        marker.setMap(null);
    }
    // Place a draggable marker on the map
    marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
    });
}
angular.module('LumeAngular', ['ui.bootstrap', 'ngCookies'])
    .controller('MutualController', ["$scope", '$http', "$cookies", function ($scope, $http, $cookies) {
        $scope.GetNumber = function ($number) {
            var x = 0;
            while (x < $number) {
                x = x + 1;
            }
            return x;
        }
        $scope.UserName = false;
        $scope.isCompany = false;
        if ($cookies.get('userName') == null) {
            $scope.loading = true;
            $http.get("../Account/GetName")
                .then(function (response) {
                    $cookies.putObject('IsAuthenticated', response.data.IsAuthenticated)
                    if (response.data.IsAuthenticated) {
                        $cookies.put('userName', response.data.UserName);
                        $cookies.putObject('isCompany', response.data.isCompany)
                        $scope.UserName = response.data.UserName;
                    }
                    $scope.loading = false;
                });
        }
        else {
            if ($cookies.getObject('IsAuthenticated')) {
                $scope.UserName = $cookies.get('userName');
                $scope.isCompany = $cookies.getObject('isCompany');
            }
        }
        $scope.Logout = function () {
            $cookies.remove('userName');
            $cookies.remove('isCompany');
            $cookies.putObject('IsAuthenticated', false)
            $scope.UserName = false;
            $scope.isCompany = false;
            $http.get("../Account/LogOut")
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
                    $cookies.put('isCompany', response.data.isCompany)
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
        $scope.isNew = false;
        $scope.New = function ($b) {
            $scope.isNew = $b;
        }
        $scope.allEvents = {};
        $scope.events = {};
        $scope.types = {};
        $scope.SelectedEvents = {};
        $scope.SelectedType = {};
        $scope.allSelectedEvents = [];
        $scope.typeSelect = function ($type) {
            if (!!$type)
                $scope.SelectedType = $type;
            $scope.events = {};
            $scope.SelectedEvent= false;
            for (i = 0; i < $scope.allEvents.length; i++) {
                if ($scope.allEvents[i].TypeId == $scope.SelectedType.Id) {
                    $scope.events[i] = $scope.allEvents[i];
                    if (!$scope.SelectedEvent) {
                        $scope.SelectedEvent = $scope.events[i];
                    }
                }
            }
        }
        $scope.deleteEvent = function ($index) {
            $scope.allSelectedEvents.splice($index, 1);
        }
        $scope.Event = {};
        $scope.allSelectedEvents = [];
        $scope.addEvent = function ($newEvent, $newType) {
            $scope.Event = {};
            if ($scope.isNew) {
                $scope.Event['Id'] = -1;
                $scope.Event['Data'] = $newEvent;
                if (!!$newType) {
                    $scope.Event['TypeId'] = -1;
                    $scope.Event['TypeData'] = $newType;
                }
                else {
                    $scope.Event['TypeId'] = $scope.SelectedType.Id;
                    $scope.Event['TypeData'] = $scope.SelectedType.Data;
                }
            }
            else {
                $scope.Event['TypeId'] = $scope.SelectedType.Id;
                $scope.Event['TypeData'] = $scope.SelectedType.Data;
                $scope.Event['Data'] = $scope.SelectedEvent.Data;
                $scope.Event['Id'] = $scope.SelectedEvent.Id;
            }
            $scope.allSelectedEvents.push($scope.Event);
        }
        $http({
            method: 'GET',
            url: '../Home/GetAllEvents',
        }).then(function (response) {
            $scope.allEvents = response.data.events;
            $scope.types = response.data.types;
            $scope.events = $scope.allEvents;
            $scope.SelectedType = $scope.types[0];
            $scope.typeSelect();
            $scope.loading = false;
        });

        $scope.modalShown = false;
        $scope.toggleModal = function () {
            $scope.modalShown = !$scope.modalShown;
        };
        $scope.hideModal = function () {
            $scope.modalShown = !$scope.modalShown;
        }
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
            fd.append("Event", JSON.stringify($scope.allSelectedEvents[0]))
            $http({
                url: "../Home/UploadPhoto",
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
            url: '../Home/GetAllImages',
        }).then(function (data) {
            $scope.allImages = data.data;
            $scope.loading = false;
            });
        $scope.currentPage = 0;
        $scope.pageDec = function () {
            $scope.filtredImages = [];
            $scope.currentPage = $scope.currentPage - 1;
        }
        $scope.pageInc = function () {
            $scope.currentPage = $scope.currentPage + 1;
            $scope.filtredImages = [];
        }
        $scope.deleteImage = function ($id) {
            $http({
                method: 'GET',
                url: '../Home/DeletePhoto?id=' + $id,
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
        $scope.filtredImages = [];
        $scope.imageFilter = function ($image) {
            angular.forEach($scope.allImages, function (value, key) {
                if (value.Id == $image.Id) {
                    if (key == 0) {
                    $scope.Img = [];
                    $scope.filtredImages = [];
                    }
                }
            });
            var result = true;
            if (!$image.IsConfirmed) {
                result = false;
            }
            if ($scope.onlyMine) {
                result = $image.isMy;
            }
            if (result) {
                result = ( $scope.currentPage * 8  <= $scope.filtredImages.length) && ($scope.filtredImages.length < (($scope.currentPage +1 ) * 8 ));
                $scope.filtredImages.push(1);
            }
            return result;
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
    .controller('ViewAllStocksController', ["$scope", '$http', "$timeout", function ($scope, $http, $timeout) {
        $scope.loading = true;
        $scope.allStocks = {};
        $http({
            method: 'GET',
            url: '../Home/GetAllStocks',
        }).then(function (data) {
            $scope.allStocks = data.data;
            $scope.loading = false;
            $scope.filtredImages = [];
            })
        $scope.selecteStock = {};
        $scope.ImagesToView = null;
        $scope.modalShown = false;
        $scope.ImagesModalShown = false;
        $scope.toggleModal = function ($id) {
            angular.forEach($scope.allStocks, function (value, key) {
                if (value.Id == $id) {
                    $scope.selecteStock = value;
                }
            });
            $scope.modalShown = !$scope.modalShown;
        };
        $scope.toggleImagesModal = function ($images) {
            $scope.currentPage = 0;
            $scope.ImagesToView = $images;
            $scope.ImagesModalShown = !$scope.ImagesModalShown;
        }
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
                    url: '../Home/UpdateStock?id=' + $scope.selecteStock.Id + "&desc=" + $scope.selecteStock.Description + "&name=" + $scope.selecteStock.Name,
                });
            }
        }
        $scope.TakePart = function ($id) {
            $http({
                method: 'GET',
                url: '../Home/TakePart?id=' + $id,
            });
        }
        $scope.UsersModalShown = false;
        $scope.rowCollection = [];
        $scope.searchTextChange = function ($searchText) {
            $scope.searchText = $searchText;
        }
        $scope.searchText = "";
        $scope.MyTableFilter = function ($item) {
            if ($item.Email.indexOf($scope.searchText) == -1) {
                return false;
            }
            for (var i = 0; i < $scope.rowCollection.length; i++) {
                if ($scope.rowCollection[i].Email == $item.Email) {
                    return i < ($scope.currentPage + 1) * 3;
                }
            }
        }
        $scope.isOnlyMine = false;
        $scope.onlyMyStocks = function ()
        {
            $scope.isOnlyMine = !$scope.isOnlyMine;
        }
        $scope.filtredActions = [];
        $scope.currentPage = 0;
        $scope.pageDec = function () {
            $scope.filtredImages = [];
            $scope.currentPage = $scope.currentPage - 1;
        }
        $scope.pageInc = function () {
            $scope.currentPage = $scope.currentPage + 1;
            $scope.filtredImages = [];
        }
        $scope.StockFilter = function ($stock) {
            angular.forEach($scope.allStocks, function (value, key) {
                if (value.Id == $stock.Id) {
                    if (key == 0) {
                        $scope.filtredActions = [];
                    }
                }
            });
            var result = $stock.isTakeParticapent || !$scope.isOnlyMine;
            if (result) {
                result = ($scope.currentPage * 4  <= $scope.filtredActions.length) && ($scope.filtredActions.length < (($scope.currentPage + 1) * 4));
                $scope.filtredActions.push(1);
            }
            return result;
        }
        $scope.toggleUsersModal = function () {
            $scope.rowCollection = [];
            for (var i = 0; i < $scope.selecteStock.Participants.length; i++) {
                $scope.rowCollection.push({ Email: $scope.selecteStock.Participants[i].Email, Scanned: $scope.selecteStock.Participants[i].Scanned })
            }
            $scope.UsersModalShown = true;
        }
        $scope.currentPage = 0;
        $scope.currentUserPage = 0;
        $scope.pageUserInc = function () {
            $scope.currentUserPage++;
        }
        $scope.pageUserDec = function () {
            $scope.currentUserPage--;
        }
        $scope.pageInc = function () {
            $scope.currentPage++;
        }
        $scope.pageDec = function () {
            $scope.currentPage--;
        }
        $scope.ImagesToViewPages = function ($image) {
            if (!!$scope.ImagesToView) {
                for (var i = 0; i < $scope.ImagesToView.length; i++) {
                    if ($scope.ImagesToView[i].Id == $image.Id) {
                        return i == $scope.currentPage;
                    }
                }
            }
            return false;
        }
    }])
    .controller('UploadStockController', ["$scope", '$http', function ($scope, $http) {
        $scope.loading = true;
        $scope.isNew = false;
        $scope.stockToUpload = {};
        $scope.ImageForSelect = [];
        $scope.TypeForSelect = [];
        $http({
            method: 'GET',
            url: '../Home/GetAllMyImages'
        }).then(function (response) {
            for (i = 0; i < response.data.length; i++) {
                $scope.ImageForSelect.push({ Src: response.data[i].Src, Id: response.data[i].Id, Checked: false });
            }
        })
        $http({
            method: 'GET',
            url: '../Home/GetAllTypes'
        }).then(function (response) {
            for (i = 0; i < response.data.length; i++) {
                $scope.TypeForSelect.push({ Name: response.data[i].Name, Id: response.data[i].Id });
            }
            })
        $scope.New = function ($b) {
            $scope.isNew = $b;
        }
        $scope.allPrizes = {};
        $scope.prizes = {};
        $scope.types = {};
        $scope.SelectedPrize = {};
        $scope.SelectedType = {};
        $scope.allSelectedPrizes = [];
        $scope.typeSelect = function ($type) {
            if (!!$type)
                $scope.SelectedType = $type;
            $scope.prizes = {};
            $scope.SelectedPrize = false;
            for (i = 0; i < $scope.allPrizes.length; i++) {
                if ($scope.allPrizes[i].Id_PrizeType == $scope.SelectedType.Id) {
                    $scope.prizes[i] = $scope.allPrizes[i];
                    if (!$scope.SelectedPrize) {
                        $scope.SelectedPrize = $scope.prizes[i];
                    }
                }
            }
        }
        $scope.deletePrizre = function ($index) {
            $scope.allSelectedPrizes.splice($index, 1);
        }
        $scope.addPrize = function ($newPrize, $newType,$newPrizeData) {
            var newPrizeEnt = {};
            if ($scope.isNew) {
                newPrizeEnt['Id'] = -1;
                newPrizeEnt['Description'] = $newPrize;
                if (!!$newType) {
                    newPrizeEnt['id_PrizeType'] = -1;
                    newPrizeEnt['PrizeType'] = $newType;
                }
                else {
                    newPrizeEnt['id_PrizeType'] = $scope.SelectedType.Id;
                }
            }
            else {
                newPrizeEnt['id_PrizeType'] = $scope.SelectedType.Id;
                newPrizeEnt['Description'] = $scope.SelectedPrize.Description;
                newPrizeEnt['Id'] = $scope.SelectedPrize.Id;
            }
            newPrizeEnt['Data'] = $newPrizeData;   
            $scope.allSelectedPrizes.push(newPrizeEnt);
        }
        $http({
            method: 'GET',
            url: '../Home/GetAllPrizes',
        }).then(function (response) {
            $scope.allPrizes = response.data.prizes;
            $scope.types = response.data.types;
            $scope.prizes = $scope.allPrizes;
            $scope.SelectedType = $scope.types[0];
            $scope.typeSelect();
            $scope.loading = false;
        });

        $scope.modalShown = false;
        $scope.toggleModal = function () {
            $scope.modalShown = !$scope.modalShown;
        };
        $scope.hideModal = function () {
            $scope.modalShown = !$scope.modalShown;
        }

        $scope.AddStock = function () {
            $scope.stockToUpload.Image = [];

            for (i = 0; i < $scope.ImageForSelect.length; i++) {
                if ($scope.ImageForSelect[i].Checked) {
                    $scope.stockToUpload.Image.push({ Id: $scope.ImageForSelect[i].Id })
                }
            }
            if ($scope.stockToUpload.stockType == "")
                $scope.stockToUpload.stockType = null;
            $scope.stockToUpload.prizesToUpload = $scope.allSelectedPrizes;
            $http({
                method: 'POST',
                url: '../Home/UploadAction',
                data: $scope.stockToUpload
            })
        }

        //DatePicker
        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.dt = null;
        };

        $scope.inlineOptions = {
            customClass: getDayClass,
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };

        // Disable weekend selection
        function disabled(data) {
            var date = data.date,
                mode = data.mode;
            return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        }

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };

        $scope.open2 = function () {
            $scope.popup2.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.dt = new Date(year, month, day);
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.popup1 = {
            opened: false
        };

        $scope.popup2 = {
            opened: false
        };

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
            {
                date: tomorrow,
                status: 'full'
            },
            {
                date: afterTomorrow,
                status: 'partially'
            }
        ];

        function getDayClass(data) {
            var date = data.date,
                mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return '';
        }
        //DatePicker
    }])
    .controller('ProfileController', ["$scope", '$http', '$timeout', '$cookies', function ($scope, $http, $timeout, $cookies) {
       
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
        $scope.loading = true;
        $http({
            method: 'GET',
            url: '../Account/GetCities',
        }).then(function (response) {
            $scope.allCities = response.data.cities;
            $scope.countries = response.data.countries;
            $scope.cities = $scope.allCities;
            $http({
                method: 'GET',
                url: '../Home/GetUser'
            }).then(function (response) {
                $scope.profileData = response.data;
                var countryId = {};
                var selecCity = {};
                for (i = 0; i < $scope.allCities.length; i++) {
                    if ($scope.allCities[i].Id == $scope.profileData.Id_City)
                    {
                        countryId = $scope.allCities[i].idCountry;
                        selecCity = $scope.allCities[i];
                        break;
                    }
                }
                for (i = 0; i < $scope.countries.length; i++) {
                    if ($scope.countries[i].Id == countryId) {
                        $scope.SelectedCoutry = $scope.countries[i];
                        $scope.coutrySelect();
                        $scope.SelectedCity = selecCity;
                        break;
                    }
                }
                $scope.loading = false;
                })
            });
        $scope.profileEdit = function ()
        {
            $scope.loading = true;
            $scope.profileData['Id_City'] = $scope.SelectedCity.Id;
            $http({
                method: 'POST',
                url: '../Home/EditProfile',
                data: $scope.profileData,
            }).then(function (response) {
                if (response.data.Error) {
                    $scope.error = response.data.Error;
                    $timeout(function () {
                        $scope.error = false;
                    }, 4000);
                }
                else {
                    $scope.success = response.data.Success;
                    $timeout(function () {
                        $cookies.remove('userName');
                        $scope.success = false;
                        window.location = response.data.Url
                    }, 1000);
                }
                $scope.loading = false;
            })
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
  
