using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace Loja.Mvc.Helpers
{
    public static class CulturaHelper
    {
        public const string LinguagemPadrao = "pt-BR";

        private static List<string> LinguagensSuportadas { get; } = 
            new List<string> { "pt-BR", "en-US", "es"};

        public static RegionInfo ObterCultura()
        {
            var linguagem = LinguagemPadrao;
            var linguagemCookie = HttpContext.Current.Request.Cookies["linguagemSelecionada"];

            if (linguagemCookie != null)
            {
                linguagem = LinguagensSuportadas.Contains(linguagemCookie.Value) ? linguagemCookie.Value : LinguagemPadrao;
            }

            var cultura = CultureInfo.CreateSpecificCulture(linguagem);

            return new RegionInfo(cultura.LCID);

            //return Thread.CurrentThread.CurrentCulture.Name; //pt-BR 
            //return Thread.CurrentThread.CurrentCulture.DisplayName; //Português (Brasil) 
            //return Thread.CurrentThread.CurrentCulture.NativeName; //português (Brasil) 
            //return Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName; //por

        }
    }
}