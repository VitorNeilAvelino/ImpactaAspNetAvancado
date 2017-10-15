using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Loja.Mvc.Hubs
{
    public class LeilaoHub : Hub
    {
        public async Task Participar(string nomeParticipante, string produtoId)
        {
            await Groups.Add(Context.ConnectionId, produtoId);
            Clients.Group(produtoId).adicionarMensagem("Administrador", $"{nomeParticipante} entrou ({Context.ConnectionId}).");

            //Clients.All
            //Clients.Caller
            //Clients.Others
            //Clients.Client(connectionId).seuMetodoJs()...
        }

        public void RealizarLance(string nomeParticipante, string valor, string produtoId)
        {
            Clients.Group(produtoId).adicionarMensagem(nomeParticipante, valor);
        }
    }
}