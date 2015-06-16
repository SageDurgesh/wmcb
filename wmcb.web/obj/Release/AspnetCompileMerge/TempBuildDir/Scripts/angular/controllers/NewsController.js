WMCBApp.controller('NewsCtrl', ["$scope", "wmcbService", function ($scope, wmcbService) {
    $scope.News = "";
    wmcbService.getNewsFeed().then(function (data) {
        $scope.News = data;
    });
}]);