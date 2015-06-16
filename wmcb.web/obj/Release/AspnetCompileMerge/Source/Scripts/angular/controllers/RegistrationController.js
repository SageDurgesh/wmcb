WMCBApp.controller('RegistrationCtrl', ["$scope", "$filter", "wmcbService", "filteredListService", function ($scope, $filter, wmcbService, filteredListService) {
    $scope.registration = {
        FirstName :'',
        LastName: '',
        Phone :'',
        Email: '',
        Password: '',
        ConfirmPassword :''
    };

    $scope.registerUser = function (isValid) {
        if (isValid) {
            wmcbService.addnewuser($scope.registration).then(function (response) {
                if (response != undefined && response.data != undefined && response.data) {
                    alert("user has been created");
                }
                else {
                    alert("an error occured!");
                }
            });
        }
        else {

        }
    };
}]);