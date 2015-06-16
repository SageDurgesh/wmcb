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
    this.getConfPoints = function (id) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/points/conf/' + id,
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }
    this.addnewuser = function (user) {        
        return $http({
            url: '/wmcb/user/add',
            data: user,
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }
        });
    };
    //function handleError(response) {
    //    // The API response from the server should be returned in a
    //    // nomralized format. However, if the request was not handled by the
    //    // server (or what not handles properly - ex. server error), then we
    //    // may have to normalize it on our end, as best we can.
    //    if (response==undefined || response.data == undefined || !response.data) {
    //            return ($q.reject("An unknown error occurred."));
    //    }
    //}
    //// I transform the successful response, unwrapping the application data
    //// from the API response payload.
    //function handleSuccess(response) {
    //    return (response.data);
    //}
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
