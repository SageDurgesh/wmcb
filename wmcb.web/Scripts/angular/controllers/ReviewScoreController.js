WMCBApp.controller('ReviewCtrl', ["$scope", "$filter", "MatchEntryService",
    function ($scope, $filter, MatchEntryService) {
        $scope.Matches = '';
        $scope.Scores = [];
        $scope.HomeScores = [];
        $scope.AwayScores = [];
        $scope.BattingInn1 = [];
        $scope.BowlingInn1 = [];
        $scope.BattingInn2 = [];
        $scope.BowlingInn2 = [];
        $scope.Inn1BattingTeam = '';
        $scope.ExtraInn1 = [];
        $scope.ExtraInn2 = [];
        $scope.Inn2BattingTeam = '';
        $scope.Inn1BowlingTeam = '';
        $scope.Inn2BowlingTeam = '';
        $scope.Inn1TotalScore = 0;
        $scope.Inn2TotalScore = 0;
        $scope.selectedRow = -1;
        MatchEntryService.getMatches().then(function (data) {
            $scope.Matches = data;
        });
        $scope.ViewScore = function (m, index) {
            $scope.selectedRow = index;
            $scope.Scores = [];
            $scope.HomeScores = [];
            $scope.AwayScores = [];
            $scope.HomeTeamStats = [];
            $scope.AwayTeamStats = [];
            $scope.BattingInn1 = [];
            $scope.BattingInn2 = [];
            $scope.BowlingInn1 = [];
            $scope.BowlingInn2 = [];
            MatchEntryService.getMatchPlayerStats(m.Match.ID).then(function (data) {
                $scope.Scores.push.apply($scope.Scores, data);
                MatchEntryService.getMatchTeamStats(m.Match.ID).then(function (data) {
                    $scope.HomeTeamStats = $filter('filter')(data, function (s) { return s.TeamId == m.Schedule.HomeId });
                    $scope.AwayTeamStats = $filter('filter')(data, function (s) { return s.TeamId == m.Schedule.AwayId });
                });
                //angular.forEach($scope.Scores, function (item) {
                //    //if (item.HowOut != null) {
                    //    var desc = item.HowOutDesc;
                    //    if (item.HowOutDesc == "b") {
                    //        desc = item.HowOutDesc + " " + item.BowlerName;
                    //    }
                    //    else if (item.HowOutDesc == "lbw" || item.HowOutDesc == "st" || item.HowOutDesc == "b") {
                    //        desc = item.HowOutDesc + "&b " + item.BowlerName;
                    //    }
                    //    else if (item.HowOutDesc == "c" || item.HowOutDesc == "run out") {
                    //        if (item.BowlerId == item.FielderId) {
                    //            desc = item.HowOutDesc + "&b " + item.BowlerName;
                    //        }
                    //        else {
                    //            desc = item.HowOutDesc + " " + item.FielderName + ",<br/> b " + item.BowlerName;
                    //        }
                    //    }
                    //    item.HowOutDesc = desc;
                    //}
                //});
                $scope.HomeScores = $filter('filter')($scope.Scores, function (s) { return s.TeamId == m.Schedule.HomeId });
                $scope.AwayScores = $filter('filter')($scope.Scores, function (s) { return s.TeamId == m.Schedule.AwayId });

                var scoreInn1 = [];
                var scoreInn2 = [];
                if (m.Match.TeamBattedFirst == m.Schedule.AwayId) {
                    $scope.Inn1BattingTeam = m.Schedule.Away;
                    $scope.Inn2BattingTeam = m.Schedule.Home;
                    $scope.Inn1BowlingTeam = m.Schedule.Home;
                    $scope.Inn2BowlingTeam = m.Schedule.Away;
                    scoreInn1 = $scope.AwayScores;
                    scoreInn2 = $scope.HomeScores;
                    $scope.Inn1TotalScore = m.Match.AwayTeamScore;
                    $scope.Inn2TotalScore = m.Match.HomeTeamScore;
                }
                else {
                    $scope.Inn1BattingTeam = m.Schedule.Home;
                    $scope.Inn2BattingTeam = m.Schedule.Away;
                    $scope.Inn1BowlingTeam = m.Schedule.Away;
                    $scope.Inn2BowlingTeam = m.Schedule.Home;
                    scoreInn1 = $scope.HomeScores;
                    scoreInn2 = $scope.AwayScores;
                    $scope.Inn1TotalScore = m.Match.HomeTeamScore;
                    $scope.Inn2TotalScore = m.Match.AwayTeamScore;
                }
               
                //Inning 1
                angular.forEach(scoreInn1, function (s) {
                    if (s.HowOut != null && s.HowOut > 0) {
                        $scope.BattingInn1.push(s);
                    }
                    if (s.OversBowled != null && s.OversBowled != -1) {
                        $scope.BowlingInn2.push(s);
                    }
                });
                //Inning 2
                angular.forEach(scoreInn2, function (s) {
                    if (s.HowOut != null && s.HowOut > 0) {
                        $scope.BattingInn2.push(s);
                    }
                    if (s.OversBowled != null && s.OversBowled != -1) {
                        $scope.BowlingInn1.push(s);
                    }
                });
                $scope.$watchCollection('[HomeTeamStats,AwayTeamStats]', function (newValues, oldValues) {

                    if (m.Match.TeamBattedFirst == m.Schedule.AwayId) {                        
                        $scope.ExtraInn1 = $scope.AwayTeamStats != undefined && $scope.AwayTeamStats.length > 0 ? $scope.AwayTeamStats[0] : '';
                        $scope.ExtraInn2 = $scope.HomeTeamStats != undefined && $scope.HomeTeamStats.length > 0 ? $scope.HomeTeamStats[0] : '';
                    }
                    else {
                        $scope.ExtraInn1 = $scope.HomeTeamStats != undefined && $scope.HomeTeamStats.length > 0 ? $scope.HomeTeamStats[0] : '';
                        $scope.ExtraInn2 = $scope.AwayTeamStats != undefined && $scope.AwayTeamStats.length > 0 ? $scope.AwayTeamStats[0] : '';
                    }

                });
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
        case 1: return "b";
        case 2: return "c";
        case 3: return "st";
        case 4: return "run out";
        case 5: return "not out";
        case 6: return "dnb";
        case 7: return "lbw";
        default: return "Unknown";
    }
}