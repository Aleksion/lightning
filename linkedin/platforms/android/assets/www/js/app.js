// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
angular.module('linkedin', [
  'ngCookies',
  'ionic',
  'auth0',
  'angular-storage',
  'angular-jwt',
  'linkedin.controllers',
  'linkedin.services' ])
.config(function($stateProvider, $urlRouterProvider, authProvider, $httpProvider, jwtInterceptorProvider) {

  // Ionic uses AngularUI Router which uses the concept of states
  // Learn more here: https://github.com/angular-ui/ui-router
  // Set up the various states which the app can be in.
  // Each state's controller can be found in controllers.js

  $stateProvider

    // setup an abstract state for the tabs directive

    .state('login', {
      url: "/login",
      templateUrl: "templates/login.html",
      controller: 'LoginCtrl as lctrl',
    })
    // the pet tab has its own child nav-view and history
    .state('home_landing', {
      url: '/',
      templateUrl: 'templates/home.html',
      controller: 'ContentCtrl as content',
      data: {requiresLogin: true}
    });

    authProvider.init({
    domain: 'coffeein.auth0.com',
    clientID: 'znuVI4v98d3IvLipExvIurQpzkRG68JZ',
    callbackURL: "https://coffeein.auth0.com/mobile",
    // This is the name of the state to redirect to if the user tries to enter
    // to a restricted page
    loginState: 'login'
  });

     jwtInterceptorProvider.tokenGetter = function(store, jwtHelper, auth) {
    var idToken = store.get('token');
    var refreshToken = store.get('refreshToken');
    if (!idToken || !refreshToken) {
      return null;
    }
    if (jwtHelper.isTokenExpired(idToken)) {
      return auth.refreshIdToken(refreshToken).then(function(idToken) {
        store.set('token', idToken);
        return idToken;
      });
    } else {
      return idToken;
    }
  }


   $httpProvider.interceptors.push('jwtInterceptor')
  // if none of the above states are matched, use this as the fallback
  $urlRouterProvider.otherwise('/');


})
.run(function($ionicPlatform, $rootScope, $state, auth, store, jwtHelper) {
  $ionicPlatform.ready(function() {
    // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
    // for form inputs)
  if(window.cordova && window.cordova.plugins.Keyboard) {
    cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
  }
  if(window.StatusBar) {
    StatusBar.styleDefault();
  }
});
  $rootScope.$on('$locationChangeStart', function() {
    if (!auth.isAuthenticated) {
      var token = store.get('token');
      if (token) {
        if (!jwtHelper.isTokenExpired(token)) {
          auth.authenticate(store.get('profile'), token);
        } else {
          $state.go('login');
        }
      }
    }

  });

// This hooks al auth events to check everything as soon as the app starts
  auth.hookEvents();

  
})
