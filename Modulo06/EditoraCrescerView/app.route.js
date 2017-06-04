app.config(function($routeProvider) {

    $routeProvider
        .when("/home", {
            templateUrl: "/home/home.html"
        })
        .when("/login", {
            controller: "LoginController",
            templateUrl: "/login/login.html"
        })
        .when("/administrativa", {
            controller: "LivrosController",
            templateUrl: "/administrativa/administrativa.html",
            resolve: {
                autenticado: function(authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })
        .otherwise({
            redirectTo: "/home"
        });
});