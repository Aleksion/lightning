angular.module('linkedin.controllers').controller('LoginCtrl', LoginCtrl);


LoginCtrl.$inject = ['store', '$state', 'auth']


function LoginCtrl(store, $state, auth) {

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

      
      store.set('profile', auth.profile);
      store.set('token', auth.idToken);
      store.set('refreshToken', auth.refreshToken);
      $state.go('home_landing');
  }, function() {
    console.log("Error :(", err);
  });
  
  };
}