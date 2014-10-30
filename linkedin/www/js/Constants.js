angular.module('linkedin')
/*
The config constant contains all constants required to run the service. Set these depending on the setup you are running on.
 */
.constant('CONFIG', {
    apiUrl: "http://localhost:59996/api/",
    //apiUrl: "http://coffeein.azure-mobile.net/api/"
})
.constant('AUTH_EVENTS', {
    loginSuccess: 'auth-login-success',
    loginFailed: 'auth-login-failed',
    logoutSuccess: 'auth-logout-success',
    sessionTimeout: 'auth-session-timeout',
    notAuthenticated: 'auth-not-authenticated',
    notAuthorized: 'auth-not-authorized'
})
.constant('USER_ROLES', {
    all: '*',
    admin: 'admin',
    editor: 'editor',
    guest: 'guest'
});