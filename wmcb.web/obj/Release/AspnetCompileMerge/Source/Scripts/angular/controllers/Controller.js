var WMCBApp = angular.module('WMCBApp', ['ngSanitize']);
WMCBApp.filter("sanitize", ['$sce', function ($sce) {
    return function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    }
}]);
WMCBApp.$inject = ['$scope', '$filter', '$http'];
WMCBApp.directive("customSort", function () {
    return {
        restrict: 'A',
        transclude: true,
        scope: {
            order: '=',
            sort: '='
        },
        template:
          ' <a ng-click="sort_by(order)" style="color: #555555;">' +
          '    <span ng-transclude></span>' +
          '    <span class="glyphicon"  ng-class="selectedCls(order)"></span>' +
          '</a>',
        link: function (scope) {
            // change sorting order
            scope.sort_by = function (newSortingOrder) {
                var sort = scope.sort;

                if (sort.sortingOrder == newSortingOrder) {
                    sort.reverse = !sort.reverse;
                }
                sort.sortingOrder = newSortingOrder;
            };
            scope.selectedCls = function (column) {
                if (column == scope.sort.sortingOrder) {
                    return ('glyphicon-chevron-' + ((scope.sort.reverse) ? 'down' : 'up'));
                }
                else {
                    return ''
                }
            };
        }// end link
    }
});

WMCBApp.directive("compareTo", function () {
    return {
        require: "ngModel",
        scope: {
            otherModelValue: "=compareTo"
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.compareTo = function (modelValue) {
                return modelValue == scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    };
});

