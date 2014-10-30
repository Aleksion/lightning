angular.module('linkedin.home', []).config(function($stateProvider){


$stateProvider.state('home_landing', {
      url: '/',
      templateUrl: 'templates/home.html',
      controller: 'ContentCtrl as content',
      data: {requiresLogin: true},
    });

});