angular.module('linkedin.controllers').controller('ContentCtrl',ContentCtrl);


ContentCtrl.$inject =  ['auth']


function ContentCtrl(auth) {
	var vm = this;
	auth.getProfile().then(function(profile){
		vm.profile = profile;
		
	console.log(vm.profile);
	});
}