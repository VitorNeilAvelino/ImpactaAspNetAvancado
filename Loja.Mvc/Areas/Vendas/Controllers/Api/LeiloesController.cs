using System.Linq;
using System.Web.Http;
using Loja.Mvc.Helpers;
using Loja.Repositorios.SqlServer.EF;
using System.Net.Http.Formatting;

namespace Loja.Mvc.Areas.Vendas.Controllers.Api
{
    public class LeiloesController : ApiController
    {
        private readonly LojaDbContext _db = new LojaDbContext();

        public IHttpActionResult Get()
        {
            return Ok(Mapeamento.Mapear(_db.Produtos.Where(p => p.EmLeilao).ToList()));
        }

        public IHttpActionResult Post(FormDataCollection formulario)
        {
            return CreatedAtRoute("VendasDefaultApi", new { id = formulario["lote"] },  formulario);
        }
    }
}