using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{
    [PaginaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEMSenhaRepositorio _email;
        private readonly IContatoRepositorio _contatoRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IEMSenhaRepositorio email, IContatoRepositorio contatoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _email = email;
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuario = _usuarioRepositorio.BuscarTodos();
            return View(usuario);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                if (_usuarioRepositorio.Deletar(id))
                {
                    TempData["Success"] = "Usuário deletado com sucesso!";
                }
                else
                {
                    TempData["Fail"] = "Algo de errado não está certo";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["Fail"] = ("Ocorreu um erro: {0}", erro.Message);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult ListarContatosPorUsuarioId(int id)
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(id);
            return PartialView("_ContatosUsuario", contatos);
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var assunto = "Boas vindas - Sistema Controle de Contatos";
                    var msg = $"<h3>Olá {usuario.Nome},</h3>" +
                        $"<p>Você acaba de ser registrado em nosso sistema, seja bem vindo(a).</p>" +
                        $"<p>Acesse a página do Sistema de controle de contatos e realize seu login com as seguintes informações:<br>" +
                        $"Usuário: {usuario.Login};<br>" +
                        $"Senha: {usuario.Senha};</p>" +
                        $"<p>Não esqueça de ao realizar o primiero login substituir a senha gerada por uma nova, e havendo " +
                        $"qualquer problema informe o administrador da página.</p>" +
                        $"<p>Atte,</p>" +
                        $"<p>Sistema de Controle de Contatos</p>" +
                        $"{DateTime.Now}";
                    _email.Enviar(usuario.Email, assunto, msg);
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["Success"] = "Usuário Cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["Fail"] = ("Ocorreu um erro: {0}", erro.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Success"] = "Contato alterado com sucesso!";
                    _usuarioRepositorio.Atualizar(usuario);
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);
            }
            catch (System.Exception erro)
            {
                TempData["Fail"] = ("Ocorreu um erro: {0}", erro.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
