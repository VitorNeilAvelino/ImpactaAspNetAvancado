using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace Loja.Mvc.Helpers
{
    public class CulturaHelper
    {
        private const string LinguagemPadrao = "pt-BR";
        private string _linguagemSelecionada = LinguagemPadrao;

        public CulturaHelper()
        {
            DefinirLinguagemPadrao();
            ObterRegionInfo();
        }

        private List<string> LinguagensSuportadas { get; } = new List<string> { "pt-BR", "en-US", "es" };

        public string NomeNativo { get; set; }

        public string Abreviacao { get; set; }

        private void DefinirLinguagemPadrao()
        {
            var request = HttpContext.Current.Request;

            if (request.Cookies["LinguagemSelecionada"] != null)
            {
                _linguagemSelecionada = request.Cookies["LinguagemSelecionada"].Value;
                return;
            }

            if (request.UserLanguages != null && LinguagensSuportadas.Contains(request.UserLanguages[0]))
            {
                _linguagemSelecionada = request.UserLanguages[0];
            }

            var cookie = new HttpCookie("LinguagemSelecionada", _linguagemSelecionada);
            cookie.Expires = DateTime.MaxValue;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private void ObterRegionInfo()
        {
            var cultura = CultureInfo.CreateSpecificCulture(_linguagemSelecionada);
            var regiao = new RegionInfo(cultura.LCID);

            NomeNativo = regiao.NativeName;
            Abreviacao = regiao.TwoLetterISORegionName.ToLower();

            //return Thread.CurrentThread.CurrentCulture.Name; //pt-BR 
            //return Thread.CurrentThread.CurrentCulture.DisplayName; //Português (Brasil) 
            //return Thread.CurrentThread.CurrentCulture.NativeName; //português (Brasil) 
            //return Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName; //por
        }

        public static CultureInfo ObterCultureInfo()
        {
            var linguagemSelecionada = HttpContext.Current.Request.Cookies["LinguagemSelecionada"];
            var linguagem = linguagemSelecionada?.Value ?? LinguagemPadrao;

            return CultureInfo.CreateSpecificCulture(linguagem);
        }
    }
}