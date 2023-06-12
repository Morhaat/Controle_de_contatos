using ControleDeContatos.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public interface IEMSenhaRepositorio
    {
        UsuarioModel AtualizarSenha(EmailModel usuario, string novaSenha);
        UsuarioModel ListarPorId(int id);
        UsuarioModel BuscaPorLoginEmail(string login, string email);
        bool Enviar(string email, string assunto, string msg);
        string mensagem();
    }
}
