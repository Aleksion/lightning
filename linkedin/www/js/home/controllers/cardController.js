angular.module('linkedin.home').controller('CardCtrl', CardCtrl);


CardCtrl.$inject = ['TDCardDelegate', '$ionicSideMenuDelegate', 'ProfileSvc'] ;


 function CardCtrl(TDCardDelegate, $ionicSideMenuDelegate, ProfileSvc) {


 	$ionicSideMenuDelegate.canDragContent(false);
  var vm = this;
  vm.cardSwipedLeft = function(index, parentScope, card) {
    console.log('LEFT SWIPE');
    ProfileSvc.dislikeUser(card.userId);
  };
  vm.cardSwipedRight = function(index, parentScope, card) {
    console.log('RIGHT SWIPE');
    ProfileSvc.likeUser(card.userId);
  };
};