WMCBApp.controller('PasswordCtrl', ["$scope", "wmcbService", function ($scope, wmcbService) {
    $scope.Email = "";
    $scope.Result = {
        Message: '',
        Code:''
    }
    $scope.ResetPassword = function (e) {
        wmcbService.resetpassword(e).then(function (d) {
            $scope.Result = d.data;
            if ($scope.Result.Code == 0) {
                $scope.Message = "An email has been sent to '" + e + "' email address with a temporary password.";
                $scope.Email = "";
            }
            else {
                $scope.Message = $scope.Result.Message;
            }
        });
    }
   
}]);