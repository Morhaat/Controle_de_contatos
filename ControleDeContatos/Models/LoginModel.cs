using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o usuário!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite a senha!")]
        public string Senha { get; set; }
    }
}
