WMCBApp.service("AdminService", ["$http", "$q", function ($http, $q) {
    this.AddNewsFeed = function (news) {
        return $http({
            url: '/Admin/AddNewsFeed',
            method: "POST",
            data: JSON.stringify(news)
        });
    };
}]);