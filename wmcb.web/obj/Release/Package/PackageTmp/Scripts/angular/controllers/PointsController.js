WMCBApp.controller('PointsCtrl', ["$scope", "wmcbService", function ($scope, wmcbService) {
    $scope.northernDivPoints = "";
    $scope.southernDivPoints = "";
    $scope.northernConfPoints = "";
    $scope.southernConfPoints = "";
    wmcbService.getPoints(1).then(function (data) {
        $scope.northernDivPoints = data;
    });
    wmcbService.getPoints(2).then(function (data) {
        $scope.southernDivPoints = data;
    });
    wmcbService.getConfPoints(1).then(function (data) {
        $scope.northernConfPoints = data;
    });
    wmcbService.getConfPoints(2).then(function (data) {
        $scope.southernConfPoints = data;
    });
}]);