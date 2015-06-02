WMCBApp.controller('TeamCtrl', ["$scope", "wmcbService", function ($scope, wmcbService) {
    $scope.teams = "";
    wmcbService.getAllTeams().then(function (data) {
        $scope.teams = data;
    });
}]);