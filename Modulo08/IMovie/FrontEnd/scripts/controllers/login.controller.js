imovie.controller('LoginController', function($scope, authConfig, authService, toastr, $location) {

    if (authService.isAutenticado()) {
        $location.path(authConfig.urlPrivado);
    }

    $scope.login = function(usuario) {
        authService.login(usuario)
            .then(
                function(response) {
                    var t = toastr.success('Bem vindo(a), ' + response.data.dados.nome + ".", 'Imovie');
                    toastr.refreshTimer(t, 2000);
                },
                function(response) {
                    var t = toastr.error('Erro ao acessar sua conta, tente novamente!', 'Imovie');
                    toastr.refreshTimer(t, 2000);
                });
    };

});