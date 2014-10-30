angular.module('linkedin.authentication', []).config(function($stateProvider){ 


  $stateProvider

    // setup an abstract state for the tabs directive

    .state('login', {
      url: "/login",
      templateUrl: "templates/login.html",
      controller: 'LoginCtrl as lctrl',
    })
});