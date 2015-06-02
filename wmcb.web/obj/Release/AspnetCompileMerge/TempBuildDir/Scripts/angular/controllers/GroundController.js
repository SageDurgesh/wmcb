WMCBApp.controller('GroundCtrl', ["$scope", "wmcbService", function ($scope, wmcbService) {
    $scope.grounds = "";
    wmcbService.getAllGrounds().then(function (data) {
        $scope.grounds = data;
    });
}]);