﻿using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace Loja.Mvc.Helpers
{
    public class CulturaHelper
    {
        public CulturaHelper()
        {
            ObterRegiao();
        }

        public const string LinguagemPadrao = "pt-BR";

        public string NomeNativo { get; set; }

        public string Abreviacao { get; set; }

        public CultureInfo CultureInfo { get; set; }
        
        private List<string> LinguagensSuportadas { get; } =
            new List<string> { "pt-BR", "en-US", "es" };

        private void ObterRegiao()
        {
            var linguagem = LinguagemPadrao;
            var linguagemSelecionada = HttpContext.Current.Request.Cookies["linguagemSelecionada"];

            if (linguagemSelecionada != null)
            {
                linguagem = LinguagensSuportadas.Contains(linguagemSelecionada.Value) ?
                    linguagemSelecionada.Value :
                    LinguagemPadrao;
            }

            var cultura = CultureInfo.CreateSpecificCulture(linguagem);

            this.CultureInfo = cultura;

            var regiao = new RegionInfo(cultura.LCID);

            NomeNativo = regiao.NativeName;
            Abreviacao = regiao.TwoLetterISORegionName.ToLower();

            //return Thread.CurrentThread.CurrentCulture.Name; //pt-BR 
            //return Thread.CurrentThread.CurrentCulture.DisplayName; //Português (Brasil) 
            //return Thread.CurrentThread.CurrentCulture.NativeName; //português (Brasil) 
            //return Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName; //por
        }
    }
}