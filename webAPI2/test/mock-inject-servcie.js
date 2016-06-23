angular.module('myServiceModule', []).
 controller('MyController', ['$scope', 'notify', function ($scope, notify) {
     $scope.callNotify = function (msg) {
         notify(msg);
     };
 }]).
factory('notify', ['$window', function (win) {
    var msgs = [];
    return function (msg) {
        msgs.push(msg);
        if (msgs.length == 3) {
            win.alert(msgs.join("\n"));
            msgs = [];
        }
    };
}]).
service('dataService',[function() {
       this.sayHello=function() {
           return "hello";
       }
    }]);


describe('how to mock a $window service  -- create a child module and override $window there', function() {
    var mockWin;
    beforeEach(function() {
        mockWin = { alert: jasmine.createSpy() };
        angular.module('test', ['myServiceModule']).value('$window', mockWin);
        module('test');
    });
       
    it('should return service', inject(function(notify) {
        notify('one'); // notify service is still availble within the context of 'test' module
        expect(mockWin.alert).not.toHaveBeenCalled();
    }));

    it('should return service', inject(function (notify) {
        notify('one');
        notify('two');
        notify('three');

        expect(mockWin.alert).toHaveBeenCalledWith("one\ntwo\nthree");
    }));

});


describe('how to mock a $window service  -- explicitly load a new module', function () {
    var mock, notify;
    beforeEach(function () {
        module('myServiceModule');
        mock = { alert: jasmine.createSpy() };
        // module() takes functions or strings (module aliases)  ==> so essentially we are create a new module here;
        module(function ($provide) {
            $provide.value('$window', mock);  //set a $window value service
        });

        //use inject for GET
        inject(function ($injector) {
            notify = $injector.get('notify');
        });
    });

    it('should return service', inject(function (notify) {
        notify('one');
        expect(mock.alert).not.toHaveBeenCalled();
    }));

    it('should return service', inject(function (notify) {
        notify('one');
        notify('two');
        notify('three');
        expect(mock.alert).toHaveBeenCalledWith("one\ntwo\nthree");
    }));

});