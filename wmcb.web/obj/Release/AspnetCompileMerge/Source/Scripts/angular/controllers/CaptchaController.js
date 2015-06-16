WMCBApp.controller('CaptchaCtrl', ["$scope", function ($scope) {
    $scope.captchaOptions = {
        imgPath: 'Images/captcha/',
        captcha: { numberOfImages: 5 },
        // use init callback to get captcha object
        init: function (captcha) {
            $scope.captcha = captcha;
        }
    };
    // Show an alert saying if visualCaptcha is filled or not
    var _sayIsVisualCaptchaFilled = function () {
        if (captcha.getCaptchaData().valid) {
            window.alert('visualCaptcha is filled!');
        } else {
            window.alert('visualCaptcha is NOT filled!');
        }
    };
}]);