var Index = {
    inicializar: function () {
        this.conectarLeilaoHub();
        ko.applyBindings(this.viewModel);
    },

    conectarLeilaoHub: function () {
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("LeilaoHub");

        //"ATUALIZARLISTA": cusiosamente, não é case sensitive.
        hub.on("atualizarOfertas", this.atualizarOfertas.bind(this));

        connection.start();
    },

    atualizarOfertas: function () {
        //document.location.reload();
        this.viewModel.produtos.push(
            { id: 1, nome: "Grampeador", preco: 18.51, estoque: 51, categoriaNome: "Papelaria" }
        );
    },

    viewModel: {
        produtos: ko.observableArray()
    }
};