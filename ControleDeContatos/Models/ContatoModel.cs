using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {

        public int Id { get; set; }
        [Required (ErrorMessage ="Atenção! Digite o nome do contato.")]
        public string Nome { get; set; }
        [Required (ErrorMessage = "Atenção! Digite o e-mail do contato.")]
        [EmailAddress (ErrorMessage ="Endereço de e-mail inválido")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Atenção! Digite o celular do contato.")]
        [Phone (ErrorMessage ="Numero de teefone inválido")]
        public string Celular { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
