WMCBApp.controller('ReviewCtrl', ["$scope", "$filter", "MatchEntryService",
    function ($scope, $filter, MatchEntryService) {
        $scope.Matches = '';
        $scope.Scores = [];
        $scope.HomeScores = [];
        $scope.AwayScores = [];
        $scope.selectedRow = -1;
        MatchEntryService.getMatches().then(function (data) {
            $scope.Matches = data;
        });
        $scope.ViewScore = function (m, index) {
            $scope.selectedRow = index;
            $scope.Scores = [];
            MatchEntryService.getMatchPlayerStats(m.Match.ID).then(function (data) {
                $scope.Scores.push.apply($scope.Scores, data);
                angular.forEach($scope.Scores, function (item) {
                    item.HowOutDesc = getHowOutDesc(item.HowOut);
                });
                $scope.HomeScores = $filter('filter')($scope.Scores, function (s) { return s.TeamId == m.Schedule.HomeId });
                $scope.AwayScores = $filter('filter')($scope.Scores, function (s) { return s.TeamId == m.Schedule.AwayId });
               
            });
            MatchEntryService.getMatchTeamStats(m.Match.ID).then(function (data) {
                $scope.HomeTeamStats = $filter('filter')(data, function (s) { return s.TeamId == m.Schedule.HomeId });
                $scope.AwayTeamStats = $filter('filter')(data, function (s) { return s.TeamId == m.Schedule.AwayId });
            });
        };
        $scope.Approve = function (m, index) {
            if ($scope.HomeScores.length > 0 && $scope.AwayScores.length > 0) {
                MatchEntryService.getMatchPlayerStats(m.Match.ID).then(function (data) {
                    $scope.Scores.push.apply($scope.Scores, data);
                    $scope.HomeScores = $filter('filter')($scope.Scores, function (s) { return s.TeamId == m.Schedule.HomeId });
                    $scope.AwayScores = $filter('filter')($scope.Scores, function (s) { return s.TeamId == m.Schedule.AwayId });
                });
                if ($scope.HomeScores.length > 0 && $scope.AwayScores.length > 0) {
                    MatchEntryService.approveMatchScore(m.Match).then(function (data) {
                        MatchEntryService.getMatches().then(function (data) {
                            $scope.Matches = data;
                        });
                        $scope.selectedRow = -1;

                    });
                }
            }
        }
        $scope.Reject = function (m, index) {
            MatchEntryService.rejectMatchScore(m.Match).then(function (data) {
                MatchEntryService.getMatches().then(function (data) {
                    $scope.Matches = data;
                });
                $scope.selectedRow = -1;

            });
        }
        $scope.counter = function (i) {
            return i + 1;
        }
        
    }
]);

function getHowOutDesc(id) {
    switch (id) {
        case 1: return "Bowled";
        case 2: return "Caught";
        case 3: return "Run Out";
        case 4: return "Not Out";
        case 5: return "Did not bat";
        default: return "Unknown";
    }
}