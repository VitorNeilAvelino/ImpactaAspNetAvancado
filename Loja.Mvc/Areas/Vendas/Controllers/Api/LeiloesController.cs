using System.Linq;
using System.Web.Http;
using Loja.Mvc.Helpers;
using Loja.Repositorios.SqlServer.EF;

namespace Loja.Mvc.Areas.Vendas.Controllers.Api
{
    public class LeiloesController : ApiController
    {
        private readonly LojaDbContext _db = new LojaDbContext();

        public IHttpActionResult Get()
        {
            return Ok(Mapeamento.Mapear(_db.Produtos.Where(p => p.EmLeilao).ToList()));
        }
    }
}