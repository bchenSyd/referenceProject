var myApp = angular.module('myApp', []);

myApp.controller('myCtl', ['$scope', function ($scope) {
    $scope.languages = [{
        value: '1',
        text: '.net C# , asp.net mvc 4,5 web api'
    }, {
        value: '2',
        text: 'unity, oauth, owin, identity'
    }, {
        value: '3',
        text: 'angular js, jquery, react.js, node.js'
    }, {
        value: '4',
        text: 'EF 6.0 with sql server 2008,2012'
    }];
}]);