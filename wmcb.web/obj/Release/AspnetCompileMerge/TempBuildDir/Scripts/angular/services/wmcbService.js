WMCBApp.service("wmcbService", ["$http", "$q", function ($http, $q) {
    this.getLatestNewsFeed = function () {
        var deferred = $q.defer();
        $http({
            url: '/api/ServiceAPI/LatestNewsFeed',
            method: "GET",
            params: { count: 1}
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };
    this.getNewsFeed = function () {
        var deferred = $q.defer();
        $http({
            url: '/api/ServiceAPI/News',
            method: "GET",
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };
    this.getAllTeams = function () {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/teams',
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };
    this.getAllGrounds = function () {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/grounds',
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };
    this.getSchedule = function () {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/schedule',
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };
    this.getUpcomingGames = function () {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/upcominggames/7',
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };
    this.getPoints = function (type) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/points/' + type,
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }
}]);
WMCBApp.service('filteredListService', function () {
    this.searched = function (valLists, toSearch) {
        return _.filter(valLists,
        function (i) {
            /* Search Text in all 3 fields */
            return searchUtil(i, toSearch);
        });
    };
 
    this.paged = function (valLists, pageSize) {
        retVal = [];
        for (var i = 0; i < valLists.length; i++) {
            if (i % pageSize === 0) {
                retVal[Math.floor(i / pageSize)] = [valLists[i]];
            } else {
                retVal[Math.floor(i / pageSize)].push(valLists[i]);
            }
        }
        return retVal;
    };
});
