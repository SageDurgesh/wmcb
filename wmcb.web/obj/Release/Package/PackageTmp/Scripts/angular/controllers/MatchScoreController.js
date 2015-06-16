WMCBApp.controller('MatchScoreCtrl', ["$scope", "$filter", "$location", "MatchEntryService",
    function ($scope, $filter, $location, MatchEntryService) {
        $scope.OutList = [
           { Id: 1, Type: "Bowled" },
           { Id: 2, Type: "Caught" },
           { Id: 3, Type: "Stumped" },
           { Id: 4, Type: "Run Out" },
           { Id: 5, Type: "Not Out" }];
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
        $scope.HTByes = null;
        $scope.HTLegByes = null;
        $scope.HTWides = null;
        $scope.HTNoBalls = null;
        $scope.HTPenaltyRuns = null;
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

        $scope.init = function (model) {
            
            $scope.YourTeamId = model.teamId;
            $scope.IsLeagueOfficial = model.hasPermission;

            MatchEntryService.getMatch($location.search()['MatchId']).then(function (data) {
                $scope.Match = data;
                
                MatchEntryService.getMatchTeamStats($location.search()['MatchId']).then(function (data) {
                    angular.forEach(data, function (value, key) {
                        
                        if (value.TeamId == $scope.Match.HomeTeam.ID) {
                            $scope.HTWides = value.Wides;
                            $scope.HTByes = value.Byes;
                            $scope.HTLegByes = value.LegByes;
                            $scope.HTNoBalls = value.NoBalls;
                            $scope.HTPenaltyRuns = value.PenaltyRuns;
                            $scope.HTExtras = value.Wides*1 + value.Byes*1 + value.LegByes*1 + value.NoBalls*1 + value.PenaltyRuns*1;

                            $scope.TeamStats.push({
                                //"ID": value.ID,
                                "MatchId": $scope.Match.ID,
                                "TeamId": $scope.Match.HomeTeam.ID,
                                "Wides": $scope.HTWides,
                                "Byes": $scope.HTByes,
                                "LegByes": $scope.HTLegByes,
                                "NoBalls": $scope.HTNoBalls,
                                "PenaltyRuns": $scope.HTPenaltyRuns,
                            });
                        }
                        else if (value.TeamId == $scope.Match.AwayTeam.ID) {
                            //$scope.ATStatId = value.ID;
                            $scope.ATWides = value.Wides;
                            $scope.ATByes = value.Byes;
                            $scope.ATLegByes = value.LegByes;
                            $scope.ATNoBalls = value.NoBalls;
                            $scope.ATPenaltyRuns = value.PenaltyRuns;
                            $scope.ATExtras = value.Wides * 1 + value.Byes * 1 + value.LegByes * 1 + value.NoBalls * 1 + value.PenaltyRuns * 1;

                            $scope.TeamStats.push({
                                "MatchId": $scope.Match.ID,
                                "TeamId": $scope.Match.AwayTeam.ID,
                                "Wides": $scope.ATWides,
                                "Byes": $scope.ATByes,
                                "LegByes": $scope.ATLegByes,
                                "NoBalls": $scope.ATNoBalls,
                                "PenaltyRuns": $scope.ATPenaltyRuns,
                            });
                        }
                    })
                })
                MatchEntryService.getTeamPlayers($scope.Match.HomeTeam.ID).then(function (data) {
                        $scope.HomeTeamPlayers = data;
                });
                MatchEntryService.getTeamPlayers($scope.Match.AwayTeam.ID).then(function (data) {
                        $scope.AwayTeamPlayers = data;

                        MatchEntryService.getMatchPlayerStats($scope.Match.ID).then(function (data) {
                        angular.forEach(data, function (value, key) {
                            var bowler = null;
                            var fielder = null;

                            if (value.TeamId == $scope.Match.HomeTeam.ID) {
                                angular.forEach($scope.AwayTeamPlayers, function (player, playerKey) {
                                    if (player.ID == value.Bowler)
                                        bowler = player;
                                    if (player.ID == value.Fielder)
                                        fielder = player;
                                });
                            }
                            else if (value.TeamId == $scope.Match.AwayTeam.ID){
                                angular.forEach($scope.HomeTeamPlayers, function (player, playerKey) {
                                    if (player.ID == value.Bowler)
                                        bowler = player;
                                    if (player.ID == value.Fielder)
                                        fielder = player;
                                });
                            }

                            var player = {
                                "ID": value.ID,
                                "TeamId": value.TeamId,
                                "MatchId": value.MatchId,
                                "PlayerId": value.PlayerId,
                                "FullName": value.Player.FullName,
                                "BattingRuns": value.BattingRuns,
                                "BallsFaced": value.BallsFaced,
                                "HowOut": value.HowOut,
                                "OversBowled": value.OversBowled,
                                "MaidenOvers": value.MaidenOvers,
                                "Wickets": value.Wickets,
                                "BowlingRuns": value.BowlingRuns,
                                "Bowler": value.Bowler,
                                "BowlerName": bowler == null? '' : bowler.FullName,
                                "Fielder": value.Fielder,
                                "FielderName": fielder == null ? '' : fielder.FullName,
                                "BowlerNumber": value.BowlerNumber
                            };

                            if (value.TeamId == $scope.Match.HomeTeam.ID) {
                                $scope.HomeTeamMatchPlayers.push(player);
                                $scope.HTBattingRuns += player.BattingRuns;
                            }
                            else if (value.TeamId == $scope.Match.AwayTeam.ID) {
                                $scope.AwayTeamMatchPlayers.push(player);
                                $scope.ATBattingRuns += player.BattingRuns;
                            }
                        });
                    });
                });
            });
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

        $scope.SavePlayerStats = function () {
            var homeTeamStats = $filter('filter')($scope.TeamStats, { TeamId: $scope.Match.HomeTeam.ID });
            $scope.ATExtras = $scope.ATWides * 1 + $scope.ATByes * 1 + $scope.ATLegByes * 1 + $scope.ATNoBalls * 1 + $scope.ATPenaltyRuns * 1;
            $scope.HTExtras = $scope.HTWides * 1 + $scope.HTByes * 1 + $scope.HTLegByes * 1 + $scope.HTNoBalls * 1 + $scope.HTPenaltyRuns * 1;
            
            homeTeamStats = {
                //"ID": $scope.HTStatId,
                "MatchId": $scope.Match.ID,
                "TeamId": $scope.Match.HomeTeam.ID,
                "Wides": $scope.HTWides,
                "Byes": $scope.HTByes,
                "LegByes": $scope.HTLegByes,
                "NoBalls": $scope.HTNoBalls,
                "PenaltyRuns": $scope.HTPenaltyRuns,
                "TeamScores": $scope.HTBattingRuns*1 + $scope.HTExtras*1
            };

            var awayTeamStats = $filter('filter')($scope.TeamStats, { TeamId: $scope.Match.AwayTeam.ID });
            awayTeamStats = {
                //"ID": $scope.ATStatId,
                "MatchId": $scope.Match.ID,
                "TeamId": $scope.Match.AwayTeam.ID,
                "Wides": $scope.ATWides,
                "Byes": $scope.ATByes,
                "LegByes": $scope.ATLegByes,
                "NoBalls": $scope.ATNoBalls,
                "PenaltyRuns": $scope.ATPenaltyRuns,
                "TeamScores": $scope.ATBattingRuns*1 + $scope.ATExtras*1
            };

            $scope.TeamStats.splice(0, $scope.TeamStats.length);
            $scope.TeamStats.push(homeTeamStats);
            $scope.TeamStats.push(awayTeamStats);

            MatchEntryService.setPlayerStats($scope.NewPlayersToAdd);
            MatchEntryService.setTeamStats($scope.TeamStats);

            $scope.NewPlayersToAdd = new Array();
            $scope.IsDirty = false;
        }

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
