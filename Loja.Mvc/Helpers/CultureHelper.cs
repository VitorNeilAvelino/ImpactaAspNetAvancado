using System.Threading;

namespace Loja.Mvc.Helpers
{
    public static class CultureHelper
    {
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name; //pt-BR 
            //return Thread.CurrentThread.CurrentCulture.DisplayName; //Português (Brasil) 
            //return Thread.CurrentThread.CurrentCulture.NativeName; //português (Brasil) 
        }
    }
}