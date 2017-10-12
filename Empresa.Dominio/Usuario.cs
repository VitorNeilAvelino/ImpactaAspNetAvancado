//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Empresa.Dominio
{
    public class Usuario //: IdentityUser
    {
        // Você pode criar suas próprias propriedades.
        //public int MyProperty { get; set; }

        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; } 
    }
}