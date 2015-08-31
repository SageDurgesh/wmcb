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
        }).success(deferred.resolve).error(function (data, status) { alert(JSON.stringify(data)); });
        return deferred.promise;
    };
    this.getAllGames = function () {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/schedule/all',
            method: "GET"
        }).success(deferred.resolve).error(function (data, status) { alert(JSON.stringify(data)); });
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
            url: '/Admin/AddUser',
            data: user,
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }
        });
    };
    this.registeruser = function (user) {
        return $http({
            url: '/Admin/RegisterUser',
            data: user,
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }
        });
    };
    this.resetpassword = function (email) {
        return $http({
            url: '/Admin/ResetPassword',
            method: "POST",
            data: JSON.stringify({email:email})
        });
    };
    this.getusers = function () {
        return $http({
            url: '/Account/Users',
            method: "GET",
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }
    this.updatemyprofile = function (user) {
        return $http({
            url: '/Account/UpdateMyProfile',
            method: "POST",
            data:user,
            headers: {
                'Content-Type': 'application/json'
            }
        });
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
WMCBApp.service("MatchEntryService", ["$http", "$q", function ($http, $q) {
    this.getMyMatches = function (teamID) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/myMatches/' + teamID,
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }
    this.getMatchesWithNoScore = function () {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/MatchesWithNoScore/',
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }
    this.getMatches = function () {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/Matches/',
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }
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
            url: '/wmcb/teamPlayers/' + id,
            method: "GET"
        }).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    };

    this.getMatchPlayerStats = function (id) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/matchPlayerStats/' + id,
            method: "GET"
        }).success(deferred.resolve).error(function (data, status) { alert(JSON.stringify(data)); });
        return deferred.promise;
    };

    this.getMatchTeamStats = function (id) {
        var deferred = $q.defer();
        $http({
            url: '/wmcb/matchTeamStats/' + id,
            method: "GET"
        }).success(deferred.resolve).error(function (data, status) { alert(JSON.stringify(data)); });
        return deferred.promise;
    };

    this.approveMatchScore = function (match) {
        return $http({
            url: '/wmcb/ApproveMatchScore',
            method: "POST",
            data: JSON.stringify(match)
        });
    };
    this.rejectMatchScore = function (match) {
        return $http({
            url: '/wmcb/RejectMatchScore',
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
            url: '/wmcb/SetTeamStats',
            method: "POST",
            data: JSON.stringify(teamStats)
        });
    };
    this.SavePlayerStats = function (playerStats) {
        return $http({
            url: '/wmcb/SavePlayerStats',
            method: "POST",
            data: JSON.stringify(playerStats)
        });
    }; 
}]);
