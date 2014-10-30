/**
* FirebaseSvc Module
*
* Description
*/
angular.module('linkedin.services').provider('FirebaseService', FirebaseServiceProvider);


FirebaseServiceProvider.$inject = [];

function FirebaseServiceProvider () {

	var baseUrl = null
	this.setBaseUrl = function(newBaseUrl) {
        baseUrl = newBaseUrl;
    };

	this.$get = [function(){
		return new FirebaseService(baseUrl);
	}];
}


function FirebaseService(baseUrl) {

	var fireBaseService = this;
	fireBaseService.ref = new Firebase(baseUrl);


}
