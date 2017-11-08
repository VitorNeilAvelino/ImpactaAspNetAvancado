var Index = {
    inicializar: function () {
        this.conectarLeilaoHub();
    },

    conectarLeilaoHub: function(){
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("LeilaoHub");

        //"ATUALIZARLISTA": cusiosamente, não é case sensitive.
        hub.on("atualizarOfertas", this.atualizarOfertas.bind(this));

        connection.start();
    },

    atualizarOfertas: function(){
        document.location.reload();
    }
};