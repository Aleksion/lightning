angular.module('linkedin.home').controller('ContentCtrl',ContentCtrl);


ContentCtrl.$inject =  ['auth', 'ProfileSvc', '$location']


function ContentCtrl(auth, ProfileSvc,$location) {
	var vm = this;
	auth.getProfile().then(function(profile){
		vm.profile = profile;
		
	console.log(vm.profile);
	});

	vm.matchingProfiles = ProfileSvc.getPotentialMatches().$object;

	vm.cardDestroyed = function(index) {
    	vm.matchingProfiles.splice(index, 1);
  };
    vm.cardSwipedLeft = function(index, card) {
    console.log('LEFT SWIPE');
    ProfileSvc.dislikeUser(card.userId);
  };
  vm.cardSwipedRight = function(index, card) {
    console.log('RIGHT SWIPE');
    ProfileSvc.likeUser(card.userId);
  };

  vm.go = function(path){
  	$location.path(path);
  }
}