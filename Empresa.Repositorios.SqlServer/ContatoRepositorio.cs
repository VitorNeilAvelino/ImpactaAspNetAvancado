using System.Collections.Generic;
using System.Linq;
using Empresa.Dominio;

namespace Empresa.Repositorios.SqlServer
{
    public class ContatoRepositorio : RepositorioBase<Contato>
    {
        public ContatoRepositorio(EmpresaDbContext contexto) : base(contexto)
        {
                
        }

        public List<Contato> SelecionarPorAssunto(string assunto)
        {
            return Contexto.Contatos.Where(c => c.Assunto.Contains(assunto)).ToList();
        }
    }
}