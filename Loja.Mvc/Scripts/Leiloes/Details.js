var Details = {
    leilaoHub: {},
    produtoId: 0,
    connectionId: "",
    nomeParticipante: "",

    inicializar: function (produtoId) {
        this.produtoId = produtoId;        
        this.conectarLeilaoHub();
        this.vincularEventos();
    },

    conectarLeilaoHub: function () {
        var self = this;
        var connection = $.hubConnection();

        self.leilaoHub = connection.createHubProxy("LeilaoHub");

        connection.start().done(function () {
            self.connectionId = connection.id;
        });
    },

    vincularEventos: function () {
        var self = this;

        // Não funciona...
        //$("#entrarButton").on("click", this.entrarLeilao);
        //$("#entrarButton").on("click", self.entrarLeilao);
        //$("#entrarButton").on("click", function () { this.entrarLeilao; });
        //$("#entrarButton").on("click", function () { this.entrarLeilao(); });
        //$("#entrarButton").on("click", function () { self.entrarLeilao; });

        //Funciona!
        //$("#entrarButton").on("click", this.entrarLeilao.bind(this));

        $("#entrarButton").on("click", function () { self.entrarLeilao(); });
        $("#enviarLanceButton").on("click", function () { self.realizarLance(); });

        this.leilaoHub.on("adicionarMensagem", function (remetente, connectionId, mensagem) {
            self.adicionarMensagem(remetente, connectionId, mensagem);
        });

        // Elemento não existe ainda no DOM.
        //$("a[data-connectionId]").on("click", function () { self.enviarLike($(this).data("connectionId")); });
        $(document).on("click", "a[data-connection-id]", function () { self.enviarLike($(this).data("connection-id")); });

        this.leilaoHub.on("receberLike", function (nomeRemetente) {
            self.receberLike(nomeRemetente);
        });
    },

    entrarLeilao: function () {
        this.nomeParticipante = $("#nomeParticipante").val();

        this.leilaoHub.invoke("Participar", this.nomeParticipante, this.produtoId);

        $("#participanteDiv").hide();
        $("#lanceDiv").show();
        $("#valorLance").focus();
    },

    adicionarMensagem: function (nomeRemetente, connectionId, mensagem) {
        $("#lancesRealizadosTable").append(this.montarMensagem(nomeRemetente, connectionId, mensagem));

        var lancesRealizadosDiv = $("#lancesRealizadosDiv");
        lancesRealizadosDiv.animate({ scrollTop: lancesRealizadosDiv.prop("scrollHeight") }, 500);
    },

    montarMensagem: function (nomeRemetente, connectionId, mensagem) {
        var tr = "<tr>";
        tr += "<td>" + nomeRemetente + "</td>";
        tr += "<td>" + mensagem + "</td>";

        var like = "<a data-connection-id='" + connectionId + "' href='#'>" +
                    "<span class='glyphicon glyphicon-thumbs-up' style='font-size:18px'></span></a>";
        var enviadaPorMim = this.connectionId === connectionId;

        tr += "<td>" + (enviadaPorMim ? "" : like) + "</td>";

        tr += "</tr>";

        return tr;
    },

    realizarLance: function () {
        this.leilaoHub.invoke("RealizarLance", this.nomeParticipante, $("#valorLance").val(), this.produtoId);
    },

    enviarLike: function (connectionIdDestinatario) {
        this.leilaoHub.invoke("EnviarLike", this.nomeParticipante, connectionIdDestinatario);
    },

    receberLike: function (nomeRemetente) {
        $("#sinoNotificacoes")
            .popover("destroy")
            .popover({
                content: "<span class='glyphicon glyphicon-thumbs-up' style='font-size:24px'></span>",
                html: true,
                placement: "left",
                title: nomeRemetente + " diz:"
            })
            .popover("show");
    }
};