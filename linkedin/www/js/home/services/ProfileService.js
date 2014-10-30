angular.module('linkedin.home').factory('ProfileSvc',ProfileSvc);


ProfileSvc.$inject =  ['auth', 'DataService']


function ProfileSvc(auth, DataService) {
	
  
  return {
  	getPotentialMatches: function(){
  		return DataService.getList("profiles");
	},
  	likeUser: function(likedUserId){
  		var like = {likedUser: likedUserId}
  		return DataService.insert(like, "like")
  	},
  	dislikeUser: function(dislikedUserId){
  		var dislike = {dislikedUser: dislikedUserId}
  		return DataService.insert(dislike, "dislike")
  	},
  	getMatches: function(){
  		return DataService.getList("match");
  	}

  }
}