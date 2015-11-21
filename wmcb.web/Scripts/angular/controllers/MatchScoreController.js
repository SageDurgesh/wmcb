
WMCBApp.controller('MatchScoreCtrl', ["$scope", "$timeout", "$filter", "$location", "MatchEntryService",
    function ($scope, $timeout, $filter, $location, MatchEntryService) {
        $scope.OutList = [
          { Id: 1, Type: "Bowled", Code: "b" },
          { Id: 2, Type: "Caught", Code: "c" },
          { Id: 3, Type: "Stumped", Code: "st" },
          { Id: 4, Type: "Run Out", Code: "run out" },
          { Id: 5, Type: "Not Out", Code: "not out" },
          { Id: 6, Type: "Leg Before Wicket", Code: "lbw" },
          { Id: 7, Type: "Did not bat", Code: "dnb" }];
        $scope.hasPermission = '';
        $scope.HomePlayers = '';
        $scope.AwayPlayers = '';
        $scope.TeamID = '';
        $scope.TossWon = '';
        $scope.BattedFirst = '';
        $scope.BattingScore = [battingscore];
        $scope.BowlingScore = [bowlingscore];
        $scope.AwayBattingScore = [awaybattingscore];
        $scope.AwayBowlingScore = [awaybowlingscore];
        $scope.SelectedMatchText = 'Select a Match';
        $scope.SelectedMatch = '';
        $scope.AgainstTeams = [];
        $scope.done = false;
        $scope.HTByes = 0;
        $scope.HTLegByes = 0;
        $scope.HTWides = 0;
        $scope.HTNoBalls = 0;
        $scope.HTPenaltyRuns = 0;
        $scope.ATByes = 0;
        $scope.ATLegByes = 0;
        $scope.ATWides = 0;
        $scope.ATNoBalls = 0;
        $scope.ATPenaltyRuns = 0;
        $scope.Match = {};
        $scope.HomeTeamId = null;
        $scope.AwayTeamId = null;
        $scope.IsLeagueOfficial = false;
        $scope.HTBattingRuns = 0;
        $scope.HTExtras = 0;
        $scope.ATBattingRuns = 0;
        $scope.ATExtras = 0;
        $scope.TeamStats = new Array();

        $scope.init = function (hasPermission, isAdmin, TeamID) {
            if (isAdmin) {
                MatchEntryService.getMatchesWithNoScore().then(function (data) {
                    $scope.Matches = data;
                }
              );
            }
            else {
                $scope.TeamId = TeamID;
                $scope.hasPermission = hasPermission;
                MatchEntryService.getMyMatches(TeamID).then(function (data) {
                    $scope.Matches = data;
                    angular.forEach(data, function (item) {
                        if (item.HomeId == TeamID) {
                            var sch = {
                                ID: item.ID,
                                AgainstTeamId: item.AwayId,
                                AgainstTeamName: item.Away,
                                Week: item.Week,
                                DateTime: item.DateTime,
                                Match: item
                            };
                            $scope.AgainstTeams.push(sch);
                        }
                        else if (item.AwayId == TeamID) {
                            var sch = {
                                ID: item.ID,
                                AgainstTeamId: item.HomeId,
                                AgainstTeamName: item.Home,
                                Week: item.Week,
                                DateTime: item.DateTime,
                                Match: item
                            };
                            $scope.AgainstTeams.push(sch);
                        }
                    });
                });
                MatchEntryService.getTeamPlayers(TeamID).then(function (data) {
                    $scope.HomePlayers = data;
                });
            }
        };
        $scope.SelectMatchAsAdmin = function (match) {
            if (match) {
                $scope.SelectedMatch = match;
                $scope.TeamId = match.HomeId;
                var dt = $filter('date')(match.DateTime, 'EEE, MM/dd');
                $scope.SelectedMatchText = match.Home + " Vs " + match.Away + " - " + dt;
                MatchEntryService.getTeamPlayers(match.HomeId).then(function (data) {
                    $scope.HomePlayers = data;
                });
                MatchEntryService.getTeamPlayers(match.AwayId).then(function (data) {
                    $scope.AwayPlayers = data;
                });
            }
        }
        $scope.SelectMatch = function (match) {
            if (match) {
                $scope.SelectedMatch = match.Match;
                var dt = $filter('date')(match.DateTime, 'EEE, MM/dd');
                $scope.SelectedMatchText = match.HomeTeamName + " Vs " + match.AgainstTeamName + " - " + dt;
                MatchEntryService.getTeamPlayers(match.AgainstTeamId).then(function (data) {
                    $scope.AwayPlayers = data;
                });
            }
        };
        //inside your controller
        $scope.clearUnselected = function (index) {
            $timeout(function () {
                if ($scope.BattingScore[index].Batsman == undefined) {   //the model was not set by the typeahead
                    $scope.BattingScore[index].Batsman = '';
                }
                if ($scope.BattingScore[index].Bowler == undefined) {   //the model was not set by the typeahead
                    $scope.BattingScore[index].Bowler = '';
                }
                if ($scope.BattingScore[index].HowOut == undefined) {   //the model was not set by the typeahead
                    $scope.BattingScore[index].HowOut = '';
                }
            }, 100);    //a 250 ms delay should be safe enough
        }
        $scope.SubmitMatchScore = function (isAdmin) {
            var awayscoresubmission = false;
            var homescoresubmission = false;
            var homeplayerstats = [];
            var awayplayerstats = [];
            var found = false;
            var i = 1, j = 1;
            angular.forEach($scope.BattingScore, function (item) {
                if (item.Batsman != "" && item.Batsman.ID != undefined) {
                    var s = {
                        TeamId: $scope.SelectedMatch.HomeId,
                        MatchId: $scope.SelectedMatch.ID,
                        PlayerId: item.Batsman.ID,
                        BattingRuns: item.Runs != undefined ? item.Runs : 0,
                        BallsFaced: item.Balls != undefined ? item.Balls : 0,
                        Fours: item.Fours != undefined ? item.Fours : 0,
                        Sixes: item.Sixes != undefined ? item.Sixes : 0,
                        HowOut: item.HowOut != undefined ? item.HowOut.Id : -1,
                        Fielder: item.Fielder != undefined ? item.Fielder.ID : null,
                        BowlerNumber: -1,
                        OversBowled: -1,
                        Wickets: 0,
                        MaidenOvers: 0,
                        BowlingRuns: 0,
                        Wide: 0,
                        noBalls: 0,
                        Bowler: item.Bowler != undefined ? item.Bowler.ID : null,
                        FOWRuns: item.FOWRuns != undefined ? item.FOWRuns : 0,
                        WicketNumber: item.WicketNumber != undefined ? item.WicketNumber : 0
                    };
                    if (item.Bowler != undefined) {
                        s.Bowler = item.Bowler.ID;
                    }
                    if (item.HowOut != undefined) {
                        s.HowOut = item.HowOut.Id;
                    }
                    if (item.Fielder != undefined) {
                        s.Fielder = item.Fielder.ID;
                    }
                    $scope.HTBattingRuns = parseInt($scope.HTBattingRuns * 1) + parseInt(s.BattingRuns);
                    homeplayerstats.push(s);
                }
            });
            angular.forEach($scope.BowlingScore, function (item) {
                if (item.Bowler != "" && item.Bowler.ID != undefined) {
                    found = false;
                    var existPlayerID = item.Bowler.ID;
                    angular.forEach(homeplayerstats, function (player) {
                        if (existPlayerID == player.PlayerId) {
                            found = true;
                            player.OversBowled = item.Overs;
                            player.Wickets = item.Wickets;
                            player.MaidenOvers = item.Maiden;
                            player.BowlingRuns = item.RunsGiven;
                            player.Wide = item.Wide;
                            player.NoBalls = item.NoBalls;
                            player.BowlerNumber = i;
                            i++;
                        }
                    });
                    if (!found) {
                        var s = {
                            TeamId: $scope.SelectedMatch.HomeId,
                            MatchId: $scope.SelectedMatch.ID,
                            PlayerId: item.Bowler.ID,
                            BattingRuns: 0,
                            BallsFaced: 0,
                            HowOut: -1,
                            Fielder: '',
                            Fours: 0,
                            Sixes: 0,
                            Bowler: '',
                            BowlerNumber: i,
                            OversBowled: item.Overs != undefined ? item.Overs : -1,
                            Wickets: item.Wickets != undefined ? item.Wickets : 0,
                            MaidenOvers: item.Maiden != undefined ? item.Maiden : 0,
                            BowlingRuns: item.RunsGiven != undefined ? item.RunsGiven : 0,
                            Wide: item.Wide != undefined ? item.Wide : 0,
                            noBalls: item.NoBalls != undefined ? item.NoBalls : 0,
                            FOWRuns: 0,
                            WicketNumber: 0
                        };
                        i++;
                        homeplayerstats.push(s);
                    }
                }
            });
            if (isAdmin) {
                angular.forEach($scope.AwayBattingScore, function (item) {
                    if (item.Batsman != "" && item.Batsman.ID != undefined) {
                        var s = {
                            TeamId: $scope.SelectedMatch.AwayId,
                            MatchId: $scope.SelectedMatch.ID,
                            PlayerId: item.Batsman.ID,
                            BattingRuns: item.Runs != undefined ? item.Runs : 0,
                            BallsFaced: item.Balls != undefined ? item.Balls : 0,
                            Fours: item.Fours != undefined ? item.Fours : 0,
                            Sixes: item.Sixes != undefined ? item.Sixes : 0,
                            HowOut: item.HowOut != undefined ? item.HowOut.Id : -1,
                            Fielder: item.Fielder != undefined ? item.Fielder.ID : null,
                            BowlerNumber: -1,
                            OversBowled: -1,
                            Wickets: 0,
                            MaidenOvers: 0,
                            BowlingRuns: 0,
                            Wide: 0,
                            noBalls: 0,
                            Bowler: item.Bowler != undefined ? item.Bowler.ID : null,
                            FOWRuns: item.FOWRuns != undefined ? item.FOWRuns : 0,
                            WicketNumber: item.WicketNumber != undefined ? item.WicketNumber : 0
                        };
                        $scope.ATBattingRuns = parseInt($scope.ATBattingRuns * 1) + parseInt(s.BattingRuns);
                        awayplayerstats.push(s);
                    }
                });
                angular.forEach($scope.AwayBowlingScore, function (item) {
                    if (item.Bowler != "" && item.Bowler.ID != undefined) {
                        found = false;
                        var existPlayerID = item.Bowler.ID;
                        angular.forEach(awayplayerstats, function (player) {
                            if (existPlayerID == player.PlayerId) {
                                player.OversBowled = item.Overs != undefined ? item.Overs : -1;
                                player.Wickets = item.Wickets != undefined ? item.Wickets : 0;
                                player.MaidenOvers = item.Maiden != undefined ? item.Maiden : 0;
                                player.BowlingRuns = item.RunsGiven != undefined ? item.RunsGiven : 0;
                                player.Wide = item.Wide != undefined ? item.Wide : 0;
                                player.NoBalls = item.NoBalls != undefined ? item.NoBalls : 0;
                                player.BowlerNumber = j;
                                j++;
                                found = true;
                            }
                        });
                        if (!found) {
                            var s = {
                                TeamId: $scope.SelectedMatch.AwayId,
                                MatchId: $scope.SelectedMatch.ID,
                                PlayerId: item.Bowler.ID,
                                BattingRuns: -1,
                                BallsFaced: -1,
                                HowOut: -1,
                                Bowler: '',
                                Fielder: '',
                                Fours: 0,
                                Sixes: 0,
                                BowlerNumber: j,
                                OversBowled: item.Overs != undefined ? item.Overs : -1,
                                Wickets: item.Wickets != undefined ? item.Wickets : 0,
                                MaidenOvers: item.Maiden != undefined ? item.Maiden : 0,
                                BowlingRuns: item.RunsGiven != undefined ? item.RunsGiven : 0,
                                Wide: item.Wide != undefined ? item.Wide : 0,
                                noBalls: item.NoBalls != undefined ? item.NoBalls : 0,
                                FOWRuns: 0,
                                WicketNumber: 0
                            };
                            j++;
                            awayplayerstats.push(s);
                        }
                    }
                });
            }
            if (homeplayerstats.length > 0)
                MatchEntryService.SavePlayerStats(homeplayerstats);

            if (awayplayerstats.length > 0) {
                MatchEntryService.SavePlayerStats(awayplayerstats);
            }
            var tosswinningnteam = '';
            var battingteam = '';
            if ($scope.TossWon == 1)
                tosswinningnteam = $scope.SelectedMatch.HomeId;
            else
                tosswinningnteam = $scope.SelectedMatch.AwayId;

            if ($scope.BattedFirst == 1)
                battingteam = $scope.SelectedMatch.HomeId;
            else
                battingteam = $scope.SelectedMatch.AwayId;

            $scope.HTExtras = $scope.HTWides * 1 + $scope.HTByes * 1 + $scope.HTLegByes * 1 + $scope.HTNoBalls * 1 + $scope.HTPenaltyRuns * 1;
            var homeTeamStats = {
                "MatchId": $scope.SelectedMatch.ID,
                "TeamId": $scope.SelectedMatch.HomeId,
                "TeamWonToss": tosswinningnteam,
                "TeamBattedFirst": battingteam,
                "Wides": $scope.HTWides,
                "Byes": $scope.HTByes,
                "LegByes": $scope.HTLegByes,
                "NoBalls": $scope.HTNoBalls,
                "PenaltyRuns": $scope.HTPenaltyRuns,
                "TeamScore": (parseInt($scope.HTBattingRuns) * 1) + (parseInt($scope.HTExtras) * 1)
            };

            if (isAdmin) {
                //AwayTeam Score
                $scope.ATExtras = $scope.ATWides * 1 + $scope.ATByes * 1 + $scope.ATLegByes * 1 + $scope.ATNoBalls * 1 + $scope.ATPenaltyRuns * 1;
                var awayTeamStats = {
                    "MatchId": $scope.SelectedMatch.ID,
                    "TeamId": $scope.SelectedMatch.AwayId,
                    "TeamWonToss": tosswinningnteam,
                    "TeamBattedFirst": battingteam,
                    "Wides": $scope.ATWides,
                    "Byes": $scope.ATByes,
                    "LegByes": $scope.ATLegByes,
                    "NoBalls": $scope.ATNoBalls,
                    "PenaltyRuns": $scope.ATPenaltyRuns,
                    "TeamScore": (parseInt($scope.ATBattingRuns) * 1) + (parseInt($scope.ATExtras) * 1)
                };
                MatchEntryService.setTeamStats(homeTeamStats).then(function () {
                    MatchEntryService.setTeamStats(awayTeamStats).then(function () {
                        var dt = $filter('date')($scope.SelectedMatch.DateTime, 'MM/dd/yy hh:mm a EEEE');
                        var msg = "Score for  match '" + $scope.SelectedMatch.Home + " Vs " + $scope.SelectedMatch.Away + " - " + dt + "' has been sucessfully submitted for review.";
                        $scope.displaymessage = msg;
                        $scope.done = true;
                    });
                });
            }
            else {
                MatchEntryService.setTeamStats(homeTeamStats).then(function () {
                    var dt = $filter('date')($scope.SelectedMatch.DateTime, 'MM/dd/yy hh:mm a EEEE');
                    var msg = "Your team's score for  match '" + $scope.SelectedMatch.Home + " Vs " + $scope.SelectedMatch.Away + " - " + dt + "' has been sucessfully submitted for review.";
                    $scope.displaymessage = msg;
                    $scope.done = true;
                });
            }

        }
        $scope.ShowFielder = function (howout) {
            if (howout == null || howout == undefined) return false;
            if (howout == 2 || howout == 4) return true;
            return false;
        }
        function getExistingScore(matchID) {
            MatchEntryService.getMatchPlayerStats(matchID).then(function (data) {
                $scope.Scores.push.apply($scope.Scores, data);
                MatchEntryService.getMatchTeamStats(match.ID).then(function (data) {
                    $scope.HomeTeamStats = $filter('filter')(data, function (s) { return s.TeamId == m.Schedule.HomeId });
                    $scope.AwayTeamStats = $filter('filter')(data, function (s) { return s.TeamId == m.Schedule.AwayId });
                });
                $scope.BattingScore = $filter('filter')($scope.Scores, function (s) { return (s.TeamId == m.Schedule.HomeId && (s.HowOut != null && s.HowOut > 0)) });
                $scope.BowlingScore = $filter('filter')($scope.Scores, function (s) { return (s.TeamId == m.Schedule.HomeId && (s.OversBowled != null && s.OversBowled != -1)) });

                $scope.AwayBattingScore = $filter('filter')($scope.Scores, function (s) { return (s.TeamId == m.Schedule.AwayId && (s.HowOut != null && s.HowOut > 0)) });
                $scope.AwayBowlingScore = $filter('filter')($scope.Scores, function (s) { return (s.TeamId == m.Schedule.AwayId && (s.OversBowled != null && s.OversBowled != -1)) });
            });
        }
        function reset() {
            $scope.PlayerId = null;
            $scope.BattingRuns = null;
            $scope.BallsFaced = null;
            $scope.HowOut = null;
            $scope.Bowler = null;
            $scope.Fielder = null;
            $scope.BowlerNumber = null;
            $scope.Overs = null;
            $scope.Maiden = null;
            $scope.BowlingRuns = null;
            $scope.Wickets = null;
            $scope.PlayerToAdd = null;
        };
    }]);

var battingscore = {
    PlayerName: '',
    HowOut: '',
    BowlerId: '',
    BowlerName:'',
    Fielder: '',
    Runs: '',
    Balls: 0,
    Fours: 0,
    Sixes: 0,
    WicketNumber: '',
    FOWRuns: ''
};

var bowlingscore = {
    Bowler: '',
    Fielder: '',
    Overs: '',
    Maiden: '',
    RunsGiven: '',
    Wickets: '',
    Wide: '',
    NoBalls: ''
};
var awaybattingscore = {
    Batsman: '',
    HowOut: '',
    Bowler: '',
    Fielder: '',
    Runs: '',
    Balls: '',
    Fours: 0,
    Sixes: 0,
    WicketNumber: 0,
    FOWRuns: 0
};

var awaybowlingscore = {
    Bowler: '',
    Fielder: '',
    Overs: '',
    Maiden: '',
    RunsGiven: '',
    Wickets: '',
    Wide: '',
    NoBalls: ''
};