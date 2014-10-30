angular.module('linkedin.messaging').controller('MessagingCtrl',MessagingCtrl);


MessagingCtrl.$inject =  ['ProfileSvc', '$timeout', '$ionicScrollDelegate']


function MessagingCtrl( ProfileSvc, $timeout, $ionicScrollDelegate) {
	var vm = this;
		 vm.data = {};
  vm.myId = '12345';
  vm.messages = [];


	var alternate,
    isIOS = ionic.Platform.isWebView() && ionic.Platform.isIOS();

  vm.sendMessage = function() {
    alternate = !alternate;
    vm.messages.push({
      userId: alternate ? '12345' : '54321',
      text: vm.data.message
    });
    delete vm.data.message;
    $ionicScrollDelegate.scrollBottom(true);
  }

  vm.inputUp = function() {
    if (isIOS) $scope.data.keyboardHeight = 216;
    $timeout(function() {
      $ionicScrollDelegate.scrollBottom(true);
    }, 300);

  }
  vm.inputDown = function() {
    if (isIOS) vm.data.keyboardHeight = 0;
    $ionicScrollDelegate.resize();
  }
vm.closeKeyboard = function(){
  cordova.plugins.Keyboard.close();
}
 

}