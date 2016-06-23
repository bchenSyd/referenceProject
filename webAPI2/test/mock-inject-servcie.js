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



var mock, notify;
beforeEach(module('myServiceModule'));
beforeEach(function () {
    mock = { alert: jasmine.createSpy() };

    //*****************************************************************
    //we set up the mock at MODULE level
    module(function ($provide) {
        $provide.value('$window', mock);
    });
    //*****************************************************************

    //*********************************************************************************************************************
    //            this is how you get service, it's different from getting value and get controller
      inject(function ($injector) {
        notify = $injector.get('notify');
    });
    //*********************************************************************************************************************
});

it('should not alert first two notifications', function () {
    notify('one');
    notify('two');

    expect(mock.alert).not.toHaveBeenCalled();
});

it('should alert all after third notification', function () {
    notify('one');
    notify('two');
    notify('three');

    expect(mock.alert).toHaveBeenCalledWith("one\ntwo\nthree");
});

it('should clear messages after alert', function () {
    notify('one');
    notify('two');
    notify('third');
    notify('more');
    notify('two');
    notify('third');

    expect(mock.alert.callCount).toEqual(2);
    expect(mock.alert.mostRecentCall.args).toEqual(["more\ntwo\nthird"]);
});