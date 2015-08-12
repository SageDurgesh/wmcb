
WMCBApp.controller('MatchScoreCtrl', ["$scope", "$timeout" , "$filter", "$location", "MatchEntryService",
    function ($scope, $timeout, $filter, $location, MatchEntryService) {
        $scope.OutList = [
          { Id: 1, Type: "Bowled" },
          { Id: 2, Type: "Caught" },
          { Id: 3, Type: "Stumped" },
          { Id: 4, Type: "Run Out" },
          { Id: 5, Type: "Not Out" },
          {Id:6, Type:"Did not bat(dnb)"}];
        $scope.hasPermission = '';
        $scope.HomePlayers = '';
        $scope.AwayPlayers = '';        
        $scope.TeamID = '';
        $scope.TossWon = '';
        $scope.Battedfirst = '';
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
            angular.forEach($scope.BattingScore, function (item) {
                var s = {
                    TeamId: $scope.SelectedMatch.HomeId,
                    MatchId: $scope.SelectedMatch.ID,
                    PlayerId: item.Batsman.ID,
                    BattingRuns: item.Runs,
                    BallsFaced: item.Balls,
                    HowOut: item.HowOut.Id,                   
                    BowlerNumber: 0,
                    OversBowled: 0,
                    Wickets: 0,
                    MaidenOvers: 0,
                    Bowlingruns: 0,
                    Wide: 0,
                    noBalls: 0
                };
                if (item.Bowler != undefined) {
                    s.Bowler = item.Bowler.ID;
                }
                $scope.HTBattingRuns = parseInt($scope.HTBattingRuns * 1) + parseInt(s.BattingRuns);
                homeplayerstats.push(s);
            });
            angular.forEach($scope.BowlingScore, function (item) {
                found = false;
                angular.forEach(homeplayerstats, function (player) {
                    if (item.PlayerId == player.PlayerId) {
                        player.OversBowled = item.Overs;
                        player.Wickets = item.Wickets;
                        player.MaidenOvers = item.Maiden;
                        player.BowlingRuns = item.Runs;
                        player.Wide = item.Wide;
                        player.NoBalls = item.NoBalls;
                    }
                });
                if (!found) {
                    var s = {
                        TeamId: $scope.SelectedMatch.HomeId,
                        MatchId: $scope.SelectedMatch.ID,
                        PlayerId: item.Bowler.ID,
                        BattingRuns: 0,
                        BallsFaced: 0,
                        HowOut: 0,
                        BowlerNumber: 0,
                        OversBowled: item.Overs,
                        Wickets: item.Wickets,
                        MaidenOvers: item.Maiden,
                        Bowlingruns: item.Runs,
                        Wide: item.Wide,
                        noBalls: item.NoBalls
                    };
                    homeplayerstats.push(s);
                }
            });
            if (isAdmin) {
                angular.forEach($scope.AwayBattingScore, function (item) {
                    var s = {
                        TeamId: $scope.SelectedMatch.AwayId,
                        MatchId: $scope.SelectedMatch.ID,
                        PlayerId: item.Batsman.ID,
                        BattingRuns: item.Runs,
                        BallsFaced: item.Balls,
                        HowOut: item.HowOut.Id,
                        BowlerNumber: 0,
                        OversBowled: 0,
                        Wickets: 0,
                        MaidenOvers: 0,
                        Bowlingruns: 0,
                        Wide: 0,
                        noBalls: 0
                    };
                    if (item.Bowler != undefined) {
                        s.Bowler = item.Bowler.ID;
                    }
                    $scope.ATBattingRuns = parseInt($scope.ATBattingRuns * 1) + parseInt(s.BattingRuns);
                    awayplayerstats.push(s);
                });
                angular.forEach($scope.AwayBowlingScore, function (item) {
                    found = false;
                    angular.forEach(awayplayerstats, function (player) {
                        if (item.PlayerId == player.PlayerId) {
                            player.OversBowled = item.Overs;
                            player.Wickets = item.Wickets;
                            player.MaidenOvers = item.Maiden;
                            player.BowlingRuns = item.Runs;
                            player.Wide = item.Wide;
                            player.NoBalls = item.NoBalls;
                            found = true;
                        }
                    });
                    if (!found) {
                        var s = {
                            TeamId: $scope.SelectedMatch.AwayId,
                            MatchId: $scope.SelectedMatch.ID,
                            PlayerId: item.Bowler.ID,
                            BattingRuns: 0,
                            BallsFaced: 0,
                            HowOut: 0,
                            BowlerNumber: 0,
                            OversBowled: item.Overs,
                            Wickets: item.Wickets,
                            MaidenOvers: item.Maiden,
                            Bowlingruns: item.Runs,
                            Wide: item.Wide,
                            noBalls: item.NoBalls
                        };
                        awayplayerstats.push(s);
                    }
                });
            }
            MatchEntryService.SavePlayerStats(homeplayerstats);
            MatchEntryService.SavePlayerStats(awayplayerstats);
            var tosswinningnteam = '';
            var battingteam ='';
            if($scope.TossWon)
                tosswinningnteam = $scope.SelectedMatch.HomeId;
            else
                tosswinningnteam = $scope.SelectedMatch.AwayId;

            if($scope.BattedFirst)
                battingteam =$scope.SelectedMatch.HomeId;
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
            else
            {
                MatchEntryService.setTeamStats(homeTeamStats).then(function () {
                    var dt = $filter('date')($scope.SelectedMatch.DateTime, 'MM/dd/yy hh:mm a EEEE');
                    var msg = "Your team's score for  match '" + $scope.SelectedMatch.Home + " Vs " + $scope.SelectedMatch.Away + " - " + dt + "' has been sucessfully submitted for review.";
                    $scope.displaymessage = msg;
                    $scope.done = true;
                });
            }
                     
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
    Batsman: '',
    HowOut: '',
    Bowler: '',
    Runs: '',
    Balls: '',
    Fours: '',
    Sixes: '',
    WicketNumber: '',
    FOWRuns:''
};

var bowlingscore = {
    Bowler: '',
    Fielder:'',
    Overs: '',
    Maiden: '',
    RunsGiven: '',
    Wickets: '',
    Wide:'',
    NoBalls:''
};
var awaybattingscore = {
    Batsman: '',
    HowOut: '',
    Bowler: '',
    Runs: '',
    Balls: '',
    Fours: '',
    Sixes: '',
    WicketNumber: '',
    FOWRuns: ''
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