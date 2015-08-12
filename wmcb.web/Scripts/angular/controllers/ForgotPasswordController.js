WMCBApp.controller('PasswordCtrl', ["$scope", "wmcbService", function ($scope, wmcbService) {
    $scope.Email = "";
    $scope.Result = {
        Message: '',
        Code:''
    }
    $scope.ResetPassword = function (e) {
        wmcbService.resetpassword(e).then(function (data) {
            $scope.Result = data;
            if ($scope.Result.Code == 0) {
                $scope.Message = "An email has been sent to your email address with a temporary password.";
            }
        });
    }
   
}]);