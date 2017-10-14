using Microsoft.AspNet.SignalR;

namespace Loja.Mvc.Hubs
{
    public class LeilaoHub : Hub
    {
        public void Participar(string produtoId)
        {
            //Apenas o grupo que está na página do produto?
            Groups.Add(Context.ConnectionId, produtoId);
            Clients.Group(produtoId).adicionarMensagem(Context.User.Identity.Name + " entrou.");

            //Clients.All.atualizarListaCompradoresLogados();

            //Clients.All
            //Clients.Caller
            //Clients.Others
        }
    }
}