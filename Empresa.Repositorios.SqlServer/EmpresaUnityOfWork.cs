using System;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Repositorios.SqlServer
{
    public class EmpresaUnityOfWork : IDisposable
    {
        private readonly EmpresaDbContext _contexto;
        public EmpresaUnityOfWork(DbContextOptions dbContextOptions)
        {
            _contexto =  new EmpresaDbContext(dbContextOptions);

            Contatos = new ContatoRepositorio(_contexto);
        }

        public ContatoRepositorio Contatos { get; set; }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
