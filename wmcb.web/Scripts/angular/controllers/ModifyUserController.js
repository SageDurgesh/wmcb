WMCBApp.controller('ModifyUserCtrl', ["$scope", "$filter", "wmcbService", "filteredListService", function ($scope, $filter, wmcbService, filteredListService) {
    $scope.users = '';
    wmcbService.getusers().then(function (d) {
        $scope.users = d.data;
    });
}]);