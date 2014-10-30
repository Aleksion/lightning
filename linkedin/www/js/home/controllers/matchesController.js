angular.module('linkedin.home').controller('MatchesCtrl',MatchesCtrl);


MatchesCtrl.$inject =  ['auth', 'ProfileSvc']


function MatchesCtrl(auth, ProfileSvc) {
	var vm = this;
	

	vm.matches = ProfileSvc.getMatches().$object;

	vm.getItemHeight = function(item, index) {
    //Make evenly indexed items be 10px taller, for the sake of example
    return (index % 2) === 0 ? 50 : 60;
  };
}