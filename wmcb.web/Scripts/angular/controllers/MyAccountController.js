WMCBApp.controller('MyAccountCtrl', ["$scope", "$filter", "wmcbService", "filteredListService", function ($scope, $filter, wmcbService, filteredListService) {
    $scope.defaultprofile = '';
    $scope.updateprofile = profile;
    $scope.init = function (myinfo) {
        $scope.defaultprofile = myinfo;
        $scope.updateprofile = myinfo;
    }
    $scope.updatemyprofile = function (info) {
        wmcbService.updatemyprofile(info).then(function (d) {
            if (d.data.Code == 0) {
                alert("saved");
                $scope.defaultprofile = info;
                $scope.updateprofile = info;
                $scope.updateprofile.CurrentPassword = '';
                $scope.updateprofile.NewPassword = '';
            }
        });
    }
    $scope.cancelupdate = function(){
        $scope.updateprofile = $scope.defaultprofile;
    }
   
}]);
var profile = {
    FirstName: '',
    LastName: '',
    Email: '',
    Phone: '',
    CurrentPassword: '',
    NewPassword: ''
}