WMCBApp.controller('MatchScoreCtrl', ["$scope", "MatchEntryService",
    function ($scope, MatchEntryService) {
        $scope.OutList = [
           { Id: 1, Type: "Bowled" },
           { Id: 2, Type: "Caught" },
           { Id: 3, Type: "Stumped" },
           { Id: 4, Type: "Run Out" }];
        init();
        $scope.PlayerSelected = false;
        $scope.PlayerToAdd = null;
        $scope.IsDirty = false;
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

        $scope.AddedPlayers = new Array();
        $scope.NewPlayersToAdd = new Array();
        $scope.HomeTeamPlayers = [];
        $scope.AwayTeamPlayers = [];
        $scope.Match = {};

        $scope.init = function (value) {
            alert(value);
        };
        function init() {
            MatchEntryService.getMatch(matchId).then(function (data) {
                $scope.Match = data;

                MatchEntryService.getTeamPlayers($scope.Match.HomeTeam.ID).then(function (data) {
                    $scope.HomeTeamPlayers = data;
                });
                MatchEntryService.getTeamPlayers($scope.Match.AwayTeam.ID).then(function (data) {
                    $scope.AwayTeamPlayers = data;


                    MatchEntryService.getMatchStats($scope.Match.ID).then(function (data) {
                        angular.forEach(data, function (value, key) {
                            var bowler = null;
                            var fielder = null;
                            
                            angular.forEach($scope.AwayTeamPlayers, function (bowlerValue, bowlerKey) {
                                if (bowlerValue.ID == value.Bowler)
                                    bowler = bowlerValue;
                                if (bowlerValue.ID == value.Fielder)
                                    fielder = bowlerValue;
                            });

                            var player = {
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
                                "BowlerName": bowler != null ? bowler.FullName: '',
                                "Fielder": value.Fielder,
                                "FielderName": fielder != null ? fielder.FullName: '',
                                "BowlerNumber": value.BowlerNumber
                            };
                            
                            $scope.AddedPlayers.push(player);
                        });
                    });
                });
                
            });
        };

        $scope.EnableScore = function () {
            $scope.PlayerSelected = true;
        };
        $scope.OutChanged = function (out) {
            alert(out.Id);
            $scope.HowOut = out;
        };
        
        $scope.AddPlayerScore = function () {               
            $scope.IsDirty = true;
            $scope.PlayerSelected = false;
            var curPlayer = new Object($scope.PlayerToAdd)
            var player = new Object({
                "TeamId": $scope.Match.HomeTeam.ID,
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
                "BowlerNumber": $scope.BowlerNumber
            });
            $scope.AddedPlayers.push(new Object(player));
            $scope.NewPlayersToAdd.push(new Object(player));
            
            reset();
        };

        function reset() {
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
            MatchEntryService.SetPlayerStats($scope.NewPlayersToAdd);
            $scope.IsDirty = false;
        }

        $scope.bowlersFilter = function (element) {
            return element.OversBowled > 0 ? true : false;
        };

        $scope.ValidateBowler = function (element) {
            
            angular.forEach($scope.AddedPlayers, function(value, key){
                $scope.MatchScoreForm.BowlerNumber.$setValidity("Duplicate", value.BowlerNumber != $scope.BowlerNumber)
            });
        };
    }]);
