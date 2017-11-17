using System.Collections.Generic;
using System.Linq;

namespace Loja.Dominio
{
    public class Leilao
    {
        private const decimal _descontoMaximo = 0.1m;
        public int Id { get; set; }
        public string NomeLote { get; set; }
        public decimal Valor { get; set; }
        public List<Produto> Produtos { get; set; }

        public List<string> Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(NomeLote))
            {
                erros.Add("Nome do Lote é obrigatório.");
            }

            if (Produtos.Count == 0)
            {
                erros.Add("Selecione os Produtos.");
            }

            var somaProdutos = Produtos.Sum(p => p.Preco);

            if (somaProdutos - Valor > somaProdutos * _descontoMaximo)
            {
                erros.Add("Desconto máximo excedido.");
            }

            return erros;
        }
    }
}