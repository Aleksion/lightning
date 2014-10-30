angular.module('linkedin.core.services').provider('DataService',DataServiceProvider);

DataServiceProvider.$inject = ["RestangularProvider"]

function DataServiceProvider (RestangularProvider){

	var accountBasedEndpoint = false;
	var accountBasedEndpointPath = null;
	var restangular = false;


    this.setBaseUrl = function(newBaseUrl) {
        RestangularProvider.setBaseUrl(newBaseUrl);
    };
    this.useAccountBasedEndpoint = function(useAccountBasedEndpoint) {
        accountBasedEndpoint = useAccountBasedEndpoint;
    };
    this.setAccountBasedEndpointPath = function(endpointPath) {
        accountBasedEndpointPath = endpointPath;
    };

    this.useRestangular = function(useRestangular) {
        restangular = useRestangular;
    };
	this.$get = ["Restangular", function(Restangular){
		return new DataService(Restangular, accountBasedEndpoint, accountBasedEndpointPath, restangular);
	}];
}


function DataService(Restangular, accountBasedEndpoint, accountBasedEndpointPath, restangular) {




	if(restangular){
		this.dataContext = Restangular;
	}
	if(accountBasedEndpoint){
		//If this is set to true, you have to call setAccountId
		this.useAccountBasedEndpoints = accountBasedEndpoint;
		this.accountBasedEndpointPath = accountBasedEndpointPath;
	}


    this.setAccountId = function(accountId) {
        this.dataContext.account = Restangular.one(this.accountBasedEndpointPath, accountId);
    }; // body...


}

DataService.prototype.setResourceName = function(resourceName) {
    this.resourceName = resourceName;
};

DataService.prototype.getList = function(resourceName){
	if(this.useAccountBasedEndpoints){
		return this.dataContext.account.all(resourceName || this.resourceName).getList();
	}else{
		return this.dataContext.all(this.resourceName).getList();
	}
}

DataService.prototype.get = function(id, resourceName){
	return this.dataContext.one(resourceName || this.resourceName, id).get();
}

DataService.prototype.update = function(id, content, resourceName){
	return this.dataContext.all(resourceName ||this.resourceName).customPUT(content, id);
}

DataService.prototype.delete = function(id, content, resourceName){
	return this.dataContext.one(resourceName || this.resourceName, id).delete();
}

DataService.prototype.insert = function(content, resourceName){
	if(this.useAccountBasedEndpoints){
		return this.dataContext.account.all(resourceName ||this.resourceName).customPOST(content);
	}else{
		return this.dataContext.all(resourceName ||this.resourceName).customPOST(content);
	}
}

DataService.prototype.insertAtBase = function(content, resourceName){
		return this.dataContext.all(resourceName ||this.resourceName).customPOST(content);
}