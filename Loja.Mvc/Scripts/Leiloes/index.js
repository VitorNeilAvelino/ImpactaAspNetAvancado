var Index = {
    inicializar: function () {
        this.conectarLeilaoHub();
        ko.applyBindings(this.viewModel);
        this.getOfertas();
    },

    conectarLeilaoHub: function () {
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("LeilaoHub");

        hub.on("atualizarOfertas", this.getOfertas.bind(this));

        connection.start();
    },

    atualizarOfertas: function (produtos) {
        //document.location.reload();

        //this.viewModel.produtos.push(
        //    { id: 1, nome: "Grampeador", preco: 18.51, estoque: 51, categoriaNome: "Papelaria" }
        //);

        this.viewModel.produtos(produtos);
    },

    viewModel: {
        produtos: ko.observableArray()
    },

    getOfertas: function () {
        var self = this;

        return $.getJSON("/api/Vendas/Leiloes", function (produtos) {
            self.atualizarOfertas(produtos);
        });
    }
};