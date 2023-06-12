using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class EmailModel
    {
        [Required(ErrorMessage = "Digite um usuário válido") ]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        public string NovaSenha()
        {
            var rdn = new Random();
            int nextRdn = rdn.Next(1000, 9999);
            string novaSenha = nextRdn.ToString();
            return novaSenha;
        }
    }
}
