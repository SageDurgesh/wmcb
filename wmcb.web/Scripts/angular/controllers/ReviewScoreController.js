WMCBApp.controller('ReviewCtrl', ["$scope", "$filter", "MatchEntryService",
    function ($scope, $filter, MatchEntryService) {
        $scope.Matches = '';
        $scope.Scores = [];
        $scope.selectedRow = '';
        MatchEntryService.getMatches().then(function (data) {
            $scope.Matches = data;
        });
        $scope.ViewScore = function (m, index) {
            $scope.selectedRow = index;
            $scope.Scores = [];
            MatchEntryService.getMatchPlayerStats(m.Match.ID).then(function (data) {

                $scope.Scores.push.apply($scope.Scores, data);
            });
        };
    }
]);