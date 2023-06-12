using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{
    public class RecuperarController : Controller
    {
        private readonly IEMSenhaRepositorio _recupera;
        public RecuperarController(IEMSenhaRepositorio recupera)
        {
            _recupera = recupera;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enviar(EmailModel recupera)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var novaSenha = recupera.NovaSenha();
                    var assunto = "Recuperação de senha - Sistema Controle de Contatos";
                    var msg = $"<h3>Olá {recupera.Usuario},</h3>" +
                        $"<p>Sua senha foi alterada!</p>" +
                        $"<p>Acesse a página de login com a nova senha: {novaSenha}</p>" +
                        $"<p>Não esqueça de ao realizar o primiero login substituir a senha gerada por uma nova, e havendo " +
                        $"qualquer problema informe o administrador da página.</p>" +
                        $"<p>Atte,</p>" +
                        $"<p>Sistema de Controle de Contatos</p>" +
                        $"{DateTime.Now}";
                    if(_recupera.Enviar(recupera.Email, assunto, msg))
                    {
                        _recupera.AtualizarSenha(recupera, novaSenha);
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        TempData["Fail"] = ($"Ocorreu um erro: {_recupera.mensagem()}");
                    }
                    
                }
                return View("Index");
            }
            catch (System.Exception erro)
            {
                TempData["Fail"] = ("Ocorreu um erro: {0}", erro.Message);
                return RedirectToAction("Index", "Login");
            }
        }

    }
}
