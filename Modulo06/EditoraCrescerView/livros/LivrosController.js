app.controller("LivrosController", function($scope, LivrosService) {

    $scope.salvar = salvar;
    $scope.remover = remover;
    obterTodosOsLivros();

    //Paginação

    function paginacao() {
        $http({
                url: urlBase,
                method: 'GET',
                params: parametros
            })
            .then(function(response) {
                $scope.livros = response.data.dados;
            });
    }

    //Salvar e Editar
    function salvar(livro) {
        let promise = {};

        if (angular.isDefined(livro.Isbn)) {
            promise = LivrosService.atualizar(livro);
            promise.then(function(response) {
                obterTodosOsLivros();
                swal("Sucesso!", "Livro editado com sucesso!", "success")
            });
        } else {
            promise = LivrosService.criar(livro);
            promise.then(function(response) {
                obterTodosOsLivros();
                swal("Sucesso!", "Livro cadastrado com sucesso!", "success")
            });
        }

        $scope.novoLivro = {};
    }

    //Remover
    function remover(livro) {
        LivrosService.remover(livro).then(function() {
            swal("Apagado!", "Livro excluído com sucesso!", "success");
            obterTodosOsLivros();
        })
        sweetAlert("Oops...", "Alguma falha ocorreu!", "error");
    }

    //Listagens

    //ListarTodos
    function obterTodosOsLivros() {
        LivrosService.obterTodosOsLivros().then(function(response) {
            console.log(response);
            $scope.livros = response.data.dados;
        });
    }

    //Lançamento
    function obterLivroLancamentos() {
        LivrosService.obterLivroLancamentos().then(function(response) {
            console.log(response);
            $scope.lancamentos = response.data.dados;
        });
    }

    //Por Isbn
    function obterLivroPorIsbn(Isbn) {
        LivrosService.obterLivroPorIsbn(Isbn).then(function(response) {
            console.log(response);
            $scope.livroPorIsbn = response.data.dados;
        });
    }

    //Por Genero
    function obterLivroPorGenero(genero) {
        LivrosService.obterLivroPorGenero(genero).then(function(response) {
            console.log(response);
            $scope.livroPorGenero = response.data.dados;
        });
    }

})