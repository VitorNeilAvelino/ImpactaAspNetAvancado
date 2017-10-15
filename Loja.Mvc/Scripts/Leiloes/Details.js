var Details = {
    leilaoHub: {},
    produtoId: 0,
    connectionId: "",

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
        //$("#entrarButton").on("click", this.entrarLeilao);
        //$("#entrarButton").on("click", self.entrarLeilao);
        //$("#entrarButton").on("click", function () { this.entrarLeilao; });
        //$("#entrarButton").on("click", function () { this.entrarLeilao(); });
        //$("#entrarButton").on("click", function () { self.entrarLeilao; });

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
        this.leilaoHub.invoke("Participar", $("#nomeParticipante").val(), this.produtoId);

        $("#participanteDiv").hide();
        $("#lanceDiv").show();
        $("#valorLance").focus();
    },

    adicionarMensagem: function (nomeRemetente, connectionId, mensagem) {
        $("#lancesRealizadosTable").append(this.montarMensagem(nomeRemetente, connectionId, mensagem));
        $('#lancesRealizadosDiv').animate({ scrollTop: $('#lancesRealizadosDiv').prop('scrollHeight') }, 500);
    },

    montarMensagem: function (nomeRemetente, connectionId, mensagem) {
        var tr = "<tr>"
        tr += "<td>" + nomeRemetente + "</td>";
        tr += "<td>" + mensagem + "</td>";

        var like = "<a data-connection-id='" + connectionId + "' href='#'>" +
                    "<span class='glyphicon glyphicon-thumbs-up' style='font-size:18px'></span></a>";
        var enviadaPorMim = this.connectionId == connectionId;

        tr += "<td>" + (enviadaPorMim ? "" : like) + "</td>";

        return tr += "</tr>"
    },

    realizarLance: function () {
        this.leilaoHub.invoke("RealizarLance", $("#nomeParticipante").val(), this.connectionId,
            $("#valorLance").val(), this.produtoId);
    },

    enviarLike: function (connectionIdDestinatario) {
        this.leilaoHub.invoke("EnviarLike", $("#nomeParticipante").val(), connectionIdDestinatario);
    },

    receberLike: function (nomeRemetente) {
        $("#sinoNotificacoes")
            .popover("destroy")
            .popover({
                content: "<span class='glyphicon glyphicon-thumbs-up' style='font-size:24px'></span>",
                html: true,
                placement: 'left',
                title: nomeRemetente + " diz"                
            })
            .popover("show");
    }
};