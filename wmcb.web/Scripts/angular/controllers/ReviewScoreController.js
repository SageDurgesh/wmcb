WMCBApp.controller('ReviewCtrl', ["$scope", "$filter", "MatchEntryService",
    function ($scope, $filter, MatchEntryService) {
        $scope.Matches = '';
        $scope.Scores = [];
        MatchEntryService.getMatches().then(function (data) {
            $scope.Matches = data;
        });
        $scope.ViewScore = function (m, index) {
            MatchEntryService.getPlayerStatsByTeam(m.Match.ID).then(function (data) {
                $scope.Scores.splice(index,0,data);
            });
        };
    }
]);