WMCBApp.controller('NewsFeedAdminCtrl', ["$scope","wmcbService", "AdminService", function ($scope,wmcbService, adminService) {
    $scope.NewFeed = {
        Headline: '',
        Content:''
    };
    $scope.AllNewsFeed = '';

    $scope.message = '';
    wmcbService.getNewsFeed().then(function (data) {
        $scope.AllNewsFeed = data;
    });
    $scope.AddNewFeed = function (news) {
        adminService.AddNewsFeed(news).then(function (data) {
            if (data.d.Code == 0) {
                $scope.message = "Your new feed has been added successfully.";
                $scope.NewFeed.Headline = '';
                $scope.NewFeed.Content = '';
            }
            else
            {
                $scope.message = data.d.Message;
            }
        });
    }
}]);