using Microsoft.AspNet.SignalR;

namespace Loja.Mvc.Hubs
{
    public class LeilaoHub : Hub
    {
        public void AdicionarComprador(string nome)
        {
            //Apenas o grupo que está na página do produto?
            Clients.All.atualizarListaCompradoresLogados();

            //Clients.Caller
            //Clients.Others
        }
    }
}