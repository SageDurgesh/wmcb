
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
        $scope.BattingScore = [battingscore];
        $scope.BowlingScore = [bowlingscore];
        $scope.SelectedMatchText = 'Select a Match';
        $scope.SelectedMatch = '';
        $scope.AgainstTeams = [];
        $scope.done = false;
                
       
        //init();
        $scope.PlayerSelected = false;
        $scope.PlayerToAdd = null;
        $scope.IsDirty = false;
        $scope.PlayerId = null;
        $scope.BattingRuns = null;
        $scope.BallsFaced = null;
        $scope.HowOut = new Object();
        $scope.Bowler = new Object();
        $scope.Fielder = new Object();
        $scope.BowlerNumber = null;
        $scope.Overs = null;
        $scope.Maiden = null;
        $scope.BowlingRuns = null;
        $scope.Wickets = null;
        $scope.IsReviewed = false;
        $scope.HTStatId = null;
        $scope.HTByes = 0;
        $scope.HTLegByes = 0;
        $scope.HTWides = 0;
        $scope.HTNoBalls = 0;
        $scope.HTPenaltyRuns = 0;
        $scope.ATStatId = null;
        $scope.ATByes = null;
        $scope.ATLegByes = null;
        $scope.ATWides = null;
        $scope.ATNoBalls = null;
        $scope.ATPenaltyRuns = null;

        $scope.AddedPlayers = new Array();
        $scope.NewPlayersToAdd = new Array();
        $scope.HomeTeamPlayers = [];
        $scope.AwayTeamPlayers = [];
        $scope.HomeTeamMatchPlayers = [];
        $scope.AwayTeamMatchPlayers = [];
        $scope.MatchPlayers = [];
        $scope.Match = {};
        $scope.YourTeamId = null;
        $scope.HomeTeamId = null;
        $scope.AwayTeamId = null;
        $scope.IsLeagueOfficial = false;
        $scope.HTBattingRuns = 0;
        $scope.HTExtras = 0;
        $scope.ATBattingRuns = 0;
        $scope.ATExtras = 0;
        $scope.TeamStats = new Array();

        $scope.init = function (hasPermission, TeamID) {            
            $scope.TeamId = TeamID;
            $scope.hasPermission = hasPermission;
            MatchEntryService.getMyMatches(TeamID).then(function(data){
                $scope.Matches = data;
               
                angular.forEach(data, function (item) {
                    if(item.HomeId == TeamID){
                         var sch = {
                             ID: item.ID,
                             AgainstTeamId: item.AwayId,
                             AgainstTeamName: item.Away,
                             Week: item.Week,
                             DateTime: item.DateTime
                         };
                         $scope.AgainstTeams.push(sch);
                    }
                    else if (item.AwayId == TeamID) {
                        var sch = {
                            ID: item.ID,
                            AgainstTeamId: item.HomeId,
                            AgainstTeamName: item.Home,
                            Week: item.Week,
                            DateTime: item.DateTime
                        };
                        $scope.AgainstTeams.push(sch);
                    }
                });
            });
            MatchEntryService.getTeamPlayers(TeamID).then(function (data) {
                $scope.HomePlayers = data;
            });
        };
        $scope.SelectMatch = function (match) {
            if (match) {
                $scope.SelectedMatch = match;
                var dt = $filter('date')(match.DateTime, 'EEE, MM/dd');
                $scope.SelectedMatchText = "Vs " + match.AgainstTeamName + " - " + dt;
                MatchEntryService.getTeamPlayers(match.AgainstTeamId).then(function (data) {
                    $scope.AwayPlayers = data;
                });
            }
        };
        $scope.PlayerComparer = function (player, viewValue) {
            return viewValue === secretEmptyKey || ('' + player).toLowerCase().indexOf(('' + viewValue).toLowerCase()) > -1;
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
        $scope.SubmitMatchScore = function () {
            //var homeTeamStats = $filter('filter')($scope.TeamStats, { TeamId: $scope.Match.HomeTeam.ID });          
            var playerstats = [];
            angular.forEach($scope.BattingScore, function (item) {
                var s = {
                    TeamId: $scope.TeamId,
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
                playerstats.push(s);
            });
            angular.forEach($scope.BowlingScore, function (item) {
                angular.forEach(playerstats, function (player) {
                    if (item.Bowler.ID == player.PlayerId) {
                        player.OversBowled = item.Overs;
                        player.Wickets = item.Wickets;
                        player.MaidenOvers = item.Maiden;
                        player.BowlingRuns = item.Runs;
                        player.Wide = item.Wide;
                        player.NoBalls = item.NoBalls;
                    }
                });
            });
            MatchEntryService.SavePlayerStats(playerstats);
            $scope.HTExtras = $scope.HTWides * 1 + $scope.HTByes * 1 + $scope.HTLegByes * 1 + $scope.HTNoBalls * 1 + $scope.HTPenaltyRuns * 1;
            var homeTeamStats = {
                "MatchId": $scope.SelectedMatch.ID,
                "TeamId": $scope.TeamId,
                "Wides": $scope.HTWides,
                "Byes": $scope.HTByes,
                "LegByes": $scope.HTLegByes,
                "NoBalls": $scope.HTNoBalls,
                "PenaltyRuns": $scope.HTPenaltyRuns,
                "TeamScore": (parseInt($scope.HTBattingRuns) * 1) + (parseInt($scope.HTExtras) * 1)
            };
            MatchEntryService.setTeamStats(homeTeamStats).then(function () {
                var msg = "Scorecard for your team has been sucessfully submitted for review.";
                $scope.displaymessage = msg;
                $scope.done = true;
            });


            // $scope.ATExtras = $scope.ATWides * 1 + $scope.ATByes * 1 + $scope.ATLegByes * 1 + $scope.ATNoBalls * 1 + $scope.ATPenaltyRuns * 1; 
            //var awayTeamStats = $filter('filter')($scope.TeamStats, { TeamId: $scope.Match.AwayTeam.ID });
            //awayTeamStats = {
            //    //"ID": $scope.ATStatId,
            //    "MatchId": $scope.Match.ID,
            //    "TeamId": $scope.Match.AwayTeam.ID,
            //    "Wides": $scope.ATWides,
            //    "Byes": $scope.ATByes,
            //    "LegByes": $scope.ATLegByes,
            //    "NoBalls": $scope.ATNoBalls,
            //    "PenaltyRuns": $scope.ATPenaltyRuns,
            //    "TeamScores": $scope.ATBattingRuns*1 + $scope.ATExtras*1
            //};

            //$scope.TeamStats.splice(0, $scope.TeamStats.length);
            //$scope.TeamStats.push(homeTeamStats);
            //$scope.TeamStats.push(awayTeamStats);

            //MatchEntryService.setPlayerStats($scope.NewPlayersToAdd);
            //MatchEntryService.setTeamStats($scope.TeamStats);

            //$scope.NewPlayersToAdd = new Array();
            //$scope.IsDirty = false;
        }
        $scope.ResetScoreCard = function () {
            
        };
        $scope.EnableScore = function () {
            if ($scope.PlayerToAdd == null)
                $scope.PlayerSelected = false;
            else
                $scope.PlayerSelected = true;
        };

        $scope.Cancel = function () {
            $scope.PlayerSelected = false;
            $scope.PlayerToAdd = null;
            reset();
        };

        $scope.OutChanged = function (out) {
            $scope.HowOut = out;
        };
        
        $scope.EditPlayer = function (player) {
            $scope.PlayerId = player.PlayerId;
            $scope.BattingRuns = player.BattingRuns;
            $scope.BallsFaced = player.BallsFaced,
            $scope.HowOut = player.HowOut != null ? player.HowOut.Id : null,
            $scope.Bowler = player.Bowler != null ? player.Bowler.Id : null,
            $scope.Fielder = player.Fielder != null ? player.Fielder.Id : null,
            $scope.BowlerNumber = player.BowlerNumber,
            $scope.Overs = player.OversBowled,
            $scope.Maiden = player.MaidenOvers,
            $scope.BowlingRuns = player.BowlingRuns,
            $scope.Wickets = player.Wickets

            $scope.PlayerToAdd = player;
            $scope.PlayerToAdd.ID = player.PlayerId;
            $scope.PlayerToAdd.TeamId = player.TeamId;
            $scope.PlayerSelected = true;
        }

        $scope.DeletePlayer = function (player) {
            var p = $filter('filter')($scope.NewPlayersToAdd, { ID: player.PlayerId });
            player.IsDeleted = true;
            $scope.IsDirty = true;

            if (player.TeamId == $scope.Match.HomeTeam.ID)
            {
                var tp = $filter('filter')($scope.HomeTeamMatchPlayers, { ID: player.PlayerId });
                $scope.HTBattingRuns -= player.BattingRuns;
                $scope.HomeTeamMatchPlayers.splice($scope.HomeTeamMatchPlayers.indexOf(player), 1);
            }
            else if (player.TeamId == $scope.Match.AwayTeam.ID)
            {
                var tp = $filter('filter')($scope.AwayTeamMatchPlayers, { ID: player.PlayerId });
                $scope.ATBattingRuns -= player.BattingRuns;
                $scope.AwayTeamMatchPlayers.splice($scope.AwayTeamMatchPlayers.indexOf(player), 1);
            }

            $scope.NewPlayersToAdd.splice($scope.NewPlayersToAdd.indexOf(p), 1, player);
        }

        $scope.AddPlayerScore = function () {               
            $scope.IsDirty = true;
            $scope.PlayerSelected = false;
            var curPlayer = new Object($scope.PlayerToAdd)

            var player = new Object({
                //"ID": $scope.PlayerStatsId,
                "TeamId": curPlayer.TeamId,
                "MatchId": $scope.Match.ID,
                "PlayerId": curPlayer.ID,
                "FullName": curPlayer.FullName,
                "BattingRuns": $scope.BattingRuns,
                "BallsFaced": $scope.BallsFaced,    
                "HowOut": $scope.HowOut != null ? $scope.HowOut.Id : null,
                "Bowler": $scope.Bowler != null ? $scope.Bowler.ID : null,
                "BowlerName" :  $scope.Bowler != null ? $scope.Bowler.FullName : '',
                "Fielder": $scope.Fielder != null ? $scope.Fielder.ID : null, 
                "FielderName": $scope.Fielder != null ? $scope.Fielder.FullName : '',
                "OversBowled": $scope.Overs,
                "MaidenOvers": $scope.Maiden,
                "Wickets": $scope.Wickets,
                "BowlingRuns": $scope.BowlingRuns,
                "BowlerNumber": $scope.BowlerNumber,
                "IsDeleted": false
            });

            if(curPlayer.TeamId == $scope.Match.HomeTeam.ID)
                $scope.UpdateHomePlayer(curPlayer.ID, player);
            else if (curPlayer.TeamId == $scope.Match.AwayTeam.ID)
                $scope.UpdateAwayPlayer(curPlayer.ID, player);

            var p = $filter('filter')($scope.NewPlayersToAdd, { ID: player.PlayerId });
            if(p != null)
                $scope.NewPlayersToAdd.splice($scope.NewPlayersToAdd.indexOf(p), 1, player);
            else
                $scope.NewPlayersToAdd.push(new Object(player));
            
            reset();
        };

        $scope.UpdateHomePlayer = function (playerId, teamMatchPlayer) {
            var player = $filter('filter')($scope.HomeTeamMatchPlayers, { ID: playerId });
            
            $scope.HTBattingRuns = $scope.HTBattingRuns * 1 + teamMatchPlayer.BattingRuns * 1;

            if (player.length == 0)
                $scope.HomeTeamMatchPlayers.push(teamMatchPlayer);
            else {
                $scope.HTBattingRuns = $scope.HTBattingRuns * 1 - player[0].BattingRuns * 1;
                $scope.HomeTeamMatchPlayers.splice($scope.HomeTeamMatchPlayers.indexOf(player), 1, teamMatchPlayer);
            }
        };

        $scope.UpdateAwayPlayer = function (playerId, teamMatchPlayer) {
            var player = $filter('filter')($scope.AwayTeamMatchPlayers, { ID: playerId });
            
            $scope.ATBattingRuns = $scope.ATBattingRuns*1 + teamMatchPlayer.BattingRuns*1;

            if (player.length == 0)
                $scope.AwayTeamMatchPlayers.push(teamMatchPlayer);
            else {
                $scope.ATBattingRuns = $scope.ATBattingRuns * 1 - player[0].BattingRuns * 1;
                $scope.AwayTeamMatchPlayers.splice($scope.AwayTeamMatchPlayers.indexOf(player), 1, teamMatchPlayer);
            }
        };

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

        $scope.CompleteMatchScore = function()
        {
            $scope.Match.IsReviewed = $scope.IsReviewed;
            MatchEntryService.CompleteMatchScore($scope.Match);
        }

        $scope.bowlersFilter = function (element) {
            return  element.OversBowled > 0 ? true : false;
        };

        $scope.ValidateBowler = function () {
            $scope.MatchScoreForm.BowlerNumber.$setValidity("Missing", ($scope.Overs != null && $scope.BowlerNumber != null) || $scope.Overs == '' || $scope.Overs == null);

            angular.forEach($scope.HomeTeamMatchPlayers, function (value, key) {
                $scope.MatchScoreForm.BowlerNumber.$setValidity("Duplicate", value.PlayerId == $scope.PlayerId || value.BowlerNumber != $scope.BowlerNumber)
            });
            angular.forEach($scope.AwayTeamMatchPlayers, function (value, key) {
                $scope.MatchScoreForm.BowlerNumber.$setValidity("Duplicate", value.PlayerId == $scope.PlayerId || value.BowlerNumber != $scope.BowlerNumber)
            });
        };

        $scope.MarkDirty = function (element) {
            $scope.IsDirty = true;
        }
    }]);

var battingscore = {
    Batsman: '',
    HowOut: '',
    Bowler: '',
    Runs: 0,
    Balls: 0,
    Fours: 0,
    Sixes: 0,
    WicketNumber: 0,
    FOWRuns:0
};

var bowlingscore = {
    Bowler: '',
    Fielder:'',
    Overs: '',
    Maiden: '',
    RunsGiven: 0,
    Wickets: 0,
    Wide:0,
    NoBalls:0
};