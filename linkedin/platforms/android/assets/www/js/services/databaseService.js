/**
* DatabaseService Module
*
* Description
*/
angular.module('linkedin.services').provider('BackendService', BackendServiceProvider);


BackendServiceProvider.$inject = [];

function BackendServiceProvider () {

	var baseUrl = null
	var backend = null;
	this.setBaseUrl = function(newBaseUrl) {
        baseUrl = newBaseUrl;
    };

    this.setBackendType = function(newBackend){
    	backend = newBackend;
    }

	this.$get = [function(){
		return new BackendService(baseUrl, backend);
	}];
}


function BackendService(baseUrl, backend) {

	var backendService = this;

	backend.intialize(baseUrl);

	//to access discrete options
	backendService.backend = backend;

	backendService.getCurrentUser = function(){
		return backend.getCurrentUser();
	}


}


