angular.module('linkedin.messaging', []).config(function($stateProvider){


$stateProvider.state('messaging', {
      url: '/messaging/:userId',
      templateUrl: 'templates/messaging.html',
      controller: 'MessagingCtrl as messageCtrl',
      data: {requiresLogin: true},
    });

});