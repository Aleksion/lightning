angular.module('linkedin.authentication').controller('LoginCtrl', LoginCtrl);


LoginCtrl.$inject = ['store', '$state', 'auth', 'DataService', 'Restangular']


function LoginCtrl(store, $state, auth, DataService, Restangular) {

  var vm = this;

  vm.login = function() {
    auth.signin({
      popup: true,
    // Make the widget non closeable
    standalone: true,

    device: 'Phone',
    authParams: {
      scope: 'openid offline_access'
    }
  },function() {
    auth.getToken({targetClientId: "znuVI4v98d3IvLipExvIurQpzkRG68JZ"}).then(function(result){

     var userId = auth.profile.user_id.split("|")[1];
     DataService.setAccountId(userId);

     Restangular.setDefaultHeaders({"X-ZUMO-AUTH": result})
     Restangular.setDefaultHeaders({"X-ZUMO-APPLICATION":"SvNUywaXLojSLqizEHysXMmCcpmxSW41"});



     DataService.get(userId, "user").then(function(result){
      var test = result;

      store.set('profile', auth.profile);
      store.set('token', auth.idToken);
      store.set('refreshToken', auth.refreshToken);
      $state.go('home_landing');
    },function(error){
      if(error.status == 404){
        DataService.insertAtBase({id: userId}, "user").then(function(user){
          DataService.insert(auth.profile,"profile").then(function(result){
             store.set('profile', auth.profile);
      store.set('token', auth.idToken);
      store.set('refreshToken', auth.refreshToken);
      $state.go('home_landing');
        }, function(error){
          var er = error;
        });
        }, function(error){
          var err = error;
        })
        
      }
    });
   });

  }, function() {
    console.log("Error :(", err);
  });

  };
}