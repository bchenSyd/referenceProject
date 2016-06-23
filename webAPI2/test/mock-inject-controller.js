describe('PasswordController', function () {
    /*
    Note that angular.module and angular.mock.module are not the same. The window.module function is an alias for angular.mock.module    
    */
    beforeEach(module('app'));  


    //why do we need $controller??
    //Often, we would like to inject a reference once, in a beforeEach() block and reuse this in multiple it() clauses. To be able to do this we must assign the reference to a variable that is declared in the scope of the describe() block
    var $controller;

    beforeEach(inject(function (_$controller_) {
        // The injector unwraps the underscores (_) from around the parameter names when matching
        $controller = _$controller_;
    }));

    describe('$scope.grade', function () {
        var $scope, controller;

        beforeEach(function () {
            $scope = {};
            controller = $controller('PasswordController', { $scope: $scope });
        });

        it('sets the strength to "strong" if the password length is >8 chars', function () {
            $scope.password = 'longerthaneightchars';
            $scope.grade();
            expect($scope.strength).toEqual('strong');
        });

        it('sets the strength to "weak" if the password length <3 chars', function () {
            $scope.password = 'a';
            $scope.grade();
            expect($scope.strength).toEqual('weak');
        });
    });
});