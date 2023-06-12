using ControleDeContatos.Enums;
using ControleDeContatos.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Atenção! Digite o nome do usuário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Atenção! Digite o login do usuário.")]
        public string Login { get; set;}

        [Required(ErrorMessage = "Atenção! Digite o e-mail do usuário.")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido")]
        public string Email { get; set;}

        
        public PerfilEnum Perfil { get; set;}

        public string Senha { get; set;}

        public DateTime DataCadastro { get; set;}

        public DateTime? DataAtualizacao { get; set;}

        public virtual List<ContatoModel> Contatos{ get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public string SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
