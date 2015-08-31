WMCBApp.controller('RegistrationCtrl', ["$scope", "$filter", "wmcbService", "filteredListService", function ($scope, $filter, wmcbService, filteredListService) {
    $scope.registration = {
        FirstName :'',
        LastName: '',
        Phone :'',
        Email: '',
        TeamID:''
    };
    $scope.myresult = {
        Code:-100,
        Message:''
    };
    $scope.SelectedTeamText = 'Select a Team';
    $scope.SelectedTeam = '';
    $scope.init = function () {
        wmcbService.getAllTeams().then(function (data) {
            $scope.Teams = data;          
        });
        $scope.myresult.Code = -100;
        $scope.myresult.Message = '';
    };
    $scope.SelectTeam = function (team) {
        if (team) {
            $scope.SelectedTeam = team;           
            $scope.SelectedTeamText = team.Name;
            $scope.registration.TeamID = $scope.SelectedTeam.ID;
        }
    };
    $scope.registerUser = function (isValid) {
        if (isValid) {
            wmcbService.registeruser($scope.registration).then(function (response) {
                if (response != undefined && response.data != undefined) {                    
                    $scope.myresult = response.data;
                    switch ($scope.myresult.Code) {
                        case 0: $scope.myresult.Message = "The user '" + $scope.registration.Email + "' has been created successfully!";
                            break;
                        case 1:
                        case 2:
                        case 3:
                            $scope.myresult.Message = "Please correct the data.";
                            break;
                        case 4:
                            $scope.myresult.Message = "The user '" + $scope.registration.Email + "' has been updated successfully!";
                            break;    
                    }
                }
                $scope.reset();
            });
        }
        else {             
           $scope.myresult.Message = "Please correct the data.";      
        }
    };
    $scope.reset = function () {        
        $scope.SelectedTeam = '';
        $scope.SelectedTeamText = 'Select a Team';
        $scope.registration.FirstName = '';
        $scope.registration.LastName = '';
        $scope.registration.Phone = '';
        $scope.registration.Email = '';
        $scope.registration.TeamID = '';
    }
}]);