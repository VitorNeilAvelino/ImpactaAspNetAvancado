using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Loja.Dominio.Tests
{
    [TestClass()]
    public class LeilaoTests
    {
        [TestMethod()]
        public void ValidarSucessoTest()
        {
            //Arranja
            var leilao = new Leilao();
            leilao.NomeLote = "NomeLote";
            leilao.Valor = 90;
            leilao.Produtos = new List<Produto> { new Produto { Preco = 100 } };

            //Age
            var erros = leilao.Validar();

            //Afirma
            Assert.IsTrue(erros.Count == 0);
        }

        [TestMethod]
        public void ValidarErroDescontoTeste()
        {
            //Arranja
            var leilao = new Leilao();
            leilao.NomeLote = "NomeLote";
            leilao.Valor = 80;
            leilao.Produtos = new List<Produto> { new Produto { Preco = 100 } };

            //Age
            var erros = leilao.Validar();

            //Afirma
            Assert.IsTrue(erros.Contains("Desconto máximo excedido."));
        }
    }
}