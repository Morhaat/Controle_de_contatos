using ControleDeContatos.Data;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public class EMSenhaRepositorio : IEMSenhaRepositorio
    {
        private readonly IConfiguration _configuration;
        private readonly BancoContext _bancocontext;
        public string mensagem;

        public EMSenhaRepositorio(BancoContext bancocontext, IConfiguration configuration)
        {
            _bancocontext = bancocontext;
            _configuration = configuration;
        }

        public UsuarioModel AtualizarSenha(EmailModel usuario, string novaSenha)
        {
            UsuarioModel usuarioDb = BuscaPorLoginEmail(usuario.Usuario, usuario.Email);
            if(usuarioDb == null) throw new Exception("Houve um erro na atualização do usuário!");

            usuarioDb.Senha = novaSenha.GerarHash();
            usuarioDb.DataAtualizacao = DateAndTime.Now;
            _bancocontext.Usuarios.Update(usuarioDb);
            _bancocontext.SaveChanges();

            return usuarioDb;

        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancocontext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel BuscaPorLoginEmail(string login, string email)
        {
            return _bancocontext.Usuarios.FirstOrDefault(x => x.Login == login && x.Email == email);
        }

        public bool Enviar(string email, string assunto, string msg)
        {
                try
                {
                    string host = _configuration.GetValue<string>("SMTP:Host");
                    string nome = _configuration.GetValue<string>("SMTP:Nome");
                    string username = _configuration.GetValue<string>("SMTP:Username");
                    string senha = _configuration.GetValue<string>("SMTP:Senha");
                    int porta = _configuration.GetValue<int>("SMTP:Porta");

                    MailMessage mail = new MailMessage()
                    {
                        From = new MailAddress(username, nome)
                    };
                    mail.To.Add(email);
                    mail.Subject = assunto;
                    mail.Body = msg;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    using (SmtpClient smtp = new SmtpClient(host, porta))
                    {
                        smtp.Credentials = new NetworkCredential(username, senha);
                        smtp.EnableSsl = true;

                        smtp.Send(mail);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    mensagem = e.Message;
                    return false;
                }
        }

        string IEMSenhaRepositorio.mensagem()
        {
            return mensagem;
        }
    }
}