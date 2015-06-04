WMCBApp.service("wmcbService", ["$http", "$q", function ($http, $q) {
    this.getLatestNewsFeed = function () {
        var deferred = $q.defer();
        $http({
            url: '/api/ServiceAPI/LatestNewsFeed',
            method: "GET",
            params: { count: 4 }
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



WMCBApp.service("MatchEntryService", ["$http", "$q", function ($http, $q) {
    

    this.getMatch = function (matchId) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/match?matchId='+matchId,
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };

    this.getCurrentTeam = function (userName) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/userTeam?userName=' + userName,
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };

    this.getTeamPlayers = function (id) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/teamPlayers?teamId=' + id,
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };

    this.getMatchPlayerStats = function (id) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/matchPlayerStats?matchId=' + id,
            method: "GET"
        }).success(deferred.resolve).error(function (data, status) { alert(JSON.stringify(data)); });
        return deferred.promise;
    };

    this.getMatchTeamStats = function (id) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/matchTeamStats?matchId=' + id,
            method: "GET"
        }).success(deferred.resolve).error(function (data, status) { alert(JSON.stringify(data)); });
        return deferred.promise;
    };

    this.CompleteMatchScore = function (match) {
        return $http({
            url: '/wmcb/completeMatchScore',
            method: "POST",
            data: JSON.stringify(match)
        });
    };

    this.setPlayerStats = function (playerStats) {
        return $http({
            url: '/wmcb/setPlayerStats',
            method: "POST",
            data: JSON.stringify(playerStats)
        });
    };

    this.setTeamStats = function (teamStats) {
        return $http({
            url: '/wmcb/setTeamStats',
            method: "POST",
            data: JSON.stringify(teamStats)
        });
    };
}]);
