WMCBApp.controller('IndexCtrl', ["$scope", "wmcbService", function ($scope, wmcbService){
    $scope.latestNewsFeed = "";
    $scope.UpcomingGames = "";
    wmcbService.getLatestNewsFeed().then(function (data) {
        $scope.latestNewsFeed = data;
    });
    wmcbService.getUpcomingGames().then(function (data) {
        $scope.UpcomingGames = data;
    });
}]);