using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Empresa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Repositorios.SqlServer
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        protected readonly EmpresaDbContext Contexto;
        private readonly DbSet<T> _dbSet;

        public RepositorioBase(EmpresaDbContext contexto)
        {
            Contexto = contexto;
            _dbSet = Contexto.Set<T>();
        }

        public void Adicionar(T entidade)
        {
            _dbSet.Add(entidade);
        }

        public void Atualizar(T entidade)
        {
            var entry = Contexto.Entry(entidade);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entidade);
            }

            entry.State = EntityState.Modified;
        }
        public List<T> Obter()
        {
            return _dbSet.ToList();
        }

        public List<T> Obter(Expression<Func<T, bool>> query)
        {
            return _dbSet.Where(query).ToList();
        }

        public T Obter(int id)
        {
            return _dbSet.Find(id);
        }

        public void Remover(int id)
        {
            var entidade = _dbSet.Find(id);

            _dbSet.Remove(entidade);
        }
    }
}