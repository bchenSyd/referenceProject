angular.module('myApplicationModule', [])
    .value('mode', 'app')
    .value('version', 'v1.0.1')
    .service();


describe('MyApp', function () {

    // You need to load modules that you want to test,
    // it loads only the "ng" module by default.
    beforeEach(module('myApplicationModule'));

    /*
    within the inject function, e.g. inject(function(_$controller_)}{
        localVar = _$controller_ })
    you claim what you want from angular and becuase inject will inject your function within the 'context' of the mock moduel, 
    you function would have access to everything that is accessible within the module
    say, there is a global variable declared in angular module, , you can claim that by making the name of that variable in your 
    function parameter to inject     
    */

    // inject() is used to inject arguments of all given functions
    it('should provide a version', inject(function (mode, version) {
        expect(version).toEqual('v1.0.1');
        expect(mode).toEqual('app');
    }));


    // The inject and module method can also be used inside of the it or beforeEach  ==> doesn't work!
    it('should override a version and test the new version is injected', function () {
        module(function ($provide) {
            $provide.value('version', 'overridden'); // override version here
        });

        // module() takes functions or strings (module aliases)
        inject(function (version) {
            expect(version).toEqual('overridden');
        });

       

        
    });
});