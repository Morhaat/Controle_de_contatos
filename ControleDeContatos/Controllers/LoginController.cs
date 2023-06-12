using ControleDeContatos.Filters;
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
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(ISessao sessao, IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoveSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Esqueci()
        {
            return RedirectToAction("Index", "Recuperar");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscaPorLogin(loginModel.Login);
                    if(usuario != null && usuario.SenhaValida(loginModel.Senha))
                    {
                        _sessao.CriarSessaoDoUsuario(usuario);
                        var session = _sessao.BuscarToken();
                        TempData["Success"] = $"{session}";
                        return RedirectToAction("Index", "Home");
                    }

                    TempData["Fail"] = $"Login e/ou senha incorretos!";
                    return View("Index");

                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["Fail"] = $"Erro ao efetuar o login no usuário: {erro.Message}";
                return View("Index");
            }
        }
    }
}
