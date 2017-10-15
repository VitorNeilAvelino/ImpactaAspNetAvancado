var Details = {
    leilaoHub: {},
    produtoId: 0,

    inicializar: function (produtoId) {
        this.produtoId = produtoId;
        this.conectarLeilaoHub();
        this.vincularEventos();
    },

    conectarLeilaoHub: function () {
        var connection = $.hubConnection();

        this.leilaoHub = connection.createHubProxy("LeilaoHub");

        connection.start();
    },

    vincularEventos: function () {
        var self = this;
        //$("#entrarButton").on("click", this.entrarLeilao);
        //$("#entrarButton").on("click", self.entrarLeilao);
        //$("#entrarButton").on("click", function () { this.entrarLeilao; });
        //$("#entrarButton").on("click", function () { this.entrarLeilao(); });
        //$("#entrarButton").on("click", function () { self.entrarLeilao; });

        $("#entrarButton").on("click", function () { self.entrarLeilao(); });
        $("#enviarLanceButton").on("click", function () { self.realizarLance(); });

        this.leilaoHub.on("adicionarMensagem", function (remetente, mensagem) {
            self.adicionarMensagem(remetente, mensagem);
        });
    },

    entrarLeilao: function () {
        this.leilaoHub.invoke("Participar", $("#nomeParticipante").val(), this.produtoId);

        $("#participanteDiv").hide();
        $("#lanceDiv").show();
        $("#valorLance").focus();
    },

    adicionarMensagem: function (remetente, mensagem) {
        $("#lancesRealizados").append("<tr><td>" + remetente + "</td><td>" + mensagem + "</td></tr>");
    },

    realizarLance: function () {
        this.leilaoHub.invoke("RealizarLance", $("#nomeParticipante").val(), $("#valorLance").val(), this.produtoId);
    }
};